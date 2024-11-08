using DatabaseLibrary.Entities.ProcurementProperties;

namespace DatabaseLibrary.Queries;

public static class PULL
{
    //public static bool City(City city)
    //{
    //    using ParsethingContext db = new();
    //    City? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Cities
    //            .Where(cs => cs.Id == city.Id)
    //            .First();

    //        def.Name = city.Name;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ComponentState(ComponentState componentState)
    //{
    //    using ParsethingContext db = new();
    //    ComponentState? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.ComponentStates
    //            .Where(cs => cs.Id == componentState.Id)
    //            .First();

    //        def.Kind = componentState.Kind;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool ComponentType(ComponentType componentType)
    //{
    //    using ParsethingContext db = new();
    //    ComponentType? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.ComponentTypes
    //            .Where(ct => ct.Id == componentType.Id)
    //            .First();

    //        def.Kind = componentType.Kind;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ComponentHeaderType(ComponentHeaderType componentHeaderType)
    //{
    //    using ParsethingContext db = new();
    //    ComponentHeaderType? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.ComponentHeaderTypes
    //            .Where(ct => ct.Id == componentHeaderType.Id)
    //            .First();

    //        def.Kind = componentHeaderType.Kind;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool Document(Document document)
    //{
    //    using ParsethingContext db = new();
    //    Document? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Documents
    //            .Where(d => d.Id == document.Id)
    //            .First();

    //        def.Title = document.Title;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Employee(Employee employee)
    //{
    //    using ParsethingContext db = new();
    //    Employee? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Employees
    //            .Include(e => e.Position)
    //            .Where(e => e.Id == employee.Id)
    //            .First();

    //        def.FullName = employee.FullName;
    //        def.UserName = employee.UserName;
    //        def.Password = employee.Password;
    //        def.PositionId = employee.PositionId;
    //        def.Photo = employee.Photo;
    //        def.IsAvailable = employee.IsAvailable;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool ComponentCalculation(ComponentCalculation componentCalculation)
    //{
    //    using ParsethingContext db = new();
    //    ComponentCalculation? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.ComponentCalculations
    //            .Where(e => e.Id == componentCalculation.Id)
    //            .First();

    //        def.IndexOfComponent = componentCalculation.IndexOfComponent;
    //        def.PartNumber = componentCalculation.PartNumber;
    //        def.HeaderTypeId = componentCalculation.HeaderTypeId;
    //        def.ComponentName = componentCalculation.ComponentName;
    //        def.ComponentNamePurchase = componentCalculation.ComponentNamePurchase;
    //        def.ManufacturerId = componentCalculation.ManufacturerId;
    //        def.ManufacturerIdPurchase = componentCalculation.ManufacturerIdPurchase;
    //        def.Price = componentCalculation.Price;
    //        def.PricePurchase = componentCalculation.PricePurchase;
    //        def.Count = componentCalculation.Count;
    //        def.CountPurchase = componentCalculation.CountPurchase;
    //        def.SellerId = componentCalculation.SellerId;
    //        def.SellerIdPurchase = componentCalculation.SellerIdPurchase;
    //        def.ComponentStateId = componentCalculation.ComponentStateId;
    //        def.Date = componentCalculation.Date;
    //        def.Reserve = componentCalculation.Reserve;
    //        def.ReservePurchase = componentCalculation.ReservePurchase;
    //        def.Note = componentCalculation.Note;
    //        def.NotePurchase = componentCalculation.NotePurchase;
    //        def.AssemblyMap = componentCalculation.AssemblyMap;
    //        def.IsDeleted = componentCalculation.IsDeleted;
    //        def.IsAdded = componentCalculation.IsAdded;
    //        def.IsHeader = componentCalculation.IsHeader;
    //        def.ParentName = componentCalculation.ParentName;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool LegalEntity(LegalEntity legalEntity)
    //{
    //    using ParsethingContext db = new();
    //    LegalEntity? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.LegalEntities
    //            .Where(m => m.Id == legalEntity.Id)
    //            .First();

    //        def.Name = legalEntity.Name;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Manufacturer(Manufacturer manufacturer)
    //{
    //    using ParsethingContext db = new();
    //    Manufacturer? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Manufacturers
    //            .Where(m => m.ManufacturerName == manufacturer.ManufacturerName)
    //            .First();

    //        def.ManufacturerName = manufacturer.ManufacturerName;
    //        def.FullManufacturerName = manufacturer.FullManufacturerName;
    //        def.ManufacturerCountryId = manufacturer.ManufacturerCountryId;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ManufacturerCountry(ManufacturerCountry manufacturerCountry)
    //{
    //    using ParsethingContext db = new();
    //    ManufacturerCountry? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.ManufacturerCountries
    //            .Where(m => m.Id == manufacturerCountry.Id)
    //            .First();

