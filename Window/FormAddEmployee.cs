using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace School_tametable
{
    public partial class FormAddEmployee : Form
    {
        private MainForm form;
        public FormAddEmployee()
        {
            InitializeComponent();
        }

        public FormAddEmployee(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Добавление сотрудника
            using (schoolContext db = new schoolContext())
            {
                try

                {
                    string? name = textBox2.Text.Count() == 0 ? null : textBox2.Text;
                    string? email = textBox3.Text.Count() == 0 ? null : textBox3.Text.ToLower();
                    Employee emp = new Employee { NameEmployess = name, Email = email };
                    db.Employees.Add(emp);

                }

                catch (Exception ex)
                {

                    FormUp.MessegeOk(ex.Message);

                }

                //сохраняем изменения в БД                
                Employee.TruCathcSave(db, form, this, "ФИО и email должны быть заполнеными, уникальными и не пустыми");

            }


        }

        private void FormAddEmployee_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }
    }
}
