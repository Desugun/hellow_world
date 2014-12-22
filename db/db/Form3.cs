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
    public partial class Form3 : Form
    {
        string name;
        string job;
        string id;
        public db_connect c;
        public Form3(string uid)
        {
            InitializeComponent();
            c = new db_connect();
            id=uid;
            name = c.SqlSelectname(uid);
            job = c.SqlSelectjob(uid);
            label6.Text=name;
            label7.Text = job;
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form6 a=new Form6(id);
            a.Show();
            this.Hide();
            a.FormClosed += delegate(object s, FormClosedEventArgs fe) { this.Show(); };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (job != "空")
            {
                Form7 a = new Form7(id);
                a.Show();
                this.Hide();
                a.FormClosed += delegate(object s, FormClosedEventArgs fe) { this.Show(); };

            }
            else
                MessageBox.Show("还未选择职业");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 a = new Form8(job);
            a.Show();
            this.Hide();
            a.FormClosed += delegate(object s, FormClosedEventArgs fe) { this.Show(); };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 a = new Form5(id,job);
            a.Show();
            this.Hide();
            a.FormClosed += delegate(object s, FormClosedEventArgs fe) { this.Show(); };
            job = c.SqlSelectjob(id);
        }
    }
}
