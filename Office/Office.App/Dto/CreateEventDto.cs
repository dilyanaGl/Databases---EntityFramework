using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Office.App.Dto
{
    public class CreateEventDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

    }
}
