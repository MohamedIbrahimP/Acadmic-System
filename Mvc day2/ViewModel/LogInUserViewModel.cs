using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.ViewModel
{
    public class LogInUserViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
