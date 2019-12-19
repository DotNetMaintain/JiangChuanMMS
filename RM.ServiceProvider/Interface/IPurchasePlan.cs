using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IPurchasePlan
    {
        List<MMS_PurchasePlanContent> GetAllInfo();

        int InsertInfo(MMS_PurchasePlanContent info);

        bool UpdateInfo(MMS_PurchasePlanContent info);
        bool UpdateInfoDetail(MMS_PurchasePlanDetail info);

        bool DeleteInfo(int id);

        MMS_PurchasePlanContent GetInfo(int id);

        MMS_PurchasePlanDetail GetInfoDetail(int id);

        /// <summary>
        ///   自动生成采购计划
        /// </summary>
        /// <param name="oprCode"> 操作员代码 </param>
        /// <returns> </returns>
        TPurchasePlan GeneralPurchasePlan(string oprCode);

        /// <summary>
        ///   获得采购计划实体
        /// </summary>
        /// <param name="id"> 计划单据号 </param>
        /// <returns> 自定义采购计划实体 </returns>
        TPurchasePlan GetPurchasePlan(int id);


      

        /// <summary>
        ///   保存采购计划
        /// </summary>
        /// <param name="obj"> 自定义采购计划实体 </param>
        /// <returns> 计划主表ID号 </returns>
        int SavePurchasePlan(TPurchasePlan obj);

        /// <summary>
        ///   审核采购计划
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <returns> </returns>
        bool AuditPurchasePlan(int id, bool isAudit, string operatorCode);


        /// <summary>
        /// 领料单审核退回
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAudit"></param>
        /// <param name="operatorCode"></param>
        /// <returns></returns>
        bool AuditReturnPurchasePlan(int id, bool isAudit, string operatorCode);
    }
}