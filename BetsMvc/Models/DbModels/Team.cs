using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsMvc.Models.DbModels
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
