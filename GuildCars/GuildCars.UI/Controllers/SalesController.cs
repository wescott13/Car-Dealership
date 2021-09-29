using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.Factory;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.Models.UIViews;
using Microsoft.AspNet.Identity;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Sales,Admin")]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purchase(int vehicleId)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            SalesPurchase salesPurchase = new SalesPurchase();
            VehicleDetails saleVehicle = repo.GetDetails(vehicleId);
            salesPurchase.vehicleDetails = saleVehicle;
            salesPurchase.States = new SelectList(repo.GetAllStates(), "StateId", "StateId");
            salesPurchase.PurchaseTypes = new SelectList(repo.GetAllPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName");

            return View(salesPurchase);
        }

        [HttpPost]
        public ActionResult Purchase(SalesPurchase salesPurchase)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            if (salesPurchase.purchaseDetails.PurchaseName == null)
            {
                ModelState.AddModelError("Error", "Name Required.");
            }
            
            if (salesPurchase.purchaseDetails.Email == null && salesPurchase.purchaseDetails.PhoneNumber == null)
            {
                ModelState.AddModelError("Error", "Email or phone number Required");
            }

            if (salesPurchase.purchaseDetails.Street1 == null)
            {
                ModelState.AddModelError("Error", "Street Required.");
            }

            if (salesPurchase.purchaseDetails.City == null)
            {
                ModelState.AddModelError("Error", "City Required.");
            }

            if (salesPurchase.purchaseDetails.ZipCode == null)
            {
                ModelState.AddModelError("Error", "ZipCode required");
            }

            if (salesPurchase.purchaseDetails.PurchasePrice < (salesPurchase.vehicleDetails.SalePrice * .95M) ||
                salesPurchase.purchaseDetails.PurchasePrice > salesPurchase.vehicleDetails.MSRP)
            {
                ModelState.AddModelError("Error", "Purchase Price cannot be less than 95% of the sales price and cannot exceed the MSRP.");
            }

            if (ModelState.IsValid)
            {
                salesPurchase.purchaseDetails.UserId = User.Identity.GetUserId();

                PurchaseVehicle sold = new PurchaseVehicle();
                sold.UserId = salesPurchase.purchaseDetails.UserId;
                sold.PurchaseName = salesPurchase.purchaseDetails.PurchaseName;
                sold.PhoneNumber = salesPurchase.purchaseDetails.PhoneNumber;
                sold.Email = salesPurchase.purchaseDetails.Email;
                sold.Street1 = salesPurchase.purchaseDetails.Street1;
                sold.Street2 = salesPurchase.purchaseDetails.Street2;
                sold.City = salesPurchase.purchaseDetails.City;
                sold.StateId = salesPurchase.purchaseDetails.StateId;
                sold.ZipCode = salesPurchase.purchaseDetails.ZipCode;
                sold.PurchasePrice = salesPurchase.purchaseDetails.PurchasePrice;
                sold.PurchaseTypeId = salesPurchase.purchaseDetails.PurchaseTypeId;
                
                repo.AddPurchaseVehicle(sold);

                repo.DeleteVehicle(salesPurchase.vehicleDetails.VehicleId, salesPurchase.vehicleDetails.ImageFileName);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                salesPurchase.vehicleDetails = repo.GetDetails(salesPurchase.vehicleDetails.VehicleId);
                salesPurchase.States = new SelectList(repo.GetAllStates(), "StateId", "StateId");
                salesPurchase.PurchaseTypes = new SelectList(repo.GetAllPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName");

                return View(salesPurchase);
            }
            
        }
    }
}