using System.Data.SqlClient;

namespace DatabaseLibrary.Queries;

public static class PUT
{
    //public static bool City(City city)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Cities.Add(city);
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
    //        _ = db.ComponentStates.Add(componentState);
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
    //        _ = db.ComponentTypes.Add(componentType);
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
    //        _ = db.ComponentHeaderTypes.Add(componentHeaderType);
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
    //        _ = db.ComponentCalculations.Add(componentCalculation);
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
    //        _ = db.Documents.Add(document);
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
    //        _ = db.Employees.Add(employee);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool History(History history)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Histories.Add(history);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Law(Law law)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Laws.Add(law);
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
    //        _ = db.LegalEntities.Add(legalEntity);
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
    //        _ = db.Manufacturers.Add(manufacturer);
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
    //        _ = db.ManufacturerCountries.Add(manufacturerCountry);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool Method(Method method)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;
    //    try
    //    {
    //        _ = db.Methods.Add(method);
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
    //        _ = db.Minopttorgs.Add(minopttorg);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Organization(Organization organization)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Organizations.Add(organization);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Platform(Platform platform)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Platforms.Add(platform);
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
    //        _ = db.Positions.Add(position);
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
    //        _ = db.PredefinedComponents.Add(predefinedComponent);
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
    //        _ = db.Preferences.Add(preference);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    //public static bool ProcurementsPreferences(ProcurementsPreference procurementsPreference)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        if (procurementsPreference.Procurement != null && procurementsPreference.Preference != null)
    //        {
    //            procurementsPreference.ProcurementId = procurementsPreference.Procurement.Id;
    //            procurementsPreference.PreferenceId = procurementsPreference.Preference.Id;
    //        }
    //        else throw new Exception();
    //        procurementsPreference.Procurement = null;
    //        procurementsPreference.Preference = null;

    //        _ = db.ProcurementsPreferences.Add(procurementsPreference);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool ProcurementsDocuments(ProcurementsDocument procurementsDocument)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        if (procurementsDocument.Procurement != null && procurementsDocument.Document != null)
    //        {
    //            procurementsDocument.ProcurementId = procurementsDocument.Procurement.Id;
    //            procurementsDocument.DocumentId = procurementsDocument.Document.Id;
    //        }
    //        else throw new Exception();
    //        procurementsDocument.Procurement = null;
    //        procurementsDocument.Document = null;

    //        _ = db.ProcurementsDocuments.Add(procurementsDocument);
    //        _ = db.SaveChanges();
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
    //        _ = db.ProcurementStates.Add(procurementState);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
    public static async Task<bool> ProcurementSource(Procurement procurement)
    {
        using ParsethingContext db = new();
        bool isSaved = true;
        Procurement? def = null;

        try
        {
            def = await db.Procurements
                .Where(p => p.InitialPrice == procurement.InitialPrice && p.Object == procurement.Object)
                .FirstAsync();
        }
        catch { }

        try
        {
            if (def == null)
            {
                DeletedProcurement? deletedProcurement = Controllers.GET.Procurements.One.Deleted(procurement.Number);
                if (deletedProcurement == null)
                {
                    procurement.ProcurementStateId = 20;
                    if (procurement.TimeZoneId == null)
                    {
                        TimeZone? timeZone = await Controllers.GET.TimeZones.One("см. zakupki.gov.ru");
                        if (timeZone != null)
                        {
                            procurement.TimeZoneId = timeZone.Id;
                        }
                        else
                        {
                            _ = await Controllers.POST.TimeZone(new()
                            {
                                Offset = "см. zakupki.gov.ru"
                            });
                            timeZone = await Controllers.GET.TimeZones.One("см. zakupki.gov.ru");
                            procurement.TimeZoneId = timeZone != null ? timeZone.Id : 0;
                        }
                    }
                    _ = await db.Procurements.AddAsync(procurement);
                    _ = await db.SaveChangesAsync();
                }
                else
                    return isSaved = false;
            }
            else if (!PULL.ProcurementSource(procurement, def))
                throw new Exception();
        }
        catch { isSaved = false; }

        return isSaved;
    }


