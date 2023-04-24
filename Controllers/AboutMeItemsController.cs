using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutMeItemsController : ControllerBase
    {
        private readonly AboutMeContext _context;

        public AboutMeItemsController(AboutMeContext context)
        {
            _context = context;
        }

        // GET: api/AboutMeItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AboutMeItem>>> GetAboutMeItems()
        {
          if (_context.AboutMeItems == null)
          {
              return NotFound();
          }
            return await _context.AboutMeItems.ToListAsync();
        }

        // GET: api/AboutMeItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AboutMeItem>> GetAboutMeItem(int id)
        {
          if (_context.AboutMeItems == null)
          {
              return NotFound();
          }
            var aboutMeItem = await _context.AboutMeItems.FindAsync(id);

            if (aboutMeItem == null)
            {
                return NotFound();
            }

            return aboutMeItem;
        }

        // PUT: api/AboutMeItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAboutMeItem(int id, AboutMeItem aboutMeItem)
        {
            if (id != aboutMeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(aboutMeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutMeItemExists(id))
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

        // POST: api/AboutMeItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AboutMeItem>> PostAboutMeItem(AboutMeItem aboutMeItem)
        {
          if (_context.AboutMeItems == null)
          {
              return Problem("Entity set 'AboutMeContext.AboutMeItems'  is null.");
          }
            _context.AboutMeItems.Add(aboutMeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAboutMeItem", new { id = aboutMeItem.Id }, aboutMeItem);
        }

        // DELETE: api/AboutMeItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutMeItem(int id)
        {
            if (_context.AboutMeItems == null)
            {
                return NotFound();
            }
            var aboutMeItem = await _context.AboutMeItems.FindAsync(id);
            if (aboutMeItem == null)
            {
                return NotFound();
            }

            _context.AboutMeItems.Remove(aboutMeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AboutMeItemExists(int id)
        {
            return (_context.AboutMeItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
