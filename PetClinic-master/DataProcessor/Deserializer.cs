using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using PetClinic.DataProcessor.Inports;
using PetClinic.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Deserializer
    {
        private const string successAnimalAidsMessage = "Record {0} successfully imported.";

        private const string successAnimalMessage =
            "Record {0} Passport №: {1} successfully imported.";

        private const string successVetImport = "Record {0} successfully imported.";

        private const string successProcedureImport = "Record successfully imported.";
        private const string failureMessage = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedAnimalAids = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);

            var validAnimalAids = new List<AnimalAid>();

            foreach (var aid in deserializedAnimalAids)
            {
                if (!IsValid(aid))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                if (validAnimalAids.Any(p => p.Name == aid.Name))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                var animalAid = new AnimalAid()
                {
                    Name = aid.Name,
                    Price = aid.Price,
                    AnimalAidProcedures = new List<ProcedureAnimalAid>()
                };

                validAnimalAids.Add(animalAid);
                sb.AppendLine(String.Format(successAnimalAidsMessage, aid.Name));

            }

            context.AnimalAids.AddRange(validAnimalAids);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedJson = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);
            var validAnimals = new List<Animal>();

            foreach (var animalDto in deserializedJson)
            {
                if (!IsValid(animalDto))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                if (validAnimals.Any(p => p.Passport.SerialNumber.Equals(animalDto.Passport.SerialNumber)))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                if (animalDto.Passport.OwnerName.Length < 3 || animalDto.Passport.OwnerName.Length > 20)
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }


                var regexPhoneNumber = new Regex("(^[+]359[0-9]{9}$)|(^(0[0-9]{9})$)");
                var regexSerialNumber = new Regex("^[a-zA-Z]{7}\\d{3}$");

                if (!regexSerialNumber.IsMatch(animalDto.Passport.SerialNumber) ||
                    !regexPhoneNumber.IsMatch(animalDto.Passport.OwnerPhoneNumber))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }


                DateTime registrationDate;
                try
                {
                    registrationDate = DateTime.ParseExact(animalDto.Passport.RegistrationDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture);
                }
                catch
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                var passport = new Passport()
                {
                    SerialNumber = animalDto.Passport.SerialNumber,
                    OwnerName = animalDto.Passport.OwnerName,
                    OwnerPhoneNumber = animalDto.Passport.OwnerPhoneNumber,
                    RegistrationDate = registrationDate

                };

                var animal = new Animal()
                {
                    Name = animalDto.Name,
                    Age = animalDto.Age,
                    Type = animalDto.Type,
                    Passport = passport,
                    PassportSerialNumber = passport.SerialNumber,
                    Procedures = new List<Procedure>()

                };

                validAnimals.Add(animal);
                sb.AppendLine(String.Format(successAnimalMessage, animal.Name, animal.PassportSerialNumber));
            }

            context.Animals.AddRange(validAnimals);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserializedVets = (VetDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validVets = new List<Vet>();

            foreach (var vetDto in deserializedVets)
            {
                if (!IsValid(vetDto))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                if (validVets.Any(p => p.PhoneNumber.Equals(vetDto.PhoneNumber)))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                var vet = new Vet()
                {
                    Name = vetDto.Name,
                    PhoneNumber = vetDto.PhoneNumber,
                    Age = vetDto.Age,
                    Profession = vetDto.Profession,
                    Procedures = new List<Procedure>()
                };

                validVets.Add(vet);
                sb.AppendLine(String.Format(successVetImport, vetDto.Name));

            }

            context.Vets.AddRange(validVets);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserializedProcedures =
                (ProcedureDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validProcedures = new List<Procedure>();

            var currentVets = context.Vets.ToArray();
            var animalSerialNumbers = context.Animals.ToArray();
            var animalAids = context.AnimalAids.ToArray();

            var procedureAids = new List<ProcedureAnimalAid>();

            foreach (var procedureDto in deserializedProcedures)
            {

                if (!IsValid(procedureDto))
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                var vet = currentVets.Where(p => p.Name == procedureDto.Vet).SingleOrDefault();
                var animal = animalSerialNumbers.Where(p => p.PassportSerialNumber == procedureDto.Animal).SingleOrDefault();
                bool doAllAidsExist = true;
                foreach (var aid in procedureDto.AnimalAids)
                {
                    if (!animalAids.Any(p => p.Name == aid.Name))
                    {
                        doAllAidsExist = false;
                    }
                }

                if (vet == null || animal == null || !doAllAidsExist)
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                DateTime date;
                try
                {
                    date = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                if (procedureDto.AnimalAids.Count() != procedureDto.AnimalAids.Select(p => p.Name).Distinct().Count())
                {
                    sb.AppendLine(failureMessage);
                    continue;
                }

                var currentAids = context.AnimalAids
                    .Where(p => procedureDto.AnimalAids.Select(k => k.Name).Contains(p.Name))
                    .ToArray();

                var cost = currentAids.Select(p => p.Price).Sum();

                var procedure = new Procedure()
                {
                    Vet = vet,
                    Animal = animal,
                    Cost = cost,
                    DateTime = date,
                    ProcedureAnimalAids = new List<ProcedureAnimalAid>()

                };

                //for (int i = 0; i < currentAids.Length; i++)
                //{
                //    var procedureAid = new ProcedureAnimalAid()
                //    {
                //        Procedure = procedure,
                //        AnimalAid = currentAids[i]

                //    };

                //    procedureAids.Add(procedureAid);

                //}

                validProcedures.Add(procedure);
                sb.AppendLine(successProcedureImport);

            }

            context.Procedures.AddRange(validProcedures);
            //context.ProceduresAnimalAids.AddRange(procedureAids);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}































