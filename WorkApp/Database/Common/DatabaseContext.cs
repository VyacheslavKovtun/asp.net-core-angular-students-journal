using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Entities;

namespace WorkApp.Database.Common
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
