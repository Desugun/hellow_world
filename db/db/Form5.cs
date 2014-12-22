using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db
{
    public partial class Form5 : Form
    {
        string id;
        string ejob;
        db_connect c;
        public Form5(string uid,string job)
        {
            InitializeComponent();
            id = uid;
            ejob=job;
            c = new db_connect();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“projectDataSet.jobs”中。您可以根据需要移动或删除它。
            this.jobsTableAdapter.Fill(this.projectDataSet.jobs);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string job = comboBox1.Text.Trim();
            if (ejob == "")
            {
                c.find_course(job, id);
                c.Updateusers(job, id);
                MessageBox.Show("选择成功");
                this.Close();
            }
            else if (ejob != job)
            {
                MessageBox.Show("已有正在进行的职业目标");
            }
            else
            {
                MessageBox.Show("已选择该职业");
            }
            
        }
    }
}
