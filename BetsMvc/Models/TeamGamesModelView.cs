using BetsMvc.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsMvc.Models
{
    public class TeamGamesModelView
    {
        public List<Team>Teams { get; set; }
        public List<Game>Games { get; set; }
        public List<Map>Maps { get; set; }
    }
}
