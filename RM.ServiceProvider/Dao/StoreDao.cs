using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class StoreDao
    {
        private static RMDataContext dc;

        public StoreDao()
        {
            if (dc == null)
            {
                dc = new RMDataContext(ConnectionManager.ConnectionString);
            }
        }

        /// <summary>
        ///   获得所有库存列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_Store> GetAllInfo()
        {
            return dc.MMS_Store.Select(itm => itm).ToList();
        }

        /// <summary>
        ///   插入库存信息
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_Store info)
        {
            dc.MMS_Store.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        /// <summary>
        ///   更新库存信息
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_Store info)
        {
            var query = from item in dc.MMS_Store
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        /// <summary>
        ///   删除库存信息
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_Store
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                dc.MMS_Store.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        /// <summary>
        ///   根据主键值获得库存信息
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public MMS_Store GetInfo(int id)
        {
            return dc.MMS_Store.Where(itm => itm.ID == id).FirstOrDefault();
        }

        /// <summary>
        ///   根据仓库,商品代码获得库存实体
        /// </summary>
        /// <param name="obj"> 只需传入仓库代码,商品代码即可 </param>
        /// <returns> 库存实体 </returns>
        public MMS_Store GetStore(MMS_Store obj)
        {
            return dc.MMS_Store.FirstOrDefault(itm => itm.Warehouse == obj.Warehouse && itm.ProductCode == obj.ProductCode);
        }

        /// <summary>
        ///   判断仓库中是否有或曾经有商品(是否被使用过)
        /// </summary>
        /// <param name="houseCode"> </param>
        /// <returns> </returns>
        public bool WarehouseHasProduct(string houseCode)
        {
            return dc.MMS_Store.FirstOrDefault(itm => itm.Warehouse == houseCode) != null;
        }

        /// <summary>
        ///   生成库存盘点表
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库,操作员 </param>
        /// <returns> 生成盘点表返回true </returns>
        public bool StoreCheck(MMS_StoreCheck storeChk)
        {
            //当前期间、仓库是否已经生成库存盘点表
            MMS_StoreCheck tempCheck =
                dc.MMS_StoreCheck.FirstOrDefault(
                    itm => itm.Warehouse == storeChk.Warehouse && itm.CheckPeriod == storeChk.CheckPeriod);
            if (tempCheck != null)
            {
                return false;
            }
            //将当前库存信息插入到库存盘点表
            string sql =
                "insert into StoreCheck (CheckPeriod,Warehouse,ProductCode,Quantity,CheckQty,Operator,OperateDate)" +
                " select '{0}',Warehouse,ProductCode,Quantity,Quantity,'{1}',Getdate() from MMS_Store where Warehouse = '{2}'";
            sql = string.Format(sql, storeChk.CheckPeriod, storeChk.Operator, storeChk.Warehouse);
            dc.ExecuteCommand(sql);
            return true;
        }

        /// <summary>
        ///   获得库存盘点表
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库 </param>
        /// <returns> </returns>
        public List<MMS_StoreCheck> GetStoreCheck(MMS_StoreCheck storeChk)
        {
            return
                dc.MMS_StoreCheck.Where(
                    itm => itm.CheckPeriod == storeChk.CheckPeriod && itm.Warehouse == storeChk.Warehouse).ToList();
        }

        /// <summary>
        ///   获得库存盘点表指定页信息
        /// </summary>
        /// <param name="storeChk"> 只需传入盘点期间,仓库 </param>
        /// <returns> </returns>
        public List<MMS_StoreCheck> GetStoreCheck(MMS_StoreCheck storeChk, int CurrentPageIndex, int PageSize)
        {
            if (CurrentPageIndex > 0) //如果当前页索引大于0
            {
                if (string.IsNullOrEmpty(storeChk.ProductCode)) //如果货品代码参数为空
                {
                    //返回指定期间、仓库所有货品的前当页信息
                    return
                        dc.MMS_StoreCheck.Where(
                            itm => itm.CheckPeriod == storeChk.CheckPeriod && itm.Warehouse == storeChk.Warehouse).Skip(
                                (CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
                }
                else //如果货品代码参数不为空
                {
                    //返回指定期间、仓库、指定货品的前当页信息
                    return
                        dc.MMS_StoreCheck.Where(
                            itm =>
                            itm.ProductCode == storeChk.ProductCode && itm.CheckPeriod == storeChk.CheckPeriod &&
                            itm.Warehouse == storeChk.Warehouse).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).
                            ToList();
                }
            }
            else //如果当前页索引等于-1
            {
                int cnt = 0;
                if (string.IsNullOrEmpty(storeChk.ProductCode)) //如果货品代码参数为空
                {
                    //返回指定期间、仓库所有货品的记录总数
                    cnt =
                        dc.MMS_StoreCheck.Where(
                            itm => itm.CheckPeriod == storeChk.CheckPeriod && itm.Warehouse == storeChk.Warehouse).Count
                            ();
                }
                else //如果货品代码参数不为空
                {
                    //返回指定期间、仓库、指定货品的记录总数
                    cnt =
                        dc.MMS_StoreCheck.Where(
                            itm =>
                            itm.ProductCode == storeChk.ProductCode && itm.CheckPeriod == storeChk.CheckPeriod &&
                            itm.Warehouse == storeChk.Warehouse).Count();
                }
                List<MMS_StoreCheck> resu = new List<MMS_StoreCheck>();
                resu.Add(new MMS_StoreCheck { ID = cnt });
                return resu;
            }
        }

        /// <summary>
        ///   更新盘点数量
        /// </summary>
        /// <param name="id"> 盘点货品主键 </param>
        /// <param name="checkQty"> 盘点数量 </param>
        /// <returns> </returns>
        public bool UpdateCheckQty(int id, int checkQty)
        {
            MMS_StoreCheck storeChk = dc.MMS_StoreCheck.First(itm => itm.ID == id);
            if (storeChk.CheckQty != checkQty)
            {
                storeChk.CheckQty = checkQty;
                dc.SubmitChanges();
            }
            return true;
        }
    }
}