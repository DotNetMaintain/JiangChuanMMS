using System;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using RM.Busines;
using System.Text;
using System.Web.UI.WebControls;


namespace RM.Web.MMS.report
{
    public partial class Matybselect : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        ReportDocument reportDoc = new ReportDocument();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlcondition = string.Empty;


           
            if (ttbStartDate.Text.ToString().Trim() != "" && ttbEndDate.Text.ToString().Trim() != "")
            {
                dt = dt_Query();
                reportDoc.Load(Server.MapPath("rpt_matybselect.rpt"));
                reportDoc.SetDataSource(dt);

                string TEXT_OBJECT_NAME = @"Text12";
                TextObject text;
                text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
                text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";

                CrystalReportViewer1.ReportSource = reportDoc;
            }



        }


        protected DataTable dt_Query()
        {


//            string strSql = @"select material_id,material_name,material_specification,case when reqquantity is  null  then 0 else  reqquantity end  Reqquantity,
//case when mstorequantity is  null  then 0 else  mstorequantity end  mstorequantity
// from (select material_id,material_name,material_Specification,reqquantity,mstorequantity from (
//select  case when req.ProductCode is not null 
//then 
//req.ProductCode
//else 
//   mstore.ProductCode
//end productcode,
//   case when req.quantity is not null then  req.quantity
//else 0
//end Reqquantity,
//case when mstore.quantity is not null then  mstore.quantity
//else 0
//end mstorequantity 
// from (
//select productcode,isnull(sum(quantity),0) as quantity from dbo.StorageQuery where PurchaseDate>='{0}' and PurchaseDate<='{1}'
//group by productcode) req
//full join
//(
//select productcode,isnull(sum(quantity),0) quantity  from (
//select PurchaseBillCode,productcode,price,isnull(qua,0) quantity,isnull(price*qua,0) amount  from 
//(select PurchaseBillCode,productcode,price,sum(quantity) qua from 
//(select delivery.* from (
//select purchasebillcode,productcode,sum(quantity) quantity,price from MMS_PurchasePlanDetail 
//where AuditFlag IS NULL
//group by purchasebillcode,productcode,price)  PlanDetail
//inner join (
//select purchasebillcode,productcode,sum(quantity) quantity,price,OperatorDate from MMS_Delivery_Detail 
//group by purchasebillcode,productcode,price,OperatorDate
//) delivery on PlanDetail.price=delivery.price and plandetail.productcode=delivery.productcode
//and plandetail.purchasebillcode=delivery.purchasebillcode
//where delivery.OperatorDate>='{0}' and delivery.OperatorDate<='{1}')req
//group by PurchaseBillCode,productcode,price) req)req
//group by ProductCode
//) mstore
//on req.ProductCode=mstore.ProductCode) matt
//right join MMS_MaterialInfo mms
//on mms.material_id=matt.productcode
//where deletemark=1 and material_name like '%胃管%'
//or 
//deletemark=1 and material_name  like '%导尿%'
//or 
//deletemark=1 and material_name like '%留置%'
//or 
//deletemark=1 and material_name like '%集尿%'
//or 
//deletemark=1 and material_name like '%泡沫敷料%'
//or 
//deletemark=1 and material_name like '%水凝胶伤口敷料%'
//or 
//deletemark=1 and material_name like '%藻酸钙伤口敷料%'
//or 
//deletemark=1 and material_name like '%含银敷料%'
//or 
//deletemark=1 and material_name like '%胰岛素针%'
//) mastore
// where 1=1
// order by material_name";




            string strSql = @"select * from (select material_id,material_name,material_specification,price,sum(lastquantity) lastmonthquantity, sum(storequantity) Reqquantity,sum(reqquantity) mstorequantity 
from MMS_MaterialInfo mms inner join 
(
select case when laststore.ProductCode is not null 
then 
laststore.ProductCode
else 
   case when store.ProductCode is not null then store.ProductCode
   else
     req.ProductCode
   end 
end productcode,
case when laststore.price is not null 
then 
laststore.price
else 
   case when store.price is not null then store.price
   else
     req.price
   end 
end price,
case when laststore.quantity is not null then  laststore.quantity
else
0
end lastquantity,
case when store.quantity is not null then  store.quantity
else
0
end storequantity,
case when Req.quantity is not null then  Req.quantity
else
0
end Reqquantity
  from (
select productcode,price,isnull(sum(amount),0) as amount,isnull(sum(quantity),0) as quantity from MMS_Store where year(createdatetime)='{2}' and month(createdatetime)='{3}'
group by productcode,price) laststore
full join
(select productcode,price,isnull(sum(quantity),0) as quantity,isnull(sum(amount),0) as amount from dbo.StorageQuery where PurchaseDate>='{0}' and PurchaseDate<='{1}'
group by productcode,price) store on laststore.ProductCode=store.ProductCode and laststore.Price=store.Price
full join 
 (select productcode,price,isnull(sum(quantity),0) as quantity,isnull(sum(amount),0) as amount from 
(  select req.purchasebillcode,req.productcode,req.price,isnull(qub,0) as quantity,isnull((req.price*qub),0) amount from (select PurchaseBillCode,productcode,price,sum(quantity) qua from RequisitionQuery 
group by productcode,price,purchasebillcode)Req
inner join (
select purchasebillcode,productcode,price,sum(quantity) qub from 
(select * from MMS_Delivery_Detail where OperatorDate>='{0}' and OperatorDate<='{1}')  devlivery
group by productcode,price,purchasebillcode) Deli on
req.productcode=deli.productcode and req.price=deli.price and req.purchasebillcode=deli.purchasebillcode   ) req 
group by ProductCode,price) req
on laststore.ProductCode=req.ProductCode and laststore.Price=req.Price) store
on mms.Material_ID=store.productcode
where deletemark=1 and material_name like '%胃管%'
or 
deletemark=1 and material_name  like '%导尿%'
or 
deletemark=1 and material_name like '%留置%'
or 
deletemark=1 and material_name like '%集尿%'
or 
deletemark=1 and material_name like '%泡沫敷料%'
or 
deletemark=1 and material_name like '%水凝胶伤口敷料%'
or 
deletemark=1 and material_name like '%藻酸钙伤口敷料%'
or 
deletemark=1 and material_name like '%含银敷料%'
or 
deletemark=1 and material_name like '%胰岛素针%'
or 
deletemark=1 and material_name like '一次性使用遮光输液器'
or 
deletemark=1 and material_name like '一次性使用精密过滤输液器'
or 
deletemark=1 and material_name like '费领支具'
or 
deletemark=1 and material_name like '肋骨带'
or 
deletemark=1 and material_name like '腕部支具'
or 
deletemark=1 and material_name like '颈托'
or 
deletemark=1 and material_name like '凝胶敷料（微赛恩）'
group by material_id,material_name,material_specification,price) store
where 1=1
 order by material_name";


            strSql = string.Format(strSql, ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), Convert.ToDateTime(ttbStartDate.Text.ToString().Trim()).AddDays(-1).Year, Convert.ToDateTime(ttbStartDate.Text.ToString().Trim()).AddDays(-1).Month);


            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(strSql));
            string price = dt.Rows[0][3].ToString();
            Console.WriteLine(price);
            return dt;
        }


        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            if (ttbStartDate.Text.ToString().Trim() != "" && ttbEndDate.Text.ToString().Trim() != "")
            {
                dt = dt_Query();
                reportDoc.Load(Server.MapPath("rpt_matybselect.rpt"));
                reportDoc.SetDataSource(dt);

                string TEXT_OBJECT_NAME = @"Text12";
                TextObject text;
                text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
                text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";

                CrystalReportViewer1.ReportSource = reportDoc;
            }
        }

    }
}