using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Buyit.BOL.DTO.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassWord { get; set; }



    }
}
