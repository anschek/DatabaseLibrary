using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<Tag>?> Tags() // Получить тэги для парсинга 
        {
            using ParsethingContext db = new();
            List<Tag>? tags = null;

            try { tags = await db.Tags.ToListAsync(); }
            catch { }

            return tags;
        }
    }
}
