using System;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using RM.Busines;
using System.Text;
using System.Web.UI.WebControls;
using RM.Busines.DAL;

namespace RM.Web.MMS.StorageManagement
{
    public partial class DeliveryDeptMatypeTotal : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        private MMS_StoreTransation_Dal storetransation_idao = new MMS_StoreTransation_Dal();
        protected void Page_Load(object sender, EventArgs e)
        {

           dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), ttbMateriay_Name.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_DeliveryDeptMatypeTotal.rpt"));
            reportDoc.SetDataSource(dt);
            string TEXT_OBJECT_NAME = @"FinanceDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            if (!string.IsNullOrEmpty(ttbEndDate.Text.ToString().Trim()))
            {
                text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month + "月";
            }
            


            CrystalReportViewer1.ReportSource = reportDoc;


            if (!base.IsPostBack)
            {
                init_Page();
            }
        }
        private void init_Page()
        {
            ddlMaterialType.Items.Insert(0, new ListItem("", "-1"));
            ddlMaterialType.Items.Insert(1, new ListItem("卫生材料", "0"));
            ddlMaterialType.Items.Insert(1, new ListItem("低值易耗", "1"));
            ddlMaterialType.Items.Insert(1, new ListItem("其他材料", "2"));
            BindDept(ddlDeptName, " ");


        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataTable ds = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), ttbMateriay_Name.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_DeliveryDeptMatypeTotal.rpt"));
            reportDoc.SetDataSource(ds);
            string TEXT_OBJECT_NAME = @"FinanceDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            if (!string.IsNullOrEmpty(ttbEndDate.Text.ToString().Trim()))
            {
                text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month + "月";
            }


            CrystalReportViewer1.ReportSource = reportDoc;
        }





        protected DataTable dt_Query(string beginyear, string beginmonth, string materialname)
        {
            string sqlcondition = string.Empty;
            switch (ddlMaterialType.SelectedValue)
            {
                case "-1":
                    sqlcondition = sqlcondition + "";
                    break;
                case "0":
                    sqlcondition = sqlcondition + "AND Material_Attr01='0' ";
                    break;
                case "1":
                    sqlcondition = sqlcondition + "AND Material_Attr01='1' ";
                    break;
                case "2":
                    sqlcondition = sqlcondition + "AND Material_Attr01='2' ";
                    break;
                default:
                    break;
            };
            string strSql =
               @"select  plancontent.deptName,substring(mat.material_type,0,charindex('-',mat.material_type)) material_type,detail.Quantity,detail.Price,sum(detail.Quantity*detail.Price) as amount,mat.Material_Name,mat.Material_Attr01 from MMS_PurchasePlanContent plancontent inner join 
(select * from MMS_PurchasePlanDetail where  AuditFlag IS NULL) detail on plancontent.PurchaseBillCode=detail.PurchaseBillCode
inner join MMS_Delivery_Detail  devlivery
on detail.PurchaseBillCode=devlivery.PurchaseBillCode and detail.ProductCode=devlivery.ProductCode and detail.price=devlivery.price
inner join MMS_MaterialInfo mat on detail.productcode=mat.material_id 
where 1=1";
            strSql += sqlcondition;
            if (!string.IsNullOrEmpty(beginyear)&& !string.IsNullOrEmpty(beginmonth))
            {
                strSql += " and devlivery.OperatorDate>='"+ beginyear + "' AND devlivery.OperatorDate<'"+ beginmonth + "'";
            }else if (!string.IsNullOrEmpty(beginyear))
            {
                strSql += " and devlivery.OperatorDate>='" + beginyear + "'";
            }
            else if (!string.IsNullOrEmpty(beginmonth))
            {
                strSql += " and devlivery.OperatorDate<'" + beginmonth + "'";
            }
            if (ddlDeptName.SelectedValue.ToString().Trim() != "" && ddlDeptName.SelectedValue.ToString().Trim() != "-1")
            {
                strSql += " and DeptName ='" + ddlDeptName.SelectedValue.ToString().Trim() + "'";
            }
            if (!string.IsNullOrEmpty(materialname))
            {
                strSql += " and Material_Name like '%"+ materialname + "%'";
                strSql += "group by DeptName,material_type,detail.Quantity,detail.Price,mat.Material_Name,mat.Material_Attr01";
            }
            else
            {
                strSql += "group by DeptName,material_type,detail.Quantity,detail.Price,mat.Material_Name,mat.Material_Attr01";
            }
            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(strSql));

            return dt;
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

        protected void ddlMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlMaterialType.SelectedValue)
            {
                case "0":
                    BindDept(ddlDeptName, "AND Material_Attr01='0' ");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
                case "1":
                    BindDept(ddlDeptName, "AND Material_Attr01='1' ");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
                case "2":
                    BindDept(ddlDeptName, "AND Material_Attr01='2' ");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
                default:
                    BindDept(ddlDeptName, " ");
                    ddlDeptName.Items.Insert(0, new ListItem("", "-1"));
                    break;
            };
        }
    }
}