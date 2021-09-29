using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.ADO;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;


namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class ADOTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepositoryADO();

            var states = repo.GetAll();

            Assert.AreEqual(52, states.Count);

            Assert.AreEqual("MN", states[23].StateId);
            Assert.AreEqual("Minnesota", states[23].StateName);
        }

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialsRepositoryADO();

            var specials = repo.GetAll();

            Assert.AreEqual(5, specials.Count);

            Assert.AreEqual(1, specials[0].SpecialId);
            Assert.AreEqual("SpecialTest_1", specials[0].Title);
            Assert.AreEqual("The Special Test 1.", specials[0].SpecialDescription);
        }

        [Test]
        public void CanAddSpecials()
        {
            var repo = new SpecialsRepositoryADO();
            var specials = repo.GetAll();

            Assert.AreEqual(5, specials.Count);

            Specials specialToAdd = new Specials();

            specialToAdd.Title = "ADO_Special_Add_Test_Title";
            specialToAdd.SpecialDescription = "ADO Special to add test description.";

            repo.Insert(specialToAdd);

            Assert.AreEqual(6, specialToAdd.SpecialId);
            var specialsAdded = repo.GetAll();

            Assert.AreEqual(6, specialsAdded.Count);
            Assert.AreEqual("ADO_Special_Add_Test_Title", specialsAdded[5].Title);
            Assert.AreEqual("ADO Special to add test description.", specialsAdded[5].SpecialDescription);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            Specials specialToAdd = new Specials();
            var repo = new SpecialsRepositoryADO();

            specialToAdd.Title = "ADO_Special_Add_Test_Title";
            specialToAdd.SpecialDescription = "ADO Special to add test description.";
            repo.Insert(specialToAdd);
            var specialsAdded = repo.GetAll();
            Assert.AreEqual(6, specialsAdded.Count);
            Assert.AreEqual("ADO_Special_Add_Test_Title", specialsAdded[5].Title);
            Assert.AreEqual("ADO Special to add test description.", specialsAdded[5].SpecialDescription);

            repo.Delete(6);
            var deletedSpecials = repo.GetAll();
            Assert.AreEqual(5, deletedSpecials.Count);
        }

        [Test]
        public void CanLoadFeatured()
        {
            var repo = new VehicleRepositoryADO();

            List<FeaturedVehicleShortItem> featured = repo.GetFeatured().ToList();

            Assert.AreEqual(3, featured.Count());

            Assert.AreEqual(2, featured[0].vehicleId);
            Assert.AreEqual("BMW", featured[0].MakeName);
            Assert.AreEqual("A1", featured[0].ModelTypeName);
            Assert.AreEqual(20000, featured[0].SalePrice);
            Assert.AreEqual("placeholder1.png", featured[0].ImageFileName);
            Assert.AreEqual(2005, featured[0].VehicleYear);
        }

        [Test]
        public void CanLoadNewVehicles()
        {
            var repo = new VehicleRepositoryADO();

            List<VehicleDetails> newVehicles = repo.GetNewVehicles().ToList();

            Assert.AreEqual(5, newVehicles.Count());

            Assert.AreEqual(8, newVehicles[0].VehicleId);
            Assert.AreEqual("Audi", newVehicles[0].MakeName);
            Assert.AreEqual("A4", newVehicles[0].ModelTypeName);
            Assert.AreEqual(15000, newVehicles[0].SalePrice);
            Assert.AreEqual("test.png", newVehicles[0].ImageFileName);
            Assert.AreEqual(2000, newVehicles[0].VehicleYear);
            Assert.AreEqual(19000, newVehicles[0].MSRP);
            Assert.AreEqual("VinTest5", newVehicles[0].VIN);
            Assert.AreEqual("Yellow", newVehicles[0].ExteriorColorName);
            Assert.AreEqual("Black", newVehicles[0].InteriorColorName);
            Assert.AreEqual("Car", newVehicles[0].BodyStyleName);
            Assert.AreEqual("Automatic", newVehicles[0].TransmissionTypeName);
            Assert.AreEqual(0, newVehicles[0].Mileage);

        }

        [Test]
        public void CanSearchNewVehiclesOnMinPrice()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchNewVehicles(new VehicleSearchParameters { MinPrice = 1000M });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanSearchNewVehiclesOnMaxPrice()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchNewVehicles(new VehicleSearchParameters { MaxPrice = 2000M });

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void CanSearchNewVehicleOnSearchTerm()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchNewVehicles(new VehicleSearchParameters { SearchTerm = "A" });
            Assert.AreEqual(5, found.Count());

            found = repo.SearchNewVehicles(new VehicleSearchParameters { SearchTerm = "2" });
            Assert.AreEqual(2, found.Count());

            found = repo.SearchNewVehicles(new VehicleSearchParameters { SearchTerm = "1" });
            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanLoadUsedVehicles()
        {
            var repo = new VehicleRepositoryADO();

            List<VehicleDetails> usedVehicles = repo.GetUsedVehicles().ToList();

            Assert.AreEqual(3, usedVehicles.Count());

            Assert.AreEqual(7, usedVehicles[0].VehicleId);
            Assert.AreEqual("DB test model", usedVehicles[0].ModelTypeName);
            Assert.AreEqual(60000, usedVehicles[0].SalePrice);
            Assert.AreEqual("placeholder1.png", usedVehicles[0].ImageFileName);
            Assert.AreEqual(2022, usedVehicles[0].VehicleYear);
            Assert.AreEqual(60000, usedVehicles[0].MSRP);
            Assert.AreEqual("VinTest7", usedVehicles[0].VIN);
            Assert.AreEqual("", usedVehicles[0].ExteriorColorName);
            Assert.AreEqual("", usedVehicles[0].InteriorColorName);
            Assert.AreEqual("", usedVehicles[0].BodyStyleName);
            Assert.AreEqual("", usedVehicles[0].TransmissionTypeName);
            Assert.AreEqual(1000, usedVehicles[0].Mileage);
        }

        [Test]
        public void CanSearchUsedVehiclesOnMinPrice()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchUsedVehicles(new VehicleSearchParameters { MinPrice = 60000M });

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void CanSearchUsedVehiclesOnMaxPrice()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchUsedVehicles(new VehicleSearchParameters { MaxPrice = 25000M });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchUsedVehicleOnSearchTerm()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchUsedVehicles(new VehicleSearchParameters { SearchTerm = "A" });
            Assert.AreEqual(1, found.Count());

            found = repo.SearchUsedVehicles(new VehicleSearchParameters { SearchTerm = "2" });
            Assert.AreEqual(3, found.Count());

            found = repo.SearchUsedVehicles(new VehicleSearchParameters { SearchTerm = "1" });
            Assert.AreEqual(0, found.Count());
        }

        [Test]
        public void CanLoadListingDetails()
        {
            var repo = new VehicleRepositoryADO();
            var vehicle = repo.GetDetails(1);

            Assert.IsNotNull(vehicle);

            Assert.AreEqual(1, vehicle.VehicleId);
            Assert.AreEqual(1970, vehicle.VehicleYear);
            Assert.AreEqual("Audi", vehicle.MakeName);
            Assert.AreEqual("A1", vehicle.ModelTypeName);
            Assert.AreEqual("Car", vehicle.BodyStyleName);
            Assert.AreEqual("Automatic", vehicle.TransmissionTypeName);
            Assert.AreEqual("Yellow", vehicle.ExteriorColorName);
            Assert.AreEqual("Black", vehicle.InteriorColorName);
            Assert.AreEqual(0, vehicle.Mileage);
            Assert.AreEqual("VinTest1", vehicle.VIN);
            Assert.AreEqual(1000, vehicle.SalePrice);
            Assert.AreEqual(5000, vehicle.MSRP);
            Assert.AreEqual("placeholder1.png", vehicle.ImageFileName);
            Assert.AreEqual("Test Vehicle 1 description.", vehicle.VehicleDescription);

            var vehicle2 = repo.GetDetails(2);

            Assert.IsNotNull(vehicle2);
            Assert.AreEqual(2, vehicle2.VehicleId);
            Assert.AreEqual("A1", vehicle2.ModelTypeName);

        }

        [Test]
        public void CanAddContactUs()
        {
            var repo = new ContactUsRepositoryADO();

            ContactUs contactUsToAdd = new ContactUs();

            contactUsToAdd.ContactName = "Test";
            contactUsToAdd.Email = "testContactUs2@test.com";
            contactUsToAdd.PhoneNumber = "222-222-2222";
            contactUsToAdd.ContactUsMessage = "test contact us message 2";

            repo.Insert(contactUsToAdd);

            Assert.AreEqual(2, contactUsToAdd.ContactUsId);

        }

        [Test]
        public void CanLoadVehicles()
        {
            var repo = new VehicleRepositoryADO();

            List<VehicleDetails> vehicles = repo.GetSalesVehicles().ToList();

            Assert.AreEqual(8, vehicles.Count());

            Assert.AreEqual(8, vehicles[3].VehicleId);
            Assert.AreEqual("A4", vehicles[3].ModelTypeName);
            Assert.AreEqual(15000, vehicles[3].SalePrice);
            Assert.AreEqual("test.png", vehicles[3].ImageFileName);
            Assert.AreEqual(2000, vehicles[3].VehicleYear);
            Assert.AreEqual(19000, vehicles[3].MSRP);
            Assert.AreEqual("VinTest5", vehicles[3].VIN);
            Assert.AreEqual("Yellow", vehicles[3].ExteriorColorName);
            Assert.AreEqual("Black", vehicles[3].InteriorColorName);
            Assert.AreEqual("Car", vehicles[3].BodyStyleName);
            Assert.AreEqual("Automatic", vehicles[3].TransmissionTypeName);
            Assert.AreEqual(0, vehicles[3].Mileage);
        }

        [Test]
        public void CanSearchSalesVehiclesOnMinPrice()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchSalesVehicles(new VehicleSearchParameters { MinPrice = 10000M });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanSearchSalesVehiclesOnMaxPrice()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchSalesVehicles(new VehicleSearchParameters { MaxPrice = 10000M });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchSalesVehicleOnSearchTerm()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchSalesVehicles(new VehicleSearchParameters { SearchTerm = "A" });
            Assert.AreEqual(6, found.Count());

            found = repo.SearchSalesVehicles(new VehicleSearchParameters { SearchTerm = "2000" });
            Assert.AreEqual(2, found.Count());

            found = repo.SearchSalesVehicles(new VehicleSearchParameters { SearchTerm = "1" });
            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanAddPurchaseVehicleAndDeleteVehicle()
        {
            var repo = new VehicleRepositoryADO();

            PurchaseVehicle purchaseVehicle = new PurchaseVehicle();
            Vehicle vehicle = new Vehicle();

            purchaseVehicle.UserId = "11111111 - 1111 - 1111 - 1111 - 111111111111";
            purchaseVehicle.PurchaseName = "ADO Test Purchaser 1";
            purchaseVehicle.PhoneNumber = "444-444-4444";
            purchaseVehicle.Email = "testpurchase4@test.com";
            purchaseVehicle.Street1 = "ADO Test Street 1";
            purchaseVehicle.Street2 = "ADO Test Street 2";
            purchaseVehicle.City = "ADO Test City 1";
            purchaseVehicle.StateId = "LA";
            purchaseVehicle.ZipCode = "44444";
            purchaseVehicle.PurchasePrice = 19000;
            purchaseVehicle.PurchaseTypeId = 1;

            purchaseVehicle.PurchaseId = 4;
            vehicle.VehicleId = 8;
            vehicle.ImageFileName = "test.png";

            repo.AddPurchaseVehicle(purchaseVehicle);
            Assert.AreEqual(4, purchaseVehicle.PurchaseId);

            repo.DeleteVehicle(vehicle.VehicleId, vehicle.ImageFileName);

            List<VehicleDetails> vehicles = repo.GetSalesVehicles().ToList();

            Assert.AreEqual(7, vehicles.Count());
        }

        [Test]
        public void CanAddVehicle()
        {
            var repo = new VehicleRepositoryADO();
            List<VehicleDetails> vehicles = repo.GetSalesVehicles().ToList();

            Assert.AreEqual(8, vehicles.Count());

            Vehicle vehicleToAdd = new Vehicle();

            vehicleToAdd.MakeId = 1;
            vehicleToAdd.ModelId = 1;
            vehicleToAdd.TypeId = 1;
            vehicleToAdd.BodyStyleId = 1;
            vehicleToAdd.TransmissionId = 1;
            vehicleToAdd.ExteriorColorId = 1;
            vehicleToAdd.InteriorColorId = 1;
            vehicleToAdd.Mileage = 12345;
            vehicleToAdd.VIN = "ADO VinTest";
            vehicleToAdd.MSRP = 30000;
            vehicleToAdd.SalePrice = 3000;
            vehicleToAdd.VehicleYear = 2003;
            vehicleToAdd.VehicleDescription = "ADO Test desc 1";
            vehicleToAdd.HasFeatured = true;
            vehicleToAdd.ImageFileName = "test.png";

            repo.InsertVehicle(vehicleToAdd);

            Assert.AreEqual(9, vehicleToAdd.VehicleId);

            List<VehicleDetails> vehicleAdded = repo.GetSalesVehicles().ToList();

            Assert.AreEqual(9, vehicleAdded.Count());
            Assert.AreEqual("VinTest7", vehicleAdded[0].VIN);
            Assert.AreEqual(60000, vehicleAdded[0].MSRP);
            Assert.AreEqual("X3 Sports Activity Coupe", vehicleAdded[1].ModelTypeName);
        }

        [Test]
        public void CanUpdateVehicle()
        {
            var repo = new VehicleRepositoryADO();
            Vehicle vehicleToAdd = new Vehicle();

            vehicleToAdd.MakeId = 1;
            vehicleToAdd.ModelId = 1;
            vehicleToAdd.TypeId = 1;
            vehicleToAdd.BodyStyleId = 1;
            vehicleToAdd.TransmissionId = 1;
            vehicleToAdd.ExteriorColorId = 1;
            vehicleToAdd.InteriorColorId = 1;
            vehicleToAdd.Mileage = 12345;
            vehicleToAdd.VIN = "ADO VinTest1";
            vehicleToAdd.MSRP = 30000;
            vehicleToAdd.SalePrice = 3000;
            vehicleToAdd.VehicleYear = 2003;
            vehicleToAdd.VehicleDescription = "ADO Test desc 1";
            vehicleToAdd.HasFeatured = false;
            vehicleToAdd.ImageFileName = "placeholder.png";

            repo.InsertVehicle(vehicleToAdd);

            List<VehicleDetails> vehicleAdded = repo.GetSalesVehicles().ToList();

            Assert.AreEqual(9, vehicleAdded.Count());
            Assert.AreEqual("VinTest7", vehicleAdded[0].VIN);
            Assert.AreEqual(60000, vehicleAdded[0].MSRP);
            Assert.AreEqual("X3 Sports Activity Coupe", vehicleAdded[1].ModelTypeName);

            vehicleToAdd.MakeId = 2;
            vehicleToAdd.ModelId = 2;
            vehicleToAdd.TypeId = 2;
            vehicleToAdd.BodyStyleId = 2;
            vehicleToAdd.TransmissionId = 2;
            vehicleToAdd.ExteriorColorId = 2;
            vehicleToAdd.InteriorColorId = 2;
            vehicleToAdd.Mileage = 1;
            vehicleToAdd.VIN = "ADO VinTestUpdate";
            vehicleToAdd.MSRP = 40000;
            vehicleToAdd.SalePrice = 4000;
            vehicleToAdd.VehicleYear = 2004;
            vehicleToAdd.VehicleDescription = "ADO Test desc 1Update";
            vehicleToAdd.HasFeatured = true;
            vehicleToAdd.ImageFileName = "updated.png";

            repo.UpdateVehicle(vehicleToAdd);

            Assert.AreEqual(9, vehicleToAdd.VehicleId);

            List<VehicleDetails> vehicleUpdated = repo.GetSalesVehicles().ToList();

            Assert.AreEqual(9, vehicleUpdated.Count());
            Assert.AreEqual("VinTest7", vehicleUpdated[0].VIN);
            Assert.AreEqual(60000, vehicleUpdated[0].MSRP);
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new VehicleRepositoryADO();

            List<VehicleMakes> makes = repo.GetVehicleMakes().ToList();

            Assert.AreEqual(9, makes.Count());

            Assert.AreEqual("Audi", makes[0].MakeName);
            Assert.AreEqual("test@test.com", makes[0].UserEmail);
        }

        [Test]
        public void CanAddMake()
        {
            var repo = new VehicleRepositoryADO();
            List<VehicleMakes> makes = repo.GetVehicleMakes().ToList();
            Assert.AreEqual(9, makes.Count());

            VehicleMakes makeToAdd = new VehicleMakes();

            makeToAdd.MakeName = "ADO Test Make";
            makeToAdd.UserEmail = "ADOtest@test.com";

            repo.InsertMake(makeToAdd);

            Assert.AreEqual(10, makeToAdd.MakeId);

            List<VehicleMakes> makeAdded = repo.GetVehicleMakes().ToList();

            Assert.AreEqual(10, makeAdded.Count());
        }

        [Test]
        public void CanLoadModels()
        {
            var repo = new VehicleRepositoryADO();

            List<VehicleModels> models = repo.GetVehicleModels().ToList();

            Assert.AreEqual(12, models.Count());

            Assert.AreEqual("A1", models[0].ModelTypeName);
            Assert.AreEqual("test@test.com", models[0].UserEmail);
        }

        [Test]
        public void CanAddModel()
        {
            var repo = new VehicleRepositoryADO();
            List<VehicleModels> models = repo.GetVehicleModels().ToList();
            Assert.AreEqual(12, models.Count());

            VehicleModels modelToAdd = new VehicleModels();

            modelToAdd.MakeId = 1;
            modelToAdd.ModelTypeName = "ADOtest";
            modelToAdd.UserEmail = "ADOtest@test.com";

            repo.InsertModel(modelToAdd);

            Assert.AreEqual(13, modelToAdd.ModelId);

            List<VehicleModels> modelAdded = repo.GetVehicleModels().ToList();

            Assert.AreEqual(13, modelAdded.Count());
        }

        [Test]
        public void CanLoadNewVehicleInventory()
        {
            var repo = new VehicleRepositoryADO();

            List<VehicleInventory> newVehicleInventory = repo.GetNewVehicleInventory().ToList();

            Assert.AreEqual(4, newVehicleInventory.Count());

            Assert.AreEqual(2, newVehicleInventory[0].VehicleCount);
            Assert.AreEqual(37000, newVehicleInventory[0].StockValue);
        }

        [Test]
        public void CanLoadUsedVehicleInventory()
        {
            var repo = new VehicleRepositoryADO();

            List<VehicleInventory> usedVehicleInventory = repo.GetUsedVehicleInventory().ToList();

            Assert.AreEqual(3, usedVehicleInventory.Count());

            Assert.AreEqual(1, usedVehicleInventory[0].VehicleCount);
            Assert.AreEqual(60000, usedVehicleInventory[0].StockValue);
        }

        [Test]
        public void CanLoadSalesReport()
        {
            var repo = new VehicleRepositoryADO();

            List<SalesReport> report = repo.GetSalesReport().ToList();

            Assert.AreEqual(2, report.Count());

            Assert.AreEqual(2, report[0].TotalVehicles);
            Assert.AreEqual(50000, report[0].TotalSales);
            Assert.AreEqual("FirstnameSales", report[0].FirstName);
        }

        [Test]
        public void CanSearchSalesReport()
        {
            var repo = new VehicleRepositoryADO();

            var found = repo.SearchSalesReport(new SalesReportSearchParameters { UserId = "03f2f18c-e5c5-4d17-a8f4-9c22060dce88", FromDate = new DateTime(2002, 2, 2), ToDate = new DateTime(2003, 3, 3) });

            Assert.AreEqual(1, found.Count());
        }

    }
}
