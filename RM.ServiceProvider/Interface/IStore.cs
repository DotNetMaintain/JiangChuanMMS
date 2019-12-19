using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IStore
    {
        List<MMS_Store> GetAllInfo();

        int InsertInfo(MMS_Store info);

        bool UpdateInfo(MMS_Store info);

        bool DeleteInfo(int id);

        MMS_Store GetInfo(int id);

        /// <summary>
        ///   根据仓库,商品代码获得库存实体
        /// </summary>
        /// <param name="obj"> 只需传入仓库代码,商品代码即可 </param>
        /// <returns> 库存实体 </returns>
        MMS_Store GetStore(MMS_Store obj);

        /// <summary>
        ///   判断仓库中是否有或曾经有商品(是否被用过去时)
        /// </summary>
        /// <param name="houseCode"> </param>
        /// <returns> </returns>
        bool WarehouseHasProduct(string houseCode);

        /// <summary>
        ///   生成库存盘点表
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库,操作员 </param>
        /// <returns> 生成盘点表返回true </returns>
        bool StoreCheck(MMS_StoreCheck storeChk);

        /// <summary>
        ///   获得库存盘点表
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库 </param>
        /// <returns> </returns>
        List<MMS_StoreCheck> GetStoreCheck(MMS_StoreCheck storeChk);

        /// <summary>
        ///   获得库存盘点表
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库 </param>
        /// <returns> </returns>
        List<MMS_StoreCheck> GetStoreCheck(MMS_StoreCheck storeChk, int CurrentPageIndex, int PageSize);

        bool UpdateCheckQty(int id, int checkQty);
    }
}