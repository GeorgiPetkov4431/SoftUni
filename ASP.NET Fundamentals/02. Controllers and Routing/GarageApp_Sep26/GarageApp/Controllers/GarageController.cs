using GarageApp.Data;
using GarageApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GarageApp.Common.ApplicationConstants;

namespace GarageApp.Controllers
{
    public class GarageController : Controller
    {
        private readonly GarageAppDbContext _dbContext;

        public GarageController(GarageAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            /* URL Path is: /Garage/Index */
            /* View Path is: /Garage/Index.cshtml */
            IEnumerable<Garage> allGarages = _dbContext.Garages
                .AsNoTracking()
                .Include(g => g.Cars)
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Location)
                .Take(MaxEntitiesPerPage)
                .ToArray();

            return View(allGarages);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest("There was a problem with your request! Try again...");
            }

            Garage? garageDetails = _dbContext.Garages
                .AsNoTracking()
                .Include(g => g.Cars)
                .SingleOrDefault(g => g.Id == id);

            if (garageDetails == null)
            {
                return this.NotFound("The garage was not found! Try again...");
            }

            return View(garageDetails);
        }
    }
}
