using System.Collections.Generic;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Interface
{
    public interface IQuery
    {
        /// <summary>
        ///   采购计划查询
        /// </summary>
        /// <param name="sqlWhere"> </param>
        /// <returns> </returns>
        List<TPurchasePlanQuery> PurchasePlanQuery(string sqlWhere, int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   采购订单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        List<TPurchaseIndentQuery> PurchaseIndentQuery(string sqlWhere, int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   采购入库查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        List<TPurchaseQuery> PurchaseQuery(string sqlWhere, int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   采购退货查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        List<TPurchaseReturnQuery> PurchaseReturnQuery(string sqlWhere, int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   销售单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        List<TSaleQuery> SaleQuery(string sqlWhere, int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   销售退货查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        List<TSaleReturnQuery> SaleReturnQuery(string sqlWhere, int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   调拨单查询
        /// </summary>
        /// <param name="sqlWhere"> sql条件语句 </param>
        /// <returns> </returns>
        List<TAdjustQuery> AdjustQuery(string sqlWhere, int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   商品进销存统计表
        /// </summary>
        /// <param name="beginDate"> 销售及采购统计期间 </param>
        /// <param name="endDate"> 销售及采购统计期间 </param>
        /// <param name="warehouse"> 按仓库统计 </param>
        /// <returns> </returns>
        List<TProductStockTotal> ProductStockTotal(string beginDate, string endDate, string warehouse,
                                                   int CurrentPageIndex, int PageSize);

        /// <summary>
        ///   滞销库存统计
        /// </summary>
        /// <returns> </returns>
        List<TUnsalableTotal> UnsalableTotal(int CurrentPageIndex, int PageSize);
    }
}