    //        def.Name = manufacturerCountry.Name;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool Minopttorg(Minopttorg minopttorg)
    //{
    //    using ParsethingContext db = new();
    //    Minopttorg? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Minopttorgs
    //            .Where(m => m.Id == minopttorg.Id)
    //            .First();

    //        def.Name = minopttorg.Name;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Position(Position position)
    //{
    //    using ParsethingContext db = new();
    //    Position? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Positions
    //            .Where(p => p.Id == position.Id)
    //            .First();

    //        def.Kind = position.Kind;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool PredefinedComponent(PredefinedComponent predefinedComponent)
    //{
    //    using ParsethingContext db = new();
    //    PredefinedComponent? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.PredefinedComponents
    //            .Where(p => p.Id == predefinedComponent.Id)
    //            .First();

    //        def.ComponentName = predefinedComponent.ComponentName;
    //        def.ManufacturerId = predefinedComponent.ManufacturerId;
    //        def.ComponentTypeId = predefinedComponent.ComponentTypeId;
    //        def.Price = predefinedComponent.Price;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool Preference(Preference preference)
    //{
    //    using ParsethingContext db = new();
    //    Preference? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Preferences
    //            .Where(p => p.Id == preference.Id)
    //            .First();

    //        def.Kind = preference.Kind;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

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

            def.Deadline = procurement.Deadline;
            def.ResultDate = procurement.ResultDate;
            def.Securing = procurement.Securing;
            def.Enforcement = procurement.Enforcement;
            def.Warranty = procurement.Warranty;
            def.RegionId = procurement.RegionId;
            def.CityId = procurement.CityId;
            def.Distance = procurement.Distance;
            def.OrganizationContractName = procurement.OrganizationContractName;
            def.OrganizationContractPostalAddress = procurement.OrganizationContractPostalAddress;
            def.ContactPerson = procurement.ContactPerson;
            def.ContactPhone = procurement.ContactPhone;
            def.OrganizationEmail = procurement.OrganizationEmail;
            def.HeadOfAcceptance = procurement.HeadOfAcceptance;
            def.DeliveryDetails = procurement.DeliveryDetails;
            def.DeadlineAndType = procurement.DeadlineAndType;
            def.DeliveryDeadline = procurement.DeliveryDeadline;
            def.AcceptanceDeadline = procurement.AcceptanceDeadline;
            def.ContractDeadline = procurement.ContractDeadline;
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
            def.ApplicationNumber = procurement.ApplicationNumber;
            def.Bet = procurement.Bet;
            def.MinimalPrice = procurement.MinimalPrice;
            def.ContractAmount = procurement.ContractAmount;
            def.ReserveContractAmount = procurement.ReserveContractAmount;
            def.IsUnitPrice = procurement.IsUnitPrice;
            def.ProtocolDate = procurement.ProtocolDate;
            def.RejectionReason = procurement.RejectionReason;
            def.CompetitorSum = procurement.CompetitorSum;
            def.ShipmentPlanId = procurement.ShipmentPlanId;
            def.WaitingList = procurement.WaitingList;
            def.Calculating = procurement.Calculating;
            def.Purchase = procurement.Purchase;
            def.ExecutionStateId = procurement.ExecutionStateId;
            def.ExecutionPrice = procurement.ExecutionPrice;
            def.ExecutionDate = procurement.ExecutionDate;
            def.WarrantyStateId = procurement.WarrantyStateId;
            def.WarrantyPrice = procurement.WarrantyPrice;
            def.WarrantyDate = procurement.WarrantyDate;
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
            def.PostingDate = procurement.PostingDate;
            def.CalculatingAmount = procurement.CalculatingAmount;
            def.PurchaseAmount = procurement.PurchaseAmount;
            def.PassportOfMonitor = procurement.PassportOfMonitor;
            def.PassportOfPc = procurement.PassportOfPc;
            def.PassportOfMonoblock = procurement.PassportOfMonoblock;
            def.PassportOfNotebook = procurement.PassportOfNotebook;
            def.PassportOfAw = procurement.PassportOfAw;
            def.PassportOfUps = procurement.PassportOfUps;
            def.IsProcurementBlocked = procurement.IsProcurementBlocked;
            def.IsPurchaseBlocked = procurement.IsPurchaseBlocked;
            def.IsCalculationBlocked = procurement.IsCalculationBlocked;
            def.ProcurementUserId = procurement.ProcurementUserId;
            def.PurchaseUserId = procurement.PurchaseUserId;
            def.CalculatingUserId = procurement.CalculatingUserId;
            def.ParentProcurementId = procurement.ParentProcurementId;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ClosingActiveSessionsByEmployee(int employeeId)
    {
        using ParsethingContext db = new();
        Procurement? def = null;
        bool isSaved = true;

        try
        {
            def = db.Procurements
                .FirstOrDefault(p => p.CalculatingUserId == employeeId || p.PurchaseUserId == employeeId || p.ProcurementUserId == employeeId);

            if (def != null)
            {
                if (def.CalculatingUserId == employeeId)
                {
                    def.CalculatingUserId = null;
                    def.IsCalculationBlocked = false;
                }

                if (def.PurchaseUserId == employeeId)
                {
                    def.PurchaseUserId = null;
                    def.IsPurchaseBlocked = false;
                }

                if (def.ProcurementUserId == employeeId)
                {
                    def.ProcurementUserId = null;
                    def.IsProcurementBlocked = false;
                }

                _ = db.SaveChanges();
            }
            else
            {
                isSaved = false;
            }
        }
        catch
        {
            isSaved = false;
        }

        return isSaved;
    }

