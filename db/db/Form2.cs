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
    public partial class zhuce : Form
    {
        public db_connect connector;
        public zhuce()
        {
            InitializeComponent();
            connector = new db_connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Trim();
            string keyword = textBox2.Text.Trim();
            string keyword1 = textBox3.Text.Trim();
            string name = textBox5.Text.Trim();
            string age = textBox4.Text.Trim();
            int temp = -1;
            temp = connector.login(id, "0");
            if (keyword != keyword1)
            {
                MessageBox.Show("密码不一致");
            }
            else if (temp != 2)
            {
                MessageBox.Show("用户已注册");
            }
            else
            { 
                //加入到数据库
                connector.SqlInsertuser(id,keyword,name,age);
                MessageBox.Show("注册成功");
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
