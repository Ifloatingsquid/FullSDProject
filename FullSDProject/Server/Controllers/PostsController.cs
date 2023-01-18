using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullSDProject.Server.Data;
using FullSDProject.Shared.Domain;
using FullSDProject.Server.IRepository;

namespace FullSDProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public PostsController(ApplicationDbContext context)
        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Posts
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        public async Task<IActionResult> GetPosts()
        {
            //return await _context.Posts.ToListAsync();
            var posts = await _unitOfWork.Posts.GetAll();
            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Post>> GetPost(int id)
        public async Task<IActionResult> GetPost(int id)
        {
            //var post = await _context.Posts.FindAsync(id);
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            //_context.Entry(post).State = EntityState.Modified;
            _unitOfWork.Posts.Update(post);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!PostExists(id))
                if (!await PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            //_context.Posts.Add(post);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Posts.Insert(post);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            //var post = await _context.Posts.FindAsync(id);
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            //_context.Posts.Remove(post);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Posts.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> PostExists(int id)
        {
            //return _context.Posts.Any(e => e.Id == id);
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);
            return post != null;
        }
    }
}
