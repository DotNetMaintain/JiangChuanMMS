using System;
using System.Collections.Generic;
using System.Transactions;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class PurchaseReturnService : IPurchaseReturn
    {
        private static IPurchaseReturn _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IPurchaseReturn Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new PurchaseReturnService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly PurchaseReturnDao dao;
        private readonly StoreDao daoStore;

        public PurchaseReturnService()
        {
            dao = new PurchaseReturnDao();
            daoStore = new StoreDao();
        }

        #region IPurchaseReturn 成员

        public List<MMS_PurchaseReturnContent> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        public int InsertInfo(MMS_PurchaseReturnContent info)
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

        public bool UpdateInfo(MMS_PurchaseReturnContent info)
        {
            return dao.UpdateInfo(info);
        }

        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        public MMS_PurchaseReturnContent GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region IPurchaseReturn 成员

        public TPurchaseReturn GetPurchaseReturn(int id)
        {
            return dao.GetPurchaseReturn(id);
        }

        public int SavePurchaseReturn(TPurchaseReturn obj)
        {
            if (obj.Content.ID != null && obj.Content.ID > 0)
            {
                MMS_PurchaseReturnContent content = dao.GetInfo(obj.Content.ID);
                if (content.AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + content.PurchaseBillCode);
                }
            }
            return dao.SavePurchaseReturn(obj);
        }

        public bool AuditPurchaseReturn(int id, bool isAudit, string operatorCode)
        {
            using (TransactionScope ts = new TransactionScope()) //开启DTC事务
            {
                TPurchaseReturn tPur = dao.GetPurchaseReturn(id);
                MMS_PurchaseReturnContent content = tPur.Content;
                if (content.AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + content.PurchaseBillCode);
                }
                foreach (TPurchaseReturnDetail tPurDetail in tPur.Detail)
                {
                    MMS_Store storeParam = new MMS_Store();
                    storeParam.Warehouse = tPur.Content.WareHouse;
                    storeParam.ProductCode = tPurDetail.DetDetail.ProductCode;
                    MMS_Store sto = null;
                    sto = daoStore.GetStore(storeParam);
                    if (sto == null) //库存中没有该商品
                    {
                        throw new Exception("库存中没有该商品,不能退货" + tPurDetail.DetDetail.ProductCode);
                    }
                    else
                    {
                        if (tPurDetail.DetDetail.Quantity > sto.Quantity)
                        {
                            throw new Exception("库存数据不足,不能退货" + tPurDetail.DetDetail.ProductCode);
                        }
                        sto.Quantity -= tPurDetail.DetDetail.Quantity;
                        sto.Amount -= tPurDetail.DetDetail.Quantity*tPurDetail.DetDetail.Price;
                        if (sto.Quantity > 0)
                            sto.Price = sto.Amount/sto.Quantity;
                        daoStore.UpdateInfo(sto);
                    }
                }
                content.AuditFlag = isAudit;
                content.Operator = operatorCode;
                content.OperateDate = DateTime.Now;
                dao.UpdateInfo(content);
                ts.Complete();
            }
            return true;
        }

        #endregion
    }
}