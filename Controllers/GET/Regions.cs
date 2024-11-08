using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Regions
        {
            public static async Task<Region?> One(string title) // Получить регион по названию
            {
                using ParsethingContext db = new();
                Region? region = null;

                try
                {
                    region = await db.Regions
                        .Where(r => r.Title == title)
                        .FirstAsync();
                }
                catch { }

                return region;
            }

            public static async Task<List<Region>?> Many() // Получить список регионов
            {
                using ParsethingContext db = new();
                List<Region>? regions = null;

                try { regions = await db.Regions.ToListAsync(); }
                catch { }

                return regions;
            }
        }
    }
}
