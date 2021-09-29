using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.Factory;
using GuildCars.Data.Interfaces;
using GuildCars.Models.UIViews;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ISpecialsRepository specialsRepository = SpecialRepositoryFactory.GetRepository();
            IVehicleRepository featuredVehicleRepository = VehicleRepositoryFactory.GetRepository();

            
            {
                var HomeIndex = new HomeIndex();
                HomeIndex.special = specialsRepository.GetAll().ToList();
                HomeIndex.featuredVehicles = (List<FeaturedVehicleShortItem>)featuredVehicleRepository.GetFeatured();

                return View(HomeIndex);
            };
            
        }

        public ActionResult Specials()
        {
            ISpecialsRepository specialsRepository = SpecialRepositoryFactory.GetRepository();

            {
                var HomeIndex = new HomeIndex();
                HomeIndex.special = specialsRepository.GetAll().ToList();

                return View(HomeIndex);
            };
        }

        public ActionResult Contact(string VIN)
        {
            ViewBag.VIN = VIN;

            ContactUs contactUs = new ContactUs();
            
            if (VIN != null)
                contactUs.ContactUsMessage = VIN;
            
            return View(contactUs);
        }

        [HttpPost]
        public ActionResult Contact(ContactUs contactUs)
        {
            if (contactUs.Email == null && contactUs.PhoneNumber == null)
            {
                ModelState.AddModelError("Error", "We need an email or phone number to contact you");
            }

            if (contactUs.ContactUsMessage == null)
            {
                ModelState.AddModelError("Error", "What should we contact you about?");
            }

            else if (ModelState.IsValid)
            {
                IContactUsRepository repo = ContactUsRepositoryFactory.GetRepository();
                repo.Insert(contactUs);

                return RedirectToAction("Index", "Home");
            }
            return View(contactUs);
        }
    }
}