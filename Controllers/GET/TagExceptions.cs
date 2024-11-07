using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<TagException>?> TagExceptions() // Получить тэги для парсинга 
        {
            using ParsethingContext db = new();
            List<TagException>? tagExceptions = null;

            try { tagExceptions = await db.TagExceptions.ToListAsync(); }
            catch { }

            return tagExceptions;
        }
    }
}
