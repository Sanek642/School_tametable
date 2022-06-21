using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class NameClass
    {
        public NameClass()
        {
            Classes = new HashSet<Class>();
        }

        public long IdNameCl { get; set; }
        public string? NameClass1 { get; set; }
        public long? Change { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public static void UpdateDG(DataGridView dg)
        {
            //очищаем datagrid перед заполнением
            dg.Rows.Clear();
            using (schoolContext db = new schoolContext())
            {

                // получаем объекты из бд и передаем в датагрид
                var classes = db.NameClasses.OrderBy(c => c.NameClass1).ToList();
                foreach (var i in classes)
                {
                    dg.Rows.Add(i.IdNameCl, i.NameClass1,i.Change);
                }
            }
        }

        public static void TruCathcSave(schoolContext db, MainForm mainForm, Form curForm, string error)
        {
            try
            {
                db.SaveChanges();
                NameClass.UpdateDG(mainForm.dataGridView4);
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
