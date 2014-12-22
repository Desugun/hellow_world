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
    public partial class Form7 : Form
    {
        db_connect c;
        string study_course_name;
        List<string> course_list;
        string select_course;
        string uid;
        public Form7(string id)
        {
            InitializeComponent();
            uid = id;
            select_course = "";
            c = new db_connect();
            study_course_name=c.SqlSelect_study_course(id);
            course_list = new List<string>();
            CoursesToList(study_course_name);
            //画图
            int x = 50;//横坐标
            int y = 50;//纵坐标
            int k = 0;
            for (int j = 0; j < course_list.Count; j++)
            {
                this.button2 = new System.Windows.Forms.Button();
                this.button2.Tag = j;
                this.button2.Location = new System.Drawing.Point(x, y);
                this.button2.Name = course_list[j];
                this.button2.Size = new System.Drawing.Size(50, 50);
                this.button2.Text = course_list[j];
                this.button2.UseVisualStyleBackColor = true;
                this.button2.Click += new System.EventHandler(this.button2_Click);
                this.Controls.Add(this.button2);
                x += 80;
                if (k == 2)
                {
                    x = 50;
                    y += 60;
                }
                k++;


            }

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
                MessageBox.Show("分数不能为空");
            else
            {
                int score = int.Parse(textBox1.Text.Trim());
                c.Updatescore(uid,select_course,score);
                //判断下一步和职业是否完成
                string t=c.SqlSelect_user_complite(uid);
                string ne = c.NewCourse(t, select_course);
                c.updateuser_complite(uid, ne);
                c.updateallnext(select_course,uid);
                MessageBox.Show("添加分数完成");
                this.Close();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
        void CoursesToList(string courses)
        {

            int head = courses.IndexOf(",");
            while (head > 0)
            {
                string tmp = courses.Substring(0, head);
                if (!course_list.Contains(tmp))
                {
                    course_list.Add(tmp);
                }

                courses = courses.Substring(head + 1);
                head = courses.IndexOf(",");
                if (head < 0 && !course_list.Contains(courses))
                {
                    if (courses != "")
                        course_list.Add(courses);
                    break;
                }
            }
        }
        void button2_Click(object sender, EventArgs e)
        {
            //int size = 5;//初始化
            Button button2 = sender as Button;
           // int j = (int)button2.Tag;
            //int recommend = 80;//初始化推荐值 
            select_course = button2.Text.Trim();
            //this.label2 = new System.Windows.Forms.Label();
            //int recommend2 = ((Convert.ToInt32(textBox1.Text.ToString())) + 80) / 2;//recommend2记录计算后的推荐值
           // string recommend3 = recommend2.ToString();
           // int Lx = button2.Location.X;
           // int Ly = button2.Location.Y;
           // this.label2.Name = "recommend_value";
           // this.label2.Size = new System.Drawing.Size(35, 12);
           // this.label1.Location = new System.Drawing.Point(Lx, Ly + 30);
           // this.label1.Text = "推荐值：" + recommend3;
           // this.Controls.Add(label2);
        }
    }
}
