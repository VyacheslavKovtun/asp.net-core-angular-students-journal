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
    public class MarksRepository: BaseRepository<int, Mark>
    {
        public MarksRepository(DatabaseContext ctx) : base(ctx) { }

        public async override Task<Mark> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async override Task UpdateAsync(Mark value)
        {
            var mark = await GetAsync(value.Id);
            mark.SMark = value.SMark;
            mark.DateTime = value.DateTime;

            Subject subject = new Subject()
            {
                Id = value.Subject.Id,
                Name = value.Subject.Name
            };

            mark.Subject = subject;

            ctx.Entry(mark).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
