using System;
using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class PurchaseReturnDao
    {
        private readonly RMDataContext dc;

        public PurchaseReturnDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<MMS_PurchaseReturnContent> GetAllInfo()
        {
            return dc.MMS_PurchaseReturnContent.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }

        public int InsertInfo(MMS_PurchaseReturnContent info)
        {
            dc.MMS_PurchaseReturnContent.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        public bool UpdateInfo(MMS_PurchaseReturnContent info)
        {
            var query = from item in dc.MMS_PurchaseReturnContent
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_PurchaseReturnContent
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                if (query.First().AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + query.First().PurchaseBillCode);
                }
                var qry = dc.MMS_PurchaseReturnDetail.Where(itm => itm.PurchaseBillCode == query.First().PurchaseBillCode);
                dc.MMS_PurchaseReturnDetail.DeleteAllOnSubmit(qry);
                dc.MMS_PurchaseReturnContent.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public MMS_PurchaseReturnContent GetInfo(int id)
        {
            return dc.MMS_PurchaseReturnContent.Where(itm => itm.ID == id).FirstOrDefault();
        }

        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_PurchaseReturnContent info)
        {
            int cnt1 =
                dc.MMS_PurchaseReturnContent.Where(itm => itm.PurchaseBillCode == info.PurchaseBillCode && itm.ID != info.ID)
                    .Count();
            if (cnt1 > 0)
            {
                return "代码重复";
            }
            return "";
        }

        /// <summary>
        ///   获得组合实体
        /// </summary>
        /// <param name="id"> 实体id号 </param>
        /// <returns> 自定义组合实体 </returns>
        public TPurchaseReturn GetPurchaseReturn(int id)
        {
            TPurchaseReturn resu = new TPurchaseReturn();
            resu.OprType = OperateType.otNone;
            resu.Content = dc.MMS_PurchaseReturnContent.Where(itm => itm.ID == id).First();

            List<MMS_PurchaseReturnDetail> tempList =
                dc.MMS_PurchaseReturnDetail.Where(itm => itm.PurchaseBillCode == resu.Content.PurchaseBillCode).ToList();
            foreach (MMS_PurchaseReturnDetail item in tempList)
            {
                TPurchaseReturnDetail TDetail = new TPurchaseReturnDetail();
                TDetail.OprType = OperateType.otNone;
                TDetail.DetDetail = item;
                resu.Detail.Add(TDetail);
            }
            return resu;
        }

        /// <summary>
        ///   保存组合实体
        /// </summary>
        /// <param name="obj"> 实体id号 </param>
        /// <returns> 自定义组合实体 </returns>
        public int SavePurchaseReturn(TPurchaseReturn obj)
        {
            if (obj.OprType == OperateType.otInsert)
            {
                dc.MMS_PurchaseReturnContent.InsertOnSubmit(obj.Content);
            }
            else if (obj.OprType == OperateType.otUpdate)
            {
                var query = from item in dc.MMS_PurchaseReturnContent
                            where item.ID == obj.Content.ID
                            select item;
                BatchEvaluate.Eval(obj.Content, query.First());
            }
            else if (obj.OprType == OperateType.otDelete)
            {
                dc.MMS_PurchaseReturnContent.DeleteOnSubmit(obj.Content);
            }

            foreach (TPurchaseReturnDetail item in obj.Detail)
            {
                if (item.OprType == OperateType.otInsert)
                {
                    dc.MMS_PurchaseReturnDetail.InsertOnSubmit(item.DetDetail);
                }
                else if (item.OprType == OperateType.otUpdate)
                {
                    var query = from itm in dc.MMS_PurchaseReturnDetail
                                where itm.ID == item.DetDetail.ID
                                select itm;
                    BatchEvaluate.Eval(item.DetDetail, query.First());
                }
                else if (item.OprType == OperateType.otDelete)
                {
                    dc.MMS_PurchaseReturnDetail.DeleteOnSubmit(item.DetDetail);
                }
            }
            dc.SubmitChanges();
            return obj.Content.ID;
        }
    }
}