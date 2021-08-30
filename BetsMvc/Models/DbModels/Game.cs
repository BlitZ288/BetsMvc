using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsMvc.Models.DbModels
{
    public class Game
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string DateTime { get; set; }
        public string Score { get; set; }
        public bool Win { get; set; }
        public int Teamid { get; set; }
        public ICollection<Map> Maps { get; set; }
        public virtual Team Team { get; set; }

    }
}
