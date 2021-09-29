using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;

namespace GuildCars.Data.Mock
{
    public class StatesRepositoryMock : IStatesRepository
    {
        public List<States> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
