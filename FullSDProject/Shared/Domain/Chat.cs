using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSDProject.Shared.Domain
{
    public class Chat : BaseDomainModel
    {
        public string MessageText { get; set; }
        public DateTime ChatTimeStamp { get; set; }
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}
