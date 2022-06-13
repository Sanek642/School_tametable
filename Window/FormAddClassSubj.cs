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
    public partial class FormAddClassSubj : Form
    {
        MainForm form;
        NameClass cl;
        public FormAddClassSubj()
        {
            InitializeComponent();
        }

        public FormAddClassSubj(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();

            using (schoolContext db = new schoolContext())
            {
                // получаем объекты из бд и передаем в датагрид
                var nclass = db.NameClasses.OrderBy(c => c.NameClass1).ToList();
                foreach (var i in nclass)
                {
                    comboBox1.Items.Add(i.NameClass1);
                }

                var es = db.EmployeeSubjects.Include(c => c.Employees).Include(c => c.Subjects).ToList();
                foreach (var i in es)
                {
                    comboBox2.Items.Add(i.Employees.NameEmployess + "\\" + i.Subjects.NameSubject);
                }
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
        }

        public FormAddClassSubj(MainForm f, object si, NameClass nc)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
            comboBox1.Items.Add(si);
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.Enabled = false;

            using (schoolContext db = new schoolContext())
            {
                long[] su = db.Classes.Where(c => c.NameClasses == nc).Select(c => c.EmployeeSubjectId).ToArray();
                var es = db.EmployeeSubjects.Include(c => c.Employees).Include(c => c.Subjects).Where(c=>!su.Contains(c.IdEs)).ToList();

                foreach (var i in es)
                {
                    comboBox2.Items.Add(i.Employees.NameEmployess + "\\" + i.Subjects.NameSubject);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string classn = comboBox1.SelectedItem == null ? null : comboBox1.SelectedItem.ToString();
            string subj = comboBox2.SelectedItem == null ? null : comboBox2.SelectedItem.ToString();
            
            string hw = maskedTextBox1.MaskCompleted ? maskedTextBox1.Text : null;
            string cab = maskedTextBox2.MaskCompleted? maskedTextBox2.Text:null;


            if (string.IsNullOrEmpty(classn) || string.IsNullOrEmpty(subj) || string.IsNullOrEmpty(hw) || string.IsNullOrEmpty(cab))
            {
                FormUp.MessegeOk("Заполните поля!");
            }
            else
            {
                using (schoolContext db = new schoolContext())
                {
                    string[] es = subj.Split("\\");    
                    cl = db.NameClasses.Where(c => c.NameClass1 == classn).FirstOrDefault();
                    var sub = db.EmployeeSubjects.Include(c=>c.Subjects).Where(c=>c.Subjects.NameSubject == es[1]).
                                                    Include(c => c.Employees).Where(c => c.Employees.NameEmployess == es[0]).FirstOrDefault();
                    Class @class = new Class
                    { HoursPerWeek = Convert.ToInt64(hw),
                        Cabinet = cab,
                        EmployeeSubjectId = sub.IdEs,
                        NameClassesId = cl.IdNameCl,                 
                    };

                    db.Classes.Add(@class);
                    cl.Classes.Add(@class);
                    sub.Classes.Add(@class);

                   Class.TruCathcSave(db, form, this, "Аналогичные данные сохранены!");
                }
            }
        }

        private void FormAddClassSubj_Load(object sender, EventArgs e)
        {
            
        }

        private void FormAddClassSubj_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool plussclass = checkBox1.Checked;
            if (plussclass)
            {
                FormAddClassSubj f = new FormAddClassSubj(form, comboBox1.SelectedItem, cl);
                f.Show();
            }
            else
            {
                form.Enabled = true;
                form.TopMost = true;
            }
        }
    }
}
