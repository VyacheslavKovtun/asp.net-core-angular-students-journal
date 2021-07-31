using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Base;

namespace WorkApp.Database.Entities
{
    public class Subject: BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
