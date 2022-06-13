using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace School_tametable
{
    public partial class FormEditClassSubj : Form
    {
        MainForm form;
        Class cl;
        public FormEditClassSubj()
        {
            InitializeComponent();
        }

        public FormEditClassSubj(MainForm f)
        {
            form = f;
            this.TopMost = true;
            cl = Class.GetClass(form, form.dataGridView5);
            InitializeComponent();
        }

        private void FormEditClassSubj_Load(object sender, EventArgs e)
        {
            textBox1.Text = cl.NameClasses.NameClass1;
            textBox1.Enabled = false;
            maskedTextBox1.Text = cl.HoursPerWeek.ToString();
            maskedTextBox2.Text = cl.Cabinet.ToString();
            using (schoolContext db = new schoolContext())
            {
                var es = db.EmployeeSubjects.Include(c => c.Employees).Include(c => c.Subjects).ToList();
                foreach (var i in es)
                {
                    comboBox2.Items.Add(i.Employees.NameEmployess + "\\" + i.Subjects.NameSubject);
                }
            }

            comboBox2.SelectedIndex = comboBox2.FindString(cl.EmployeeSubject.Employees.NameEmployess + "\\" + cl.EmployeeSubject.Subjects.NameSubject);

        }

        private void FormEditClassSubj_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subj = comboBox2.SelectedItem == null ? null : comboBox2.SelectedItem.ToString();
            string hw = maskedTextBox1.MaskCompleted ? maskedTextBox1.Text : null;
            string cab = maskedTextBox2.MaskCompleted ? maskedTextBox2.Text : null;


            if (string.IsNullOrEmpty(subj) || string.IsNullOrEmpty(hw) || string.IsNullOrEmpty(cab))
            {
                FormUp.MessegeOk("Заполните поля!");
            }

            else
            {
                using (schoolContext db = new schoolContext())
                {
                    string[] es = subj.Split("\\");
                    var sub = db.EmployeeSubjects.Include(c => c.Subjects).Where(c => c.Subjects.NameSubject == es[1]).
                                                  Include(c => c.Employees).Where(c => c.Employees.NameEmployess == es[0]).FirstOrDefault();
                    cl.EmployeeSubject = sub;
                    cl.HoursPerWeek = Convert.ToInt64(hw);
                    cl.Cabinet = cab;

                    db.Update(cl);

                    Class.TruCathcSave(db, form, this, "Аналогичные данные сохранены!");
                }
            }
        }
    }
}
