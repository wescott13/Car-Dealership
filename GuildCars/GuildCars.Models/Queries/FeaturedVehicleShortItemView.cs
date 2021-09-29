using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Tables;

namespace GuildCars.Models.Queries
{
    public class FeaturedVehicleShortItemView
    {
        public class ShortItemFeaturedVehicles
        {
            public class ShortItemFeaturedVehiclesUI
            {
                public ShortItemFeaturedVehiclesUI()
                {
                    shortItemFeaturedVehicle = new List<FeaturedVehicleShortItem>();
                }
                public List<FeaturedVehicleShortItem> shortItemFeaturedVehicle { get; set; }
            }
        }
    }
}
