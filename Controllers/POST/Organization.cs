using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> Organization(Organization organization)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = await db.Organizations.AddAsync(organization);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
