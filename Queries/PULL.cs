﻿namespace DatabaseLibrary.Queries;

public static class PULL
{
    public static bool ComponentState(ComponentState componentState)
    {
        using ParsethingContext db = new();
        ComponentState? def = null;
        bool isSaved = true;

        try
        {
            def = db.ComponentStates
                .Where(cs => cs.Id == componentState.Id)
                .First();

            def.Kind = componentState.Kind;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ComponentType(ComponentType componentType)
    {
        using ParsethingContext db = new();
        ComponentType? def = null;
        bool isSaved = true;

        try
        {
            def = db.ComponentTypes
                .Where(ct => ct.Id == componentType.Id)
                .First();

            def.Kind = componentType.Kind;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Employee(Employee employee)
    {
        using ParsethingContext db = new();
        Employee? def = null;
        bool isSaved = true;

        try
        {
            def = db.Employees
                .Include(e => e.Position)
                .Where(e => e.Id == employee.Id)
                .First();

            def.FullName = employee.FullName;
            def.UserName = employee.UserName;
            def.Password = employee.Password;
            def.PositionId = employee.PositionId;
            def.Photo = employee.Photo;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Manufacturer(Manufacturer manufacturer)
    {
        using ParsethingContext db = new();
        Manufacturer? def = null;
        bool isSaved = true;

        try
        {
            def = db.Manufacturers
                .Where(m => m.Id == manufacturer.Id)
                .First();

            def.Name = manufacturer.Name;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Position(Position position)
    {
        using ParsethingContext db = new();
        Position? def = null;
        bool isSaved = true;

        try
        {
            def = db.Positions
                .Where(p => p.Id == position.Id)
                .First();

            def.Kind = position.Kind;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Procurement(Procurement procurement)
    {
        using ParsethingContext db = new();
        Procurement? def = null;
        bool isSaved = true;

        try
        {
            def = db.Procurements
                .Where(p => p.Id == procurement.Id)
                .First();

            if (def.RegionId != null)
                def.RegionId = procurement.RegionId;

            def.OrganizationContractName = procurement.OrganizationContractName;
            def.OrganizationContractPostalAddress = procurement.OrganizationContractPostalAddress;
            def.ContactPerson = procurement.ContactPerson;
            def.ContactPhone = procurement.ContactPhone;
            def.DeliveryDetails = procurement.DeliveryDetails;
            def.DeadlineAndType = procurement.DeadlineAndType;
            def.DeliveryDeadline = procurement.DeliveryDeadline;
            def.AcceptanceDeadline = procurement.AcceptanceDeadline;
            def.ContractDeadline = procurement.ContractDeadline;
            def.Indefinitely = procurement.Indefinitely;
            def.AnotherDeadline = procurement.AnotherDeadline;
            def.DeadlineAndOrder = procurement.DeadlineAndOrder;
            def.RepresentativeTypeId = procurement.RepresentativeTypeId;
            def.CommissioningWorksId = procurement.CommissioningWorksId;
            def.PlaceCount = procurement.PlaceCount;
            def.FinesAndPennies = procurement.FinesAndPennies;
            def.PenniesPerDay = procurement.PenniesPerDay;
            def.TerminationConditions = procurement.TerminationConditions;
            def.EliminationDeadline = procurement.EliminationDeadline;
            def.GuaranteePeriod = procurement.GuaranteePeriod;
            def.Inn = procurement.Inn;
            def.ContractNumber = procurement.ContractNumber;
            def.AssemblyNeed = procurement.AssemblyNeed;
            def.MinopttorgId = procurement.MinopttorgId;
            def.LegalEntityId = procurement.LegalEntityId;
            def.Applications = procurement.Applications;
            def.Bet = procurement.Bet;
            def.MinimalPrice = procurement.MinimalPrice;
            def.ContractAmount = procurement.ContractAmount;
            def.ReserveContractAmount = procurement.ReserveContractAmount;
            def.ProtocolDate = procurement.ProtocolDate;
            def.ShipmentPlanId = procurement.ShipmentPlanId;
            def.WaitingList = procurement.WaitingList;
            def.Calculating = procurement.Calculating;
            def.Purchase = procurement.Purchase;
            def.ExecutionStateId = procurement.ExecutionStateId;
            def.WarrantyStateId = procurement.WarrantyStateId;
            def.SigningDeadline = procurement.SigningDeadline;
            def.SigningDate = procurement.SigningDate;
            def.ConclusionDate = procurement.ConclusionDate;
            def.ActualDeliveryDate = procurement.ActualDeliveryDate;
            def.DepartureDate = procurement.DepartureDate;
            def.DeliveryDate = procurement.DeliveryDate;
            def.MaxAcceptanceDate = procurement.MaxAcceptanceDate;
            def.CorrectionDate = procurement.CorrectionDate;
            def.ActDate = procurement.ActDate;
            def.MaxDueDate = procurement.MaxDueDate;
            def.ClosingDate = procurement.ClosingDate;
            def.RealDueDate = procurement.RealDueDate;
            def.Amount = procurement.Amount;
            def.SignedOriginalId = procurement.SignedOriginalId;
            def.Judgment = procurement.Judgment;
            def.Fas = procurement.Fas;
            def.ProcurementStateId = procurement.ProcurementStateId;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ProcurementSource(Procurement procurement, Procurement def, bool isGetted)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            def.LawId = procurement.LawId;
            def.Object = procurement.Object;
            def.InitialPrice = procurement.InitialPrice;
            def.OrganizationId = procurement.OrganizationId;
            if (isGetted)
            {
                def.MethodId = procurement.MethodId;
                def.PlatformId = procurement.PlatformId;
                def.Location = procurement.Location;
                def.StartDate = procurement.StartDate;
                def.Deadline = procurement.Deadline;
                def.TimeZoneId = procurement.TimeZoneId;
                def.Securing = procurement.Securing;
                def.Enforcement = procurement.Enforcement;
                def.Warranty = procurement.Warranty;
            }

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Tag(Tag tag)
    {
        using ParsethingContext db = new();
        Tag? def = null;
        bool isSaved = true;

        try
        {
            def = db.Tags
                .Where(t => t.Id == tag.Id)
                .First();

            def.Keyword = tag.Keyword;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }
}