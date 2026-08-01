using CarDealer.DTOs.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.Utilities
{
    public static class XmlSerializerWrapper
    {
        public static T? Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);

            // XmlSerializer class is used for both deserialization and serialization!
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);
            using StringReader stringReader = new StringReader(inputXml);

            T? result = (T?)xmlSerializer.Deserialize(stringReader);

            return result;
        }
    }
}
