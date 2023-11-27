using System.ComponentModel.DataAnnotations;

namespace Mvc_day2.ViewModel
{
    public class RoleViewModel
    {
        public string? id { get; set; }
        [Required (ErrorMessage ="Name of Role is req!")]
        public string name { get; set; }
         
    }
}
