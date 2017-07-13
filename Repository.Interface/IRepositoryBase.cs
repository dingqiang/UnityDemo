using System;
using System.Collections;
using System.Collections.Generic;
using Data.Entities;

namespace Repository.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        TEntity Get(object id);

        void Add(TEntity entity);

        void Delete(object id);

        IList<TEntity> FindAll(Func<TEntity, bool> func);
    }
}