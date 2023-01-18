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
    public class ChatsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public ChatsController(ApplicationDbContext context)
        public ChatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Chats
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        public async Task<IActionResult> GetChats()
        {
            //return await _context.Chats.ToListAsync();
            var chats = await _unitOfWork.Chats.GetAll(includes: q => q.Include(x =>x.Match));
            return Ok(chats);
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Chat>> GetChat(int id)
        public async Task<IActionResult> GetChat(int id)
        {
            //var chat = await _context.Chats.FindAsync(id);
            var chat = await _unitOfWork.Chats.Get(q => q.Id == id);

            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }

        // PUT: api/Chats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(int id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            //_context.Entry(chat).State = EntityState.Modified;
            _unitOfWork.Chats.Update(chat);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);    
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ChatExists(id))
                if (!await ChatExists(id))
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

        // POST: api/Chats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(Chat chat)
        {
            //_context.Chats.Add(chat);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Chats.Insert(chat);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetChat", new { id = chat.Id }, chat);
        }

        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            //var chat = await _context.Chats.FindAsync(id);
            var chat = await _unitOfWork.Chats.Get(q => q.Id == id);
            if (chat == null)
            {
                return NotFound();
            }

            //_context.Chats.Remove(chat);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Chats.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> ChatExists(int id)
        {
            //return _context.Chats.Any(e => e.Id == id);
            var chat = await _unitOfWork.Chats.Get(q => q.Id == id);
            return chat != null;
        }
    }
}
