using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Tag(Tag tag)
        {
            using ParsethingContext db = new();
            Tag? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Tags
                    .Where(t => t.Id == tag.Id)
                    .FirstAsync();

                def.Keyword = tag.Keyword;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
