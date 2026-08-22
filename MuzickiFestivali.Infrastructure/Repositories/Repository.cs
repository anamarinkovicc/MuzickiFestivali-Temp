using Microsoft.EntityFrameworkCore;
using MuzickiFestivali.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MuzickiFestivaliDbContext Context;
        protected readonly DbSet<T> DbSet; 

        public Repository(MuzickiFestivaliDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(params object[] keyValues) =>
            await DbSet.FindAsync(keyValues);

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await DbSet.ToListAsync();

        public async Task AddAsync(T entity) =>
            await DbSet.AddAsync(entity);

        public void Update(T entity) =>
            DbSet.Update(entity);

        public virtual void Delete(T entity) =>
            DbSet.Remove(entity);
    }
}
