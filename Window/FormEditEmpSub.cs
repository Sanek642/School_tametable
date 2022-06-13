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
    public partial class FormEditEmpSub : Form
    {
        MainForm form;
        EmployeeSubject empsub;
        
        public FormEditEmpSub()
        {
            InitializeComponent();
        }

        public FormEditEmpSub(MainForm f)
        {
            this.TopMost = true;
            form = f;
            empsub = EmployeeSubject.GetEmpSub(form, form.dataGridView3);
            InitializeComponent();
        }

        private void FormEditEmpSub_Load(object sender, EventArgs e)
        {
            textBox1.Text = empsub.Employees.NameEmployess;
            textBox1.Enabled = false;
            using (schoolContext db = new schoolContext())
            {
                var subject = db.Subjects.ToList();
                foreach (var i in subject)
                {
                    comboBox2.Items.Add(i.NameSubject);
                }
            }
            comboBox2.SelectedIndex = comboBox2.FindString(empsub.Subjects.NameSubject);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string namesub = comboBox2.SelectedItem.ToString();

            using (schoolContext db = new schoolContext())
            {
                var subject = db.Subjects.Where(c => c.NameSubject == namesub).FirstOrDefault();
                empsub.Subjects = subject;
                db.Update(empsub);
                EmployeeSubject.TruCathcSave(db, form, this, "Уже указанно, что учитель ведет данный предмет!");
            }
        }

        private void FormEditEmpSub_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }
    }
}
