using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<Minopttorg>?> Minopttorgs() // Получить список Миноптторг
        {
            using ParsethingContext db = new();
            List<Minopttorg>? minopttorgs = null;

            try { minopttorgs = await db.Minopttorgs.ToListAsync(); }
            catch { }

            return minopttorgs;
        }
    }
}
