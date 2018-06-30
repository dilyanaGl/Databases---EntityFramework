using System;
using System.Linq;
using PetClinic.Data;

namespace PetClinic.DataProcessor
{
    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            var vet = context.Vets.SingleOrDefault(p => p.PhoneNumber == phoneNumber);

            if (vet == null)
            {
                return $"Vet with phone number {phoneNumber} not found!";
            }

            string oldProfession = vet.Profession;

            if (newProfession.Length < 3 || newProfession.Length > 50)
            {
                return "";
            }

            vet.Profession = newProfession;

            context.SaveChanges();

            return $"{vet.Name}'s profession updated from {oldProfession} to {newProfession}.";


        }
    }
}
