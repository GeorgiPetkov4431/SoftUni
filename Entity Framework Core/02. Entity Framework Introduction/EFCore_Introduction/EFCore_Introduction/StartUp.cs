using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Net.Mime;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext dbContext = new SoftUniContext();

            //Ex. 3
            //string result = GetEmployeesFullInformation(dbContext);

            //Ex. 4
            //string result = GetEmployeesWithSalaryOver50000(dbContext);

            //Ex. 5
            //string result = GetEmployeesFromResearchAndDevelopment(dbContext);

            //Ex. 6
            //string result = AddNewAddressToEmployee(dbContext);

            //Ex. 7
            //string result = GetEmployeesInPeriod(dbContext);

            //Ex. 8
            //string result = GetAddressesByTown(dbContext);

            //Ex.9
            //string result = GetEmployee147(dbContext);

            //Ex.10
            //string result = GetDepartmentsWithMoreThan5Employees(dbContext);

            //Ex. 11
            //string result = GetLatestProjects(dbContext);

            //Ex.12
            //string result = IncreaseSalaries(dbContext);

            //Ex.13
            //string result = GetEmployeesByFirstNameStartingWithSa(dbContext);

            //Ex. 14
            //string result = DeleteProjectById(dbContext);

            //Ex. 15
            string result = RemoveTown(dbContext);

            Console.WriteLine(result);
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            /* .ToArray()/.ToList() -> used to perform materialization of the query. */
            /* Materiaization -> Generates SQL Query based on LINQ Expression Tree
             * and SQL is transmitted to SQL Server and response is awaited. */
            /* Materialization is usually a slow operation. Always use .Take() to limit row set count */
            /* .Select() is used to perform projection (select only desired columns + computed columns) */
            /* LINQ projection enables Select() to be used with both class and anonymous types (most cases) */
            /* Formatting decimal numbers is preferable to be performed in-memory */
            /* Ordering should be performed in the database, when possible */
            var employees = context.Employees
                //.AsNoTracking()
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            /* Keep in mind that employees are tracked after fetching from the DB! Any changes
             * will be persisted on .SaveChanges() invocation! */
            /* Explicitly state not to be tracked -> .AsNoTracking() */
            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} - {e.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            /* SQL Sequence:
             * 1. Create new entry for new Address in the Address table
             * 2. Set AddressId of Employee to AddressId of the new Address */

            Address newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            /* Add new Address to the Addresses DbSet -> ChangeTracker marks it as new Entity! */
            /* Still not persisted in the DB -> only in-memory record */
            context.Addresses.Add(newAddress);

            /* Employee entity is tracked by the ChangeTracker! */
            Employee nakovEmployee = context.Employees
                .First(e => e.LastName == "Nakov");

            /* There is no need to use the FK to change the Employee's Address */
            nakovEmployee.Address = newAddress;

            /* All changes are maintained by ChangeTracker in-memory -> Persist the changes */
            context.SaveChanges();

            /* Take(n) limits number of returned rows in the result set -> TOP(N)/FETCH-OFFSET */
            IEnumerable<string?> employeesAddress = context.Employees
                .Where(e => e.AddressId != null)
                .OrderByDescending(e => e.AddressId)
                .Select(e => new
                {
                    AddressText = e.Address != null ?
                        e.Address.AddressText : string.Empty,
                })
                .Select(e => e.AddressText)
                .Take(10)
                .ToArray();

            return string.Join(Environment.NewLine, employeesAddress);
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            /* Keep in mind that nested queries (projections) also need to be materialized! */
            var top10EmployeesWithProjects = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager != null ? e.Manager.FirstName : null,
                    ManagerLastName = e.Manager != null ? e.Manager.LastName : null,
                    Projects = e.EmployeesProjects
                        .Select(ep => ep.Project)
                        .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
                        .Select(p => new
                        {
                            p.Name,
                            p.StartDate,
                            p.EndDate
                        })
                        .ToArray()
                })
                .Take(10)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var e in top10EmployeesWithProjects)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");
                foreach (var project in e.Projects)
                {
                    string startDateFormatted = project.StartDate.ToString("M/d/yyyy h:mm:ss tt");
                    string endDateFormatted = project.EndDate.HasValue
                        ? project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt")
                        : "not finished";
                    sb.AppendLine($"--{project.Name} - {startDateFormatted} - {endDateFormatted}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            string[] addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town != null ? a.Town.Name : null,
                    EmployeesCount = a.Employees.Count
                })
                .OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a => $"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees")
                .ToArray();

            return string.Join(Environment.NewLine, addresses);
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee147 = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                        .Select(ep => ep.Project.Name)
                        .OrderBy(p => p)
                        .ToArray()
                })
                .FirstOrDefault();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");
            sb.Append(string.Join(Environment.NewLine, employee147.Projects));

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    d.Name,
                    ManagerFirstName = d.Manager != null ? d.Manager.FirstName : null,
                    ManagerLastName = d.Manager != null ? d.Manager.LastName : null,
                    Employees = d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle}")
                        .ToArray()
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");
                sb.AppendLine(string.Join(Environment.NewLine, d.Employees));
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt")
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
                sb.AppendLine(p.Description);
                sb.AppendLine(p.StartDate);
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            string[] departments = { "Engineering", "Tool Design", "Marketing", "Information Services" };
            decimal salaryModifier = 1.12m;

            Employee[] employees = context.Employees
                .Where(e => departments.Contains(e.Department.Name))
                .ToArray();

            foreach (Employee e in employees)
            {
                e.Salary *= salaryModifier;
            }

            context.SaveChanges();

            string[] employeesInfo = employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} (${e.Salary:F2})")
                .ToArray();

            return string.Join(Environment.NewLine, employeesInfo);
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            string[] employeesWithFirstNameStartingWithSa = context.Employees
                .Where(e => e.FirstName.Substring(0, 2).ToLower() == "sa")
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})")
                .ToArray();

            return string.Join(Environment.NewLine, employeesWithFirstNameStartingWithSa);
        }


        public static string DeleteProjectById(SoftUniContext context)
        {
            /* EF Core supports Cascading, Nullify and Restrict delete behaviors
             * Cascading -> all related entities will be deleted
             * Nullify -> all related entities will be updated with null value for the foreign key
             * Restrict -> all related entities will be left unchanged, but the delete operation will be rejected by the database if there are related entities */

            Project projectId2ToDelete = context.Projects.Find(2);

            

            if (projectId2ToDelete != null)
            {
                /* Not materialized yet -> ChangeTracker is able to work with non-materialized queries */
                /* Optimization: no in-memory store of these entities, delaying the SQL query */
                IQueryable<EmployeeProject> employeeProjectsToDelete = context.EmployeesProjects
                    .Where(ep => ep.ProjectId == 2);

                /* Remove all related entites first */
                context.EmployeesProjects.RemoveRange(employeeProjectsToDelete);

                /* Remove entity from DbSet -> marks the Entity as removed in ChangeTracker in-memory */
                /* Still no physical delete is performed in the DB */
                context.Projects.Remove(projectId2ToDelete);

                /* Persist changes -> physical delete is performed in the DB! */
                context.SaveChanges();
            }

            IEnumerable<string> top10ProjectNames = context.Projects
                .Select(p => p.Name)
                .Take(10)
                .ToArray();

            return string.Join(Environment.NewLine, top10ProjectNames);
        }

        public static string RemoveTown(SoftUniContext context)
        {
            const string townName = "Seattle";

            Town townToDelete = context.Towns
                .Where(t => t.Name == townName)
                .FirstOrDefault();

            Address[] addressesToDelete = context.Addresses
                .Where(a => a.TownId == townToDelete.TownId)
                .ToArray();

            IEnumerable<Employee> employeesToRemoveAddressFrom = context.Employees
                .Where(e => addressesToDelete.Contains(e.Address))
                .ToArray();

            foreach (Employee e in employeesToRemoveAddressFrom)
            {
                e.AddressId = null;
            }

            context.Addresses.RemoveRange(addressesToDelete);
            context.Towns.Remove(townToDelete);
            context.SaveChanges();

            return $"{addressesToDelete.Count()} addresses in {townName} were deleted";
        }
    }
}
