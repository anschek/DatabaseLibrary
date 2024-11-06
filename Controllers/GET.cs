using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static class GET
    {
        public static class City
        {
            public static async Task<Entities.ProcurementProperties.City?> One(string name) // Получить закон
            {
                using ParsethingContext db = new();
                Entities.ProcurementProperties.City? city = null;

                try
                {
                    city = await db.Cities
                        .Where(l => l.Name == name)
                        .FirstAsync();
                }
                catch { }

                return city;
            }

            public static async Task<List<Entities.ProcurementProperties.City>?> Many() // Получить города
            {
                using ParsethingContext db = new();
                List<Entities.ProcurementProperties.City>? cities = null;

                try
                {
                    cities = await db.Cities
                        .ToListAsync();
                }
                catch { }

                return cities;
            }
        }

    }
}
