using RM.Busines;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using RM.ServiceProvider;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;
using RM.ServerControl;

namespace RM.Web.MMS.MMS_Requisition
{
    public partial class Requisition_Query : PageBase
{
    private string SqlWhere
    {
        get
        {
            if (ViewState["SqlWhere"] == null || ViewState["SqlWhere"].ToString() == "")
            {
                return "";
            }
            else
            {
                return ViewState["SqlWhere"].ToString();
            }
        }
        set { ViewState["SqlWhere"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        //this.PageControl1.pageHandler += new EventHandler(this.pager_PageChanged);
        if (!IsPostBack)
        {
            SqlWhere = "a.operator='" + RequestSession.GetSessionUser().UserId + "'";
            List<TPurchasePlanQuery> sumList = QueryService.Instance.PurchasePlanQuery(SqlWhere, -1, OurPager1.PageSize);
            OurPager1.RecordCount = Convert.ToInt32(sumList[0].ID);
            LoadData(SqlWhere, OurPager1.CurrentPageIndex);
        }
    }

    private void LoadData(string sqlWhere, int newPageIndex)
    {
        sqlWhere = "a.operator='" + RequestSession.GetSessionUser().UserId+ "'";

        List<TPurchasePlanQuery> lst = QueryService.Instance.PurchasePlanQuery(sqlWhere, newPageIndex,
                                                                               OurPager1.PageSize);
        dgvInfo.DataKeyNames = new[] {"ID"}; //设置GridView数据主键
        dgvInfo.DataSource = lst;
        dgvInfo.DataBind();
    }

    protected void dgvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandName)) //判断命令名是否为空
        {
            if (e.CommandName == "Edi") //如果触发的是详细信息按钮事件
            {
                int index = Convert.ToInt32(e.CommandArgument); //取GridView行索引
                RM.ServerControl.GridView grid = (RM.ServerControl.GridView)e.CommandSource; //取当前操作的GridView
                int id = Convert.ToInt32(grid.DataKeys[index].Value); //取GridView主键值
                Response.Redirect(@"~/MMS/MMS_Requisition/Requisition_Form.aspx?Query=True&&ID=" + id.ToString());
                LoadData(SqlWhere, OurPager1.CurrentPageIndex);
            }
        }


       
    }

    protected void dgvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        (sender as RM.ServerControl.GridView).PageIndex = e.NewPageIndex; //指定GridView新页索引
        (sender as RM.ServerControl.GridView).DataBind(); //GridView数据源绑定
    }

    protected void dgvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            List<TPurchasePlanQuery> sumList = QueryService.Instance.PurchasePlanQuery(SqlWhere, -1, OurPager1.PageSize);
            if (sumList.Count > 0)
            {
                RM.ServerControl.GridView grid = sender as RM.ServerControl.GridView; //取当前操作的GridView
                TPurchasePlanQuery sum = sumList[0];
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            switch (e.Row.Cells[5].Text.ToString().Trim()){
                case "1":
                    e.Row.Cells[5].Text = "未审核";
                    break;
                case "2":
                    e.Row.Cells[5].Text = "已审核";
                    break;
                case "3":
                    e.Row.Cells[5].Text = "库房审核退回";
                    break;
                case "4":
                    e.Row.Cells[5].Text = "院长审核退回";
                    break;
                
                default:
                    e.Row.Cells[5].Text = "未审核";
                    break;
            
            }

            if (e.Row.Cells[5].Text.ToString() == "1")
            {
                e.Row.Cells[5].Text = "未审核";
            }
        }

    }

    protected void btnQueryCondition_Click(object sender, EventArgs e)
    {
        SqlWhere = hidQueryCondition.Value;
        if (!string.IsNullOrEmpty(SqlWhere))
        {
            List<TPurchasePlanQuery> sumList = QueryService.Instance.PurchasePlanQuery(SqlWhere, -1, OurPager1.PageSize);
            OurPager1.RecordCount = Convert.ToInt32(sumList[0].ID);
            LoadData(SqlWhere, OurPager1.CurrentPageIndex);
        }
    }

    protected void OurPager1_PageChanged(object sender, PageArgs e)
    {
        LoadData(SqlWhere, e.NewPageIndex);
    }
}
}



