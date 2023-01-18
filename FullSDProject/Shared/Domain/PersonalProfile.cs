using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FullSDProject.Shared.Domain
{
    public class PersonalProfile : BaseDomainModel
    {
        public string Hobby { get; set; }
        public string FavouriteSong { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string ProfileDes { get; set; }
        public int DatingUserId { get; set; }
        public virtual DatingUser DatingUser { get; set; }
        //public virtual List<Match> Matches { get; set; }
        //public virtual List<Topic> Topics { get; set; }
        //public virtual List<Post> Posts { get; set; }
    }
}
