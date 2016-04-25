using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CETAPUtilities.Model.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey Id);
        void Add(TEntity entity);
        void Remove(TEntity entity);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
