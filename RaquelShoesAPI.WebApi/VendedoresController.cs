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
    public class VendedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/vendedores
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendedores = await _context.Vendedores.Include(v => v.Zapatos).ToListAsync();
            return Ok(vendedores);
        }

        // GET: api/vendedores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vendedor = await _context.Vendedores.Include(v => v.Zapatos)
                                                    .FirstOrDefaultAsync(v => v.Id == id);
            if (vendedor == null)
                return NotFound();
            return Ok(vendedor);
        }

        // POST: api/vendedores
        [HttpPost]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = vendedor.Id }, vendedor);
        }

        // PUT: api/vendedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
                return BadRequest();

            _context.Entry(vendedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/vendedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null)
                return NotFound();

            _context.Vendedores.Remove(vendedor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
