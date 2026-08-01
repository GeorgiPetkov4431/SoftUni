using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Supplier")]
    public class ImportSupplierDto
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; } = null!;

        // Two handling strategies for property types:
        // 1. Use the same type as in the model class (bool) - May cause deserialization exceptions and issues and abort the entire deserialization due to incorrect values
        // 2. Use string type - Allows us to skip only DTOs with incorrect values and continue deserialization by using TryParse() method
        [Required]
        [XmlElement("isImporter")]
        public string IsImporter { get; set; } = null!;
    }
}
