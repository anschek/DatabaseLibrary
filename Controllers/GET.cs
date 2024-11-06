using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static class GET
    {
        public static class City
        {
            public static async Task<Entities.ProcurementProperties.City?> One(string name) // Получить закон
            {
                using ParsethingContext db = new();
                Entities.ProcurementProperties.City? city = null;

                try
                {
                    city = await db.Cities
                        .Where(l => l.Name == name)
                        .FirstAsync();
                }
                catch { }

                return city;
            }

            public static async Task<List<Entities.ProcurementProperties.City>?> Many() // Получить города
            {
                using ParsethingContext db = new();
                List<Entities.ProcurementProperties.City>? cities = null;

                try
                {
                    cities = await db.Cities
                        .ToListAsync();
                }
                catch { }

                return cities;
            }
        }

        public static class Employee
        {
            public static async Task<Entities.EmployeeMuchToMany.Employee?> One(string userName, string password) // Авторизация
            {
                using ParsethingContext db = new();
                Entities.EmployeeMuchToMany.Employee? employee = null;

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

            public static async Task<List<Entities.EmployeeMuchToMany.Employee>?> Many() // Получить сотрудников
            {
                using ParsethingContext db = new();
                List<Entities.EmployeeMuchToMany.Employee>? employees = null;

                try
                {
                    employees = await db.Employees
                        .Include(e => e.Position)
                        .ToListAsync();
                }
                catch { }

                return employees;
            }

            public static async Task<List<Entities.EmployeeMuchToMany.Employee>?> ManyByPositions(string premierPosition, string secondPosition, string thirdPosition) // Получить сотрудников по трем должностям
            {
                using ParsethingContext db = new();
                List<Entities.EmployeeMuchToMany.Employee>? employees = null;

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
