using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Methods
        {
            public static async Task<Method?> One(string text) // Получить метод
            {
                using ParsethingContext db = new();
                Method? method = null;
                try
                {
                    method = await db.Methods
                        .Where(m => m.Text == text)
                        .FirstAsync();
                }
                catch { }

                return method;
            }

            public static async Task<List<Method>?> Many() // Получить все методы определения поставщика
            {
                using ParsethingContext db = new();
                List<Method> methods = null;
                try { methods = await db.Methods.ToListAsync(); }
                catch { }

                return methods;
            }
        }
    }
}
