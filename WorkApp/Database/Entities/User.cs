using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Database.Base;

namespace WorkApp.Database.Entities
{
    public class User: BaseEntity<int>
    {
        public enum AuthRole
        {
            User,
            Editor,
            Admin
        }

        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public AuthRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }
        public int Course { get; set; }
        public List<Mark> Marks { get; set; }
    }
}
