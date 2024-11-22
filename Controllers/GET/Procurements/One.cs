using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static partial class GET
    {
        public static partial class Procurements
        {
            public static class One
            {
                public static async Task<Procurement?> ByNumber(string number) // Получить тендер
                {
                    using ParsethingContext db = new();
                    Procurement? procurement = null;

                    try
                    {
                        procurement = await db.Procurements
                            .Where(p => p.Number == number)
                            .FirstAsync();
                    }
                    catch { }

                    return procurement;
                }
                public static async Task<Procurement?> ById(int id) // Получить тендер по id
                {
                    using ParsethingContext db = new();
                    Procurement? procurement = null;

                    try
                    {
                        procurement = await Queries.All()
                            .Where(p => p.Id == id)
                            .FirstAsync();
                    }
                    catch { }

                    return procurement;
                }

                public static async Task<DeletedProcurement?> Deleted(string number) // Получить удаленную закупку по номеру
                {
                    using ParsethingContext db = new();
                    DeletedProcurement? deletedProcurement = null;

                    try
                    {
                        deletedProcurement = await db.DeletedProcurements
                            .Where(dp => dp.Number == number)
                            .FirstAsync();
                    }
                    catch { }

                    return deletedProcurement;
                }

                public static async Task<int> ActualId(int currentProcurementId, int? parentProcurementId)
                {
                    using ParsethingContext db = new();

                    if (parentProcurementId.HasValue)
                    {
                        var procurement = await db.Procurements
                            .FirstOrDefaultAsync(p => p.DisplayId == parentProcurementId.Value);

                        if (procurement != null) return procurement.Id;

                    }

                    return currentProcurementId;
                }
            }
        }
    }
}