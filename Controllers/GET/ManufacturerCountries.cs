using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ManufacturerCountry>?> ManufacturerCountries() // Получить список стран-производителей
        {
            using ParsethingContext db = new();
            List<ManufacturerCountry>? manufacturerCountries = null;

            try { manufacturerCountries = await db.ManufacturerCountries.ToListAsync(); }
            catch { }

            return manufacturerCountries;
        }
    }
}
