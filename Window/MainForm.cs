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
            FormUp.MessegeOk("Тест!");
        }
    }
}