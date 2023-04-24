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

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorksItemDTO>>> GetWorksItems()
        {
            return await _context.WorksItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<WorksItemDTO>> GetWorksItem(long id)
        {
            var worksItem = await _context.WorksItems.FindAsync(id);

            if (worksItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(worksItem);
        }
        // </snippet_GetByID>

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Update>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorksItem(long id, WorksItemDTO worksDTO)
        {
            if (id != worksDTO.Id)
            {
                return BadRequest();
            }

            var worksItem = await _context.WorksItems.FindAsync(id);
            if (worksItem == null)
            {
                return NotFound();
            }

            worksItem.Title = worksDTO.Title;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!WorksItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // </snippet_Update>

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Create>
        [HttpPost]
        public async Task<ActionResult<WorksItemDTO>> PostWorksItem(WorksItemDTO worksDTO)
        {
            var worksItem = new WorksItem
            {
                Title = worksDTO.Title
            };

            _context.WorksItems.Add(worksItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetWorksItem),
                new { id = worksItem.Id },
                ItemToDTO(worksItem));
        }
        // </snippet_Create>

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorksItem(long id)
        {
            var worksItem = await _context.WorksItems.FindAsync(id);
            if (worksItem == null)
            {
                return NotFound();
            }

            _context.WorksItems.Remove(worksItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorksItemExists(long id)
        {
            return _context.WorksItems.Any(e => e.Id == id);
        }

        private static WorksItemDTO ItemToDTO(WorksItem worksItem) =>
           new WorksItemDTO
           {
               Id = worksItem.Id,
               Title = worksItem.Title,
           };
    }
}