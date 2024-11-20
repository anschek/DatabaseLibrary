using DatabaseLibrary.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static class Procurements
        {
            public static async Task<bool> One(Procurement procurement)
            {
                using ParsethingContext db = new();
                bool isSaved = true;

                try
                {
                    _ = await db.Procurements.AddAsync(procurement);
                    _ = await db.SaveChangesAsync();
                }
                catch { isSaved = false; }

                return isSaved;
            }

            public static async Task<bool> Source(Procurement procurement)
            {
                using ParsethingContext db = new();
                bool isSaved = true;
                Procurement? def = null;

                try
                {
                    def = await db.Procurements
                        .Where(p => p.InitialPrice == procurement.InitialPrice && p.Object == procurement.Object)
                        .FirstAsync();
                }
                catch { }

                try
                {
                    if (def == null)
                    {
                        DeletedProcurement? deletedProcurement = await GET.Procurements.One.Deleted(procurement.Number);
                        if (deletedProcurement == null)
                        {
                            procurement.ProcurementStateId = 20;
                            if (procurement.TimeZoneId == null)
                            {
                                TimeZone? timeZone = await GET.TimeZones.One("см. zakupki.gov.ru");
                                if (timeZone != null)
                                {
                                    procurement.TimeZoneId = timeZone.Id;
                                }
                                else
                                {
                                    _ = await Controllers.POST.TimeZone(new()
                                    {
                                        Offset = "см. zakupki.gov.ru"
                                    });
                                    timeZone = await Controllers.GET.TimeZones.One("см. zakupki.gov.ru");
                                    procurement.TimeZoneId = timeZone != null ? timeZone.Id : 0;
                                }
                            }
                            _ = await db.Procurements.AddAsync(procurement);
                            _ = await db.SaveChangesAsync();
                        }
                        else
                            return isSaved = false;
                    }
                    else if (!await PUT.Procurements.Source(procurement, def))
                        throw new Exception();
                }
                catch { isSaved = false; }

                return isSaved;
            }
        }

    }
}
