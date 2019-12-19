using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class QueryDao
    {
        private readonly RMDataContext dc;

        public QueryDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        /// <summary>
        ///   采购计划查询
        /// </summary>
        /// <param name="sqlWhere"> 查询条件 </param>
        /// <param name="CurrentPageIndex"> 当前页索引,当小于0时查询汇总信息 </param>
        /// <param name="PageSize"> 页尺寸 </param>
        /// <returns> 返回实体列表 </returns>
        public List<TPurchasePlanQuery> PurchasePlanQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            //string sql =
            //    "select a.ID,a.PurchaseBillCode,emp.User_Name PurchaseMan,a.PurchaseDate,client.ShortName Provider, " +
            //    "b.ProductCode, product.ShortName ProductName, isnull(b.Quantity,0) Quantity,isnull(b.Price,0) Price,isnull(b.Quantity,0)*isnull(b.Price,0) Amount,a.AuditFlag " +
            //    "from MMS_PurchasePlanContent a " +
            //    "left join MMS_PurchasePlanDetail b on a.PurchaseBillCode=b.PurchaseBillCode " +
            //    "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
            //    "left join Base_UserInfo emp on a.PurchaseMan = emp.User_ID " +
            //    "left join MMS_ClientInfo client on a.Provider = client.ClientCode ";


            string sql = @"select a.ID,a.PurchaseBillCode,a.DeptName,emp.User_Name PurchaseMan,a.PurchaseDate,client.ShortName Provider,a.AuditFlag,a.PayMode  
from MMS_PurchasePlanContent a
left join Base_UserInfo emp on a.operator = emp.User_ID 
left join MMS_ClientInfo client on a.Provider = client.ClientCode";

            //string sqlSum =
            //    "select count(a.ID) ID,isnull(sum(b.Quantity),0) Quantity,isnull(avg(b.Price),0) Price,isnull(sum(isnull(b.Quantity,0)*isnull(b.Price,0)),0) Amount " +
            //    "from MMS_PurchasePlanContent a " +
            //    "left join MMS_PurchasePlanDetail b on a.PurchaseBillCode=b.PurchaseBillCode " +
            //    "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
            //    "left join Base_UserInfo emp on a.PurchaseMan = emp.User_ID " +
            //    "left join MMS_ClientInfo client on a.Provider = client.ClientCode ";

            string sqlSum = @"select count(a.ID) ID from MMS_PurchasePlanContent a 
left join Base_UserInfo emp on a.PurchaseMan = emp.User_ID 
left join MMS_ClientInfo client on a.Provider = client.ClientCode ";
            string tempWhere = "";
            tempWhere = sqlWhere;
            if (string.IsNullOrEmpty(sqlWhere) == false)
            {
                tempWhere = " where " + sqlWhere;
            }
            if (CurrentPageIndex > 0)
            {
                sql += tempWhere;
                sql = sql + " order by  PurchaseBillCode desc";
                return
                    dc.ExecuteQuery<TPurchasePlanQuery>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList
                        ();
            }
            else
            {
                sql = sqlSum + tempWhere;
                return dc.ExecuteQuery<TPurchasePlanQuery>(sql).ToList();
            }
        }

        /// <summary>
        ///   采购订单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TPurchaseIndentQuery> PurchaseIndentQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            //string sql =
            //    "select a.ID,a.PurchaseBillCode,emp.[Name] PurchaseMan,a.PurchaseDate,client.ShortName Provider," +
            //    " b.ProductCode,ProductName = product.ShortName,isnull(b.Quantity,0) Quantity, isnull(b.Price,0) Price, isnull(b.Quantity,0)*isnull(b.Price,0) Amount, a.AuditFlag " +
            //    "from MMS_PurchaseIndentContent a " +
            //    "left join MMS_PurchaseIndentDetail b on a.PurchaseBillCode=b.PurchaseBillCode " +
            //    "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
            //    "left join EmployeeInfo emp on a.PurchaseMan = emp.EmployeeCode " +
            //    "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
            //    "left join MMS_ClientInfo client on a.Provider = client.ClientCode ";

            string sql = @"select a.ID,a.PurchaseBillCode,a.DeptName,emp.User_Name PurchaseMan,a.PurchaseDate,client.ShortName Provider,a.AuditFlag 
