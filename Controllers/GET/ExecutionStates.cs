using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static async Task<List<ExecutionState>?> ExecutionStates() // Получить список статусов БГ
        {
            using ParsethingContext db = new();
            List<ExecutionState>? executionStates = null;

            try { executionStates = await db.ExecutionStates.ToListAsync(); }
            catch { }

            return executionStates;
        }
    }
}
