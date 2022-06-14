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

        private void button1_Click(object sender, EventArgs e)
        {
            bool ch1 = checkBox1.Checked;
            bool ch2 = checkBox2.Checked;
            bool ch3 = checkBox3.Checked;

            List<MaskedTextBox> list = new List<MaskedTextBox> { maskedTextBox1,maskedTextBox2,maskedTextBox3,maskedTextBox4,
                                                                 maskedTextBox5,maskedTextBox6,maskedTextBox7,maskedTextBox8};
            if(Listfalse(list))
            {
                FormUp.MessegeOk("Заполните все поля!");
            }
            else
            {
                if(ch1)
                {
                    List<MaskedTextBox> list1 = new List<MaskedTextBox> { maskedTextBox9,maskedTextBox10,maskedTextBox11,maskedTextBox12,
                                                                          maskedTextBox13,maskedTextBox14,maskedTextBox15,maskedTextBox16};
                    bool b = Listfalse(list1);
                    bool cb = string.IsNullOrEmpty(comboBox1.Text);

                    if (cb || b)
                    {
                        FormUp.MessegeOk("Заполните все поля и выбирите день!");
                    }

                }
                
                if(ch2)
                {
                    if (ch3)
                    {

                        List<MaskedTextBox> list2 = new List<MaskedTextBox> { maskedTextBox17, maskedTextBox18, maskedTextBox19, maskedTextBox20 };
                        bool b = Listfalse(list2);

                        if(b)
                        {
                            FormUp.MessegeOk("Заполните все поля для субботы!");
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
                    }

                    //Начинаем работать с данными 
                }

            }

        }
    }
}
