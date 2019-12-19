using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class WarehouseInfoDao
    {
        private readonly RMDataContext dc;

        public WarehouseInfoDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<Base_WarehouseInfo> GetAllInfo()
        {
            return dc.Base_WarehouseInfo.Select(itm => itm).ToList();
        }

        public int InsertInfo(Base_WarehouseInfo info)
        {
            dc.Base_WarehouseInfo.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.Base_WarehouseInfo_ID;
        }

        public bool UpdateInfo(Base_WarehouseInfo info)
        {
            var query = from house in dc.Base_WarehouseInfo
                        where house.Base_WarehouseInfo_ID == info.Base_WarehouseInfo_ID
                        select house;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(int id)
        {
            var query = from house in dc.Base_WarehouseInfo
                        where house.Base_WarehouseInfo_ID == id
                        select house;
            if (query.Count() > 0)
            {
                dc.Base_WarehouseInfo.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public Base_WarehouseInfo GetInfo(int id)
        {
            return dc.Base_WarehouseInfo.Where(itm => itm.Base_WarehouseInfo_ID == id).FirstOrDefault();
        }


        /// <summary>
        ///   验证仓库代码及简称是否有重复的
        /// </summary>
        /// <param name="info"> 仓库实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(Base_WarehouseInfo info)
        {
            int cnt1 =
                dc.Base_WarehouseInfo.Where(itm => itm.WareHouseCode == info.WareHouseCode && itm.Base_WarehouseInfo_ID != info.Base_WarehouseInfo_ID).Count();
            if (cnt1 > 0)
            {
                return "仓库代码重复";
            }
            int cnt2 = dc.Base_WarehouseInfo.Where(itm => itm.ShortName == info.ShortName && itm.Base_WarehouseInfo_ID != info.Base_WarehouseInfo_ID).Count();
            if (cnt2 > 0)
            {
                return "仓库简称重复";
            }
            return "";
        }

        public Base_WarehouseInfo GetWarehouseInfoByCode(string warehouseCode)
        {
            return dc.Base_WarehouseInfo.FirstOrDefault(itm => itm.WareHouseCode == warehouseCode);
        }
    }
}