using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;

namespace GuildCars.Data.Mock
{
    public class SpecialsRepositoryMock : ISpecialsRepository
    {
        private static List<Specials> specialsList = new List<Specials>()
        {
            new Specials(){SpecialId = 1, Title = "Mock Special Test 1 Title", SpecialDescription = "Mock Special Test 1 Description."},
            new Specials(){SpecialId = 2, Title = "Mock Special Test 2 Title", SpecialDescription = "Mock Special Test 2 Description."},
            new Specials(){SpecialId = 3, Title = "Mock Special Test 3 Title", SpecialDescription = "Mock Special Test 3 Description."}

        };
        public void Delete(int specialsId)
        {
            specialsList.RemoveAll(Id => Id.SpecialId == specialsId);
        }

        public List<Specials> GetAll()
        {
            return specialsList;
        }
        private int SpecialsId()
        {
            if (specialsList.Count == 0)
                return 1;
            else
                return specialsList.Max(Id => Id.SpecialId) + 1;
        }

        public void Insert(Specials specials)
        {
            specials.SpecialId = SpecialsId();
            specialsList.Add(specials);
        }
    }
}