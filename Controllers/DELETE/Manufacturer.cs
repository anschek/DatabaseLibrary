using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class DELETE
    {
        public static async Task<bool> Manufacturer(Manufacturer manufacturer)
        {
            using ParsethingContext db = new();
            bool isSaved = true;
            try
            {
                _ = db.Manufacturers.Remove(manufacturer);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
