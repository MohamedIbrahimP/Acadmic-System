using Microsoft.AspNetCore.Identity;

namespace Mvc_day2.Models
{
    public class ApplicationIdentityUser:IdentityUser
    {
        public string Address { get; set; }

    }
}