    public static bool ProcurementsEmployee(ProcurementsEmployee procurementsEmployee, string premierPosition, string secondPosition, string thirdPosition)
    {
        using ParsethingContext db = new();
        ProcurementsEmployee? def = null;
        bool isSaved = true;

        try
        {
            if (procurementsEmployee.Procurement != null && procurementsEmployee.Employee != null)
            {
                procurementsEmployee.ProcurementId = procurementsEmployee.Procurement.Id;
                procurementsEmployee.EmployeeId = procurementsEmployee.Employee.Id;
            }
            else throw new Exception();
            procurementsEmployee.Procurement = null;
            procurementsEmployee.Employee = null;
        }
        catch { isSaved = false; }

        try
        {
            def = db.ProcurementsEmployees
                .Include(pe => pe.Employee)
                .Include(pe => pe.Employee.Position)
                .Where(pe => pe.ProcurementId == procurementsEmployee.ProcurementId && (pe.Employee.Position.Kind == premierPosition || pe.Employee.Position.Kind == secondPosition || pe.Employee.Position.Kind == thirdPosition))
                .First();

            def.ProcurementId = procurementsEmployee.ProcurementId;
            def.EmployeeId = procurementsEmployee.EmployeeId;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Procurement_ProcurementState(Procurement procurement, int procurementStateId)
    {
        using ParsethingContext db = new();
        Procurement? def = null;
        bool isSaved = true;
        try
        {
            def = db.Procurements
                .Where(p => p.Id == procurement.Id)
                .First();

            def.ProcurementStateId = procurementStateId;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ProcurementState(ProcurementState procurementState)
    {
        using ParsethingContext db = new();
        ProcurementState? def = null;
        bool isSaved = true;

        try
        {
            def = db.ProcurementStates
                .Where(p => p.Id == procurementState.Id)
                .First();

            def.Kind = procurementState.Kind;

            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }
    public static bool ProcurementSource(Procurement procurement, Procurement def)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            def = db.Procurements
                .Where(p => p.InitialPrice == procurement.InitialPrice && p.Object == procurement.Object)
                .First();

            def.RequestUri = procurement.RequestUri;
            def.LawId = procurement.LawId;
            def.Object = procurement.Object;
            def.InitialPrice = Convert.ToDecimal(DbValueConverter.ToNullableString(Convert.ToString(procurement.InitialPrice)));
            def.OrganizationId = procurement.OrganizationId;
            def.MethodId = procurement.MethodId;
            def.Location = procurement.Location;
            def.StartDate = procurement.StartDate;
            def.Deadline = procurement.Deadline;
            def.ResultDate = procurement.ResultDate;
            def.TimeZoneId = procurement.TimeZoneId;
            def.Securing = procurement.Securing;
            def.Enforcement = procurement.Enforcement;
            def.Warranty = procurement.Warranty;
            def.ContactPerson = procurement.ContactPerson;
            def.ContactPhone = procurement.ContactPhone;
            def.OrganizationEmail = procurement.OrganizationEmail;
            def.TimeZoneId = procurement.TimeZoneId;
            def.RegionId = procurement.RegionId;


            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }


    //public static bool Region(Region region)
    //{
    //    using ParsethingContext db = new();
    //    Region? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Regions
    //            .Where(r => r.Id == region.Id)
    //            .First();

    //        def.Title = region.Title;
    //        def.Distance = region.Distance;
    //        def.RegionCode = region.RegionCode;
    //        def.BicoId = region.BicoId;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Seller(Seller seller)
    //{
    //    using ParsethingContext db = new();
    //    Seller? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Sellers
    //            .Where(r => r.Id == seller.Id)
    //            .First();

    //        def.Name = seller.Name;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Tag(Tag tag)
    //{
    //    using ParsethingContext db = new();
    //    Tag? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.Tags
    //            .Where(t => t.Id == tag.Id)
    //            .First();

    //        def.Keyword = tag.Keyword;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool TagException(TagException tagException)
    //{
    //    using ParsethingContext db = new();
    //    TagException? def = null;
    //    bool isSaved = true;

    //    try
    //    {
    //        def = db.TagExceptions
    //            .Where(t => t.Id == tagException.Id)
    //            .First();

    //        def.Keyword = tagException.Keyword;

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
}