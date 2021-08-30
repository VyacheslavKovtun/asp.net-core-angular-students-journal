using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Business.DTO
{
    public class MarkDTO
    {
        public int Id { get; set; }
        public int SMark { get; set; }
        public long DateTime { get; set; }
        public SubjectDTO Subject { get; set; }
    }
}