from MMS_PurchaseIndentContent a
left join Base_UserInfo emp on a.operator = emp.User_ID 
left join MMS_ClientInfo client on a.Provider = client.ClientCode";

           

            //string sqlSum =
            //    "select count(a.ID) ID,isnull(sum(b.Quantity),0) Quantity,isnull(avg(b.Price),0) Price,isnull(sum(isnull(b.Quantity,0)*isnull(b.Price,0)),0) Amount " +
            //    "from MMS_PurchaseIndentContent a " +
            //    "left join MMS_PurchaseIndentDetail b on a.PurchaseBillCode=b.PurchaseBillCode " +
            //    "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
            //    "left join EmployeeInfo emp on a.PurchaseMan = emp.EmployeeCode " +
            //    "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
            //    "left join MMS_ClientInfo client on a.Provider = client.ClientCode ";

            string sqlSum = @"select count(a.ID) ID from MMS_PurchaseIndentContent a 
left join Base_UserInfo emp on a.PurchaseMan = emp.User_ID 
left join MMS_ClientInfo client on a.Provider = client.ClientCode ";

            string tempWhere = "";
            if (string.IsNullOrEmpty(sqlWhere) == false)
            {
                tempWhere = " where " + sqlWhere;
            }
            if (CurrentPageIndex > 0)
            {
                sql += tempWhere;
                return
                    dc.ExecuteQuery<TPurchaseIndentQuery>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).
                        ToList();
            }
            else
            {
                sql = sqlSum + tempWhere;
                return dc.ExecuteQuery<TPurchaseIndentQuery>(sql).ToList();
            }
        }

        /// <summary>
        ///   采购入库查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TPurchaseQuery> PurchaseQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            string sql =
                @"select a.ID,a.PurchaseBillCode,emp.User_Name PurchaseMan,a.PurchaseDate,client.ShortName Provider,a.AuditFlag 
from MMS_PurchaseContent a
left join Base_UserInfo emp on a.operator = emp.User_ID 
left join MMS_ClientInfo client on a.Provider = client.ClientCode  ";

            string sqlSum =
                @"select count(a.ID) ID
