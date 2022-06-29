using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class UserDetails : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}
