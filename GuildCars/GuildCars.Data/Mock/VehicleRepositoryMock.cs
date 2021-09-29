using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GuildCars.Data.Factory;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.Models.UIViews;
using static GuildCars.Models.Queries.FeaturedVehicleShortItemView.ShortItemFeaturedVehicles;

namespace GuildCars.Data.Mock
{
    public class VehicleRepositoryMock : IVehicleRepository
    {
        private static List<Vehicle> vehicleList = new List<Vehicle>()
        {
            new Vehicle(){VehicleId = 1, MakeId = 1, ModelId = 1, TypeId = 1, BodyStyleId = 1, TransmissionId = 1, ExteriorColorId = 1, InteriorColorId = 1, Mileage = 1, 
                VIN = "TestVin1", MSRP = 10000, SalePrice = 9500, VehicleDescription = "Mock Vehicle 1.", VehicleYear = 2021, HasFeatured = false, ImageFileName = "veh2.png" },
            new Vehicle(){VehicleId = 5, MakeId = 1, ModelId = 1, TypeId = 1, BodyStyleId = 1, TransmissionId = 1, ExteriorColorId = 1, InteriorColorId = 1, Mileage = 1,
                VIN = "TestVin5", MSRP = 20000, SalePrice = 19500, VehicleDescription = "Mock Vehicle 5.", VehicleYear = 2021, HasFeatured = false, ImageFileName = "veh2.png" },
            new Vehicle(){VehicleId = 2, MakeId = 2, ModelId = 2, TypeId = 1, BodyStyleId = 2, TransmissionId = 2, ExteriorColorId = 2, InteriorColorId = 2, Mileage = 2,
                VIN = "TestVin2", MSRP = 20000, SalePrice = 19500, VehicleDescription = "Mock Vehicle 2.", VehicleYear = 2022, HasFeatured = false, ImageFileName = "veh3.png" },
            new Vehicle(){VehicleId = 3, MakeId = 1, ModelId = 1, TypeId = 2, BodyStyleId = 1, TransmissionId = 1, ExteriorColorId = 1, InteriorColorId = 1, Mileage = 100000,
                VIN = "TestVin3", MSRP = 30000, SalePrice = 29500, VehicleDescription = "Mock Vehicle 3.", VehicleYear = 2010, HasFeatured = true, ImageFileName = "placeholder1.png" },
            new Vehicle(){VehicleId = 6, MakeId = 2, ModelId = 2, TypeId = 2, BodyStyleId = 1, TransmissionId = 1, ExteriorColorId = 1, InteriorColorId = 1, Mileage = 100000,
                VIN = "TestVin3", MSRP = 30000, SalePrice = 29500, VehicleDescription = "Mock Vehicle 3.", VehicleYear = 2016, HasFeatured = false, ImageFileName = "placeholder2.png" },
            new Vehicle(){VehicleId = 4, MakeId = 2, ModelId = 2, TypeId = 2, BodyStyleId = 2, TransmissionId = 1, ExteriorColorId = 2, InteriorColorId = 2, Mileage = 200000,
                VIN = "TestVin4", MSRP = 40000, SalePrice = 39500, VehicleDescription = "Mock Vehicle 4.", VehicleYear = 2016, HasFeatured = true, ImageFileName = "placeholder3.png" }
        };
        private static List<BodyStyles> bodyStylesList = new List<BodyStyles>()
        {
            new BodyStyles(){BodyStyleId = 1, BodyStyleName = "Car"},
            new BodyStyles(){BodyStyleId = 2, BodyStyleName = "SUV"}
        };
        private static List<VehicleMakes> vehicleMakesList = new List<VehicleMakes>()
        {
            new VehicleMakes(){MakeId = 1, MakeName = "Audi", UserEmail = "mocktest.com", CreatedDate =  DateTime.Now },
            new VehicleMakes(){MakeId = 2, MakeName = "BMW", UserEmail = "mocktest.com", CreatedDate =  DateTime.Now }
        };
        private static List<VehicleModels> vehicleModelsList = new List<VehicleModels>()
        {
            new VehicleModels(){ModelId = 1, MakeId = 1, ModelTypeName = "A1", UserEmail = "mocktest.com", CreatedDate = DateTime.Now },
            new VehicleModels(){ModelId = 2, MakeId = 1, ModelTypeName = "A4", UserEmail = "mocktest.com", CreatedDate = DateTime.Now },
            new VehicleModels(){ModelId = 2, MakeId = 2, ModelTypeName = "X3", UserEmail = "mocktest.com", CreatedDate = DateTime.Now }
        };
        private static List<Transmission> transmissionList = new List<Transmission>()
        {
            new Transmission(){TransmissionId = 1, TransmissionTypeName = "Automatic"},
            new Transmission(){TransmissionId = 2, TransmissionTypeName = "Manual"}
        };
        private static List<ExteriorColor> exteriorColorsList = new List<ExteriorColor>()
        {
            new ExteriorColor(){ExteriorColorId = 1, ExteriorColorName = "Yellow"},
            new ExteriorColor(){ExteriorColorId = 2, ExteriorColorName = "Black"}
        };
        private static List<InteriorColor> interiorColorsList = new List<InteriorColor>()
        {
            new InteriorColor(){InteriorColorId = 1, InteriorColorName = "Black"},
            new InteriorColor(){InteriorColorId = 2, InteriorColorName = "Gray"},
        };
        private static List<VehicleType> vehicleTypesList = new List<VehicleType>()
        {
            new VehicleType(){TypeId = 1, TypeName = "New"},
            new VehicleType(){TypeId = 2, TypeName = "Used"}

        };

