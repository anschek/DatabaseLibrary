using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Cities
        {
            public static async Task<City?> One(string name) // Получить закон
            {
                using ParsethingContext db = new();
                City? city = null;

                try
                {
                    city = await db.Cities
                        .Where(l => l.Name == name)
                        .FirstAsync();
                }
                catch { }

                return city;
            }

            public static async Task<List<City>?> Many() // Получить города
            {
                using ParsethingContext db = new();
                List<City>? cities = null;

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
