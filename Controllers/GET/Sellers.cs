using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<Seller>?> Sellers() // Получить список дистрибьюторов 
        {
            using ParsethingContext db = new();
            List<Seller>? sellers = null;

            try { sellers = await db.Sellers.ToListAsync(); }
            catch { }

            return sellers;
        }
    }
}
