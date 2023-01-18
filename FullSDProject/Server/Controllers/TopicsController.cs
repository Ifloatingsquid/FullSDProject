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
    public class TopicsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public TopicsController(ApplicationDbContext context)
        public TopicsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Topics
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
        public async Task<IActionResult> GetTopics()
        {
            //return await _context.Topics.ToListAsync();
            var topics = await _unitOfWork.Topics.GetAll();
            return Ok(topics);
        }

        // GET: api/Topics/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Topic>> GetTopic(int id)
        public async Task<IActionResult> GetTopic(int id)
        {
            //var topic = await _context.Topics.FindAsync(id);
            var topic = await _unitOfWork.Topics.Get(q => q.Id == id);

            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);
        }

        // PUT: api/Topics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopic(int id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            //_context.Entry(topic).State = EntityState.Modified;
            _unitOfWork.Topics.Update(topic);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!TopicExists(id))
                if (!await TopicExists(id))
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

        // POST: api/Topics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Topic>> PostTopic(Topic topic)
        {
            //_context.Topics.Add(topic);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Topics.Insert(topic);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetTopic", new { id = topic.Id }, topic);
        }

        // DELETE: api/Topics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            //var topic = await _context.Topics.FindAsync(id);
            var topic = await _unitOfWork.Topics.Get(q => q.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            //_context.Topics.Remove(topic);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Topics.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> TopicExists(int id)
        {
            //return _context.Topics.Any(e => e.Id == id);
            var topic = await _unitOfWork.Topics.Get(q => q.Id == id);
            return topic != null;
        }
    }
}
