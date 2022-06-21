using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public static void UpdateDG(DataGridView dg)
        {
            //очищаем datagrid перед заполнением
            dg.Rows.Clear();
            using (schoolContext db = new schoolContext())
            {

                // получаем объекты из бд и передаем в датагрид
                //var es = db.EmployeeSubjects.
                var cl = db.Classes.Include(c=>c.NameClasses).Include(c=>c.EmployeeSubject.Employees).
                    Include(c=>c.EmployeeSubject.Subjects).ToList();


                foreach (var i in cl)
                {
                    dg.Rows.Add(i.ClassesId, i.NameClasses.NameClass1, i.EmployeeSubject.Employees.NameEmployess, i.EmployeeSubject.Subjects.NameSubject,i.HoursPerWeek, i.Cabinet);
                }
            }
        }

        public static void TruCathcSave(schoolContext db, MainForm mainForm, Form curForm, string error)
        {
            try
            {
                db.SaveChanges();
                Class.UpdateDG(mainForm.dataGridView5);
                curForm.Close();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {

                FormUp.MessegeOk(error);
                curForm.TopMost = true;

            }

        }
        public static void Delete(long index)
        {
            using (schoolContext db = new schoolContext())
            {
                Class? empsub = db.Classes.Where(e => e.ClassesId == index).FirstOrDefault();
                if (empsub != null)
                {
                    //удаляем объект
                    db.Classes.Remove(empsub);
                    db.SaveChanges();
                }

            }
        }

        public static Class GetClass(Form form, DataGridView dg)
        {
            if (form is MainForm form1)
            {
                long index = FormUp.CurIndex(dg);
                using schoolContext db = new();

                Class? emp = db.Classes.Include(c => c.NameClasses).Include(c => c.EmployeeSubject.Employees).
                                        Include(c=>c.EmployeeSubject.Subjects).Where(e => e.ClassesId == index).FirstOrDefault();
                if (emp != null)
                    return emp;
                else
                    return new Class();
            }
            else
                return new Class();

        }

    }
}
