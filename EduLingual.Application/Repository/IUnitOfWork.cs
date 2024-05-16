using Microsoft.EntityFrameworkCore;

namespace EduLingual.Application.Repository
{
    public interface IUnitOfWork
    {
        public interface IUnitOfWork : IGenericRepositoryFactory, IDisposable
        {
            int Commit();

            Task<int> CommitAsync();
        }

        public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
        {
            TContext Context { get; }
        }
    }
}
