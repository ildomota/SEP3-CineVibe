using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineVibeAPI.Data;
using CineVibeAPI.Models;

namespace CineVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        private const string AdminSecretCode = "010108";

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Ottieni tutti gli amministratori
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // Ottieni un amministratore specifico per ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return admin;
        }

        // Registra un nuovo amministratore
        [HttpPost("register")]
        public async Task<ActionResult<Admin>> RegisterAdmin([FromBody] Admin admin)
        {
            if (admin.SecretCode != AdminSecretCode)
            {
                return Unauthorized("Invalid admin secret code.");
            }

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdmin), new { id = admin.AdminId }, admin);
        }

        // Login per gli amministratori
        [HttpPost("login")]
        public async Task<ActionResult<Admin>> Login([FromBody] AdminLoginRequest loginRequest)
        {
            if (loginRequest.SecretCode != AdminSecretCode)
            {
                return Unauthorized("Invalid admin secret code.");
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == loginRequest.Username && a.PasswordHash == loginRequest.PasswordHash);

            if (admin == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(admin);
        }
        

        // Aggiorna i dettagli di un amministratore
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] Admin updatedAdmin)
        {
            if (id != updatedAdmin.AdminId)
            {
                return BadRequest("Admin ID mismatch.");
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            admin.Username = updatedAdmin.Username;
            admin.Email = updatedAdmin.Email;
            admin.Status = updatedAdmin.Status;

            // La password viene aggiornata solo se viene inviata
            if (!string.IsNullOrEmpty(updatedAdmin.PasswordHash))
            {
                admin.PasswordHash = updatedAdmin.PasswordHash;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Elimina un amministratore
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    

    // Modello per il login degli amministratori
    public class AdminLoginRequest
    {
        public string SecretCode { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
    
    
}
