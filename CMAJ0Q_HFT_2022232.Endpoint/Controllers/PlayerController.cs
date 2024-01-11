using CMAJ0Q_HFT_2022232.Logic.Interfaces;
using CMAJ0Q_HFT_2022232.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMAJ0Q_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        IPlayerLogic playerLogic;

        public PlayerController(IPlayerLogic patLogic/*, IHubContext<SignalRHub> hub*/)
        {
            this.playerLogic = patLogic;
        }


        // GET: /player
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return playerLogic.GetAllPlayers();
        }

        // GET /player/id
        [HttpGet("{id}")]
        public Player Get(int id)
        {
            return playerLogic.GetPlayerById(id);
        }

        // POST /player
        [HttpPost]
        public void Post([FromBody] Player value)
        {
            playerLogic.AddNewPlayer(value);
        }

        // PUT /player
        [HttpPut()]
        public void Put([FromBody] Player value)
        {
            playerLogic.UpdatePlayer(value);
        }

        // DELETE /player/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var player = playerLogic.GetPlayerById(id);
            playerLogic.DeletePlayer(id);
        }
    }
}
