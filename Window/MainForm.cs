using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;


namespace School_tametable

{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Employee.UpdateDG(dataGridView1);
            Subject.UpdateDG(dataGridView2);
            EmployeeSubject.UpdateDG(dataGridView3);
            NameClass.UpdateDG(dataGridView4);
            Class.UpdateDG(dataGridView5);
            LessonsTime.UpdateDG(dataGridView6);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormAddEmployee femp = new FormAddEmployee(this);
            femp.Show(); //передаем ссылку на главную форму
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Enabled = false;
            DialogResult result = FormUp.MessegeYesNo("Удалить информацию о сотруднике");

            if (result == DialogResult.Yes)
            {
                Employee.DeleteEmp(FormUp.CurIndex(dataGridView1));
                
                Employee.UpdateDG(dataGridView1);
                EmployeeSubject.UpdateDG(dataGridView3);
                this.TopMost = true;
            }

            contextMenuStrip1.Enabled = true;
            //this.TopMost = true;
        }

        private void button3_Click(object sender, EventArgs e)

        {
            this.Enabled = false;
            FormEditEmployee femp = new FormEditEmployee(this);
            femp.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormExEmp femp = new FormExEmp(this);
            femp.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            FormUp.RightMouse(this, e, dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormAddSubject femp = new FormAddSubject(this);
            femp.Show(); //передаем ссылку на главную форму
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Enabled = false;
            DialogResult result = FormUp.MessegeYesNo("Удалить информацию о предмете");

            if (result == DialogResult.Yes)
            {
                Subject.Delete(FormUp.CurIndex(dataGridView2));

                Subject.UpdateDG(dataGridView2);
                EmployeeSubject.UpdateDG(dataGridView3);
                this.TopMost = true;
            }

           // contextMenuStrip1.Enabled = true;
            this.TopMost = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormEditSubject femp = new FormEditSubject(this);
            femp.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            FormUp.RightMouse(this, e, dataGridView2);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormEmpSubj femp = new FormEmpSubj(this);
            femp.Show();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            DialogResult result = FormUp.MessegeYesNo("Удалить запись?");

            if (result == DialogResult.Yes)
            {
                EmployeeSubject.Delete(FormUp.CurIndex(dataGridView3));

                EmployeeSubject.UpdateDG(dataGridView3);
                this.TopMost = true;
            }

            // contextMenuStrip1.Enabled = true;
            this.TopMost = true;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormEditEmpSub femp = new FormEditEmpSub(this);
            femp.Show();
        }

        private void dataGridView3_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            FormUp.RightMouse(this, e, dataGridView3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormClass fclass = new FormClass(this);
            fclass.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormAddClassSubj fclass = new FormAddClassSubj(this);
            fclass.Show();
        }

        private void dataGridView5_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            FormUp.RightMouse(this, e, dataGridView5);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            DialogResult result = FormUp.MessegeYesNo("Удалить запись?");

            if (result == DialogResult.Yes)
            {
                Class.Delete(FormUp.CurIndex(dataGridView5));

                Class.UpdateDG(dataGridView5);
                this.TopMost = true;
            }

            // contextMenuStrip1.Enabled = true;
            this.TopMost = true;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormEditClassSubj fclass = new FormEditClassSubj(this);
            fclass.Show();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormCopyClass f = new FormCopyClass(this);
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormLT f = new FormLT(this);
            f.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Word.Document doc = null;

            try
            {
                string year = DateTime.Today.Year.ToString();
                string yearn = (DateTime.Today.Year + 1).ToString();

                List<string> par = new List<string>();
               

                par.Add(year);
                par.Add(yearn);


                using (schoolContext db = new schoolContext())
                {
                    //Формируем список вычислительных параметров для обычных дней
                    var dayl = db.LessonsTimes.Where(c => c.DayOfWeek != "СУБ." && !Regex.IsMatch(c.DayOfWeek, @"^[А-Я]{2}")).Select(c => c.DayOfWeek).Distinct().ToArray();
                    //Объединяем список в одну строку
                    string dl = string.Join("", dayl);

                    // Название дней недели обычное расписание
                    par.Add(dl);

                    //Получаем список элементов для любого дня

                    var sl1 = db.LessonsTimes.Where(c => c.DayOfWeek == dayl[0] && c.Change==1).ToList();

                    FormUp.ListPar(sl1, par);

                    var sl2 = db.LessonsTimes.Where(c => c.DayOfWeek == dayl[0] && c.Change == 2).ToList();
                    
                    FormUp.ListPar(sl2, par);

                    //заполняем параметры особого дня

                    var oday1 = db.LessonsTimes.Where(c => c.Change==1 && c.DayOfWeek != "СБ." && Regex.IsMatch(c.DayOfWeek, @"^[А-Я]{2}")).ToList();

                    //Название особого дня

                    string od = oday1[0].DayOfWeek;
                    par.Add(od);

                    FormUp.ListPar(oday1, par);

                    var oday2 = db.LessonsTimes.Where(c => c.DayOfWeek != "СБ." && Regex.IsMatch(c.DayOfWeek, @"^[А-Я]{2}")&&c.Change==2).ToList();

                    FormUp.ListPar(oday2, par);

                    //Заполняем для субботы
                    var sub = db.LessonsTimes.Where(c => c.DayOfWeek == "СБ." && c.Change==1).ToList();
                    FormUp.ListPar(sub, par);
                    var sub2 = db.LessonsTimes.Where(c => c.DayOfWeek == "СБ." && c.Change == 2).ToList();
                    if(sub2.Any())
                    {
                        FormUp.ListPar(sub2, par);
                    }
                    else
                    {
                        for(int j=0;j<7;j++)
                        {
                            par.Add(" ");
                        }
                    }

                }

                //Открываем документ и заполняем параметры
                string spath = @"C:\Users\Alexa\OneDrive\Program\School_tametable\Шаблоны\Расписание звонков.docx";

                Word.Application app = new Word.Application();

                // Открываем
                doc = app.Documents.Add(spath);
                doc.Activate();

                // Добавляем информацию
                // wBookmarks содержит все закладки
                Word.Bookmarks wBookmarks = doc.Bookmarks;
                Word.Range wRange;
                var dc = doc.Content;

                int i = 0;

                object oMissing = System.Reflection.Missing.Value;
                Word.Range dRange = doc.Range(ref oMissing, ref oMissing);


                foreach (Word.Bookmark mark in wBookmarks)
                {
                    //dRange.Start = mark.Range.Start;
                    //dRange.End = mark.Range.End;
                    //dRange.Delete();
                    //doc.Save();
                    //
                    wRange = mark.Range;
                    wRange.Text = par[i];

                    i++;

                }
              
                // Закрываем документ

                doc.Close();
                doc = null;
            }



            catch (Exception ex)
            {
                doc.Close();
                doc = null;
                FormUp.MessegeOk(ex.Message);

            }
        }
    }
}