from MMS_PurchaseContent a
left join Base_UserInfo emp on a.operator = emp.User_ID 
left join MMS_ClientInfo client on a.Provider = client.ClientCode";
            string tempWhere = "";
            if (string.IsNullOrEmpty(sqlWhere) == false)
            {
                tempWhere = " where " + sqlWhere;
            }
            if (CurrentPageIndex > 0)
            {
                sql += tempWhere;
                sql = sql + " ORDER BY a.PurchaseDate DESC";
                return
                    dc.ExecuteQuery<TPurchaseQuery>(sql).Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                sql = sqlSum + tempWhere;
               // sql = sql + "order by PurchaseDate desc";
                return dc.ExecuteQuery<TPurchaseQuery>(sql).ToList();
            }
        }

        /// <summary>
        ///   采购退货查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TPurchaseReturnQuery> PurchaseReturnQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            string sql =
                "select a.ID,a.PurchaseBillCode, emp.[Name] PurchaseMan,a.PurchaseDate,client.ShortName Provider,house.ShortName WareHouse, " +
                "b.ProductCode, ProductName = product.ShortName,isnull(b.Quantity,0) Quantity, isnull(b.Price,0) Price,isnull(b.Quantity,0)*isnull(b.Price,0) Amount,a.AuditFlag " +
                " from MMS_PurchaseReturnContent a " +
                "left join MMS_PurchaseReturnDetail b on a.PurchaseBillCode=b.PurchaseBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.CheckMan = emp.EmployeeCode " +
                "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
                "left join MMS_ClientInfo client on a.Provider = client.ClientCode ";

            string sqlSum =
                "select count(a.ID) ID,isnull(sum(b.Quantity),0) Quantity,isnull(avg(b.Price),0) Price,isnull(sum(isnull(b.Quantity,0)*isnull(b.Price,0)),0) Amount " +
                " from MMS_PurchaseReturnContent a " +
                "left join MMS_PurchaseReturnDetail b on a.PurchaseBillCode=b.PurchaseBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.CheckMan = emp.EmployeeCode " +
                "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
                "left join MMS_ClientInfo client on a.Provider = client.ClientCode ";
            string tempWhere = "";
            if (string.IsNullOrEmpty(sqlWhere) == false)
            {
                tempWhere = " where " + sqlWhere;
            }
            if (CurrentPageIndex > 0)
            {
                sql += tempWhere;
                return
                    dc.ExecuteQuery<TPurchaseReturnQuery>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).
                        ToList();
            }
            else
            {
                sql = sqlSum + tempWhere;
                return dc.ExecuteQuery<TPurchaseReturnQuery>(sql).ToList();
            }
        }

        /// <summary>
        ///   销售单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TSaleQuery> SaleQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            string sql =
                "select a.ID,a.SaleBillCode, emp.[Name] SaleMan,a.SaleDate,client.ShortName Provider,house.ShortName WareHouse, " +
                "b.ProductCode, ProductName = product.ShortName,isnull(b.Quantity,0) Quantity,isnull(b.Price,0) Price,isnull(b.Quantity,0)*isnull(b.Price,0) Amount, isnull(b.Quantity,0)*( isnull(b.Price,0)-isnull(b.Cost,0) ) GrossProfit " +
                " from MMS_SaleContent a " +
                "left join SaleDetail b on a.SaleBillCode=b.SaleBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.SaleMan = emp.EmployeeCode " +
                "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
                "left join MMS_ClientInfo client on a.ClientCode = client.ClientCode ";

            string sqlSum =
                "select count(a.ID) ID,isnull(sum(b.Quantity),0) Quantity,isnull(avg(b.Price),0) Price,isnull(sum(isnull(b.Quantity,0)*isnull(b.Price,0)),0) Amount " +
                " from MMS_SaleContent a " +
                "left join SaleDetail b on a.SaleBillCode=b.SaleBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.SaleMan = emp.EmployeeCode " +
                "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
                "left join MMS_ClientInfo client on a.ClientCode = client.ClientCode ";
            string tempWhere = "";
            if (string.IsNullOrEmpty(sqlWhere) == false)
            {
                tempWhere = " where " + sqlWhere;
            }
            if (CurrentPageIndex > 0)
            {
                sql += tempWhere;
                return dc.ExecuteQuery<TSaleQuery>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
            }
            else
            {
                sql = sqlSum + tempWhere;
                return dc.ExecuteQuery<TSaleQuery>(sql).ToList();
            }
        }

        /// <summary>
        ///   销售退货查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TSaleReturnQuery> SaleReturnQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            string sql =
                "select a.ID,a.SaleBillCode,emp.[Name] SaleMan,a.SaleDate,client.ShortName Provider,house.ShortName WareHouse, " +
                "b.ProductCode, ProductName = product.ShortName,isnull(b.Quantity,0) Quantity,isnull(b.Price,0) Price,isnull(b.Quantity,0)*isnull(b.Price,0) Amount, a.AuditFlag " +
                " from MMS_SaleReturnContent a " +
                "left join MMS_SaleReturnContent b on a.SaleBillCode=b.SaleBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.SaleMan = emp.EmployeeCode " +
                "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
                "left join MMS_ClientInfo client on a.ClientCode = client.ClientCode ";

            string sqlSum =
                "select count(a.ID) ID,isnull(sum(b.Quantity),0) Quantity,isnull(avg(b.Price),0) Price,isnull(sum(isnull(b.Quantity,0)*isnull(b.Price,0)),0) Amount " +
                " from MMS_SaleReturnContent a " +
                "left join MMS_SaleReturnContent b on a.SaleBillCode=b.SaleBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.SaleMan = emp.EmployeeCode " +
                "left join WareHouseInfo house on a.WareHouse = house.WareHouseCode " +
                "left join MMS_ClientInfo client on a.ClientCode = client.ClientCode ";
            string tempWhere = "";
            if (string.IsNullOrEmpty(sqlWhere) == false)
            {
                tempWhere = " where " + sqlWhere;
            }
            if (CurrentPageIndex > 0)
            {
                sql += tempWhere;
                return
                    dc.ExecuteQuery<TSaleReturnQuery>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
            }
            else
            {
                sql = sqlSum + tempWhere;
                return dc.ExecuteQuery<TSaleReturnQuery>(sql).ToList();
            }
        }

        /// <summary>
        ///   调拨单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TAdjustQuery> AdjustQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            //构造查询调拨单列表的sql
            string sql =
                "select a.ID,a.AdjustBillCode, emp.[Name] AdjustMan,a.AdjustDate, shouse.ShortName SourceWarehouse,thouse.ShortName TargetWarehouse,a.Memo,  " +
                "b.ProductCode, ProductName = product.ShortName,isnull(b.Quantity,0) Quantity,isnull(b.Price,0) Price,isnull(b.Quantity,0)*isnull(b.Price,0) Amount,isnull(a.AuditFlag,0) AuditFlag " +
                "  from MMS_AdjustContent a " +
                "left join MMS_AdjustDetail b on a.AdjustBillCode=b.AdjustBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.AdjustMan = emp.EmployeeCode " +
                "left join WarehouseInfo shouse on a.SourceWarehouse = shouse.WarehouseCode " +
                "left join WarehouseInfo thouse on a.TargetWarehouse = thouse.WarehouseCode ";
            //构造查询调拨单汇总信息的sql
            string sqlSum =
                "select count(a.ID) ID,isnull(sum(b.Quantity),0) Quantity,isnull(avg(b.Price),0) Price,isnull(sum(isnull(b.Quantity,0)*isnull(b.Price,0)),0) Amount " +
                "  from MMS_AdjustContent a " +
                "left join MMS_AdjustDetail b on a.AdjustBillCode=b.AdjustBillCode " +
                "left join MMS_ProductInfo product on b.ProductCode = product.ProductCode " +
                "left join EmployeeInfo emp on a.AdjustMan = emp.EmployeeCode " +
                "left join WarehouseInfo shouse on a.SourceWarehouse = shouse.WarehouseCode " +
                "left join WarehouseInfo thouse on a.TargetWarehouse = thouse.WarehouseCode ";
            string tempWhere = "";
            if (string.IsNullOrEmpty(sqlWhere) == false) //在查询条件字符串前加where 
            {
                tempWhere = " where " + sqlWhere;
            }
            if (CurrentPageIndex > 0) //如果页索引大于0
            {
                sql += tempWhere;
                //返回调拨单指定页的信息
                return dc.ExecuteQuery<TAdjustQuery>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
            }
            else
            {
                sql = sqlSum + tempWhere;
                //返回调拨单合计信息
                return dc.ExecuteQuery<TAdjustQuery>(sql).ToList();
            }
        }

        /// <summary>
        ///   商品进销存统计表
        /// </summary>
        /// <param name="beginDate"> 销售及采购统计期间 </param>
        /// <param name="endDate"> 销售及采购统计期间 </param>
        /// <param name="warehouse"> 按仓库统计 </param>
        /// <returns> </returns>
        public List<TProductStockTotal> ProductStockTotal(string beginDate, string endDate, string warehouse,
                                                          int CurrentPageIndex, int PageSize)
        {
            string sql = " exec P_productStockTotal '{0}','{1}','{2}' ";
            sql = string.Format(sql, beginDate, endDate, warehouse);
            if (CurrentPageIndex > 0) //如果页索引大于0
            {
                //调用进销存综合统计存储过程返回指定页的统计信息
                return
                    dc.ExecuteQuery<TProductStockTotal>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList
                        ();
            }
            else
            {
                //调用存储过程返回记录总数
                int cnt = dc.ExecuteQuery<TProductStockTotal>(sql).Count();
                List<TProductStockTotal> resuList = new List<TProductStockTotal>();
                resuList.Add(new TProductStockTotal {ProductCode = cnt.ToString()});
                return resuList;
            }
        }

        /// <summary>
        ///   滞销库存统计
        /// </summary>
        /// <returns> </returns>
        public List<TUnsalableTotal> UnsalableTotal(int CurrentPageIndex, int PageSize)
        {
            string sql =
                "select b.ProductCode,b.ShortName ProductName, isnull(Quantity,0) Quantity,isnull(Price,0) Price,isnull(b.MaxStore,0) MaxQty, isnull(Quantity,0)-isnull(b.MaxStore,0) AdjustQty " +
                "  from ( select ProductCode,sum(Quantity) Quantity,max(Price) Price from MMS_Store group by ProductCode) a " +
                "left join MMS_ProductInfo b on a.ProductCode = b.ProductCode " +
                "where (MaxStore is not null and MaxStore >0) and  a.Quantity > b.MaxStore ";

            string sqlSum =
                "select convert(varchar,count(a.ProductCode)) ProductCode,isnull(sum(a.Quantity),0) Quantity,isnull(avg(a.Price),0) Price " +
                "  from ( select ProductCode,sum(Quantity) Quantity,max(Price) Price from MMS_Store group by ProductCode) a " +
                "left join MMS_ProductInfo b on a.ProductCode = b.ProductCode " +
                "where (MaxStore is not null and MaxStore >0) and  a.Quantity > b.MaxStore ";
            if (CurrentPageIndex > 0)
            {
                return
                    dc.ExecuteQuery<TUnsalableTotal>(sql).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
            }
            else
            {
                sql = sqlSum;
                return dc.ExecuteQuery<TUnsalableTotal>(sql).ToList();
            }
        }
    }
}