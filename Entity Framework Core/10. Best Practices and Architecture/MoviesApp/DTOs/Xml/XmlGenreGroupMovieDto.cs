using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Serialization;
using static MoviesApp.Common.EntityValidation;

namespace MoviesApp.DTOs.Xml
{
    [XmlType("Movie")]
    public class XmlGenreGroupMovieDto
    {
        [Range(MovieIdMinValue, MovieIdMaxValue)]
        [XmlAttribute("id")]
        public int Id { get; set; }

        [Range(MovieDurationMinLength, MovieDurationMaxLength)]
        [XmlAttribute("duration")]
        public int Duration { get; set; }

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        [XmlElement("Title")]
        public string Title { get; set; } = null!;

        [XmlElement("Details")]
        public XmlMovieDetailsDto Details { get; set; } = null!;

        [Required]
        [MinLength(MovieDescriptionMinLength)]
        [MaxLength(MovieDescriptionMaxLength)]
        [XmlElement("Description")]
        public string Description { get; set; } = null!;

        /* Combination between static DTO and LINQ to XML approach */
        [XmlAnyElement("Media")]
        public XmlElement MediaElement { get; set; } = null!;

        [MaxLength(MovieImageUrlMaxLength)]
        public string? ImageUrl
            => MediaElement.InnerText;
    }
}
