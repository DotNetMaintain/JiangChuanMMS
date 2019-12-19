using RM.Busines.DAL;
using RM.Busines.DAO;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using RM.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RM.Web.UserControl;

namespace RM.Web.RMBase.SysBaseInfo
{
    public partial class SelectMaterialOrg_List : PageBase
    {
          private MMS_MaterialInfo_Dal material_idao = new MMS_MaterialInfo_Dal();
        private string _MaterialType_ID;
		protected void Page_Load(object sender, EventArgs e)
		{
            this._MaterialType_ID = base.Request["MaterialType_ID"];
			this.PageControl1.pageHandler += new EventHandler(this.pager_PageChanged);
			if (!base.IsPostBack)
			{

			}
		}
		protected void pager_PageChanged(object sender, EventArgs e)
		{
			this.DataBindGrid();
		}
		private void DataBindGrid()
		{
			int count = 0;
			StringBuilder SqlWhere = new StringBuilder();
			IList<SqlParam> IList_param = new List<SqlParam>();
			if (!string.IsNullOrEmpty(this.txt_Search.Value))
			{
				SqlWhere.Append(" and U." + this.Searchwhere.Value + " like @obj ");
				IList_param.Add(new SqlParam("@obj", '%' + this.txt_Search.Value.Trim() + '%'));
			}
            if (!string.IsNullOrEmpty(this._MaterialType_ID))
			{
                SqlWhere.Append(" AND typeid IN(" + this._MaterialType_ID + ")");
			}
            DataTable dt = this.material_idao.GetMaterialTypeInfoPage(SqlWhere, IList_param, this.PageControl1.PageIndex, this.PageControl1.PageSize, ref count);
			ControlBindHelper.BindRepeaterList(dt, this.rp_Item);
			this.PageControl1.RecordCount = Convert.ToInt32(count);
		}
		protected void rp_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				
			}
		}
		protected void lbtSearch_Click(object sender, EventArgs e)
		{
			this.DataBindGrid();
		}

    }
}