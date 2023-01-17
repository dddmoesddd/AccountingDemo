using FrameWork.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FrameWork.Infrastruture.Persistance
{
	public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IAggregateRoot
	{
		private DbContext Context;

		private DbSet<TEntity> _dbSet;

		public RepositoryBase(DbContext context)
		{
			Context = context;

			if (context != null)
			{
				_dbSet = context.Set<TEntity>();
			}
		}

		public TEntity Add(TEntity entity)
		{
			Context.Set<TEntity>().Add(entity);
			return entity;
		}
		public void Delete(TEntity entity)
		{

			Context.Set<TEntity>().Remove(entity);


		}

		public IReadOnlyList<TEntity> GetAll()
		{
			return Context.Set<TEntity>().ToList();
		}

		public void SetDbContext(DbContext context)
		{
			Context = context;
		}

		public void Update(TEntity entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
		}


	}
}
