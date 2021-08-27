using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$", 
            ErrorMessage = "Password must be between 6 and 20 characters long and cotains at least one lower case, upper case letter, special character and number")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Repeated password is required.")]
        public string PasswordCheck { get; set; }
        
    }
}
