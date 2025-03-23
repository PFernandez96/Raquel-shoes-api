using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaquelShoesAPI.Domain;
using RaquelShoesAPI.Infrastructure.Data;

namespace RaquelShoesAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TiendasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/tiendas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tiendas = await _context.Tiendas.Include(t => t.Zapatos).ToListAsync();
            return Ok(tiendas);
        }

        // GET: api/tiendas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tienda = await _context.Tiendas.Include(t => t.Zapatos)
                                               .FirstOrDefaultAsync(t => t.Id == id);
            if (tienda == null)
                return NotFound();
            return Ok(tienda);
        }

        // POST: api/tiendas
        [HttpPost]
        public async Task<IActionResult> Create(Tienda tienda)
        {
            _context.Tiendas.Add(tienda);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = tienda.Id }, tienda);
        }

        // PUT: api/tiendas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tienda tienda)
        {
            if (id != tienda.Id)
                return BadRequest();

            _context.Entry(tienda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/tiendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda == null)
                return NotFound();

            _context.Tiendas.Remove(tienda);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
