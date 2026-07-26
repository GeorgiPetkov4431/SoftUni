using P02_FootballBetting.Data;

namespace P02_FootballBetting
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                using FootballBettingContext dbContext = new FootballBettingContext();

                /* Tip: Use Migrations API instead! */
                /* Dev only solution */
                /* First drop any existing database to ensure sync with latest changes */
                dbContext.Database.EnsureDeleted();

                /* Try to create the database from the latest Code First model */
                dbContext.Database.EnsureCreated();

                Console.WriteLine("Database created from Code-First model!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
