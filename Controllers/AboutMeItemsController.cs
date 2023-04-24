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

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AboutMeItemDTO>>> GetAboutMeItems()
        {
            return await _context.AboutMeItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<AboutMeItemDTO>> GetAboutMeItem(long id)
        {
            var aboutmeItem = await _context.AboutMeItems.FindAsync(id);

            if (aboutmeItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(aboutmeItem);
        }
        // </snippet_GetByID>

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Update>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAboutMeItem(long id, AboutMeItemDTO aboutmeDTO)
        {
            if (id != aboutmeDTO.Id)
            {
                return BadRequest();
            }

            var aboutmeItem = await _context.AboutMeItems.FindAsync(id);
            if (aboutmeItem == null)
            {
                return NotFound();
            }

            aboutmeItem.Name = aboutmeDTO.Name;
          

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AboutMeItemExists(id))
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
        public async Task<ActionResult<AboutMeItemDTO>> PostAboutMeItem(AboutMeItemDTO aboutmeDTO)
        {
            var aboutmeItem = new AboutMeItem
            {
                
                Name = aboutmeDTO.Name
            };

            _context.AboutMeItems.Add(aboutmeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAboutMeItem),
                new { id = aboutmeItem.Id },
                ItemToDTO(aboutmeItem));
        }
        // </snippet_Create>

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var aboutmeItem = await _context.AboutMeItems.FindAsync(id);
            if (aboutmeItem == null)
            {
                return NotFound();
            }

            _context.AboutMeItems.Remove(aboutmeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AboutMeItemExists(long id)
        {
            return _context.AboutMeItems.Any(e => e.Id == id);
        }

        private static AboutMeItemDTO ItemToDTO(AboutMeItem aboutmeItem) =>
           new AboutMeItemDTO
           {
               Id = aboutmeItem.Id,
               Name = aboutmeItem.Name,
        
           };
    }
    }