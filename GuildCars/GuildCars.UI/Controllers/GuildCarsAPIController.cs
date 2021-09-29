using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GuildCars.Data.Factory;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.UI.Controllers
{
    public class GuildCarsAPIController : ApiController
    {
        [Route("api/newVehicles/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult searchNew(decimal? minPrice, decimal? maxPrice, string minYear, string maxYear, string searchTerm)
        {

            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    SearchTerm = searchTerm
                };
                var results = repo.SearchNewVehicles(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/usedVehicles/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult searchUsed(decimal? minPrice, decimal? maxPrice, string minYear, string maxYear, string searchTerm)
        {

            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    SearchTerm = searchTerm
                };
                var results = repo.SearchUsedVehicles(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/salesVehicles/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult searchSalesVehicles(decimal? minPrice, decimal? maxPrice, string minYear, string maxYear, string searchTerm)
        {

            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    SearchTerm = searchTerm
                };
                var results = repo.SearchSalesVehicles(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/make/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModelMakes(int id)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            return Ok(repo.GetModelMakes(id));
        }

        [Route("api/userSales/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(DateTime? fromDate, DateTime? toDate, string userId)
        {

            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new SalesReportSearchParameters()
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    UserId = userId
                    
                };
                var results = repo.SearchSalesReport(parameters);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
