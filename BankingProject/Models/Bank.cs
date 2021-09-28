using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class Bank
    {
        public Bank()
        {
            Benificiaries = new HashSet<Benificiaries>();
            UserProfile = new HashSet<UserProfile>();
        }

        public string Ifsccode { get; set; }
        public string BranchName { get; set; }
        public int? Id { get; set; }

        public virtual BankMaster IdNavigation { get; set; }
        public virtual ICollection<Benificiaries> Benificiaries { get; set; }
        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
