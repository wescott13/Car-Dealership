using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.Data.Mock
{
    public class UserRepositoryMock : IUserRepository
    {
        public List<UserDetails> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Roles> GetAllRoles()
        {
            throw new NotImplementedException();
        }
    }
}
