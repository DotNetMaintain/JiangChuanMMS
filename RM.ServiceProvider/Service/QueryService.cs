using System.Collections.Generic;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class QueryService : IQuery
    {
        private static IQuery _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IQuery Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new QueryService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly QueryDao dao;

        public QueryService()
        {
            dao = new QueryDao();
        }

        #region IQuery Members

        /// <summary>
        ///   采购计划查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TPurchasePlanQuery> PurchasePlanQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            return dao.PurchasePlanQuery(sqlWhere, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   采购订单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TPurchaseIndentQuery> PurchaseIndentQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            return dao.PurchaseIndentQuery(sqlWhere, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   采购入库查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TPurchaseQuery> PurchaseQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            return dao.PurchaseQuery(sqlWhere, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   采购退货查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TPurchaseReturnQuery> PurchaseReturnQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            return dao.PurchaseReturnQuery(sqlWhere, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   销售单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TSaleQuery> SaleQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            return dao.SaleQuery(sqlWhere, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   销售退货查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TSaleReturnQuery> SaleReturnQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            return dao.SaleReturnQuery(sqlWhere, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   调拨单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        public List<TAdjustQuery> AdjustQuery(string sqlWhere, int CurrentPageIndex, int PageSize)
        {
            return dao.AdjustQuery(sqlWhere, CurrentPageIndex, PageSize);
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
            return dao.ProductStockTotal(beginDate, endDate, warehouse, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   滞销库存统计
        /// </summary>
        /// <returns> </returns>
        public List<TUnsalableTotal> UnsalableTotal(int CurrentPageIndex, int PageSize)
        {
            return dao.UnsalableTotal(CurrentPageIndex, PageSize);
        }

        #endregion
    }
}