using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface ISaleReturn
    {
        List<MMS_SaleReturnContent> GetAllInfo();

        int InsertInfo(MMS_SaleReturnContent info);

        bool UpdateInfo(MMS_SaleReturnContent info);

        bool DeleteInfo(int id);

        MMS_SaleReturnContent GetInfo(int id);

        /// <summary>
        ///   获得销售退货实体
        /// </summary>
        /// <param name="id"> 销售退货单据号 </param>
        /// <returns> 自定义销售退货实体 </returns>
        TSaleReturn GetSaleReturn(int id);

        /// <summary>
        ///   保存销售退货
        /// </summary>
        /// <param name="obj"> 自定义销售退货实体 </param>
        /// <returns> 销售退货主表ID号 </returns>
        int SaveSaleReturn(TSaleReturn obj);

        /// <summary>
        ///   审核销售退货
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <returns> </returns>
        bool AuditSaleReturn(int id, bool isAudit, string operatorCode);
    }
}