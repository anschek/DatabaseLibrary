using System.ComponentModel.DataAnnotations;

namespace DatabaseLibrary.Entities.EmployeeMuchToMany;

public partial class ProcurementsEmployee
{
    [Key]
    public int Id { get; set; }
    public int ProcurementId { get; set; }
    public int EmployeeId { get; set; }
    public string? ActionType { get; set; }

    public virtual Employee? Employee { get; set; }
    public virtual Procurement? Procurement { get; set; }
}