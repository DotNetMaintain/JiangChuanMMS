using System;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using RM.Busines;
using System.Text;

namespace RM.Web.MMS.StorageManagement
{
    public partial class MaterialTypeTotal : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            dt = ds_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_MaterialTypeQuery.rpt"));
            reportDoc.SetDataSource(dt);
            string TEXT_OBJECT_NAME = @"BeginDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = ttbStartDate.Text.ToString().Trim();

            string TEXT_OBJECT_NAME2 = @"EndDate";
            TextObject text2;
            text2 = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME2] as TextObject;
            text2.Text = ttbEndDate.Text.ToString().Trim();



            CrystalReportViewer1.ReportSource = reportDoc;
        }




        protected void btnQuery_Click(object sender, EventArgs e)
        {
            dt = ds_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_MaterialTypeQuery.rpt"));
            reportDoc.SetDataSource(dt);
            string TEXT_OBJECT_NAME = @"BeginDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = ttbStartDate.Text.ToString().Trim();

            string TEXT_OBJECT_NAME2 = @"EndDate";
            TextObject text2;
            text2 = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME2] as TextObject;
            text2.Text = ttbEndDate.Text.ToString().Trim();



            CrystalReportViewer1.ReportSource = reportDoc;
        }



        protected DataTable  ds_Query(string begindate, string enddate)
        {
            string str = @"select b.*,isnull(a.LastMonthBalance,0) as LastMonthBalancePrice from 
(select purchase.*,delivery.InStoragePrice  from (select Material_type,Material_name,Sum(Amount) as ReceivePrice from 
(SELECT Material.Material_type,Material.Material_name,Storage.* FROM 
(select PurCont.PurchaseDate,PurDetail.ProductCode,(quantity*price) as Amount from MMS_PurchaseContent PurCont inner join 
MMS_PurchaseDetail  PurDetail on PurCont.PurchaseBillCode=PurDetail.PurchaseBillCode 
WHERE PurchaseDate>='{0}' and PurchaseDate<='{1}' ) Storage
LEFT JOIN MMS_MaterialInfo Material on Storage.ProductCode=Material.Material_Code) StorageQuery
group by Material_type,Material_name) purchase
left join (
select * from(
select Material_type,Material_name,sum(amount) as InStoragePrice from (
(select productcode,(quantity*price) as amount,OperatorDate from MMS_Delivery_Detail
where OperatorDate>='{0}' and OperatorDate<='{1}' )) Delivery
LEFT JOIN   MMS_MaterialInfo Material on Delivery.ProductCode=Material.Material_Code
group by Material_type,Material_name) delivery ) delivery
on purchase.material_name=delivery.material_name) b
left join (
select Material_type,Material_name,(InstoragePrice-ReceivePrice) as LastMonthBalance from 
(select purchase.*,delivery.InStoragePrice  from (select Material_type,Material_name,Sum(Amount) as ReceivePrice from 
(SELECT Material.Material_type,Material.Material_name,Storage.* FROM 
(select PurCont.PurchaseDate,PurDetail.ProductCode,(quantity*price) as Amount from MMS_PurchaseContent PurCont inner join 
MMS_PurchaseDetail  PurDetail on PurCont.PurchaseBillCode=PurDetail.PurchaseBillCode 
WHERE dateadd(month,1,convert(datetime,PurchaseDate))>='{0}' and dateadd(month,1,convert(datetime,PurchaseDate))<='{1}') Storage
LEFT JOIN MMS_MaterialInfo Material on Storage.ProductCode=Material.Material_Code) StorageQuery
group by Material_type,Material_name) purchase
left join (
select * from(
select Material_type,Material_name,sum(amount) as InStoragePrice from (
(select productcode,(quantity*price) as amount,OperatorDate from MMS_Delivery_Detail
where dateadd(month,1,convert(datetime,OperatorDate))>='{0}' and dateadd(month,1,convert(datetime,OperatorDate))<='{1}')) Delivery
LEFT JOIN   MMS_MaterialInfo Material on Delivery.ProductCode=Material.Material_Code
group by Material_type,Material_name) delivery ) delivery
on purchase.material_name=delivery.material_name) a ) a on b.material_name=a.material_name";
            str = string.Format(str, begindate, enddate);
            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(str));
            return dt;

        }


    }
}