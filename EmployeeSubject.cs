using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class EmployeeSubject
    {
        public EmployeeSubject()
        {
            Classes = new HashSet<Class>();
        }

        public long IdEs { get; set; }
        public long? EmployeesId { get; set; }
        public long? SubjectsId { get; set; }

        public virtual Employee? Employees { get; set; }
        public virtual Subject? Subjects { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}
