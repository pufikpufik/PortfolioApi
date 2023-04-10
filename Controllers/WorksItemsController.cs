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
    public class WorksItemsController : ControllerBase
    {
        private readonly WorksContext _context;

        public WorksItemsController(WorksContext context)
        {
            _context = context;
        }

        // GET: api/WorksItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorksItem>>> GetWorksItem()
        {
          if (_context.WorksItem == null)
          {
              return NotFound();
          }
            return await _context.WorksItem.ToListAsync();
        }

        // GET: api/WorksItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorksItem>> GetWorksItem(long id)
        {
          if (_context.WorksItem == null)
          {
              return NotFound();
          }
            var worksItem = await _context.WorksItem.FindAsync(id);

            if (worksItem == null)
            {
                return NotFound();
            }

            return worksItem;
        }

        // PUT: api/WorksItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorksItem(long id, WorksItem worksItem)
        {
            if (id != worksItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(worksItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorksItemExists(id))
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

        // POST: api/WorksItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorksItem>> PostWorksItem(WorksItem worksItem)
        {
          if (_context.WorksItem == null)
          {
              return Problem("Entity set 'WorksContext.WorksItem'  is null.");
          }
            _context.WorksItem.Add(worksItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorksItem", new { id = worksItem.Id }, worksItem);
        }

        // DELETE: api/WorksItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorksItem(long id)
        {
            if (_context.WorksItem == null)
            {
                return NotFound();
            }
            var worksItem = await _context.WorksItem.FindAsync(id);
            if (worksItem == null)
            {
                return NotFound();
            }

            _context.WorksItem.Remove(worksItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorksItemExists(long id)
        {
            return (_context.WorksItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
