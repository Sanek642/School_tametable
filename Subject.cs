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
        public static void UpdateDG(DataGridView dg)
        {
            //очищаем datagrid перед заполнением
            dg.Rows.Clear();
            using (schoolContext db = new schoolContext())
            {

                // получаем объекты из бд и передаем в датагрид
                var subject = db.Subjects.ToList();
                foreach (var i in subject)
                {
                    dg.Rows.Add(i.IdSubjects, i.NameSubject, i.Successively==1?"Да":"Нет", i.Share == 1 ? "Да" : "Нет");
                }
            }
        }
        public static void TruCathcSave(schoolContext db, MainForm mainForm, Form curForm, string error)
        {
            try
            {
                db.SaveChanges();
                Subject.UpdateDG(mainForm.dataGridView2);
                curForm.Close();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {

                FormUp.MessegeOk(error);
                curForm.TopMost = true;

            }

        }

        //delete subject
        public static void Delete(long index)
        {
            using (schoolContext db = new schoolContext())
            {
                Subject? sub = db.Subjects.Where(e => e.IdSubjects == index).FirstOrDefault();
                if (sub != null)
                {
                    //удаляем объект
                    db.Subjects.Remove(sub);
                    db.SaveChanges();
                }

            }
        }

        //Получение информации о предмете из дата грид (по выделенной строке)
        public static Subject Get(Form form)
        {
            if (form is MainForm form1)
            {
                long index = FormUp.CurIndex(form1.dataGridView2);
                using schoolContext db = new();
                Subject? sub = db.Subjects.Where(e => e.IdSubjects == index).FirstOrDefault();
                if (sub != null)
                    return sub;
                else
                    return new Subject();
            }
            else
                return new Subject();

        }
    }
}
