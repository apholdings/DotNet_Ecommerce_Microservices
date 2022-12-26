using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductAPI.Data;
using ProductAPI.Repository.IRepository;
using System.Linq.Expressions;
using System.Reflection;

namespace ProductAPI.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		private ApplicationDbContext db;
		private readonly IMemoryCache _memoryCache;
		public Repository(ApplicationDbContext db, IMemoryCache memoryCache)
		{
			_db = db;
			_memoryCache = memoryCache;
			this.dbSet = _db.Set<T>();
		}

		public Repository(ApplicationDbContext db)
		{
			this.db = db;
		}

		public async Task CreateAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			await SaveAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
		{
			IQueryable<T> query = dbSet;

			if (!tracked)
			{
				query = query.AsNoTracking();
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}



			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
			int pageSize = 0, int pageNumber = 1)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (pageSize > 0)
			{
				if (pageSize > 100)
				{
					pageSize = 100;
				}
				query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
			}

			return await query.ToListAsync();
		}

		public async Task RemoveAsync(T entity)
		{
			dbSet.Remove(entity);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		
	}
}
