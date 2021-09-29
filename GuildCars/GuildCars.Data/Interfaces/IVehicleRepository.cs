using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using Vehicle = GuildCars.Models.Tables.Vehicle;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleRepository
    {
        IEnumerable<FeaturedVehicleShortItem> GetFeatured();
        IEnumerable<VehicleDetails> GetNewVehicles();
        IEnumerable<VehicleDetails> SearchNewVehicles(VehicleSearchParameters parameters);
        IEnumerable<VehicleDetails> GetUsedVehicles();
        IEnumerable<VehicleDetails> SearchUsedVehicles(VehicleSearchParameters parameters);
        IEnumerable<VehicleDetails> GetSalesVehicles();
        IEnumerable<VehicleDetails> SearchSalesVehicles(VehicleSearchParameters parameters);
        VehicleDetails GetDetails(int vehicleId);
        void AddPurchaseVehicle(PurchaseVehicle addPurchaseVehicle);
        void DeleteVehicle(int vehicleId, string imageFileName);
        void InsertVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        IEnumerable<VehicleMakes> GetVehicleMakes();
        void InsertMake(VehicleMakes make);
        IEnumerable<VehicleModels> GetVehicleModels();
        void InsertModel(VehicleModels model);
        List<VehicleInventory> GetUsedVehicleInventory();
        List<VehicleInventory> GetNewVehicleInventory();
        IEnumerable<SalesReport> GetSalesReport();
        IEnumerable<SalesReport> SearchSalesReport(SalesReportSearchParameters parameters);
        List<PurchaseTypes> GetAllPurchaseTypes();
        List<States> GetAllStates();
        IEnumerable<VehicleType> GetVehicleType();
        IEnumerable<BodyStyles> GetBodyStyle();
        IEnumerable<Transmission> GetTransmission();
        IEnumerable<ExteriorColor> GetExteriorColor();
        IEnumerable<InteriorColor> GetInteriorColor();
        List<VehicleModels> GetModelMakes(int id);
        Vehicle GetVehicleById(int vehicleId);
        

    }
}
