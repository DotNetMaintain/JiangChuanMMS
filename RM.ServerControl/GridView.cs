using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RM.ServerControl
{
    public class GridView : System.Web.UI.WebControls.GridView
    {
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("数据源为空时是否呈现表头")]
        [TypeConverter(typeof (BooleanConverter))]
        public bool EnableEmptyContentRender
        {
            get
            {
                if (ViewState["EnableEmptyContentRender"] == null)
                {
                    return true;
                }
                return Convert.ToBoolean(ViewState["EnableEmptyContentRender"]);
            }

            set { ViewState["EnableEmptyContentRender"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        [Description("数据源为空时表格样式类")]
        public string EmptyDataTableCssClass
        {
            get
            {
                if (ViewState["EmptyDataTableCssClass"] == null)
                {
                    return "";
                }
                return ViewState["EmptyDataTableCssClass"].ToString();
            }
            set { ViewState["EmptyDataTableCssClass"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        [Description("数据源为空时标题行样式类")]
        public string EmptyDataTitleRowCssClass
        {
            get
            {
                if (ViewState["EmptyDataTitleRowCssClass"] == null)
                {
                    return "";
                }
                return ViewState["EmptyDataTitleRowCssClass"].ToString();
            }
            set { ViewState["EmptyDataTitleRowCssClass"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        [Description("数据源为空时内容行样式类")]
        public string EmptyDataContentRowCssClass
        {
            get
            {
                if (ViewState["EmptyDataContentRowCssClass"] == null)
                {
                    return "";
                }
                return ViewState["EmptyDataContentRowCssClass"].ToString();
            }
            set { ViewState["EmptyDataContentRowCssClass"] = value; }
        }

        /// <summary>
        ///   GridView数据为空时呈现的样式
        /// </summary>
        /// <param name="writer"> </param>
        protected virtual void RenderEmptyContent(HtmlTextWriter writer)
        {
            Table t = new Table(); //创建一个HTML的Table
            t.GridLines = GridLines; //设置Table的线型与GridView相同
            t.BorderStyle = BorderStyle; //设置边界风格与GridView相同
            t.BorderWidth = 0; //??
            t.CellPadding = 1;
            t.CellSpacing = 1;
            t.HorizontalAlign = HorizontalAlign; //设置水平对齐风格与GridView相同
            t.Width = Width; //设置Table宽度与GridView相同
            t.CopyBaseAttributes(this);
            t.BorderColor = BorderColor; //设置边界颜色与GridView相同
            t.EnableTheming = EnableTheming; //设置主题是否生效与GridView相同
            t.ForeColor = ForeColor; //设置前景色与GridView相同
            t.SkinID = SkinID; //设置皮肤样式与GridView相同
            t.ToolTip = ToolTip; //设置提示信息与GridView相同
            t.Visible = Visible; //设置是否可见与GridView相同
            t.Font.CopyFrom(Font); //设置字体对象与GridView相同
            //设置Table的层叠样式表
            t.CssClass = EmptyDataTableCssClass != "" ? EmptyDataTableCssClass : CssClass;
            TableRow row = new TableRow(); //新建一个行
            row.CssClass = EmptyDataTitleRowCssClass; //设置行的与层叠样式表
            row.Height = 25;
            //将行添加到Table中
            t.Rows.Add(row);
            //根据GridView中的字段创建Table行的单元格
            foreach (DataControlField field in Columns)
            {
                if (field.Visible) //如果GridView中的该字段可见
                {
                    TableCell cell = new TableCell(); //创建单元格
                    cell.Text = field.HeaderText; //设置单元格文本
                    row.Cells.Add(cell); //将单元格添加到行中
                }
            }
            TableRow row2 = new TableRow(); //新建第二行
            row2.CssClass = EmptyDataContentRowCssClass; //设置行的与层叠样式表
            t.Rows.Add(row2);
            TableCell msgCell = new TableCell();
            if (EmptyDataTemplate != null) //如果GridView中设置了空模板
            {
                EmptyDataTemplate.InstantiateIn(msgCell);
            }
            else
            {
                msgCell.Text = EmptyDataText; //设置单元格文本为空数据提示
            }
            msgCell.HorizontalAlign = HorizontalAlign.Center; //设置单元格的水平对齐
            msgCell.ColumnSpan = Columns.Count; //设置单元格的列合并
            row2.Cells.Add(msgCell);
            t.RenderControl(writer); //将Table发送给HTML呈现流
        }

        /// <summary>
        ///   重写Control类的Render方法
        /// </summary>
        /// <param name="writer"> </param>
        protected override void Render(HtmlTextWriter writer)
        {
            //如果GridView数据为空
            if (EnableEmptyContentRender && (Rows.Count == 0 || Rows[0].RowType == DataControlRowType.EmptyDataRow))
            {
                RenderEmptyContent(writer); //调用自定义呈现方法
            }
            else
            {
                base.Render(writer); //调用基类呈现方法
            }
        }
    }
}