using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Entities.DTOs
{
    public class ProcurementsGroup // Класс для группировки тендеров по историям (используется в аналитике)
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
        public int TenderCount { get; set; }
        public List<Procurement> Procurements { get; set; }
    }
}
