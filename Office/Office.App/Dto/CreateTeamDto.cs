using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Office.App.Dto
{
    public class CreateTeamDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        public string Acronym { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }
        
    }
}
