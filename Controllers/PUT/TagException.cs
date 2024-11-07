using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> TagException(TagException tagException)
        {
            using ParsethingContext db = new();
            TagException? def = null;
            bool isSaved = true;

            try
            {
                def = await db.TagExceptions
                    .Where(t => t.Id == tagException.Id)
                    .FirstAsync();

                def.Keyword = tagException.Keyword;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