        private static List<PurchaseVehicle> purchaseVehicleList = new List<PurchaseVehicle>()
        {
            new PurchaseVehicle(){PurchaseId = 1, UserId = "e33cead2-2d9a-4944-bec9-85f36c2ce364", PurchaseName = "Mock", PhoneNumber = "111-111-1111", Email = "testpurchase1@test.com", 
                Street1 = "Mock St 1", Street2 = "Mock St 2", City = "Mock City", StateId = "AZ", ZipCode = "11111", PurchasePrice = 1000, PurchaseTypeId = 1, PurchaseDate = new DateTime(2010, 1, 1)},
            new PurchaseVehicle(){PurchaseId = 1, UserId = "03f2f18c-e5c5-4d17-a8f4-9c22060dce88", PurchaseName = "Mock", PhoneNumber = "111-111-1111", Email = "testpurchase1@test.com",
                Street1 = "Mock St 1", Street2 = "Mock St 2", City = "Mock City", StateId = "AZ", ZipCode = "11111", PurchasePrice = 1000, PurchaseTypeId = 1, PurchaseDate = new DateTime(2020, 2, 2)},
            new PurchaseVehicle(){PurchaseId = 1, UserId = "03f2f18c-e5c5-4d17-a8f4-9c22060dce88", PurchaseName = "Mock", PhoneNumber = "111-111-1111", Email = "testpurchase1@test.com",
                Street1 = "Mock St 1", Street2 = "Mock St 2", City = "Mock City", StateId = "AZ", ZipCode = "11111", PurchasePrice = 1000, PurchaseTypeId = 1, PurchaseDate = new DateTime(2015, 1, 1)},
        };

        public void AddPurchaseVehicle(PurchaseVehicle addPurchaseVehicle)
        {
            if (purchaseVehicleList.Any())
                addPurchaseVehicle.PurchaseId = purchaseVehicleList.Max(x => x.PurchaseId) + 1;
            else
                addPurchaseVehicle.PurchaseId = 1;
            addPurchaseVehicle.PurchaseDate = DateTime.Now;

            purchaseVehicleList.Add(addPurchaseVehicle);
        }

        public void DeleteVehicle(int vehicleId, string imageFileName)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string dirImages = @"D:\Repos\Projects\GuildCars\GuildCars.UI\Images\" + imageFileName;
            File.Delete(dirImages);

            Vehicle vehicle = vehicleList.FirstOrDefault(x => x.VehicleId == vehicleId);
            vehicleList.Remove(vehicle); 
        }

        public List<PurchaseTypes> GetAllPurchaseTypes()
        {
            List<PurchaseTypes> purchaseTypes = new List<PurchaseTypes>();

            purchaseTypes.Add(new PurchaseTypes() { PurchaseTypeId = 1, PurchaseTypeName = "Bank Finance" });
            purchaseTypes.Add(new PurchaseTypes() { PurchaseTypeId = 2, PurchaseTypeName = "Cash" });

            return purchaseTypes;
        }

        public List<States> GetAllStates()
        {
            List<States> states = new List<States>();

            states.Add(new States() { StateId = "AK", StateName = "Alaska" });
            states.Add(new States() { StateId = "AZ", StateName = "Arizona" });

            return states;
        }

        public IEnumerable<BodyStyles> GetBodyStyle()
        {
            List<BodyStyles> bodyStyles = new List<BodyStyles>();
            foreach (BodyStyles bodyStyle in bodyStylesList)
            {
                BodyStyles addBodyStyle = new BodyStyles();
                addBodyStyle.BodyStyleName = bodyStyle.BodyStyleName;
                addBodyStyle.BodyStyleId = bodyStyle.BodyStyleId;

                bodyStyles.Add(addBodyStyle);
            };

            return bodyStyles;
        }

