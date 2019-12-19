using RM.Web.App_Code;
using System;
using System.Linq;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Dao;

namespace RM.Web.RMBase.SysDictionaryManage
{
    public partial class DictionaryInput : PageBase
    {

        private string code
        {
            get
            {
                if (ViewState["code"] == null || ViewState["code"].ToString() == "")
                {
                    return "";
                }
                else
                {
                    return ViewState["code"].ToString();
                }
            }
            set { ViewState["code"] = value; }
        }

        private string DictionaryInfo_ID
        {
            get
            {
                if (ViewState["DictionaryInfo_ID"] == null || ViewState["DictionaryInfo_ID"].ToString() == "")
                {
                    return "";
                }
                else
                {
                    return ViewState["DictionaryInfo_ID"].ToString();
                }
            }
            set { ViewState["DictionaryInfo_ID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                code = Request.QueryString["code"].ToString();
                if (Request.QueryString["DictionaryInfo_ID"] == null)
                {
                    ClearTextBox(); //清除服务器控件的内容
                }
                else
                {
                    DictionaryInfo_ID = Request.QueryString["DictionaryInfo_ID"];
                    Base_DictionaryInfo info = DictionaryInfoService.Instance.GetInfo(Convert.ToInt32(DictionaryInfo_ID));
                    EditTextBox(info);
                    txtValueCode.ReadOnly = true;
                }
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
                EditModel(info);
                info.Operator = Context.User.Identity.Name;
                info.OperateDate = DateTime.Now;
                if (string.IsNullOrEmpty(DictionaryInfo_ID)) //插入
                {
                    int cnt = DictionaryInfoService.Instance.InsertInfo(info);
                    if (cnt > 0)
                    {
                        ClearTextBox(); //清除服务器控件的内容
                        Response.Write("<Script>window.alert('保存成功!')</Script>");
                    }
                }
                else //修改
                {
                    info.DictionaryInfo_ID = Convert.ToInt32(DictionaryInfo_ID);
                    bool succ = DictionaryInfoService.Instance.UpdateInfo(info);
                    if (succ)
                    {
                        Response.Write("<Script>window.alert('保存成功!')</Script>");
                    }
                }
            }
        }

        private void EditModel(Base_DictionaryInfo info)
        {
            Base_DictionaryInfo temp;
            if (string.IsNullOrEmpty(DictionaryInfo_ID))
            {
                temp = DictionaryInfoService.Instance.GetAllInfo().FirstOrDefault(itm => itm.TypeCode == code);
            }
            else
            {
                temp = DictionaryInfoService.Instance.GetInfo(Convert.ToInt32(DictionaryInfo_ID));
            }
            info.TypeCode = temp.TypeCode;
            info.TypeName = temp.TypeName;
            info.ValueCode = txtValueCode.Text;
            info.ValueName = txtValueName.Text;
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




