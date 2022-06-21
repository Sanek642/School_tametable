using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace School_tametable
{
    public partial class Lesson
    {
        public long IdLesson { get; set; }
        public long? LessonsTimeId { get; set; }
        public long? ClassesId { get; set; }

        public virtual Class? Classes { get; set; }
        public virtual LessonsTime? LessonsTime { get; set; }

        public static void UpdateDG(DataGridView dg)
        {
            //очищаем datagrid перед заполнением
            dg.Rows.Clear();
            using (schoolContext db = new schoolContext())
            {
                // получаем связные объекты

                var es = db.Lessons.Include(c => c.LessonsTime).Include(c => c.Classes.NameClasses).Include(c => c.Classes.EmployeeSubject.Subjects).ToList();

                foreach (var i in es)
                {
                    dg.Rows.Add(i.IdLesson, i.LessonsTime.DayOfWeek, i.Classes.NameClasses.NameClass1,i.LessonsTime.Number,i.Classes.EmployeeSubject.Subjects.NameSubject, i.Classes.Cabinet);
                }
            }
        }

        public static void TruCathcSave(schoolContext db, MainForm mainForm, string error)
        {
            try
            {
                db.SaveChanges();
                     
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {

                FormUp.MessegeOk(error);
                mainForm.TopMost = true;

            }

        }
    }
}
