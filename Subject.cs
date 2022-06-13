using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class Subject
    {
        public Subject()
        {
            EmployeeSubjects = new HashSet<EmployeeSubject>();
        }

        public long IdSubjects { get; set; }
        public string NameSubject { get; set; } = null!;
        public long Successively { get; set; }
        public long Share { get; set; }

        public virtual ICollection<EmployeeSubject> EmployeeSubjects { get; set; }
    }
}
