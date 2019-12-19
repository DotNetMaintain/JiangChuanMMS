using System;
using System.Collections.Generic;
using System.Transactions;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class AdjustService : IAdjust
    {
        private static IAdjust _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IAdjust Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new AdjustService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly AdjustDao dao;
        private readonly StoreDao daoStore;

        public AdjustService()
        {
            dao = new AdjustDao();
            daoStore = new StoreDao();
        }

        #region IAdjust 成员

        public List<MMS_AdjustContent> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        public int InsertInfo(MMS_AdjustContent info)
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

        public bool UpdateInfo(MMS_AdjustContent info)
        {
            return dao.UpdateInfo(info);
        }

        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        public MMS_AdjustContent GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region IAdjust 成员

        public TAdjust GetAdjust(int id)
        {
            return dao.GetAdjust(id);
        }

        public int SaveAdjust(TAdjust obj)
        {
            if (obj.Content.ID != null && obj.Content.ID > 0)
            {
                MMS_AdjustContent content = dao.GetInfo(obj.Content.ID);
                if (content.AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + content.AdjustBillCode);
                }
            }
            return dao.SaveAdjust(obj);
        }

        /// <summary>
        ///   打印调拨单时调用
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="isAudit"> </param>
        /// <param name="operatorCode"> </param>
        /// <returns> </returns>
        public bool AuditAdjust(int id, bool isAudit, string operatorCode)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                TAdjust tad = dao.GetAdjust(id);
                MMS_AdjustContent adContent = tad.Content;

                foreach (TAdjustDetail adDetail in tad.Detail)
                {
                    string productCode = adDetail.DetDetail.ProductCode;
                    int qty = adDetail.DetDetail.Quantity;
                    double price = adDetail.DetDetail.Price;
                    if (qty != 0)
                    {
                        MMS_Store Param1 = new MMS_Store();
                        Param1.Warehouse = adContent.SourceWareHouse;
                        Param1.ProductCode = productCode;
                        MMS_Store sourceStore = daoStore.GetStore(Param1);
                        if (sourceStore == null)
                        {
                            throw new Exception("货品在源仓库不存在");
                        }
                        if (qty > sourceStore.Quantity)
                        {
                            throw new Exception("源仓库货品数量不足");
                        }
                        sourceStore.Quantity -= qty;
                        sourceStore.Amount -= qty*price;
                        if (sourceStore.Quantity != 0)
                            sourceStore.Price = sourceStore.Amount/sourceStore.Quantity;
                        daoStore.UpdateInfo(sourceStore);

                        MMS_Store Param2 = new MMS_Store();
                        Param2.Warehouse = adContent.TargetWareHouse;
                        Param2.ProductCode = productCode;
                        MMS_Store targetStore = daoStore.GetStore(Param2);
                        if (targetStore != null)
                        {
                            targetStore.Quantity += qty;
                            targetStore.Amount += qty*price;
                            if (targetStore.Quantity != 0)
                                targetStore.Price = targetStore.Amount/targetStore.Quantity;
                            daoStore.UpdateInfo(targetStore);
                        }
                        else
                        {
                            MMS_Store newTarSto = new MMS_Store();
                            newTarSto.Warehouse = adContent.TargetWareHouse;
                            newTarSto.ProductCode = productCode;
                            newTarSto.Quantity = qty;
                            newTarSto.Price = price;
                            newTarSto.Amount = qty*price;
                            daoStore.InsertInfo(newTarSto);
                        }
                    }
                }
                adContent.AuditFlag = true;
                dao.UpdateInfo(adContent);
                ts.Complete();
            }
            return true;
        }

        #endregion
    }
}