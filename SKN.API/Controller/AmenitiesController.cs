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
    public class AmenitiesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDto>>> GetAmenities()
        {
            var amenities = await _unitOfWork.Repository<Amenity>().GetAllAsync();
            
            var amenityDtos = amenities.Select(a => new AmenityDto
            {
                Id = a.Id,
                Name = a.Name,
                Icon = a.Icon,
                UsageCount = a.HotelAmenities?.Count ?? 0
            });

            return Ok(amenityDtos);
        }

        // GET: api/amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDto>> GetAmenity(int id)
        {
            var amenity = await _unitOfWork.Repository<Amenity>().GetByIdAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            var amenityDto = new AmenityDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Icon = amenity.Icon,
                UsageCount = amenity.HotelAmenities?.Count ?? 0
            };

            return amenityDto;
        }

        // POST: api/amenities
        [HttpPost]
        public async Task<ActionResult<AmenityDto>> CreateAmenity(CreateAmenityDto createAmenityDto)
        {
            var amenity = new Amenity
            {
                Name = createAmenityDto.Name,
                Icon = createAmenityDto.Icon
            };

            await _unitOfWork.Repository<Amenity>().AddAsync(amenity);
            await _unitOfWork.CompleteAsync();

            var amenityDto = new AmenityDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Icon = amenity.Icon,
                UsageCount = 0
            };

            return CreatedAtAction(nameof(GetAmenity), new { id = amenity.Id }, amenityDto);
        }

        // PUT: api/amenities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAmenity(int id, UpdateAmenityDto updateAmenityDto)
        {
            var amenity = await _unitOfWork.Repository<Amenity>().GetByIdAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            amenity.Name = updateAmenityDto.Name;
            amenity.Icon = updateAmenityDto.Icon;

            _unitOfWork.Repository<Amenity>().Update(amenity);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            var amenity = await _unitOfWork.Repository<Amenity>().GetByIdAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Amenity>().Delete(amenity);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}