        public VehicleDetails GetDetails(int vehicleId)
        {
            Vehicle vehicle = vehicleList.FirstOrDefault(x => x.VehicleId == vehicleId);
            var currentVehicle = new VehicleDetails();

            currentVehicle.VehicleId = vehicle.VehicleId;
            currentVehicle.MakeName = (from Vehicle in vehicleList
                                       join VehicleMakes in vehicleMakesList
                                       on vehicle.MakeId equals VehicleMakes.MakeId
                                       where vehicle.MakeId == VehicleMakes.MakeId
                                       select VehicleMakes.MakeName).ToList().FirstOrDefault();
            currentVehicle.ModelTypeName = (from Vehicle in vehicleList
                                            join VehicleModels in vehicleModelsList
                                            on vehicle.ModelId equals VehicleModels.ModelId
                                            where vehicle.ModelId == VehicleModels.ModelId
                                            select VehicleModels.ModelTypeName).ToList().FirstOrDefault();
            currentVehicle.BodyStyleName = (from Vehicle in vehicleList
                                            join BodyStyles in bodyStylesList
                                            on vehicle.BodyStyleId equals BodyStyles.BodyStyleId
                                            where vehicle.BodyStyleId == BodyStyles.BodyStyleId
                                            select BodyStyles.BodyStyleName).ToList().FirstOrDefault();
            currentVehicle.TransmissionTypeName = (from Vehicle in vehicleList
                                                   join Transmission in transmissionList
                                                   on vehicle.TransmissionId equals Transmission.TransmissionId
                                                   where vehicle.TransmissionId == Transmission.TransmissionId
                                                   select Transmission.TransmissionTypeName).ToList().FirstOrDefault();
            currentVehicle.ExteriorColorName = (from Vehicle in vehicleList
                                                join ExteriorColor in exteriorColorsList
                                                on vehicle.ExteriorColorId equals ExteriorColor.ExteriorColorId
                                                where vehicle.ExteriorColorId == ExteriorColor.ExteriorColorId
                                                select ExteriorColor.ExteriorColorName).ToList().FirstOrDefault();
            currentVehicle.InteriorColorName = (from Vehicle in vehicleList
                                                join InteriorColor in interiorColorsList
                                                on vehicle.InteriorColorId equals InteriorColor.InteriorColorId
                                                where vehicle.InteriorColorId == InteriorColor.InteriorColorId
                                                select InteriorColor.InteriorColorName).ToList().FirstOrDefault();
            currentVehicle.Mileage = vehicle.Mileage;
            currentVehicle.VIN = vehicle.VIN;
            currentVehicle.MSRP = vehicle.MSRP;
            currentVehicle.SalePrice = vehicle.SalePrice;
            currentVehicle.VehicleYear = vehicle.VehicleYear;
            currentVehicle.ImageFileName = vehicle.ImageFileName;
            currentVehicle.VehicleDescription = vehicle.VehicleDescription;

            return currentVehicle;
        }

        public IEnumerable<ExteriorColor> GetExteriorColor()
        {
            List<ExteriorColor> exteriorColors = new List<ExteriorColor>();
            foreach (ExteriorColor exteriorColor in exteriorColorsList)
            {
                ExteriorColor addExteriorColor = new ExteriorColor();
                addExteriorColor.ExteriorColorName = exteriorColor.ExteriorColorName;
                addExteriorColor.ExteriorColorId = exteriorColor.ExteriorColorId;

                exteriorColors.Add(addExteriorColor);
            };

            return exteriorColors;
        }

        public IEnumerable<FeaturedVehicleShortItem> GetFeatured()
        {
            IList<FeaturedVehicleShortItem> featuredVehicleShortItems = new List<FeaturedVehicleShortItem>();
            var featuredVehicle = new FeaturedVehicleShortItem();

            foreach(var vehicle in vehicleList)
            {
                var currentVehicle = new FeaturedVehicleShortItem();

                if (vehicle.HasFeatured == true)
                {
                    currentVehicle.vehicleId = vehicle.VehicleId;
                    currentVehicle.SalePrice = vehicle.SalePrice;
                    currentVehicle.VehicleYear = vehicle.VehicleYear;
                    currentVehicle.MakeName = (from Vehicle in vehicleList
                                              join VehicleMakes in vehicleMakesList
                                              on vehicle.MakeId equals VehicleMakes.MakeId
                                              where vehicle.MakeId == VehicleMakes.MakeId
                                              select VehicleMakes.MakeName).ToList().FirstOrDefault();
                    currentVehicle.ModelTypeName = (from Vehicle in vehicleList
                                               join VehicleModels in vehicleModelsList
                                               on vehicle.ModelId equals VehicleModels.ModelId
                                               where vehicle.ModelId == VehicleModels.ModelId
                                               select VehicleModels.ModelTypeName).ToList().FirstOrDefault();
                    currentVehicle.ImageFileName = vehicle.ImageFileName;

                    featuredVehicleShortItems.Add(currentVehicle);
                }   
            }
            return featuredVehicleShortItems;
        }

        public IEnumerable<InteriorColor> GetInteriorColor()
        {
            List<InteriorColor> interiorColors = new List<InteriorColor>();
            foreach (InteriorColor interiorColor in interiorColorsList)
            {
                InteriorColor addInteriorColor = new InteriorColor();
                addInteriorColor.InteriorColorName = interiorColor.InteriorColorName;
                addInteriorColor.InteriorColorId = interiorColor.InteriorColorId;

                interiorColors.Add(addInteriorColor);
            };

            return interiorColors;
        }

        public List<VehicleModels> GetModelMakes(int id)
        {
            var vehicleModels = GetVehicleModels();
            var getModelMakes = from m in vehicleModels
                                where m.MakeId == id
                                select m;
            return getModelMakes.ToList();
        }

