using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IPurchaseIndent
    {
        List<MMS_PurchaseIndentContent> GetAllInfo();

        int InsertInfo(MMS_PurchaseIndentContent info);

        bool UpdateInfo(MMS_PurchaseIndentContent info);

        bool UpdateInfoDetail(MMS_PurchaseIndentDetail info);

        bool DeleteInfo(int id);

        MMS_PurchaseIndentContent GetInfo(int id);

        MMS_PurchaseIndentDetail GetInfoDetail(int id);

        /// <summary>
        ///   获得采购订单实体
        /// </summary>
        /// <param name="id"> 订单单据号 </param>
        /// <returns> 自定义采购订单实体 </returns>
        TPurchaseIndent GetPurchaseIndent(int id);

        /// <summary>
        ///   保存采购订单
        /// </summary>
        /// <param name="obj"> 自定义采购订单实体 </param>
        /// <returns> 订单主表ID号 </returns>
        int SavePurchaseIndent(TPurchaseIndent obj);

        /// <summary>
        ///   审核采购订单
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <returns> </returns>
        bool AuditPurchaseIndent(int id, bool isAudit, string operatorCode, string warehouse);
    }
}