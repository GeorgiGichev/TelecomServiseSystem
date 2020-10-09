namespace TelecomServiceSystem.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateModel<TInput>(TEntity model, TInput input);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
