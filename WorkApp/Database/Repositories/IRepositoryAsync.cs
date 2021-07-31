using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Database.Repositories
{
    public interface IRepositoryAsync<TKey, TValue> 
        where TKey: struct
        where TValue: class
    {
        Task CreateAsync(TValue value);
        Task<IEnumerable<TValue>> GetAllAsync();
        Task<TValue> GetAsync(TKey id);
        Task UpdateAsync(TValue value);
        Task DeleteAsync(TKey id);
    }
}
