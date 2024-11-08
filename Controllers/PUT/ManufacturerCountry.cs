using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> ManufacturerCountry(ManufacturerCountry manufacturerCountry)
        {
            using ParsethingContext db = new();
            ManufacturerCountry? def = null;
            bool isSaved = true;

            try
            {
                def = await db.ManufacturerCountries
                    .Where(m => m.Id == manufacturerCountry.Id)
                    .FirstAsync();

                def.Name = manufacturerCountry.Name;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
