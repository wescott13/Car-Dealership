using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;

namespace GuildCars.Data.ADO
{
    public class ContactUsRepositoryADO : IContactUsRepository
    {
        public void Insert(ContactUs contactUs)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactUsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactUsId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                if (contactUs.ContactName != null)
                {
                    cmd.Parameters.AddWithValue("@ContactName", contactUs.ContactName);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactName", DBNull.Value);
                }

                if (contactUs.Email != null)
                {
                    cmd.Parameters.AddWithValue("@Email", contactUs.Email);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                if (contactUs.PhoneNumber != null)
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", contactUs.PhoneNumber);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@ContactUsMessage", contactUs.ContactUsMessage);

                cn.Open();

                cmd.ExecuteNonQuery();

                contactUs.ContactUsId = (int)param.Value;
            }
        }
    }
}
