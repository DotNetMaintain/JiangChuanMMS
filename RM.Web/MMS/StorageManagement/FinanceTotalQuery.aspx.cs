using System;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using RM.Busines;
using System.Text;

namespace RM.Web.MMS.StorageManagement
{
    public partial class FinanceTotalQuery : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable ds = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_FinanceTotal.rpt"));
            reportDoc.SetDataSource(ds);
            string TEXT_OBJECT_NAME = @"FinanceDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月";


            CrystalReportViewer1.ReportSource = reportDoc;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataTable ds = dt_Query(ttbStartDate.Text.ToString().Trim(),ttbEndDate.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_FinanceTotal.rpt"));
            reportDoc.SetDataSource(ds);
            string TEXT_OBJECT_NAME = @"FinanceDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = Convert.ToDateTime(ttbStartDate.Text.ToString().Trim()).Year.ToString() + "年" + Convert.ToDateTime(ttbStartDate.Text.ToString().Trim()).Month.ToString() + "月";


            CrystalReportViewer1.ReportSource = reportDoc;
        }





        protected DataTable dt_Query(string beginyear, string beginmonth)
        {
            //            string strSql =
            //               @"select  plancontent.deptName,sum(devlivery.Quantity*devlivery.Price) as Amount from MMS_PurchasePlanContent plancontent inner join 
            //MMS_PurchasePlanDetail detail on plancontent.PurchaseBillCode=detail.PurchaseBillCode
            //inner join MMS_Delivery_Detail  devlivery
            //on detail.PurchaseBillCode=devlivery.PurchaseBillCode and detail.ProductCode=devlivery.ProductCode
            //where YEAR(devlivery.OperatorDate)='{0}' AND MONTH(devlivery.OperatorDate)='{1}'
            //group by DeptName";

            //            string strSql = @"select DeptName ,SUM(amount) amount from (select store.DeptName ,store.amount  from 
            //(select store.*,substring(mms.Material_Type,1,LEN(mms.Material_Type)-5) as material_type from 
            //(SELECT     dbo.MMS_PurchasePlanContent.PurchaseBillCode, dbo.MMS_PurchasePlanContent.DeptName, dbo.MMS_PurchasePlanDetail.ProductCode, 
            //                      dbo.MMS_PurchasePlanDetail.Quantity, dbo.MMS_PurchasePlanDetail.Price, 
            //                      dbo.MMS_PurchasePlanDetail.Price * dbo.MMS_PurchasePlanDetail.Quantity AS amount
            //FROM         dbo.MMS_PurchasePlanContent INNER JOIN
            //                      dbo.MMS_PurchasePlanDetail ON dbo.MMS_PurchasePlanContent.PurchaseBillCode = dbo.MMS_PurchasePlanDetail.PurchaseBillCode
            //WHERE     (dbo.MMS_PurchasePlanDetail.AuditFlag IS NULL)) store
            //inner join (select * from MMS_MaterialInfo) mms on store.ProductCode=mms.Material_ID
            //inner join (select * from MMS_Delivery_Detail where OperatorDate>='{0}' and OperatorDate<='{1}' )   devlivery
            //on store.PurchaseBillCode=devlivery.PurchaseBillCode and store.ProductCode=devlivery.ProductCode and  store.Price=devlivery.Price) store
            //inner join 
            //(select MMS_MaterialType.MaterialType_Name from dbo.MMS_MaterialType) parmat
            //on store.material_type =parmat.MaterialType_Name) store
            //group by DeptName";
            string strSql = @"select DeptName,
SUM(case MaterialType_Name when '低值易耗品' then amount else 0 end) 低值易耗品,
SUM(case MaterialType_Name when '其他材料' then amount else 0 end) 其他材料,
SUM(case MaterialType_Name when '其他耗材' then amount else 0 end) 其他耗材,
SUM(case MaterialType_Name when '卫生材料' then amount else 0 end) 卫生材料 from (
select content.deptname as DeptName,sum(content.amount) as amount,toptype MaterialType_Name from (select  plancontent.deptName,substring(mat.material_type,0,charindex('-',mat.material_type)) material_type,sum(detail.Quantity*detail.Price) amount,type.toptype from MMS_PurchasePlanContent plancontent inner join 
(select * from MMS_PurchasePlanDetail where  AuditFlag IS NULL) detail on plancontent.PurchaseBillCode=detail.PurchaseBillCode
inner join MMS_Delivery_Detail  devlivery
on detail.PurchaseBillCode=devlivery.PurchaseBillCode and detail.ProductCode=devlivery.ProductCode and detail.price=devlivery.price
inner join MMS_MaterialInfo mat on detail.productcode=mat.material_id
  INNER JOIN view_mattype type on type.submat=substring(mat.Material_Type,1,LEN(mat.Material_Type)-5)
where devlivery.OperatorDate>='{0}' AND devlivery.OperatorDate<'{1}'
group by DeptName,material_type,toptype) content GROUP by content.deptname,toptype)
store
group by deptname";
            strSql = string.Format(strSql, beginyear, beginmonth);
            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(strSql));

            return dt;
        }



    }
}