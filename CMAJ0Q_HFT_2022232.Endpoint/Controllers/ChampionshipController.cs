using CMAJ0Q_HFT_2022232.Logic.Interfaces;
using CMAJ0Q_HFT_2022232.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMAJ0Q_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChampionshipController : ControllerBase
    {
        IChampionshipLogic champLogic;
        

        public ChampionshipController(IChampionshipLogic champLogic/*, IHubContext<SignalRHub> hub*/)
        {
            this.champLogic = champLogic;
            
        }

        //GET: /Championship
        [HttpGet]
        public IEnumerable<Championship> Get()
        {
            return champLogic.GetAllChampionships();
        }


        // GET: /Championship/{id}
        [HttpGet("{id}")]
        public Championship Get(int id)
        {
            return champLogic.GetChampionshipById(id);
        }

        // POST: /Championship
        [HttpPost]
        public void Post([FromBody] Championship value)
        {
            champLogic.AddNewChampionship(value);
        }

        // PUT: /Championship
        [HttpPut]
        public void Put([FromBody] Championship value)
        {
            champLogic.UpdateChampionship(value);
        }

        // Delete /Championship/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var champ = champLogic.GetChampionshipById(id);
            champLogic.DeleteChampionship(id);

        }
    }
}
