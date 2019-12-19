using RM.Busines;
using RM.Busines.DAL;
using RM.Busines.DAO;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetData;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;

namespace RM.Web.MMS.StorageManagement
{
    public partial class StorageForm : PageBase
    {
        private string _key;
        private RM_System_IDAO system_idao = new RM_System_Dal();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._key = base.Request["key"];
            if (!base.IsPostBack)
            {
              
                if (!string.IsNullOrEmpty(this._key))
                {
                    this.InitData();
                }
            }
        }

        private void InitData()
        {

           // StorageForm_ID.Value = Request.QueryString["StorageForm_ID"].ToString();
            Hashtable ht = DataFactory.SqlDataBase().GetHashtableById("MMS_StoreTransation", "Store_id", this._key);
            if (ht.Count > 0 && ht != null)
            {
                ControlBindHelper.SetWebControls(this.Page, ht);
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            
           	string guid = CommonHelper.GetGuid;
			Hashtable ht = new Hashtable();
			ht = ControlBindHelper.GetWebControls(this.Page);

			

            if (!string.IsNullOrEmpty(this._key))
			{
				guid = this._key;
				ht["ModifyDate"] = DateTime.Now;
				ht["ModifyUserId"] = RequestSession.GetSessionUser().UserId;
				
			}
			else
			{

                ht["Store_id"] = guid;
				ht["CreateUserId"] = RequestSession.GetSessionUser().UserId;
               
			}


            ht.Remove("Material_Type");
            ht.Remove("Material_Code");
            ht.Remove("Material_Name");
            ht.Remove("Material_CommonlyName");
            ht.Remove("Material_Specification");
            ht.Remove("Material_Unit");
            ht.Remove("Material_Supplier");
            ht.Remove("MaterialPic_Name");
            ht.Remove("Material_Pic");
            ht.Remove("User_ID_Hidden");
            ht["Purchase_Code"] = Request.QueryString["StorageForm_ID"];


            bool IsOk = DataFactory.SqlDataBase().Submit_AddOrEdit("dbo.MMS_StoreTransation", "Store_id", this._key, ht);
			if (IsOk)
			{
				string str = this.User_ID_Hidden.Value;
               
				if (!string.IsNullOrEmpty(str))
				{
					str = this.User_ID_Hidden.Value.Substring(0, this.User_ID_Hidden.Value.Length - 1);
				}
				bool IsAllto = this.system_idao.Add_RoleAllotMember(str.Split(new char[]
				{
					','
				}), guid);
				if (IsAllto)
				{
					ShowMsgHelper.AlertMsg("操作成功！");
                    Response.Write("<script>window.opener.document.getElementById('txtInvoiceID').value=876; </script>");
                   
				}
				else
				{
					ShowMsgHelper.Alert_Error("操作失败！");
				}
			}
			else
			{
				ShowMsgHelper.Alert_Error("操作失败！");
			}
		}

        

    }
}