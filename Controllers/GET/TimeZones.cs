using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class TimeZones
        {
            public static async Task<TimeZone?> One(string offset) // Получить часовой пояс
            {
                using ParsethingContext db = new();
                TimeZone? timeZone = null;

                try
                {
                    timeZone = await db.TimeZones
                        .Where(tz => tz.Offset == offset)
                        .FirstAsync();
                }
                catch { }

                return timeZone;
            }

            public static async Task<List<TimeZone>?> Many() // Получить список часовых поясов
            {
                using ParsethingContext db = new();
                List<TimeZone>? timeZones = null;

                try { timeZones = await db.TimeZones.ToListAsync(); }
                catch { }

                return timeZones;
            }
        }

    }
}
