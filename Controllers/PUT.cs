using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static class PUT
    {
        public static async Task<bool> City(City city)
        {
            using ParsethingContext db = new();
            City? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Cities
                    .Where(cs => cs.Id == city.Id)
                    .FirstAsync();

                def.Name = city.Name;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }

        public static async Task<bool> Employee(Employee employee)
        {
            using ParsethingContext db = new();
            Employee? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Employees
                    .Include(e => e.Position)
                    .Where(e => e.Id == employee.Id)
                    .FirstAsync();

                def.FullName = employee.FullName;
                def.UserName = employee.UserName;
                def.Password = employee.Password;
                def.PositionId = employee.PositionId;
                def.Photo = employee.Photo;
                def.IsAvailable = employee.IsAvailable;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }

    }
}
