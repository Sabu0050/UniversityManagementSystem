using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.DLL.Model
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
