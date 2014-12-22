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
    public partial class Form1 : Form
    {
        public db_connect connector;
        public Form1()
        {
            InitializeComponent();
            connector = new db_connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();//用户名
            string password = textBox2.Text.Trim();//用户密码
            if (username.Equals("") || password.Equals(""))
            {
                MessageBox.Show ( "信息不能为空");
            }
            else
            {
                int tmp = -1;
                tmp = connector.login(username, password);
                if (tmp == 1)
                {
                    MessageBox.Show ( "登录成功");
                    Form3 a = new Form3(username);
                    a.Show();
                    this.Hide();
                    textBox1.Text = "";
                    textBox2.Text = "";
                   //打开用户界面

                }
                else if (tmp == 0)
                {
                   MessageBox.Show (  "登录失败：用户密码错误，请重新输入");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else if (tmp == -1)
                {
                    MessageBox.Show (  "登陆失败：数据库内操作有误。");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else if (tmp == 2)
                {
                    MessageBox.Show ( "登陆失败：该用户未注册");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                //无其他情况
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            zhuce a = new zhuce();
            a.Show();
            this.Hide();
            a.FormClosed += delegate(object s, FormClosedEventArgs fe) { this.Show(); };
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}
