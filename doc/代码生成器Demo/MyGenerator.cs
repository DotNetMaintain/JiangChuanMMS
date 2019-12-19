using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
//_来_自_5_1_a_s_p_x
namespace sql
{
    public partial class MyGenerator : Form
    {
        string connectstring;   //数据库连接字符串
        SqlConnection cn;       //数据库连接对象
        List<ProcedureInfo> list = new List<ProcedureInfo>();   //表对象
        public List<string> Allli = new List<string>();         //所有表
        public List<string> li = new List<string>();            //所关联的表
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //默认桌面路径
        string userId = "";        //用户名
        string password = "";      //密码
        string database = "master";//数据库
        string Id;                  //Id
        FileInfo file = null;       //文件流
        StreamWriter writer = null; //写入流
        string inserString = "";    //存储过程字符串
        string dalString = "";      //DAL字符串
        string bllString = "";      //Bll字符串
        string table = "";         //当前表名
        int i = 0;                  //定义一个循环变量
        public MyMethod myMethod = null;

        public MyGenerator()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            myMethod = new MyMethod();
            myMethod.MyAdd1 = true;
            myMethod.MyAddReturnId1 = true;
            myMethod.MyChange1 = true;
            myMethod.MyDelete1 = true;
            myMethod.MySelectAll1 = true;
            myMethod.MySelectById1 = true;
            myMethod.MySelectByWhere1 = true;
        }

        #region 连接测试
        /// <summary>
        /// 连接测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;

            database = textBox1.Text;
            userId = textBox2.Text;
            password = textBox3.Text;
            
            this.comboBox1.Items.Clear();
            

