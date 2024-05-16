using static EduLingual.Application.Repository.IGenericRepository;

namespace EduLingual.Application.Repository
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
