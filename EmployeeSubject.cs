using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public static void UpdateDG(DataGridView dg)
        {
            //очищаем datagrid перед заполнением
            dg.Rows.Clear();
            using (schoolContext db = new schoolContext())
            {
                // получаем связные объекты

               var es = db.EmployeeSubjects.Include(c => c.Employees).Include(c=>c.Subjects).ToList();

                foreach (var i in es)
                {
                   dg.Rows.Add(i.IdEs,i.Employees.NameEmployess, i.Subjects.NameSubject);
                }
            }
        }

        public static void TruCathcSave(schoolContext db, MainForm mainForm, Form curForm, string error)
        {
            try
            {
                db.SaveChanges();
                EmployeeSubject.UpdateDG(mainForm.dataGridView3);
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
                EmployeeSubject? empsub = db.EmployeeSubjects.Where(e => e.IdEs == index).FirstOrDefault();
                if (empsub != null)
                {
                    //удаляем объект
                    db.EmployeeSubjects.Remove(empsub);
                    db.SaveChanges();
                }

            }
        }

        public static EmployeeSubject GetEmpSub(Form form, DataGridView dg)
        {
            if (form is MainForm form1)
            {
                long index = FormUp.CurIndex(dg);
                using schoolContext db = new();
                EmployeeSubject? emp = db.EmployeeSubjects.Include(c => c.Employees).Include(c => c.Subjects).Where(e => e.IdEs == index).FirstOrDefault();
                if (emp != null)
                    return emp;
                else
                    return new EmployeeSubject();
            }
            else
                return new EmployeeSubject();

        }

    }
}
