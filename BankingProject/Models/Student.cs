using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class Student
    {
        public int Rollno { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string School { get; set; }
    }
}
