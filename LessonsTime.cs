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

        public static void UpdateDG(DataGridView dg)
        {
            //очищаем datagrid перед заполнением
            dg.Rows.Clear();
            using (schoolContext db = new schoolContext())
            {

                // получаем объекты из бд и передаем в датагрид
                var lt = db.LessonsTimes.ToList();
                foreach (var i in lt)
                {
                    dg.Rows.Add(i.IdLt, i.Change, i.DayOfWeek, i.Lessons, i.TimeBeg, i.TimeEnd);
                }
            }
        }
    }
}
