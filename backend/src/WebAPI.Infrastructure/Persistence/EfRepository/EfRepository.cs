using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Common.Interface;
using WebAPI.Domain.Base;

namespace WebAPI.Infrastructure.Persistence.EfRepository
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly WebAPIDbContext _db;
        private readonly DbSet<T> _table;
        private readonly ISessionUserService _sessionUserService;

        public EfRepository(WebAPIDbContext db, ISessionUserService sessionUserService)
        {
            _db = db;
            _table = _db.Set<T>();
            _sessionUserService = sessionUserService;
        }

        public IQueryable<T> Table => _table.AsNoTracking();
        // public IQueryable<T> Table {
        //     get{
        //         return _table as IQueryable<T>;
        //     }
        // }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            // entity.CreatedBy = _sessionUserService.UserId;
            // entity.UpdatedBy = _sessionUserService.UserId;
            entity.IsActived = true;
            entity.IsDeleted = false;
            entity.DisplayOrder = 0;
            await _table.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddBulkAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.CreatedBy = _sessionUserService.UserId;
                entity.UpdatedBy = _sessionUserService.UserId;
                entity.IsActived = true;
                entity.IsDeleted = false;
                entity.DisplayOrder = 0;
            }
            await _table.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await this.GetById(id);
            if (entity != null)
            {
                _table.Remove(entity);
            }
            await _db.SaveChangesAsync();
            // if (entity != null)
            // {
            //     await this.DeleteAsync(entity, shouldHardDel);
            // }
        }

        // public async Task DeleteAsync(T entity, bool shouldHardDel = false)
        // {
        //     if (shouldHardDel)
        //     {
        //         _table.Remove(entity);
        //         await _db.SaveChangesAsync();
        //     }
        //     else
        //     {
        //         entity.IsDeleted = true;
        //         entity.UpdatedAt = DateTime.UtcNow;
        //         entity.UpdatedBy = _sessionUserService.UserId;
        //         _table.Update(entity);
        //         await _db.SaveChangesAsync();
        //     }
        // }

        public async Task DeleteBulkAsync(IEnumerable<T> entities, bool shouldHardDel = false)
        {
            if (shouldHardDel)
            {
                _table.RemoveRange(entities);
                await _db.SaveChangesAsync();
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                    // entity.UpdatedAt=DateTime.UtcNow;
                }
                await this.UpdateBulkAsync(entities);
            }
        }

        public async Task<T?> GetById(Guid id)
        {
            var entity = await _table.FindAsync(id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _sessionUserService.UserId;

            _table.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateBulkAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = _sessionUserService.UserId;
            }
            _table.UpdateRange(entities);
            await _db.SaveChangesAsync();
        }
    }
}