using CMAJ0Q_HFT_2022232.Logic.Interfaces;
using CMAJ0Q_HFT_2022232.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMAJ0Q_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        ITeamLogic teamLogic;

        public TeamController(ITeamLogic docLogic)
        {
            this.teamLogic = docLogic;
        }


        // GET: /team
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return teamLogic.GetAllTeams();
        }

        // GET /team/id
        [HttpGet("{id}")]
        public Team Get(int id)
        {
            return teamLogic.GetTeamById(id);
        }

        // POST /team
        [HttpPost]
        public void Post([FromBody] Team value)
        {
            teamLogic.AddNewTeam(value);

        }

        // PUT /team
        [HttpPut()]
        public void Put([FromBody] Team value)
        {
            teamLogic.UpdateTeam(value);

        }

        // DELETE /team/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var team = teamLogic.GetTeamById(id);
            teamLogic.DeleteTeam(id);
        }
    }
}
