using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ChampionshipDbContext ctx { get; set; }
        public Repository(ChampionshipDbContext ctx)
        {
            this.ctx = ctx;
        }

        public abstract void Create(T t);

        public abstract void Delete(int id);

        public abstract T Get(int id);

        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>();
        }

        public abstract void Update(T item);
    }
}
