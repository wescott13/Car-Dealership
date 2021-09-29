using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data
{
    public class Settings
    {
        private static string _connectionstring;
        private static string _repositoryType;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionstring))
                _connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return _connectionstring;
        }

        public static string GetRepositoryType()
        {
            if (string.IsNullOrEmpty(_repositoryType))
                //_repositoryType = ConfigurationManager.AppSettings["RepositoryType"].ToString();
                _repositoryType = ConfigurationManager.AppSettings["Mode"].ToString();
            return _repositoryType;
        }

    }
}