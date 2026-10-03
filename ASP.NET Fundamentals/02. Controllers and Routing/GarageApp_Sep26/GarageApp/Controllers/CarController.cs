using GarageApp.Data;
using GarageApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GarageApp.Common.ApplicationConstants;

namespace GarageApp.Controllers
{
    public class CarController : Controller
    {
        private readonly GarageAppDbContext _dbContext;

        public CarController(GarageAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index(string? make)
        {
            IQueryable<Car> allCarsQuery = _dbContext.Cars
                .AsNoTracking()
                .Include(c => c.Garage)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ThenByDescending(c => c.Year)
                .ThenByDescending(c => c.ProductionMonth)
                .Take(MaxEntitiesPerPage);

            if (!string.IsNullOrEmpty(make))
            {
                allCarsQuery = allCarsQuery
                    //.Where(c => c.Make.ToLower().Contains(make.ToLower()))
                    .Where(c => EF.Functions.Like(c.Make, $"%{make.ToLower()}%"));
            }

            IEnumerable<Car> allCars = allCarsQuery
                .ToArray();

            return View(allCars);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return this.BadRequest("There was a problem with your request! Try again...");
            }

            Car? carDetails = _dbContext.Cars
                .AsNoTracking()
                .Include(c => c.Garage)
                .FirstOrDefault(c => c.Id == id);

            return View(carDetails);
        }

        //[HttpGet]
        //public IActionResult Search(string? make)
        //{
        //    return BadRequest();
        //}
    }
}
