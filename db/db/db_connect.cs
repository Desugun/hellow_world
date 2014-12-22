using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace db
{
    public class db_connect
    {
        const string Connection = @"Data Source=.;Initial Catalog=project;Integrated Security = True";
        List<string> course_list = new List<string>();
        List<string> course_list1 = new List<string>();
        public int login(string user_id, string password)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select UID,password from users where UID ='" + user_id + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            if (!sqlreader.Read())
                return 2;
            else
            {
                string temp = sqlreader["password"].ToString();
                if (temp != password)
                    return 0;
                else
                    return 1;
            }

        }
        public string SqlSelectname(string id)
        {
            string result;
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select UName from users where UID='" + id + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            if (sqlreader.Read())
                result = sqlreader["UName"].ToString();
            else
                result = "error";
            conn.Close();
            return result;
        }
        public string SqlSelectjob(string id)
        {
            string result;
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select job from users where UID='" + id + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            if (sqlreader.Read())
                result = sqlreader["job"].ToString();
            else
                result = "空";
            conn.Close();
            return result;
        }


        void SqlSelect_job_course(string job)
        {

            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select necessary_course,require_course from jobs where job='" + job + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            while (sqlreader.Read())
            {
                string necessary = sqlreader["necessary_course"].ToString();
                string require = sqlreader["require_course"].ToString();
                //分割函数，并插入list中
                CoursesToList(necessary);
                CoursesToList(require);
            }
            conn.Close();
        }
        void SqlSelect_course_urese(string course)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select necessary_courses,require_courses from Course where CName='" + course + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            while (sqlreader.Read())
            {
                string necessary = sqlreader["necessary_courses"].ToString();
                string require = sqlreader["require_courses"].ToString();
                //分割函数，并插入list中
                CoursesToList(necessary);
                CoursesToList(require);
            }
            conn.Close();
        }
        public string SqlSelect_study_course(string id)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select CName from Score where UID='" + id + "' and require_need=0 and necessary_need=0 and complite=0";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            string result = "";


            while (sqlreader.Read())
            {
                result = result + sqlreader["CName"].ToString() + ",";
            }
            return result;
        }
        public string SqlSelect_user_complite(string id)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select complite_course from users where UID='" + id + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            string result = "";
            if (sqlreader.Read())
                result = sqlreader["complite_course"].ToString();
            return result;
        }


        public string SqlSelect_job_needcomplite(string job)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select necessary_course,require_course from jobs where job='" + job + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            string result = "";
            string result1 = "";
            if (sqlreader.Read())
            {
                result = sqlreader["necessary_course"].ToString();
                result1 = sqlreader["require_course"].ToString();

            }
            string resultf = NewCourse(result, result1);
            return resultf;
        }




        public void SqlInsertuser(string id, string password, string name, string age)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "insert into users values('" + id + "','" + name + "','" + password + "','" + age + "',NULL,NULL,0,0)";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            conn.Close();
        }
        void SqlInsertscore(string id, string cname, int necessary_need, int require_need, string cnext)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "insert into Score values('" + id + "','" + cname + "',NULL,0," + require_need + "," + necessary_need + ",'" + cnext + "',NULL)";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            conn.Close();

        }
        void SqlDelete(string MusicName)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "delete from music where MusicName='" + MusicName + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.ExecuteReader();
            conn.Close();
        }

        public void Updatescore(string id, string cname, int score)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "update Score set score=" + score + "," + "complite=1 where UID='" + id + "'and CName='" + cname + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.ExecuteReader();
            conn.Close();
        }
        public void Updateusers(string job, string id)
        {
            SqlConnection conn = new SqlConnection(Connection);
            SqlConnection conn1 = new SqlConnection(Connection);
            conn.Open();
            conn1.Open();
            string sql1 = "select require_complite,necessary_complite from jobs where job='" + job + "'";
            SqlCommand sqlcmd1 = new SqlCommand(sql1, conn1);
            SqlDataReader sqlreader1 = sqlcmd1.ExecuteReader();
            int r = 0;
            int n = 0;
            if (sqlreader1.Read())
            {
                r = int.Parse(sqlreader1["require_complite"].ToString());
                n = int.Parse(sqlreader1["necessary_complite"].ToString());
            }
            string sql = "update users set job='" + job + "',require_complite=" + r + ",necessary_complite=" + n + "where UID='" + id + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            conn1.Close();
            sqlcmd.ExecuteReader();
            conn.Close();

        }
        public void updateuser_complite(string id, string course)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "update users set complite_course='" + course + "'where UID='" + id + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.ExecuteReader();
            conn.Close();
        }
        void updatenecessary(string course,string id)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "update Score set necessary_need=necessary_need-1 where CName='" + course + "'and UID='"+id+"'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.ExecuteReader();
            conn.Close();
        }
        void updaterequire(string course, string id)
        {
            SqlConnection conn = new SqlConnection(Connection);
            SqlConnection conn1 = new SqlConnection(Connection);
            conn.Open();
            conn1.Open();
            string sql = "update Score set require_need=require_need-1 where CName='" + course + "'and UID='" + id + "'";
            string sql1 = "select require_need from Score where CName='" + course + "'and UID='" + id + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlCommand sqlcmd1 = new SqlCommand(sql1, conn1);
            SqlDataReader sqlreader1 = sqlcmd1.ExecuteReader();
            if (sqlreader1.Read())
            {
                if (int.Parse(sqlreader1["require_need"].ToString()) != 0)
                {
                    conn1.Close();
                    sqlcmd.ExecuteReader();
                    conn.Close();
                }
            }
            conn1.Close();
            conn.Close();
        }
        public void find_course(string job, string id)
        {
            SqlSelect_job_course(job);
            for (int a = 0; a < course_list.Count; a++)
            {
                SqlSelect_course_urese(course_list[a]);
                SqlConnection conn = new SqlConnection(Connection);
                conn.Open();
                string sql = "select necessary_complite,require_complite,next_courses from Course where CName='" + course_list[a] + "'";
                SqlCommand sqlcmd = new SqlCommand(sql, conn);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                string next = "";
                if (sqlreader.Read())
                    next = sqlreader["next_courses"].ToString();
                SqlInsertscore(id, course_list[a], int.Parse(sqlreader["necessary_complite"].ToString()), int.Parse(sqlreader["require_complite"].ToString()), next);
            }
            course_list = new List<string>();

        }
        public void CoursesToList(string courses)
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
        public void CoursesToList1(string courses)
        {

            int head = courses.IndexOf(",");
            while (head > 0)
            {
                string tmp = courses.Substring(0, head);
                if (!course_list.Contains(tmp))
                {
                    course_list1.Add(tmp);
                }

                courses = courses.Substring(head + 1);
                head = courses.IndexOf(",");
                if (head < 0 && !course_list1.Contains(courses))
                {
                    if (courses != "")
                        course_list1.Add(courses);
                    break;
                }
            }
        }
        public string NewCourse(string inputString, string CourseNeed)
        {
            if (CourseNeed == "")
            {
                CourseNeed = inputString;
            }
            else
            {
                CourseNeed = CourseNeed + "," + inputString;
            }
            string m = CourseNeed;
            return m;
        }
        public void updateallnext(string course,string id)
        {
            SqlConnection conn = new SqlConnection(Connection);
            conn.Open();
            string sql = "select next_courses from Course where CName='" + course + "'";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            if(sqlreader.Read())
                CoursesToList(sqlreader["next_courses"].ToString());
            conn.Close();
            for (int a = 0; a < course_list.Count; a++)
            {
                SqlConnection conn1 = new SqlConnection(Connection);
                conn1.Open();
                string sql1 = "select necessary_courses,require_courses from Course where CName='" + course_list[a] + "'";
                SqlCommand sqlcmd1 = new SqlCommand(sql1, conn1);
                SqlDataReader sqlreader1 = sqlcmd1.ExecuteReader();
                while (sqlreader1.Read())
                {
                    string necessary="";
                    string require = "";
                   
                    necessary = sqlreader1["necessary_courses"].ToString();
                    require = sqlreader1["require_courses"].ToString();
                  
                    CoursesToList1(necessary);
                    if (course_list1.Contains(course))
                        updatenecessary(course_list[a],id);
                    else
                    {
                        CoursesToList1(require);
                        if (course_list1.Contains(course))
                            updaterequire(course_list[a],id);
                    }
                    course_list1 = new List<string>();
                }
                conn1.Close();
            }


        }
    }
}
    

