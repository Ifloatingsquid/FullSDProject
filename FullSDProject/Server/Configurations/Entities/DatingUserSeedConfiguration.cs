using FullSDProject.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullSDProject.Server.Configurations.Entities
{
    public class DatingUserSeedConfiguration : IEntityTypeConfiguration<DatingUser>
    {
        public void Configure(EntityTypeBuilder<DatingUser> builder)
        {
            builder.HasData(
                new DatingUser
                {
                    Id = 1,
                    Username = "James",
                    Age = 18,
                    Gender = "Male",
                    Email = "James.gmail.com",
                    Password = "1234",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new DatingUser
                {
                    Id = 2,
                    Username = "Mary",
                    Age = 18,
                    Gender = "Female",
                    Email = "mary.gmail.com",
                    Password = "1234",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
             );
        }
    }
}