            string sql = string.Format("select name from sys.databases");
            if (checkBox1.Checked)
            {
                connectstring = string.Format(" data source={0}; initial catalog=master;integrated security=sspi", database);
            }
            else
            {
                connectstring = string.Format(" data source={0}; initial catalog=master;user id={1};password={2}", database, userId, password);
            }
            cn = new SqlConnection(connectstring);
            try
            {

                cn.Open();
                SqlCommand cm = new SqlCommand(sql, cn);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0].ToString());
                        Allli.Add(dr[0].ToString());
                    }
                }
            }
            catch
            {
                groupBox2.Enabled = false;
                MessageBox.Show("用户登录失败（请检查用户名和密码）");
            }
            this.comboBox1.SelectedIndex = this.comboBox1.Items.IndexOf("master");
        }
        #endregion

        #region 集成验证
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = textBox3.Enabled = !checkBox1.Checked;
        }
        #endregion

        #region 选择表
        private void button3_Click(object sender, EventArgs e)
        {
            cn.Open();
            cn.ChangeDatabase(comboBox1.Text);
            SqlCommand cm = new SqlCommand("select name from sys.Tables", cn);
            SqlDataReader dr = cm.ExecuteReader();
            li.Clear();
            while (dr.Read())
            {
                li.Add(dr[0].ToString());

            }
            dr.Close();
            cn.Close();
            FrmSelectTabel a = new FrmSelectTabel(li);
            a.form3 = this;
            a.ShowDialog();

        }
        #endregion

        #region 选择数据库
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            database = comboBox1.Text;
            try
            {
            cn.Open();
            cn.ChangeDatabase(comboBox1.Text);            
            li.Clear();
            SqlCommand cm = new SqlCommand("select name from sys.Tables", cn);
            using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    li.Add(dr[0].ToString());
                }
            }
            }
            catch { MessageBox.Show("该数据库不可操作 请尝试关闭重试"); cn.Close(); }
            
            finally{
            
            }
        }
        #endregion

        #region 生成三层
        /// <summary>
        /// 生成三层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.chenggong.Visible = false;
            this.timer1.Enabled = false;
            Add();
            this.chenggong.Visible = true;
            this.timer1.Enabled = true;
        }
        #endregion

        #region 选择路径
        /// <summary>
        /// 选择生成路径 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();

            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.SelectedPath;
                this.textBox4.Text = path;
            }
        }
        #endregion

        #region 返回数据库类型
        public bool isString(string type)
        {
            bool b = false;
            if (type == "varchar" || type == "char" || type == "nvarchar" || type == "nchar")
            {
                b = true;
            }
            return b;
        }
        #endregion

        #region 返回C#数据类型
        public string findType(string name)
        {
            if (name == "int" || name == "smallint")
            {
                return "Convert.ToInt32(";
            }
            else if (name == "tinyint")
            {
                return "Convert.ToByte(";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Convert.ToSingle(";
            }
            else if (name == "decimal")
            {
                return "Convert.ToDecimal(";
            }
            else if (name == "char" || name == "varchar" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return ".ToString()";
            }
            else if (name == "bit")
            {
                return "Convert.ToBoolean(";
            }
            else if (name == "datetime" || name == "smalldatetime")
            {
                return "Convert.ToDateTime(";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "Convert.ToDouble(";
            }
            else
            {
                return ".ToString()";
            }
        }
        #endregion

        #region 首字母大写
        public string fristToUpper(string name)
        {
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            return name;
        }
        #endregion

        #region 首字母小写
        public string fristToLower(string name)
        {
            name = name.Substring(0, 1).ToLower() + name.Substring(1);
            return name;
        }
        #endregion      

        #region 转换空格
        private string createplace(int a)
        {
            return new string(' ', a);
        }
        #endregion

        #region 返回C#实体数据类型
        public string findModelsType(string name)
        {
            if (name == "int" || name == "smallint")
            {
                return "int";
            }
            else if (name == "tinyint")
            {
                return "byte";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Single";
            }
            else if (name == "float")
            {
                return "float";
            }
            else if (name == "decimal")
            {
                return "decimal";
            }
            else if (name == "char" || name == "varchar" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "string";
            }
            else if (name == "bit")
            {
                return "bool";
            }
            else if (name == "datetime" || name == "smalldatetime")
            {
                return "DateTime";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "double";
            }
            else
            {
                return "string";
            }
        }
        #endregion

        #region 窗口关闭和最小化事件
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            //if (File.Exists(Application.StartupPath + "\\新建文件夹\\btn_close_down.png"))
            //    this.pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\新建文件夹\\btn_close_down.png");
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            //if (File.Exists(Application.StartupPath + "\\新建文件夹\\btn_close_highlight.png"))
            //    this.pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\新建文件夹\\btn_close_highlight.png");
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            //if (File.Exists(Application.StartupPath + "\\新建文件夹\\btn_mini_down.png"))
            //    this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\新建文件夹\\btn_mini_down.png");
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            //if (File.Exists(Application.StartupPath + "\\新建文件夹\\btn_mini_highlight.png"))
            //    this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\新建文件夹\\btn_mini_highlight.png");
        }
        #endregion

        #region 生成数据库INSERT 语句
        private void button5_Click(object sender, EventArgs e)
        {
            this.chenggong.Visible = false;
            this.timer1.Enabled = false;
            string SQLstring = "";
            this.progressBar1.Maximum = li.Count;
            int count = 0;
            foreach (string tablename in li)
            {
                count++;
                this.progressBar1.Value = count;
                string sql = "SELECT col.name AS 列名, typ.name as 数据类型,col.max_length AS 占用字节数," +
                       "  col.is_nullable  AS 是否允许非空,col.is_identity  AS 是否自增," +
                       " case when exists  ( SELECT 1  FROM sys.indexes idx join sys.index_columns idxCol on (idx.object_id = idxCol.object_id)" +
                       "  WHERE idx.object_id = col.object_id AND idxCol.index_column_id = col.column_id AND idx.is_primary_key = 1" +
                       " ) THEN 1 ELSE 0 END  AS 是否是主键 FROM sys.columns col left join sys.types typ on (col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id)" +
                       " WHERE col.object_id =(SELECT object_id FROM sys.tables WHERE name = '" + tablename + "')" +
                       "select t.name as 表名,c.Name as 列名,type1.name as 类型,c.length as 长度,col.is_nullable  AS 是否允许非空,col.is_identity  AS 是否自增 from sysColumns c,sys.tables t,sys.types type1,sys.columns col where t.name='singertab' and t.object_id = c.id and c.xusertype=type1.user_type_id and  col.object_id =(SELECT object_id FROM sys.tables WHERE name = '" + tablename + "')";
                List<ProcedureInfo> list = new List<ProcedureInfo>();

                cn.Open();
                cn.ChangeDatabase(comboBox1.Text);
                SqlCommand cm = new SqlCommand(sql, cn);
                string table = tablename.Substring(0, 1).ToUpper() + tablename.Substring(1);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        ProcedureInfo pro = new ProcedureInfo();
                        pro.Row = (string)dr[0];
                        pro.Type = (string)dr[1];
                        pro.Length = Convert.ToInt16(dr[2]);
                        pro.IsNull = Convert.ToBoolean(dr[3]);
                        pro.IsIdentity = Convert.ToBoolean(dr[4]);
                        list.Add(pro);
                    }
                }
                SQLstring += "go\r\n";
                cn.Open();
                cn.ChangeDatabase(comboBox1.Text);
                 sql = "select * from " + table;
                 cm = new SqlCommand(sql, cn);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        SQLstring += "insert into " + table + " values(";
                        int i = 0;
                        foreach (ProcedureInfo pro in list)
                        {
                            if (pro.IsIdentity)
                            {
                                i++;
                                continue;
                            }
                            if (DBNull.Value == dr[i])
                            {
                                SQLstring += "null";
                            }
                            else if (findModelsType(pro.Type) == "string" || findModelsType(pro.Type) == "DateTime")
                            {
                                SQLstring += "'";
                                SQLstring += dr[i].ToString();
                                SQLstring += "'";
                            }
                            else if (findModelsType(pro.Type)=="bool")
                            {
                                if(Convert.ToBoolean(dr[i]))
                                    SQLstring += 1.ToString();
                                else
                                    SQLstring += 0.ToString();
                            }
                            else
                            {
                                SQLstring += dr[i].ToString();
                            }
                            if (i < list.Count-1)
                            {
                                SQLstring += " ,";
                            }
                            i++;
                        }
                        SQLstring += ")";
                        SQLstring += "\r\n";
                        
                    }
                }
            }
            if (!Directory.Exists(path + @"\SQL"))
            {
                Directory.CreateDirectory(path + @"\SQL");
            }
            file = new FileInfo(path + @"\SQL" + "\\" + comboBox1.Text + "SQL.txt");
            writer = file.CreateText();
            writer.WriteLine(SQLstring);
            writer.Close();
            this.chenggong.Visible = true;
            this.timer1.Enabled = true;
        }
        #endregion

        #region 生成三层方法
        public void Add()
        {
            int count = 0;
            if (checkBox2.Checked)
            {
                count++;
            }
            if (checkBox3.Checked)
            {
                count++;
                if (!Directory.Exists(path + @"\DAL"))
                {
                    Directory.CreateDirectory(path + @"\DAL");
                }
            }
            if (checkBox4.Checked)
            {
                count++;
                if (!Directory.Exists(path + @"\BLL"))
                {
                    Directory.CreateDirectory(path + @"\BLL");
                }
            }
            if (checkBox5.Checked)
            {
                count++;
                if (!Directory.Exists(path + @"\Models"))
                {
                    Directory.CreateDirectory(path + @"\Models");
                }
            }
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = li.Count;
            inserString = "\r\nuse " + database + "\r\ngo\r\n"; ;
            dalString = "";
            bllString = "";
            foreach (string tablename in li)
            {

                string sql = "SELECT col.name AS 列名, typ.name as 数据类型,col.max_length AS 占用字节数," +
                       "  col.is_nullable  AS 是否允许非空,col.is_identity  AS 是否自增," +
                       " case when exists  ( SELECT 1  FROM sys.indexes idx join sys.index_columns idxCol on (idx.object_id = idxCol.object_id)" +
                       "  WHERE idx.object_id = col.object_id AND idxCol.index_column_id = col.column_id AND idx.is_primary_key = 1" +
                       " ) THEN 1 ELSE 0 END  AS 是否是主键 FROM sys.columns col left join sys.types typ on (col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id)" +
                       " WHERE col.object_id =(SELECT object_id FROM sys.tables WHERE name = '" + tablename + "')" +
                       "select t.name as 表名,c.Name as 列名,type1.name as 类型,c.length as 长度,col.is_nullable  AS 是否允许非空,col.is_identity  AS 是否自增 from sysColumns c,sys.tables t,sys.types type1,sys.columns col where t.name='singertab' and t.object_id = c.id and c.xusertype=type1.user_type_id and  col.object_id =(SELECT object_id FROM sys.tables WHERE name = '" + tablename + "')";

                list = new List<ProcedureInfo>();

                cn.Open();
                cn.ChangeDatabase(comboBox1.Text);
                SqlCommand cm = new SqlCommand(sql, cn);
                table = tablename.Substring(0, 1).ToUpper() + tablename.Substring(1);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        ProcedureInfo pro = new ProcedureInfo();
                        pro.Row = (string)dr[0];
                        pro.Type = (string)dr[1];
                        pro.Length = Convert.ToInt16(dr[2]);
                        pro.IsNull = Convert.ToBoolean(dr[3]);
                        pro.IsIdentity = Convert.ToBoolean(dr[4]);
                        list.Add(pro);
                    }
                }
                if (checkBox2.Checked)
                {

                    if (myMethod.MyAdd1)
                    {
                        SQLAdd();
                    }
                    if (myMethod.MyAddReturnId1)
                    {
                        SQLAddReturnId();
                    }
                    if (myMethod.MyChange1)
                    {
                        SQLChange();
                    }
                    if (myMethod.MyDelete1)
                    {
                        SQLDelete();
                    }
                    if (myMethod.MySelectAll1)
                    {
                        SQLSelectAll();
                    }
                    if (myMethod.MySelectById1)
                    {
                        SQLSelectById();
                    }
                    if (myMethod.MySelectByWhere1)
                    {
                        SQLSelectByWhere();
                    }
                }
                if (checkBox3.Checked)
                {

                    dalString = "";
                    dalString += "using System;\r\n";
                    dalString += "using System.Collections.Generic;\r\n";
                    dalString += "using " + textBox9.Text.Trim() + ";\r\n";
                    dalString += "using " + textBox7.Text.Trim() + ";\r\n";
                    dalString += "using System.Data;\r\n";
                    dalString += "using System.Data.SqlClient;\r\n";
                    dalString += "\r\n";
                    dalString += "namespace " + textBox5.Text.Trim() + "\r\n";
                    dalString += "{\r\n";
                    dalString += "    public class " + table + "Service\r\n";
                    dalString += "    {\r\n";

                    if (myMethod.MyAdd1)
                    {
                        DALAdd();
                    }
                    if (myMethod.MyAddReturnId1)
                    {
                        DALAddReturnId();
                    }
                    if (myMethod.MyChange1)
                    {
                        DALChange();
                    }
                    if (myMethod.MyDelete1)
                    {
                        DALDelete();
                    }
                    if (myMethod.MySelectAll1)
                    {
                        DALSelectAll();
                    }
                    if (myMethod.MySelectById1)
                    {
                        DALSelectById();
                    }
                    if (myMethod.MySelectByWhere1)
                    {
                        DALSelectByWhere();
                    }
                    file = new FileInfo(path + @"\DAL" + "\\" + table + "Service.cs");
                    writer = file.CreateText();
                    writer.WriteLine(dalString);
                    writer.Close();
                }
                if (checkBox4.Checked)
                {

                    bllString = "";
                    bllString += "using System;\r\n";
                    bllString += "using System.Collections.Generic;\r\n";
                    bllString += "using " + textBox5.Text.Trim() + ";\r\n";
                    bllString += "using " + textBox9.Text.Trim() + ";\r\n";
                    bllString += "\r\n";
                    bllString += "namespace " + textBox8.Text.Trim() + "\r\n";
                    bllString += "{\r\n   public class " + table + "Manager\r\n";
                    bllString += "   {\r\n";
                    bllString += "        \r\n       ";
                    bllString += fristToUpper(table) + "Service dal = new "+fristToUpper(table) + "Service();\r\n\r\n";
                    
                    if (myMethod.MyAdd1)
                    {
                        BLLAdd();
                    }
                    if (myMethod.MyAddReturnId1)
                    {
                        BLLAddReturnId();
                    }
                    if (myMethod.MyChange1)
                    {
                        BLLChange();
                    }
                    if (myMethod.MyDelete1)
                    {
                        BLLDelete();
                    }
                    if (myMethod.MySelectAll1)
                    {
                        BLLSelectAll();
                    }
                    if (myMethod.MySelectById1)
                    {
                        BLLSelectById();
                    }
                    if (myMethod.MySelectByWhere1)
                    {
                        BLLSelectByWhere();
                    }

                    file = new FileInfo(path + @"\BLL\" + table + "Manager.cs");
                    writer = file.CreateText();
                    writer.WriteLine(bllString);
                    writer.Close();

                }

                if (checkBox5.Checked)
                {

                    StreamWriter write = new StreamWriter(path + @"\Models\" + table + ".cs", false, Encoding.Default);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("using System;");
                    sb.Append(Environment.NewLine);
                    sb.Append("using System.Collections.Generic;");
                    sb.Append(Environment.NewLine);
                    sb.Append("using System.Text;");
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("namespace ");
                    sb.Append(textBox9.Text);
                    sb.Append(Environment.NewLine);
                    sb.Append("{");
                    sb.Append(Environment.NewLine);
                    sb.Append("    [Serializable]");
                    sb.Append(Environment.NewLine);
                    sb.Append("    public class ");
                    sb.Append(table);
                    sb.Append(Environment.NewLine);
                    sb.Append(createplace(4));
                    sb.Append("{");
                    sb.Append(Environment.NewLine);

                    foreach (ProcedureInfo pro in list)
                    {
                        string name = pro.Row;
                        string type = pro.Type;

                        sb.Append(createplace(8));
                        sb.Append("private");
                        sb.Append(" ");
                        sb.Append(findModelsType(type));
                        sb.Append(createplace(1));
                        sb.Append(name.Substring(0, 1).ToLower() + name.Substring(1));
                        sb.Append(";");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append(createplace(8));
                        sb.Append("public");
                        sb.Append(" ");
                        sb.Append(findModelsType(type));
                        sb.Append(" ");
                        sb.Append(name.Substring(0, 1).ToUpper() + name.Substring(1));
                        sb.Append(Environment.NewLine);
                        sb.Append(createplace(8));
                        sb.Append("{");
                        sb.Append(Environment.NewLine);
                        sb.Append(createplace(10));
                        sb.Append("get { return ");
                        sb.Append(name.Substring(0, 1).ToLower() + name.Substring(1));
                        sb.Append(";}");
                        sb.Append(Environment.NewLine);
                        sb.Append(createplace(10));
                        sb.Append("set { ");
                        sb.Append(name.Substring(0, 1).ToLower() + name.Substring(1));
                        sb.Append("=value");
                        sb.Append(";}");
                        sb.Append(Environment.NewLine);
                        sb.Append(createplace(8));
                        sb.Append("}");
                        sb.Append(Environment.NewLine);


                    }
                    sb.Append(createplace(4));
                    sb.Append("}");
                    sb.Append(Environment.NewLine);
                    sb.Append("}");


                    write.WriteLine(sb.ToString());
                    write.Flush();
                    write.Close();
                    write = null;
                }
                progressBar1.Value++;
            }
           
            
            if (!Directory.Exists(path + @"\Procedure"))
            {
                Directory.CreateDirectory(path + @"\Procedure");
            }
            file = new FileInfo(path + @"\Procedure" + "\\" + comboBox1.Text + ".txt");
            writer = file.CreateText();
            writer.WriteLine(inserString);
            writer.Close();

        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 存储过程增加
        /// </summary>
        public void SQLAdd()
        {
            ///增加方法
             i = 0;
            inserString += "-- =============================================\r\n";
            inserString += "-- Author:" + textBox6.Text.Trim() + "<Author,,Name>\r\n";
            inserString += "-- Create date: " + DateTime.Now.ToShortDateString() + "<Create Date,,>\r\n";
            inserString += "-- Description:增加<Description,,>\r\n";
            inserString += "-- =============================================";
            inserString += "\r\n";
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    inserString += "CREATE PROCEDURE " + table + "_Add\r\n\r\n";
                    if (!pro.IsIdentity)
                    {
                        inserString += "@" + pro.Row + " " + pro.Type;
                        if (isString(pro.Type))
                        {
                            inserString += "(" + pro.Length + ")";
                        }
                        if (pro.IsNull)
                            inserString += "=null";
                        inserString += ",\r\n";

                    }
                }
                else
                {

                    inserString += "@" + pro.Row + " " + pro.Type;
                    if (isString(pro.Type))
                    {
                        inserString += "(" + pro.Length + ")";
                    }
                    if (pro.IsNull)
                        inserString += "=null";
                    if (i < list.Count)
                    {
                        inserString += ",";
                    }
                    inserString += "\r\n";
                }
            }
            inserString += "\r\nAS\r\n";
            inserString += "BEGIN\r\n";
            foreach (ProcedureInfo pro in list)
            {
                if (pro.IsNull && pro.Type == "int")
                {
                    inserString += "if(";
                    inserString += "@" + pro.Row + "=0)\r\n";
                    inserString += "	begin\r\n";
                    inserString += "		set @" + pro.Row + "=null\r\n";
                    inserString += "	end\r\n";
                }
            }
            inserString += "	insert into " + table + " values(";
            i = 0;
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    if (!pro.IsIdentity)
                    {
                        inserString += "@" + pro.Row + ",";
                    }
                }
                else
                {
                    inserString += "@" + pro.Row;

                    if (i < list.Count)
                    {
                        inserString += ",";
                    }
                }
            }
            inserString += ")\r\n";
            inserString += "END\r\n";
            inserString += "GO\r\n\r\n\r\n";
        }

        /// <summary>
        /// 存储过程删除
        /// </summary>
        public void SQLDelete()
        {
            inserString += "-- =============================================\r\n";
            inserString += "-- Author:" + textBox6.Text.Trim() + "<Author,,Name>\r\n";
            inserString += "-- Create date: " + DateTime.Now.ToShortDateString() + "<Create Date,,>\r\n";
            inserString += "-- Description:删除<Description,,>\r\n";
            inserString += "-- =============================================";
            inserString += "\r\n";
            foreach (ProcedureInfo pro in list)
            {

                inserString += "CREATE PROCEDURE " + table + "_Delete\r\n\r\n";

                inserString += "@" + pro.Row + " " + pro.Type;
                if (isString(pro.Type))
                {
                    inserString += "(" + pro.Length + ")";
                }
                inserString += "\r\n\r\nAS\r\n";
                inserString += "BEGIN\r\n";
                inserString += "	delete from " + table + " where ";
                inserString += pro.Row + "=@" + pro.Row;
                inserString += "\r\n";
                inserString += "END\r\n";
                inserString += "GO\r\n\r\n\r\n";
                break;
            }
        }

        /// <summary>
        /// 存储过程修改
        /// </summary>
        public void SQLChange()
        {
            i = 0;
            inserString += "-- =============================================\r\n";
            inserString += "-- Author:" + textBox6.Text.Trim() + "<Author,,Name>\r\n";
            inserString += "-- Create date: " + DateTime.Now.ToShortDateString() + "<Create Date,,>\r\n";
            inserString += "-- Description:修改<Description,,>\r\n";
            inserString += "-- =============================================";
            inserString += "\r\n";
            i = 0;
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    inserString += "CREATE PROCEDURE " + table + "_Change\r\n\r\n";
                    inserString += "@" + pro.Row + " " + pro.Type;
                    if (isString(pro.Type))
                    {
                        inserString += "(" + pro.Length + ")";
                    }
                    if (pro.IsNull)
                        inserString += "=null";
                    inserString += ",\r\n";

                }
                else
                {

                    inserString += "@" + pro.Row + " " + pro.Type; ;
                    if (isString(pro.Type))
                    {
                        inserString += "(" + pro.Length + ")";
                    }
                    if (pro.IsNull)
                        inserString += "=null";
                    if (i < list.Count)
                    {
                        inserString += ",";
                    }
                    inserString += "\r\n";
                }
            }
            inserString += "\r\nAS\r\n";
            inserString += "BEGIN\r\n";
            foreach (ProcedureInfo pro in list)
            {
                if (pro.IsNull && pro.Type == "int")
                {
                    inserString += "if(";
                    inserString += "@" + pro.Row + "=0)\r\n";
                    inserString += "	begin\r\n";
                    inserString += "		set @" + pro.Row + "=null\r\n";
                    inserString += "	end\r\n";
                }

            }
            inserString += "	update " + table + " set ";
            i = 0;
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    Id = pro.Row;
                }
                else
                {
                    inserString += pro.Row + "=@" + pro.Row;

                    if (i < list.Count)
                    {
                        inserString += ",";
                    }
                }

            }
            inserString += " where " + Id + "=@" + Id;
            inserString += "\r\n";
            inserString += "END\r\n";
            inserString += "GO\r\n\r\n\r\n";

        }

        /// <summary>
        /// 存储过程通过ID查询
        /// </summary>
        public void SQLSelectById()
        {
            inserString += "-- =============================================\r\n";
            inserString += "-- Author:" + textBox6.Text.Trim() + "<Author,,Name>\r\n";
            inserString += "-- Create date: " + DateTime.Now.ToShortDateString() + "<Create Date,,>\r\n";
            inserString += "-- Description:通过ID查询<Description,,>\r\n";
            inserString += "-- =============================================";
            inserString += "\r\n";
            foreach (ProcedureInfo pro in list)
            {

                inserString += "CREATE PROCEDURE " + table + "_SelectById\r\n\r\n";

                inserString += "@" + pro.Row + " " + pro.Type;
                if (isString(pro.Type))
                {
                    inserString += "(" + pro.Length + ")";
                }
                if (pro.IsNull)
                    inserString += "=null";
                inserString += "\r\n\r\nAS\r\n";
                inserString += "BEGIN\r\n";
                inserString += "	select * from " + table + " where ";
                inserString += pro.Row + "=@" + pro.Row;
                inserString += "\r\n";
                inserString += "END\r\n";
                inserString += "GO\r\n\r\n\r\n";
                break;

            }
        }

        /// <summary>
        /// 存储过程通过ID查询
        /// </summary>
        public void SQLSelectByWhere()
        {
            inserString += "-- =============================================\r\n";
            inserString += "-- Author:" + textBox6.Text.Trim() + "<Author,,Name>\r\n";
            inserString += "-- Create date: " + DateTime.Now.ToShortDateString() + "<Create Date,,>\r\n";
            inserString += "-- Description:通过条件查询<Description,,>\r\n";
            inserString += "-- =============================================";
            inserString += "\r\n";
            foreach (ProcedureInfo pro in list)
            {

                inserString += "CREATE PROCEDURE " + table + "_SelectByWhere\r\n\r\n";
                inserString += "@where varchar(200)";
                inserString += "\r\n\r\nAS\r\n";
                inserString += "BEGIN\r\n";
                inserString += "	declare @String varchar(200)\r\n";
                inserString += "	set @String='select * from " + table + " '+ @where\r\n";
                inserString += "	exec(@String)\r\n";
                inserString += "END\r\n";
                inserString += "GO\r\n\r\n\r\n";
                break;

            }
        }

        /// <summary>
        /// 存储过程查询全部
        /// </summary>
        public void SQLSelectAll()
        {
            inserString += "-- =============================================\r\n";
            inserString += "-- Author:" + textBox6.Text.Trim() + "<Author,,Name>\r\n";
            inserString += "-- Create date: " + DateTime.Now.ToShortDateString() + "<Create Date,,>\r\n";
            inserString += "-- Description:全部查询<Description,,>\r\n";
            inserString += "-- =============================================";
            inserString += "\r\n";
            foreach (ProcedureInfo pro in list)
            {
                inserString += "CREATE PROCEDURE " + table + "_SelectAll";
                inserString += "\r\nAS\r\n";
                inserString += "BEGIN\r\n";
                inserString += "	select * from " + table;
                inserString += "\r\n";
                inserString += "END\r\n";
                inserString += "GO\r\n\r\n\r\n";
                break;
            }

            inserString += "-- ===============================================================================================================================================\r\n\r\n\r\n";

        }
        

        /// <summary>
        /// 存储过程增加返回Id
        /// </summary>
        public void SQLAddReturnId()
        {
            string insertName = "";
            i = 0;
            insertName += "-- =============================================\r\n";
            insertName += "-- Author:" + textBox6.Text.Trim() + "<Author,,Name>\r\n";
            insertName += "-- Create date: " + DateTime.Now.ToShortDateString() + "<Create Date,,>\r\n";
            insertName += "-- Description:增加并返回自增ID<Description,,>\r\n";
            insertName += "-- =============================================";
            insertName += "\r\n";
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    insertName += "CREATE PROCEDURE " + table + "_AddReturnId\r\n\r\n";
                    if (!pro.IsIdentity)
                    {
                        insertName += "@" + pro.Row + " " + pro.Type;
                        if (isString(pro.Type))
                        {
                            insertName += "(" + pro.Length + ")";
                        }
                        if (pro.IsNull)
                            insertName += "=null";
                        insertName += ",\r\n";

                    }
                }
                else
                {

                    insertName += "@" + pro.Row + " " + pro.Type;
                    if (isString(pro.Type))
                    {
                        insertName += "(" + pro.Length + ")";
                    }
                    if (pro.IsNull)
                        insertName += "=null";
                    if (i < list.Count)
                    {
                        insertName += ",";
                    }
                    insertName += "\r\n";
                }
            }
            insertName += "\r\nAS\r\n";
            insertName += "BEGIN\r\n";
            foreach (ProcedureInfo pro in list)
            {
                if (pro.IsNull && pro.Type == "int")
                {
                    insertName += "if(";
                    insertName += "@" + pro.Row + "=0)\r\n";
                    insertName += "	begin\r\n";
                    insertName += "		set @" + pro.Row + "=null\r\n";
                    insertName += "	end\r\n";
                }
            }
            insertName += "	insert into " + table + " values(";
            i = 0;
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    if (!pro.IsIdentity)
                    {
                        insertName += "@" + pro.Row + ",";
                    }
                }
                else
                {
                    insertName += "@" + pro.Row;

                    if (i < list.Count)
                    {
                        insertName += ",";
                    }
                }
            }
            insertName += ")\r\n";
            insertName += "select @@identity \r\n";
            insertName += "END\r\n";
            insertName += "GO\r\n\r\n\r\n";
            inserString += insertName;
        }


        #endregion

        #region 数据访问层
        /// <summary>
        /// 数据访问增加方法
        /// </summary>
        public void DALAdd()
        {
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 增加\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            dalString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            dalString += "        public bool add(" + table + " model)\r\n";
            dalString += "        {\r\n";
            dalString += "            SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "            {\r\n";
            i = 0;
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    if (pro.IsIdentity)
                    {
                        continue;
                    }
                }
                dalString += "                new SqlParameter (\"@" + pro.Row + "\",model." + fristToUpper(pro.Row) + ")";
                if (i < list.Count)
                {
                    dalString += ",";
                }
                dalString += "\r\n";
            }
            dalString += "            };\r\n";
            dalString += "           return " + textBox10.Text + ".ExecuteNonQuery(\"" + table + "_Add\",param);\r\n";
            dalString += "        }\r\n";
        }
        /// <summary>
        /// 数据访问增加返回ID方法
        /// </summary>
        public void DALAddReturnId()
        {
            string dalName = "";
            dalName += "        /// <summary>\r\n";
            dalName += "        /// 增加\r\n";
            dalName += "        /// </summary>\r\n";
            dalName += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            dalName += "        /// <returns>int值,返回自增ID</returns>\r\n";
            dalName += "        public int addReturnId(" + table + " model)\r\n";
            dalName += "        {\r\n";
            dalName += "            SqlParameter[] param = new SqlParameter[]\r\n";
            dalName += "            {\r\n";
            i = 0;
            foreach (ProcedureInfo pro in list)
            {
                i++;
                if (i == 1)
                {
                    if (pro.IsIdentity)
                    {
                        continue;
                    }
                }
                dalName += "                new SqlParameter (\"@" + pro.Row + "\",model." + fristToUpper(pro.Row) + ")";
                if (i < list.Count)
                {
                    dalName += ",";
                }
                dalName += "\r\n";
            }
            dalName += "            };\r\n";
            dalName += "           return Convert.ToInt32(" + textBox10.Text + ".ExecuteScalar (\"" + table + "_AddReturnId\",param));\r\n";
            dalName += "        }\r\n";
            dalString += dalName;
        }
        /// <summary>
        /// 数据访问删除方法
        /// </summary>
        public void DALDelete()
        {
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 删除\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            dalString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            dalString += "        public bool delete(int Id)\r\n";
            dalString += "        {\r\n";
            dalString += "            SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "            {\r\n";
            dalString += "                new SqlParameter (\"@" + list[0].Row + "\",Id)\r\n";
            dalString += "            };\r\n";
            dalString += "           return " + textBox10.Text + ".ExecuteNonQuery (\"" + table + "_Delete\",param);\r\n";
            dalString += "        }\r\n";
        } 
        /// <summary>
        /// 数据访问修改方法
        /// </summary>
        public void DALChange()
        {
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 修改\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            dalString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            dalString += "        public bool change(" + table + " model)\r\n";
            dalString += "        {\r\n";
            dalString += "            SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "            {\r\n";
            i = 0;
            foreach (ProcedureInfo pro in list)
            {
                i++;
                dalString += "                new SqlParameter (\"@" + pro.Row + "\",model." + fristToUpper(pro.Row) + ")";
                if (i < list.Count)
                {
                    dalString += ",";
                }
                dalString += "\r\n";
            }
            dalString += "            };\r\n";
            dalString += "           return " + textBox10.Text + ".ExecuteNonQuery(\"" + table + "_Change\",param);\r\n";
            dalString += "        }\r\n";
        } 
        /// <summary>
        /// 数据访问通过ID查询
        /// </summary>
        public void DALSelectById()
        {
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 通过Id查询\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            dalString += "        /// <returns>" + table + "实体类对象</returns>\r\n";
            dalString += "        public " + table + " selectById(int Id)\r\n";
            dalString += "        {\r\n";
            dalString += "            SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "            {\r\n";
            dalString += "                new SqlParameter (\"@" + list[0].Row + "\",Id)\r\n";
            dalString += "            };\r\n";
            dalString += "            " + table + " model = new " + table + "();\r\n";
            dalString += "            using (SqlDataReader dr = " + textBox10.Text + ".ExecuteReader(\"" + table + "_SelectById\", param))\r\n";
            dalString += "            {\r\n";
            dalString += "                if (dr.Read())\r\n";
            dalString += "                {\r\n";
            foreach (ProcedureInfo pro in list)
            {
                if (pro.IsNull)
                    dalString += "                    if (DBNull.Value!=dr[\"" + pro.Row + "\"])\r\n    ";
                if (findType(pro.Type) == ".ToString()")
                {
                    dalString += "                    model." + fristToUpper(pro.Row) + " = dr[\"" + pro.Row + "\"].ToString();\r\n";
                }
                else
                {
                    dalString += "                    model." + fristToUpper(pro.Row) + "= " + findType(pro.Type) + "dr[\"" + pro.Row + "\"]);\r\n";
                }
            }
            dalString += "                }\r\n";
            dalString += "            }\r\n";
            dalString += "            return model;\r\n";
            dalString += "        }\r\n";
        } 
        /// <summary>
        /// 数据访问查询全部
        /// </summary>
        public void DALSelectAll()
        {
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 查看全部\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <returns>list集合</returns>\r\n";
            dalString += "        public List<" + table + "> selectAll()\r\n";
            dalString += "        {\r\n";
            dalString += "            List<" + table + "> list = new List<" + table + ">();\r\n";
            dalString += "            " + table + " model = null;\r\n";
            dalString += "            using (SqlDataReader dr = " + textBox10.Text + ".ExecuteReader(\"" + table + "_SelectAll\", null))\r\n";
            dalString += "            {\r\n";
            dalString += "                while (dr.Read())\r\n";
            dalString += "                {\r\n";
            dalString += "                    model = new " + table + "();\r\n";
            foreach (ProcedureInfo pro in list)
            {
                if (pro.IsNull)
                    dalString += "                    if (DBNull.Value!=dr[\"" + pro.Row + "\"])\r\n    ";
                if (findType(pro.Type) == ".ToString()")
                {
                    dalString += "                    model." + fristToUpper(pro.Row) + " = dr[\"" + pro.Row + "\"].ToString();\r\n";
                }
                else
                {
                    dalString += "                    model." + fristToUpper(pro.Row) + "= " + findType(pro.Type) + "dr[\"" + pro.Row + "\"]);\r\n";
                }
            }
            dalString += "                    list.Add(model);\r\n";
            dalString += "                }\r\n";
            dalString += "            }\r\n";
            dalString += "            return list;\r\n";
            dalString += "        }\r\n";
        }
        /// <summary>
        /// 数据访问通过条件查询
        /// </summary>
        public void DALSelectByWhere()
        {
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 通过条件查询\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"WhereString\">查询条件</param>\r\n";
            dalString += "        /// <returns>" + table + "实体类对象</returns>\r\n";
            dalString += "        public List<" + table + "> selectByWhere(string WhereString)\r\n";
            dalString += "        {\r\n";
            dalString += "            SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "            {\r\n";
            dalString += "                new SqlParameter (\"@where\",WhereString)\r\n";
            dalString += "            };\r\n";
            dalString += "            List<" + table + "> list = new List<" + table + ">();\r\n";
            dalString += "            " + table + " model = null;\r\n";
            dalString += "            using (SqlDataReader dr = " + textBox10.Text + ".ExecuteReader(\"" + table + "_SelectByWhere\", param))\r\n";
            dalString += "            {\r\n";
            dalString += "                while (dr.Read())\r\n";
            dalString += "                {\r\n";
            dalString += "                    model = new " + table + "();\r\n";
            foreach (ProcedureInfo pro in list)
            {
                if (pro.IsNull)
                    dalString += "                    if (DBNull.Value!=dr[\"" + pro.Row + "\"])\r\n    ";
                if (findType(pro.Type) == ".ToString()")
                {
                    dalString += "                    model." + fristToUpper(pro.Row) + " = dr[\"" + pro.Row + "\"].ToString();\r\n";
                }
                else
                {
                    dalString += "                    model." + fristToUpper(pro.Row) + "= " + findType(pro.Type) + "dr[\"" + pro.Row + "\"]);\r\n";
                }
            }
            dalString += "                    list.Add(model);\r\n";
            dalString += "                }\r\n";
            dalString += "            }\r\n";
            dalString += "            return list;\r\n";
            dalString += "        }\r\n";
            dalString += "    }\r\n";
            dalString += "}\r\n";
        }
        #endregion

        #region 业务逻辑层

        /// <summary>
        /// 业务逻辑增加
        /// </summary>
        public void BLLAdd()
        {
            bllString += "        /// <summary>\r\n";
            bllString += "        /// 增加\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            bllString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            bllString += "        public bool add(" + table + " model)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.add(model);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
        }
        /// <summary>
        /// 业务逻辑增加并返回ID
        /// </summary>
        public void BLLAddReturnId()
        {
            bllString += "        /// <summary>\r\n";
            bllString += "        /// 增加\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            bllString += "        /// <returns>int值,返回自增ID</returns>\r\n";
            bllString += "        public int addReturnId(" + table + " model)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.addReturnId(model);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
        }
        /// <summary>
        /// 业务逻辑删除
        /// </summary>
        public void BLLDelete()
        {
            bllString += "        /// <summary>\r\n";
            bllString += "        /// 删除\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            bllString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            bllString += "        public bool delete(int Id)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.delete(Id);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
        }
        /// <summary>
        /// 业务逻辑修改
        /// </summary>
        public void BLLChange()
        {
            bllString += "        /// <summary>\r\n";
            bllString += "        /// 修改\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            bllString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            bllString += "        public bool change(" + table + " model)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.change(model);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
        }
        /// <summary>
        /// 业务逻辑查询全部
        /// </summary>
        public void BLLSelectAll()
        {
            bllString += "        /// <summary>\r\n";
            bllString += "        /// 查询全部\r\n";
            bllString += "        /// </summary>\r\n";
            dalString += "        /// <returns>" + table + "实体类对象集合</returns>\r\n";
            bllString += "        public List<" + table + "> selectAll()\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.selectAll();\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
        }
        /// <summary>
        /// 业务逻辑通过ID查询
        /// </summary>
        public void BLLSelectById()
        {
            bllString += "        /// <summary>\r\n";
            bllString += "        /// 通过Id查询\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            dalString += "        /// <returns>" + table + "实体类对象</returns>\r\n";
            bllString += "        public " + table + " selectById(int Id)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.selectById(Id);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
        }
        /// <summary>
        /// 业务逻辑通过条件查询
        /// </summary>
        public void BLLSelectByWhere()
        {
            bllString += "        /// <summary>\r\n";
            bllString += "        /// 通过条件查询\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"WhereString\">主键Id</param>\r\n";
            dalString += "        /// <returns>" + table + "实体类对象集合</returns>\r\n";
            bllString += "        public List<" + table + "> selectByWhere(string WhereString)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.selectByWhere(WhereString);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
            bllString += "    }\r\n";
            bllString += "}\r\n";
        }

        #endregion

        #region  选择方法
        private void button6_Click(object sender, EventArgs e)
        {
            FrmSelectMethod f = new FrmSelectMethod();
            f.form3 = this;
            f.ShowDialog();          
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.chenggong.Visible = false;
            this.progressBar1.Value = 0;
            this.timer1.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmDBHelp f = new FrmDBHelp();
            f.Show();
        }
    }
}
