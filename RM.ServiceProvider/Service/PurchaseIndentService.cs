using System;
using System.Collections.Generic;
using System.Transactions;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class PurchaseIndentService : IPurchaseIndent
    {
        private static IPurchaseIndent _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IPurchaseIndent Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new PurchaseIndentService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly PurchaseIndentDao dao;
        private readonly PurchaseDao daoStock;

        public PurchaseIndentService()
        {
            dao = new PurchaseIndentDao();
            daoStock = new PurchaseDao();
        }

        #region IPurchaseIndent 成员

        public List<MMS_PurchaseIndentContent> GetAllInfo()
        {
            return dao.GetAllInfo();
        }



        public MMS_PurchaseIndentDetail  GetInfoDetail(int id)
        {
            return dao.GetInfoDetail(id);
        }




        public int InsertInfo(MMS_PurchaseIndentContent info)
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

        public bool UpdateInfo(MMS_PurchaseIndentContent info)
        {
            return dao.UpdateInfo(info);
        }


        public bool UpdateInfoDetail(MMS_PurchaseIndentDetail info)
        {
            return dao.UpdateInfoDetail(info);
        }

        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        public MMS_PurchaseIndentContent GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region IPurchaseIndent 成员

        public TPurchaseIndent GetPurchaseIndent(int id)
        {
            return dao.GetPurchaseIndent(id);
        }

        public int SavePurchaseIndent(TPurchaseIndent obj)
        {
            if (obj.Content.ID != null && obj.Content.ID > 0)
            {
                MMS_PurchaseIndentContent content = dao.GetInfo(obj.Content.ID);
                if (content.AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + content.PurchaseBillCode);
                }
            }
            return dao.SavePurchaseIndent(obj);
        }

        public bool AuditPurchaseIndent(int id, bool isAudit, string operatorCode, string warehouse)
        {
            try
            {


                TPurchaseIndent TPlan = dao.GetPurchaseIndent(id); //调用数据访问层方法取采购计划单
                if (TPlan.Content.AuditFlag == true)
                {
                    throw new Exception("该计划已生成订单,不能重复生成订单");
                }
                //if (operatorCode == "end")
                //{
                //    string[] audit = new string[2]; 
                //    //如果该采购单是两个审核人员则判断该采购单的当前用户审核完成后，该采购单明细是否全部审核完
                //    if (TPlan.Content.Provider != null && TPlan.Content.Provider == "")
                //    {
                //        audit = TPlan.Content.Provider.Split(',');
                //        if (audit.Length == 1)
                //        {
                //            TPlan.Content.AuditFlag = true;
                //            TPlan.Content.PayMode = "2";
                //        }
                //    }

                    
                //}
                //else
                //{
                //    TPlan.Content.PayMode = "2";
                //    TPlan.Content.AuditFlag = true;
                //}


                TPlan.Content.PayMode = "2";
                TPlan.Content.AuditFlag = true;


                dao.UpdateInfo(TPlan.Content);
              

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        #endregion
    }
}