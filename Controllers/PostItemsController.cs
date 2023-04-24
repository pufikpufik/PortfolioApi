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


        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostItemDTO>>> GetPostItems()
        {
            return await _context.PostItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostItemDTO>> GetPostItem(long id)
        {
            var postItem = await _context.PostItems.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(postItem);
        }
        // </snippet_GetByID>

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Update>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostItem(long id, PostItemDTO postDTO)
        {
            if (id != postDTO.Id)
            {
                return BadRequest();
            }

            var postItem = await _context.PostItems.FindAsync(id);
            if (postItem == null)
            {
                return NotFound();
            }

            postItem.Title = postDTO.Title;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PostItemExists(id))
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
        public async Task<ActionResult<PostItemDTO>> PostTodoItem(PostItemDTO postDTO)
        {
            var postItem = new PostItem
            {

                Title = postDTO.Title
            };

            _context.PostItems.Add(postItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPostItem),
                new { id = postItem.Id },
                ItemToDTO(postItem));
        }
        // </snippet_Create>

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostItem(long id)
        {
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
            return _context.PostItems.Any(e => e.Id == id);
        }

        private static PostItemDTO ItemToDTO(PostItem postItem) =>
           new PostItemDTO
           {
               Id = postItem.Id,
               Title = postItem.Title,

           };
    }
}