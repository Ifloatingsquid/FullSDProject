using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDProject.Shared.Domain
{
    public class Topic : BaseDomainModel
    {
        public DateTime TimeStamp { get; set; }
        public string TopicTitle { get; set; }
        public string TopicDesc { get; set; }
        public int PersonalProfileId { get; set; }
        public virtual PersonalProfile PersonalProfile { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
