using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class DeliveryInfoService : IDeliveryDetail
    {
        private static IDeliveryDetail _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IDeliveryDetail Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new DeliveryInfoService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly DeliveryInfoDao dao;


        public DeliveryInfoService()
        {
            dao = new DeliveryInfoDao();

        }

        #region IPurchasePlan 成员

        /// <summary>
        ///   获得所有采购计划单列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_Delivery_Detail> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

   

        /// <summary>
        ///   插入采购计划单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_Delivery_Detail info)
        {
            
                return dao.InsertInfo(info);
            
        }

        /// <summary>
        ///   修改采购计划单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_Delivery_Detail info)
        {
            return dao.UpdateInfo(info);
        }


    
        /// <summary>
        ///   删除采购计划单
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        /// <summary>
        ///   根据主键值获取采购计划单实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public MMS_Delivery_Detail GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

      
    }
}