using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace School_tametable
{
    public partial class Employee
    {
        private string email;
        public Employee()
        {
            EmployeeSubjects = new HashSet<EmployeeSubject>();
        }

        public long IdEmployess { get; set; }

        public string NameEmployess { get; set; } = null!;

        //public string Email { get; set; } = null!;
        public string Email
        {

            get { return email; }
            set
            {
                if (value == null) throw new Exception("Email не заполнен!");
                if (Regex.IsMatch(value, @"(\b\w+@[a-zA-Z_]+\b)")) email = value;
                else throw new Exception("Используйте корректный формат элетронного адреса");

            }
        }

        public virtual ICollection<EmployeeSubject> EmployeeSubjects { get; set; }

        //Получение информации о сотруднике из дата грид (по выделенной строке)
        public static Employee GetEmp(Form form, DataGridView dg)
        {
            if (form is MainForm form1)
            {

                long index = FormUp.CurIndex(dg);
                using schoolContext db = new();
                Employee? emp = db.Employees.Where(e => e.IdEmployess == index).FirstOrDefault();
                if (emp != null)
                    return emp;
                else
                    return new Employee();
            }
            else
                return new Employee();

        }

        //Обновление/загрузка данных в датагрид
        public static void UpdateDG(DataGridView dg)
        {
            //очищаем datagrid перед заполнением
            dg.Rows.Clear();
            using (schoolContext db = new schoolContext())
            {

                // получаем объекты из бд и передаем в датагрид
                var employees = db.Employees.ToList();
                foreach (var i in employees)
                {
                    dg.Rows.Add(i.IdEmployess, i.NameEmployess, i.Email);
                }
            }
        }

        //Удаление записи из справочника

        public static void DeleteEmp(long index)
        {
            using (schoolContext db = new schoolContext())
            {
                Employee? emp = db.Employees.Where(e => e.IdEmployess == index).FirstOrDefault();
                if (emp != null)
                {
                    //удаляем объект
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                }

            }
        }

        public static void TruCathcSave(schoolContext db, MainForm mainForm, Form curForm, string error)
        {
            try
            {
                db.SaveChanges();
                Employee.UpdateDG(mainForm.dataGridView1);
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