        public List<VehicleInventory> GetNewVehicleInventory()
        {
            IList<VehicleInventory> newVehiclesReportList = new List<VehicleInventory>();
            
            foreach (Vehicle vehicle in vehicleList)
            {
                if (vehicle.TypeId == 1)
                {
                    VehicleInventory newVehicleInventoryItem = new VehicleInventory()
                    {
                        VehicleYear = vehicle.VehicleYear,
                        MakeName = (from Vehicle in vehicleList
                                    join VehicleMakes in vehicleMakesList
                                    on vehicle.MakeId equals VehicleMakes.MakeId
                                    where vehicle.MakeId == VehicleMakes.MakeId
                                    select VehicleMakes.MakeName).ToList().FirstOrDefault(),
                        ModelTypeName = (from Vehicle in vehicleList
                                         join VehicleModels in vehicleModelsList
                                         on vehicle.ModelId equals VehicleModels.ModelId
                                         where vehicle.ModelId == VehicleModels.ModelId
                                         select VehicleModels.ModelTypeName).ToList().FirstOrDefault(),
                        VehicleCount = 1,
                        StockValue = vehicle.MSRP,
                    };

                    bool addToReport = true;
                    if (newVehiclesReportList.Any())
                    {  
                        foreach (VehicleInventory vehicleReportItem in newVehiclesReportList)
                        {
                            if (newVehicleInventoryItem.VehicleYear == vehicleReportItem.VehicleYear && newVehicleInventoryItem.MakeName == vehicleReportItem.MakeName && newVehicleInventoryItem.ModelTypeName == vehicleReportItem.ModelTypeName)
                            {
                                vehicleReportItem.StockValue += vehicle.MSRP;
                                vehicleReportItem.VehicleCount++;
                                addToReport = false;
                            }
                        }
                    }
                    if (addToReport)
                    {
                        newVehiclesReportList.Add(newVehicleInventoryItem);
                    }
                };  
            }
            return (List<VehicleInventory>)newVehiclesReportList;
        }

        public IEnumerable<VehicleDetails> GetNewVehicles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesReport> GetSalesReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDetails> GetSalesVehicles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transmission> GetTransmission()
        {
            List<Transmission> transmissions = new List<Transmission>();
            foreach (Transmission transmission in transmissionList)
            {
                Transmission addTransmission = new Transmission();
                addTransmission.TransmissionTypeName = transmission.TransmissionTypeName;
                addTransmission.TransmissionId = transmission.TransmissionId;

                transmissions.Add(addTransmission);
            };

            return transmissions;
        }

        public List<VehicleInventory> GetUsedVehicleInventory()
        {
            IList<VehicleInventory> usedVehiclesReportList = new List<VehicleInventory>();

            foreach (Vehicle vehicle in vehicleList)
            {
                if (vehicle.TypeId == 2)
                {
                    VehicleInventory usedVehicleInventoryItem = new VehicleInventory()
                    {
                        VehicleYear = vehicle.VehicleYear,
                        MakeName = (from Vehicle in vehicleList
                                    join VehicleMakes in vehicleMakesList
                                    on vehicle.MakeId equals VehicleMakes.MakeId
                                    where vehicle.MakeId == VehicleMakes.MakeId
                                    select VehicleMakes.MakeName).ToList().FirstOrDefault(),
                        ModelTypeName = (from Vehicle in vehicleList
                                         join VehicleModels in vehicleModelsList
                                         on vehicle.ModelId equals VehicleModels.ModelId
                                         where vehicle.ModelId == VehicleModels.ModelId
                                         select VehicleModels.ModelTypeName).ToList().FirstOrDefault(),
                        VehicleCount = 1,
                        StockValue = vehicle.MSRP,
                    };

                    bool addToReport = true;
                    if (usedVehiclesReportList.Any())
                    {
                        foreach (VehicleInventory vehicleReportItem in usedVehiclesReportList)
                        {
                            if (usedVehicleInventoryItem.VehicleYear == vehicleReportItem.VehicleYear && usedVehicleInventoryItem.MakeName == vehicleReportItem.MakeName && usedVehicleInventoryItem.ModelTypeName == vehicleReportItem.ModelTypeName)
                            {
                                vehicleReportItem.StockValue += vehicle.MSRP;
                                vehicleReportItem.VehicleCount++;
                                addToReport = false;
                            }
                        }
                    }
                    if (addToReport)
                    {
                        usedVehiclesReportList.Add(usedVehicleInventoryItem);
                    }
                };
            }
            return (List<VehicleInventory>)usedVehiclesReportList;
        }

        public IEnumerable<VehicleDetails> GetUsedVehicles()
        {
            throw new NotImplementedException();
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            Vehicle vehicle = vehicleList.FirstOrDefault(x => x.VehicleId == vehicleId);
            var currentVehicle = new Vehicle();

            currentVehicle.VehicleId = vehicle.VehicleId;
            currentVehicle.MakeId = vehicle.MakeId;
            currentVehicle.ModelId = vehicle.ModelId;
            currentVehicle.BodyStyleId = vehicle.BodyStyleId;
            currentVehicle.TransmissionId = vehicle.TransmissionId;
            currentVehicle.ExteriorColorId = vehicle.ExteriorColorId;
            currentVehicle.InteriorColorId = vehicle.InteriorColorId;
            currentVehicle.Mileage = vehicle.Mileage;
            currentVehicle.VIN = vehicle.VIN;
            currentVehicle.MSRP = vehicle.MSRP;
            currentVehicle.SalePrice = vehicle.SalePrice;
            currentVehicle.VehicleYear = vehicle.VehicleYear;
            currentVehicle.ImageFileName = vehicle.ImageFileName;
            currentVehicle.VehicleDescription = vehicle.VehicleDescription;

            return currentVehicle;
        }

        public IEnumerable<VehicleMakes> GetVehicleMakes()
        {
            List<VehicleMakes> make = new List<VehicleMakes>();
            foreach (VehicleMakes makes in vehicleMakesList)
            {
                VehicleMakes addMake = new VehicleMakes();
                addMake.MakeName = makes.MakeName;
                addMake.MakeId = makes.MakeId;
                addMake.UserEmail = makes.UserEmail;
                addMake.CreatedDate = makes.CreatedDate;

                make.Add(addMake);
            };

            return make;
           
        }

