using RM.Busines.DAL;
using RM.Busines.DAO;
using RM.Web.App_Code;
using RM.Web;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using RM.Web.UserControl;

namespace RM.Web.MMS.MMS_MaterialType
{
	public partial  class MaterialType_List : PageBase
	{
        protected HtmlForm form1;
        protected LoadButton LoadButton1;
		public StringBuilder str_tableTree = new StringBuilder();
        private MMS_MaterialTypeInfo_IDAO materialtype_idao = new MMS_MaterialTypeInfo_Dal();
		private RM_UserInfo_IDAO user_idao = new RM_UserInfo_Dal();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GetTreeTable();
			}
		}
		public void GetTreeTable()
		{
            DataTable dtMtype = this.materialtype_idao.GetMaterialTypeList();
            DataView dv = new DataView(dtMtype);
			dv.RowFilter = " ParentId = '0'";
            dv.Sort = "MaterialType_Code";
			int eRowIndex = 0;
			foreach (DataRowView drv in dv)
			{
				string trID = "node-" + eRowIndex.ToString();
				this.str_tableTree.Append("<tr id='" + trID + "'>");
                this.str_tableTree.Append("<td style='width: 50px;padding-left:0px;'><span class=\"folder\">" + drv["MaterialType_Name"].ToString() + "</span></td>");
                this.str_tableTree.Append("<td style='width: 50px;text-align: center;'>" + drv["MaterialType_Code"].ToString() + "</td>");
                this.str_tableTree.Append("<td style='display:none'>" + drv["MaterialType_ID"].ToString() + "</td>");
				this.str_tableTree.Append("</tr>");
                this.str_tableTree.Append(this.GetTableTreeNode(drv["MaterialType_ID"].ToString(), dtMtype, trID));
				eRowIndex++;
			}
		}
		public string GetTableTreeNode(string parentID, DataTable dtMenu, string parentTRID)
		{
			StringBuilder sb_TreeNode = new StringBuilder();
			DataView dv = new DataView(dtMenu);
			dv.RowFilter = "ParentId = '" + parentID + "'";
            dv.Sort = "MaterialType_Code";
			int i = 1;
			foreach (DataRowView drv in dv)
			{
				string trID = parentTRID + "-" + i.ToString();
				sb_TreeNode.Append(string.Concat(new string[]
				{
					"<tr id='",
					trID,
					"' class='child-of-",
					parentTRID,
					"'>"
				}));
                sb_TreeNode.Append("<td style='padding-left:0px;'><span class=\"folder\">" + drv["MaterialType_Name"].ToString() + "</span></td>");
                sb_TreeNode.Append("<td style='width: 50px;text-align: center;'>" + drv["MaterialType_Code"].ToString() + "</td>");
                sb_TreeNode.Append("<td style='display:none'>" + drv["MaterialType_ID"].ToString() + "</td>");
				sb_TreeNode.Append("</tr>");
                sb_TreeNode.Append(this.GetTableTreeNode(drv["MaterialType_ID"].ToString(), dtMenu, trID));
				i++;
			}
			return sb_TreeNode.ToString();
		}
	}
}
