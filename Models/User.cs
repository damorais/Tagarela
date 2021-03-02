using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Tagarela.Models
{
    public class User : IdentityUser<Guid>
    {
        public override Guid Id {get;set;} = Guid.NewGuid();
        
        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        public string DisplayName { get; set; }

        public ICollection<User> Segue { get; set; }

        public ICollection<User> SeguidoPor { get; set; }

    }
}