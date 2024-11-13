using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class DELETE
    {
        public static async Task<bool> ProcurementDocument(ProcurementsDocument procurementsDocument)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                var procurementToDelete = await db.ProcurementsDocuments.FirstOrDefaultAsync(pd => pd.ProcurementId == procurementsDocument.ProcurementId && pd.DocumentId == procurementsDocument.DocumentId);
                if (procurementsDocument != null)
                {
                    _ = db.ProcurementsDocuments.Remove(procurementToDelete);
                    _ = await db.SaveChangesAsync();
                }
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
