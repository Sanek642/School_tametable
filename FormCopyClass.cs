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
using System.Text.RegularExpressions;

namespace School_tametable
{
    public partial class FormCopyClass : Form
    {
        MainForm form;
        Class cl;
        string str;
        public FormCopyClass()
        {
            InitializeComponent();
        }

        public FormCopyClass(MainForm f)
        {
            form = f;
            this.TopMost = true;
            cl = Class.GetClass(form, form.dataGridView5);
            InitializeComponent();
        }


        private void FormCopyClass_Load(object sender, EventArgs e)
        {
            str = textBox1.Text = cl.NameClasses.NameClass1;
            string s;
            
            if(Regex.IsMatch(str,@"^\d\d\w$"))
            {
                s=str.Substring(0,2);
            }
            else
            {
                s = str.Substring(0,1);
            }


            using (schoolContext db = new schoolContext())
            
            {
                var clasname = db.NameClasses.Where(c => EF.Functions.Like(c.NameClass1, $"{s}%") && c.NameClass1!=str).ToList();
                if (clasname.Any())
                {
                    foreach (var c in clasname)
                    {
                        comboBox1.Items.Add(c.NameClass1);
                    }
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    FormUp.MessegeOk("Класс на параллели один!");
                    comboBox1.Enabled = false;
                    button1.Enabled = false;           
                }
            }
        }

        private void FormCopyClass_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = FormUp.MessegeYesNo("При копировании текущие записи для выбраного класса будут удалены. Продолжить?");

            string ss = comboBox1.Text.ToString();

            if (result == DialogResult.Yes)
            {
                    //Удаляем текущие записи

                using (schoolContext db = new schoolContext())
                {
                    var cn = db.Classes.Where(c=>c.NameClasses.NameClass1==ss).ToList();
                    foreach(var tmp in cn)
                    {
                        db.Classes.Remove(tmp);
                    }
                   

                    //Копируем все записи выбранного класса

                    var cs = db.NameClasses.Where(c => c.NameClass1 == ss).FirstOrDefault();

                    var cll = db.Classes.Include(c => c.NameClasses).Include(c => c.EmployeeSubject.Employees).
                    Include(c => c.EmployeeSubject.Subjects).Where(c=>c.NameClasses.NameClass1==str).ToList();

                    foreach(var tmp in cll)
                    {

                        Class @class = new Class
                        {
                            HoursPerWeek = tmp.HoursPerWeek,
                            Cabinet = tmp.Cabinet,
                            EmployeeSubjectId = tmp.EmployeeSubjectId,
                            NameClassesId = cs.IdNameCl,
                        };

                        db.Classes.Add(@class);

                    }

                    Class.TruCathcSave(db, form, this, "Аналогичные данные сохранены!");


                }


                Class.UpdateDG(form.dataGridView5);
                this.TopMost = true;
            }

            this.Close();
        }
    }
}
