using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static class DELETE
    {
        public static async Task<bool> City(City city)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = db.Cities.Remove(city);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
