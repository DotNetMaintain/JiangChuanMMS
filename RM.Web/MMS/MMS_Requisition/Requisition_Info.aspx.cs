using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using RM.Busines;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;

namespace RM.Web.MMS.MMS_Requisition
{
    public partial class Requisition_Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        /// <summary>
        ///   GridView数据导入方法
        /// </summary>
        private void LoadData()
        {
            string str = @"select plancon.*,material.Material_Type,Material_Name,Material_Specification,Material_Unit,Material_Supplier from (
                            select plancontent.PurchaseBillCode,plancontent.DeptName,plancontent.PurchaseDate,detail.ProductCode,detail.Quantity from MMS_PurchasePlanContent plancontent inner join 
                            MMS_PurchasePlanDetail detail on plancontent.PurchaseBillCode=detail.PurchaseBillCode
                            where AuditFlag=1) plancon 
                            left join dbo.MMS_MaterialInfo material on plancon.ProductCode=material.Material_ID
                            order by deptname,productcode";
            str = string.Format(str);
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(str));
            ControlBindHelper.BindRepeaterList(dt, this.rp_Item);




        }


    }
}