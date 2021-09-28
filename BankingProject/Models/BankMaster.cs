using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class BankMaster
    {
        public BankMaster()
        {
            Bank = new HashSet<Bank>();
        }

        public int Id { get; set; }
        public string BankName { get; set; }

        public virtual ICollection<Bank> Bank { get; set; }
    }
}
