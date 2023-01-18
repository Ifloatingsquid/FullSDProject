using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDProject.Shared.Domain
{
    public class Post : BaseDomainModel
    {
        public string PostText { get; set; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        //public int PersonalProfileId { get; set; }
        //public virtual PersonalProfile PersonalProfile { get; set; }

    }
}
