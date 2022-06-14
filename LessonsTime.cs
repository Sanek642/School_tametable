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
        public string DayOfWeek { get; set; } = null!;
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
                    long longVarb = BitConverter.ToInt64(i.TimeBeg, 0);
                    DateTime dateTimeBeg = DateTime.FromBinary(longVarb);

                    long longVare = BitConverter.ToInt64(i.TimeEnd, 0);
                    DateTime dateTimeEnd = DateTime.FromBinary(longVare);

                    dg.Rows.Add(i.IdLt, i.Change, i.DayOfWeek, i.Number, dateTimeBeg.TimeOfDay, dateTimeEnd.TimeOfDay);
                }
            }
        }

        public static void TruCathcSave(schoolContext db, MainForm mainForm, Form curForm, string error)
        {
            try
            {
                db.SaveChanges();
                LessonsTime.UpdateDG(mainForm.dataGridView6);
                curForm.Close();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {

                FormUp.MessegeOk(error);
                curForm.TopMost = true;

            }

        }


    }
}
