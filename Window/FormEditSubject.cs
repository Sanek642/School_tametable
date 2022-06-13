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
    public partial class FormEditSubject : Form
    {
        private MainForm form;
        private Subject sub;
        public FormEditSubject()
        {
            InitializeComponent();
        }
        public FormEditSubject(MainForm f)
        {
            this.TopMost = true;
            form = f;
            sub = Subject.Get(form);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (schoolContext db = new())
            {
                //изменяем данные
                sub.NameSubject = textBox1.Text.Count() == 0 ? null : textBox1.Text;
                sub.Successively  = checkBox1.Checked ? 1 : 0;
                sub.Share = checkBox2.Checked ? 1 : 0;
                //применяем изменения
                db.Update(sub);

                //сохраняем с проверкой
                Subject.TruCathcSave(db, form, this, "Названия предметов должны быть уникальными и не пустыми");

            }
        }

        private void FormEditSubject_Load(object sender, EventArgs e)
        {
            textBox1.Text = sub.NameSubject;
            checkBox1.Checked = sub.Successively==1?true:false;
            checkBox2.Checked = sub.Share==1?true:false;

        }

        private void FormEditSubject_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }
    }
}
