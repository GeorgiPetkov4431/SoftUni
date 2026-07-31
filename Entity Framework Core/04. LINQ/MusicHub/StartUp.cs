namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            const int producerId = 9;
            const int duration = 4;

            using MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
            //string result = ExportAlbumsInfo(context, producerId);
            string result = ExportSongsAboveDuration(context, duration);

            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext dbContext, int producerId)
        {
            /* AsNoTracking() -> tells EF Core that we do NOT need to track changes and perform identity resolution */
            /* Use .AsNoTracking() for read-only queries to increase performance, because ChangeTracker is not involved */
            /* EF Core can decide to perform some operations at client-side, despite working with IQueryable<T> and the query still not being materialized! */
            /* Formatting operations are a responsibility of the front-end and should NOT be delegated to the DB Server! */
            /* Anonymous types are NOT tracked Entities by ChangeTracker! */
            /* Property of anonymous types that are of Entity Types are TRACKED by ChangeTracker! */
            /* Some complex queries cannot be translated by EF Core Db Provider! */
            /* Can't translate exception -> move materialization of the query (.ToArray()) before failing LINQ */
            /* .AsEnumerable() can be used to delegate the query execution at client-side earlier */
            /* It is recommended to use .Skip() and .Take() to limit the total amount of result data */
            /* .Skip() and .Take() are slow LINQ operations, because .Skip() passes through all skipped rows */
            /* Basic Pagination with Number Navigation -> .Skip()/.Take() */
            /* Advanced Pagination with sequence loading -> Unique .OrderBy() + .Where(paginationFilter) */
            /* Use .AsSplitQuery() when your query is performing many LEFT JOINs with other Entities of the same query level (CROSS-PRODUCT) */
            /* .AsSplitQuery() -> Split "fat" query into several small queries and results of these queries will be joined client-side */
            var albumsInfo = dbContext.Albums
                .AsNoTracking()
                .AsSplitQuery()
                .Include(a => a.Songs)
                .Where(a => a.ProducerId == producerId)
                .Select(a => new
                {
                    a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy"),
                    ProducerName = a.Producer != null ? a.Producer.Name : null,
                    /* Because we're doing an inner select, .Include() needs to be used */
                    Songs = a.Songs
                        .OrderByDescending(s => s.Name)
                        .ThenBy(s => s.Writer.Name)
                        .Select(s => new
                        {
                            s.Name,
                            s.Price,
                            WriterName = s.Writer != null ? s.Writer.Name : null
                        })
                        .ToArray(),
                    TotalPrice = a.Price
                })
                .AsEnumerable()
                .OrderByDescending(a => a.TotalPrice)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var album in albumsInfo)
            {
                sb.AppendLine($"-AlbumName: {album.Name}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs:");
                int index = 1;
                foreach (var song in album.Songs)
                {
                    sb.AppendLine($"---#{index++}");
                    sb.AppendLine($"---SongName: {song.Name}");
                    sb.AppendLine($"---Price: {song.Price:F2}");
                    sb.AppendLine($"---Writer: {song.WriterName}");
                }
                sb.AppendLine($"-AlbumPrice: {album.TotalPrice:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext dbContext, int duration)
        {
            /* Ducation.TotalSeconds can't be translated to SQL */
            /* Workarouns 1: Move filtering (selection) to client-side */
            /* Workarouns 2: Use .FromSql() to start with raw SQL query containing the .Where() clause */
            var songs = dbContext.Songs
            //.FromSqlInterpolated($"SELECT * FROM [Songs] WHERE DATEPART(SECOND, [Duration]) > {duration}")
            .AsNoTracking()
            .Select(s => new
            {
                s.Name,
                Performers = s.SongPerformers
                    .Select(sp => sp.Performer)
                    .Select(p => p.FirstName + " " + p.LastName)
                    .OrderBy(p => p)
                    .ToArray(),
                WriterName = s.Writer.Name,
                AlbumProducerName = (s.Album != null && s.Album.Producer != null)
                    ? s.Album.Producer.Name : null,
                s.Duration
            })
            .OrderBy(s => s.Name)
            .ThenBy(s => s.WriterName)
            .AsEnumerable()
            .Where(s => s.Duration.TotalSeconds > duration)
            .ToArray();

            StringBuilder sb = new StringBuilder();

            int index = 1;
            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{index++}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.WriterName}");
                foreach (string performer in song.Performers)
                {
                    sb.AppendLine($"---Performer: {performer}");
                }
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducerName}");
                sb.AppendLine($"---Duration: {song.Duration.ToString("c")}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
