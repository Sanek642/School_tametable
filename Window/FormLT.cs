using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;

namespace School_tametable
{
    public partial class FormLT : Form
    {
        string[] day = { "Пн.","Вт.","Ср.","Чт.","Пт.","Сб."};

        MainForm form;
        bool gbd = true;
        bool gbs = true;
        bool gb2s = false;
        List<bool> boolsmgb = new List<bool>();

        bool Listfalse(List<MaskedTextBox> l)
        {
            bool b = false;
            foreach(MaskedTextBox tb in l)
            {
                if(!tb.MaskCompleted)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        public FormLT()
        {
            InitializeComponent();
        }

        public FormLT(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }

        private void FormLT_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = gbd;
            groupBox4.Visible = gbd;
            comboBox1.Visible = gbd;
            gbd = !gbd;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox5.Visible = gbs;
            groupBox6.Visible = gbs;
            checkBox3.Visible = gbs;
            gbs = !gbs;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox6.Enabled = gb2s;
            gb2s = !gb2s;
        }

        private void AddSmena(int smena,DateTime time,schoolContext db, int n, int per, int bper)
        {
            DateTime time1 = time;
            for (int i = 0; i < 6; i++) //дней всегда 6 
            {
                for (int j = 0; j < smena; j++)
                {
                    LessonsTime lessonsTime = new LessonsTime() { Change = n, DayOfWeek = day[i], Number = j + 1, TimeBeg = BitConverter.GetBytes(time1.Ticks), 
                                                                   TimeEnd = BitConverter.GetBytes((time1.AddMinutes(40).Ticks)) };
                    db.LessonsTimes.Add(lessonsTime);
                    if (j == 2 || j == 3)
                    {
                        time1 = time1.AddMinutes(40 + bper);
                    }
                    else
                    {
                        time1 = time1.AddMinutes(40 + per);
                    }
                }
                time1 = time;

            }
        }

        private void AddDSmena(int smena, DateTime time, schoolContext db, int n, int per, int bper, string d)
        {

                for (int j = 0; j < smena; j++)
                {
                    LessonsTime lessonsTime = new LessonsTime()
                    {
                        Change = n,
                        DayOfWeek = d,
                        Number = j + 1,
                        TimeBeg = BitConverter.GetBytes(time.Ticks),
                        TimeEnd = BitConverter.GetBytes((time.AddMinutes(40).Ticks))
                    };

                    db.LessonsTimes.Add(lessonsTime);

                    if (j == 2 || j == 3)
                    {
                        time = time.AddMinutes(40 + bper);
                    }
                    else
                    {
                        time = time.AddMinutes(40 + per);
                    }
                }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool ch1 = checkBox1.Checked;
            bool ch2 = checkBox2.Checked;
            bool ch3 = checkBox3.Checked;

            int smena1;
            int smena2;
            DateTime tbeg;
            int per;
            int bper;

            List<MaskedTextBox> list = new List<MaskedTextBox> { maskedTextBox1,maskedTextBox2,maskedTextBox3,maskedTextBox4,
                                                                 maskedTextBox5,maskedTextBox6,maskedTextBox7,maskedTextBox8};
            if(Listfalse(list))
            {
                FormUp.MessegeOk("Заполните все поля!");

            }

            else
            {
                using (schoolContext db = new schoolContext())

                {
                    var lt = db.LessonsTimes.ToList();

                    if (lt.Any())
                    {
                        foreach (var lt2 in lt)
                        {
                            db.LessonsTimes.Remove(lt2);
                        }

                        LessonsTime.TruCathcSave(db, form, this, "Удаляем все из таблицы!");
                    }

                    smena1 = Convert.ToInt32(maskedTextBox2.Text);
                    tbeg = Convert.ToDateTime(maskedTextBox1.Text);
                    per = Convert.ToInt32(maskedTextBox3.Text);
                    bper = Convert.ToInt32(maskedTextBox4.Text);

                    AddSmena(smena1, tbeg, db, 1, per, bper);

                    smena2 = Convert.ToInt32(maskedTextBox7.Text);
                    per = Convert.ToInt32(maskedTextBox6.Text);
                    bper = Convert.ToInt32(maskedTextBox5.Text);
                    tbeg = Convert.ToDateTime(maskedTextBox8.Text);

                    AddSmena(smena2, tbeg, db, 2, per, bper);

                    LessonsTime.TruCathcSave(db, form, this, "Добавляем");
                }

                if (ch1)
                {
                    List<MaskedTextBox> list1 = new List<MaskedTextBox> { maskedTextBox9,maskedTextBox10,maskedTextBox11,maskedTextBox12,
                                                                          maskedTextBox13,maskedTextBox14,maskedTextBox15,maskedTextBox16};
                    bool b = Listfalse(list1);
                    bool cb = string.IsNullOrEmpty(comboBox1.SelectedItem.ToString());
                    
                    if (cb || b)
                    {
                        FormUp.MessegeOk("Заполните все поля и выбирите день!");
                    }
                    else
                    {
                        using (schoolContext db = new schoolContext())
                        {
                            string str = day[comboBox1.SelectedIndex];

                            var ltday = db.LessonsTimes.Where(c => c.DayOfWeek == str).ToList();
                            if (ltday.Any())
                            {
                                foreach (var lt2 in ltday)
                                {
                                    db.LessonsTimes.Remove(lt2);
                                }
                            }

                            LessonsTime.TruCathcSave(db, form, this, "Удаляем все из таблицы день исключения!");

                            smena1 = Convert.ToInt32(maskedTextBox11.Text);
                            tbeg = Convert.ToDateTime(maskedTextBox12.Text);
                            per = Convert.ToInt32(maskedTextBox10.Text);
                            bper = Convert.ToInt32(maskedTextBox9.Text);

                            AddDSmena(smena1, tbeg, db, 1, per, bper, str);

                            smena2 = Convert.ToInt32(maskedTextBox15.Text);
                            tbeg = Convert.ToDateTime(maskedTextBox16.Text);
                            per = Convert.ToInt32(maskedTextBox14.Text);
                            bper = Convert.ToInt32(maskedTextBox13.Text);

                            AddDSmena(smena2, tbeg, db, 2, per, bper, str);

                            LessonsTime.TruCathcSave(db, form, this, "Добавляем особенный день!");

                        }


                    }

                }
                
                if(ch2)
                {
                    string str = day.Last();

                    using (schoolContext db = new schoolContext())
                    {
                        

                        var ltday = db.LessonsTimes.Where(c => c.DayOfWeek == str).ToList();
                        if (ltday.Any())
                        {
                            foreach (var lt2 in ltday)
                            {
                                db.LessonsTimes.Remove(lt2);
                            }
                        }
                        LessonsTime.TruCathcSave(db, form, this, "Удаляем субботу!");
                    }

                    if (ch3)
                    {

                        List<MaskedTextBox> list2 = new List<MaskedTextBox> { maskedTextBox17, maskedTextBox18, maskedTextBox19, maskedTextBox20 };
                        bool b = Listfalse(list2);

                        if (b)
                        {
                            FormUp.MessegeOk("Заполните все поля для субботы!");
                        }

                        else
                        {
                            using (schoolContext db = new schoolContext())
                            {
                                smena1 = Convert.ToInt32(maskedTextBox19.Text);
                                tbeg = Convert.ToDateTime(maskedTextBox20.Text);
                                per = Convert.ToInt32(maskedTextBox18.Text);
                                bper = Convert.ToInt32(maskedTextBox17.Text);

                                AddDSmena(smena1, tbeg, db, 1, per, bper, str);
                                LessonsTime.TruCathcSave(db, form, this, "Добавляем субботу!");
                            }
                        }


                    }
                    if(!ch3)
                    {
                        List<MaskedTextBox> list3 = new List<MaskedTextBox> { maskedTextBox17, maskedTextBox18, maskedTextBox19, maskedTextBox20,
                                                                              maskedTextBox21, maskedTextBox22, maskedTextBox23, maskedTextBox24};
                        bool b = Listfalse(list3);

                        if (b)
                        {
                            FormUp.MessegeOk("Заполните все поля для субботы!");
                        }
                        else
                        {
                            using (schoolContext db = new schoolContext())
                            {
                                smena1 = Convert.ToInt32(maskedTextBox19.Text);
                                tbeg = Convert.ToDateTime(maskedTextBox20.Text);
                                per = Convert.ToInt32(maskedTextBox18.Text);
                                bper = Convert.ToInt32(maskedTextBox17.Text);

                                AddDSmena(smena1, tbeg, db, 1, per, bper, str);

                                smena2 = Convert.ToInt32(maskedTextBox23.Text);
                                tbeg = Convert.ToDateTime(maskedTextBox24.Text);
                                per = Convert.ToInt32(maskedTextBox22.Text);
                                bper = Convert.ToInt32(maskedTextBox21.Text);

                                AddDSmena(smena2, tbeg, db, 2, per, bper, str);

                                LessonsTime.TruCathcSave(db, form, this, "Добавляем субботу!");
                            }

                        }
                    }
   
                }

            }

        }
    }
}
