using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class Class
    {
        public Class()
        {
            Lessons = new HashSet<Lesson>();
        }

        public long HoursPerWeek { get; set; }
        public string Cabinet { get; set; } = null!;
        public long EmployeeSubjectId { get; set; }
        public long ClassesId { get; set; }
        public long NameClassesId { get; set; }

        public virtual EmployeeSubject EmployeeSubject { get; set; } = null!;
        public virtual NameClass NameClasses { get; set; } = null!;
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
