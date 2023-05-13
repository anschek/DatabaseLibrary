﻿namespace DatabaseLibrary.Entities.ComponentCalculationProperties;

public partial class ComponentCalculation
{
    public int Id { get; set; }
    public int ProcurementId { get; set; }
    public string? PartNumber { get; set; }
    public string? ComponentName { get; set; }
    public int? ManufacturerId { get; set; }
    public decimal? Price { get; set; }
    public decimal? PricePurchase { get; set; }
    public int? Count { get; set; }
    public int? SellerId { get; set; }
    public int? ComponentStateId { get; set; }
    public DateTime? Date { get; set; }
    public string? Reserve { get; set; }
    public string? Note { get; set; }
    public string? NotePurchase { get; set; }
    public int? ComponentTypeId { get; set; }

    
    public virtual Procurement Procurement { get; set; } = null!;
    public virtual Manufacturer? Manufacturer { get; set; }
    public virtual Seller? Seller { get; set; }
    public virtual ComponentState? ComponentState { get; set; }
    public virtual ComponentType? ComponentType { get; set; }
}