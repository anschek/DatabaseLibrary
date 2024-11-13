using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ProcurementsDocument>?> ProcurementsDocuments(int procurementId) // Получить документы по конкретному тендеру
        {
            using ParsethingContext db = new();
            List<ProcurementsDocument>? procurementsDocuments = null;

            try
            {
                procurementsDocuments = await db.ProcurementsDocuments
                    .Include(pd => pd.Procurement)
                    .Include(pd => pd.Document)
                    .Where(pd => pd.ProcurementId == procurementId)
                    .ToListAsync();
            }
            catch { }

            return procurementsDocuments;
        }
    }
}
