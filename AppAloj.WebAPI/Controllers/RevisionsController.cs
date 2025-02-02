﻿using System;
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
    public class RevisionsController : ControllerBase
    {
        private readonly AppAlojDbContext _context;

        public RevisionsController(AppAlojDbContext context)
        {
            _context = context;
        }

        // GET: api/Revisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Revision>>> GetRevisions()
        {
            return await _context.Revisions.ToListAsync();
        }

        // GET: api/Revisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Revision>> GetRevision(int id)
        {
            var revision = await _context.Revisions.FindAsync(id);

            if (revision == null)
            {
                return NotFound();
            }

            return revision;
        }

        // PUT: api/Revisions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRevision(int id, Revision revision)
        {
            if (id != revision.IdRevision)
            {
                return BadRequest();
            }

            _context.Entry(revision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevisionExists(id))
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

        // POST: api/Revisions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Revision>> PostRevision(Revision revision)
        {
            _context.Revisions.Add(revision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRevision", new { id = revision.IdRevision }, revision);
        }

        // DELETE: api/Revisions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Revision>> DeleteRevision(int id)
        {
            var revision = await _context.Revisions.FindAsync(id);
            if (revision == null)
            {
                return NotFound();
            }

            _context.Revisions.Remove(revision);
            await _context.SaveChangesAsync();

            return revision;
        }

        private bool RevisionExists(int id)
        {
            return _context.Revisions.Any(e => e.IdRevision == id);
        }
    }
}
