using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Position(Position position)
        {
            using ParsethingContext db = new();
            Position? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Positions
                    .Where(p => p.Id == position.Id)
                    .FirstAsync();

                def.Kind = position.Kind;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
