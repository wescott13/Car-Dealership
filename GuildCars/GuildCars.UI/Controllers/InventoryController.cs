using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.Factory;
using GuildCars.Models.Queries;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Used()
        {
            return View();
        }

        public ActionResult Details(int vehicleId)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var model = repo.GetDetails(vehicleId);

            return View(model);
        }
    }
}