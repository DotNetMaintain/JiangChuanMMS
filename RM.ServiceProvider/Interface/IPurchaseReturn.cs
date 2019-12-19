using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IPurchaseReturn
    {
        List<MMS_PurchaseReturnContent> GetAllInfo();

        int InsertInfo(MMS_PurchaseReturnContent info);

        bool UpdateInfo(MMS_PurchaseReturnContent info);

        bool DeleteInfo(int id);

        MMS_PurchaseReturnContent GetInfo(int id);

        /// <summary>
        ///   获得采购退货实体
        /// </summary>
        /// <param name="id"> 退货单据号 </param>
        /// <returns> 自定义采购退货实体 </returns>
        TPurchaseReturn GetPurchaseReturn(int id);

        /// <summary>
        ///   保存采购退货
        /// </summary>
        /// <param name="obj"> 自定义采购退货实体 </param>
        /// <returns> 退货主表ID号 </returns>
        int SavePurchaseReturn(TPurchaseReturn obj);

        /// <summary>
        ///   审核采购退货
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <returns> </returns>
        bool AuditPurchaseReturn(int id, bool isAudit, string operatorCode);
    }
}