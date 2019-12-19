using System;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using RM.Busines;
using System.Text;
using System.Web.UI.WebControls;

namespace RM.Web.MMS.MMS_Material
{
    public partial class MaterialNumQuery : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ttbStartDate.Text.ToString().Trim() != "")
            {

                dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), ddlMaterialType.SelectedValue.ToString());
                ReportDocument reportDoc = new ReportDocument();
                reportDoc.Load(Server.MapPath("Rpt_MaterialNumQuery.rpt"));
                reportDoc.SetDataSource(dt);
                string TEXT_OBJECT_NAME = @"FinanceMonth";
                TextObject text;
                text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
                text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";
                CrystalReportViewer1.ReportSource = reportDoc;

            }

            if (!IsPostBack)
            { init_Page(); }


        }


        private void init_Page()
        {
            ddlMaterialType.Items.Insert(0, new ListItem("", "-1"));
            ddlMaterialType.Items.Insert(1, new ListItem("卫生材料", "0"));
            ddlMaterialType.Items.Insert(1, new ListItem("低值易耗", "1"));
            ddlMaterialType.Items.Insert(1, new ListItem("其他材料", "2"));




        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(),ddlMaterialType.SelectedValue.ToString());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_MaterialNumQuery.rpt"));
            reportDoc.SetDataSource(dt);
            string TEXT_OBJECT_NAME = @"FinanceMonth";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";

           
            CrystalReportViewer1.ReportSource = reportDoc;
        }

        protected DataTable dt_Query(string starttime, string endtime,string material_type)
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
                @"select * from (select toptype,midtype material_type,productcode Material_ID,Material_Attr01,Material_Name,price,sum(lastquantity) lastquantity,sum(lastquantity*price) lastamount,sum(storequantity) storequantity,sum(storequantity*price) storeamount,sum(reqquantity) reqquantity,sum(reqquantity*price) reqamount,sum(lastquantity+storequantity-reqquantity) quantity,sum(price*(lastquantity+storequantity-reqquantity)) amount from (
select case when re.ProductCode is not null 
then 
re.ProductCode
else 
   req.ProductCode 
end productcode,
case when re.price is not null 
then 
re.price
else 
   req.price 