        public IEnumerable<VehicleModels> GetVehicleModels()
        {
            List<VehicleModels> models = new List<VehicleModels>();

            foreach (VehicleModels model in vehicleModelsList)
            {
                VehicleModels addModel = new VehicleModels();
                addModel.ModelTypeName = model.ModelTypeName;
                addModel.ModelId = model.ModelId;
                addModel.MakeId = model.MakeId;
                addModel.MakeTypeName = (from Vehicle in vehicleModelsList
                                         join VehicleMakes in vehicleMakesList
                                         on addModel.MakeId equals VehicleMakes.MakeId
                                         where addModel.MakeId == VehicleMakes.MakeId
                                         select VehicleMakes.MakeName).ToList().FirstOrDefault();
                addModel.UserEmail = model.UserEmail;
                addModel.CreatedDate = model.CreatedDate;

                models.Add(addModel);
            };

            return models;
        }

        public IEnumerable<VehicleType> GetVehicleType()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();

            foreach (VehicleType vehicleType in vehicleTypesList)
            {
                VehicleType addVehicleType = new VehicleType();
                addVehicleType.TypeName = vehicleType.TypeName;
                addVehicleType.TypeId = vehicleType.TypeId;

                vehicleTypes.Add(addVehicleType);
            };

            return vehicleTypes;
        }

        public void InsertMake(VehicleMakes make)
        {
            if (vehicleMakesList.Any())
                make.MakeId = vehicleMakesList.Max(x => x.MakeId) + 1;
            else
                make.MakeId = 1;
            make.CreatedDate = DateTime.Now;

            vehicleMakesList.Add(make);
        }

        public void InsertModel(VehicleModels model)
        {
            if (vehicleModelsList.Any())
                model.ModelId = vehicleModelsList.Max(x => x.ModelId) + 1;
            else
                model.ModelId = 1;
            model.CreatedDate = DateTime.Now;

            vehicleModelsList.Add(model);
        }

        public void InsertVehicle(Vehicle vehicle)
        {
            if (vehicleList.Any())
                vehicle.VehicleId = vehicleList.Max(x => x.VehicleId) + 1;
            else
                vehicle.VehicleId = 1;
            
            vehicleList.Add(vehicle);
        }

        public IEnumerable<VehicleDetails> SearchNewVehicles(VehicleSearchParameters parameters)
        {
            IList<VehicleDetails> newVehiclesList = new List<VehicleDetails>();
            IList<VehicleDetails> newVehiclesSearch = new List<VehicleDetails>();

            if (parameters.SearchTerm == null)
            {
                parameters.SearchTerm = "";
            }

            foreach (var vehicle in vehicleList)
            {
                var currentVehicle = new VehicleDetails();

                if (vehicle.TypeId == 1)
                {
                    currentVehicle.VehicleId = vehicle.VehicleId;
                    currentVehicle.MakeName = (from Vehicle in vehicleList
                                              join VehicleMakes in vehicleMakesList
                                              on vehicle.MakeId equals VehicleMakes.MakeId
                                              where vehicle.MakeId == VehicleMakes.MakeId
                                              select VehicleMakes.MakeName).ToList().FirstOrDefault();
                    currentVehicle.ModelTypeName = (from Vehicle in vehicleList
                                               join VehicleModels in vehicleModelsList
                                               on vehicle.ModelId equals VehicleModels.ModelId
                                               where vehicle.ModelId == VehicleModels.ModelId
                                               select VehicleModels.ModelTypeName).ToList().FirstOrDefault();
                    currentVehicle.BodyStyleName = (from Vehicle in vehicleList
                                                    join BodyStyles in bodyStylesList
                                                    on vehicle.BodyStyleId equals BodyStyles.BodyStyleId
                                                    where vehicle.BodyStyleId == BodyStyles.BodyStyleId
                                                    select BodyStyles.BodyStyleName).ToList().FirstOrDefault();
                    currentVehicle.TransmissionTypeName = (from Vehicle in vehicleList
                                                    join Transmission in transmissionList
                                                    on vehicle.TransmissionId equals Transmission.TransmissionId
                                                    where vehicle.TransmissionId == Transmission.TransmissionId
                                                    select Transmission.TransmissionTypeName).ToList().FirstOrDefault();
                    currentVehicle.ExteriorColorName = (from Vehicle in vehicleList
                                                           join ExteriorColor in exteriorColorsList
                                                           on vehicle.ExteriorColorId equals ExteriorColor.ExteriorColorId
                                                           where vehicle.ExteriorColorId == ExteriorColor.ExteriorColorId
                                                           select ExteriorColor.ExteriorColorName).ToList().FirstOrDefault();
                    currentVehicle.InteriorColorName = (from Vehicle in vehicleList
                                                        join InteriorColor in interiorColorsList
                                                        on vehicle.InteriorColorId equals InteriorColor.InteriorColorId
                                                        where vehicle.InteriorColorId == InteriorColor.InteriorColorId
                                                        select InteriorColor.InteriorColorName).ToList().FirstOrDefault();
                    currentVehicle.Mileage = vehicle.Mileage;
                    currentVehicle.VIN = vehicle.VIN;
                    currentVehicle.MSRP = vehicle.MSRP;
                    currentVehicle.SalePrice = vehicle.SalePrice;
                    currentVehicle.VehicleYear = vehicle.VehicleYear;
                    currentVehicle.ImageFileName = vehicle.ImageFileName;
                    currentVehicle.VehicleDescription = vehicle.VehicleDescription;

                    newVehiclesList.Add(currentVehicle);
                }       
            }
            foreach (var vehicle in newVehiclesList)
            {
                var currentVehicle = new VehicleDetails();
                if (vehicle.SalePrice >= parameters.MinPrice && vehicle.SalePrice <= parameters.MaxPrice)
                {
                    if (vehicle.VehicleYear >= Int32.Parse(parameters.MinYear) && vehicle.VehicleYear <= Int32.Parse(parameters.MaxYear))
                    {
                        if (vehicle.MakeName.Contains(parameters.SearchTerm) || vehicle.ModelTypeName.Contains(parameters.SearchTerm) || vehicle.VehicleYear.ToString().Contains(parameters.SearchTerm))
                        {
                            currentVehicle.VehicleId = vehicle.VehicleId;
                            currentVehicle.MakeName = vehicle.MakeName;
                            currentVehicle.ModelTypeName = vehicle.ModelTypeName;
                            currentVehicle.BodyStyleName = vehicle.BodyStyleName;
                            currentVehicle.TransmissionTypeName = vehicle.TransmissionTypeName;
                            currentVehicle.ExteriorColorName = vehicle.ExteriorColorName;
                            currentVehicle.InteriorColorName = vehicle.InteriorColorName;
                            currentVehicle.Mileage = vehicle.Mileage;
                            currentVehicle.VIN = vehicle.VIN;
                            currentVehicle.MSRP = vehicle.MSRP;
                            currentVehicle.SalePrice = vehicle.SalePrice;
                            currentVehicle.VehicleYear = vehicle.VehicleYear;
                            currentVehicle.ImageFileName = vehicle.ImageFileName;
                            currentVehicle.VehicleDescription = vehicle.VehicleDescription;

                            newVehiclesSearch.Add(currentVehicle);
                        }
                    }
                }

            }

            return newVehiclesSearch;
        }

        public IEnumerable<SalesReport> SearchSalesReport(SalesReportSearchParameters parameters)
        {
            IList<SalesReport> searchPurchaseVehicleList = new List<SalesReport>();
            List<UserDetails> userDetails = UserRepositoryFactory.GetRepository().GetAll();

            if (parameters.UserId == null)
            {
                parameters.UserId = "";
            }
            if (parameters.FromDate == null)
            {
                parameters.FromDate = DateTime.MinValue;
            }
            if (parameters.ToDate == null)
            {
                parameters.ToDate = DateTime.MaxValue;
            }

            if (parameters.UserId != "")
            {
                IList<PurchaseVehicle> purchasedVehicles = purchaseVehicleList.Where(x => x.UserId == parameters.UserId).Where(x => x.PurchaseDate > parameters.FromDate).Where(x => x.PurchaseDate < parameters.ToDate).ToList();
                
                UserDetails user = userDetails.FirstOrDefault(x => x.Id == parameters.UserId);
                SalesReport sale = new SalesReport()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        TotalVehicles = purchasedVehicles.Count(x => x.UserId == user.Id),
                        TotalSales = purchasedVehicles.Where(x => x.UserId == user.Id).Sum(x => x.PurchasePrice)
                    };
                    searchPurchaseVehicleList.Add(sale);
            }
            else
            {
                IList<PurchaseVehicle> purchasedVehicles = purchaseVehicleList.Where(x => x.PurchaseDate > parameters.FromDate).Where(x => x.PurchaseDate < parameters.ToDate).ToList();

                foreach (UserDetails user in userDetails)
                {   
                    SalesReport sale = new SalesReport()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        TotalVehicles = purchasedVehicles.Count(x => x.UserId == user.Id),
                        TotalSales = purchasedVehicles.Where(x => x.UserId == user.Id).Sum(x => x.PurchasePrice)
                    };

                    searchPurchaseVehicleList.Add(sale);
                }
            }
            return searchPurchaseVehicleList;       
        }

        public IEnumerable<VehicleDetails> SearchSalesVehicles(VehicleSearchParameters parameters)
        {
            IList<VehicleDetails> salesVehiclesList = new List<VehicleDetails>();
            IList<VehicleDetails> salesVehiclesSearch = new List<VehicleDetails>();

            if (parameters.SearchTerm == null)
            {
                parameters.SearchTerm = "";
            }

            foreach (var vehicle in vehicleList)
            {
                var currentVehicle = new VehicleDetails();

                if (vehicle.TypeId == 1 || vehicle.TypeId == 2)
                {
                    currentVehicle.VehicleId = vehicle.VehicleId;
                    currentVehicle.MakeName = (from Vehicle in vehicleList
                                               join VehicleMakes in vehicleMakesList
                                               on vehicle.MakeId equals VehicleMakes.MakeId
                                               where vehicle.MakeId == VehicleMakes.MakeId
                                               select VehicleMakes.MakeName).ToList().FirstOrDefault();
                    currentVehicle.ModelTypeName = (from Vehicle in vehicleList
                                                    join VehicleModels in vehicleModelsList
                                                    on vehicle.ModelId equals VehicleModels.ModelId
                                                    where vehicle.ModelId == VehicleModels.ModelId
                                                    select VehicleModels.ModelTypeName).ToList().FirstOrDefault();
                    currentVehicle.BodyStyleName = (from Vehicle in vehicleList
                                                    join BodyStyles in bodyStylesList
                                                    on vehicle.BodyStyleId equals BodyStyles.BodyStyleId
                                                    where vehicle.BodyStyleId == BodyStyles.BodyStyleId
                                                    select BodyStyles.BodyStyleName).ToList().FirstOrDefault();
                    currentVehicle.TransmissionTypeName = (from Vehicle in vehicleList
                                                           join Transmission in transmissionList
                                                           on vehicle.TransmissionId equals Transmission.TransmissionId
                                                           where vehicle.TransmissionId == Transmission.TransmissionId
                                                           select Transmission.TransmissionTypeName).ToList().FirstOrDefault();
                    currentVehicle.ExteriorColorName = (from Vehicle in vehicleList
                                                        join ExteriorColor in exteriorColorsList
                                                        on vehicle.ExteriorColorId equals ExteriorColor.ExteriorColorId
                                                        where vehicle.ExteriorColorId == ExteriorColor.ExteriorColorId
                                                        select ExteriorColor.ExteriorColorName).ToList().FirstOrDefault();
                    currentVehicle.InteriorColorName = (from Vehicle in vehicleList
                                                        join InteriorColor in interiorColorsList
                                                        on vehicle.InteriorColorId equals InteriorColor.InteriorColorId
                                                        where vehicle.InteriorColorId == InteriorColor.InteriorColorId
                                                        select InteriorColor.InteriorColorName).ToList().FirstOrDefault();
                    currentVehicle.Mileage = vehicle.Mileage;
                    currentVehicle.VIN = vehicle.VIN;
                    currentVehicle.MSRP = vehicle.MSRP;
                    currentVehicle.SalePrice = vehicle.SalePrice;
                    currentVehicle.VehicleYear = vehicle.VehicleYear;
                    currentVehicle.ImageFileName = vehicle.ImageFileName;
                    currentVehicle.VehicleDescription = vehicle.VehicleDescription;

                    salesVehiclesList.Add(currentVehicle);
                }
            }
            foreach (var vehicle in salesVehiclesList)
            {
                var currentVehicle = new VehicleDetails();
                if (vehicle.SalePrice >= parameters.MinPrice && vehicle.SalePrice <= parameters.MaxPrice)
                {
                    if (vehicle.VehicleYear >= Int32.Parse(parameters.MinYear) && vehicle.VehicleYear <= Int32.Parse(parameters.MaxYear))
                    {
                        if (vehicle.MakeName.Contains(parameters.SearchTerm) || vehicle.ModelTypeName.Contains(parameters.SearchTerm) || vehicle.VehicleYear.ToString().Contains(parameters.SearchTerm))
                        {
                            currentVehicle.VehicleId = vehicle.VehicleId;
                            currentVehicle.MakeName = vehicle.MakeName;
                            currentVehicle.ModelTypeName = vehicle.ModelTypeName;
                            currentVehicle.BodyStyleName = vehicle.BodyStyleName;
                            currentVehicle.TransmissionTypeName = vehicle.TransmissionTypeName;
                            currentVehicle.ExteriorColorName = vehicle.ExteriorColorName;
                            currentVehicle.InteriorColorName = vehicle.InteriorColorName;
                            currentVehicle.Mileage = vehicle.Mileage;
                            currentVehicle.VIN = vehicle.VIN;
                            currentVehicle.MSRP = vehicle.MSRP;
                            currentVehicle.SalePrice = vehicle.SalePrice;
                            currentVehicle.VehicleYear = vehicle.VehicleYear;
                            currentVehicle.ImageFileName = vehicle.ImageFileName;
                            currentVehicle.VehicleDescription = vehicle.VehicleDescription;

                            salesVehiclesSearch.Add(currentVehicle);
                        }
                    }
                }

            }

            return salesVehiclesSearch;
        }

        public IEnumerable<VehicleDetails> SearchUsedVehicles(VehicleSearchParameters parameters)
        {
            IList<VehicleDetails> usedVehiclesList = new List<VehicleDetails>();
            IList<VehicleDetails> usedVehiclesSearch = new List<VehicleDetails>();

            if (parameters.SearchTerm == null)
            {
                parameters.SearchTerm = "";
            }

            foreach (var vehicle in vehicleList)
            {
                var currentVehicle = new VehicleDetails();

                if (vehicle.TypeId == 2)
                {
                    currentVehicle.VehicleId = vehicle.VehicleId;
                    currentVehicle.MakeName = (from Vehicle in vehicleList
                                               join VehicleMakes in vehicleMakesList
                                               on vehicle.MakeId equals VehicleMakes.MakeId
                                               where vehicle.MakeId == VehicleMakes.MakeId
                                               select VehicleMakes.MakeName).ToList().FirstOrDefault();
                    currentVehicle.ModelTypeName = (from Vehicle in vehicleList
                                                    join VehicleModels in vehicleModelsList
                                                    on vehicle.ModelId equals VehicleModels.ModelId
                                                    where vehicle.ModelId == VehicleModels.ModelId
                                                    select VehicleModels.ModelTypeName).ToList().FirstOrDefault();
                    currentVehicle.BodyStyleName = (from Vehicle in vehicleList
                                                    join BodyStyles in bodyStylesList
                                                    on vehicle.BodyStyleId equals BodyStyles.BodyStyleId
                                                    where vehicle.BodyStyleId == BodyStyles.BodyStyleId
                                                    select BodyStyles.BodyStyleName).ToList().FirstOrDefault();
                    currentVehicle.TransmissionTypeName = (from Vehicle in vehicleList
                                                           join Transmission in transmissionList
                                                           on vehicle.TransmissionId equals Transmission.TransmissionId
                                                           where vehicle.TransmissionId == Transmission.TransmissionId
                                                           select Transmission.TransmissionTypeName).ToList().FirstOrDefault();
                    currentVehicle.ExteriorColorName = (from Vehicle in vehicleList
                                                        join ExteriorColor in exteriorColorsList
                                                        on vehicle.ExteriorColorId equals ExteriorColor.ExteriorColorId
                                                        where vehicle.ExteriorColorId == ExteriorColor.ExteriorColorId
                                                        select ExteriorColor.ExteriorColorName).ToList().FirstOrDefault();
                    currentVehicle.InteriorColorName = (from Vehicle in vehicleList
                                                        join InteriorColor in interiorColorsList
                                                        on vehicle.InteriorColorId equals InteriorColor.InteriorColorId
                                                        where vehicle.InteriorColorId == InteriorColor.InteriorColorId
                                                        select InteriorColor.InteriorColorName).ToList().FirstOrDefault();
                    currentVehicle.Mileage = vehicle.Mileage;
                    currentVehicle.VIN = vehicle.VIN;
                    currentVehicle.MSRP = vehicle.MSRP;
                    currentVehicle.SalePrice = vehicle.SalePrice;
                    currentVehicle.VehicleYear = vehicle.VehicleYear;
                    currentVehicle.ImageFileName = vehicle.ImageFileName;
                    currentVehicle.VehicleDescription = vehicle.VehicleDescription;

                    usedVehiclesList.Add(currentVehicle);
                }
            }
            foreach (var vehicle in usedVehiclesList)
            {
                var currentVehicle = new VehicleDetails();
                if (vehicle.SalePrice >= parameters.MinPrice && vehicle.SalePrice <= parameters.MaxPrice)
                {
                    if (vehicle.VehicleYear >= Int32.Parse(parameters.MinYear) && vehicle.VehicleYear <= Int32.Parse(parameters.MaxYear))
                    {
                        if (vehicle.MakeName.Contains(parameters.SearchTerm) || vehicle.ModelTypeName.Contains(parameters.SearchTerm) || vehicle.VehicleYear.ToString().Contains(parameters.SearchTerm))
                        {
                            currentVehicle.VehicleId = vehicle.VehicleId;
                            currentVehicle.MakeName = vehicle.MakeName;
                            currentVehicle.ModelTypeName = vehicle.ModelTypeName;
                            currentVehicle.BodyStyleName = vehicle.BodyStyleName;
                            currentVehicle.TransmissionTypeName = vehicle.TransmissionTypeName;
                            currentVehicle.ExteriorColorName = vehicle.ExteriorColorName;
                            currentVehicle.InteriorColorName = vehicle.InteriorColorName;
                            currentVehicle.Mileage = vehicle.Mileage;
                            currentVehicle.VIN = vehicle.VIN;
                            currentVehicle.MSRP = vehicle.MSRP;
                            currentVehicle.SalePrice = vehicle.SalePrice;
                            currentVehicle.VehicleYear = vehicle.VehicleYear;
                            currentVehicle.ImageFileName = vehicle.ImageFileName;
                            currentVehicle.VehicleDescription = vehicle.VehicleDescription;

                            usedVehiclesSearch.Add(currentVehicle);
                        }
                    }
                }

            }

            return usedVehiclesSearch;
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            Vehicle currentVehicle = vehicleList.FirstOrDefault(x => x.VehicleId == vehicle.VehicleId);
            
            currentVehicle.VehicleId = vehicle.VehicleId;
            currentVehicle.MakeId = vehicle.MakeId;
            currentVehicle.ModelId = vehicle.ModelId;
            currentVehicle.BodyStyleId = vehicle.BodyStyleId;
            currentVehicle.TransmissionId = vehicle.TransmissionId;
            currentVehicle.ExteriorColorId = vehicle.ExteriorColorId;
            currentVehicle.InteriorColorId = vehicle.InteriorColorId;
            currentVehicle.Mileage = vehicle.Mileage;
            currentVehicle.VIN = vehicle.VIN;
            currentVehicle.MSRP = vehicle.MSRP;
            currentVehicle.SalePrice = vehicle.SalePrice;
            currentVehicle.VehicleYear = vehicle.VehicleYear;
            currentVehicle.ImageFileName = vehicle.ImageFileName;
            currentVehicle.VehicleDescription = vehicle.VehicleDescription;
            currentVehicle.HasFeatured = vehicle.HasFeatured;

            Vehicle deleteVehicle = vehicleList.FirstOrDefault(x => x.VehicleId == vehicle.VehicleId);
            vehicleList.Remove(deleteVehicle);

            vehicleList.Add(currentVehicle);
        }
    }
}







