using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<Organization?> OneByName(string name) // Получить организацию
        {
            using ParsethingContext db = new();
            Organization? organization = null;

            try
            {
                organization = await db.Organizations
                    .Where(o => o.Name == name)
                    .FirstAsync();
            }
            catch { }

            return organization;
        }

        public static async Task<Organization?> OneByNameAndAddress(string name, string? postalAddress) // Получить организацию по имени и адресу поставки
        {
            using ParsethingContext db = new();
            Organization? organization = null;

            try
            {
                organization = await db.Organizations
                    .Where(o => o.Name == name)
                    .Where(o => o.PostalAddress == postalAddress)
                    .FirstAsync();
            }
            catch { }

            return organization;
        }

        public static async Task<List<Organization>?> Many() // Получить все организации
        {
            using ParsethingContext db = new();
            List<Organization> organizations = null;

            try { organizations = await db.Organizations.ToListAsync(); }
            catch { }

            return organizations;
        }
    }
}
