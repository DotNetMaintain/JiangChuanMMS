using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IProductInfo
    {
        List<MMS_ProductInfo> GetAllInfo();

        int InsertInfo(MMS_ProductInfo info);

        bool UpdateInfo(MMS_ProductInfo info);

        bool DeleteInfo(int id);

        MMS_ProductInfo GetInfo(int id);

        MMS_ProductInfo GetProductInfoByCode(string productCode);

        List<MMS_ProductInfo> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize);
    }
}