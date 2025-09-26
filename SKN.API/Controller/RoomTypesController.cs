using Microsoft.AspNetCore.Mvc;
using SKN.Core.DTOs;
using SKN.Core.Interfaces;
using SKN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SKN.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomTypeRepository _RoomTypeRepository;

        public RoomTypesController(IRoomTypeRepository RoomTypeRepository)
        {
            _RoomTypeRepository = RoomTypeRepository;
        }
       

        // GET: api/roomtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeDto>>> GetRoomTypes()
        {
            var roomTypes = await _RoomTypeRepository.GetRoomTypesWithHotelAsync();
            
            var roomTypeDtos = roomTypes.Select(rt => new RoomTypeDto
            {
                Id = rt.Id,
                Name = rt.Name,
                Description = rt.Description,
                PricePerNight = rt.PricePerNight,
                Capacity = rt.Capacity,
                AvailableRooms = rt.AvailableRooms,
                HotelName = rt.Hotel.Name,
                HotelId = rt.HotelId
            });

            return Ok(roomTypeDtos);
        }

        // GET: api/roomtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeDto>> GetRoomType(int id)
        {
            var roomType = await _unitOfWork.Repository<RoomType>().GetByIdAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }

            var roomTypeDto = new RoomTypeDto
            {
                Id = roomType.Id,
                Name = roomType.Name,
                Description = roomType.Description,
                PricePerNight = roomType.PricePerNight,
                Capacity = roomType.Capacity,
                AvailableRooms = roomType.AvailableRooms,
                HotelName = roomType.Hotel.Name,
                HotelId = roomType.HotelId
            };

            return roomTypeDto;
        }

        // GET: api/roomtypes/hotel/5
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<RoomTypeDto>>> GetRoomTypesByHotel(int hotelId)
        {
            var roomTypes = await _unitOfWork.Repository<RoomType>().FindAsync(rt => rt.HotelId == hotelId);
            
            var roomTypeDtos = roomTypes.Select(rt => new RoomTypeDto
            {
                Id = rt.Id,
                Name = rt.Name,
                Description = rt.Description,
                PricePerNight = rt.PricePerNight,
                Capacity = rt.Capacity,
                AvailableRooms = rt.AvailableRooms,
                HotelName = rt.Hotel.Name,
                HotelId = rt.HotelId
            });

            return Ok(roomTypeDtos);
        }

        // POST: api/roomtypes
        [HttpPost]
        public async Task<ActionResult<RoomTypeDto>> CreateRoomType(CreateRoomTypeDto createRoomTypeDto)
        {
            var roomType = new RoomType
            {
                Name = createRoomTypeDto.Name,
                Description = createRoomTypeDto.Description,
                PricePerNight = createRoomTypeDto.PricePerNight,
                Capacity = createRoomTypeDto.Capacity,
                AvailableRooms = createRoomTypeDto.AvailableRooms,
                HotelId = createRoomTypeDto.HotelId
            };

            await _unitOfWork.Repository<RoomType>().AddAsync(roomType);
            await _unitOfWork.CompleteAsync();

            var roomTypeDto = new RoomTypeDto
            {
                Id = roomType.Id,
                Name = roomType.Name,
                Description = roomType.Description,
                PricePerNight = roomType.PricePerNight,
                Capacity = roomType.Capacity,
                AvailableRooms = roomType.AvailableRooms,
                HotelId = roomType.HotelId
            };

            return CreatedAtAction(nameof(GetRoomType), new { id = roomType.Id }, roomTypeDto);
        }

        // PUT: api/roomtypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomType(int id, UpdateRoomTypeDto updateRoomTypeDto)
        {
            var roomType = await _unitOfWork.Repository<RoomType>().GetByIdAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }

            roomType.Name = updateRoomTypeDto.Name;
            roomType.Description = updateRoomTypeDto.Description;
            roomType.PricePerNight = updateRoomTypeDto.PricePerNight;
            roomType.Capacity = updateRoomTypeDto.Capacity;
            roomType.AvailableRooms = updateRoomTypeDto.AvailableRooms;

            _unitOfWork.Repository<RoomType>().Update(roomType);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/roomtypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var roomType = await _unitOfWork.Repository<RoomType>().GetByIdAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<RoomType>().Delete(roomType);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}