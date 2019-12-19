using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface ISale
    {
        List<MMS_SaleContent> GetAllInfo();

        int InsertInfo(MMS_SaleContent info);

        bool UpdateInfo(MMS_SaleContent info);

        bool DeleteInfo(int id);

        MMS_SaleContent GetInfo(int id);

        /// <summary>
        ///   获得销售实体
        /// </summary>
        /// <param name="id"> 计划单据号 </param>
        /// <returns> 自定义销售实体 </returns>
        TSale GetSale(int id);

        /// <summary>
        ///   保存销售
        /// </summary>
        /// <param name="obj"> 自定义销售实体 </param>
        /// <returns> 计划主表ID号 </returns>
        int SaveSale(TSale obj);

        /// <summary>
        ///   打印销售单时调用该方法,置AuditFlag,更新库存
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <param name="operatorCode"> </param>
        /// <returns> </returns>
        bool AuditSale(int id, bool isAudit, string operatorCode);

        /// <summary>
        ///   获得该客户最后一次销售单价,如无此客户则取所有客户最后一次的,如无则取货品建议价
        /// </summary>
        /// <param name="clientCode"> </param>
        /// <param name="productCode"> </param>
        /// <returns> </returns>
        double GetLastSalePrice(string clientCode, string productCode);
    }
}