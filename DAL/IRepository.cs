using entities.Models;
using System.Linq.Expressions;

namespace DAL
{
    public interface IRepository : IDisposable
    {
        Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class;

        Task<bool> DeleteAsync<TEntity>(TEntity toDelete) where TEntity : class;

        Task<bool> UpdateAsync<TEntity>(TEntity todelete) where TEntity : class;
        //Task<TEntity> RetreiveAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        Task<List<TEntity>> FirlterAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        Task<TEntity> RetreiveAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        Task RetrieveAsync(Expression<Func<Customer, bool>> criteria);
        Task<T> RetrieveAsync<T>(Func<T, bool> value);
        Task<List<T>?> FilterAsync<T>(Expression<Func<T, bool>> allCustomersCriteria);
    }
}