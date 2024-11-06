using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class Employees
        {
            public static async Task<Employee?> One(string userName, string password) // Авторизация
            {
                using ParsethingContext db = new();
                Employee? employee = null;

                try
                {
                    employee = await db.Employees
                        .Include(e => e.Position)
                        .Where(e => e.UserName == userName)
                        .Where(e => e.Password == password)
                        .FirstAsync();
                }
                catch { }

                return employee;
            }

            public static async Task<List<Employee>?> Many() // Получить сотрудников
            {
                using ParsethingContext db = new();
                List<Employee>? employees = null;

                try
                {
                    employees = await db.Employees
                        .Include(e => e.Position)
                        .ToListAsync();
                }
                catch { }

                return employees;
            }

            public static async Task<List<Employee>?> ManyByPositions(string premierPosition, string secondPosition, string thirdPosition) // Получить сотрудников по трем должностям
            {
                using ParsethingContext db = new();
                List<Employee>? employees = null;

                try
                {
                    employees = await db.Employees
                        .Include(e => e.Position)
                        .Where(e => e.Position.Kind == premierPosition || e.Position.Kind == secondPosition || e.Position.Kind == thirdPosition)
                        .Where(e => e.IsAvailable == true)
                        .ToListAsync();
                }
                catch { }

                return employees;
            }
        }
    }
}
