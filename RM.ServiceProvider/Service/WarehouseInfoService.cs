using System;
using System.Collections.Generic;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class WarehouseInfoService : IWarehouseInfo
    {
        private static IWarehouseInfo _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IWarehouseInfo Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new WarehouseInfoService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly WarehouseInfoDao dao;
        private readonly StoreDao daoStore;

        public WarehouseInfoService()
        {
            dao = new WarehouseInfoDao();
            daoStore = new StoreDao();
        }

        #region IWarehouseInfo 成员

        public List<Base_WarehouseInfo> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        public int InsertInfo(Base_WarehouseInfo info)
        {
            string msg = dao.ValidateRepeat(info);
            if (msg == "")
            {
                return dao.InsertInfo(info);
            }
            else
            {
                throw new Exception(msg);
            }
        }

        public bool UpdateInfo(Base_WarehouseInfo info)
        {
            return dao.UpdateInfo(info);
        }

        public bool DeleteInfo(int id)
        {
            Base_WarehouseInfo house = dao.GetInfo(id);
            if (daoStore.WarehouseHasProduct(house.WareHouseCode))
            {
                throw new Exception("该仓库信息已使用,不能删除");
            }
            return dao.DeleteInfo(id);
        }

        public Base_WarehouseInfo GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region IWarehouseInfo 成员

        public Base_WarehouseInfo GetWarehouseInfoByCode(string warehouseCode)
        {
            return dao.GetWarehouseInfoByCode(warehouseCode);
        }

        #endregion
    }
}