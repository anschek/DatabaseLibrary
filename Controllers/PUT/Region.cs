using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Region(Region region)
        {
            using ParsethingContext db = new();
            Region? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Regions
                    .Where(r => r.Id == region.Id)
                    .FirstAsync();

                def.Title = region.Title;
                def.Distance = region.Distance;
                def.RegionCode = region.RegionCode;
                def.BicoId = region.BicoId;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
