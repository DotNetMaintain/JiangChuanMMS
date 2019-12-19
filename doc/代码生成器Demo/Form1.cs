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

namespace sql
{
    public partial class Form1 : Form
    {
        
        string connectstring;
        SqlConnection cn;
        List<string> list = new List<string>();
        List<string> li = new List<string>();
        string userId = "";
        string password = "";
        string database = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = textBox3.Enabled = !checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;

             database = textBox1.Text;
             userId = textBox2.Text;
             password = textBox3.Text;
            try
            {
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
                cn.Open();
                SqlCommand cm = new SqlCommand(sql, cn);
                SqlDataReader dr = cm.ExecuteReader();
                
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString ());
                    list.Add(dr[0].ToString());
                }
                comboBox1.SelectedIndex = 0;
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cn.Open();
            cn.ChangeDatabase(comboBox1.Text);
            SqlCommand cm = new SqlCommand("select name from sys.Tables", cn);
            SqlDataReader dr = cm.ExecuteReader();


            while (dr.Read())
            {
                li.Add(dr[0].ToString());

            }
            dr.Close();
            cn.Close();
            
          

            FrmSelectTabel a = new FrmSelectTabel(li);
          
                a.Show();
           
            
            
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog files = new FolderBrowserDialog();
            if (files.ShowDialog() == DialogResult.OK )
            {
                textBox4.Text = files.SelectedPath;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cn.ChangeDatabase(comboBox1.Text);
                SqlCommand cm = new SqlCommand("select name from sys.Tables", cn);
                SqlDataReader dr=cm.ExecuteReader ();
                
                
                while(dr.Read ())
                {
                    li.Add(dr[0].ToString ());
                    
                }
                dr.Close();
                cn.Close();
                
                progressBar1.Maximum = li.Count;
                progressBar1.Minimum = 0;
                foreach (string table in li)
                {
                    createmodel(table);
                    progressBar1.Value++;
                    Thread.Sleep(100);
                    label10.Text = string.Format(@"{0}\{1}", progressBar1.Value, progressBar1.Maximum);
                    label10.Refresh();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        private void createmodel(string table)
        {

            string tablename = table.Replace(" ", "_");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\model";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string nameplace = "model";
            string sql = string.Format("select * from {0} where 1=2", table);

            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                nameplace = textBox5.Text;
            }
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                path = textBox4.Text;
            }
            try
            {
                cn.Open();
                cn.ChangeDatabase(comboBox1.Text);
                SqlCommand cm = new SqlCommand(sql, cn);
                SqlDataReader dr = cm.ExecuteReader(CommandBehavior.KeyInfo);

                DataTable dt = dr.GetSchemaTable();
                dr.Close();
                cn.Close();

                StreamWriter write = new StreamWriter(path + @"\" + tablename + ".cs", false, Encoding.Default);
                StringBuilder sb = new StringBuilder();
                sb.Append("using System;");
                sb.Append(Environment.NewLine);
                sb.Append("using System.Collections.Generic;");
                sb.Append(Environment.NewLine);
                sb.Append("using System.Text;");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("namespace ");
                sb.Append(nameplace);
                sb.Append(Environment.NewLine);
                sb.Append("{");
                sb.Append(Environment.NewLine);
                sb.Append("    public class ");
                sb.Append(tablename);
                sb.Append(Environment.NewLine);
                sb.Append(createplace(4));
                sb.Append("{");
                sb.Append(Environment.NewLine);

                foreach (DataRow ss in dt.Rows)
                {
                    string name = ss["ColumnName"].ToString();
                    string type = ss["DataType"].ToString();

                    sb.Append(createplace(8));
                    sb.Append("private");
                    sb.Append(" ");
                    sb.Append(type);
                    sb.Append(createplace(1));
                    sb.Append(name);
                    sb.Append(";");
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append(createplace(8));
                    sb.Append("public");
                    sb.Append(" ");
                    sb.Append(type);
                    sb.Append(" ");
                    sb.Append(name.Substring(0, 1).ToUpper() + name.Substring(1));
                    sb.Append(Environment.NewLine);
                    sb.Append(createplace(8));
                    sb.Append("{");
                    sb.Append(Environment.NewLine);
                    sb.Append(createplace(10));
                    sb.Append("get { return ");
                    sb.Append(name);
                    sb.Append(";}");
                    sb.Append(Environment.NewLine);
                    sb.Append(createplace(10));
                    sb.Append("set { ");
                    sb.Append(name);
                    sb.Append("=value");
                    sb.Append(";}");
                    sb.Append(Environment.NewLine);
                    sb.Append(createplace (8));
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }  
            
        }
        private string createplace(int a)
        {
            return new string(' ', a);
        }
    
    }
}
