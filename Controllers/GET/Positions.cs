using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<Position>?> Positions() // Получить список должностей
        {
            using ParsethingContext db = new();
            List<Position>? positions = null;

            try { positions = await db.Positions.ToListAsync(); }
            catch { }

            return positions;
        }
    }
}
