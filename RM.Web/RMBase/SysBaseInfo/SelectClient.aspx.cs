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

    public partial class SelectClient : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Base_ClientInfo> sumList =ClientInfoService.Instance.GetAllInfo(txtHelpCode.Text,-1,1);
            OurPager1.RecordCount = sumList[0].ID;

            LoadData(OurPager1.CurrentPageIndex);
        }
    }

    private void LoadData(int currentPageIndex)
    {
        
        List<Base_ClientInfo> infoList = new List<Base_ClientInfo>();
        infoList = ClientInfoService.Instance.GetAllInfo(txtHelpCode.Text, currentPageIndex, OurPager1.PageSize);
        List<Base_DictionaryInfo> dictList = DictionaryInfoService.Instance.GetAllInfo();

        var query = from info in dictList
                    where info.TypeCode == "ClientType"
                    select new
                         {
                             Id = info.DictionaryInfo_ID,
                             info.TypeCode,
                             info.TypeName,
                             info.ValueName,
                             info.ValueCode 
                         };
        //var query = from info in infoList
        //            join dictClientType in dictList
        //                on info.ClientType equals dictClientType.ValueCode
        //            where dictClientType.TypeCode == "ClientType"
        //            join dictClientKind in dictList
        //                on info.ClientKind equals dictClientKind.ValueCode
        //            where dictClientKind.TypeCode == "ClientKind"
        //            join dictClientArea in dictList
        //                on info.ClientArea equals dictClientArea.ValueCode
        //            where dictClientArea.TypeCode == "ClientArea"
        //            join dictClientProperty in dictList
        //                on info.ClientProperty equals dictClientProperty.ValueCode
        //            where dictClientProperty.TypeCode == "ClientProperty"
                    //select new
                    //    {
                    //        Id = info.ID,
                    //        info.ClientCode,
                    //        info.HelpCode,
                    //        info.ShortName,
                    //        info.CompanyName,
                    //        info.CompanyTel,
                    //        info.Linkman,
                    //        info.LinkTel,
                    //        ClientType = dictClientType.ValueName,
                    //        ClientKind = dictClientKind.ValueName,
                    //        ClientArea = dictClientArea.ValueName,
                    //        ClientProperty = dictClientProperty.ValueName
                    //    };
        dgvInfo.DataKeyNames = new[] { "Id" };
        dgvInfo.DataSource = query.ToList(); //设置GridView数据源
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
            List<Base_DictionaryInfo> dictList = DictionaryInfoService.Instance.GetAllInfo();

            var query = from info in dictList
                        where info.TypeCode == "ClientType" && info.ValueName.Contains(txtHelpCode.Text)
                        select new
                        {
                            Id = info.DictionaryInfo_ID,
                            info.TypeCode,
                            info.TypeName,
                            info.ValueName,
                            info.ValueCode
                        };

            dgvInfo.DataKeyNames = new[] { "Id" };
            dgvInfo.DataSource = query.ToList(); //设置GridView数据源
            dgvInfo.DataBind();
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




