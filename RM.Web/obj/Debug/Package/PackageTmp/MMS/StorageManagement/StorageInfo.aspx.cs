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
using RM.Common.DotNetBean;


namespace RM.Web.MMS.StorageManagement
{
    public partial class StorageInfo : PageBase
    {
        private string _key;
        private RM_System_IDAO system_idao = new RM_System_Dal();
        private MMS_StoreTransation_Dal storetransation_idao = new MMS_StoreTransation_Dal();
        private string _Invoice_ID;
        protected PageControl PageControl1;
        protected Repeater rp_Item;
        public int i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            
            
            this._Invoice_ID = base.Request["StorageForm_ID"];
            this.PageControl1.pageHandler += new EventHandler(this.pager_PageChanged);
            if (!base.IsPostBack)
            {
 
                    Init_StorageInfo();
                    
            }

        }



        private void Init_StorageInfo()
        {
            ddlStates.Items.Add("未发料");
            ddlStates.Items.Add("已发料");
            ddlStates.Items.Add("退回");

            BindDept(ddlDeptName, " and CheckQuantity=0  and quantity>checkquantity and (AuditFlag is null or AuditFlag>'0')");
            //ddlDeptName.Items.Add(new ListItem("", "-1"));
            ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
            
            ddlDeptName.Items.Add(new ListItem("",""));
        }

        protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlStates.SelectedValue)
            {
                case "未发料":
                    BindDept(ddlDeptName, " and quantity>checkquantity and auditflag is null");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
                case "已发料":
                    BindDept(ddlDeptName, "");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
                case "退回":
                    BindDept(ddlDeptName, " and auditflag=0");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
                default:
                    BindDept(ddlDeptName, " and quantity>checkquantity and auditflag is null");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
            };


        }



        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.DataBindGrid();
        }
        private void DataBindGrid()
        {
            int count = 0;
            StringBuilder SqlWhere = new StringBuilder();
            SqlWhere.Append(" and CheckQuantity=0  and quantity>checkquantity and (AuditFlag is null or AuditFlag>'0')");
            IList<SqlParam> IList_param = new List<SqlParam>();
                DataTable dt = this.storetransation_idao.GetStoreDeliveryPage(SqlWhere, IList_param, this.PageControl1.PageIndex, this.PageControl1.PageSize, ref count);
                ControlBindHelper.BindRepeaterList(dt, this.rp_Item);
                this.PageControl1.RecordCount = Convert.ToInt32(count);
           
            
        }



        protected void rp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int count = 0;
           
            string sqlcondition = string.Empty;
            string statues = string.Empty;


            //发货的状态
            switch (ddlStates.Text.ToString().Trim())
            {

                case "已发料":
                    statues = @" and CheckQuantity>0 and (AuditFlag is null or AuditFlag>'0')";
                    break;
                case "未发料":
                    statues = @" and CheckQuantity=0  and quantity>0 and (AuditFlag is null or AuditFlag>'0')";
                    break;
                case "退回":
                    statues = @" and AuditFlag='0'";
                    break;

            };





            StringBuilder SqlWhere = new StringBuilder();
            IList<SqlParam> IList_param = new List<SqlParam>();
            if (ddlDeptName.SelectedValue.ToString().Trim() != "" && ddlDeptName.SelectedValue.ToString().Trim() !="-1")
            {
                string sql = @" and DeptName='{0}'";
                sql = string.Format(sql, ddlDeptName.SelectedValue.ToString().Trim());
                sqlcondition = sqlcondition + sql;
            }

            if (txtInvoiceID.Text.ToString().Trim() != "")
            {
                string sql_voice = @" and PurchaseBillCode like '%{0}%'";
                sql_voice = string.Format(sql_voice, txtInvoiceID.Text.ToString().Trim());
                sqlcondition = sqlcondition + sql_voice;
            }

            if (ttbStartDate.Text.ToString().Trim() != "")
            {
                string sql_StartDate = @" and PurchaseDate>= '{0}'";
                sql_StartDate = string.Format(sql_StartDate, ttbStartDate.Text.ToString().Trim());
                sqlcondition = sqlcondition + sql_StartDate;
            }

            if (ttbEndDate.Text.ToString().Trim() != "")
            {
                string sql_EndDate = @" and PurchaseDate<= '{0}'";
                sql_EndDate = string.Format(sql_EndDate, ttbEndDate.Text.ToString().Trim());
                sqlcondition = sqlcondition + sql_EndDate;
            }


            sqlcondition = sqlcondition + statues;
            SqlWhere.Append(sqlcondition);



            DataTable dt = this.storetransation_idao.GetStoreDeliveryPage(SqlWhere);
            //DataTable dt = this.storetransation_idao.GetStoreDeliveryPage(SqlWhere, IList_param, this.PageControl1.PageIndex, this.PageControl1.PageSize, ref count);
            ControlBindHelper.BindRepeaterList(dt, this.rp_Item);
            this.PageControl1.RecordCount = Convert.ToInt32(count);
        }



        private void BindDept(DropDownList ddl, string where)
        {
            DataTable afterdt = new DataTable();
            StringBuilder SqlWhere = new StringBuilder();
            SqlWhere.Append(where);
            DataTable dt = this.storetransation_idao.GetStoreDeliveryPage(SqlWhere);
            DataView dv = new DataView(dt);

            afterdt = dv.ToTable(true, "DeptName");
            ddl.DataSource = afterdt; //设置下拉框的数据源
            ddl.DataTextField = "DeptName";
            ddl.DataValueField = "DeptName";
            ddl.DataBind(); //下拉框数据绑定
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}