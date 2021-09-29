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
    public class ContactUsRepositoryMock : IContactUsRepository
    {
        private static List<ContactUs> contactUsList = new List<ContactUs>();

        private int ContactUsId()
        {
            if (contactUsList.Count == 0)
                return 1;
            else
                return contactUsList.Max(Id => Id.ContactUsId) + 1;
        }

        public void Insert(ContactUs contactUs)
        {
            contactUs.ContactUsId = ContactUsId();
            contactUsList.Add(contactUs); 
        }
    }
}
