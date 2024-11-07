using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<Document>?> Documents() // Получить документы к тендерам
        {
            using ParsethingContext db = new();
            List<Document>? documents = null;

            try
            {
                documents = await db.Documents
                    .ToListAsync();
            }
            catch { }

            return documents;
        }
    }
}
