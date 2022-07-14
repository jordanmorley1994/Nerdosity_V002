using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Nerdosity_V002.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
