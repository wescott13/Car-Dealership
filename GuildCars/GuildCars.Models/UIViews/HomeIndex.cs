using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Tables;
using GuildCars.Models.Queries;

namespace GuildCars.Models.UIViews
{
    public class HomeIndex
    {
        public List<Specials> special { get; set; }
        public List<FeaturedVehicleShortItem> featuredVehicles { get; set; }

    }
}
