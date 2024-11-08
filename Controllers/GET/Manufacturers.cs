using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Manufacturers
        {
            public static async Task<Manufacturer?> One(int id) // Получить производителя
            {
                using ParsethingContext db = new();
                Manufacturer? manufacturer = null;
                try
                {
                    manufacturer = await db.Manufacturers
                        .Where(m => m.Id == id)
                        .Include(m => m.ManufacturerCountry)
                        .FirstAsync();
                }
                catch { }

                return manufacturer;
            }

            public static async Task<List<Manufacturer>?> Many() // Получить список производителей
            {
                using ParsethingContext db = new();
                List<Manufacturer>? manufacturers = null;

                try
                {
                    manufacturers = await db.Manufacturers
                        .Include(m => m.ManufacturerCountry)
                        .ToListAsync();
                }
                catch { }

                return manufacturers;
            }
        }
    }
}
