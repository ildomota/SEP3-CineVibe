using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Data;
using CineVibeAPI.Models;

namespace CineVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnacksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SnacksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/snacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Snacks>>> GetSnacks()
        {
            return await _context.Snacks.ToListAsync();
        }

        // GET: api/snacks/{id}
 [HttpGet("{id}")]
 public async Task<ActionResult<Snacks>> GetSnack(int id)
 {
     var snack = await _context.Snacks.FindAsync(id);
     if (snack == null)
     {
         return NotFound();
     }
 
     // Assicurati che ImagePath sia un percorso valido
     snack.ImagePath = $"/{snack.ImagePath}";
     return snack;
 }


        // PUT e DELETE possono essere rimossi se non devono essere usati
    }
}