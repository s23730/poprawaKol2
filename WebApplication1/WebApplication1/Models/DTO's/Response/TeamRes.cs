using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.DTO_s.Response
{
    public class TeamRes
    {
        public Team Team { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
