using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Repositories;

namespace WorkApp.Database.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        MarksRepository MarksRepository { get; }
        UsersRepository UsersRepository { get; }
    }
}
