using CMAJ0Q_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Repository.ModelRepositories
{
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    {
        public PlayerRepository(ChampionshipDbContext ctx) : base(ctx)
        {

        }

        public override void Create(Player t)
        {
            ctx.Players.Add(t);
            ctx.SaveChanges();
        }

        public override void Delete(int id)
        {
            var playerDelete = Get(id);
            ctx.Players.Remove(playerDelete);
            ctx.SaveChanges();
        }

        public override Player Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.PlayerId.Equals(id));
        }

        public override void Update(Player t)
        {
            var playerUpdate = Get(t.PlayerId);
            playerUpdate.Name = t.Name;
            playerUpdate.Age = t.Age;
            playerUpdate.Nationality = t.Nationality;
            ctx.SaveChanges();
        }
    }
}
