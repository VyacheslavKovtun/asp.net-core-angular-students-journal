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
    public class MarksRepository : BaseRepository<int, Mark>
    {
        public MarksRepository(DatabaseContext ctx): base(ctx) { }

        public async override Task<Mark> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async override Task UpdateAsync(Mark value)
        {
            var mark = await GetAsync(value.Id);
            mark.SMark = value.SMark;

            Subject subject = new Subject();
            subject.Id = value.Subject.Id;
            subject.Name = value.Subject.Name;

            mark.Subject = subject;

            ctx.Entry(mark).State = EntityState.Modified;

            await ctx.SaveChangesAsync();
        }
    }
}
