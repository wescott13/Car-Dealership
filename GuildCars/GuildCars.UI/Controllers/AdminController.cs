using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GuildCars.Data.Factory;
using GuildCars.Models.Tables;
using GuildCars.Models.UIViews;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReportsIndex()
        {
            return View();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Vehicles()
        {
            return View();
        }

        public ActionResult AddVehicle()
        {
            var model = new AddEditVehicle();
            var repo = VehicleRepositoryFactory.GetRepository();

            model.VehicleMakes = new SelectList(repo.GetVehicleMakes(), "MakeId", "MakeName");
            model.VehicleModels = new SelectList(repo.GetVehicleModels(), "ModelId", "ModelTypeName");
            model.VehicleType = new SelectList(repo.GetVehicleType(), "TypeId", "TypeName");
            model.BodyStyles = new SelectList(repo.GetBodyStyle(), "BodyStyleId", "BodyStyleName");
            model.Transmission = new SelectList(repo.GetTransmission(), "TransmissionId", "TransmissionTypeName");
            model.ExteriorColor = new SelectList(repo.GetExteriorColor(), "ExteriorColorId", "ExteriorColorName");
            model.InteriorColor = new SelectList(repo.GetInteriorColor(), "InteriorColorId", "InteriorColorName");

            return View(model);
        }
        [HttpPost]
        public ActionResult AddVehicle(AddEditVehicle addVehicle)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            if (addVehicle.Vehicle.VehicleYear < 2000 || addVehicle.Vehicle.VehicleYear > DateTime.Now.Year + 1)
            {
                ModelState.AddModelError("Error", "Year must be a 4 digit year between 2000 and the current year + 1");
            }
            if (addVehicle.Vehicle.TypeId == 1 && (addVehicle.Vehicle.Mileage < 0 || addVehicle.Vehicle.Mileage > 1000))
            {
                ModelState.AddModelError("Error", "New cars must have mileage between 0 and 1000");
            }
            if (addVehicle.Vehicle.TypeId == 2 && addVehicle.Vehicle.Mileage < 1000)
            {
                ModelState.AddModelError("Error", "Used cars must have mileage 1000+");
            }
            if (addVehicle.Vehicle.VIN == null)
            {
                ModelState.AddModelError("Error", "VIN required");
            }
            if (addVehicle.Vehicle.SalePrice < 0)
            {
                ModelState.AddModelError("Error", "Sale Price must be positive");
            }
            if (addVehicle.Vehicle.MSRP < 0)
            {
                ModelState.AddModelError("Error", "MSRP must be positive");
            }
            if (addVehicle.Vehicle.SalePrice > addVehicle.Vehicle.MSRP)
            {
                ModelState.AddModelError("Error", "Sale Price must not be greater than MSRP");
            }
            if (addVehicle.Vehicle.VehicleDescription == null)
            {
                ModelState.AddModelError("Error", "A description is required.");
            }

            if (addVehicle.ImageUpload != null && addVehicle.ImageUpload.ContentLength > 0)
            {
                string fileNameWithExtension = Path.GetFileName(addVehicle.ImageUpload.FileName);
                string FileExtension = fileNameWithExtension.Substring(fileNameWithExtension.LastIndexOf('.') + 1).ToLower();

                var savepath = Server.MapPath("~/Images");

                string fileName = Path.GetFileNameWithoutExtension(addVehicle.ImageUpload.FileName);
                string extension = Path.GetExtension(addVehicle.ImageUpload.FileName);

                var filePath = Path.Combine(savepath, fileName + extension);

                if (FileExtension == "jpeg" || FileExtension == "png" || FileExtension == "jpg")
                {
                    int counter = 1;
                    while (System.IO.File.Exists(filePath))
                    {
                        filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                        counter++;
                    }

                    addVehicle.ImageUpload.SaveAs(filePath);
                    addVehicle.Vehicle.ImageFileName = Path.GetFileName(filePath);
                }

                else
                {
                    ModelState.AddModelError("Error", "Need image file of png, jpg, or jpeg");
                }
            }

            if (addVehicle.Vehicle.ImageFileName == null)
            {
                ModelState.AddModelError("Error", "An Image is required.");
            }

            if (ModelState.IsValid)
            {
                Vehicle newVehicle = new Vehicle();
                newVehicle.MakeId = addVehicle.Vehicle.MakeId;
                newVehicle.ModelId = addVehicle.Vehicle.ModelId;
                newVehicle.TypeId = addVehicle.Vehicle.TypeId;
                newVehicle.BodyStyleId = addVehicle.Vehicle.BodyStyleId;
                newVehicle.TransmissionId = addVehicle.Vehicle.TransmissionId;
                newVehicle.ExteriorColorId = addVehicle.Vehicle.ExteriorColorId;
                newVehicle.InteriorColorId = addVehicle.Vehicle.InteriorColorId;
                newVehicle.Mileage = addVehicle.Vehicle.Mileage;
                newVehicle.VIN = addVehicle.Vehicle.VIN;
                newVehicle.MSRP = addVehicle.Vehicle.MSRP;
                newVehicle.SalePrice = addVehicle.Vehicle.SalePrice;
                newVehicle.VehicleDescription = addVehicle.Vehicle.VehicleDescription;
                newVehicle.VehicleYear = addVehicle.Vehicle.VehicleYear;
                newVehicle.HasFeatured = false;
                newVehicle.ImageFileName = addVehicle.Vehicle.ImageFileName;

                if (addVehicle.ImageUpload != null && addVehicle.ImageUpload.ContentLength > 0)
                {
                    var savepath = Server.MapPath("~/Images");

                    string fileName = Path.GetFileNameWithoutExtension(addVehicle.ImageUpload.FileName);
                    string extension = Path.GetExtension(addVehicle.ImageUpload.FileName);

                    var filePath = Path.Combine(savepath, fileName + extension);

                    int counter = 1;
                    while (System.IO.File.Exists(filePath))
                    {
                        filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                        counter++;
                    }

                    addVehicle.ImageUpload.SaveAs(filePath);
                    addVehicle.Vehicle.ImageFileName = Path.GetFileName(filePath);
                }


                newVehicle.ImageFileName = addVehicle.Vehicle.ImageFileName;

                repo.InsertVehicle(newVehicle);

                return RedirectToAction("EditVehicle", "Admin", new { id = newVehicle.VehicleId });
            }
            else
            {
                addVehicle.VehicleMakes = new SelectList(repo.GetVehicleMakes(), "MakeId", "MakeName");
                addVehicle.VehicleModels = new SelectList(repo.GetVehicleModels(), "ModelId", "ModelTypeName");
                addVehicle.VehicleType = new SelectList(repo.GetVehicleType(), "TypeId", "TypeName");
                addVehicle.BodyStyles = new SelectList(repo.GetBodyStyle(), "BodyStyleId", "BodyStyleName");
                addVehicle.Transmission = new SelectList(repo.GetTransmission(), "TransmissionId", "TransmissionTypeName");
                addVehicle.ExteriorColor = new SelectList(repo.GetExteriorColor(), "ExteriorColorId", "ExteriorColorName");
                addVehicle.InteriorColor = new SelectList(repo.GetInteriorColor(), "InteriorColorId", "InteriorColorName");

                return View(addVehicle);
            }

        }
        public ActionResult EditVehicle(int id)
        {
            var model = new AddEditVehicle();
            var repo = VehicleRepositoryFactory.GetRepository();

            model.VehicleMakes = new SelectList(repo.GetVehicleMakes(), "MakeId", "MakeName");
            model.VehicleModels = new SelectList(repo.GetVehicleModels(), "ModelId", "ModelTypeName");
            model.VehicleType = new SelectList(repo.GetVehicleType(), "TypeId", "TypeName");
            model.BodyStyles = new SelectList(repo.GetBodyStyle(), "BodyStyleId", "BodyStyleName");
            model.Transmission = new SelectList(repo.GetTransmission(), "TransmissionId", "TransmissionTypeName");
            model.ExteriorColor = new SelectList(repo.GetExteriorColor(), "ExteriorColorId", "ExteriorColorName");
            model.InteriorColor = new SelectList(repo.GetInteriorColor(), "InteriorColorId", "InteriorColorName");

            model.Vehicle = repo.GetVehicleById(id);

            return View(model);
        }
        
        [HttpPost]
        public ActionResult EditVehicle(AddEditVehicle model, string action)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var oldVehicleDetails = repo.GetDetails(model.Vehicle.VehicleId);

            switch (action)
            {
                case "delete":
                    repo.DeleteVehicle(model.Vehicle.VehicleId, model.Vehicle.ImageFileName);

                    return RedirectToAction("AddVehicle", "Admin");
                    
                case "save":

                    if (model.Vehicle.ModelId == null)
                    {
                        ModelState.AddModelError("Error", "Please select a model.");
                    }

                    if (ModelState.IsValid)
                    {
                        if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                        {
                            var savepath = Server.MapPath("~/Images");

                            string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                            string extension = Path.GetExtension(model.ImageUpload.FileName);

                            var filePath = Path.Combine(savepath, fileName + extension);

                            int counter = 1;
                            while (System.IO.File.Exists(filePath))
                            {
                                filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                                counter++;
                            }

                            model.ImageUpload.SaveAs(filePath);
                            model.Vehicle.ImageFileName = Path.GetFileName(filePath);

                            //delete old file
                            var oldPath = Path.Combine(savepath, oldVehicleDetails.ImageFileName);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }
                        else
                        {
                            // they did not replace the old file, so keep the old file name
                            model.Vehicle.ImageFileName = oldVehicleDetails.ImageFileName;
                        }

                        repo.UpdateVehicle(model.Vehicle);

                        return RedirectToAction("EditVehicle", "Admin", new { id = model.Vehicle.VehicleId });

                    }
                    else
                    {
                        model.VehicleMakes = new SelectList(repo.GetVehicleMakes(), "MakeId", "MakeName");
                        model.VehicleModels = new SelectList(repo.GetVehicleModels(), "ModelId", "ModelTypeName");
                        model.VehicleType = new SelectList(repo.GetVehicleType(), "TypeId", "TypeName");
                        model.BodyStyles = new SelectList(repo.GetBodyStyle(), "BodyStyleId", "BodyStyleName");
                        model.Transmission = new SelectList(repo.GetTransmission(), "TransmissionId", "TransmissionTypeName");
                        model.ExteriorColor = new SelectList(repo.GetExteriorColor(), "ExteriorColorId", "ExteriorColorName");
                        model.InteriorColor = new SelectList(repo.GetInteriorColor(), "InteriorColorId", "InteriorColorName");

                        model.Vehicle = new Vehicle();
                        return View(model);
                    }
                    
                default: throw new ArgumentOutOfRangeException();
            }
        }
        public ActionResult Users()
        {
            var model = UserRepositoryFactory.GetRepository().GetAll();
            
            return View(model);
        }
        public ActionResult AddMake()
        {
            var model = new AddMakes();
            var repo = VehicleRepositoryFactory.GetRepository();
            model.MakesList = (List<VehicleMakes>)repo.GetVehicleMakes();
            return View(model);
        }

        // POST: /Admin/AddMake
        [HttpPost]
        
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMake(AddMakes model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            model.Make.UserEmail = user.Email;

            var repo = VehicleRepositoryFactory.GetRepository();
            
            if (model.Make.MakeName == null)
            {
                ModelState.AddModelError("Error", "Make name required");
            }
            if (ModelState.IsValid)
            {
                VehicleMakes make = new VehicleMakes();
                make.UserEmail = model.Make.UserEmail;
                make.MakeName = model.Make.MakeName;

                repo.InsertMake(make);

                var returnModel = new AddMakes();
                ModelState.Clear();
                var returnRepo = VehicleRepositoryFactory.GetRepository();
                returnModel.MakesList = (List<VehicleMakes>)returnRepo.GetVehicleMakes();
                
                return View(returnModel);

            }

            model.MakesList = (List<VehicleMakes>)repo.GetVehicleMakes();
            return View(model);

        }
        public ActionResult AddModel()
        {
            var model = new AddModels();
            var repo = VehicleRepositoryFactory.GetRepository();
            model.ModelsList = (List<VehicleModels>)repo.GetVehicleModels();
            model.VehicleMakes = new SelectList(repo.GetVehicleMakes(), "MakeId", "MakeName");
            return View(model);
        }

        // POST: /Admin/AddModel
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AddModel(AddModels model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            model.vehicleModel.UserEmail = user.Email;

            var repo = VehicleRepositoryFactory.GetRepository();

            if (model.vehicleModel.ModelTypeName == null)
            {
                ModelState.AddModelError("Error", "Model name required");
            }
            if (ModelState.IsValid)
            {
                VehicleModels vehicleModel = new VehicleModels();
                vehicleModel.UserEmail = model.vehicleModel.UserEmail;
                vehicleModel.ModelTypeName = model.vehicleModel.ModelTypeName;
                vehicleModel.MakeId = model.vehicleModel.MakeId;

                repo.InsertModel(vehicleModel);

                var returnModel = new AddModels();
                ModelState.Clear();
                var returnRepo = VehicleRepositoryFactory.GetRepository();
                returnModel.ModelsList = (List<VehicleModels>)returnRepo.GetVehicleModels();
                returnModel.VehicleMakes = new SelectList(repo.GetVehicleMakes(), "MakeId", "MakeName");

                return View(returnModel);

            }
            
            model.ModelsList = (List<VehicleModels>)repo.GetVehicleModels();
            model.VehicleMakes = new SelectList(repo.GetVehicleMakes(), "MakeId", "MakeName");
            return View(model);

        }
        public ActionResult AddDeleteSpecial()
        {
            var model = new AddDeleteSpecials();
            var repo = SpecialRepositoryFactory.GetRepository();
            model.SpecialsList = repo.GetAll();

            return View(model);
        }

        // POST: /Admin/AddDeleteSpecial
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public ActionResult AddDeleteSpecial(AddDeleteSpecials model)
        {
            var repo = SpecialRepositoryFactory.GetRepository();

            if (model.Special.Title == null)
            {
                ModelState.AddModelError("Error", "Title required");
            }
            if (model.Special.SpecialDescription == null)
            {
                ModelState.AddModelError("Error", "Description required");
            }

            if (ModelState.IsValid)
            {
                Specials special = new Specials();
                special.Title = model.Special.Title;
                special.SpecialDescription = model.Special.SpecialDescription;

                repo.Insert(special);

                var returnModel = new AddDeleteSpecials();
                ModelState.Clear();
                var returnRepo = SpecialRepositoryFactory.GetRepository();
                returnModel.SpecialsList = repo.GetAll();

                return View(returnModel);

            }

            model.SpecialsList = repo.GetAll();

            return View(model);

        }

        public ActionResult DeleteSpecial(int Id)
        {
            var repo = SpecialRepositoryFactory.GetRepository();
            repo.Delete(Id);

            return RedirectToAction("AddDeleteSpecial", "Admin");
        }

        public ActionResult SalesReport()
        {
            var model = new SalesReportView();
            var repo = UserRepositoryFactory.GetRepository();
            model.Users = new SelectList(repo.GetAll(), "Id", "Email");

            return View(model);
        }

        public ActionResult InventoryReport()
        {
            var model = new InventoryReport();
            var repo = VehicleRepositoryFactory.GetRepository();
            model.NewVehicles = repo.GetNewVehicleInventory();
            model.UsedVehicles = repo.GetUsedVehicleInventory();

            return View(model);
        }
    }
}