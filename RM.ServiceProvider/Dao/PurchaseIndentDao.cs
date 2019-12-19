using System;
using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class PurchaseIndentDao
    {
        private readonly RMDataContext dc;

        public PurchaseIndentDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<MMS_PurchaseIndentContent> GetAllInfo()
        {
            return dc.MMS_PurchaseIndentContent.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }

        public int InsertInfo(MMS_PurchaseIndentContent info)
        {
            dc.MMS_PurchaseIndentContent.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        public bool UpdateInfo(MMS_PurchaseIndentContent info)
        {
            var query = from item in dc.MMS_PurchaseIndentContent
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_PurchaseIndentContent
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                if (query.First().AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + query.First().PurchaseBillCode);
                }
                var qry = dc.MMS_PurchaseIndentDetail.Where(itm => itm.PurchaseBillCode == query.First().PurchaseBillCode);
                dc.MMS_PurchaseIndentDetail.DeleteAllOnSubmit(qry);
                dc.MMS_PurchaseIndentContent.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public MMS_PurchaseIndentContent GetInfo(int id)
        {
            return dc.MMS_PurchaseIndentContent.Where(itm => itm.ID == id).FirstOrDefault();
        }

        public MMS_PurchaseIndentDetail GetInfoDetail(int id)
        {
            return dc.MMS_PurchaseIndentDetail.Where(itm => itm.ID == id).FirstOrDefault();
        }



        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_PurchaseIndentContent info)
        {
            int cnt1 =
                dc.MMS_PurchaseIndentContent.Where(itm => itm.PurchaseBillCode == info.PurchaseBillCode && itm.ID != info.ID)
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
        public TPurchaseIndent GetPurchaseIndent(int id)
        {
            TPurchaseIndent resu = new TPurchaseIndent();
            resu.OprType = OperateType.otNone;
            resu.Content = dc.MMS_PurchaseIndentContent.Where(itm => itm.ID == id).First();

            List<MMS_PurchaseIndentDetail> tempList =
                dc.MMS_PurchaseIndentDetail.Where(itm => itm.PurchaseBillCode == resu.Content.PurchaseBillCode).ToList();
            foreach (MMS_PurchaseIndentDetail item in tempList)
            {
                TPurchaseIndentDetail TDetail = new TPurchaseIndentDetail();
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
        public int SavePurchaseIndent(TPurchaseIndent obj)
        {
            if (obj.OprType == OperateType.otInsert)
            {
                dc.MMS_PurchaseIndentContent.InsertOnSubmit(obj.Content);
            }
            else if (obj.OprType == OperateType.otUpdate)
            {
                var query = from item in dc.MMS_PurchaseIndentContent
                            where item.ID == obj.Content.ID
                            select item;
                BatchEvaluate.Eval(obj.Content, query.First());
            }
            else if (obj.OprType == OperateType.otDelete)
            {
                dc.MMS_PurchaseIndentContent.DeleteOnSubmit(obj.Content);
            }

            foreach (TPurchaseIndentDetail item in obj.Detail)
            {
                if (item.OprType == OperateType.otInsert)
                {
                    dc.MMS_PurchaseIndentDetail.InsertOnSubmit(item.DetDetail);
                }
                else if (item.OprType == OperateType.otUpdate)
                {
                    var query = from itm in dc.MMS_PurchaseIndentDetail
                                where itm.ID == item.DetDetail.ID
                                select itm;
                    BatchEvaluate.Eval(item.DetDetail, query.First());
                }
                else if (item.OprType == OperateType.otDelete)
                {
                    dc.MMS_PurchaseIndentDetail.DeleteOnSubmit(item.DetDetail);
                }
            }
            dc.SubmitChanges();
            return obj.Content.ID;
        }



        public bool UpdateInfoDetail(MMS_PurchaseIndentDetail info)
        {
            var query = from item in dc.MMS_PurchaseIndentDetail
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }




    }
}