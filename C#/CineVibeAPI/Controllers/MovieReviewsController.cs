using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Data;
using CineVibeAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class MovieReviewsController : ControllerBase
{
    private readonly AppDbContext _context;

    public MovieReviewsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/moviereviews
    [HttpPost]
    public async Task<IActionResult> SubmitReview([FromBody] MovieReviews review)
    {
        if (review == null)
            return BadRequest("Review cannot be null.");

        if (review.Rating < 1 || review.Rating > 10 || string.IsNullOrWhiteSpace(review.ReviewText))
            return BadRequest("Invalid review data. Ensure Rating is between 1-10 and ReviewText is not empty.");

        try
        {
            review.CreatedAt = DateTime.UtcNow;
            review.UpdatedAt = DateTime.UtcNow;

            _context.MovieReviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Review submitted successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    // GET: api/moviereviews/{movieId}
    [HttpGet("{movieId}")]
    public async Task<IActionResult> GetReviewsByMovie(int movieId)
    {
        try
        {
            var reviews = await _context.MovieReviews
                .Where(r => r.MovieId == movieId)
                .ToListAsync();

            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching reviews: {ex.Message}");
        }
    }

// GET: api/moviereviews
    [HttpGet("all-reviews")]
    public async Task<IActionResult> GetAllReviews()
    {
        try
        {
            // Recupera tutte le recensioni
            var reviews = await _context.MovieReviews.ToListAsync();

            // Recupera tutti i clienti e i film correlati
            var customers = await _context.Customers.ToListAsync();
            var movies = await _context.Movies.ToListAsync();

            // Raggruppa le recensioni per cliente
            var groupedReviews = reviews
                .GroupBy(r => r.CustomerId)
                .Select(group => new
                {
                    CustomerId = group.Key,
                    Username = customers.FirstOrDefault(c => c.CustomerId == group.Key)?.Username ?? "Unknown",
                    Reviews = group.Select(r => new
                    {
                        ReviewId = r.ReviewId,
                        MovieTitle = movies.FirstOrDefault(m => m.movieid == r.MovieId)?.Title ?? "Unknown",
                        ReviewText = r.ReviewText,
                        Rating = r.Rating,
                        CreatedAt = r.CreatedAt
                    }).ToList()
                })
                .ToList();

            return Ok(groupedReviews);
        }
        catch (Exception ex)
        {
            // Log dell'errore
            Console.WriteLine($"Error retrieving reviews: {ex.Message}");
            return StatusCode(500, "An error occurred while fetching reviews.");
        }
    }

    
    // GET: api/moviereviews/customer/{customerId}
    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetCustomerReviewsWithMovies(int customerId)
    {
        try
        {
            var reviews = await _context.MovieReviews
                .Where(r => r.CustomerId == customerId)
                .Join(
                    _context.Movies,
                    review => review.MovieId,
                    movie => movie.movieid,
                    (review, movie) => new
                    {
                        ReviewId = review.ReviewId,
                        MovieTitle = movie.Title,
                        PosterURL = movie.PosterURL,
                        Rating = review.Rating,
                        ReviewText = review.ReviewText,
                        CreatedAt = review.CreatedAt
                    }
                )
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            if (!reviews.Any())
                return NotFound("No reviews found for this customer.");

            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // DELETE: api/moviereviews/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        try
        {
            // Find the review in the database
            var review = await _context.MovieReviews.FindAsync(id);
            if (review == null)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            // Remove the review
            _context.MovieReviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on success
        }
        catch (Exception ex)
        {
            // Log the exception (if needed)
            Console.WriteLine($"Error deleting review: {ex.Message}");
            return StatusCode(500, "An error occurred while deleting the review.");
        }
    }


}
