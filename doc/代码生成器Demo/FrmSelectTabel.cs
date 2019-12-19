using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sql
{
    public partial class FrmSelectTabel : Form
    {
        List<string> list = null;
        public MyGenerator form3;
        public FrmSelectTabel()
        {
            InitializeComponent();
        }
        public FrmSelectTabel(List<string> list)
        {
            InitializeComponent();
            this.list = list;
        }
         List<Panel> listpanel = new List<Panel>();
        private void Form2_Load(object sender, EventArgs e)
        {
            listpanel.Clear();
            this.panel1.Controls.Clear();
            this.panel2.Controls.Clear();
            //List<string> list = ((Form1)Owner).list;
            int x = 2; int y = 0;
            int x1 = 2;
            int i=0;
            int y1=12;
            List<List<Object>> listAll= GetChangePage(list.Cast<Object>().ToList(), 70);
            Panel p = null;
            LinkLabel ll = null;
            CheckBox ch = null;
            foreach (List<Object> lis in listAll)
            {
                i++;
                p = new Panel();
                if (i == 1)
                {
                    this.panel1.Controls.Add(p);
                }
                p.Name = "p-" + i;
                p.Tag = i;
                p.Dock = DockStyle.Fill;
                
                ll = new LinkLabel();
                this.panel2.Controls.Add(ll);
                ll.Name = "llll" + i;
                ll.Text = i.ToString();
                ll.Left =x1;
                ll.AutoSize = true;
                x1 += ll.Width;
                ll.Top = y1;
                ll.ForeColor = Color.Black;
                ll.Width = 7;
                
                
                ll.Click += new EventHandler(ll_Click);
                x1 += ll.Width;
                x = 2;
                y = 0;
                if (i != 0 && i % 25 == 0)
                {
                    y1 += 15;
                    x1 = 2;
                }
                foreach (string li in lis)
                {
                    ch = new CheckBox();
                    ch.Left = x;
                    ch.Top = y;
                    ch.Text = getName(li);
                    ch.Tag = li;
                    ch.Checked = true;
                    ch.MouseEnter += new EventHandler(ch_MouseEnter);
                    ch.MouseLeave += new EventHandler(ch_MouseLeave);
                    p.Controls.Add(ch);

                    y += ch.Height;
                    if (y >= this.Size.Height - 200)
                    {
                        x += ch.Width;
                        y = 0;
                    }
                }
                listpanel.Add(p);
            }
  
            
        }

        void ll_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int i = Convert.ToInt32((sender as LinkLabel).Text);
            Panel pa = new Panel();
            if (listpanel.Count > 0)
            {
                pa = listpanel[i - 1];
            }
            panel1.Controls.Add(pa);
            //foreach (Control con in panel1.Controls)
            //{
            //    if (con is Panel)
            //    {
            //        Panel pa = con as Panel;
            //        if (pa.Tag != null)
            //        {
            //            listpanel[
            //            if (pa.Tag.ToString() == (sender as LinkLabel).Text)
            //            {
            //                pa.Visible = true;
            //                pa.BringToFront();
            //            }
            //            else
            //            {
            //                pa.Visible = false;
            //                pa.SendToBack();
            //            }
            //        }
            //    }
            //}
        }

        void ch_MouseLeave(object sender, EventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            this.label2.Text = "暂无";
        }

        void ch_MouseEnter(object sender, EventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            this.label2.Text = ch.Tag.ToString();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                for (int i = 0; i < panel1.Controls.Count; i++)
                {
                    Panel pa = (Panel)panel1.Controls[i];
                    foreach (Control con in pa.Controls)
                    {
                        if (con is CheckBox)
                        {
                            CheckBox ch = con as CheckBox;
                            ch.Checked = true;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < panel1.Controls.Count; i++)
                {
                    Panel pa = (Panel)panel1.Controls[i];
                    foreach (Control con in pa.Controls)
                    {
                        if (con is CheckBox)
                        {
                            CheckBox ch = con as CheckBox;
                            ch.Checked = false;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list=new List<string> ();
            for (int i = 0; i < panel1.Controls.Count; i++)
            {

                Panel pa = (Panel)panel1.Controls[i];
                foreach (Control con in pa.Controls)
                {
                    if (con is CheckBox)
                    {
                        CheckBox ch = con as CheckBox;
                        if (ch.Checked)
                            list.Add(ch.Text);
                    }
                }
            }
            form3.li = list;
            this.Close();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public  List<List<Object>> GetChangePage(List<Object> list, int count)
        {

            List<List<Object>> listBig = new List<List<object>>();
            int count1 = list.Count / count;
            for (int i = 0; i < count1; i++)
            {
                List<Object> list2 = new List<Object>();
                for (int j = i * count; j < (i + 1) * count; j++)
                {
                    list2.Add(list[j]);
                }
                listBig.Add(list2);
            }
            if (list.Count % count != 0)
            {
                List<Object> list3 = new List<Object>();
                for (int i = count1 * count; i < list.Count; i++)
                {
                    list3.Add(list[i]);
                }
                listBig.Add(list3);
            }
            return listBig;
        }
        public string getName(string name)
        {
            if (name.Length >= 9)
            {
                name = name.Substring(0, 9) + "...";
            }
            return name;
        }
    }
}
