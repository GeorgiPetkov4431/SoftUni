using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using CarDealer.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            using CarDealerContext dbContext = new CarDealerContext();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            string xmlFileName = "suppliers.xml";
            string xmlFilePath = GetXmlFilePath(xmlFileName);

            string xmlFileContent = File.ReadAllText(xmlFilePath);

            string result = ImportSuppliers(dbContext, xmlFileContent);

            Console.WriteLine(result);
        }

        public static string ImportSuppliers(CarDealerContext dbContext, string inputXml)
        {
            IEnumerable<ImportSupplierDto>? supplierDtos = XmlSerializerWrapper
                .Deserialize<ImportSupplierDto[]>(inputXml, "Suppliers");

            if (supplierDtos == null)
            {
                supplierDtos = Array.Empty<ImportSupplierDto>();
            }

            ICollection<Supplier> suppliesToPersist = new List<Supplier>();
            foreach (ImportSupplierDto supplierDto in supplierDtos)
            {
                if (!IsValid(supplierDto)) continue;

                bool isImporterValid = bool.TryParse(supplierDto.IsImporter, out bool isImporterVal);

                // Skip the current record, as it contains invalid data for IsImporter property that can't be parsed to bool.
                if (!isImporterValid) continue;

                Supplier newSupplier = new Supplier
                {
                    Name = supplierDtos.First().Name,
                    IsImporter = bool.Parse(supplierDtos.First().IsImporter)
                };

                suppliesToPersist.Add(newSupplier);
            }

            dbContext.Suppliers.AddRange(suppliesToPersist);
            dbContext.SaveChanges();

            return $"Successfully imported {suppliesToPersist.Count}";
        }

        private static string GetXmlFilePath(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlDirectory = Path.Combine(currentDirectory, @"..\..\..\Datasets\", fileName);

            return Path.GetFullPath(xmlDirectory);
        }

        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator
                .TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}