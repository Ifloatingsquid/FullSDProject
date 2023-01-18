using FullSDProject.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullSDProject.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Chat> Chats { get; }
        IGenericRepository<DatingUser> DatingUsers { get; }
        IGenericRepository<Match> Matches { get; }
        IGenericRepository<PersonalProfile> PersonalProfiles { get; }
        IGenericRepository<Post> Posts { get; }
        IGenericRepository<Topic> Topics { get; }
    }
}