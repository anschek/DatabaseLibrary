using DatabaseLibrary.Entities.PreferenceMuchToMany;

namespace DatabaseLibrary.Queries;

public static class DELETE
{
    //public static bool City(City city)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Cities.Remove(city);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ComponentState(ComponentState componentState)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.ComponentStates.Remove(componentState);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ComponentHeaderType(ComponentHeaderType componentHeaderType)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.ComponentHeaderTypes.Remove(componentHeaderType);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ComponentCalculation(ComponentCalculation componentCalculation)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.ComponentCalculations.Remove(componentCalculation);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ComponentCalculation(int id)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;
    //    try
    //    {
    //        var componentCalculationToDelete = db.ComponentCalculations.Where(cc => cc.Id == id || cc.ParentName == id);
    //        db.ComponentCalculations.RemoveRange(componentCalculationToDelete);

    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }
    //    return isSaved;
    //}

    //public static bool ComponentType(ComponentType componentType)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.ComponentTypes.Remove(componentType);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Document(Document document)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Documents.Remove(document);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Employee(Employee employee)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Employees.Remove(employee);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool LegalEntity(LegalEntity legalEntity)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;
    //    try
    //    {
    //        _ = db.LegalEntities.Remove(legalEntity);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Manufacturer(Manufacturer manufacturer)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;
    //    try
    //    {
    //        _ = db.Manufacturers.Remove(manufacturer);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ManufacturerCountry(ManufacturerCountry manufacturerCountry)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;
    //    try
    //    {
    //        _ = db.ManufacturerCountries.Remove(manufacturerCountry);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool Minopttorg(Minopttorg minopttorg)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;
    //    try
    //    {
    //        _ = db.Minopttorgs.Remove(minopttorg);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Position(Position position)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Positions.Remove(position);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool PredefinedComponent(PredefinedComponent predefinedComponent)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.PredefinedComponents.Remove(predefinedComponent);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool Preference(Preference preference)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Preferences.Remove(preference);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ProcurementPreference(ProcurementsPreference procurementsPreference)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        var procurementToDelete = db.ProcurementsPreferences.FirstOrDefault(pp => pp.ProcurementId == procurementsPreference.ProcurementId && pp.PreferenceId == procurementsPreference.PreferenceId);
    //        if (procurementsPreference != null)
    //        {
    //            _ = db.ProcurementsPreferences.Remove(procurementToDelete);
    //            _ = db.SaveChanges();
    //        }
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool ProcurementDocument(ProcurementsDocument procurementsDocument)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        var procurementToDelete = db.ProcurementsDocuments.FirstOrDefault(pp => pp.ProcurementId == procurementsDocument.ProcurementId && pp.DocumentId == procurementsDocument.DocumentId);
    //        if (procurementsDocument != null)
    //        {
    //            _ = db.ProcurementsDocuments.Remove(procurementToDelete);
    //            _ = db.SaveChanges();
    //        }
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool ProcurementState(ProcurementState procurementState)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.ProcurementStates.Remove(procurementState);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Region(Region region)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Regions.Remove(region);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Seller(Seller seller)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Sellers.Remove(seller);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Tag(Tag tag)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Tags.Remove(tag);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool TagException(TagException tagException)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.TagExceptions.Remove(tagException);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Procurement(Procurement procurement)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Procurements.Remove(procurement);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
}