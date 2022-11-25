using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email adress")]
        [Required(ErrorMessage ="Email address is needed")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
