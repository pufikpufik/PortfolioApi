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
    public class RecallItemsController : ControllerBase
    {
        private readonly RecallContext _context;

        public RecallItemsController(RecallContext context)
        {
            _context = context;
        }

        // GET: api/RecallItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecallItem>>> GetRecallItems()
        {
          if (_context.RecallItems == null)
          {
              return NotFound();
          }
            return await _context.RecallItems.ToListAsync();
        }

        // GET: api/RecallItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecallItem>> GetRecallItem(int id)
        {
          if (_context.RecallItems == null)
          {
              return NotFound();
          }
            var recallItem = await _context.RecallItems.FindAsync(id);

            if (recallItem == null)
            {
                return NotFound();
            }

            return recallItem;
        }

        // PUT: api/RecallItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecallItem(int id, RecallItem recallItem)
        {
            if (id != recallItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(recallItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecallItemExists(id))
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

        // POST: api/RecallItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecallItem>> PostRecallItem(RecallItem recallItem)
        {
          if (_context.RecallItems == null)
          {
              return Problem("Entity set 'RecallContext.RecallItems'  is null.");
          }
            _context.RecallItems.Add(recallItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecallItem", new { id = recallItem.Id }, recallItem);
        }

        // DELETE: api/RecallItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecallItem(int id)
        {
            if (_context.RecallItems == null)
            {
                return NotFound();
            }
            var recallItem = await _context.RecallItems.FindAsync(id);
            if (recallItem == null)
            {
                return NotFound();
            }

            _context.RecallItems.Remove(recallItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecallItemExists(int id)
        {
            return (_context.RecallItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
