using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Office.App.Dto
{
    public class RegisterUserDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }

        [MinLength(6)]
        [MaxLength(30)]
        [RegularExpression(@"^(?=.*?[0-9])(?=.*[A-Z]).{6,12}$")]
        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Range(0, int.MaxValue)]
        public int Age { get; set; }

        public string Gender { get; set; }
    }
}
