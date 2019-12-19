using System;
using System.Collections.Generic;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class ProductInfoService : IProductInfo
    {
        private static IProductInfo _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IProductInfo Instance
        {
            get
            {
                if (_Instance == null) //判断静态变量_Instance等于空
                {
                    lock (_Lock) //锁定静态变量_Lock以保证多用户调用时排队
                    {
                        if (_Instance == null) //再次判断静态变量_Instance等于空
                        {
                            //创建货品信息类实例并赋给判断静态变量等于空_Instance
                            _Instance = new ProductInfoService();
                        }
                    }
                }
                return _Instance; //返回静态变量_Instance
            }
        }

        #endregion

        private readonly ProductInfoDao dao;

        public ProductInfoService()
        {
            dao = new ProductInfoDao(); //创建调用的数据访问层类的实例
        }

        #region IProductInfo 成员

        /// <summary>
        ///   获得所有的货品信息列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_ProductInfo> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        /// <summary>
        ///   插入货品信息
        /// </summary>
        /// <param name="info"> 货品信息实体 </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_ProductInfo info)
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

        /// <summary>
        ///   更新货品信息
        /// </summary>
        /// <param name="info"> 货品信息实体 </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_ProductInfo info)
        {
            return dao.UpdateInfo(info);
        }

        /// <summary>
        ///   删除货品信息
        /// </summary>
        /// <param name="id"> 货品信息主键 </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        /// <summary>
        ///   根据主键id获得货品信息实体
        /// </summary>
        /// <param name="id"> 货品信息主键 </param>
        /// <returns> 货品信息实体 </returns>
        public MMS_ProductInfo GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        /// <summary>
        ///   通过货品代码获得货品信息实体
        /// </summary>
        /// <param name="productCode"> 货品代码 </param>
        /// <returns> 货品信息实体 </returns>
        public MMS_ProductInfo GetProductInfoByCode(string productCode)
        {
            return dao.GetProductInfoByCode(productCode);
        }

        /// <summary>
        ///   根据货品助记码获得所有货品信息的当前页信息
        /// </summary>
        /// <param name="helpCode"> 货品助记码 </param>
        /// <param name="CurrentPageIndex"> 当前页索引 </param>
        /// <param name="PageSize"> 页尺寸 </param>
        /// <returns> 货品信息列表 </returns>
        public List<MMS_ProductInfo> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize)
        {
            return dao.GetAllInfo(helpCode, CurrentPageIndex, PageSize);
        }

        #endregion
    }
}