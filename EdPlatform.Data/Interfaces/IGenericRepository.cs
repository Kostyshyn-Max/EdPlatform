using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> Get(int id);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        
        Task Add(T entity);

        void Update(T entity);

        void Remove(int id);
    }
}
