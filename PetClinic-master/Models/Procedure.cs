using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Procedure
    {
        public int Id { get; set; }

        [Required]
        public int AnimalId { get; set; }

        public Animal Animal { get; set; }

        public decimal Cost { get; set; }

        public int VetId { get; set; }

        [Required]
        public Vet Vet { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }
    }
}