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
using RM.ServiceProvider;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;


namespace RM.Web.RMBase.SysDictionaryManage
{
    public partial class DictionaryTypeInput : PageBase
    {

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Base_DictionaryInfo info = new Base_DictionaryInfo();
                
                info.Operator = Context.User.Identity.Name;
                info.OperateDate = DateTime.Now;
                
               int cnt = InsertModel(info);
                    if (cnt > 0)
                    {
                        ClearTextBox(); //清除服务器控件的内容
                        Response.Write("<Script>window.alert('保存成功!')</Script>");
                    }
               
                
            }
        }

        private int InsertModel(Base_DictionaryInfo info)
        {
            info.TypeCode = txtValueCode.Text.ToString().Trim();
            info.TypeName = txtValueName.Text.ToString().Trim();
            info.ValueCode = "default";
            info.ValueName = "default";

            int result = DictionaryInfoService.Instance.InsertInfo(info);
            return result;
        }

        private void ClearTextBox()
        {
            txtValueCode.Text = "";
            txtValueName.Text = "";
        }

        private void EditTextBox(Base_DictionaryInfo info)
        {
            txtValueCode.Text = info.ValueCode;
            txtValueName.Text = info.ValueName;
        }
    }
}




