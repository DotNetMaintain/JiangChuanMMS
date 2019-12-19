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


namespace RM.Web.MMS.MMS_Purchase
{
    public partial class Purchase_Info : PageBase
    {
        private string _key;
        private RM_System_IDAO system_idao = new RM_System_Dal();
        private MMS_StoreTransation_Dal storetransation_idao = new MMS_StoreTransation_Dal();
        private string _Invoice_ID;
        protected PageControl PageControl1;
        protected Repeater rp_Item;
        RM_UserInfo_IDAO user_idao = new RM_UserInfo_Dal();

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
            ddlStates.Items.Add("未打印");
            ddlStates.Items.Add("已打印");
            BindDept(ddlDeptName);
            
            ddlDeptName.Items.Add(new ListItem("",""));
        }



        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.DataBindGrid();
        }
        private void DataBindGrid()
        {
            int count = 0;
            StringBuilder SqlWhere = new StringBuilder();
            SqlWhere.Append("  and (AuditFlag is null or AuditFlag='1')");
            IList<SqlParam> IList_param = new List<SqlParam>();
                DataTable dt = this.storetransation_idao.GetStorePurchasePage(SqlWhere);
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

                case "已打印":
                    statues = @" and (AuditFlag='2')";
                    break;
                case "未打印":
                    statues = @" and (AuditFlag='1' or AuditFlag is null)";
                    break;
                

            };





            StringBuilder SqlWhere = new StringBuilder();
            IList<SqlParam> IList_param = new List<SqlParam>();
            if (ddlDeptName.SelectedValue.ToString().Trim() != "")
            {
                string sql = @" and DeptName like '%{0}%'";
                sql = string.Format(sql, ddlDeptName.SelectedValue.ToString().Trim());
                sqlcondition = sqlcondition + sql;
            }

            if (txtInvoiceID.Text.ToString().Trim() != "")
            {
                string sql_voice = @" and PurchaseBillCode like '%{0}%'";
                sql_voice = string.Format(sql_voice, txtInvoiceID.Text.ToString().Trim());
                sqlcondition = sqlcondition + sql_voice;
            }


            sqlcondition = sqlcondition + statues;
            SqlWhere.Append(sqlcondition);



            DataTable dt = this.storetransation_idao.GetStorePurchasePage(SqlWhere);
            //DataTable dt = this.storetransation_idao.GetStoreDeliveryPage(SqlWhere, IList_param, this.PageControl1.PageIndex, this.PageControl1.PageSize, ref count);
            ControlBindHelper.BindRepeaterList(dt, this.rp_Item);
            this.PageControl1.RecordCount = Convert.ToInt32(count);

            hidApplyName.Value = user_idao.GetUserInfo(dt.Rows[0]["Operator"].ToString()).Rows[0]["User_Name"].ToString();
            string audit=dt.Rows[0]["Provider"].ToString();
            string[] arr = audit.Split(',');
            string auditusername = string.Empty;
            if (arr.Length > 0)
            {

                int i = 0;
                foreach (string name in arr)
                {
                    i = i + 1;
                    if (i == arr.Length)
                    {
                        auditusername = auditusername + user_idao.GetUserInfoName(name);
                    }
                    else
                    {
                        auditusername = auditusername + user_idao.GetUserInfoName(name) + ",";
                    }

                }

            }
            else
            {
                auditusername=user_idao.GetUserInfoName(dt.Rows[0]["Provider"].ToString());
            }
            hidAuditName.Value = auditusername;

        }



        private void BindDept(DropDownList ddl)
        {
            DataTable afterdt=new DataTable ();
            StringBuilder SqlWhere = new StringBuilder();
            DataTable dt = this.storetransation_idao.GetStorePurchasePage(SqlWhere);
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