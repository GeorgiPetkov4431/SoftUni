using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                using StudentSystemContext context = new StudentSystemContext();

                context.Database.EnsureDeleted();

                context.Database.EnsureCreated();

                Console.WriteLine("Database successfully created from Code-First model!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
