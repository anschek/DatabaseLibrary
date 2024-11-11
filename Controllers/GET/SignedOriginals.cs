using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<SignedOriginal>?> SignedOriginals() // Получить список подписанных оригиналов
        {
            using ParsethingContext db = new();
            List<SignedOriginal>? signedOriginals = null;

            try { signedOriginals = await db.SignedOriginals.ToListAsync(); }
            catch { }

            return signedOriginals;
        }
    }
}
