using RM.Busines;
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
namespace RM.Web.MMS.MMS_MaterialType
{
	public  class MaterialType_Form : PageBase
	{
        private string _key;
        private string _ParentId;
        protected HtmlForm form1;
        protected HtmlInputText MaterialType_Code;
        protected HtmlInputText MaterialType_Name;
        protected HtmlSelect ParentId;
        protected LinkButton Save;
		protected void Page_Load(object sender, EventArgs e)
		{
			this._key = base.Request["key"];
			this._ParentId = base.Request["ParentId"];
			if (!base.IsPostBack)
			{
				this.InitParentId();
				if (!string.IsNullOrEmpty(this._ParentId))
				{
					this.ParentId.Value = this._ParentId;
				}
				if (!string.IsNullOrEmpty(this._key))
				{
					this.InitData();
				}
			}
		}
		private void InitData()
		{
            Hashtable ht = DataFactory.SqlDataBase().GetHashtableById("MMS_MaterialType", "MaterialType_ID", this._key);
			if (ht.Count > 0 && ht != null)
			{
				ControlBindHelper.SetWebControls(this.Page, ht);
			}
		}
		private void InitParentId()
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MaterialType_ID,\r\n                            MaterialType_Name+' - '+CASE ParentId WHEN '0' THEN '父节' ELSE  '子节' END AS MaterialType_Name\r\n                            FROM MMS_MaterialType WHERE DeleteMark = 1 ORDER BY SortCode ASC");
			DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
			if (!string.IsNullOrEmpty(this._key))
			{
				if (DataTableHelper.IsExistRows(dt))
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
                        if (dt.Rows[i]["MaterialType_ID"].ToString() == this._key)
						{
							dt.Rows.RemoveAt(i);
						}
					}
				}
			}
            ControlBindHelper.BindHtmlSelect(dt, this.ParentId, "MaterialType_Name", "MaterialType_ID", "物资类型 - 父节");
		}
		protected void Save_Click(object sender, EventArgs e)
		{
			Hashtable ht = new Hashtable();
			ht = ControlBindHelper.GetWebControls(this.Page);
			if (this.ParentId.Value == "")
			{
				ht["ParentId"] = "0";
			}
			if (!string.IsNullOrEmpty(this._key))
			{
				ht["ModifyDate"] = DateTime.Now;
				ht["ModifyUserId"] = RequestSession.GetSessionUser().UserId;
				ht["ModifyUserName"] = RequestSession.GetSessionUser().UserName;
			}
			else
			{
                ht["MaterialType_ID"] = CommonHelper.GetGuid;
                ht["ModifyUserId"] = RequestSession.GetSessionUser().UserId;
                ht["ModifyUserName"] = RequestSession.GetSessionUser().UserName;
			}
            bool IsOk = DataFactory.SqlDataBase().Submit_AddOrEdit("MMS_MaterialType", "MaterialType_ID", this._key, ht);
			if (IsOk)
			{
				ShowMsgHelper.AlertMsg("操作成功！");
			}
			else
			{
				ShowMsgHelper.Alert_Error("操作失败！");
			}
		}
	}
}
