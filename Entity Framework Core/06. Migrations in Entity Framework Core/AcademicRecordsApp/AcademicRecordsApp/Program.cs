using AcademicRecordsApp.Data;

namespace AcademicRecordsApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            AcademicRecordsDbContext context = new AcademicRecordsDbContext();

            Console.WriteLine("Database connected...");
        }
    }
}
