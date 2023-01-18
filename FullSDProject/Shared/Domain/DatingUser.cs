using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDProject.Shared.Domain
{
    public class DatingUser : BaseDomainModel
    {
        public string Username { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
