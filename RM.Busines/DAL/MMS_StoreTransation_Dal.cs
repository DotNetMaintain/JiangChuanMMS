using RM.Busines.DAO;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetEncrypt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RM.Busines.DAL
{
    public class MMS_StoreTransation_Dal : MMS_StoreTransation_IDAO
    {


         #region method

         public DataTable Load_StoreTransationList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM MMS_StoreTransation ORDER BY Material_id ASC");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }


        public DataTable GetStoreTransationPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from MMS_StoreTransation U where 1=1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "Desc", pageIndex, pageSize, ref count);
        }



        public DataTable GetStoreDeliveryPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (
                        select plancon.*,material.Material_Type,Material_Name,Material_Specification,Material_Unit,Material_Supplier from (
                        select detail.id,plancontent.PurchaseBillCode,plancontent.DeptName,CONVERT(varchar(10),plancontent.PurchaseDate,120) as PurchaseDate,plancontent.user_name,detail.ProductCode,detail.CheckQuantity,detail.Quantity,detail.Memo,detail.AuditFlag,detail.price from (select PurchaseBillCode,DeptName, PurchaseDate,user_name from (select * from MMS_PurchasePlanContent where AuditFlag='True') p
inner join dbo.Base_UserInfo u on  p.operator=u.User_ID) plancontent inner join 
                        MMS_PurchasePlanDetail detail on plancontent.PurchaseBillCode=detail.PurchaseBillCode
                        where  detail.quantity-detail.checkquantity>0) plancon 
                        left join dbo.MMS_MaterialInfo material on plancon.ProductCode=material.Material_id) planconen where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "PurchaseDate", "Desc", pageIndex, pageSize, ref count);
        }

        public DataTable GetStoreDeliveryPage(StringBuilder SqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (
                        select plancon.*,material.Material_Type,Material_Name,Material_Attr01,Material_Specification,Material_Unit,Material_Supplier from (
                        select detail.id,plancontent.PurchaseBillCode,plancontent.DeptName,CONVERT(varchar(10),plancontent.PurchaseDate,120) as PurchaseDate,plancontent.user_name,detail.ProductCode,detail.CheckQuantity,detail.Quantity,detail.Memo,detail.AuditFlag,detail.price from (select PurchaseBillCode,DeptName, PurchaseDate,user_name from (select * from MMS_PurchasePlanContent where AuditFlag='True') p
inner join dbo.Base_UserInfo u on  p.operator=u.User_ID) plancontent inner join 
                        MMS_PurchasePlanDetail detail on plancontent.PurchaseBillCode=detail.PurchaseBillCode
                       ) plancon 
                        left join dbo.MMS_MaterialInfo material on plancon.ProductCode=material.Material_id) planconen where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }

        public DataTable GetStorePurchasePage(StringBuilder SqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (
                        select plancon.*,material.Material_Type,Material_Name,Material_Specification,Material_Unit,Material_Supplier from (
                        select detail.id,plancontent.PurchaseBillCode,plancontent.DeptName,CONVERT(varchar(10),plancontent.PurchaseDate,120) as PurchaseDate,plancontent.Provider,detail.ProductCode,detail.Quantity,detail.Memo,detail.AuditFlag,plancontent.Operator
from (select * from MMS_PurchaseIndentContent where paymode=2 and  Auditflag=1) plancontent 
                     inner join 
                        MMS_PurchaseIndentDetail detail on plancontent.PurchaseBillCode=detail.PurchaseBillCode) plancon 
                        left join dbo.MMS_MaterialInfo material on plancon.ProductCode=material.Material_id) planconen where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }



        public DataTable GetStoreTransation(StringBuilder SqlWhere, IList<SqlParam> IList_param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from MMS_StoreTransation where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, IList_param.ToArray<SqlParam>());
        }




        public bool Add_MaterialAllotMember(string[] pkVal, string Store_id)
        {
            bool result;
            try
            {
                StringBuilder[] sqls = new StringBuilder[pkVal.Length + 1];
                object[] objs = new object[pkVal.Length + 1];
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.Append("Delete From MMS_StoreTransation Where Store_id =@Store_id");
                SqlParam[] parm = new SqlParam[]
				{
					new SqlParam("@Store_id", Store_id)
				};
                sqls[0] = sbDelete;
                objs[0] = parm;
                int index = 1;
                for (int i = 0; i < pkVal.Length; i++)
                {
                    string item = pkVal[i];
                    if (item.Length > 0)
                    {
                        StringBuilder sbadd = new StringBuilder();
                        sbadd.Append("Insert into MMS_StoreTransation(");
                        sbadd.Append("UserRole_ID,User_ID,Roles_ID,CreateUserId,CreateUserName");
                        sbadd.Append(")Values(");
                        sbadd.Append("@UserRole_ID,@User_ID,@Material_ID,@CreateUserId,@CreateUserName)");
                        SqlParam[] parmAdd = new SqlParam[]
						{
							new SqlParam("@UserRole_ID", CommonHelper.GetGuid),
							new SqlParam("@User_ID", item),
							new SqlParam("@Store_id", Store_id),
							new SqlParam("@CreateUserId", RequestSession.GetSessionUser().UserId),
							new SqlParam("@CreateUserName", RequestSession.GetSessionUser().UserName)
						};
                        sqls[index] = sbadd;
                        objs[index] = parmAdd;
                        index++;
                    }
                }
                result = (DataFactory.SqlDataBase().BatchExecuteBySql(sqls, objs) >= 0);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        #endregion Method





    }
}
