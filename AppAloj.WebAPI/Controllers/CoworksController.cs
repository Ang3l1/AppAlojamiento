using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAloj.Datos;
using AppAloj.Entidades;

namespace AppAloj.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoworksController : ControllerBase
    {
        private readonly AppAlojDbContext _context;

        public CoworksController(AppAlojDbContext context)
        {
            _context = context;
        }

        // GET: api/Coworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cowork>>> GetCoworks()
        {
            return await _context.Coworks.ToListAsync();
        }

        // GET: api/Coworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cowork>> GetCowork(int id)
        {
            var cowork = await _context.Coworks.FindAsync(id);

            if (cowork == null)
            {
                return NotFound();
            }

            return cowork;
        }

        // PUT: api/Coworks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCowork(int id, Cowork cowork)
        {
            if (id != cowork.IdCowork)
            {
                return BadRequest();
            }

            _context.Entry(cowork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoworkExists(id))
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

        // POST: api/Coworks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cowork>> PostCowork(Cowork cowork)
        {
            _context.Coworks.Add(cowork);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCowork", new { id = cowork.IdCowork }, cowork);
        }

        // DELETE: api/Coworks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cowork>> DeleteCowork(int id)
        {
            var cowork = await _context.Coworks.FindAsync(id);
            if (cowork == null)
            {
                return NotFound();
            }

            _context.Coworks.Remove(cowork);
            await _context.SaveChangesAsync();

            return cowork;
        }

        private bool CoworkExists(int id)
        {
            return _context.Coworks.Any(e => e.IdCowork == id);
        }
    }
}
