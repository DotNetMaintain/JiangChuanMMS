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

namespace RM.Web.MMS.MMS_FixedAassets
{
    public partial class FixedAassetsList : PageBase
	{
		
        protected Repeater rp_Item;
        protected PageControl PageControl1;

        FixedAassetsInfo_Dal FixedAassetsInfo = new FixedAassetsInfo_Dal();

        private string _FA_Code;
        protected void Page_Load(object sender, EventArgs e)
        {

            this._FA_Code = base.Request["FA_Code"];

            this.PageControl1.pageHandler += new EventHandler(this.pager_PageChanged);
            
            if (!base.IsPostBack)
            {
                //this.PageControl1.pageHandler += new EventHandler(this.pager_PageChanged);
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
            if (!string.IsNullOrEmpty(this._FA_Code))
            {
                SqlWhere.Append(" AND FA_Code IN(" + this._FA_Code + ")");
            }
             FixedAassetsInfo_Dal a=new FixedAassetsInfo_Dal();
            DataTable dt =  FixedAassetsInfo.GetFixedAassetsInfoPage(SqlWhere, IList_param, this.PageControl1.PageIndex, this.PageControl1.PageSize, ref count);
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
            this.PageControl1.PageChecking();

        }



	}
}
