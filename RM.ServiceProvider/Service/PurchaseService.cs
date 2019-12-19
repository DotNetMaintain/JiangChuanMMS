using System;
using System.Collections.Generic;
using System.Transactions;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class PurchaseService : IPurchase
    {
        private static IPurchase _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IPurchase Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new PurchaseService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly PurchaseDao dao;
        private readonly StoreDao daoStore;

        public PurchaseService()
        {
            dao = new PurchaseDao();
            daoStore = new StoreDao();
        }

        #region IPurchase 成员

        /// <summary>
        ///   获得所有入库单列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_PurchaseContent> GetAllInfo()
        {
            return dao.GetAllInfo();
        }





        public List<MMS_PurchaseDetail> GetDetailList(string MaterialCode)
        {
            return dao.GetDetailList(MaterialCode);
        }

        public List<MMS_PurchaseDetail> GetDetailPriceList(string MaterialCode, double Price)
        {
            return dao.GetDetailPriceList(MaterialCode,Price);
        }


        /// <summary>
        ///   插入入库单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_PurchaseContent info)
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
        ///   修改入库单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_PurchaseContent info)
        {
            return dao.UpdateInfo(info);
        }




        public bool UpdateInfoDetail(MMS_PurchaseDetail info)
        {
            return dao.UpdateInfoDetail(info);
        }



        /// <summary>
        ///   删除入库单
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        public MMS_PurchaseContent GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        public MMS_PurchaseDetail GetInfoDetail(int id)
        {
            return dao.GetDetail(id);
        }

        /// <summary>
        ///   根据主键值获取入库单实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public TPurchase GetPurchase(int id)
        {
            return dao.GetPurchase(id);
        }

        /// <summary>
        ///   保存入库单
        /// </summary>
        /// <param name="obj"> 自定义入库单实体 </param>
        /// <returns> 入库单主表ID号 </returns>
        public int SavePurchase(TPurchase obj)
        {
            if (obj.Content.ID.ToString() != null && obj.Content.ID > 0)
            {
                MMS_PurchaseContent content = dao.GetInfo(obj.Content.ID);
                if (content.AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + content.PurchaseBillCode);
                }
            }
            return dao.SavePurchase(obj);
        }

        /// <summary>
        ///   入库单审核确认
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <param name="operatorCode"> </param>
        /// <returns> </returns>
        public bool AuditPurchase(int id, bool isAudit, string operatorCode)
        {
            using (TransactionScope ts = new TransactionScope()) //开启DTC事务
            {
                TPurchase tPur = dao.GetPurchase(id); //取入库单组合实体
                MMS_PurchaseContent content = tPur.Content; //取入库单主内容实体
                if (content.AuditFlag == true) //如果入库单已审核
                {
                    throw new Exception("该单据已经审核" + content.PurchaseBillCode);
                }
                foreach (TPurchaseDetail tPurDetail in tPur.Detail) //遍历入库单货品
                {
                    MMS_Store storeParam = new MMS_Store();
                    storeParam.Warehouse = tPur.Content.WareHouse;
                    storeParam.ProductCode = tPurDetail.DetDetail.ProductCode;
                    MMS_Store sto = null;
                    //根据仓库、货品代码取库存表中该货品信息
                    sto = daoStore.GetStore(storeParam);
                    if (sto == null) //库存中没有该货品
                    {
                        sto = new MMS_Store(); //创建库存表实例
                        sto.Warehouse = tPur.Content.WareHouse;
                        sto.ProductCode = tPurDetail.DetDetail.ProductCode;
                        sto.Quantity = tPurDetail.DetDetail.Quantity;
                        sto.Price = tPurDetail.DetDetail.Price;
                        sto.Amount = sto.Quantity*sto.Price;
                        daoStore.InsertInfo(sto); //将该货品信息插入到库存表
                        sto.Memo = "";
                    }
                    else //库存中有该货品
                    {
                        sto.Quantity += tPurDetail.DetDetail.Quantity; //累加货品数量
                        //累加计算货品金额
                        sto.Amount += tPurDetail.DetDetail.Quantity*tPurDetail.DetDetail.Price;
                        if (sto.Quantity > 0)
                            sto.Price = sto.Amount/sto.Quantity; //根据金额、数量计算单价
                        daoStore.UpdateInfo(sto); //更新库存表
                    }
                }
                content.AuditFlag = isAudit; //入库审核标志
                content.Operator = operatorCode;
                content.OperateDate = DateTime.Now;
                dao.UpdateInfo(content); //更新入库单的审核标志
                ts.Complete(); //提交事务
            }
            return true;
        }

        /// <summary>
        ///   获得货品的采购单价
        /// </summary>
        /// <param name="clientCode"> </param>
        /// <param name="productCode"> </param>
        /// <returns> </returns>
        public double GetLastPurchasePrice(string clientCode, string productCode)
        {
            //获得该客户最后一次采购单价,如无此客户则取所有客户最后一次的,如无则取货品最低价
            return dao.GetLastPurchasePrice(clientCode, productCode);
        }

        #endregion
    }
}