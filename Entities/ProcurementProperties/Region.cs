﻿namespace DatabaseLibrary.Entities.ProcurementProperties;

public partial class Region
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Distance { get; set; }
    public int? RegionCode { get; set; }
    public int? BicoId { get; set; }

    public virtual ICollection<Procurement> Procurements { get; } = new List<Procurement>();
}