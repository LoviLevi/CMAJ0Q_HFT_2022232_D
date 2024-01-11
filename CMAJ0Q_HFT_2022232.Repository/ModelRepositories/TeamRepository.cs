using CMAJ0Q_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Repository.ModelRepositories
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        public TeamRepository(ChampionshipDbContext ctx) : base(ctx)
        {

        }
        public override void Create(Team t)
        {
            ctx.Teams.Add(t);
            ctx.SaveChanges();
        }

        public override void Delete(int id)
        {
            var teamDelete = Get(id);
            ctx.Teams.Remove(teamDelete);
            ctx.SaveChanges();
        }

        public override Team Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.TeamId.Equals(id));
        }

        public override void Update(Team t)
        {
            var teamUpdate = Get(t.TeamId);
            teamUpdate.Name = t.Name;
            teamUpdate.Nickname = t.Nickname;
            ctx.SaveChanges();
        }
    }
}
