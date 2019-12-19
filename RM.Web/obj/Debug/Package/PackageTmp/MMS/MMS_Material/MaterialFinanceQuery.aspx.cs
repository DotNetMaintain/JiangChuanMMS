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
    public partial class MaterialFinanceQuery : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        ReportDocument reportDoc = new ReportDocument();

      
         
        protected void Page_Load(object sender, EventArgs e)
        {

            ReportDocument reportDoc = new ReportDocument();
            if (!IsPostBack)
            { init_Page(); }
            else
            {

                if (IsPostBack)
                {

                    dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());

                    reportDoc.Load(Server.MapPath("Rpt_MaterialFinance.rpt"));
                    reportDoc.SetDataSource(dt);
                    string TEXT_OBJECT_NAME = @"FinanceMonth";
                    TextObject text;
                    text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
                    text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";
                    CrystalReportViewer1.ReportSource = reportDoc;
                }

            }
        }


        private void init_Page()
        {
            //ddlMaterialType.Items.Insert(0, new ListItem("", "-1"));
            //ddlMaterialType.Items.Insert(1, new ListItem("运保物资", "0"));
            //ddlMaterialType.Items.Insert(1, new ListItem("医疗耗材", "1"));




        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());
            reportDoc.Load(Server.MapPath("Rpt_MaterialFinance.rpt"));
            reportDoc.SetDataSource(dt);
            string TEXT_OBJECT_NAME = @"FinanceMonth";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";

           
            CrystalReportViewer1.ReportSource = reportDoc;
        }

        protected DataTable dt_Query(string starttime, string endtime)
        {

//            string strSql = @"select MaterialType_Name as Material_Type,MaterialType_Code,SUM(LastMonthBalancePrice) as LastMonthBalancePrice,SUM(InStoragePrice) as InStoragePrice,
//SUM(ReceivePrice) as ReceivePrice,SUM(CurrentMonthBalancePrice) as CurrentMonthBalancePrice
// from (
//select MaterialType_Name,mattype.MaterialType_Code,LastMonthBalancePrice,InStoragePrice,ReceivePrice,LastMonthBalancePrice+InStoragePrice-ReceivePrice as CurrentMonthBalancePrice 
//from (select substring(material_type,0,charindex('-',material_type)) as material_type,sum(lastamount) LastMonthBalancePrice ,sum(storeamount) InStoragePrice,sum(reqamount) ReceivePrice from (
//select mms.*,isnull(laststore.amount,0) as lastamount,isnull(store.amount,0)as storeamount,isnull(req.amount,0) as reqamount from  (select * from MMS_MaterialInfo where deletemark='1') mms left join 
//(select productcode,isnull(sum(amount),0) as amount,isnull(sum(quantity),0) as quantity from MMS_Store where year(createdatetime)='{2}' and month(createdatetime)+1='{3}'
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
//group by productcode) req on mms.material_id=req.productcode ) storetotal
//group by material_type) store 
//inner join  dbo.MMS_MaterialType mattype on store.material_type=mattype.MaterialType_Name
//)
// materialtypetotal group by MaterialType_Name,MaterialType_Code
//order by MaterialType_Code";

            string strSql = @"select mat.toptype,mat.midtype,matstore.* from (select  Material_Type,MaterialType_Code,sum(lastquantity) lastquantity,sum(LastMonthBalancePrice) LastMonthBalancePrice,sum(storequantity) storequantity,sum(InStoragePrice) InStoragePrice,sum(reqquantity) reqquantity,sum(ReceivePrice) ReceivePrice,sum(currentquntity) currentquntity,sum(CurrentMonthBalancePrice) CurrentMonthBalancePrice
from (select Material_Type,lastquantity,LastMonthBalancePrice,storequantity,InStoragePrice,reqquantity,ReceivePrice,lastquantity+storequantity-reqquantity currentquntity,LastMonthBalancePrice+InStoragePrice-ReceivePrice as CurrentMonthBalancePrice from (
select substring(mms.material_type,0,charindex('-',mms.material_type)) Material_Type,sum(lastquantity) lastquantity,sum(price*lastquantity) LastMonthBalancePrice,sum(storequantity) storequantity, sum(price*storequantity) InStoragePrice,sum(reqquantity) reqquantity,sum(price*reqquantity) ReceivePrice 
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
group by Material_Type) store) matstore
inner join  dbo.MMS_MaterialType mattype on matstore.material_type=mattype.MaterialType_Name
group by Material_Type,MaterialType_Code) matstore
inner join view_mattype mat on matstore.Material_Type=mat.submat
order by MaterialType_Code";
            strSql = string.Format(strSql, starttime, endtime, Convert.ToDateTime(starttime).Year, Convert.ToDateTime(starttime).Month);
            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(strSql));