end price,
case when lastquantity is not null then  lastquantity
else 0 end lastquantity,
case when Reqquantity is not null then  Reqquantity
else 0
end storequantity,
case when Req.quantity is not null then  Req.quantity
else 0
end reqquantity
 from (
select case when laststore.ProductCode is not null 
then 
laststore.ProductCode
else 
   store.ProductCode 
end productcode,
case when laststore.price is not null 
then 
laststore.price
else 
   store.price 
end price,
case when laststore.quantity is not null then  laststore.quantity
else 0 end lastquantity,
case when store.quantity is not null then  store.quantity
else 0
end Reqquantity
from (select productcode,price,isnull(sum(price*quantity),0) as amount,isnull(sum(quantity),0) as quantity from MMS_Store where year(createdatetime)='{2}' and month(createdatetime)='{3}'
group by productcode,price) laststore
full join
(select productcode,price,isnull(sum(quantity),0) as quantity,isnull(sum(amount),0) as amount from dbo.StorageQuery where PurchaseDate>='{0}' and PurchaseDate<='{1}'
group by productcode,price) store on laststore.ProductCode=store.ProductCode and laststore.Price=store.Price) re
full join 
 (
select productcode,price,isnull(sum(quantity),0) quantity,isnull(sum(amount),0) amount  from (
select PurchaseBillCode,productcode,price,isnull(qua,0) quantity,isnull(price*qua,0) amount  from 
(select PurchaseBillCode,productcode,price,sum(quantity) qua from 
(select delivery.* from (
select purchasebillcode,productcode,sum(quantity) quantity,price from MMS_PurchasePlanDetail 
where AuditFlag IS NULL
group by purchasebillcode,productcode,price)  PlanDetail
inner join (
select purchasebillcode,productcode,sum(quantity) quantity,price,OperatorDate from MMS_Delivery_Detail 
group by purchasebillcode,productcode,price,OperatorDate
) delivery on PlanDetail.price=delivery.price and plandetail.productcode=delivery.productcode
and plandetail.purchasebillcode=delivery.purchasebillcode
where delivery.OperatorDate>='{0}' and delivery.OperatorDate<='{1}')req
group by PurchaseBillCode,productcode,price) req)req
group by ProductCode,price) req
on re.ProductCode=req.ProductCode and re.Price=req.Price) store
inner join MMS_MaterialInfo mms
on mms.Material_ID=store.productcode
inner join dbo.view_mattype mattype on substring(mms.material_type,0,charindex('-',mms.material_type))=mattype.submat
group by toptype,midtype,productcode,Material_Name,Material_Attr01,price) mstore where (lastquantity!=0 or storequantity!=0 or reqquantity!=0 or quantity!=0)";
            strSql= strSql+sqlcondition;
            if (Convert.ToInt32(material_type)!=-1) {
                strSql += " and Material_Attr01="+ Convert.ToInt32(material_type);
            }
             strSql = string.Format(strSql, starttime, endtime, Convert.ToDateTime(starttime).Year, Convert.ToDateTime(starttime).Month);
            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(strSql));

            return dt;
        }

        protected void lbtExce_Click(object sender, EventArgs e)
        {
//            string strSql = @"insert into dbo.MMS_Store(productcode,quantity,price,amount,createdatetime)
//select laststorage.productcode,laststorage.CurrentMonthBalancePrice Quantity,laststorage.price,(laststorage.price*laststorage.CurrentMonthBalancePrice) as amount,'{1}' CreateDateTime
// from (select distinct productcode,
//case when storageprice=0 then 
//  case when  deliveryprice=0 then storeprice else  deliveryprice end
//else storageprice  end price,storagequantity InStoragePrice ,deliveryquantity ReceivePrice,storequantity LastMonthBalancePrice,
//storagequantity-deliveryquantity+storequantity  CurrentMonthBalancePrice from (
//select mms.productcode,isnull(storage.price,0) storageprice,isnull(storage.quantity,0) storagequantity,isnull(delivery.price,0) deliveryprice,isnull(delivery.quantity,0) deliveryquantity,isnull(store.price,0) storeprice,isnull(store.quantity,0) storequantity from dbo.StorageQuery mms
//full join (select productcode,price,sum(quantity) as quantity  from storagequery where purchasedate>='{0}' and purchasedate<='{1}'
//group by productcode,price) storage on mms.productcode=storage.productcode and mms.price=storage.price
//full join (select productcode,price,sum(quantity) as quantity  from DeliveryQuery where OperatorDate>='{0}' and OperatorDate<='{1}'
//group by productcode,price) delivery 
//on mms.productcode=delivery.productcode and mms.price=delivery.price
//full join (select productcode,price,sum(quantity) as quantity  from MMS_Store store where createdatetime>=dateadd(month,-1,'{0}') and createdatetime<=dateadd(month,-1,'{1}')
//group by productcode,price) store
//on mms.productcode=store.productcode and mms.price=store.price) laststorage) laststorage 
// inner join dbo.MMS_MaterialInfo mat 
//on laststorage.productcode=mat.Material_ID  ";

//            string strSql = @"select mms.material_id,isnull(laststore.quantity,0) as lastquantity,isnull(laststore.amount,0) as lastamount,isnull(store.amount,0)as storeamount,isnull(req.amount,0) as reqamount from  (select * from MMS_MaterialInfo where deletemark='1') mms left join 
//(select productcode,isnull(sum(amount),0) as amount,isnull(sum(quantity),0) as quantity from MMS_Store where year(createdatetime)='{2}' and month(createdatetime)+1='{{3}'
//group by productcode) laststore on mms.material_id=laststore.productcode
//full join 
//(select productcode,isnull(sum(quantity),0) as quantity,isnull(sum(amount),0) as amount from dbo.StorageQuery where PurchaseDate>='{0}' and PurchaseDate<='{1}'
//group by productcode) store  on mms.material_id=store.productcode
//full join 
// (select productcode,isnull(sum(quantity),0) as quantity,isnull(sum(amount),0) as amount from 
//(SELECT RequisitionQuery.* FROM dbo.RequisitionQuery  
//INNER JOIN MMS_Delivery_Detail  devlivery on RequisitionQuery.PurchaseBillCode =devlivery .PurchaseBillCode and RequisitionQuery.ProductCode=devlivery.ProductCode
//and RequisitionQuery.Price=devlivery.Price 
//where OperatorDate>='{0}' and OperatorDate<='{1}')RequisitionQuery
//group by productcode) req on mms.material_id=req.productcode ";


            string strSql = @"insert into dbo.MMS_Store(productcode,price,quantity,amount,createdatetime)
select productcode,price,quantity,price*quantity amount,'{1}' from (select productcode,price,lastquantity+storequantity-reqquantity quantity
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
on mms.Material_ID=store.productcode) a";
            strSql = string.Format(strSql, ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), Convert.ToDateTime(ttbStartDate.Text.ToString().Trim()).Year, Convert.ToDateTime(ttbStartDate.Text.ToString().Trim()).Month);

          //  strSql = string.Format(strSql, ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), ddlMaterialType.SelectedValue.ToString());
            int re = DataFactory.SqlDataBase().ExecuteBySql(new StringBuilder(strSql));

            Response.Write("<script language='javascript'>alert('结转成功！');</script>");
        }
    }
}