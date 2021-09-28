using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class Account
    {
        public Account()
        {
            Benificiaries = new HashSet<Benificiaries>();
            Transactions = new HashSet<Transactions>();
        }

        public long AccountNumber { get; set; }
        public int? CustomerId { get; set; }
        public string AccountType { get; set; }
        public decimal? Balance { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TransactionPassword { get; set; }

        public virtual UserProfile EmailNavigation { get; set; }
        public virtual ICollection<Benificiaries> Benificiaries { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
