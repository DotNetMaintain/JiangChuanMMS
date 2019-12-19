using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//_5_1_a_s_p_x
namespace sql
{
    public partial class FrmDBHelp : Form
    {

        string name = "";
        FileInfo file = null;       //文件流
        StreamWriter writer = null; //写入流
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //默认桌面路径

        public FrmDBHelp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            file = new FileInfo(path + @"\\Helper.cs");
            writer = file.CreateText();
            writer.WriteLine(name);
            writer.Close();
            this.label1.Visible = true;

        }
        public void load()
        {
            name += "using System;\r\n";
            name += "using System.Collections.Generic;\r\n";
            name += "using System.Data;\r\n";
            name += "using System.Data.SqlClient;\r\n";
            name += "using System.Configuration;\r\n\r\n";
            name += "namespace DBHelp\r\n";
            name += "{\r\n";
            name += "    public abstract class Helper\r\n";
            name += "    {\r\n";
            name += "        private static  readonly string connectString = ConfigurationManager.ConnectionStrings[\"connectString\"].ToString();\r\n"; 
            name += "\r\n";
            name += "        /// <summary>\r\n";
            name += "        /// 执行命令(没有返回自定义结果）\r\n";
            name += "        /// </summary>\r\n";
            name += "        /// <param name=\"cmdText\"></param>\r\n";
            name += "        /// <param name=\"param\"></param>\r\n";
            name += "        /// <returns></returns>\r\n";
            name += "        public static bool ExecuteNonQuery(string cmdText, SqlParameter[] param)\r\n";
            name += "        {\r\n";
            name += "            SqlConnection conn = new SqlConnection();\r\n";
            name += "            SqlCommand cmd = new SqlCommand();\r\n";
            name += "            PreCommand(conn, cmd, cmdText, param);\r\n";
            name += "            bool b = false;\r\n";
            name += "            try\r\n";
            name += "            {\r\n";
            name += "                cmd.ExecuteNonQuery();\r\n";
            name += "                cmd.Parameters.Clear();\r\n";
            name += "                b = true;\r\n";
            name += "            }\r\n";
            name += "            catch { }\r\n";
            name += "            finally\r\n";
            name += "            {\r\n";
            name += "                conn.Close();\r\n";
            name += "            }\r\n";
            name += "            return b;\r\n";
            name += "        }\r\n";
            name += "        /// <summary>\r\n";
            name += "        /// 执行命令并返回自定义结果\r\n";
            name += "        /// </summary>\r\n";
            name += "        /// <returns></returns>\r\n";
            name += "        public static object ExecuteScalar(string cmdText, SqlParameter[] param)\r\n";
            name += "        {\r\n";
            name += "            SqlConnection conn = new SqlConnection();\r\n";
            name += "            SqlCommand cmd = new SqlCommand();\r\n";
            name += "            PreCommand(conn, cmd, cmdText, param);\r\n";
            name += "            object obj = null;\r\n";
            name += "            try\r\n";
            name += "            {\r\n";
            name += "               obj= cmd.ExecuteScalar();\r\n";
            name += "               cmd.Parameters.Clear();\r\n";
            name += "            }\r\n";
            name += "            catch { }\r\n";
            name += "            finally\r\n";
            name += "            {\r\n";
            name += "                conn.Close();\r\n";
            name += "            }\r\n";
            name += "            return obj;\r\n";
            name += "        }\r\n";
            name += "        /// <summary>\r\n";
            name += "        /// 返回读取器对象\r\n";
            name += "        /// </summary>\r\n";
            name += "        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] param)\r\n";
            name += "        {\r\n";
            name += "            SqlConnection conn = new SqlConnection();\r\n";
            name += "            SqlCommand cmd = new SqlCommand();\r\n";
            name += "            PreCommand(conn, cmd, cmdText, param);\r\n";
            name += "            SqlDataReader rd = null;\r\n";
            name += "            try\r\n";
            name += "            {\r\n";
            name += "                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);\r\n";
            name += "                cmd.Parameters.Clear();\r\n";
            name += "            }\r\n";
            name += "            catch { conn.Close(); }\r\n";
            name += "            return rd;\r\n";
            name += "        }\r\n";
            name += "        /// <summary>\r\n";
            name += "        /// 公共方法\r\n";
            name += "        /// </summary>\r\n";
            name += "        private static void PreCommand(SqlConnection conn, SqlCommand cmd, string cmdText, SqlParameter[] para)\r\n";
            name += "        {\r\n";
            name += "            conn.ConnectionString = connectString;\r\n";
            name += "            if (conn.State == ConnectionState.Closed)\r\n";
            name += "                conn.Open();\r\n";
            name += "            cmd.Connection = conn;\r\n";
            name += "            cmd.CommandText = cmdText;\r\n";
            name += "            cmd.CommandType = CommandType.StoredProcedure;\r\n";
            name += "            if(para !=null)\r\n";
            name += "               cmd.Parameters.AddRange(para);\r\n";
            name += "        }\r\n";
            name += "    }\r\n";
            name += "}\r\n";
           
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            load();
            this.textBox1.Text = name;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.label1.Visible = false;
        }
    }
}
