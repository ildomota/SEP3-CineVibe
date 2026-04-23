using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Data;
using CineVibeAPI.Models;

namespace CineVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Booking/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return booking;
        }

       // POST: api/Booking - Create multiple bookings
       [HttpPost]
       public async Task<ActionResult> PostBookings([FromBody] IEnumerable<Booking> bookings)
       {
           if (bookings == null || !bookings.Any())
           {
               return BadRequest("No booking data provided.");
           }
       
           var conflictingBookings = new List<string>();
       
           foreach (var booking in bookings)
           {
               booking.BookingDate = DateTime.SpecifyKind(booking.BookingDate, DateTimeKind.Utc);
       
               var conflict = await _context.Bookings.AnyAsync(b =>
                   b.MovieId == booking.MovieId &&
                   b.BookingDate.Date == booking.BookingDate.Date &&
                   b.BookingTime == booking.BookingTime &&
                   b.SeatNumber == booking.SeatNumber);
       
               if (conflict && !string.IsNullOrEmpty(booking.SeatNumber))
               {
                   conflictingBookings.Add(booking.SeatNumber);
               }
           }
       
           if (conflictingBookings.Any())
           {
               return Conflict(new
               {
                   Message = $"The following seats are already booked: {string.Join(", ", conflictingBookings)}"
               });
           }
       
           // Add bookings to the database
               foreach (var booking in bookings)
               {
                   booking.Status = "Confirmed";
                   _context.Bookings.Add(booking);
           
                   // Aggiornamento o inserimento dei punti nella tabella LoyaltyPoints
                   var customerLoyalty = await _context.LoyaltyPoints
                       .FirstOrDefaultAsync(lp => lp.UserId == booking.CustomerId);
           
                   if (customerLoyalty != null)
                   {
                       customerLoyalty.Points += 5; // Aggiunge 5 punti
                       customerLoyalty.UpdatedAt = DateTime.UtcNow;
                   }
                   else
                   {
                       _context.LoyaltyPoints.Add(new LoyaltyPoints
                       {
                           UserId = booking.CustomerId,
                           Points = 5, // Aggiunge i primi 5 punti
                           UpdatedAt = DateTime.UtcNow
                       });
                   }
           
                   // Aggiornamento dei punti nella tabella Customer
                   var customer = await _context.Customers
                       .FirstOrDefaultAsync(c => c.CustomerId == booking.CustomerId);
           
                   if (customer != null)
                   {
                     customer.LoyaltyPoints += 5; // Incrementa di 5 punti
                       _context.Entry(customer).State = EntityState.Modified; // Segnala che il customer è stato aggiornato
                   }
               }
           
               // Salva tutte le modifiche una sola volta
               await _context.SaveChangesAsync();
           
               return Ok(new
               {
                   Message = "Bookings confirmed successfully!",
                   Bookings = bookings
               });
       }


        // GET: api/Booking/seats/{movieId}/{date}/{time} - Retrieve unavailable seats
        [HttpGet("seats/{movieId}/{date}/{time}")]
        public async Task<ActionResult<IEnumerable<string>>> GetUnavailableSeats(int movieId, string date, string time)
        {
            try
            {
                var parsedDate = DateTime.SpecifyKind(DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTimeKind.Utc);

                // DEBUG: Log della query per capire cosa sta succedendo
                Console.WriteLine($"Parameters - MovieId: {movieId}, Date: {parsedDate.Date}, Time: {time}");

                var unavailableSeatsQuery = _context.Bookings
                    .Where(b => b.MovieId == movieId
                                && b.BookingDate.Date == parsedDate.Date
                                && b.BookingTime == time);

                var unavailableSeats = await unavailableSeatsQuery.Select(b => b.SeatNumber).ToListAsync();

                // Log per vedere i risultati
                Console.WriteLine($"Seats Found: {string.Join(", ", unavailableSeats)}");

                return Ok(unavailableSeats);
            }
            catch (FormatException ex)
            {
                return BadRequest($"Invalid date format: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/Booking/{id} - Update an existing booking
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest("Booking ID mismatch.");
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bookings.Any(e => e.BookingId == id))
                {
                    return NotFound("Booking not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
            [HttpPost("updateExpiredBookings")]
            public async Task<IActionResult> UpdateExpiredBookings()
            {
                try
                {
                    // Ottieni la data corrente senza orario
                    var today = DateTime.UtcNow.Date;
            
                    // Trova le prenotazioni "Confirmed" con una data passata
                    var expiredBookings = await _context.Bookings
                        .Where(b => b.Status == "Confirmed" && b.BookingDate.Date < today)
                        .ToListAsync();
            
                    // Aggiorna lo status a "Expired"
                    foreach (var booking in expiredBookings)
                    {
                        booking.Status = "Expired";
                    }
            
                    // Salva le modifiche nel database
                    await _context.SaveChangesAsync();
            
                    return Ok(new { Message = $"{expiredBookings.Count} bookings marked as expired." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

// DELETE: api/Booking/{id} - Delete a booking
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteBooking(int id)
{
    var booking = await _context.Bookings.FindAsync(id);
    if (booking == null)
    {
        return NotFound("Booking not found.");
    }

    // Rimuovere punti solo se la prenotazione è "Confirmed"
    if (booking.Status == "Confirmed")
    {
        // Aggiorna i punti nella tabella LoyaltyPoints
        var loyalty = await _context.LoyaltyPoints
            .FirstOrDefaultAsync(lp => lp.UserId == booking.CustomerId);
    
        if (loyalty != null && loyalty.Points >= 5)
        {
            loyalty.Points -= 5; // Rimuovi 5 punti
        }
    
        // Aggiorna i punti nella tabella Customer
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == booking.CustomerId);
    
        if (customer != null && customer.LoyaltyPoints >= 5)
        {
            customer.LoyaltyPoints -= 5; // Rimuovi 5 punti
            _context.Customers.Update(customer);
        }
    }


    // Rimuovi la prenotazione
    _context.Bookings.Remove(booking);
    await _context.SaveChangesAsync();

    return Ok("Booking deleted successfully and loyalty points updated.");
}


        // GET: api/Booking/customer/{customerId} - Retrieve bookings for a specific customer
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetCustomerBookings(int customerId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.BookingDate)
                .ThenBy(b => b.BookingTime)
                .ToListAsync();

            if (!bookings.Any())
            {
                return NotFound(new { Message = "No bookings found for this customer." });
            }

            return Ok(bookings);
        }
        
        // GET: api/Booking/snacks/{customerId}
        [HttpGet("snacks/{customerId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsForSnacks(int customerId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.CustomerId == customerId && b.Status == "Confirmed")
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        
            if (!bookings.Any())
            {
                return NotFound("No active bookings available for snacks.");
            }
        
            return Ok(bookings);
        }

    }
    


}