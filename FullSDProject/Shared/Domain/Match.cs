using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FullSDProject.Shared.Domain
{
    public class Match : BaseDomainModel
    {
        public int PersonalProfileIdone { get; set; }
        public int PersonalProfileIdtwo { get; set; }
        //[ForeignKey("profileID_one")]
        public virtual PersonalProfile PersonalProfileId_one { get; set; }
        //[ForeignKey("ProfileID_two")]
        public virtual PersonalProfile PersonalProfileId_two { get; set; }
        //public virtual List<Chat> Chats { get; set; }
    }
}
