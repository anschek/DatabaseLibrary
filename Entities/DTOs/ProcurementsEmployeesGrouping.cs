using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Entities.DTOs
{
    public class ProcurementsEmployeesGrouping // Класс для формирования результатов запросов на группировку
    {
        public string Id { get; set; }
        public int CountOfProcurements { get; set; }
        public List<Procurement> Procurements { get; set; }

        public decimal TotalAmount => Procurements?.Sum(p =>
        p.ReserveContractAmount != null && p.ReserveContractAmount != 0 ?
        p.ReserveContractAmount.Value :
        (p.ContractAmount != null && p.ContractAmount != 0 ?
        p.ContractAmount.Value :
        p.InitialPrice)) ?? 0m;
    }

}
