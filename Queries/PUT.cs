﻿namespace DatabaseLibrary.Queries;

public static class PUT
{
    public static bool Component(Component component)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Components.Add(component);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ComponentState(ComponentState componentState)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.ComponentStates.Add(componentState);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ComponentType(ComponentType componentType)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.ComponentTypes.Add(componentType);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Document(Document document)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Documents.Add(document);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Employee(Employee employee)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Employees.Add(employee);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool History(History history)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Histories.Add(history);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Law(Law law)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Laws.Add(law);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool LegalEntity(LegalEntity legalEntity)
    {
        using ParsethingContext db = new();
        bool isSaved = true;
        try
        {
            _ = db.LegalEntities.Add(legalEntity);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Manufacturer(Manufacturer manufacturer)
    {
        using ParsethingContext db = new();
        bool isSaved = true;
        try
        {
            _ = db.Manufacturers.Add(manufacturer);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Method(Method method)
    {
        using ParsethingContext db = new();
        bool isSaved = true;
        try
        {
            _ = db.Methods.Add(method);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Minopttorg(Minopttorg minopttorg)
    {
        using ParsethingContext db = new();
        bool isSaved = true;
        try
        {
            _ = db.Minopttorgs.Add(minopttorg);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Organization(Organization organization)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Organizations.Add(organization);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Platform(Platform platform)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Platforms.Add(platform);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Position(Position position)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Positions.Add(position);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Preference(Preference preference)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Preferences.Add(preference);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }
    public static bool ProcurementsPreferences(ProcurementsPreference procurementsPreference)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            if (procurementsPreference.Procurement != null && procurementsPreference.Preference != null)
            {
                procurementsPreference.ProcurementId = procurementsPreference.Procurement.Id;
                procurementsPreference.PreferenceId = procurementsPreference.Preference.Id;
            }
            else throw new Exception();
            procurementsPreference.Procurement = null;
            procurementsPreference.Preference = null;

            _ = db.ProcurementsPreferences.Add(procurementsPreference);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ProcurementState(ProcurementState procurementState)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.ProcurementStates.Add(procurementState);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ProcurementSource(Procurement procurement, bool isGetted)
    {
        using ParsethingContext db = new();
        bool isSaved = true;
        Procurement? def = null;

        try
        {
            def = db.Procurements
                .Where(p => p.Number == procurement.Number)
                .First();
        }
        catch { }

        try
        {
            if (def == null)
            {
                procurement.ProcurementStateId = 20;

                _ = db.Procurements.Add(procurement);
                _ = db.SaveChanges();
            }
            else if (!PULL.ProcurementSource(procurement, def, isGetted))
                throw new Exception();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool ProcurementsEmployees(ProcurementsEmployee procurementsEmployee)
    {
        using ParsethingContext db = new();
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

            _ = db.ProcurementsEmployees.Add(procurementsEmployee);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Region(Region region)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Regions.Add(region);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Seller(Seller seller)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Sellers.Add(seller);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool Tag(Tag tag)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.Tags.Add(tag);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }

    public static bool TimeZone(TimeZone timeZone)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.TimeZones.Add(timeZone);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }
}