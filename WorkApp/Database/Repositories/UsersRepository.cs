using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Base;
using WorkApp.Database.Common;
using WorkApp.Database.Entities;

namespace WorkApp.Database.Repositories
{
    public class UsersRepository: BaseRepository<int, User>
    {
        public UsersRepository(DatabaseContext ctx): base(ctx) { }

        public async override Task<User> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async override Task UpdateAsync(User value)
        {
            var user = await GetAsync(value.Id);

            user.Login = value.Login;
            user.Password = value.Password;
            user.FirstName = value.FirstName;
            user.LastName = value.LastName;
            user.Age = value.Age;
            user.Group = value.Group;
            user.Course = value.Course;

            if (value.Marks.Count != 0)
            {
                foreach (var mark in value.Marks)
                {
                    user.Marks.Add(mark);
                }
            }

            ctx.Entry(user).State = EntityState.Modified;

            await ctx.SaveChangesAsync();
        }
    }
}
