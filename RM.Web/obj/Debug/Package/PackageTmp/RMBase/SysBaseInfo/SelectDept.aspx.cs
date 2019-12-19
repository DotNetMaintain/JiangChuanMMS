using RM.Busines;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using System;
using System.Collections;
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
using GridView = System.Web.UI.WebControls.GridView;

namespace RM.Web.RMBase.SysBaseInfo
{

    public partial class SelectDept : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            List<Base_Organization> sumList = DeptInfoService.Instance.GetAllInfo();
            OurPager1.RecordCount = sumList.Count;

            LoadData(OurPager1.CurrentPageIndex);
        }
    }

    private void LoadData(int currentPageIndex)
    {
        List<Base_Organization> infoList = new List<Base_Organization>();
        infoList = DeptInfoService.Instance.GetAllInfo(txtHelpCode.Text, currentPageIndex, OurPager1.PageSize);
        List<Base_DictionaryInfo> dictList = DictionaryInfoService.Instance.GetAllInfo();

        var query = from info in infoList
                    select new
                        {
                            Organization_ID = info.Organization_ID,
                            info.Organization_Code,
                            info.Organization_Name 
                        };
        dgvInfo.DataKeyNames = new[] { "Organization_ID" };
        dgvInfo.DataSource = query.ToList().OrderBy(i=>i.Organization_Code ); //设置GridView数据源
        dgvInfo.DataBind();
    }

    protected void dgvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void dgvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //如果是数据行
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#6699ff' ");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            GridView grid = sender as GridView; //取当前操作的GridView
            string code = grid.DataKeys[e.Row.RowIndex].Value.ToString();
            string name = e.Row.Cells[1].Text;
            ((LinkButton) (e.Row.Cells[grid.Columns.Count - 1].Controls[0])).Attributes.Add("onclick",
                                                                                            "btnSelect_onclick('" + code +
                                                                                            "','" + name + "')");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<Base_Organization> sumList = DeptInfoService.Instance.GetAllInfo(txtHelpCode.Text, -1, 1);
        OurPager1.RecordCount = sumList.Count;
        LoadData(OurPager1.CurrentPageIndex);
    }

    protected void dgvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }


    protected void OurPager1_PageChanged(object sender, PageArgs e)
    {
        LoadData(e.NewPageIndex);
    }
   
}
    
}




