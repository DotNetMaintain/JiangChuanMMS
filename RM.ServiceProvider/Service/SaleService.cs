using System;
using System.Collections.Generic;
using System.Transactions;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class SaleService : ISale
    {
        private static ISale _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static ISale Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new SaleService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly SaleDao dao;
        private readonly StoreDao daoStore;

        public SaleService()
        {
            dao = new SaleDao();
            daoStore = new StoreDao();
        }

        #region ISale 成员

        /// <summary>
        ///   获得所有销售单列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_SaleContent> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        /// <summary>
        ///   插入销售单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_SaleContent info)
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
        ///   修改销售单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_SaleContent info)
        {
            return dao.UpdateInfo(info);
        }

        /// <summary>
        ///   删除销售单
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        /// <summary>
        ///   根据主键值获取销售单主内容实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public MMS_SaleContent GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        /// <summary>
        ///   根据主键值获取销售单组合实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public TSale GetSale(int id)
        {
            return dao.GetSale(id);
        }

        /// <summary>
        ///   保存销售单
        /// </summary>
        /// <param name="obj"> 自定义销售单实体 </param>
        /// <returns> 销售单ID号 </returns>
        public int SaveSale(TSale obj)
        {
            if (obj.Content.ID != null && obj.Content.ID > 0)
            {
                MMS_SaleContent content = dao.GetInfo(obj.Content.ID);
                if (content.AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + content.SaleBillCode);
                }
            }
            return dao.SaveSale(obj);
        }

        /// <summary>
        ///   获得最后一次销售单价
        /// </summary>
        /// <param name="clientCode"> </param>
        /// <param name="productCode"> </param>
        /// <returns> </returns>
        public double GetLastSalePrice(string clientCode, string productCode)
        {
            //获得该客户最后一次销售单价,如没无此客户则取所有客户最后一次的
            return dao.GetLastSalePrice(clientCode, productCode);
        }

        /// <summary>
        ///   打印销售单
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <param name="operatorCode"> </param>
        /// <returns> </returns>
        public bool AuditSale(int id, bool isAudit, string operatorCode)
        {
            using (TransactionScope ts = new TransactionScope()) //开启DTC事务
            {
                TSale tsale = dao.GetSale(id); //取销售单组合实体
                MMS_SaleContent content = tsale.Content; //取入销售主内容实体
                if (content.AuditFlag == true) //如果销售单已审核
                {
                    throw new Exception("该单据已经审核" + content.SaleBillCode);
                }
                foreach (TSaleDetail tSaleDetail in tsale.Detail) //遍历销售单货品
                {
                    MMS_Store storeParam = new MMS_Store();
                    storeParam.Warehouse = content.WareHouse;
                    storeParam.ProductCode = tSaleDetail.DetDetail.ProductCode;
                    MMS_Store sto = null;
                    //根据仓库、货品代码取库存表中该货品信息
                    sto = daoStore.GetStore(storeParam);
                    if (sto == null) //库存中没有该商品
                    {
                        throw new Exception("库存中没有该商品,不能销售" + tSaleDetail.DetDetail.ProductCode);
                    }
                    else
                    {
                        if (tSaleDetail.DetDetail.Quantity > sto.Quantity) //如果销售数量大于库存数量
                        {
                            throw new Exception("库存数据不足,不能销售" + tSaleDetail.DetDetail.ProductCode);
                        }
                        sto.Quantity -= tSaleDetail.DetDetail.Quantity; //销售冲减数量
                        sto.Amount -= tSaleDetail.DetDetail.Quantity*sto.Price; //销售冲减金额
                        if (sto.Quantity > 0)
                            sto.Price = sto.Amount/sto.Quantity; //生新计算单价
                        daoStore.UpdateInfo(sto); //更新库存表
                    }
                    //更新成本价
                    MMS_SaleDetail saleDet = tSaleDetail.DetDetail;
                    saleDet.Cost = sto.Price;
                    dao.UpdateSaleDetail(saleDet);
                }
                content.AuditFlag = isAudit; //设销售单已审核
                content.Operator = operatorCode;
                content.OperateDate = DateTime.Now;
                dao.UpdateInfo(content);
                ts.Complete(); //提交事务
            }
            return true;
        }

        #endregion
    }
}