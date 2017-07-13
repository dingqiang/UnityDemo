using Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.DbContext;
using Repository.Interface;

namespace Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly DbEntities _dbEntities = null;

        private readonly DbSet<TEntity> _dbSet = null;

        protected RepositoryBase()
        {
            _dbEntities = new DbEntities();
            _dbSet = _dbEntities.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbEntities.SaveChanges();
        }

        public void Delete(object id)
        {
            var entity = Get(id);
            if (entity == null) return;
            entity.IsDel = true;
            _dbEntities.SaveChanges();
        }

        public TEntity Get(object id)
        {
            return _dbSet.Find(id);
        }

        public IList<TEntity> FindAll(Func<TEntity, bool> fun)
        {
            return _dbSet.Where(c => !c.IsDel).Where(fun).ToList();
        }
    }
}
