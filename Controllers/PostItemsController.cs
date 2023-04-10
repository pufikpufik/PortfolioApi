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
    public class PostItemsController : ControllerBase
    {
        private readonly PostContext _context;

        public PostItemsController(PostContext context)
        {
            _context = context;
        }

        // GET: api/PostItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostItem>>> GetTodoItems()
        {
          if (_context.PostItems == null)
          {
              return NotFound();
          }
            return await _context.PostItems.ToListAsync();
        }

        // GET: api/PostItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostItem>> GetPostItem(long id)
        {
            var postItem = await _context.PostItems.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }
                   
            return postItem;
        }

        // PUT: api/PostItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostItem(long id, PostItem postItem)
        {
            if (id != postItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(postItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostItemExists(id))
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

        // POST: api/PostItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostItem>> PostPostItem(PostItem postItem)
        {
            _context.PostItems.Add(postItem);
            await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetPostItem", new { id = postItem.Id }, postItem);
            return CreatedAtAction(nameof(GetPostItem), new { id = postItem.Id }, postItem);
        }

        // DELETE: api/PostItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostItem(long id)
        {
            if (_context.PostItems == null)
            {
                return NotFound();
            }
            var postItem = await _context.PostItems.FindAsync(id);
            if (postItem == null)
            {
                return NotFound();
            }

            _context.PostItems.Remove(postItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostItemExists(long id)
        {
            return (_context.PostItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
