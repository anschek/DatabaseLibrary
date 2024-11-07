using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static async Task<bool> Document(Document document)
        {
            using ParsethingContext db = new();
            Document? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Documents
                    .Where(d => d.Id == document.Id)
                    .FirstAsync();

                def.Title = document.Title;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
