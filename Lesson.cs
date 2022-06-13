using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class Lesson
    {
        public long IdLesson { get; set; }
        public long? LessonsTimeId { get; set; }
        public long? ClassesId { get; set; }

        public virtual Class? Classes { get; set; }
        public virtual LessonsTime? LessonsTime { get; set; }
    }
}
