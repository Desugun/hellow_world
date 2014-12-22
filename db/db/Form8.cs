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
    public partial class Form8 : Form
    {
        db_connect c;
        string complite_course;
        List<string> course_list;
        public Form8(string job)
        {
            InitializeComponent();
            //InitializeComponent();
            c = new db_connect();
            course_list = new List<string>();
            complite_course=c.SqlSelect_job_needcomplite(job);
            CoursesToList(complite_course);
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
                //this.button2.Click += new System.EventHandler(this.button2_Click);
                this.Controls.Add(this.button2);
                x += 80;
                if (k == 2)
                {
                    x = 50;
                    y += 60;
                    k = -1;
                }
                k++;


            }
        }

        private void Form8_Load(object sender, EventArgs e)
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
    }
}
