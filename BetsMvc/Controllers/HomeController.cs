using BetsMvc.Models;
using BetsMvc.Models.DbModels;
using BetsMvc.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BetsMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _myDbContext = new MyDbContext();
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {

            //TODO фильтрация данных без обновления страницы asp net (stackoverflow)
            //Ajax запросы вот такие дела , делаем это чтобы не обновлять страницу и списки не скрывались

            List<Team> Team = _myDbContext.Teams.ToList();          
            List<Game> games = _myDbContext.Games.ToList();         
            List<Map> maps = _myDbContext.Maps.ToList();          
          
            TeamGamesModelView model = new TeamGamesModelView() { Teams = Team,Games=games, Maps=maps };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Filter(int? kills = 0, int? time = 0, int idTeam = 1, int? NumberGame=0)
        {
            int ResultNumberGame = 0;
            bool flagNull = false;
            List<Map> AllMapsTeam = _myDbContext.Maps.Where(m => m.Game.Teamid == idTeam).ToList();
            List<Game> Allgames = _myDbContext.Games.Where(g=>g.Team.Id==idTeam).ToList();
            List<Game> games = new List<Game>();
            List<Map> maps = new List<Map>();
            if ( time!=0 && kills!=0)
            maps = _myDbContext.Maps.Where(m=>m.Kills>=kills && m.Time>=time && m.Game.Team.Id == idTeam).ToList();
            if (time == 0 && kills != 0)             
            maps = _myDbContext.Maps.Where(m => m.Kills >= kills &&m.Game.Team.Id==idTeam).ToList();
            if(time!=0 && kills==0)
            maps = _myDbContext.Maps.Where(m => m.Time >= time && m.Game.Team.Id == idTeam).ToList();
            if (time == 0 && kills == 0)
            {
                maps = _myDbContext.Maps.ToList();
                flagNull = true;
            }
                
                
           
            foreach (Map m in maps)
            {
                games.Add(_myDbContext.Games.Find(m.GameId)); 
            }
            if (NumberGame != 0)
            {
                var m = games.ElementAt(1).Maps;
                games.Where(g => g.Maps.Count >= NumberGame);
                ResultNumberGame = games.Count * 100 / Allgames.Count;

            }/*Валидация с пустыми приходами */
            if (flagNull) {

            }
            int ResultStat = maps.Count * 100 / AllMapsTeam.Count;
             
            TeamGamesModelView model = new TeamGamesModelView() {
                Games = games, 
                Maps = maps,
                ResultStatus= ResultStat,
                ResultStatusNumber = ResultNumberGame == 0 ? 0 : ResultNumberGame
            };
            ViewData["IdTeam"] = idTeam;

            return PartialView("_Filter",model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
