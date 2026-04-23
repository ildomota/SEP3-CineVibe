using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Data;
using CineVibeAPI.Models;

namespace CineVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnackOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SnackOrderController(AppDbContext context)
        {
            _context = context;
        }

[HttpGet("customer/{id}")]
public async Task<ActionResult<IEnumerable<SnackOrder>>> GetSnackOrdersByCustomer(int id)
{
    try
    {
        var orders = await _context.SnackOrders
            .Where(o => o.CustomerId == id)
            .ToListAsync();

        if (orders == null || !orders.Any())
        {
            return NotFound("No orders found for this customer.");
        }

        return Ok(orders);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}



[HttpPost]
public async Task<IActionResult> PostSnackOrder([FromBody] List<SnackOrder> snackOrders, [FromQuery] int bookingId)
{
    if (snackOrders == null || !snackOrders.Any())
    {
        return BadRequest("Invalid order data.");
    }

    foreach (var order in snackOrders)
    {
          // Verifica che il BookingId non sia nullo
                    if (order.BookingId == 0)
                    {
                        return BadRequest("Booking ID is required for the snack order.");
                    }
        var newOrder = new SnackOrder
        {
            CustomerId = order.CustomerId,
            SnackId = order.SnackId,
            BookingId = order.BookingId, // Nuova colonna aggiunta
            Quantity = order.Quantity,
            TotalPrice = order.Quantity * order.TotalPrice,
            OrderDate = DateTime.UtcNow
        };

        _context.SnackOrders.Add(newOrder);
    }

    await _context.SaveChangesAsync();
    return Ok("Order placed successfully!");
}

    
    // Classe per ricevere i dati dell'ordine
    public class SnackOrderRequest
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<SnackOrderDetail> Snacks { get; set; } = new List<SnackOrderDetail>();
    }
    
    public class SnackOrderDetail
    {
        public int SnackId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutSnackOrder(int id, SnackOrder snackOrder)
        {
            if (id != snackOrder.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(snackOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.SnackOrders.Any(e => e.OrderId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSnackOrder(int id)
        {
            var snackOrder = await _context.SnackOrders.FindAsync(id);
            if (snackOrder == null)
            {
                return NotFound();
            }

            _context.SnackOrders.Remove(snackOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
