using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Account = new HashSet<Account>();
        }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public long? MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Aadhar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BranchIfsc { get; set; }
        public string AccountStatus { get; set; }
        public int? ReferenceNumber { get; set; }
        public string ResAddressLine1 { get; set; }
        public string ResAddressLine2 { get; set; }
        public string ResLandmark { get; set; }
        public string ResState { get; set; }
        public string ResCity { get; set; }
        public int? ResPincode { get; set; }
        public string PerAddressLine1 { get; set; }
        public string PerAddressLine2 { get; set; }
        public string PerLandmark { get; set; }
        public string PerState { get; set; }
        public string PerCity { get; set; }
        public int? PerPincode { get; set; }
        public string OccupationType { get; set; }
        public string SourceOfIncome { get; set; }
        public decimal? GrossAnnualIncome { get; set; }

        public virtual Bank BranchIfscNavigation { get; set; }
        public virtual ICollection<Account> Account { get; set; }
    }
}
