using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;

namespace ZooAPI.Repositories
{
    public interface IRepository<TEntity>
    {
        //CREATE
        Task<int> Add(TEntity entity);
        //READ
        Task<TEntity?> GetById(int id);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        // UPDATE
        Task<bool> Update(TEntity entity);
        // DELETE
        Task<bool> Delete(int id);
    }
}
