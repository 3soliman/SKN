using Microsoft.AspNetCore.Mvc;
using SKN.Core.DTOs;
using SKN.Core.Interfaces;
using SKN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SKN.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _unitOfWork.Repository<Hotel>().GetAllAsync();
            
            var hotelDtos = hotels.Select(h => new HotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description,
                Address = h.Address,
                City = h.City,
                Latitude = h.Latitude,
                Longitude = h.Longitude,
                Amenities = h.HotelAmenities?.Select(ha => ha.Amenity.Name).ToList() ?? new List<string>(),
                ImageUrls = h.HotelImages?.Select(hi => hi.ImageUrl).ToList() ?? new List<string>()
            });

            return Ok(hotelDtos);
        }

        // GET: api/hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _unitOfWork.Repository<Hotel>().GetByIdAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDto = new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                Address = hotel.Address,
                City = hotel.City,
                Latitude = hotel.Latitude,
                Longitude = hotel.Longitude,
                Amenities = hotel.HotelAmenities?.Select(ha => ha.Amenity.Name).ToList() ?? new List<string>(),
                ImageUrls = hotel.HotelImages?.Select(hi => hi.ImageUrl).ToList() ?? new List<string>()
            };

            return hotelDto;
        }

        // POST: api/hotels
        [HttpPost]
        public async Task<ActionResult<HotelDto>> CreateHotel(CreateHotelDto createHotelDto)
        {
            var hotel = new Hotel
            {
                Name = createHotelDto.Name,
                Description = createHotelDto.Description,
                Address = createHotelDto.Address,
                City = createHotelDto.City,
                Latitude = createHotelDto.Latitude,
                Longitude = createHotelDto.Longitude
            };

            await _unitOfWork.Repository<Hotel>().AddAsync(hotel);
            await _unitOfWork.CompleteAsync();

            var hotelDto = new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                Address = hotel.Address,
                City = hotel.City,
                Latitude = hotel.Latitude,
                Longitude = hotel.Longitude
            };

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotelDto);
        }

        // PUT: api/hotels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, UpdateHotelDto updateHotelDto)
        {
            var hotel = await _unitOfWork.Repository<Hotel>().GetByIdAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            hotel.Name = updateHotelDto.Name;
            hotel.Description = updateHotelDto.Description;
            hotel.Address = updateHotelDto.Address;
            hotel.City = updateHotelDto.City;
            hotel.Latitude = updateHotelDto.Latitude;
            hotel.Longitude = updateHotelDto.Longitude;

            _unitOfWork.Repository<Hotel>().Update(hotel);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _unitOfWork.Repository<Hotel>().GetByIdAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Hotel>().Delete(hotel);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}