using CMAJ0Q_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Repository.ModelRepositories
{
    public class ChampionshipRepository : Repository<Championship>, IRepository<Championship>
    {
        public ChampionshipRepository(ChampionshipDbContext ctx) : base(ctx)
        {

        }
        public override void Create(Championship t)
        {
            ctx.Championships.Add(t);
            ctx.SaveChanges();
        }

        public override void Delete(int id)
        {
            var championshipDelete = Get(id);
            ctx.Championships.Remove(championshipDelete);
            ctx.SaveChanges();
        }

        public override Championship Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.ChampionshipId.Equals(id));
        }

        public override void Update(Championship t)
        {
            var championshipUpdate = Get(t.ChampionshipId);
            championshipUpdate.Name = t.Name;
            championshipUpdate.Location = t.Location;
            ctx.SaveChanges();
        }
    }
}
