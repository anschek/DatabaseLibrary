using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Entities.DTOs
{
    public enum KindOf // Перечисление для типизации запросов
    {
        ProcurementState, // Статус тендера
        ShipmentPlane, // План отгрузки
        Applications, // Статус "По заявкам"
        StartDate, // Дата начала подачи заявок
        Deadline, // Дата окончания подачи заявок
        ResultDate, // Дата подведения итогов
        CorrectionDate, // Дата исправления недостатков
        Judgement, // Суд
        FAS, // ФАС
        ContractConclusion, // Дата подписания контракта
        ExecutionState, // Статус обеспечения исполнения заявки
        WarrantyState, // Статус обеспечения гарантии заявки
        Calculating, // Виза расчетчиков
        Purchase, // Виза закупки
        IsUnitPrice // Цена за единицу товара
    }
}
