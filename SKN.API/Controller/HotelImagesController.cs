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
    public class HotelImagesController : ControllerBase
    {
        private readonly IHotelImageRepository _hotelImageRepository;

        public HotelImagesController(IHotelImageRepository hotelImageRepository)
        {
            _hotelImageRepository = hotelImageRepository;
        }

        // GET: api/hotelimages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelImageDto>>> GetHotelImages()
        {
            var hotelImages = await _hotelImageRepository.GetImagesWithHotelAsync();
            
            var hotelImageDtos = hotelImages.Select(hi => new HotelImageDto
            {
                Id = hi.Id,
                ImageUrl = hi.ImageUrl,
                AltText = hi.AltText,
                IsPrimary = hi.IsPrimary,
                DisplayOrder = hi.DisplayOrder,
                HotelId = hi.HotelId,
                HotelName = hi.Hotel?.Name // استخدام null conditional
            });

            return Ok(hotelImageDtos);
        }

        // GET: api/hotelimages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelImageDto>> GetHotelImage(int id)
        {
            var hotelImage = await _hotelImageRepository.GetByIdAsync(id);

            if (hotelImage == null)
            {
                return NotFound();
            }

            var hotelImageDto = new HotelImageDto
            {
                Id = hotelImage.Id,
                ImageUrl = hotelImage.ImageUrl,
                AltText = hotelImage.AltText,
                IsPrimary = hotelImage.IsPrimary,
                DisplayOrder = hotelImage.DisplayOrder,
                HotelId = hotelImage.HotelId,
                HotelName = hotelImage.Hotel?.Name // استخدام null conditional
            };

            return hotelImageDto;
        }

        // GET: api/hotelimages/hotel/5
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<HotelImageDto>>> GetImagesByHotel(int hotelId)
        {
            var hotelImages = await _hotelImageRepository.GetImagesByHotelAsync(hotelId);
            
            var hotelImageDtos = hotelImages.Select(hi => new HotelImageDto
            {
                Id = hi.Id,
                ImageUrl = hi.ImageUrl,
                AltText = hi.AltText,
                IsPrimary = hi.IsPrimary,
                DisplayOrder = hi.DisplayOrder,
                HotelId = hi.HotelId
            }).OrderBy(hi => hi.DisplayOrder);

            return Ok(hotelImageDtos);
        }

        // GET: api/hotelimages/hotel/5/primary
        [HttpGet("hotel/{hotelId}/primary")]
        public async Task<ActionResult<HotelImageDto>> GetPrimaryImage(int hotelId)
        {
            var primaryImage = await _hotelImageRepository.GetPrimaryImageAsync(hotelId);

            if (primaryImage == null)
            {
                return NotFound();
            }

            var hotelImageDto = new HotelImageDto
            {
                Id = primaryImage.Id,
                ImageUrl = primaryImage.ImageUrl,
                AltText = primaryImage.AltText,
                IsPrimary = primaryImage.IsPrimary,
                DisplayOrder = primaryImage.DisplayOrder,
                HotelId = primaryImage.HotelId
            };

            return hotelImageDto;
        }

        // POST: api/hotelimages
        [HttpPost]
        public async Task<ActionResult<HotelImageDto>> CreateHotelImage(CreateHotelImageDto createHotelImageDto)
        {
            var hotelImage = new HotelImage
            {
                ImageUrl = createHotelImageDto.ImageUrl ?? throw new ArgumentNullException(nameof(createHotelImageDto.ImageUrl)),
                AltText = createHotelImageDto.AltText ?? string.Empty,
                IsPrimary = createHotelImageDto.IsPrimary,
                DisplayOrder = createHotelImageDto.DisplayOrder,
                HotelId = createHotelImageDto.HotelId
            };

            // إذا كانت هذه الصورة أساسية، استخدم الدالة المخصصة
            if (hotelImage.IsPrimary)
            {
                // سنقوم بتحديثها بعد الإضافة
            }

            await _hotelImageRepository.AddAsync(hotelImage);
            await _hotelImageRepository.SaveChangesAsync();

            // إذا كانت أساسية، حددها كأساسية بعد الإضافة
            if (hotelImage.IsPrimary)
            {
                await _hotelImageRepository.SetPrimaryImageAsync(hotelImage.Id);
            }

            var hotelImageDto = new HotelImageDto
            {
                Id = hotelImage.Id,
                ImageUrl = hotelImage.ImageUrl,
                AltText = hotelImage.AltText,
                IsPrimary = hotelImage.IsPrimary,
                DisplayOrder = hotelImage.DisplayOrder,
                HotelId = hotelImage.HotelId
            };

            return CreatedAtAction(nameof(GetHotelImage), new { id = hotelImage.Id }, hotelImageDto);
        }

        // PUT: api/hotelimages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotelImage(int id, UpdateHotelImageDto updateHotelImageDto)
        {
            var hotelImage = await _hotelImageRepository.GetByIdAsync(id);

            if (hotelImage == null)
            {
                return NotFound();
            }

            hotelImage.AltText = updateHotelImageDto.AltText ?? hotelImage.AltText;
            hotelImage.DisplayOrder = updateHotelImageDto.DisplayOrder;

            // معالجة الصورة الأساسية باستخدام الدالة المخصصة
            if (updateHotelImageDto.IsPrimary && !hotelImage.IsPrimary)
            {
                await _hotelImageRepository.SetPrimaryImageAsync(id);
            }
            else
            {
                hotelImage.IsPrimary = updateHotelImageDto.IsPrimary;
                _hotelImageRepository.Update(hotelImage);
                await _hotelImageRepository.SaveChangesAsync();
            }

            return NoContent();
        }

        // PUT: api/hotelimages/5/set-primary
        // [HttpPut("{id}/set-primary")]
        // public async Task<IActionResult> SetAsPrimaryImage(int id)
        // {
        //     var result = await _hotelImageRepository.SetPrimaryImageAsync(id);
            
        //     if (result)
        //     {
        //         return NoContent();
        //     }
            
        //     return NotFound();
        // }

        // DELETE: api/hotelimages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelImage(int id)
        {
            var hotelImage = await _hotelImageRepository.GetByIdAsync(id);

            if (hotelImage == null)
            {
                return NotFound();
            }

            _hotelImageRepository.Delete(hotelImage);
            await _hotelImageRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}