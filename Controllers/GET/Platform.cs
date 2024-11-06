using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Platforms
        {
            public static async Task<Platform?> OneByNameAndAddress(string name, string address) // Получить платформу по имени и адресу
            {
                using ParsethingContext db = new();
                Platform? platform = null;

                try
                {
                    platform = await db.Platforms
                        .Where(p => p.Name == name)
                        .Where(p => p.Address == address)
                        .FirstAsync();
                }
                catch { }

                return platform;
            }

            public static async Task<Platform?> OneByAddress(string address) // Получить платформу по адресу
            {
                using ParsethingContext db = new();
                Platform? platform = null;

                try
                {
                    platform = await db.Platforms
                        .Where(p => p.Address == address)
                        .FirstAsync();
                }
                catch { }

                return platform;
            }
        }
    }
}
