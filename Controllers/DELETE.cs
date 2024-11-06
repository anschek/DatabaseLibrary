using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static class DELETE
    {
        public static async Task<bool> City(City city)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = db.Cities.Remove(city);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }

        public static async Task<bool> Employee(Employee employee)
        {
            using ParsethingContext db = new();
            bool isSaved = true;

            try
            {
                _ = db.Employees.Remove(employee);
                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }

        public static class ComponentCalculation
        {
            public static async Task<bool> One(Entities.ComponentCalculationProperties.ComponentCalculation componentCalculation)
            {
                using ParsethingContext db = new();
                bool isSaved = true;

                try
                {
                    _ = db.ComponentCalculations.Remove(componentCalculation);
                    _ = await db.SaveChangesAsync();
                }
                catch { isSaved = false; }

                return isSaved;
            }

            public static async Task<bool> ManyByParentName(int id)
            {
                using ParsethingContext db = new();
                bool isSaved = true;
                try
                {
                    var componentCalculationToDelete = db.ComponentCalculations.Where(cc => cc.Id == id || cc.ParentName == id);
                    db.ComponentCalculations.RemoveRange(componentCalculationToDelete);

                    _ = await db.SaveChangesAsync();
                }
                catch { isSaved = false; }
                return isSaved;
            }

        }
    }
}
