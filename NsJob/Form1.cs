using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NsJob
{
    public partial class Form1 : Form
    {
        //private readonly SqlConnection connection = new SqlConnection("Server=192.168.0.1;Database=a;User Id=sa;Password=1;Connection Timeout=1");
        private readonly SqlConnection connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=test;Integrated Security = true;");
        public Form1()
        {
            InitializeComponent();
            this.dgDetails.AutoGenerateColumns = false;
            this.dgDetails.MultiSelect = false;
            this.dgDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.ReadOnly = true;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgDetails.DataSource = SqlHelper.GetDataTable(connection, "select * from testtmp;");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var age = Convert.ToInt32(numericUpDown1.Value.ToString());
            var name = txtName.Text.ToString();

            var sql = string.Format("Insert into testtmp(name,age)values('{0}',{1})",name,age);

            var result =  SqlHelper.Excute(connection, sql);
            dgDetails.DataSource = SqlHelper.GetDataTable(connection, "select * from testtmp;");
            if (result == 1)
                MessageBox.Show("保存成功", "提示");
        }

        private void dgDetails_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (DataGridViewRow item in dgDetails.Rows)
                {
                    item.Selected = false;
                }

                var info =  dgDetails.HitTest(e.X, e.Y);
                dgDetails.Rows[info.RowIndex].Selected = true;
                var menu = new ContextMenu();
                var tmiDelete = new MenuItem(string.Format("是否刪除 {0} 的数据？",dgDetails.SelectedRows[0].Cells[0].Value.ToString()));
                tmiDelete.Click += TmiDelete_Click;
                menu.MenuItems.Add(tmiDelete);
                menu.Show(dgDetails,e.Location);
            }
        }

        private void TmiDelete_Click(object sender, EventArgs e)
        {
            var item = dgDetails.SelectedRows[0];
            var name = item.Cells[0].Value.ToString();
            var sql = string.Format("delete from  testtmp where  name =  '{0}'", name);

            var result = SqlHelper.Excute(connection, sql);
            dgDetails.DataSource = SqlHelper.GetDataTable(connection, "select * from testtmp;");
        }
    }
}
