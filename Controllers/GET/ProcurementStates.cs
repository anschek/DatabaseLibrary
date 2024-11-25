using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class GET
    {
        public static class ProcurementStates
        {
            public static async Task<List<ProcurementState>?> All() // Получить список всех статусов тендеров
            {
                using ParsethingContext db = new();
                List<ProcurementState>? procurementStates = null;

                try { procurementStates = await db.ProcurementStates.ToListAsync(); }
                catch { }

                return procurementStates;
            }

            public static async Task<List<ProcurementState>?> ManyByEmployeePosition(string employeePosition) // Получить статусы к которым имеют доступ конкретные должности
            {
                using ParsethingContext db = new();
                List<ProcurementState>? procurementStates = null;

                try
                {
                    if(employeePosition == "Администратор" || employeePosition == "Юрист") return await db.ProcurementStates.ToListAsync();
                    else
                    {
                        string[] positionPermisssion = StateKindsByEmployeePosition(employeePosition);
                        return await db.ProcurementStates.Where(ps => positionPermisssion.Contains(ps.Kind)).ToListAsync();
                    }
                }
                catch { }

                return procurementStates;
            }

            private static string[] StateKindsByEmployeePosition(string employeePosition)
            {
                switch(employeePosition)
                {
                    case "Руководитель отдела расчетов": return new string[]{ "Новый", "Посчитан", "Оформить", "Оформлен", "Выигран 1ч", "Выигран 2ч", "Разбор", "Отбой", "Неразобранный", "Проверка" };

                    case "Заместитель руководителя отдела расчетов": return new string[] { "Новый", "Посчитан", "Оформить", "Оформлен", "Выигран 1ч", "Выигран 2ч", "Разбор", "Отбой", "Неразобранный", "Проверка" };

                    case "Специалист отдела расчетов": return new string[] { "Новый", "Посчитан", "Оформлен", "Проверка" };

                    case "Руководитель тендерного отдела": return new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка", "Принят", "Отклонен", "Отмена", "Проигран" };

                    case "Заместитель руководителя тендреного отдела": return new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка", "Принят", "Отклонен", "Отмена", "Проигран" };

                    case "Специалист тендерного отдела": return new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка", "Принят" };

                    case "Специалист по работе с электронными площадками": return new string[] { "Оформлен", "Новый", "Отправлен", "Выигран 1ч", "Отмена", "Проигран", "Принят" };

                    case "Руководитель отдела закупки": return new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" };

                    case "Заместитель руководителя отдела закупок": return new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" };

                    case "Специалист закупки": return new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" };

                    case "Руководитель отдела производства":return new string[] { "Выигран 2ч", "Приемка" };

                    case "Заместитель руководителя отдела производства": return new string[] { "Выигран 2ч" };

                    case "Специалист по производству":return new string[] { "Выигран 2ч" };

                    default: return new string[] { };                
                }
            }
        }
    }
}
