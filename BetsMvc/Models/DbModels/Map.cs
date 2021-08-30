using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsMvc.Models.DbModels
{
    public class Map
    {
        public int Id { get; set; }
        public int Kills { get; set; }
        public int Time { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}
