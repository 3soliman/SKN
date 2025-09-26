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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = await _reviewRepository.GetRecentReviewsAsync(50); // الحصول على آخر 50 تقييم
            
            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserName = $"{r.User?.FirstName} {r.User?.LastName}", // استخدام null conditional
                HotelName = r.Hotel?.Name, // استخدام null conditional
                UserId = r.UserId,
                HotelId = r.HotelId,
                IsVerified = r.IsVerified
            });

            return Ok(reviewDtos);
        }

        // GET: api/reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UserName = $"{review.User?.FirstName} {review.User?.LastName}",
                HotelName = review.Hotel?.Name,
                UserId = review.UserId,
                HotelId = review.HotelId,
                IsVerified = review.IsVerified
            };

            return reviewDto;
        }

        // GET: api/reviews/hotel/5
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByHotel(int hotelId)
        {
            var reviews = await _reviewRepository.GetReviewsWithUserAsync(hotelId);
            
            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserName = $"{r.User?.FirstName} {r.User?.LastName}",
                HotelName = r.Hotel?.Name,
                IsVerified = r.IsVerified
            });

            return Ok(reviewDtos);
        }

        // GET: api/reviews/hotel/5/average
        [HttpGet("hotel/{hotelId}/average")]
        public async Task<ActionResult<object>> GetHotelAverageRating(int hotelId)
        {
            var averageRating = await _reviewRepository.GetAverageRatingAsync(hotelId);
            var totalReviews = await _reviewRepository.GetReviewCountAsync(hotelId);

            return Ok(new { 
                AverageRating = averageRating, 
                TotalReviews = totalReviews 
            });
        }

        // GET: api/reviews/hotel/5/verified
        [HttpGet("hotel/{hotelId}/verified")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetVerifiedReviews(int hotelId)
        {
            var reviews = await _reviewRepository.GetVerifiedReviewsAsync(hotelId);
            
            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserName = $"{r.User?.FirstName} {r.User?.LastName}",
                HotelName = r.Hotel?.Name,
                IsVerified = r.IsVerified
            });

            return Ok(reviewDtos);
        }

        // GET: api/reviews/recent/10
        [HttpGet("recent/{count}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetRecentReviews(int count = 10)
        {
            var reviews = await _reviewRepository.GetRecentReviewsAsync(count);
            
            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserName = $"{r.User?.FirstName} {r.User?.LastName}",
                HotelName = r.Hotel?.Name,
                IsVerified = r.IsVerified
            });

            return Ok(reviewDtos);
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview(CreateReviewDto createReviewDto)
        {
            var review = new Review
            {
                Rating = createReviewDto.Rating,
                Comment = createReviewDto.Comment ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                UserId = createReviewDto.UserId,
                HotelId = createReviewDto.HotelId,
                IsVerified = false // افتراضي غير موثق
            };

            // التأكد من أن التقييم بين 1 و 5
            if (review.Rating < 1 || review.Rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5");
            }

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UserId = review.UserId,
                HotelId = review.HotelId,
                IsVerified = review.IsVerified
            };

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, reviewDto);
        }

        // PUT: api/reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewDto updateReviewDto)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            review.Rating = updateReviewDto.Rating;
            review.Comment = updateReviewDto.Comment ?? review.Comment;

            _reviewRepository.Update(review);
            await _reviewRepository.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/reviews/5/verify
        [HttpPut("{id}/verify")]
        public async Task<IActionResult> VerifyReview(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            // review.IsVerified = true;
            _reviewRepository.Update(review);
            await _reviewRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            _reviewRepository.Delete(review);
            await _reviewRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}