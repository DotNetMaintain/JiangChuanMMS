using RM.Busines;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using System;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using RM.ServiceProvider;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;
using RM.ServerControl;
using GridView = System.Web.UI.WebControls.GridView;
using System.Text;
using System.Data;

namespace RM.Web.RMBase.SysBaseInfo
{

public partial class SelectStorageMaterial : PageBase
{

    DataTable dt_Material = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //List<MMS_MaterialInfo> sumList = MaterialInfoService.Instance.GetAllInfo(txtHelpCode.Text, -1, OurPager1.PageSize);
            //OurPager1.RecordCount = sumList[0].Material_ID;
            LoadData(OurPager1.CurrentPageIndex);
            OurPager1.RecordCount = dt_Material.Rows.Count;
        }
        
    }

    private void LoadData(int currentPageIndex)
    {
        //List<MMS_MaterialInfo> infoList = new List<MMS_MaterialInfo>();
        //infoList = MaterialInfoService.Instance.GetAllInfo(txtHelpCode.Text, currentPageIndex, OurPager1.PageSize);
        //List<Base_DictionaryInfo> dictList = DictionaryInfoService.Instance.GetAllInfo();
        string sqlwhere = string.Empty;

            //        string str = @"select mms.Material_ID,mms.material_name,mms.Material_CommonlyName,Material_Specification,Material_Unit,Material_Supplier,Material_Type,pur.* from dbo.MMS_MaterialInfo mms inner join (
            //                        select pur.productcode,(isnull(pur.qua,0)-isnull(purplan.qua,0)) as qua,price from (select productcode,sum(quantity) as  qua,price from MMS_PurchaseDetail group by productcode,price) pur
            //                        left join (select * from (select productcode,sum(quantity) as qua from MMS_PurchasePlanDetail group by productcode) purplan)purplan
            //                        on pur.ProductCode=purplan.ProductCode) pur
            //                        on mms.material_id=pur.productcode
            //                        where (qua>0 or qua is null)";

            //        string str = @"select mms.Material_ID,mms.material_name,mms.Material_CommonlyName,Material_Specification,Material_Unit,Material_Supplier,Material_Type,pur.* from dbo.MMS_MaterialInfo mms inner join (
            //                        select pur.productcode,(isnull(pur.qua,0)-isnull(purplan.qua,0)) as qua,pur.price,pur.Lot,pur.ValidDate from (select productcode,sum(quantity) as  qua,price,Lot,ValidDate from MMS_PurchaseDetail group by productcode,price,Lot,ValidDate) pur
            //                        left join (select * from (select productcode,sum(quantity) as qua,price from (
            //select detail.* from MMS_PurchasePlanContent plancontent 
            //inner join MMS_PurchasePlanDetail  detail
            //on plancontent.PurchaseBillCode=detail.PurchaseBillCode
            //where paymode in ('1','2') and detail.AuditFlag is null) de
            //group by productcode,price) purplan)purplan
            //                        on pur.ProductCode=purplan.ProductCode and pur.price=purplan.price) pur
            //                        on mms.material_id=pur.productcode
            //                        where (qua>0 or qua is null) ";




            //        string str = @"select mms.Material_ID,mms.material_name,mms.Material_CommonlyName,Material_Specification,Material_Unit,Material_Supplier,Material_Type,st.* from (
            //select material_id productcode,currentquntity qua,price from (select Material_Type,material_id,price,lastquantity,storequantity,reqquantity,lastquantity+storequantity-reqquantity currentquntity from (
            //select substring(mms.material_type,0,charindex('-',mms.material_type)) Material_Type,material_id,price,sum(lastquantity) lastquantity,sum(storequantity) storequantity,sum(reqquantity) reqquantity
            //from MMS_MaterialInfo mms inner join 
            //(
            //select case when laststore.ProductCode is not null 
            //then 
            //laststore.ProductCode
            //else 
            //   case when store.ProductCode is not null then store.ProductCode
            //   else
            //     req.ProductCode
            //   end 
            //end productcode,
            //case when laststore.price is not null 
            //then 
            //laststore.price
            //else 
            //   case when store.price is not null then store.price
            //   else
            //     req.price
            //   end 
            //end price,
            //case when laststore.quantity is not null then  laststore.quantity
            //else
            //0
            //end lastquantity,
            //case when store.quantity is not null then  store.quantity
            //else
            //0
            //end storequantity,
            //case when Req.quantity is not null then  Req.quantity
            //else
            //0
            //end Reqquantity
            //  from (
            //select productcode,price,isnull(sum(amount),0) as amount,isnull(sum(quantity),0) as quantity from MMS_Store where year(createdatetime)=year(dateadd(dd,-1,getdate())) and month(createdatetime)=month(dateadd(month,-1,getdate()))
            //group by productcode,price) laststore
            //full join
            //(select productcode,price,isnull(sum(quantity),0) as quantity,isnull(sum(amount),0) as amount from dbo.StorageQuery where PurchaseDate>='2017/05/01' and PurchaseDate<=getdate()
            //group by productcode,price) store on laststore.ProductCode=store.ProductCode and laststore.Price=store.Price
            //full join 
            // (select productcode,price,isnull(sum(quantity),0) as quantity,isnull(sum(amount),0) as amount from 
            //(  select req.purchasebillcode,req.productcode,req.price,isnull(qub,0) as quantity,isnull((req.price*qub),0) amount from (select PurchaseBillCode,productcode,price,sum(quantity) qua from RequisitionQuery 
            //group by productcode,price,purchasebillcode)Req
            //inner join (
            //select purchasebillcode,productcode,price,sum(quantity) qub from 
            //(select * from MMS_Delivery_Detail where OperatorDate>='2017/05/01' and OperatorDate<=getdate())  devlivery
            //group by productcode,price,purchasebillcode) Deli on
            //req.productcode=deli.productcode and req.price=deli.price and req.purchasebillcode=deli.purchasebillcode   ) req 
            //group by ProductCode,price) req
            //on laststore.ProductCode=req.ProductCode and laststore.Price=req.Price) store
            //on mms.Material_ID=store.productcode
            //group by Material_Type,material_id,price) store) s) st
            //inner join dbo.MMS_MaterialInfo mms on st.productcode=mms.material_id
            //where (qua>0 or qua is null)"; and checkquantity!=0

            string str = @"select mms.Material_ID,mms.material_name,mms.Material_CommonlyName,Material_Specification,Material_Unit,Material_Supplier,Material_Type,pur.* from dbo.MMS_MaterialInfo mms inner join (
                        select pur.productcode,(isnull(pur.qua,0)-isnull(purplan.qua,0)) as qua,pur.price from (select productcode,sum(quantity) as  qua,price from MMS_PurchaseDetail group by productcode,price) pur
                        left join (select * from (select productcode,sum(quantity) as qua,price from (
select detail.* from MMS_PurchasePlanContent plancontent 
inner join MMS_PurchasePlanDetail  detail
on plancontent.PurchaseBillCode=detail.PurchaseBillCode
where paymode in ('1','2') and detail.AuditFlag is null) de
group by productcode,price) purplan)purplan
                        on pur.ProductCode=purplan.ProductCode and pur.price=purplan.price) pur
                        on mms.material_id=pur.productcode
                        where (qua>0 or qua is null) ";
            if (!string.IsNullOrEmpty(txtHelpCode.Text.ToString().Trim()))
        {
            sqlwhere = " and mms.Material_CommonlyName like '%" + txtHelpCode.Text.ToString().Trim() + "%'";
            str = str + sqlwhere;
        }

        str = str + " order by mms.Material_ID";
         dt_Material = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(str));
        dgvInfo.DataKeyNames = new[] { "Material_ID" };
        dgvInfo.DataSource = dt_Material; //设置GridView数据源
        dgvInfo.DataBind();

        


        //var query = from info in infoList
        //            join dictProductType in dictList
        //                on info.Material_Type equals dictProductType.ValueName
        //            where dictProductType.TypeCode == "MaterialTypeList"
        //            select new
        //                {
        //                    info.Material_ID,
        //                    info.Material_Code,
        //                    info.Material_Name,
        //                    info.Material_CommonlyName,
        //                    info.Material_Specification ,
        //                    info.Material_Unit,
        //                    info.Material_Supplier,
        //                    ProductType = dictProductType.ValueName,
        //                    info.Material_Type ,
        //                    Unit = info.Material_Unit
                            
        //                };
        
    }

    protected void dgvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void dgvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //如果是数据行
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#6699ff' ");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            GridView grid = sender as GridView; //取当前操作的GridView
            string code = grid.DataKeys[e.Row.RowIndex].Value.ToString();
            string name = e.Row.Cells[1].Text;
            string price=e.Row .Cells[7].Text;
            string usequantity=e.Row .Cells[6].Text ;
            ((LinkButton) (e.Row.Cells[grid.Columns.Count - 1].Controls[0])).Attributes.Add("onclick",
                                                                                            "btnSelect_onclick('" + code +
                                                                                            "','" + name + "','" + price + "','" + usequantity + "')");

            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       // List<MMS_MaterialInfo> sumList = MaterialInfoService.Instance.GetAllInfo(txtHelpCode.Text, -1, OurPager1.PageSize);
        OurPager1.RecordCount = dt_Material.Rows.Count;    // sumList[0].Material_ID ;
        LoadData(OurPager1.CurrentPageIndex);
    }

    protected void dgvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void OurPager1_PageChanged(object sender, PageArgs e)
    {
        LoadData(e.NewPageIndex);
    }
}
    
}











