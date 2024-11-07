using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Laws
        {
            public static async Task<Law?> One(string number) // Получить закон
            {
                using ParsethingContext db = new();
                Law? law = null;

                try
                {
                    law = await db.Laws
                        .Where(l => l.Number == number)
                        .FirstAsync();
                }
                catch { }

                return law;
            }

            public static async Task<List<Law>?> Many() // Получить все законы
            {
                using ParsethingContext db = new();
                List<Law> laws = null;

                try { laws = await db.Laws.ToListAsync(); }
                catch { }

                return laws;
            }
        }
       
    }
}
