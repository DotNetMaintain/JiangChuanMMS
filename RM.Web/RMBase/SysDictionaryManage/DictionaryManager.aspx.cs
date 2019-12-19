using RM.Busines;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using System;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RM.Busines.DAL;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;

namespace RM.Web.RMBase.SysDictionaryManage
{

    public partial class DictionaryManager : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Wizard1.ActiveStepIndex = 0;

                List<Base_DictionaryInfo> dictList = DictionaryInfoService.Instance.GetAllInfo();
                var query = from dict in dictList
                            group dict by dict.TypeCode
                                into g
                                select new
                                    {
                                        TypeCode = g.Key,
                                        TypeName = g.Max(itm => itm.TypeName)
                                    };
                lbDictType.DataValueField = "TypeCode";
                lbDictType.DataTextField = "TypeName";
                lbDictType.DataSource = query.ToList();
                lbDictType.DataBind();
                if (query.Count() > 0)
                {
                    lbDictType.SelectedIndex = 0;
                }
            }
        }

        private void LoadData(string typeCode)
        {
            List<Base_DictionaryInfo> infoList =
                DictionaryInfoService.Instance.GetAllInfo().Where(itm => itm.TypeCode == typeCode).ToList();
            dgvInfo.DataKeyNames = new[] { "DictionaryInfo_ID" }; //设置GridView数据主键
            dgvInfo.DataSource = infoList;
            dgvInfo.DataBind();
        }

        protected void dgvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName)) //判断命令名是否为空
            {
                if (e.CommandName == "Edi") //如果触发的是详细信息按钮事件
                {
                    LoadData(hidDictType.Value);
                }
                else if (e.CommandName == "Del")
                {
                    int index = Convert.ToInt32(e.CommandArgument); //取GridView行索引
                    GridView grid = (GridView)e.CommandSource; //取当前操作的GridView
                    int id = Convert.ToInt32(grid.DataKeys[index].Value); //取GridView主键值
                    DictionaryInfoService.Instance.DeleteInfo(id);
                    LoadData(hidDictType.Value);
                }
                else if (e.CommandName == "Page")
                {
                    LoadData(hidDictType.Value);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(hidDictType.Value);
        }

        protected void dgvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            (sender as GridView).PageIndex = e.NewPageIndex; //指定GridView新页索引
            (sender as GridView).DataBind(); //GridView数据源绑定
        }

        protected void dgvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //如果是数据行
            {
                GridView grid = sender as GridView; //取当前操作的GridView
                ((LinkButton)(e.Row.Cells[grid.Columns.Count - 1].Controls[0])).Attributes.Add("onclick",
                                                                                                "return confirm('确认删除?');");
                //为GridView数据行的删除按钮添加删除确认对话框

                string code = e.Row.Cells[1].Text;
                string id = grid.DataKeys[e.Row.RowIndex].Value.ToString();
                ((LinkButton)(e.Row.Cells[grid.Columns.Count - 2].Controls[0])).Attributes.Add("onclick",
                                                                                                "btnSelect_onclick('" + id +
                                                                                                "')");
            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (string.IsNullOrEmpty(lbDictType.SelectedValue))
            {
                Response.Write("<Script>window.alert('请选择字典类别!')</Script>");
                e.Cancel = true;
                return;
            }
            hidDictType.Value = lbDictType.SelectedValue;
            LoadData(hidDictType.Value);
        }

        protected void Wizard1_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (string.IsNullOrEmpty(lbDictType.SelectedValue))
            {
                Response.Write("<Script>window.alert('请选择字典类别!')</Script>");
                e.Cancel = true;
                return;
            }
            hidDictType.Value = lbDictType.SelectedValue;
            LoadData(hidDictType.Value);
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
           // Response.Redirect(@"~/HomePage.aspx");
        }

    }
}



