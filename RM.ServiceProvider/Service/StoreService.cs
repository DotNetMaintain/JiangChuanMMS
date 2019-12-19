using System.Collections.Generic;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class StoreService : IStore
    {
        private static IStore _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IStore Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new StoreService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly StoreDao dao;

        public StoreService()
        {
            dao = new StoreDao();
        }

        #region IStore 成员

        /// <summary>
        ///   获得所有库存列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_Store> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        /// <summary>
        ///   插入库存信息
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_Store info)
        {
            return dao.InsertInfo(info);
        }

        /// <summary>
        ///   更新库存信息
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_Store info)
        {
            return dao.UpdateInfo(info);
        }

        /// <summary>
        ///   删除库存信息
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        /// <summary>
        ///   根据主键值获得库存信息
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public MMS_Store GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        /// <summary>
        ///   根据仓库,商品代码获得库存实体
        /// </summary>
        /// <param name="obj"> 只需传入仓库代码,商品代码即可 </param>
        /// <returns> 库存实体 </returns>
        public MMS_Store GetStore(MMS_Store obj)
        {
            return dao.GetStore(obj);
        }

        /// <summary>
        ///   判断仓库中是否有或曾经有商品(是否被使用过)
        /// </summary>
        /// <param name="houseCode"> </param>
        /// <returns> </returns>
        public bool WarehouseHasProduct(string houseCode)
        {
            return dao.WarehouseHasProduct(houseCode);
        }

        #endregion

        #region IStore Members

        /// <summary>
        ///   生成库存盘点表
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库,操作员 </param>
        /// <returns> 生成盘点表返回true </returns>
        public bool StoreCheck(MMS_StoreCheck storeChk)
        {
            return dao.StoreCheck(storeChk);
        }

        /// <summary>
        ///   获得库存盘点表
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库 </param>
        /// <returns> </returns>
        public List<MMS_StoreCheck> GetStoreCheck(MMS_StoreCheck storeChk)
        {
            return dao.GetStoreCheck(storeChk);
        }

        /// <summary>
        ///   获得库存盘点表指定页信息
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库 </param>
        /// <returns> </returns>
        public List<MMS_StoreCheck> GetStoreCheck(MMS_StoreCheck storeChk, int CurrentPageIndex, int PageSize)
        {
            return dao.GetStoreCheck(storeChk, CurrentPageIndex, PageSize);
        }

        /// <summary>
        ///   更新盘点数量
        /// </summary>
        /// <param name="id"> 盘点货品主键 </param>
        /// <param name="checkQty"> 盘点数量 </param>
        /// <returns> </returns>
        public bool UpdateCheckQty(int id, int checkQty)
        {
            return dao.UpdateCheckQty(id, checkQty);
        }

        #endregion
    }
}