using System;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using RM.Busines;
using System.Text;
using System.Web.UI.WebControls;
using RM.Common.DotNetCode;

namespace RM.Web.MMS.report
{
    public partial class MatStore : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        ReportDocument reportDoc = new ReportDocument();
        
        protected void Page_Load(object sender, EventArgs e)
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
                default:
                    break;
            };

            if (ttbFullName.Text.ToString().Trim() != "")
            {
                sqlcondition = sqlcondition + "and material_name like '%" + ttbFullName.Text.ToString().Trim() + "%'";
            }

            switch (ddlSafeStore.SelectedValue)
            {
                case "-1":
                    sqlcondition = sqlcondition + "";
                    break;
                case "0":
                    sqlcondition = sqlcondition + " and qua<=material_safetystock ";
                    break;
                case "1":
                    sqlcondition = sqlcondition + " and qua>material_safetystock ";
                    break;
                default:
                    break;
            };

            dt = dt_Query(sqlcondition);
            reportDoc.Load(Server.MapPath("rpt_MatStoreQuery.rpt"));
            reportDoc.SetDataSource(dt);
            
            CrystalReportViewer1.ReportSource = reportDoc;

            if (!base.IsPostBack)
            {
              init_Page();
            }


        }


        protected DataTable dt_Query(string SqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (string.IsNullOrEmpty(searchDate.Text))
            {
                strSql.Append(@"select substring(mms.Material_Type,1,LEN(mms.Material_Type)-5) Material_Type,mms.material_name,mms.Material_CommonlyName,pur.qua,pur.price,pur.price*pur.qua as amount from dbo.MMS_MaterialInfo mms inner join (
                        select pur.productcode,(isnull(pur.qua,0)-isnull(purplan.qua,0)) as qua,pur.price from (
select productcode,sum(quantity) as  qua,price from MMS_PurchaseDetail group by productcode,price) pur
                        left join (select * from (select productcode,sum(quantity) as qua,price from (
select detail.* from MMS_PurchasePlanContent plancontent 
inner join MMS_PurchasePlanDetail  detail
on plancontent.PurchaseBillCode=detail.PurchaseBillCode
where paymode in ('1','2') and detail.AuditFlag is null) de
group by productcode,price) purplan)purplan
                        on pur.ProductCode=purplan.ProductCode and pur.price=purplan.price) pur
                        on mms.material_id=pur.productcode
                        where 1=1 ");
            }
            else
            {
                string code = Convert.ToDateTime(searchDate.Text).ToString("yyyyMMddhhmmssfff");
                strSql.Append(@"select substring(mms.Material_Type,1,LEN(mms.Material_Type)-5) Material_Type,mms.material_name,mms.Material_CommonlyName,pur.qua,pur.price,pur.price*pur.qua as amount from dbo.MMS_MaterialInfo mms inner join (
                        select pur.productcode,(isnull(pur.qua,0)-isnull(purplan.qua,0)) as qua,pur.price from (
select productcode,sum(quantity) as  qua,price from MMS_PurchaseDetail where PurchaseBillCode<='" + code+"'");
                strSql.Append(@" group by productcode,price) pur
                        left join (select * from (select productcode,sum(quantity) as qua,price from (
select detail.* from MMS_PurchasePlanContent plancontent 
inner join MMS_PurchasePlanDetail  detail
on plancontent.PurchaseBillCode=detail.PurchaseBillCode
where paymode in ('1','2') and detail.AuditFlag is null) de where de.PurchaseBillCode<='" + code + "'");
                strSql.Append(@" group by productcode,price) purplan)purplan
                        on pur.ProductCode=purplan.ProductCode and pur.price=purplan.price) pur
                        on mms.material_id=pur.productcode
                        where 1=1 ");
            }
            if (chkzero.Checked) {
                strSql.Append(@" and qua>0");
            }
            strSql.Append(SqlWhere);
            strSql.Append(@" order by Material_Type");
            dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            //            strSql.Append(@"select * from (select substring(mms.Material_Type,1,LEN(mms.Material_Type)-5) Material_Type,material_name,Material_CommonlyName,Material_Attr01,Material_SafetyStock,isnull(purnum.qua,0) qua,isnull(price,0) price,isnull(purnum.qua,0)*isnull(price,0) amount from dbo.MMS_MaterialInfo mms 
            //left join (
            //select * from (
            //select pur.productcode,pur.qua-isnull(deli.qua,0) as qua,pur.price from (
            //select productcode,sum(quantity) qua,price  from  MMS_PurchaseDetail group by productcode,price) pur
            //left join (
            //select productcode,sum(quantity) qua,price from dbo.MMS_Delivery_Detail group by productcode,price) deli
            //on pur.productcode=deli.productcode and pur.Price=deli.Price) purnum) purnum
            //on mms.Material_ID=purnum.productcode) store where 1=1");

            return dt;
        }


        private void init_Page()
        {
            ddlMaterialType.Items.Insert(0, new ListItem("", "-1"));
            ddlMaterialType.Items.Insert(1, new ListItem("卫生材料", "0"));
            ddlMaterialType.Items.Insert(1, new ListItem("低值易耗", "1"));
            ddlMaterialType.Items.Insert(1, new ListItem("其他材料", "2"));


            ddlSafeStore.Items.Insert(0, new ListItem("", "-1"));
            ddlSafeStore.Items.Insert(1, new ListItem("是", "0"));
            ddlSafeStore.Items.Insert(1, new ListItem("否", "1"));


        }

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            string sqlcondition = string.Empty;
            switch(ddlMaterialType.SelectedValue)
            {
                case "-1":
                    sqlcondition = sqlcondition+"";
                    break ;
                case "0":
                    sqlcondition =sqlcondition+ "AND Material_Attr01='0' ";
                    break ;
                case "1":
                    sqlcondition =sqlcondition+ "AND Material_Attr01='1' ";
                    break;
                default :
                    break ;
            };

            if (ttbFullName.Text.ToString().Trim() != "")
            {
                sqlcondition = sqlcondition + "and material_name like '%" + ttbFullName.Text.ToString().Trim() + "%'";
            }

            switch (ddlSafeStore.SelectedValue)
            {
                case "-1":
                    sqlcondition = sqlcondition + "";
                    break;
                case "0":
                    sqlcondition = sqlcondition + " and qua<=material_safetystock ";
                    break;
                case "1":
                    sqlcondition = sqlcondition + " and qua>material_safetystock ";
                    break;
                default:
                    break;
            };

            dt = dt_Query(sqlcondition);
            reportDoc.Load(Server.MapPath("rpt_MatStoreQuery.rpt"));
            reportDoc.SetDataSource(dt);

            CrystalReportViewer1.ReportSource = reportDoc;
        }

        protected void chkzero_CheckedChanged(object sender, EventArgs e)
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
                default:
                    break;
            };

            if (ttbFullName.Text.ToString().Trim() != "")
            {
                sqlcondition = sqlcondition + "and material_name like '%" + ttbFullName.Text.ToString().Trim() + "%'";
            }

            switch (ddlSafeStore.SelectedValue)
            {
                case "-1":
                    sqlcondition = sqlcondition + "";
                    break;
                case "0":
                    sqlcondition = sqlcondition + " and qua<=material_safetystock ";
                    break;
                case "1":
                    sqlcondition = sqlcondition + " and qua>material_safetystock ";
                    break;
                default:
                    break;
            };

            dt = dt_Query(sqlcondition);
        }
    }
}