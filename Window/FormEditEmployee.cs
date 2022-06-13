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
    public partial class FormEditEmployee : Form
    {
        private MainForm form;
        private Employee emp;
        public FormEditEmployee(MainForm f)
        {
            this.TopMost = true;
            form = f;
            emp = Employee.GetEmp(form,form.dataGridView1);
            InitializeComponent();
        }

        public FormEditEmployee()
        {
            InitializeComponent();

        }


        private void FormEditEmployee_Load(object sender, EventArgs e)
        {
            //получаем текущее значение из datagrid и заполняем форму
            textBox2.Text = emp.NameEmployess;
            textBox3.Text = emp.Email;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (schoolContext db = new())
            {
                try
                {
                    //изменяем данные 
                    emp.NameEmployess = textBox2.Text.Count() == 0 ? null : textBox2.Text;
                    emp.Email = textBox3.Text.Count() == 0 ? null : textBox3.Text.ToLower();

                    //применяем изменения
                    db.Update(emp);

                }
                catch (Exception ex)
                {
                    FormUp.MessegeOk(ex.Message);

                }

                //сохраняем с проверкой
                Employee.TruCathcSave(db, form, this, "ФИО и email должны быть уникальными и не пустыми");

            }


        }

        private void FormEditEmployee_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }
    }
}