//            string strSql = @"
//select mattype.toptype,mattype.midtype,matdetail.* from 
//(
//SELECT     submat, CASE WHEN updatetoptype IS NULL THEN mat ELSE updatetoptype END AS toptype, CASE WHEN materialtype_name IS NULL 
//                      THEN submat ELSE updatemat END AS midtype
//FROM         (SELECT     submat, mat, MaterialType_Name, toptype, CASE WHEN toptype IS NULL THEN materialtype_name ELSE toptype END AS updatetoptype, 
//                                              CASE WHEN toptype IS NOT NULL THEN materialtype_name ELSE mat END AS updatemat
//                       FROM          (SELECT     mat.submat, mat.mat, mat.MaterialType_Name, mattype.MaterialType_Name AS toptype
//                                               FROM          (SELECT     paremat.submat, paremat.mat, mattype.MaterialType_Name, mattype.ParentId
//                                                                       FROM          (SELECT     paretype.ParentId, mattype_1.MaterialType_Name AS submat, paretype.MaterialType_Name AS mat
//                                                                                               FROM          (SELECT     MaterialType_ID, MaterialType_Code, MaterialType_Name, ParentId, SortCode, DeleteMark, ModifyDate, 
//                                                                                                                                              ModifyUserId, ModifyUserName
//                                                                                                                       FROM          dbo.MMS_MaterialType
//                                                                                                                       WHERE      (MaterialType_ID NOT IN
//                                                                                                                                                  (SELECT     ParentId
//                                                                                                                                                    FROM          dbo.MMS_MaterialType AS MMS_MaterialType_2))) AS mattype_1 LEFT OUTER JOIN
//                                                                                                                          (SELECT     MaterialType_ID, MaterialType_Code, MaterialType_Name, ParentId, SortCode, DeleteMark, ModifyDate, 
//                                                                                                                                                   ModifyUserId, ModifyUserName
//                                                                                                                            FROM          dbo.MMS_MaterialType AS MMS_MaterialType_1) AS paretype ON 
//                                                                                                                      mattype_1.ParentId = paretype.MaterialType_ID) AS paremat LEFT OUTER JOIN
//                                                                                              dbo.MMS_MaterialType AS mattype ON paremat.ParentId = mattype.MaterialType_ID) AS mat LEFT OUTER JOIN
//                                                                      dbo.MMS_MaterialType AS mattype ON mat.ParentId = mattype.MaterialType_ID) AS mattype_2) AS mattype_3
//) mattype inner join (
//select MaterialType_Name as Material_Type,MaterialType_Code,SUM(LastMonthBalancePrice) as LastMonthBalancePrice,SUM(InStoragePrice) as InStoragePrice,
//SUM(ReceivePrice) as ReceivePrice,SUM(CurrentMonthBalancePrice) as CurrentMonthBalancePrice
// from (
//select MaterialType_Name,mattype.MaterialType_Code,LastMonthBalancePrice,InStoragePrice,ReceivePrice,LastMonthBalancePrice+InStoragePrice-ReceivePrice as CurrentMonthBalancePrice 
//from (select substring(material_type,0,charindex('-',material_type)) as material_type,sum(lastamount) LastMonthBalancePrice ,sum(storeamount) InStoragePrice,sum(reqamount) ReceivePrice from (
//select mms.*,isnull(laststore.amount,0) as lastamount,isnull(store.amount,0)as storeamount,isnull(req.amount,0) as reqamount from  (select * from MMS_MaterialInfo where deletemark='1') mms left join 
//(select productcode,isnull(sum(amount),0) as amount,isnull(sum(quantity),0) as quantity from MMS_Store where year(createdatetime)='{2}' and month(createdatetime)+1='{3}'
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
//group by productcode) req on mms.material_id=req.productcode ) storetotal
//group by material_type) store 
//inner join  dbo.MMS_MaterialType mattype on store.material_type=mattype.MaterialType_Name
//)
// materialtypetotal group by MaterialType_Name,MaterialType_Code
//) matdetail
//on matdetail.material_type=mattype.submat
//order by materialtype_code";
//            strSql = string.Format(strSql, starttime, endtime, Convert.ToDateTime(endtime).AddDays(-1).Year, Convert.ToDateTime(endtime).AddDays(-1).Month);
//            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(strSql));

            return dt;
        }

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());
            reportDoc.Load(Server.MapPath("Rpt_MaterialFinance.rpt"));
            reportDoc.SetDataSource(dt);
            string TEXT_OBJECT_NAME = @"FinanceMonth";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";


            CrystalReportViewer1.ReportSource = reportDoc;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());
            reportDoc.Load(Server.MapPath("Rpt_MaterialFinance.rpt"));
            reportDoc.SetDataSource(dt);
            string TEXT_OBJECT_NAME = @"FinanceMonth";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Year.ToString() + "年" + Convert.ToDateTime(ttbEndDate.Text.ToString().Trim()).AddDays(-1).Month.ToString() + "月";


            CrystalReportViewer1.ReportSource = reportDoc;
        }
    }
}