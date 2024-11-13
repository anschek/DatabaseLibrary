using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> ProcurementsDocuments(ProcurementsDocument procurementsDocument)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                if (procurementsDocument.Procurement != null && procurementsDocument.Document != null)
                {
                    procurementsDocument.ProcurementId = procurementsDocument.Procurement.Id;
                    procurementsDocument.DocumentId = procurementsDocument.Document.Id;
                }
                else throw new Exception();
                procurementsDocument.Procurement = null;
                procurementsDocument.Document = null;

                _ = await db.ProcurementsDocuments.AddAsync(procurementsDocument);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
