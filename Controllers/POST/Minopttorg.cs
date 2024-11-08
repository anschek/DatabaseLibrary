using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> Minopttorg(Minopttorg minopttorg)
        {
            using ParsethingContext db = new();
            bool isSaved = true;
            try
            {
                _ = await db.Minopttorgs.AddAsync(minopttorg);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
