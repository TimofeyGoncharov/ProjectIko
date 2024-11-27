using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ProjectIko.Db.Interface;

namespace ProjectIko.Db.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;
        private readonly DbContext _context;
        private readonly ILogger<Repository<T>> _logger;

        public Repository(ILogger<Repository<T>> logger, DbContext context)
        {
            if (context != null)
            {
                _logger = logger;
                _context = context;
                _entities = context.Set<T>();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                List<T> result = await _entities.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<List<T>> GetAllReadOnlyAsync()
        {
            try
            {
                List<T> result = await _entities.AsNoTracking().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                T result = await _entities.FindAsync(id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<T> GetByIdAsync(long id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("entity");
            }
            var result = await _entities.FindAsync(id);

            return result;
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> InsertAndReturnAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _entities.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (Equals(Guid.Empty, id))
                {
                    throw new ArgumentNullException(nameof(id));
                }

                _entities.Remove(await _entities.FindAsync(id));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return;
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            _entities.Remove(await _entities.FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ReloadContext(T entity)
        {
            await _context.Entry(entity).ReloadAsync();
        }
    }
}
