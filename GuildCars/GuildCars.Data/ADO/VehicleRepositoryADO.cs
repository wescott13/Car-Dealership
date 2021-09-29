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
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.Models.UIViews;

namespace GuildCars.Data.ADO
{
    public class VehicleRepositoryADO : IVehicleRepository
    {
        public VehicleDetails GetDetails(int vehicleId)
        {
            VehicleDetails vehicleDetails = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {

                        vehicleDetails = new VehicleDetails();
                        vehicleDetails.VehicleId = (int)dr["VehicleId"];
                        vehicleDetails.MakeName = dr["MakeName"].ToString();
                        vehicleDetails.ModelTypeName = dr["ModelTypeName"].ToString();
                        vehicleDetails.BodyStyleName = dr["BodyStyleName"].ToString();
                        vehicleDetails.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        vehicleDetails.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        vehicleDetails.InteriorColorName = dr["InteriorColorName"].ToString();
                        vehicleDetails.Mileage = (int)dr["Mileage"];
                        vehicleDetails.VIN = dr["VIN"].ToString();
                        vehicleDetails.MSRP = (decimal)dr["MSRP"];
                        vehicleDetails.SalePrice = (decimal)dr["SalePrice"];
                        vehicleDetails.VehicleYear = (int)dr["VehicleYear"];

                        if (dr["VehicleDescription"] != DBNull.Value)
                            vehicleDetails.VehicleDescription = dr["VehicleDescription"].ToString();

                        if (dr["ImageFileName"] != DBNull.Value)
                            vehicleDetails.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return vehicleDetails;
        }

        public IEnumerable<FeaturedVehicleShortItem> GetFeatured()
        {
            List<FeaturedVehicleShortItem> featuredVehicleShortItems = new List<FeaturedVehicleShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FeaturedVehicles", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FeaturedVehicleShortItem currentRow = new FeaturedVehicleShortItem();
                        currentRow.vehicleId = (int)dr["VehicleId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        featuredVehicleShortItems.Add(currentRow);

                    }
                }
            }

            return featuredVehicleShortItems;
        }

        public IEnumerable<VehicleDetails> GetNewVehicles()
        {
            List<VehicleDetails> newVehicles = new List<VehicleDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("NewInventorySelect20", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.VIN = dr["VIN"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        newVehicles.Add(currentRow);

                    }
                }
            }

