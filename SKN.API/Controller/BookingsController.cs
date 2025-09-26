using Microsoft.AspNetCore.Mvc;
using SKN.Core.DTOs;
using SKN.Core.Interfaces;
using SKN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SKN.Core.Interface;

namespace SKN.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingRepository _bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // GET: api/bookings
        [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
    {
        var bookings = await _bookingRepository.GetBookingsWithDetailsAsync();
        
        var bookingDtos = bookings.Select(b => new BookingDto
        {
            Id = b.Id,
            CheckInDate = b.CheckInDate,
            CheckOutDate = b.CheckOutDate,
            NumberOfGuests = b.NumberOfGuests,
            TotalPrice = b.TotalPrice,
            Status = b.Status,
            CreatedAt = b.CreatedAt,
            UserName = $"{b.User.FirstName} {b.User.LastName}",
            HotelName = b.RoomType.Hotel.Name,
            RoomTypeName = b.RoomType.Name
        });

        return Ok(bookingDtos);
    }

        // GET: api/bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            var bookingDto = new BookingDto
            {
                Id = booking.Id,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfGuests = booking.NumberOfGuests,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status,
                CreatedAt = booking.CreatedAt,
                UserName = $"{booking.User.FirstName} {booking.User.LastName}",
                HotelName = booking.RoomType.Hotel.Name,
                RoomTypeName = booking.RoomType.Name
            };

            return bookingDto;
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingDto createBookingDto)
        {
            // الحصول على نوع الغرفة لحساب السعر
            var roomType = await _unitOfWork.Repository<RoomType>().GetByIdAsync(createBookingDto.RoomTypeId);
            if (roomType == null)
            {
                return BadRequest("Room type not found");
            }

            // حساب المبلغ الإجمالي
            var numberOfNights = (createBookingDto.CheckOutDate - createBookingDto.CheckInDate).Days;
            var totalPrice = roomType.PricePerNight * numberOfNights;

            var booking = new Booking
            {
                CheckInDate = createBookingDto.CheckInDate,
                CheckOutDate = createBookingDto.CheckOutDate,
                NumberOfGuests = createBookingDto.NumberOfGuests,
                TotalPrice = totalPrice,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UserId = createBookingDto.UserId,
                RoomTypeId = createBookingDto.RoomTypeId
            };

            await _unitOfWork.Repository<Booking>().AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            var bookingDto = new BookingDto
            {
                Id = booking.Id,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfGuests = booking.NumberOfGuests,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status,
                CreatedAt = booking.CreatedAt
            };

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, bookingDto);
        }

        // PUT: api/bookings/5 (لتحديث حالة الحجز فقط)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingStatus(int id, UpdateBookingDto updateBookingDto)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = updateBookingDto.Status;
            _unitOfWork.Repository<Booking>().Update(booking);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Booking>().Delete(booking);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}