using CMAJ0Q_HFT_2022232.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CMAJ0Q_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IChampionshipLogic champLogic;
        ITeamLogic teamLogic;
        IPlayerLogic playerLogic;

        public StatController(IChampionshipLogic champLogic, ITeamLogic teamLogic, IPlayerLogic playerLogic)
        {
            this.champLogic = champLogic;
            this.teamLogic = teamLogic;
            this.playerLogic = playerLogic;
        }
        [HttpGet] //stat/avgageofteamsplayers
        public IEnumerable<KeyValuePair<string, double>> AVGAgeOfTeamsPlayers()
        {
            return teamLogic.AVGAgeOfTeamsPlayers();
        }

        [HttpGet] //stat/teamnicknamecount
        public IEnumerable<KeyValuePair<string, int>> TeamNicknameCount()
        {
            return champLogic.TeamNicknameCount();
        }

        [HttpGet] //stat/playersperchampionship
        public IEnumerable<KeyValuePair<string, int>> PlayersPerChampionship()
        {
            return champLogic.PlayersPerChampionship().ToList();
        }

        [HttpGet("{championshipID}")] //stat/teamnicknamecountinspecificchampionship/{championshipID}
        public IEnumerable<KeyValuePair<string, int>> TeamNicknameCountInSpecificChampionship(int championshipID)
        {
            return champLogic.TeamNicknameCountInSpecificChampionship(championshipID);
        }

        [HttpGet("{nationality}")] //stat/IsNationalityPresent/{nationality}
        public bool IsNationalityPresent(string nationality)
        {
            return playerLogic.IsNationalityPresent(nationality);
        }

        [HttpGet] //stat/allnationalitiesperteam
        public IEnumerable<IEnumerable<string>> AllNationalitiesPerTeams()
        {
            var allnationaliy = teamLogic.AllNationalitiesPerTeam();


            return allnationaliy;
        }
    }
}
