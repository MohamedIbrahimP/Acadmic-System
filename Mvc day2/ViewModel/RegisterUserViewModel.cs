using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.ViewModel
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }//extra info

        public string Address { get; set; }
    }
}
