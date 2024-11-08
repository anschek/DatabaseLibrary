using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Minopttorg(Minopttorg minopttorg)
        {
            using ParsethingContext db = new();
            Minopttorg? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Minopttorgs
                    .Where(m => m.Id == minopttorg.Id)
                    .FirstAsync();

                def.Name = minopttorg.Name;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
