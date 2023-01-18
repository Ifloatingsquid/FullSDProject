using FullSDProject.Server.Data;
using FullSDProject.Server.IRepository;
using FullSDProject.Server.Models;
using FullSDProject.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FullSDProject.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Chat> _chats;
        private IGenericRepository<DatingUser> _datingusers;
        private IGenericRepository<Match> _matches;
        private IGenericRepository<PersonalProfile> _personalprofiles;
        private IGenericRepository<Post> _posts;
        private IGenericRepository<Topic> _topics;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
            
        public IGenericRepository<Chat> Chats
            => _chats ??= new GenericRepository<Chat>(_context);
        public IGenericRepository<DatingUser> DatingUsers
            => _datingusers ??= new GenericRepository<DatingUser>(_context);
        public IGenericRepository<Match> Matches
            => _matches ??= new GenericRepository<Match>(_context);
        public IGenericRepository<PersonalProfile> PersonalProfiles
            => _personalprofiles ??= new GenericRepository<PersonalProfile>(_context);
        public IGenericRepository<Post> Posts
            => _posts ??= new GenericRepository<Post>(_context);
        public IGenericRepository<Topic> Topics
            => _topics ??= new GenericRepository<Topic>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}