using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IMaterialInfo
    {
        List<MMS_MaterialInfo> GetAllInfo();

        int InsertInfo(MMS_MaterialInfo info);

        bool UpdateInfo(MMS_MaterialInfo info);

        bool DeleteInfo(int id);

        MMS_MaterialInfo GetInfo(int id);

        MMS_MaterialInfo GetProductInfoByCode(string productCode);

        List<MMS_MaterialInfo> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize);
    }
}