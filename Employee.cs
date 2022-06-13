using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeSubjects = new HashSet<EmployeeSubject>();
        }

        public long IdEmployess { get; set; }
        public string NameEmployess { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<EmployeeSubject> EmployeeSubjects { get; set; }
    }
}
