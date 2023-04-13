﻿namespace DatabaseLibrary.Queries;

public static class DELETE
{
    public static bool ComponentState(ComponentState componentState)
    {
        using ParsethingContext db = new();
        bool isSaved = true;

        try
        {
            _ = db.ComponentStates.Remove(componentState);
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
            _ = db.ComponentTypes.Remove(componentType);
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
            _ = db.Employees.Remove(employee);
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
            _ = db.Manufacturers.Remove(manufacturer);
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
            _ = db.Positions.Remove(position);
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
            _ = db.Tags.Remove(tag);
            _ = db.SaveChanges();
        }
        catch { isSaved = false; }

        return isSaved;
    }
}