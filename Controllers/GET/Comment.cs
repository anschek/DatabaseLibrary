using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<Comment>?> Comments(int? procurementId, bool onlyTechnical = false) // Получить комментарии по id тендера, если нужны только технические, передать onlyTechnical=true
        {
            using ParsethingContext db = new();

            var comments = await db.Comments
                .Include(c => c.Employee)
                .Where(pe => pe.EntryId == procurementId)
                .Where(pe => onlyTechnical ? pe.IsTechnical == true : true)
                .Where(c => c.EntityType == "Procurement")
                .ToListAsync();

            return comments;
        }

    }
}
