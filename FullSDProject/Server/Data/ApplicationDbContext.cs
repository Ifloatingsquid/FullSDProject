using FullSDProject.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FullSDProject.Server.Configurations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullSDProject.Shared.Domain;

namespace FullSDProject.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PersonalProfile> PersonalProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<DatingUser> DatingUsers { get; set; }    
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new DatingUserSeedConfiguration());
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new UserSeedConfiguration());
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        }
    }
}
