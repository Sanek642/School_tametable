using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class LessonsTime
    {
        public LessonsTime()
        {
            Lessons = new HashSet<Lesson>();
        }

        public long IdLt { get; set; }
        public long DayOfWeek { get; set; }
        public long Change { get; set; }
        public long Number { get; set; }
        public byte[] TimeBeg { get; set; } = null!;
        public byte[] TimeEnd { get; set; } = null!;

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
