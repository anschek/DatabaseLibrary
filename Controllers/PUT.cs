using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public static class PUT
    {
        public static async Task<bool> City(City city)
        {
            using ParsethingContext db = new();
            City? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Cities
                    .Where(cs => cs.Id == city.Id)
                    .FirstAsync();

                def.Name = city.Name;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }

        public static async Task<bool> Employee(Employee employee)
        {
            using ParsethingContext db = new();
            Employee? def = null;
            bool isSaved = true;

            try
            {
                def = await db.Employees
                    .Include(e => e.Position)
                    .Where(e => e.Id == employee.Id)
                    .FirstAsync();

                def.FullName = employee.FullName;
                def.UserName = employee.UserName;
                def.Password = employee.Password;
                def.PositionId = employee.PositionId;
                def.Photo = employee.Photo;
                def.IsAvailable = employee.IsAvailable;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }

        public static async Task<bool> ComponentCalculation(ComponentCalculation componentCalculation)
        {
            using ParsethingContext db = new();
            ComponentCalculation? def = null;
            bool isSaved = true;

            try
            {
                def = await db.ComponentCalculations
                    .Where(e => e.Id == componentCalculation.Id)
                    .FirstAsync();

                def.IndexOfComponent = componentCalculation.IndexOfComponent;
                def.PartNumber = componentCalculation.PartNumber;
                def.HeaderTypeId = componentCalculation.HeaderTypeId;
                def.ComponentName = componentCalculation.ComponentName;
                def.ComponentNamePurchase = componentCalculation.ComponentNamePurchase;
                def.ManufacturerId = componentCalculation.ManufacturerId;
                def.ManufacturerIdPurchase = componentCalculation.ManufacturerIdPurchase;
                def.Price = componentCalculation.Price;
                def.PricePurchase = componentCalculation.PricePurchase;
                def.Count = componentCalculation.Count;
                def.CountPurchase = componentCalculation.CountPurchase;
                def.SellerId = componentCalculation.SellerId;
                def.SellerIdPurchase = componentCalculation.SellerIdPurchase;
                def.ComponentStateId = componentCalculation.ComponentStateId;
                def.Date = componentCalculation.Date;
                def.Reserve = componentCalculation.Reserve;
                def.ReservePurchase = componentCalculation.ReservePurchase;
                def.Note = componentCalculation.Note;
                def.NotePurchase = componentCalculation.NotePurchase;
                def.AssemblyMap = componentCalculation.AssemblyMap;
                def.IsDeleted = componentCalculation.IsDeleted;
                def.IsAdded = componentCalculation.IsAdded;
                def.IsHeader = componentCalculation.IsHeader;
                def.ParentName = componentCalculation.ParentName;

                _ = await db.SaveChangesAsync();
            }
            catch { isSaved = false; }

            return isSaved;
        }

    }
}
