using RM.Busines.DAL;
using RM.Busines.DAO;
using RM.Common.DotNetData;
using RM.Web.App_Code;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

namespace RM.Web.RMBase.SysBaseInfo
{
    public partial class SelectMaterialOrg_Left : PageBase
    {
        public StringBuilder strHtml = new StringBuilder();
        private MMS_MaterialInfo_Dal mms_material = new MMS_MaterialInfo_Dal();
        private MMS_MaterialTypeInfo_Dal mms_materialtype = new MMS_MaterialTypeInfo_Dal();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.InitInfo();
			}
		}
		public void InitInfo()
		{
         
			DataTable dtMaterialType = this.mms_materialtype.GetMaterialTypeList();
            if (DataTableHelper.IsExistRows(dtMaterialType))
			{
                foreach (DataRowView drv in new DataView(dtMaterialType)
				{
					RowFilter = "ParentId = '0'"
				})
				{
					this.strHtml.Append("<li>");
                    this.strHtml.Append("<div>" + drv["MaterialType_Name"].ToString());
                    this.strHtml.Append("<span style='display:none'>" + drv["MaterialType_ID"].ToString() + "</span></div>");
                    this.strHtml.Append(this.GetTreeNode(drv["MaterialType_ID"].ToString(), dtMaterialType));
					this.strHtml.Append("</li>");
				}
			}
			else
			{
				this.strHtml.Append("<li>");
				this.strHtml.Append("<div><span style='color:red;'>暂无数据</span></div>");
				this.strHtml.Append("</li>");
			}
		}
		public string GetTreeNode(string parentID, DataTable dtNode)
		{
			StringBuilder sb_TreeNode = new StringBuilder();
			DataView dv = new DataView(dtNode);
			dv.RowFilter = "ParentId = '" + parentID + "'";
			if (dv.Count > 0)
			{
				sb_TreeNode.Append("<ul>");
				foreach (DataRowView drv in dv)
				{
					sb_TreeNode.Append("<li>");
                    sb_TreeNode.Append("<div>" + drv["MaterialType_Name"]);
                    sb_TreeNode.Append("<span style='display:none'>" + drv["MaterialType_ID"].ToString() + "</span></div>");
                    sb_TreeNode.Append(this.GetTreeNode(drv["MaterialType_ID"].ToString(), dtNode));
					sb_TreeNode.Append("</li>");
				}
				sb_TreeNode.Append("</ul>");
			}
			return sb_TreeNode.ToString();
		}
	
    }
}