    //public static bool ProcurementsEmployeesBy(ProcurementsEmployee procurementsEmployee, string premierPosition, string secondPosition, string thirdPosition)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;
    //    ProcurementsEmployee? def = null;
    //    try
    //    {
    //        if (procurementsEmployee.Procurement != null && procurementsEmployee.Employee != null)
    //        {
    //            procurementsEmployee.ProcurementId = procurementsEmployee.Procurement.Id;
    //            procurementsEmployee.EmployeeId = procurementsEmployee.Employee.Id;
    //        }
    //        else throw new Exception();
    //        procurementsEmployee.Procurement = null;
    //        procurementsEmployee.Employee = null;
    //    }
    //    catch { isSaved = false; }
    //    try
    //    {
    //        def = db.ProcurementsEmployees
    //            .Include(pe => pe.Employee)
    //            .Include(pe => pe.Employee.Position)
    //            .Where(pe => pe.ProcurementId == procurementsEmployee.ProcurementId && (pe.Employee.Position.Kind == premierPosition || pe.Employee.Position.Kind == secondPosition || pe.Employee.Position.Kind == thirdPosition))
    //            .First();
    //    }
    //    catch { }
    //    try
    //    {
    //        if (def == null)
    //        {
    //            _ = db.ProcurementsEmployees.Add(procurementsEmployee);
    //            _ = db.SaveChanges();
    //        }
    //        else if (!PULL.ProcurementsEmployee(procurementsEmployee, premierPosition, secondPosition, thirdPosition))
    //            throw new Exception();
    //    }
    //    catch { isSaved = false; }


    //    return isSaved;
    //}

    //public static bool ProcurementsEmployeesBy(int employeeId)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        var procurementToAssign = db.Procurements
    //            .Include(p => p.ProcurementState)
    //                .Include(p => p.Law)
    //                .Where(p => p.ProcurementState.Kind == "Новый"
    //                            && !db.ProcurementsEmployees.Any(pe => pe.ProcurementId == p.Id))
    //                .OrderBy(p => p.Deadline)
    //                .ThenBy(p => p.Law.Number)  // Сортировка по номеру закона
    //                .ThenBy(p => p.Object.Contains("компьютер") ? 1 : 0)  // Проверка на наличие слова "компьютер"
    //                .ThenBy(p => p.Object.Contains("системный") ? 1 : 0)  // Проверка на наличие слова "системный"
    //                .ThenBy(p => p.Object.Contains("моноблок") ? 1 : 0)   // Проверка на наличие слова "моноблок"
    //                .ThenBy(p => p.Object.Contains("автоматизирован") ? 1 : 0)  // Проверка на наличие слова "автоматизирован"
    //                .ThenBy(p => p.Object.Contains("монитор") ? 1 : 0)    // Проверка на наличие слова "монитор"
    //                .ThenBy(p => p.Object.Contains("ноутбук") ? 1 : 0)    // Проверка на наличие слова "ноутбук"
    //                .FirstOrDefault();

    //        if (procurementToAssign != null)
    //            if (procurementToAssign != null)
    //            {
    //                ProcurementsEmployee procurementEmployee = new ProcurementsEmployee
    //                {
    //                    ProcurementId = procurementToAssign.Id,
    //                    EmployeeId = employeeId
    //                };

    //                db.ProcurementsEmployees.Add(procurementEmployee);
    //                db.SaveChanges();
    //            }
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool ProcurementsEmployees(ProcurementsEmployee procurementsEmployee)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        if (procurementsEmployee.Procurement != null && procurementsEmployee.Employee != null)
    //        {
    //            procurementsEmployee.ProcurementId = procurementsEmployee.Procurement.Id;
    //            procurementsEmployee.EmployeeId = procurementsEmployee.Employee.Id;
    //        }
    //        else throw new Exception();
    //        procurementsEmployee.Procurement = null;
    //        procurementsEmployee.Employee = null;

    //        _ = db.ProcurementsEmployees.Add(procurementsEmployee);
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
    //        _ = db.Regions.Add(region);
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
    //        _ = db.Sellers.Add(seller);
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
    //        _ = db.Tags.Add(tag);
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
    //        _ = db.TagExceptions.Add(tagException);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool TimeZone(TimeZone timeZone)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.TimeZones.Add(timeZone);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}

    //public static bool Comment(Comment comment)
    //{
    //    using ParsethingContext db = new();
    //    bool isSaved = true;

    //    try
    //    {
    //        _ = db.Comments.Add(comment);
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
    //        _ = db.Procurements.Add(procurement);
    //        _ = db.SaveChanges();
    //    }
    //    catch { isSaved = false; }

    //    return isSaved;
    //}
}