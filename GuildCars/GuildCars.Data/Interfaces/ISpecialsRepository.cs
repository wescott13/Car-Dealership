using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Tables;

namespace GuildCars.Data.Interfaces
{
    public interface ISpecialsRepository
    {
        List<Specials> GetAll();
        void Insert(Specials specials);
        void Delete(int specialsId);
    }
}
