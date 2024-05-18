using DATN_back_end.Entities;

namespace DATN_back_end.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task<IQueryable<T>> Queryable<T>() where T : BaseEntity;
        Task DeleteAsync<T>(T entity, bool isHardDeleted = false) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task AddAsync<T>(T entity) where T : BaseEntity;
    }


}
