using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class Transactions
    {
        public int TransactionId { get; set; }
        public long? FromAccount { get; set; }
        public long? ToAccount { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string TransactionType { get; set; }
        public string MaturityInstructions { get; set; }
        public string Remarks { get; set; }

        public virtual Account FromAccountNavigation { get; set; }
        public virtual Benificiaries ToAccountNavigation { get; set; }
    }
}
