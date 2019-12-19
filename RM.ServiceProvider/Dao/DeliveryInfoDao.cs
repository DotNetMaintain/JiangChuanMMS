using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class DeliveryInfoDao
    {
        private readonly RMDataContext dc;

        public DeliveryInfoDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        /// <summary>
        ///   获得所有发货单列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_Delivery_Detail> GetAllInfo()
        {
            return dc.MMS_Delivery_Detail.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }

        /// <summary>
        ///   插入发货单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_Delivery_Detail info)
        {
            dc.MMS_Delivery_Detail.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.Delivery_id;
        }

        /// <summary>
        ///   修改采购计划单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_Delivery_Detail info)
        {
            var query = from item in dc.MMS_Delivery_Detail
                        where item.Delivery_id == info.Delivery_id
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }





        /// <summary>
        ///   删除采购计划单
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_Delivery_Detail
                        where item.Delivery_id == id
                        select item;
            if (query.Count() > 0)
            {

                var qry = dc.MMS_Delivery_Detail.Where(itm => itm.Delivery_id == query.First().Delivery_id);
                dc.MMS_Delivery_Detail.DeleteAllOnSubmit(qry);
                dc.SubmitChanges();
            }
            return true;
        }

        /// <summary>
        ///   根据主键值获取出货单实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public MMS_Delivery_Detail GetInfo(int id)
        {
            return dc.MMS_Delivery_Detail.Where(itm => itm.Delivery_id == id).FirstOrDefault();
        }


    }
}
