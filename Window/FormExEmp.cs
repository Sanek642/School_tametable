using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace School_tametable
{
    public partial class FormExEmp : Form
    {
        private MainForm form;

        public FormExEmp()
        {
            InitializeComponent();
        }

        public FormExEmp(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        //что бы пользоваться библиотекой необходимо добавить пакеты ExcelDataReader.DataSet, System.Text.Encoding.CodePages
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel Files|*.xlsx";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Выбор файла пользователем
                    var filePath = openFileDialog.FileName;
                    textBox1.Text = filePath;
                    textBox1.Enabled = false;

                    //Открываем файл для чтения считываем в дата сет
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    try
                    {
                        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                        {

                            var reader = ExcelReaderFactory.CreateReader(stream);
                            var dataSet = reader.AsDataSet();
                            var dataTable = dataSet.Tables[0];
                            int count = dataTable.Columns.Count;
                            if (count == 2)
                            {
                                dataGridView1.DataSource = dataTable;
                                dataGridView1.Columns[0].HeaderText = "Имя сотрудника";
                                dataGridView1.Columns[0].Width = 160;
                                dataGridView1.Columns[1].HeaderText = "Электронный адрес";
                                dataGridView1.Columns[1].Width = 153;
                                button2.Enabled = true;
                            }
                            else
                            {
                                FormUp.MessegeOk("Количество колонок в файле не равно 2");

                            }
                            this.TopMost = true;
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        FormUp.MessegeOk("Закройте выбранный файл");
                        this.TopMost = true;
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Проверяем на совпадение данных из файла и данных в справочнике, если ФИО или email совпадают то исключаем запись,
            //остальные запись добавляем в справочник

            using (schoolContext db = new schoolContext())

            {
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    string? nametmp = dataGridView1[0, j].Value.ToString();
                    string? emailtmp = dataGridView1[1, j].Value.ToString().ToLower();

                    if (nametmp != null && emailtmp != null)
                    {
                        var emp = db.Employees
                                                .Where(e => e.NameEmployess.Equals(nametmp) || e.Email.Equals(emailtmp))
                                                .ToList();

                        if (emp.Any())
                        {
                            dataGridView1.Rows.RemoveAt(j);
                            j--;

                        }

                        else
                        {
                            try
                            {
                                Employee empl = new Employee { NameEmployess = nametmp, Email = emailtmp };
                                db.Employees.Add(empl);
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                form.TopMost = true;
                                FormUp.MessegeOk(ex.Message);
                                break;
                            }
                        }

                    }
                }

                dataGridView1.Refresh();
                button2.Enabled = false;

                Employee.UpdateDG(form.dataGridView1);

            }

        }

        private void FormExEmp_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }
    }
}
