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
    public class DatingUsersController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public DatingUsersController(ApplicationDbContext context)
        public DatingUsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/DatingUsers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<DatingUser>>> GetDatingUsers()
        public async Task<IActionResult> GetDatingUsers()
        {
            //return await _context.DatingUsers.ToListAsync();
            var datingusers = await _unitOfWork.DatingUsers.GetAll();
            return Ok(datingusers);
        }

        // GET: api/DatingUsers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<DatingUser>> GetDatingUser(int id)
        public async Task<IActionResult> GetDatingUser(int id)
        {
            //var datinguser = await _context.DatingUsers.FindAsync(id);
            var datinguser = await _unitOfWork.DatingUsers.Get(q => q.Id == id);

            if (datinguser == null)
            {
                return NotFound();
            }

            return Ok(datinguser);
        }

        // PUT: api/DatingUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatingUser(int id, DatingUser datinguser)
        {
            if (id != datinguser.Id)
            {
                return BadRequest();
            }

            //_context.Entry(datinguser).State = EntityState.Modified;
            _unitOfWork.DatingUsers.Update(datinguser);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!DatingUserExists(id))
                if (!await DatingUserExists(id))
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

        // POST: api/DatingUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DatingUser>> PostDatingUser(DatingUser datinguser)
        {
            //_context.DatingUsers.Add(datinguser);
            //await _context.SaveChangesAsync();
            await _unitOfWork.DatingUsers.Insert(datinguser);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDatingUser", new { id = datinguser.Id }, datinguser);
        }

        // DELETE: api/DatingUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatingUser(int id)
        {
            //var datinguser = await _context.DatingUsers.FindAsync(id);
            var datinguser = await _unitOfWork.DatingUsers.Get(q => q.Id == id);
            if (datinguser == null)
            {
                return NotFound();
            }

            //_context.DatingUsers.Remove(datinguser);
            //await _context.SaveChangesAsync();
            await _unitOfWork.DatingUsers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> DatingUserExists(int id)
        {
            //return _context.DatingUsers.Any(e => e.Id == id);
            var datinguser = await _unitOfWork.DatingUsers.Get(q => q.Id == id);
            return datinguser != null;
        }
    }
}
