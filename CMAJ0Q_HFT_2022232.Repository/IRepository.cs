using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T t);
        T Get(int id);
        IQueryable<T> GetAll();
        void Update(T t);
        void Delete(int id);
    }
}
