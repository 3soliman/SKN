using Microsoft.AspNetCore.Mvc;
using SKN.Core.DTOs;
using SKN.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SKN.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IHotelSearchService _hotelSearchService;

        public SearchController(IHotelSearchService hotelSearchService)
        {
            _hotelSearchService = hotelSearchService;
        }

        // GET: api/search/hotels
        [HttpGet("hotels")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> SearchHotels(
            [FromQuery] string searchTerm = "",
            [FromQuery] string city = "",
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null)
        {
            var hotels = await _hotelSearchService.SearchHotelsAsync(searchTerm, city, minPrice, maxPrice);
            return Ok(hotels);
        }

        // GET: api/search/availability
        [HttpGet("availability")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> CheckAvailability(
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut,
            [FromQuery] int guests = 1)
        {
            if (checkIn >= checkOut)
            {
                return BadRequest("Check-in date must be before check-out date");
            }

            if (checkIn < DateTime.Today)
            {
                return BadRequest("Check-in date cannot be in the past");
            }

            var availableHotels = await _hotelSearchService.GetAvailableHotelsAsync(checkIn, checkOut, guests);
            return Ok(availableHotels);
        }
    }
}