            return newVehicles;
        }

        public IEnumerable<VehicleDetails> GetUsedVehicles()
        {
            List<VehicleDetails> usedVehicles = new List<VehicleDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UsedInventorySelect20", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.VIN = dr["VIN"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        usedVehicles.Add(currentRow);

                    }
                }
            }

            return usedVehicles;
        }

        public IEnumerable<VehicleDetails> GetSalesVehicles()
        {
            List<VehicleDetails> vehicles = new List<VehicleDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleInventorySelect20", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.VIN = dr["VIN"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        vehicles.Add(currentRow);

                    }
                }
            }

            return vehicles;
        }

        public IEnumerable<VehicleDetails> SearchNewVehicles(VehicleSearchParameters parameters)
        {
            List<VehicleDetails> newVehicles = new List<VehicleDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select TOP 20 VehicleId, MakeName, ModelTypeName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, VIN, MSRP, SalePrice, VehicleYear, ImageFileName from Vehicle " +
                        "INNER JOIN VehicleMakes ON Vehicle.MakeId = VehicleMakes.MakeId " +
                        "LEFT JOIN VehicleModels ON Vehicle.ModelId = VehicleModels.ModelId " +
                        "LEFT JOIN BodyStyles ON Vehicle.BodyStyleId = BodyStyles.BodyStyleId " +
                        "LEFT JOIN Transmission ON Vehicle.TransmissionId = Transmission.TransmissionId " +
                        "LEFT JOIN ExteriorColor ON Vehicle.ExteriorColorId = ExteriorColor.ExteriorColorId " +
                        "LEFT JOIN InteriorColor ON Vehicle.InteriorColorId = InteriorColor.InteriorColorId " +
                        "WHERE TypeID = 1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND (SalePrice >= @MinPrice) ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND (SalePrice <= @MaxPrice) ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (!string.IsNullOrEmpty(parameters.MinYear))
                {
                    query += "AND (VehicleYear >= @MinYear) ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (!string.IsNullOrEmpty(parameters.MaxYear))
                {
                    query += "AND (VehicleYear <= @MaxYear) ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (!string.IsNullOrEmpty(parameters.SearchTerm))
                {
                    query += "AND (CAST (VehicleYear AS CHAR) LIKE @VehicleYear OR ModelTypeName LIKE @ModelTypeName OR MakeName LIKE @MakeName)";
                    cmd.Parameters.AddWithValue("@VehicleYear", parameters.SearchTerm + '%');
                    cmd.Parameters.AddWithValue("@ModelTypeName", parameters.SearchTerm + '%');
                    cmd.Parameters.AddWithValue("@MakeName", parameters.SearchTerm + '%');

                }

                query += " ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();

                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.VIN = dr["VIN"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        newVehicles.Add(currentRow);
                    }
                }
            }

            return newVehicles;
        }

        public IEnumerable<VehicleDetails> SearchUsedVehicles(VehicleSearchParameters parameters)
        {
            List<VehicleDetails> usedVehicles = new List<VehicleDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select TOP 20 VehicleId, MakeName, ModelTypeName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, VIN, MSRP, SalePrice, VehicleYear, ImageFileName from Vehicle " +
                        "INNER JOIN VehicleMakes ON Vehicle.MakeId = VehicleMakes.MakeId " +
                        "LEFT JOIN VehicleModels ON Vehicle.ModelId = VehicleModels.ModelId " +
                        "LEFT JOIN BodyStyles ON Vehicle.BodyStyleId = BodyStyles.BodyStyleId " +
                        "LEFT JOIN Transmission ON Vehicle.TransmissionId = Transmission.TransmissionId " +
                        "LEFT JOIN ExteriorColor ON Vehicle.ExteriorColorId = ExteriorColor.ExteriorColorId " +
                        "LEFT JOIN InteriorColor ON Vehicle.InteriorColorId = InteriorColor.InteriorColorId " +
                        "WHERE TypeID = 2 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND (SalePrice >= @MinPrice) ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND (SalePrice <= @MaxPrice) ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

               if (!string.IsNullOrEmpty(parameters.MinYear))
                {
                    query += "AND (VehicleYear >= @MinYear) ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (!string.IsNullOrEmpty(parameters.MaxYear))
                {
                    query += "AND (VehicleYear <= @MaxYear) ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (!string.IsNullOrEmpty(parameters.SearchTerm))
                {
                    query += "AND (CAST (VehicleYear AS CHAR) LIKE @VehicleYear OR ModelTypeName LIKE @ModelTypeName OR MakeName LIKE @MakeName)";
                    cmd.Parameters.AddWithValue("@VehicleYear", parameters.SearchTerm + '%');
                    cmd.Parameters.AddWithValue("@ModelTypeName", parameters.SearchTerm + '%');
                    cmd.Parameters.AddWithValue("@MakeName", parameters.SearchTerm + '%');

                }

                query += " ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();

                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.VIN = dr["VIN"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        usedVehicles.Add(currentRow);
                    }
                }
            }

            return usedVehicles;
        }

        public IEnumerable<VehicleDetails> SearchSalesVehicles(VehicleSearchParameters parameters)
        {
            List<VehicleDetails> vehicles = new List<VehicleDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select TOP 20 VehicleId, MakeName, ModelTypeName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, VIN, MSRP, SalePrice, VehicleYear, ImageFileName from Vehicle " +
                        "INNER JOIN VehicleMakes ON Vehicle.MakeId = VehicleMakes.MakeId " +
                        "LEFT JOIN VehicleModels ON Vehicle.ModelId = VehicleModels.ModelId " +
                        "LEFT JOIN BodyStyles ON Vehicle.BodyStyleId = BodyStyles.BodyStyleId " +
                        "LEFT JOIN Transmission ON Vehicle.TransmissionId = Transmission.TransmissionId " +
                        "LEFT JOIN ExteriorColor ON Vehicle.ExteriorColorId = ExteriorColor.ExteriorColorId " +
                        "LEFT JOIN InteriorColor ON Vehicle.InteriorColorId = InteriorColor.InteriorColorId " +
                        "WHERE (TypeID = 1 OR TypeID = 2) ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND (SalePrice >= @MinPrice) ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND (SalePrice <= @MaxPrice) ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (!string.IsNullOrEmpty(parameters.MinYear))
                {
                    query += "AND (VehicleYear >= @MinYear) ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear);
                }

                if (!string.IsNullOrEmpty(parameters.MaxYear))
                {
                    query += "AND (VehicleYear <= @MaxYear) ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear);
                }

                if (!string.IsNullOrEmpty(parameters.SearchTerm))
                {
                    query += "AND (CAST (VehicleYear AS CHAR) LIKE @VehicleYear OR ModelTypeName LIKE @ModelTypeName OR MakeName LIKE @MakeName)";
                    cmd.Parameters.AddWithValue("@VehicleYear", parameters.SearchTerm + '%');
                    cmd.Parameters.AddWithValue("@ModelTypeName", parameters.SearchTerm + '%');
                    cmd.Parameters.AddWithValue("@MakeName", parameters.SearchTerm + '%');

                }

                query += " ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();

                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.VIN = dr["VIN"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }

            return vehicles;
        }

        public void AddPurchaseVehicle(PurchaseVehicle addPurchaseVehicle)
        {

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSale", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@PurchaseId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@UserId", addPurchaseVehicle.UserId);
                cmd.Parameters.AddWithValue("@PurchaseName", addPurchaseVehicle.PurchaseName);
                if (addPurchaseVehicle.Email != null)
                {
                    cmd.Parameters.AddWithValue("@Email", addPurchaseVehicle.Email);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                if (addPurchaseVehicle.PhoneNumber != null)
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", addPurchaseVehicle.PhoneNumber);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                }
                
                cmd.Parameters.AddWithValue("@Street1", addPurchaseVehicle.Street1);
                if (addPurchaseVehicle.Street2 != null)
                {
                    cmd.Parameters.AddWithValue("@Street2", addPurchaseVehicle.PhoneNumber);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Street2", DBNull.Value);
                }
                
                cmd.Parameters.AddWithValue("@City", addPurchaseVehicle.City);
                cmd.Parameters.AddWithValue("@StateId", addPurchaseVehicle.StateId);
                cmd.Parameters.AddWithValue("@Zipcode", addPurchaseVehicle.ZipCode);
                cmd.Parameters.AddWithValue("@PurchasePrice", addPurchaseVehicle.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseTypeId", addPurchaseVehicle.PurchaseTypeId);

                cn.Open();

                cmd.ExecuteNonQuery();

                addPurchaseVehicle.PurchaseId = (int)param.Value;
            }
        }

        public void DeleteVehicle(int vehicleId, string imageFileName)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            string dirImages = @"D:\Repos\Projects\GuildCars\GuildCars.UI\Images\" + imageFileName;
            File.Delete(dirImages);
            
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                
                SqlCommand cmd = new SqlCommand("VehicleDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void InsertVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@VehicleId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeId", vehicle.MakeId);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@TypeId", vehicle.TypeId);
                
                if (vehicle.BodyStyleId != null)
                {
                    cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyleId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BodyStyleId", DBNull.Value);
                }

                if (vehicle.TransmissionId != null)
                {
                    cmd.Parameters.AddWithValue("@TransmissionId", vehicle.TransmissionId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TransmissionId", DBNull.Value);
                }

                if (vehicle.ExteriorColorId != null)
                {
                    cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColorId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExteriorColorId", DBNull.Value);
                }
                
                if (vehicle.InteriorColorId != null)
                {
                    cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColorId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InteriorColorId", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@VIN", vehicle.VIN);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@VehicleDescription", vehicle.VehicleDescription);
                cmd.Parameters.AddWithValue("@VehicleYear", vehicle.VehicleYear);
                cmd.Parameters.AddWithValue("@HasFeatured", vehicle.HasFeatured);
                cmd.Parameters.AddWithValue("@ImageFileName", vehicle.ImageFileName);

                cn.Open();

                cmd.ExecuteNonQuery();

                vehicle.VehicleId = (int)param.Value;
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@MakeId", vehicle.MakeId);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@TypeId", vehicle.TypeId);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@TransmissionId", vehicle.TransmissionId);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColorId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColorId);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@VIN", vehicle.VIN);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@VehicleDescription", vehicle.VehicleDescription);
                cmd.Parameters.AddWithValue("@VehicleYear", vehicle.VehicleYear);
                cmd.Parameters.AddWithValue("@HasFeatured", vehicle.HasFeatured);
                cmd.Parameters.AddWithValue("@ImageFileName", vehicle.ImageFileName);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<VehicleMakes> GetVehicleMakes()
        {
            List<VehicleMakes> makes = new List<VehicleMakes>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleMakes currentRow = new VehicleMakes();
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.CreatedDate = (DateTime)dr["CreatedDate"];
                        currentRow.UserEmail = dr["UserEmail"].ToString();

                        makes.Add(currentRow);
                    }
                }
            }

            return makes;
        }
        
        public void InsertMake(VehicleMakes make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);
                cmd.Parameters.AddWithValue("@UserEmail", make.UserEmail);
                
                cn.Open();

                cmd.ExecuteNonQuery();

                make.MakeId = (int)param.Value;
            }
        }

        public IEnumerable<VehicleModels> GetVehicleModels()
        {
            List<VehicleModels> models = new List<VehicleModels>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleModels currentRow = new VehicleModels();
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.CreatedDate = (DateTime)dr["CreatedDate"];
                        currentRow.UserEmail = dr["UserEmail"].ToString();
                        currentRow.MakeTypeName = dr["MakeName"].ToString();

                        models.Add(currentRow);
                    }
                }
            }

            return models;
        }

        public void InsertModel(VehicleModels model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeId", model.MakeId);
                cmd.Parameters.AddWithValue("@ModelTypeName", model.ModelTypeName);
                cmd.Parameters.AddWithValue("@UserEmail", model.UserEmail);

                cn.Open();

                cmd.ExecuteNonQuery();

                model.ModelId = (int)param.Value;
            }
        }

        public List<VehicleInventory> GetNewVehicleInventory()
        {
            List<VehicleInventory> newVehicleInventory = new List<VehicleInventory>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("newVehicleInventory", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleInventory currentRow = new VehicleInventory();
                        currentRow.VehicleCount = (int)dr["vehicleCount"];
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.StockValue = (decimal)dr["StockValue"];

                        newVehicleInventory.Add(currentRow);

                    }
                }
            }

            return newVehicleInventory;
        }

        public List<VehicleInventory> GetUsedVehicleInventory()
        {
            List<VehicleInventory> usedVehicleInventory = new List<VehicleInventory>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("usedVehicleInventory", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleInventory currentRow = new VehicleInventory();
                        currentRow.VehicleCount = (int)dr["vehicleCount"];
                        currentRow.VehicleYear = (int)dr["VehicleYear"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelTypeName = dr["ModelTypeName"].ToString();
                        currentRow.StockValue = (decimal)dr["StockValue"];

                        usedVehicleInventory.Add(currentRow);

                    }
                }
            }

            return usedVehicleInventory;
        }

        public IEnumerable<SalesReport> GetSalesReport()
        {
            List<SalesReport> report = new List<SalesReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select FirstName AS FirstName, LastName AS LastName, UserName AS UserName, SUM(PurchasePrice) AS TotalSales, COUNT(PurchaseId) AS TotalVehicles from PurchaseVehicle " +
                        "INNER JOIN AspNetUsers ON UserId = Id " +
                        "Group by UserName, LastName, FirstName ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReport currentRow = new SalesReport();

                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.TotalSales = (decimal)dr["TotalSales"];
                        currentRow.TotalVehicles = (int)dr["TotalVehicles"];

                        report.Add(currentRow);
                    }
                }
            }

            return report;
        }

        public IEnumerable<SalesReport> SearchSalesReport(SalesReportSearchParameters parameters)
        {
            List<SalesReport> report = new List<SalesReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select FirstName AS FirstName, LastName AS LastName, UserName AS UserName, SUM(PurchasePrice) AS TotalSales, COUNT(PurchaseId) AS TotalVehicles from PurchaseVehicle " +
                        "INNER JOIN AspNetUsers ON UserId = Id " +
                        "WHERE 1 = 1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!string.IsNullOrEmpty(parameters.UserId))
                {
                    query += "AND UserId = @UserId ";
                    cmd.Parameters.AddWithValue("@UserId", parameters.UserId);
                }

                if (parameters.FromDate.HasValue)
                {
                    query += "AND PurchaseDate >= @FromDate ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate.Value);
                }

                if (parameters.ToDate.HasValue)
                {
                    query += "AND PurchaseDate <= @ToDate ";
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate.Value);
                }

                query += "Group by UserName, LastName, FirstName ";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReport currentRow = new SalesReport();

                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.TotalSales = (decimal)dr["TotalSales"];
                        currentRow.TotalVehicles = (int)dr["TotalVehicles"];

                        report.Add(currentRow);
                    }
                }
            }

            return report;
        }

        public List<PurchaseTypes> GetAllPurchaseTypes()
        {
            List<PurchaseTypes> purchaseTypes = new List<PurchaseTypes>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PurchaseTypes currentRow = new PurchaseTypes();
                        currentRow.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        currentRow.PurchaseTypeName = dr["PurchaseTypeName"].ToString();

                        purchaseTypes.Add(currentRow);

                    }
                }
            }

            return purchaseTypes;
        }

        public List<States> GetAllStates()
        {
            List<States> states = new List<States>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("StatesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        States currentRow = new States();
                        currentRow.StateId = dr["StateId"].ToString();
                        currentRow.StateName = dr["StateName"].ToString();

                        states.Add(currentRow);

                    }
                }
            }

            return states;
        }

        public IEnumerable<VehicleType> GetVehicleType()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleTypeSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleType currentRow = new VehicleType();
                        currentRow.TypeId = (int)dr["TypeId"];
                        currentRow.TypeName = dr["TypeName"].ToString();

                        vehicleTypes.Add(currentRow);

                    }
                }
            }

            return vehicleTypes;
        }

        public IEnumerable<BodyStyles> GetBodyStyle()
        {
            List<BodyStyles> bodystyles = new List<BodyStyles>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BodyStyleSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyStyles currentRow = new BodyStyles();
                        currentRow.BodyStyleId = (int)dr["BodyStyleId"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();

                        bodystyles.Add(currentRow);
                    }
                }
            }
            return bodystyles;
        }

        public IEnumerable<Transmission> GetTransmission()
        {
            List<Transmission> transmissions = new List<Transmission>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TransmissionSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Transmission currentRow = new Transmission();
                        currentRow.TransmissionId = (int)dr["TransmissionId"];
                        currentRow.TransmissionTypeName = dr["TransmissionTypeName"].ToString();

                        transmissions.Add(currentRow);
                    }
                }
            }
            return transmissions;
        }

        public IEnumerable<ExteriorColor> GetExteriorColor()
        {
            List<ExteriorColor> exteriorColors = new List<ExteriorColor>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ExteriorColorSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ExteriorColor currentRow = new ExteriorColor();
                        currentRow.ExteriorColorId = (int)dr["ExteriorColorId"];
                        currentRow.ExteriorColorName = dr["ExteriorColorName"].ToString();

                        exteriorColors.Add(currentRow);
                    }
                }
            }
            return exteriorColors;
        }

        public IEnumerable<InteriorColor> GetInteriorColor()
        {
            List<InteriorColor> interiorColors = new List<InteriorColor>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InteriorColorSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InteriorColor currentRow = new InteriorColor();
                        currentRow.InteriorColorId = (int)dr["InteriorColorId"];
                        currentRow.InteriorColorName = dr["InteriorColorName"].ToString();

                        interiorColors.Add(currentRow);
                    }
                }
            }
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

        Vehicle IVehicleRepository.GetVehicleById(int vehicleId)
        {
            Vehicle vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new Vehicle();
                        vehicle.VehicleId = (int)dr["VehicleId"];
                        vehicle.MakeId = (int)dr["MakeId"];
                        vehicle.ModelId = (int)dr["ModelId"];
                        if (dr["TypeId"] != DBNull.Value)
                            vehicle.TypeId = (int)dr["TypeId"];
                        if (dr["BodyStyleId"] != DBNull.Value)
                            vehicle.BodyStyleId = (int)dr["BodyStyleId"];
                        if (dr["TransmissionId"] != DBNull.Value)
                            vehicle.TransmissionId = (int)dr["TransmissionId"];
                        if (dr["ExteriorColorId"] != DBNull.Value)
                            vehicle.ExteriorColorId = (int)dr["ExteriorColorId"];
                        if (dr["InteriorColorId"] != DBNull.Value)
                            vehicle.ExteriorColorId = (int)dr["InteriorColorId"];
                        vehicle.Mileage = (int)dr["Mileage"];
                        vehicle.VIN = dr["VIN"].ToString();
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        if (dr["VehicleDescription"] != DBNull.Value)
                            vehicle.VehicleDescription = dr["VehicleDescription"].ToString();
                        vehicle.VehicleYear = (int)dr["VehicleYear"];
                        vehicle.HasFeatured = (bool)dr["HasFeatured"];
                        if (dr["ImageFileName"] != DBNull.Value)
                            vehicle.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return vehicle;
        }

        
    }
}







