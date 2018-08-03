using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusTicketSystem.Client.Validation
{
    public static class Validation
    {
        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }
    }
}
