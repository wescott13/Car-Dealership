using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.Data.Interfaces
{
    public interface IUserRepository
    {
        List<UserDetails> GetAll();
        List<Roles> GetAllRoles();

    }
}
