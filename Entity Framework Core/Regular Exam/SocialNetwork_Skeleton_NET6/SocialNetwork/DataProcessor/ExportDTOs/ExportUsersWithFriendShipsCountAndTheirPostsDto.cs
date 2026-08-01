using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SocialNetwork.DataProcessor.ExportDTOs
{
    [XmlType("User")]
    public class ExportUsersWithFriendShipsCountAndTheirPostsDto
    {
        [XmlElement("Username")]
        public string Username { get; set; } = null!;

        [XmlArray("Posts")]
        public ExportUserPostsDto[] Posts { get; set; }
            = Array.Empty<ExportUserPostsDto>();

        [XmlAttribute]
        public int Friendships { get; set; }
    }
}
