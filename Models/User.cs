using Microsoft.AspNetCore.Identity;

namespace Tagarela.Models 
{
    public class User : IdentityUser 
    {
        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        public string DisplayName { get; set; }
    }
}