using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IPurchase
    {
        List<MMS_PurchaseContent> GetAllInfo();

        List<MMS_PurchaseDetail> GetDetailList(string MaterialCode);

        List<MMS_PurchaseDetail> GetDetailPriceList(string MaterialCode, double Price);

        int InsertInfo(MMS_PurchaseContent info);

        bool UpdateInfo(MMS_PurchaseContent info);

        bool UpdateInfoDetail(MMS_PurchaseDetail info);

        bool DeleteInfo(int id);


        MMS_PurchaseDetail GetInfoDetail(int id);

        /// <summary>
        ///   获得采购入库实体
        /// </summary>
        /// <param name="id"> 计划单据号 </param>
        /// <returns> 自定义采购入库实体 </returns>
        TPurchase GetPurchase(int id);

        /// <summary>
        ///   保存采购入库
        /// </summary>
        /// <param name="obj"> 自定义采购入库实体 </param>
        /// <returns> 计划主表ID号 </returns>
        int SavePurchase(TPurchase obj);

        /// <summary>
        ///   审核采购入库
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <returns> </returns>
        bool AuditPurchase(int id, bool isAudit, string operatorCode);

        /// <summary>
        ///   获得该客户最后一次采购单价,如无此客户则取所有客户最后一次的,如无则取货品最低价
        /// </summary>
        /// <param name="clientCode"> </param>
        /// <param name="productCode"> </param>
        /// <returns> </returns>
        double GetLastPurchasePrice(string clientCode, string productCode);
    }
}