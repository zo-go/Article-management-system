using WebAPI.Domain.Base;

namespace WebAPI.Application.Common.Interface
{
    public interface IRepository<T>
    {
        IQueryable<T> Table { get; }
        Task<T?> GetById(Guid id);

        #region 新增

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddBulkAsync(IEnumerable<T> entities);

        #endregion

        #region 更新

        Task UpdateAsync(T entity);

        Task UpdateBulkAsync(IEnumerable<T> entities);

        #endregion

        #region 删除

        /// <summary>
        /// 删除指定id的记录：如果shouldHardDel为false，则为软删除，只是更新删除标记；否则为硬删除，记录将从数据库移除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shouldHardDel"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        // Task DeleteAsync(T entity, bool shouldHardDel = false);

        Task DeleteBulkAsync(IEnumerable<T> entities, bool shouldHardDel = false);


        #endregion
    }
}