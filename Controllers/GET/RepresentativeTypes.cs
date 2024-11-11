using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<RepresentativeType>?> RepresentativeTypes() // Получить список пусконаладочных работ
        {
            using ParsethingContext db = new();
            List<RepresentativeType>? representativeTypes = null;

            try { representativeTypes = await db.RepresentativeTypes.ToListAsync(); }
            catch { }

            return representativeTypes;
        }
    }
}
