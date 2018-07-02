﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetClinic.Models
{
    public class Passport
    {

        [RegularExpression("^[a-zA-Z]{7}\\d{3}$")]
        public string SerialNumber { get; set; }

        [Required]
        public Animal Animal { get; set; }

        [Required]
        [RegularExpression("(^[+]359[0-9]{9}$)|(^(0[0-9]{9})$)")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string OwnerName { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

       public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }






    }
}