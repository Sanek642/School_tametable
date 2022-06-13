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
    public partial class FormAddSubject : Form
    {
        MainForm form;
        public FormAddSubject()
        {
            InitializeComponent();
        }

        public FormAddSubject(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (schoolContext db = new schoolContext())
            {
                try

                {
                    string? name = textBox1.Text.Count() == 0 ? null : textBox1.Text;
                    long successively = checkBox1.Checked?1:0;
                    long share = checkBox2.Checked?1:0;
                    
                    Subject sub = new Subject { NameSubject = name, Successively = successively, Share = share };
                    db.Subjects.Add(sub);

                }

                catch (Exception ex)
                {

                    FormUp.MessegeOk(ex.Message);
                }

                //сохраняем изменения в БД                
                Subject.TruCathcSave(db, form, this, "Предмет должен иметь уникальное название и быть заполнен");

            }
        }

        private void FormAddSubject_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }
    }
}
