using System;
using System.Collections.Generic;
using System.Transactions;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class SaleReturnService : ISaleReturn
    {
        private static ISaleReturn _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static ISaleReturn Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new SaleReturnService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly SaleReturnDao dao;
        private readonly StoreDao daoStore;

        public SaleReturnService()
        {
            dao = new SaleReturnDao();
            daoStore = new StoreDao();
        }

        #region ISaleReturn 成员

        public List<MMS_SaleReturnContent> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        public int InsertInfo(MMS_SaleReturnContent info)
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

        public bool UpdateInfo(MMS_SaleReturnContent info)
        {
            return dao.UpdateInfo(info);
        }

        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        public MMS_SaleReturnContent GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region ISaleReturn 成员

        public TSaleReturn GetSaleReturn(int id)
        {
            return dao.GetSaleReturn(id);
        }

        public int SaveSaleReturn(TSaleReturn obj)
        {
            if (obj.Content.ID != null && obj.Content.ID > 0)
            {
                MMS_SaleReturnContent content = dao.GetInfo(obj.Content.ID);
                if (content.AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + content.SaleBillCode);
                }
            }
            return dao.SaveSaleReturn(obj);
        }

        public bool AuditSaleReturn(int id, bool isAudit, string operatorCode)
        {
            using (TransactionScope ts = new TransactionScope()) //开启DTC事务
            {
                TSaleReturn tsale = dao.GetSaleReturn(id);
                MMS_SaleReturnContent content = tsale.Content;
                if (content.AuditFlag == true) //??存在缓存
                {
                    throw new Exception("该单据已经审核" + content.SaleBillCode);
                }
                foreach (TSaleReturnDetail tSaleDetail in tsale.Detail)
                {
                    MMS_Store storeParam = new MMS_Store();
                    storeParam.Warehouse = content.WareHouse;
                    storeParam.ProductCode = tSaleDetail.DetDetail.ProductCode;
                    MMS_Store sto = null;
                    sto = daoStore.GetStore(storeParam);
                    if (sto == null) //库存中没有该商品
                    {
                        sto = new MMS_Store();
                        sto.Warehouse = tsale.Content.WareHouse;
                        sto.ProductCode = tSaleDetail.DetDetail.ProductCode;
                        sto.Quantity = tSaleDetail.DetDetail.Quantity;
                        sto.Price = tSaleDetail.DetDetail.Price;
                        sto.Amount = sto.Quantity*sto.Price;
                        sto.Memo = "";
                        daoStore.InsertInfo(sto);
                    }
                    else
                    {
                        sto.Quantity += tSaleDetail.DetDetail.Quantity;
                        sto.Amount += tSaleDetail.DetDetail.Quantity*sto.Price; //* tSaleDetail.DetDetail.Price;
                        if (sto.Quantity > 0)
                            sto.Price = sto.Amount/sto.Quantity;
                        daoStore.UpdateInfo(sto);
                    }
                    //更新销售退回成本价
                    MMS_SaleReturnDetail saleRetDet = tSaleDetail.DetDetail;
                    saleRetDet.Cost = sto.Price;
                    dao.UpdateSaleReturnDetail(saleRetDet);
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