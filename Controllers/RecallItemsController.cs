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
        public async Task<ActionResult<IEnumerable<RecallItemDTO>>> GetRecallItems()
        {
            return await _context.RecallItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<RecallItemDTO>> GetRecallItem(long id)
        {
            var recallItem = await _context.RecallItems.FindAsync(id);

            if (recallItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(recallItem);
        }
        // </snippet_GetByID>

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Update>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecallItem(long id, RecallItemDTO recallDTO)
        {
            if (id != recallDTO.Id)
            {
                return BadRequest();
            }

            var recallItem = await _context.RecallItems.FindAsync(id);
            if (recallItem == null)
            {
                return NotFound();
            }

            recallItem.Name = recallDTO.Name;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RecallItemExists(id))
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
        public async Task<ActionResult<RecallItemDTO>> PostRecallItem(RecallItemDTO recallDTO)
        {
            var recallItem = new RecallItem
            {
                Name = recallDTO.Name
            };

            _context.RecallItems.Add(recallItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRecallItem),
                new { id = recallItem.Id },
                ItemToDTO(recallItem));
        }
        // </snippet_Create>

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecallItem(long id)
        {
            var recallItem = await _context.RecallItems.FindAsync(id);
            if (recallItem == null)
            {
                return NotFound();
            }

            _context.RecallItems.Remove(recallItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecallItemExists(long id)
        {
            return _context.RecallItems.Any(e => e.Id == id);
        }

        private static RecallItemDTO ItemToDTO(RecallItem recallItem) =>
           new RecallItemDTO
           {
               Id = recallItem.Id,
               Name = recallItem.Name,

           };
    }
}