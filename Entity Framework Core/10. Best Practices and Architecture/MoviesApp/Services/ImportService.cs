using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MoviesApp.Data;
using MoviesApp.DTOs.Json;
using MoviesApp.DTOs.Xml;
using MoviesApp.Models;
using MoviesApp.Services.Interfaces;
using MoviesApp.Utilities;
using MoviesApp.Utilities.Interfaces;
using Newtonsoft.Json;
using System.Globalization;
using static MoviesApp.Utilities.ObjectValidator;

namespace MoviesApp.Services
{
    public class ImportService : IImportService
    {
        private readonly IFileHelper _fileHelper;
        private readonly IConfiguration _appConfiguration;
        private readonly ILogger<ImportService> _logger;
        private readonly MoviesAppDbContext _dbContext;

        public ImportService(IFileHelper fileHelper, IConfiguration appConfiguration,
            ILogger<ImportService> logger, MoviesAppDbContext dbContext)
        {
            this._appConfiguration = appConfiguration;
            this._fileHelper = fileHelper;
            this._logger = logger;
            this._dbContext = dbContext;
        }

        public async Task<int> ImportFromJsonAsync(string fileName)
        {
            string? datasetsPath = this._appConfiguration
                .GetValue<string>("Seeding:DatasetPath");

            string jsonFileContent = await this._fileHelper
                .ReadFileAsync(datasetsPath, fileName);

            IEnumerable<ImportJsonMovieDto>? moviesDtos = JsonConvert
                .DeserializeObject<ImportJsonMovieDto[]>(jsonFileContent);

            if (moviesDtos == null)
            {
                this._logger.LogInformation("No movies were found in the JSON file! Import from JSON will be skipped!");
                return 0;
            }

            bool moviesAlreadyExist = this._dbContext.Movies.Any();
            if (moviesAlreadyExist)
            {
                this._logger.LogInformation("Movies already exist in the database! Import from JSON will be skipped!");

                return 0;
            }

            ICollection<Movie> moviesToImport = new List<Movie>();

            foreach (ImportJsonMovieDto movieDto in moviesDtos)
            {
                if (!IsValid(movieDto, out List<string> validationErrors))
                {
                    this._logger.LogInformation("Movie was not imported during import movies from JSON! Please see error messages below");
                    foreach (string validationError in validationErrors)
                    {
                        this._logger.LogError(validationError);
                    }

                    continue;
                }

                bool isReleaseDateValidFormat = DateTime.TryParseExact(movieDto.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

                if (!isReleaseDateValidFormat)
                {
                    this._logger.LogError($"Movie DTO {movieDto.Id} Release Date is in incorrect format!");
                    continue;
                }

                Movie newMovie = new Movie
                {
                    Id = movieDto.Id,
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    ReleaseDate = releaseDate,
                    Director = movieDto.Director,
                    Duration = movieDto.Duration,
                    Description = movieDto.Description,
                    ImageUrl = movieDto.ImageUrl
                };

                moviesToImport.Add(newMovie);
            }

            await SeedMoviesAsync(moviesToImport);

            return moviesToImport.Count;
        }

        public async Task<int> ImportFromXmlAsync(string fileName)
        {
            string? datasetsPath = this._appConfiguration
                .GetValue<string>("Seeding:DatasetPath");

            string xmlFileContent = await this._fileHelper
                .ReadFileAsync(datasetsPath, fileName);

            IEnumerable<XmlGenreGroupMovieDto>? moviesDtos = XmlSerializerWrapper
                .Deserialize<ImportXmlGenreGroupDto[]>(xmlFileContent, "MoviesLibrary")?
                .Select(gg => gg.Movie)
                .ToArray();

            if (moviesDtos == null)
            {
                this._logger.LogInformation("No movies were found in the XML file! Import from XML will be skipped!");
                return 0;
            }

            ICollection<Movie> moviesToImport = new List<Movie>();

            foreach (XmlGenreGroupMovieDto movieDto in moviesDtos)
            {
                if (!IsValid(movieDto, out List<string> validationErrors))
                {
                    this._logger.LogInformation("Movie was not imported during import movies from XML! Please see error messages below");
                    foreach (string validationError in validationErrors)
                    {
                        this._logger.LogError(validationError);
                    }

                    continue;
                }

                if (!IsValid(movieDto.Details, out List<string> validationErrorsDetails))
                {
                    this._logger.LogInformation("Movie was not imported during import movies from XML! Please see error messages below");
                    foreach (string validationError in validationErrorsDetails)
                    {
                        this._logger.LogError(validationError);
                    }

                    continue;
                }

                bool isReleaseDateValidFormat = DateTime.TryParseExact(movieDto.Details.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

                if (!isReleaseDateValidFormat)
                {
                    this._logger.LogError($"Movie DTO {movieDto.Id} Release Date is in incorrect format!");
                    continue;
                }

                Movie newMovie = new Movie
                {
                    Id = movieDto.Id,
                    Title = movieDto.Title,
                    Genre = movieDto.Details.Genre,
                    ReleaseDate = releaseDate,
                    Director = movieDto.Details.Director,
                    Duration = movieDto.Duration,
                    Description = movieDto.Description,
                    ImageUrl = movieDto.ImageUrl
                };

                moviesToImport.Add(newMovie);
            }

            await SeedMoviesAsync(moviesToImport);

            return moviesToImport.Count;
        }

        private async Task SeedMoviesAsync(IEnumerable<Movie> moviesToImport)
        {
            IDbContextTransaction transaction = await this._dbContext.Database
                .BeginTransactionAsync();

            try
            {
                await this._dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Movies ON");

                this._dbContext.Movies.AddRange(moviesToImport);
                await this._dbContext.SaveChangesAsync();

                await this._dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Movies OFF");

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);

                await transaction.RollbackAsync();
            }
        }
    }
}
