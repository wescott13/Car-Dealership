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

namespace GuildCars.Data.ADO
{
    public class UserRepositoryADO : IUserRepository
    {
        public List<UserDetails> GetAll()
        {
            List<UserDetails> users = new List<UserDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UsersSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        UserDetails currentRow = new UserDetails();
                        currentRow.Id = dr["Id"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.RoleName = dr["RoleName"].ToString();

                        users.Add(currentRow);
                    }
                }
            }

            return users;
        }

        List<Roles> IUserRepository.GetAllRoles()
        {
            List<Roles> roles = new List<Roles>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("RolesSeletAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Roles currentRow = new Roles();
                        currentRow.Id = dr["Id"].ToString();
                        currentRow.Name = dr["Name"].ToString();

                        roles.Add(currentRow);

                    }
                }
            }

            return roles;
        }

        
    }
}
