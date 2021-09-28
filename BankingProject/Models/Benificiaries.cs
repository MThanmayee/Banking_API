using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class Benificiaries
    {
        public Benificiaries()
        {
            Transactions = new HashSet<Transactions>();
        }

        public string BenificiaryName { get; set; }
        public long? FromAccount { get; set; }
        public long ToAccount { get; set; }
        public string Ifsccode { get; set; }
        public string NickName { get; set; }

        public virtual Account FromAccountNavigation { get; set; }
        public virtual Bank IfsccodeNavigation { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
