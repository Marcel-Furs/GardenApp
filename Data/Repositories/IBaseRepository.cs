﻿using System.Linq.Expressions;

namespace GardenApp.API.Data.Repositories
{
    public interface IBaseRepository<T, TKey> where T : class
    {
        Task<T> Get(TKey id);
        Task<List<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> exp);
        Task<List<T>> GetAll(Expression<Func<T, bool>> exp);
        Task Update(T entity);
        Task Delete(TKey id);
        Task Create(T entity);
    }
}