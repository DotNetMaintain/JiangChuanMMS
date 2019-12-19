using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IAdjust
    {
        List<MMS_AdjustContent> GetAllInfo();

        int InsertInfo(MMS_AdjustContent info);

        bool UpdateInfo(MMS_AdjustContent info);

        bool DeleteInfo(int id);

        MMS_AdjustContent GetInfo(int id);

        /// <summary>
        ///   获得调拨实体
        /// </summary>
        /// <param name="id"> 计划单据号 </param>
        /// <returns> 自定义采购计划实体 </returns>
        TAdjust GetAdjust(int id);

        /// <summary>
        ///   保存调拨单
        /// </summary>
        /// <param name="obj"> 自定义采购计划实体 </param>
        /// <returns> 计划主表ID号 </returns>
        int SaveAdjust(TAdjust obj);

        /// <summary>
        ///   审核调拨单
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <returns> </returns>
        bool AuditAdjust(int id, bool isAudit, string operatorCode);
    }
}