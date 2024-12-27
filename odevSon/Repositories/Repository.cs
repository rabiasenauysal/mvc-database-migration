using Microsoft.EntityFrameworkCore;
using odevSon.Interfaces;
using odevSon.Models;
using System;
using System.Linq.Expressions;

namespace odevSon.Repositories

{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProductVT _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ProductVT context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veritabanı hatası: {ex.Message}");
                throw;
            }
        }



        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();
    }
}
