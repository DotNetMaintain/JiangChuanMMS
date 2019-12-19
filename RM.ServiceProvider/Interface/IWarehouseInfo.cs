using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IWarehouseInfo
    {
        List<Base_WarehouseInfo> GetAllInfo();

        int InsertInfo(Base_WarehouseInfo info);

        bool UpdateInfo(Base_WarehouseInfo info);

        bool DeleteInfo(int id);

        Base_WarehouseInfo GetInfo(int id);

        Base_WarehouseInfo GetWarehouseInfoByCode(string warehouseCode);
    }
}