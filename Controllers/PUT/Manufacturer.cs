using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Manufacturer(Manufacturer manufacturer)
        {
            using ParsethingContext db = new();
            Manufacturer? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Manufacturers
                    .Where(m => m.ManufacturerName == manufacturer.ManufacturerName)
                    .FirstAsync();

                def.ManufacturerName = manufacturer.ManufacturerName;
                def.FullManufacturerName = manufacturer.FullManufacturerName;
                def.ManufacturerCountryId = manufacturer.ManufacturerCountryId;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
