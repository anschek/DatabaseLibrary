using DatabaseLibrary.Entities.DTOs;
using DatabaseLibrary.Entities.EmployeeMuchToMany;
using DatabaseLibrary.Entities.ProcurementProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static partial class GET
    {
        public static partial class ProcurementsEmployees
        {
            public static class Queries
            {
                public static IQueryable<ProcurementsEmployee> All()
                {
                    using ParsethingContext db = new();
                    return db.ProcurementsEmployees
                        .Include(pe => pe.Procurement)
                        .Include(pe => pe.Procurement.ProcurementState)
                        .Include(pe => pe.Employee)
                        .Include(pe => pe.Procurement.Law)
                        .Include(pe => pe.Procurement.Method)
                        .Include(pe => pe.Procurement.Region)
                        .Include(pe => pe.Procurement.Platform)
                        .Include(pe => pe.Procurement.TimeZone)
                        .Include(pe => pe.Procurement.City)
                        .Include(pe => pe.Procurement.Organization);
                }

                public static IQueryable<ProcurementsEmployee> AllForGrouping()
                {
                    using ParsethingContext db = new();
                    return db.ProcurementsEmployees
                        .Include(pe => pe.Procurement.Law)
                        .Include(pe => pe.Employee)
                        .Include(pe => pe.Procurement.ProcurementState)
                        .Include(pe => pe.Employee.Position)
                        .Include(pe => pe.Procurement.Method)
                        .Include(pe => pe.Procurement.Region)
                        .Include(pe => pe.Procurement);
                }

                public static IQueryable<int> validProcurementIds(string procurementState, DateTime startDate)
                {
                    using ParsethingContext db = new();
                    IQueryable<int> validProcurementIds;

                    if (procurementState == "Выигран 1ч")
                    {
                        var excludedStatuses = new List<string> { "Проигран", "Отклонен", "Отмена" };
                        validProcurementIds = db.Histories
                            .Where(h => h.Text == procurementState && h.Date >= startDate)
                            .GroupBy(h => h.EntryId)
                            .Where(g => !g.Any(h => excludedStatuses.Contains(h.Text)))
                            .Select(g => g.Key)
                            .Distinct();
                    }
                    else
                    {
                        validProcurementIds = db.Histories
                            .Where(h => h.Text == procurementState && h.Date >= startDate)
                            .Select(h => h.EntryId)
                            .Distinct();
                    }
                    return validProcurementIds;
                }

                public static Expression<Func<ProcurementsEmployee, bool>> KindPredicate(KindOf kindOf, string? kind = null)
                {
                    // предикат типа фильтрует тендеры сотрудников по:
                    Expression<Func<ProcurementsEmployee, bool>> kindPredicate = kindOf switch
                    {
                        KindOf.ProcurementState => // Статусу тендера
                            pe => pe.Procurement.ProcurementState != null &&
                                pe.Procurement.ProcurementState.Kind == kind &&
                                pe.Procurement.Applications != true,

                        KindOf.ShipmentPlane =>   // Плану отгрузки, конкретным статусам
                            pe => pe.Procurement.ShipmentPlan != null &&
                                pe.Procurement.ShipmentPlan.Kind == kind &&
                                pe.Procurement.ProcurementState.Kind == "Выигран 2ч" &&
                                pe.Procurement.Applications != true,

                        KindOf.Applications => // Положительному наличию статуса "по заявкам"
                            pe => pe.Procurement.Applications == true &&
                                new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" }.Contains(pe.Procurement.ProcurementState.Kind),

                        KindOf.ExecutionState => // Статусу обеспечения заявки, конкретным статусам тендера
                            pe => new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" }.Contains(pe.Procurement.ProcurementState.Kind) &&
                                new string[] { "Запрошена БГ", "На согласовании заказчика", "Внесение правок", "Согласована БГ", "Ожидает оплаты", "Деньги(Возвратные)" }.Contains(pe.Procurement.ExecutionState.Kind),

                        KindOf.WarrantyState => // Статусу обеспечения гарантии, конкретным статусам тендера
                            pe => new string[] { "Выигран 1ч", "Выигран 2ч", "Приемка" }.Contains(pe.Procurement.ProcurementState.Kind) &&
                                new string[] { "Запрошена БГ", "На согласовании заказчика", "Внесение правок", "Согласована БГ", "Ожидает оплаты", "Деньги(Возвратные)" }.Contains(pe.Procurement.WarrantyState.Kind),

                        KindOf.CorrectionDate => // Статусу обеспечения гарантии, конкретным статусам тендера
                            pe => pe.Procurement.ProcurementState.Kind == kind &&
                                pe.Procurement.CorrectionDate != null,

                        KindOf.Judgement => // Суд
                            pe => pe.Procurement.Judgment == true,

                        KindOf.FAS => // ФАС
                        pe => pe.Procurement.Fas == true,

                        _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")
                    };

                    return kindPredicate;
                }

                public static Expression<Func<ProcurementsEmployee, bool>> TermPredicate(KindOf kindOf, bool isOverdue)
                {
                    // Прендикат срока фильтрует в соответствии с датами
                    Expression<Func<ProcurementsEmployee, bool>> termPredicate = kindOf switch
                    {
                        KindOf.Deadline => // Дате окончания подачи заявок
                            pe => (isOverdue ? pe.Procurement.Deadline < DateTime.Now : pe.Procurement.Deadline > DateTime.Now),

                        KindOf.StartDate => // Дате начала подачи заявок
                            pe => (isOverdue ? pe.Procurement.StartDate < DateTime.Now : pe.Procurement.StartDate > DateTime.Now),

                        KindOf.ResultDate =>  // Дате подведения итогов
                            pe => (isOverdue ? pe.Procurement.ResultDate < DateTime.Now : pe.Procurement.ResultDate > DateTime.Now),

                        KindOf.ContractConclusion => // Дате подписания контракта
                            pe => pe.Procurement.Applications != true &&
                                (isOverdue ? pe.Procurement.ConclusionDate != null : pe.Procurement.ConclusionDate == null),

                        // Остальные типы не поддерживаются
                        _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")
                    };

                    return termPredicate;
                } 
                
                public static Expression<Func<ProcurementsEmployee, bool>> StagePredicateByVisa(KindOf kindOf, bool stageCompleted)
                {

                    Expression<Func<ProcurementsEmployee, bool>> stagePredicate = kindOf switch
                    {
                        KindOf.Calculating => // По визе расчета
                            pe => (stageCompleted
                            ? pe.Procurement.Calculating == true
                            : pe.Procurement.Calculating == false || pe.Procurement.Calculating == null),

                        KindOf.Purchase => // По визе закупки
                            pe => (stageCompleted
                            ? pe.Procurement.Purchase == true
                            : pe.Procurement.Purchase == false || pe.Procurement.Purchase == null)
                            && pe.Procurement.Calculating == true,

                        _ => throw new ArgumentException($"KindOf.{kindOf.ToString()} is not supported for this method")

                    };
                    return stagePredicate;
                }


            }
        }
    }
}