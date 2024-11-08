using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Seller(Seller seller)
        {
            using ParsethingContext db = new();
            Seller? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Sellers
                    .Where(r => r.Id == seller.Id)
                    .FirstAsync();

                def.Name = seller.Name;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
