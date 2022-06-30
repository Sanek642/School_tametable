using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.EntityFrameworkCore;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Net;


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
            Lesson.UpdateDG(dataGridView7);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormAddEmployee femp = new FormAddEmployee(this);
            femp.Show(); //�������� ������ �� ������� �����
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Enabled = false;
            DialogResult result = FormUp.MessegeYesNo("������� ���������� � ����������");

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
            femp.Show(); //�������� ������ �� ������� �����
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Enabled = false;
            DialogResult result = FormUp.MessegeYesNo("������� ���������� � ��������");

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
            DialogResult result = FormUp.MessegeYesNo("������� ������?");

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
            DialogResult result = FormUp.MessegeYesNo("������� ������?");

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
                        //��������� ������ �������������� ���������� ��� ������� ����
                        var dayl = db.LessonsTimes.Where(c => c.DayOfWeek != "���." && !Regex.IsMatch(c.DayOfWeek, @"^[�-�]{2}")).Select(c => c.DayOfWeek).Distinct().ToArray();
                        //���������� ������ � ���� ������
                        string dl = string.Join("", dayl);

                        // �������� ���� ������ ������� ����������
                        par.Add(dl);

                        //�������� ������ ��������� ��� ������ ���

                        var sl1 = db.LessonsTimes.Where(c => c.DayOfWeek == dayl[0] && c.Change == 1).ToList();

                        FormUp.ListPar(sl1, par);

                        var sl2 = db.LessonsTimes.Where(c => c.DayOfWeek == dayl[0] && c.Change == 2).ToList();

                        FormUp.ListPar(sl2, par);

                        //��������� ��������� ������� ���

                        var oday1 = db.LessonsTimes.Where(c => c.Change == 1 && c.DayOfWeek != "��." && Regex.IsMatch(c.DayOfWeek, @"^[�-�]{2}")).ToList();

                        //�������� ������� ���

                        string od = oday1[0].DayOfWeek;
                        par.Add(od);

                        FormUp.ListPar(oday1, par);

                        var oday2 = db.LessonsTimes.Where(c => c.DayOfWeek != "��." && Regex.IsMatch(c.DayOfWeek, @"^[�-�]{2}") && c.Change == 2).ToList();

                        FormUp.ListPar(oday2, par);

                        //��������� ��� �������
                        var sub = db.LessonsTimes.Where(c => c.DayOfWeek == "��." && c.Change == 1).ToList();
                        FormUp.ListPar(sub, par);
                        var sub2 = db.LessonsTimes.Where(c => c.DayOfWeek == "��." && c.Change == 2).ToList();
                        if (sub2.Any())
                        {
                            FormUp.ListPar(sub2, par);
                        }
                        else
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                par.Add(" ");
                            }
                        }

                    }


                    //��������� �������� � ��������� ���������
                    var spath = Application.StartupPath + @"�������\���������� �������.docx";


                    Word.Application app = new Word.Application();

                    // ���������
                    doc = app.Documents.Add(spath);
                    doc.Activate();

                    // ��������� ����������
                    // wBookmarks �������� ��� ��������
                    Word.Bookmarks wBookmarks = doc.Bookmarks;
                    Word.Range wRange;
                    var dc = doc.Content;

                    int i = 0;

                    object oMissing = System.Reflection.Missing.Value;
                    Word.Range dRange = doc.Range(ref oMissing, ref oMissing);


                    foreach (Word.Bookmark mark in wBookmarks)
                    {
                        
                        wRange = mark.Range;
                        wRange.Text = par[i];

                        i++;

                    }

                    // ��������� ��������

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

        private void button11_Click(object sender, EventArgs e)
        {
            //�������� ��������� ��� �������� �������� ���� ������� �������

            List<LCE> list = new List<LCE>();
            list.Clear();

            using (schoolContext db = new schoolContext())
            {
                //������� �������

                var tmp = db.Lessons.ToList();
                foreach (var t in tmp)
                {
                    db.Remove(t);
                }
                Lesson.TruCathcSave(db, this, "������ �������� ������!");


                //�������� ������ �������

                var spisokClass = db.NameClasses.OrderBy(c => c.NameClass1).ToList();

                List<string> subjectc = new List<string>();
                //��� ������� ������ ���������� ���������� � ������ ����������� ��������� � ��������� ��������
                //�������� � ������ ��������� � ��������� � �������� �� ������
                foreach (var cl in spisokClass)
                {
                    subjectc.Clear();
                    //��������� ����� ����� ������ ������ ���� ����� ����� � ���������� ������
                    var sumhc = db.Classes.Where(c => c.NameClassesId == cl.IdNameCl).Sum(c => c.HoursPerWeek);
                    var sumhlt = db.LessonsTimes.Count(c => c.Change == cl.Change);
                    if (sumhc > sumhlt)
                    {
                        FormUp.MessegeOk($"������ � ������ {cl.NameClass1}:{sumhc} ������ ��� ��������� ���������� {sumhlt}, �������������� ���������� ����� ���������");
                        //������� �������

                        var tmpcl = db.Lessons.ToList();
                        foreach (var t in tmpcl)
                        {
                            db.Remove(t);
                        }
                        Lesson.TruCathcSave(db, this, "������ �������� ������!");
                        break;
                    }


                    //�������� ������ ��������� ��� ������
                    var subclass = db.Classes.Include(c => c.NameClasses).Include(c => c.EmployeeSubject.Employees).
                    Include(c => c.EmployeeSubject.Subjects).Where(c => c.NameClassesId == cl.IdNameCl).ToList();

                    //������������ ������ � �������� �� ������ � � ��������� ������ ������

                    foreach (var sub in subclass)
                    {

                        //���������� �������, ������� �������� ������� �� ������, ������� ��� ���������

                        if (subjectc.Contains(sub.EmployeeSubject.Subjects.NameSubject))
                        {
                            continue;
                        }


                        //�������� ���������� ����� � ������ ��������
                        int count = Convert.ToInt32(sub.HoursPerWeek);

                        //������������ �������� � �������� �� ������ � � ��������� ������
                        if (sub.EmployeeSubject.Subjects.Successively == 1 && sub.EmployeeSubject.Subjects.Share == 1)
                        {
                            string cltmp = sub.EmployeeSubject.Subjects.NameSubject;

                            //��������� ������� ���������� ��� ��������� ��������
                            subjectc.Add(cltmp);
                            //������� ������� �� ������� ��� ��������� �������� �����
                            subclass = subclass.Where(c => c.EmployeeSubject.Subjects.NameSubject != cltmp).ToList();

                            //�������� ������ ����� �� ��������� ��� ������� ������, ��� ��� ��� ������� �� ������ 
                            //����� �������� � ������ ��������� � �������� ������� ���������

                            var dg = db.Classes.Include(c => c.NameClasses).Include(c => c.EmployeeSubject.Employees).
                            Include(c => c.EmployeeSubject.Subjects).Where(c => c.NameClassesId == sub.NameClassesId
                            && c.EmployeeSubject.Subjects.NameSubject == sub.EmployeeSubject.Subjects.NameSubject).ToList();

                            switch (count)
                            {
                                case 6:
                                    var lt6 = db.LessonsTimes.Where(c => c.Change == sub.NameClasses.Change).ToList();
                                    FormUp.Time1(count, lt6, dg, db, list, this);
                                    break;
                                case 4:
                                    var lt4 = db.LessonsTimes.Where(c => !EF.Functions.Like(c.DayOfWeek, "�%")).Where(c => c.Change == sub.NameClasses.Change).ToList();
                                    FormUp.Time1(count, lt4, dg, db, list, this);
                                    break;
                                case 2:
                                    var lt2 = db.LessonsTimes.Where(c => c.DayOfWeek != "��.").Where(c => c.Change == sub.NameClasses.Change).ToList();
                                    FormUp.Time1(count, lt2, dg, db, list, this);
                                    break;
                            }

                        }

                    }

                    //������������ ������ � �������� �� ������

                    foreach (var subd in subclass)
                    {
                        //���������� �������, ������� �������� ������� �� ������, ������� ��� ���������

                        if (subjectc.Contains(subd.EmployeeSubject.Subjects.NameSubject))
                        {
                            continue;
                        }


                        //�������� ���������� ����� � ������ ��������
                        int countd = Convert.ToInt32(subd.HoursPerWeek);

                        //������������ �������� � �������� �� ������
                        if (subd.EmployeeSubject.Subjects.Share == 1)
                        {
                                                       
                            string cltmpd = subd.EmployeeSubject.Subjects.NameSubject;

                            //��������� ������� � ���������� ��� ��������� ��������
                            subjectc.Add(cltmpd);
                            //������� ������� �� ������� ��� ��������� �������� �����
                            subclass = subclass.Where(c => c.EmployeeSubject.Subjects.NameSubject != cltmpd).ToList();

                            //�������� ������ ����� �� ��������� ��� ������� ������, ��� ��� ��� ������� �� ������ 
                            //����� �������� � ������ ��������� � �������� ������� ���������
                            var dgd = db.Classes.Include(c => c.NameClasses).Include(c => c.EmployeeSubject.Employees).
                            Include(c => c.EmployeeSubject.Subjects).Where(c => c.NameClassesId == subd.NameClassesId
                            && c.EmployeeSubject.Subjects.NameSubject == subd.EmployeeSubject.Subjects.NameSubject).ToList();

                            if(dgd.Count == 1)
                            {
                                
                                try
                                {
                                    var tsub = db.Classes.Include(c => c.NameClasses).Include(c => c.EmployeeSubject.Employees).
                                    Include(c => c.EmployeeSubject.Subjects).Where(c => c.NameClassesId == subd.NameClassesId
                                    && c.EmployeeSubject.Subjects.NameSubject != subd.EmployeeSubject.Subjects.NameSubject
                                    && c.EmployeeSubject.Subjects.Share == 1 && c.EmployeeSubject.Subjects.Successively == 0
                                    && !subjectc.Contains(c.EmployeeSubject.Subjects.NameSubject)).FirstOrDefault();
                                   
                                    subjectc.Add(tsub.EmployeeSubject.Subjects.NameSubject);
                                    subclass = subclass.Where(c => c.EmployeeSubject.Subjects.NameSubject != tsub.EmployeeSubject.Subjects.NameSubject).ToList();
                                    dgd.Add(tsub);

                                }
                                catch(NullReferenceException)
                                {
                                    FormUp.MessegeOk($"��� {subd.EmployeeSubject.Subjects.NameSubject} � ������ {subd.NameClasses.NameClass1}  ��� ������� �������� � �������� �� ������!");
                                }

                            }

                            switch (countd)
                            {
                                case 6:
                                    var lt6 = db.LessonsTimes.Where(c => c.Change == subd.NameClasses.Change).ToList();
                                    FormUp.Time2(countd, lt6, dgd, db, list, this);
                                    break;
                                case 5:
                                    var lt5 = db.LessonsTimes.Where(c => c.DayOfWeek != "��.").Where(c => c.Change == subd.NameClasses.Change).ToList();
                                    FormUp.Time2(countd, lt5, dgd, db, list, this);
                                    break;
                                case 4:
                                    var lt4 = db.LessonsTimes.Where(c => !EF.Functions.Like(c.DayOfWeek, "�%")).Where(c => c.Change == subd.NameClasses.Change).ToList();
                                    FormUp.Time2(countd, lt4, dgd, db, list, this);
                                    break;
                                case 3:
                                    var lt3 = db.LessonsTimes.Where(c => EF.Functions.Like(c.DayOfWeek, "�%") || c.DayOfWeek == "��." || c.DayOfWeek == "��.").
                                        Where(c => c.Change == subd.NameClasses.Change).ToList();
                                    FormUp.Time2(countd, lt3, dgd, db, list, this);
                                    break;
                                case 2:
                                    var lt2 = db.LessonsTimes.Where(c => c.DayOfWeek == "��." || c.DayOfWeek == "��." || c.DayOfWeek == "��." || c.DayOfWeek == "��.").
                                        Where(c => c.Change == subd.NameClasses.Change).ToList();
                                    FormUp.Time2(countd, lt2, dgd, db, list, this);
                                    break;
                                case 1:
                                    var lt1 = db.LessonsTimes.Where(c => c.Change == subd.NameClasses.Change).ToList();
                                    FormUp.Time2(countd, lt1, dgd, db, list, this);
                                    break;


                            }


                        }
                    }

                    //������������ �������� ��� ���������

                    foreach (var sub in subclass)
                    {

                        //�������� ���������� ����� � ������ ��������
                        int count = Convert.ToInt32(sub.HoursPerWeek);

                        switch (count)
                        {
                            case 6:
                                var lt6 = db.LessonsTimes.Where(c => c.Change == sub.NameClasses.Change).ToList();
                                FormUp.Time3(count, lt6, sub, db, list, this);
                                break;
                            case 5:
                                var lt5 = db.LessonsTimes.Where(c => c.DayOfWeek != "��.").Where(c => c.Change == sub.NameClasses.Change).ToList();
                                FormUp.Time3(count, lt5, sub, db, list, this);
                                break;
                            case 4:
                                var lt4 = db.LessonsTimes.Where(c => !EF.Functions.Like(c.DayOfWeek, "�%")).Where(c => c.Change == sub.NameClasses.Change).ToList();
                                FormUp.Time3(count, lt4, sub, db, list, this);
                                break;
                            case 3:
                                var lt3 = db.LessonsTimes.Where(c => EF.Functions.Like(c.DayOfWeek, "�%") || c.DayOfWeek == "��." || c.DayOfWeek == "��." || c.DayOfWeek == "��.").
                                    Where(c => c.Change == sub.NameClasses.Change).ToList();
                                FormUp.Time3(count, lt3, sub, db, list, this);
                                break;
                            case 2:
                                var lt2 = db.LessonsTimes.Where(c => c.DayOfWeek == "��." || c.DayOfWeek == "��." || c.DayOfWeek == "��." || c.DayOfWeek == "��." || c.DayOfWeek == "��.").
                                    Where(c => c.Change == sub.NameClasses.Change).ToList();
                                FormUp.Time3(count, lt2, sub, db, list, this);
                                break;
                            case 1:
                                var lt1 = db.LessonsTimes.Where(c => c.Change == sub.NameClasses.Change).ToList();
                                FormUp.Time3(count, lt1, sub, db, list, this);
                                break;

                        }
                    }

                }
            }
            Lesson.UpdateDG(dataGridView7);

        }

        void MerCell(string s1, string s2, Excel.Worksheet ws, string day)
        {
            Excel.Range _excelCells2 = (Excel.Range)ws.get_Range(s1, s2).Cells;
            // ���������� �����������
            _excelCells2.Merge(Type.Missing);

            ws.Range[s1, s2].Value2 = day;
            ws.Range[s1, s2].Font.Bold = true;
            ws.Range[s1, s2].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            ws.Range[s1, s2].HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            ws.Range[s1, s2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            ws.Columns.EntireColumn.AutoFit();
        }

        void DataCell(int i, int j, Excel.Worksheet ws, string inf)
        {

            ws.Cells[i, j].Value2 = inf;
            ws.Cells[i, j].Font.Size = 10;
            ws.Cells[i, j].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            ws.Cells[i, j].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            ws.Columns.EntireColumn.AutoFit();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //�������� ������ �� �� ������� 
            List<Lesson> es = new List<Lesson>();

            using (schoolContext db = new schoolContext())
            {
                 es = db.Lessons.Include(c => c.LessonsTime).Include(c => c.Classes.NameClasses).
                                    Include(c => c.Classes.EmployeeSubject.Subjects).ToList();
                
            }
            if (es.Any())
            {

                // ������ ��������� ������ ����������
                Excel.Application excelApp = new Excel.Application();
                // ������ ��������� ������� ����� Excel
                Excel.Workbook workBook;
                // ������ ��������� ����� Excel
                Excel.Worksheet workSheet;

                workBook = excelApp.Workbooks.Add();
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

                string[] d = { "��.", "��.", "��.", "��.", "��.", "��." };

                int dj = 2;
                //���������� ������ � ��������� ���������� ������

                foreach (string s in d)
                {
                    MerCell($"A{dj}", $"A{dj + 6}", workSheet, s);
                    dj += 7;
                }

                //�������� ������ �� �� ������� 

                using (schoolContext db = new schoolContext())
                {
                    
                    //�������� ������ ������ ������� � ��������� ���������� ��� ���
                    var cl = es.OrderBy(c => c.Classes.NameClasses.NameClass1).Select(c => c.Classes.NameClasses.NameClass1).Distinct();

                    string[] alf = { "B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                                  "AA","BB","CC","DD","EE","FF","GG","HH","II","JJ","KK","LL","MM","NN","OO",
                                  "PP","QQ","RR","SS","TT","UU","VV","WW","XX","YY","ZZ"};
                    int i = 0;
                    int j = 2;
                    int k = 2;

                    foreach (var c in cl)
                    {
                        MerCell($"{alf[i]}1", $"{alf[i + 1]}1", workSheet, c);
                        i += 2;

                        //��������� ���������� ��� ������
                        var lcl = es.Where(p => p.Classes.NameClasses.NameClass1 == c).ToList();

                        foreach (string s in d)
                        {
                            var llcl = lcl.Where(p => p.LessonsTime.DayOfWeek.ToUpper() == s).OrderBy(p => p.LessonsTime.Number).ToList();
                            string namesub = null;
                            string cabinet = null;

                            foreach (var ll in llcl)
                            {
                                if (ll.Classes.EmployeeSubject.Subjects.Successively == 1 || ll.Classes.EmployeeSubject.Subjects.Share == 1)
                                //&& llcl.Where(c=>c.Classes.EmployeeSubject.Subjects.NameSubject==ll.Classes.EmployeeSubject.Subjects.NameSubject).ToList().Count!=1)
                                {
                                    if (namesub is null && cabinet is null)
                                    {
                                        cabinet = ll.Classes.Cabinet;
                                        namesub = ll.Classes.EmployeeSubject.Subjects.NameSubject;
                                        continue;
                                    }
                                    else
                                    {
                                        if (namesub == ll.Classes.EmployeeSubject.Subjects.NameSubject)
                                        {
                                            DataCell(j, k, workSheet, ll.Classes.EmployeeSubject.Subjects.NameSubject);
                                            DataCell(j, k + 1, workSheet, cabinet + "/" + ll.Classes.Cabinet);

                                            cabinet = null;
                                            namesub = null;
                                            j += 1;
                                        }
                                        else
                                        {
                                            DataCell(j, k, workSheet, namesub + "/" + ll.Classes.EmployeeSubject.Subjects.NameSubject);
                                            DataCell(j, k + 1, workSheet, cabinet + "/" + ll.Classes.Cabinet);

                                            cabinet = null;
                                            namesub = null;
                                            j += 1;
                                        }
                                    }
                                }
                                else
                                {
                                    DataCell(j, k, workSheet, ll.Classes.EmployeeSubject.Subjects.NameSubject);
                                    DataCell(j, k + 1, workSheet, ll.Classes.Cabinet);
                                    j += 1;
                                }


                            }
                            if (j <= 9)
                            {
                                j = 9;
                            }

                            if (j > 9 && j <= 15)
                            {
                                j = 16;
                            }

                            if (j > 16 && j <= 22)
                            {
                                j = 23;
                            }

                            if (j > 23 && j <= 30)
                            {
                                j = 30;
                            }
                            if (j > 30 && j <= 37)
                            {
                                j = 37;
                            }



                        }

                        j = 2;
                        k += 2;
                    }


                }

                excelApp.Visible = true;
                excelApp.UserControl = true;
            }
            else
            {
                FormUp.MessegeOk("������������ ����������!");
            }

        }

        private async void button13_Click(object sender, EventArgs e)
        {

            //�������� ������ �� �� ������� 
            List<Lesson> es = new List<Lesson>();
            List<Employee> employees = new List<Employee>();

            using (schoolContext db = new schoolContext())
            {
                es = db.Lessons.Include(c => c.LessonsTime).Include(c => c.Classes.NameClasses).
                                   Include(c => c.Classes.EmployeeSubject.Subjects).
                                   Include(c => c.Classes.EmployeeSubject.Employees).ToList();

            }
            if (es.Any())
            {
                string path = Application.StartupPath + @"�������\����.txt";
                //�������� ������ ���� �����������

                using (schoolContext db = new schoolContext())
                {
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        var emp = db.Employees.ToList();

                        
                        foreach (var t in emp)
                        {

                           
                            //�������� ���������� ������� ����������

                            var ess = es.Where(c => c.Classes.EmployeeSubject.Employees.IdEmployess == t.IdEmployess).ToList();

                            if (ess.Any())
                            {
                                await writer.WriteLineAsync(t.NameEmployess+" "+ess.Count.ToString() + "�.");

                                //��������� ������ �����������, ������� �������� �����
                                employees.Add(t);
                                foreach (var ls in ess)
                                {
                                    string str = ls.Classes.NameClasses.Change + " " + ls.Classes.NameClasses.NameClass1 + " "
                                                 + ls.LessonsTime.Number + " " + ls.Classes.EmployeeSubject.Subjects.NameSubject + " "
                                                 + ls.Classes.Cabinet;
                                    await writer.WriteLineAsync(str);
                                }
                                await writer.WriteLineAsync("\n");
                            }
                            


                        }
                    }

                    //���������� �� ����������� ����� ����

                    //// ����������� - ������������� ����� � ������������ � ������ ���
                    //MailAddress from = new MailAddress("school-22@mail.ru", "����� 22 � �. �������������");
                    //MailAddress to = new MailAddress("school-22@mail.ru");
                    //MailMessage m = new MailMessage(from, to);
                    //// ���� ������
                    //string year = DateTime.Today.Year.ToString();
                    //string yearn = (DateTime.Today.Year + 1).ToString();
                    //m.Subject = "���������� " + year + "/" + yearn;
                    //// ����� ������
                    //m.Body = "<h2>������������� � ���������� �������������</h2>";
                    //// ������ ������������ ��� html
                    //m.IsBodyHtml = true;

                    //foreach (var tmp in employees)
                    //{
                    //    m.CC.Add(tmp.Email);
                    //    //�������� �����
                    //    m.Attachments.Add(new Attachment(path));
                       
                    //}
                    //try
                    //{
                    //    // ����� smtp-������� � ����, � �������� ����� ���������� ������
                    //    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 465);
                    //    // ����� � ������
                    //    smtp.Credentials = new NetworkCredential("school-22@mail.ru", "fzytpyf.22");
                    //    smtp.EnableSsl = true;
                    //    smtp.Send(m);
                    //}
                    //catch(Exception ex)
                    //{
                    //    FormUp.MessegeOk(ex.Message);
                    //}

                }

            }

            else
            {
                FormUp.MessegeOk("������������ ����������!");
            }
        }
    }
}