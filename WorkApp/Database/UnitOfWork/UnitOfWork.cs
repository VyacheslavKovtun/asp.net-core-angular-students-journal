using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Common;
using WorkApp.Database.Repositories;

namespace WorkApp.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        DatabaseContext ctx;

        SubjectsRepository _subjectsRepository;
        MarksRepository _marksRepository;
        UsersRepository _usersRepository;

        public SubjectsRepository SubjectsRepository => _subjectsRepository
            ?? (_subjectsRepository = new SubjectsRepository(ctx));

        public MarksRepository MarksRepository => _marksRepository
            ?? (_marksRepository = new MarksRepository(ctx));

        public UsersRepository UsersRepository => _usersRepository
            ?? (_usersRepository = new UsersRepository(ctx));


        public UnitOfWork(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if (disposing)
                    ctx.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
