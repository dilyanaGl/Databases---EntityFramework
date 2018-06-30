using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.DataProcessor.Exports;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var owners = context.Animals.Where(p => p.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(p => new
                {
                    OwnerName = p.Passport.OwnerName,
                    AnimalName = p.Name,
                    Age = p.Age,
                    SerialNumber = p.Passport.SerialNumber,
                    RegisteredOn = p.Passport.RegistrationDate.ToString("dd-MM-yyyy")
                }
                )
                .OrderBy(p => p.Age)
                .ThenBy(p => p.SerialNumber)
                .ToArray();

            var json = JsonConvert.SerializeObject(owners, Newtonsoft.Json.Formatting.Indented);

            return json;

        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var sb = new StringBuilder();

            var procedures = context.Procedures
                .Select(p => new ProcedureDto
                {
                    Passport = p.Animal.Passport.SerialNumber,
                    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                    DateTime = p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    DateTimeComparison = p.DateTime,
                    AnimalAids = p.ProcedureAnimalAids
                        .Select(k => new AnimalAidDto
                        {
                            Name = k.AnimalAid.Name,
                            Price = k.AnimalAid.Price
                        })
                        .ToArray(),
                    TotalPrice = p.Cost
                })
                .OrderBy(p => p.DateTimeComparison)
                .ThenBy(p => p.Passport)
                .ToArray();

            var serializer = new XmlSerializer(typeof(Exports.ProcedureDto[]), new XmlRootAttribute("Procedures"));

            serializer.Serialize(new StringWriter(sb), procedures,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            //var procedures = context.Procedures
            //    .Select(p => new
            //    {
            //        Passport = p.Animal.Passport.SerialNumber,
            //        OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
            //        DateTime = p.DateTime,
            //        AnimalAids = p.ProcedureAnimalAids.Select(map => new
            //        {
            //            Name = map.AnimalAid.Name,
            //            Price = map.AnimalAid.Price
            //        }),
            //        TotalPrice = p.ProcedureAnimalAids.Select(paa => paa.AnimalAid.Price).Sum(),
            //    })
            //    .OrderBy(p => p.DateTime)
            //    .ThenBy(p => p.Passport)
            //    .ToArray();

            //var xDoc = new XDocument(new XElement("Procedures"));

            //foreach (var p in procedures)
            //{
            //    xDoc.Root.Add
            //    (
            //        new XElement("Procedure",
            //            new XElement("Passport", p.Passport),
            //            new XElement("OwnerNumber", p.OwnerNumber),
            //            new XElement("DateTime", p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)),
            //            new XElement("AnimalAids", p.AnimalAids.Select(aa =>
            //                new XElement("AnimalAid",
            //                    new XElement("Name", aa.Name),
            //                    new XElement("Price", aa.Price)))),
            //            new XElement("TotalPrice", p.TotalPrice))
            //    );
            //}

            //string result = xDoc.ToString();

            return sb.ToString();

        }
    }
}
