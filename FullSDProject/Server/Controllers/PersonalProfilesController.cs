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
    public class PersonalProfilesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public PersonalProfilesController(ApplicationDbContext context)
        public PersonalProfilesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/PersonalProfiles
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<PersonalProfile>>> GetPersonalProfiles()
        public async Task<IActionResult> GetPersonalProfiles()
        {
            //return await _context.PersonalProfiles.ToListAsync();
            var personalprofiles = await _unitOfWork.PersonalProfiles.GetAll(includes: q => q.Include(x => x.DatingUser));
            return Ok(personalprofiles);
        }

        // GET: api/PersonalProfiles/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<PersonalProfile>> GetPersonalProfile(int id)
        public async Task<IActionResult> GetPersonalProfile(int id)
        {
            //var personalprofile = await _context.PersonalProfiles.FindAsync(id);
            var personalprofile = await _unitOfWork.PersonalProfiles.Get(q => q.Id == id);

            if (personalprofile == null)
            {
                return NotFound();
            }

            return Ok(personalprofile);
        }

        // PUT: api/PersonalProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalProfile(int id, PersonalProfile personalprofile)
        {
            if (id != personalprofile.Id)
            {
                return BadRequest();
            }

            //_context.Entry(personalprofile).State = EntityState.Modified;
            _unitOfWork.PersonalProfiles.Update(personalprofile);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!PersonalProfileExists(id))
                if (!await PersonalProfileExists(id))
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

        // POST: api/PersonalProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonalProfile>> PostPersonalProfile(PersonalProfile personalprofile)
        {
            //_context.PersonalProfiles.Add(personalprofile);
            //await _context.SaveChangesAsync();
            await _unitOfWork.PersonalProfiles.Insert(personalprofile);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPersonalProfile", new { id = personalprofile.Id }, personalprofile);
        }

        // DELETE: api/PersonalProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalProfile(int id)
        {
            //var personalprofile = await _context.PersonalProfiles.FindAsync(id);
            var personalprofile = await _unitOfWork.PersonalProfiles.Get(q => q.Id == id);
            if (personalprofile == null)
            {
                return NotFound();
            }

            //_context.PersonalProfiles.Remove(personalprofile);
            //await _context.SaveChangesAsync();
            await _unitOfWork.PersonalProfiles.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> PersonalProfileExists(int id)
        {
            //return _context.PersonalProfiles.Any(e => e.Id == id);
            var personalprofile = await _unitOfWork.PersonalProfiles.Get(q => q.Id == id);
            return personalprofile != null;
        }
    }
}
