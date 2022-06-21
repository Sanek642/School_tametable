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
    public partial class FormEmpSubj : Form
    {
        MainForm form;
        Employee emp;
        public FormEmpSubj()
        {
            InitializeComponent();
        }

        public FormEmpSubj(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }

        private void FormEmpSubj_Load(object sender, EventArgs e)
        {

            using (schoolContext db = new schoolContext())
            {

                // получаем объекты из бд и передаем в датагрид
                var employees = db.Employees.OrderBy(c=>c.NameEmployess).ToList();
                foreach (var i in employees)
                {
                    comboBox1.Items.Add(i.NameEmployess);
                }

                var subject = db.Subjects.OrderBy(c=>c.NameSubject).ToList();
                foreach(var i in subject)
                {
                    comboBox2.Items.Add(i.NameSubject);
                }
            }
        }

        private void FormEmpSubj_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nameemp = comboBox1.SelectedItem.ToString();
            string namesub1 = comboBox2.SelectedItem.ToString();
            string? namesub2 = comboBox3.SelectedItem==null? null:comboBox3.SelectedItem.ToString();
            string? namesub3 = comboBox4.SelectedItem == null ? null : comboBox4.SelectedItem.ToString();
            string? namesub4 = comboBox5.SelectedItem == null ? null : comboBox5.SelectedItem.ToString();

            List<string> subj = new List<string>{ namesub1, namesub2, namesub3, namesub4 };

            using (schoolContext db = new schoolContext())
            {


                // получаем объекты из бд и передаем в датагрид
                var employees = db.Employees.Where(c=>c.NameEmployess==nameemp).FirstOrDefault();

                foreach (var sub in subj)
                {
                    if (sub is not null)
                    {
                        var subject = db.Subjects.Where(c => c.NameSubject == sub).FirstOrDefault();
                        EmployeeSubject es = new EmployeeSubject
                        {
                            EmployeesId = employees.IdEmployess,
                            SubjectsId = subject.IdSubjects
                        };
                        db.EmployeeSubjects.Add(es);
                        employees.EmployeeSubjects.Add(es);
                        subject.EmployeeSubjects.Add(es);
                    }
                }

                EmployeeSubject.TruCathcSave(db, form, this, "Уже указанно, что учитель ведет данный предмет!");
                
                
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox2.Enabled = false;
            checkBox1.Visible = true;
            button2.Visible = true;
           
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            button2.Visible = true;
            label3.Visible = true;
            comboBox3.Visible = true;

            comboBox3.SelectedItem = null;
            comboBox3.Items.Clear();

            using (schoolContext db = new schoolContext())
            {

                // получаем объекты из бд и передаем в датагрид иключая уже выбранный
                
                var subject = db.Subjects.Where(c => c.NameSubject != comboBox2.SelectedItem.ToString()).OrderBy(c=>c.NameSubject).ToList();
                foreach (var i in subject)
                {
                    comboBox3.Items.Add(i.NameSubject);
                }
            }


        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;

            checkBox1.Enabled = true;
            checkBox1.Checked = false;
            checkBox1.Visible = false;

            label3.Visible = false;
            comboBox3.SelectedItem = null;
            comboBox3.Items.Clear();
            comboBox3.Enabled = true;
            comboBox3.Visible = false;

            label4.Visible = false;
            comboBox4.SelectedItem = null;
            comboBox4.Items.Clear();
            comboBox4.Enabled = true;
            comboBox4.Visible = false;

            checkBox2.Enabled = true;
            checkBox2.Checked = false;
            checkBox2.Visible = false;

            label5.Visible = false;
            comboBox5.SelectedItem = null;
            comboBox5.Items.Clear();
            comboBox5.Enabled = true;
            comboBox5.Visible = false;

            checkBox3.Enabled = true;
            checkBox3.Checked = false;
            checkBox3.Visible = false;

            button2.Visible = false; 

        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                label4.Visible = true;
                comboBox4.Visible = true;

                comboBox4.SelectedItem = null;
                comboBox4.Items.Clear();

                using (schoolContext db = new schoolContext())
                {

                    // получаем объекты из бд и передаем в датагрид иключая уже выбранный

                    var subject = db.Subjects.Where(c => c.NameSubject != comboBox2.SelectedItem.ToString()
                                                         && c.NameSubject != comboBox3.SelectedItem.ToString()).OrderBy(c => c.NameSubject).ToList();
                    foreach (var i in subject)
                    {
                        comboBox4.Items.Add(i.NameSubject);
                    }
                }
            }
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                label5.Visible = true;
                comboBox5.Visible = true;

                comboBox5.SelectedItem = null;
                comboBox5.Items.Clear();

                using (schoolContext db = new schoolContext())
                {

                    // получаем объекты из бд и передаем в датагрид иключая уже выбранный

                    var subject = db.Subjects.Where(c => c.NameSubject != comboBox2.SelectedItem.ToString()
                                                         && c.NameSubject != comboBox3.SelectedItem.ToString()
                                                         && c.NameSubject != comboBox4.SelectedItem.ToString()).OrderBy(c => c.NameSubject).ToList();
                    foreach (var i in subject)
                    {
                        comboBox5.Items.Add(i.NameSubject);
                    }
                }
            }
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox4.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Visible = true;
        }

        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox5.Enabled = false;
            checkBox3.Enabled = false;
            
        }
    }
}
