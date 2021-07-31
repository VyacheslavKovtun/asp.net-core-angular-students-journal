using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Common;
using WorkApp.Database.Repositories;

namespace WorkApp.Database.Base
{
    public abstract class BaseRepository<TKey, TValue> : IRepositoryAsync<TKey, TValue>
        where TKey : struct
        where TValue : class
    {
        protected DatabaseContext ctx;
        protected DbSet<TValue> table => ctx.Set<TValue>();

        public BaseRepository(DatabaseContext ctx) 
        {
            this.ctx = ctx;
        }

        public async Task CreateAsync(TValue value)
        {
            ctx.Entry(value).State = EntityState.Added;
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(TKey id)
        {
            var item = await GetAsync(id);
            ctx.Entry(item).State = EntityState.Deleted;
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<TValue>> GetAllAsync()
        {
            return await table.ToListAsync().ConfigureAwait(false);
        }

        public abstract Task<TValue> GetAsync(TKey id);

        public abstract Task UpdateAsync(TValue value);
    }
}
