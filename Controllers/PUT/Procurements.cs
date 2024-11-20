using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Controllers
{
    public partial class PUT
    {
        public static class Procurements
        {
            public static async Task<bool> One(Procurement procurement)
            {
                using ParsethingContext db = new();
                Procurement? def = null;
                bool isSaved = true;

                try
                {
                    def = await db.Procurements
                        .Where(p => p.Id == procurement.Id)
                        .FirstAsync();

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

                    _ = await db.SaveChangesAsync();
                }
                catch { isSaved = false; }

                return isSaved;
            }
            public static async Task<bool> Source(Procurement procurement, Procurement def)
            {
                using ParsethingContext db = new();
                bool isSaved = true;

                try
                {
                    def = await db.Procurements
                        .Where(p => p.InitialPrice == procurement.InitialPrice && p.Object == procurement.Object)
                        .FirstAsync();

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


                    _ = await db.SaveChangesAsync();
                }
                catch { isSaved = false; }

                return isSaved;
            }

            public static async Task<bool> ProcurementState(Procurement procurement, int procurementStateId)
            {
                using ParsethingContext db = new();
                Procurement? def = null;
                bool isSaved = true;
                try
                {
                    def = await db.Procurements
                        .Where(p => p.Id == procurement.Id)
                        .FirstAsync();

                    def.ProcurementStateId = procurementStateId;

                    _ = await db.SaveChangesAsync();
                }
                catch { isSaved = false; }

                return isSaved;
            }

        }

    }
}
