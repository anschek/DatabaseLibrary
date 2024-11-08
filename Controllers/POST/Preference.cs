﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class POST
    {
        public static async Task<bool> Preference(Preference preference)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = await db.Preferences.AddAsync(preference);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }
    }
}
