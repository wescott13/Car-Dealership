using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;


namespace GuildCars.Models.UIViews
{
    public class SalesReportView
    {
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
