using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Data;
using CineVibeAPI.Models;

namespace CineVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyPointsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoyaltyPointsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyPoints>>> GetLoyaltyPoints()
        {
            return await _context.LoyaltyPoints.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyPoints>> GetLoyaltyPoint(int id)
        {
            var loyaltyPoint = await _context.LoyaltyPoints.FindAsync(id);
            if (loyaltyPoint == null)
            {
                return NotFound();
            }
            return loyaltyPoint;
        }

        [HttpPost("update/{userId}")]
        public async Task<IActionResult> AddOrUpdateLoyaltyPoints(int userId, int pointsToAdd)
        {
            try
            {
                var loyaltyEntry = await _context.LoyaltyPoints.FirstOrDefaultAsync(lp => lp.UserId == userId);

                if (loyaltyEntry != null)
                {
                    loyaltyEntry.Points += pointsToAdd;
                    loyaltyEntry.UpdatedAt = DateTime.UtcNow;

                    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == userId);
                    if (customer != null)
                    {
                        customer.LoyaltyPoints += pointsToAdd;
                    }
                }
                else
                {
                    loyaltyEntry = new LoyaltyPoints
                    {
                        UserId = userId,
                        Points = pointsToAdd,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.LoyaltyPoints.Add(loyaltyEntry);

                    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == userId);
                    if (customer != null)
                    {
                        customer.LoyaltyPoints += pointsToAdd;
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new { Message = $"Loyalty points updated successfully for user {userId}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoyaltyPoint(int id)
        {
            var loyaltyPoint = await _context.LoyaltyPoints.FindAsync(id);
            if (loyaltyPoint == null)
            {
                return NotFound();
            }

            _context.LoyaltyPoints.Remove(loyaltyPoint);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
