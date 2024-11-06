using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> City(City city)
        {
            using ParsethingContext db = new();
            City? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Cities
                    .Where(cs => cs.Id == city.Id)
                    .FirstAsync();

                def.Name = city.Name;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
