using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Database.Base
{
    public abstract class BaseEntity<T> where T: struct
    {
        public T Id { get; set; }
    }
}
