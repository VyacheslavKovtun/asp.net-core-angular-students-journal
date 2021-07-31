using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Common;
using WorkApp.Database.Repositories;

namespace WorkApp.Database.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private static UnitOfWork instance;
        public static UnitOfWork Instance => instance ?? (instance = new UnitOfWork());

        private DatabaseContext ctx;

        private SubjectsRepository subjectsRepository;
        private MarksRepository marksRepository;
        private UsersRepository usersRepository;

        public SubjectsRepository SubjectsRepository => subjectsRepository
            ?? (subjectsRepository = new SubjectsRepository(ctx));

        public MarksRepository MarksRepository => marksRepository
            ?? (marksRepository = new MarksRepository(ctx));

        public UsersRepository UsersRepository => usersRepository
            ?? (usersRepository = new UsersRepository(ctx));


        private UnitOfWork()
        {
            ctx = new DatabaseContext();
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
