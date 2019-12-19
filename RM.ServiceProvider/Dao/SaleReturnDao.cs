using System;
using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class SaleReturnDao
    {
        private readonly RMDataContext dc;

        public SaleReturnDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<MMS_SaleReturnContent> GetAllInfo()
        {
            return dc.MMS_SaleReturnContent.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }

        public int InsertInfo(MMS_SaleReturnContent info)
        {
            dc.MMS_SaleReturnContent.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        public bool UpdateInfo(MMS_SaleReturnContent info)
        {
            var query = from item in dc.MMS_SaleReturnContent
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_SaleReturnContent
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                if (query.First().AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + query.First().SaleBillCode);
                }
                dc.MMS_SaleReturnContent.DeleteOnSubmit(query.First());
                var qry = dc.MMS_SaleReturnContent.Where(itm => itm.SaleBillCode == query.First().SaleBillCode);
                dc.MMS_SaleReturnContent.DeleteAllOnSubmit(qry);
                dc.SubmitChanges();
            }
            return true;
        }

        public MMS_SaleReturnContent GetInfo(int id)
        {
            return dc.MMS_SaleReturnContent.Where(itm => itm.ID == id).FirstOrDefault();
        }

        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_SaleReturnContent info)
        {
            int cnt1 =
                dc.MMS_SaleReturnContent.Where(itm => itm.SaleBillCode == info.SaleBillCode && itm.ID != info.ID).Count();
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
        public TSaleReturn GetSaleReturn(int id)
        {
            TSaleReturn resu = new TSaleReturn();
            resu.OprType = OperateType.otNone;
            resu.Content = dc.MMS_SaleReturnContent.First(itm => itm.ID == id);

            List<MMS_SaleReturnDetail> tempList =
                dc.MMS_SaleReturnDetail.Where(itm => itm.SaleBillCode == resu.Content.SaleBillCode).ToList();
            foreach (MMS_SaleReturnDetail item in tempList)
            {
                TSaleReturnDetail TDetail = new TSaleReturnDetail();
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
        public int SaveSaleReturn(TSaleReturn obj)
        {
            if (obj.OprType == OperateType.otInsert)
            {
                dc.MMS_SaleReturnContent.InsertOnSubmit(obj.Content);
            }
            else if (obj.OprType == OperateType.otUpdate)
            {
                var query = from item in dc.MMS_SaleReturnContent
                            where item.ID == obj.Content.ID
                            select item;
                BatchEvaluate.Eval(obj.Content, query.First());
            }
            else if (obj.OprType == OperateType.otDelete)
            {
                dc.MMS_SaleReturnContent.DeleteOnSubmit(obj.Content);
            }

            foreach (TSaleReturnDetail item in obj.Detail)
            {
                if (item.OprType == OperateType.otInsert)
                {
                    dc.MMS_SaleReturnDetail.InsertOnSubmit(item.DetDetail);
                }
                else if (item.OprType == OperateType.otUpdate)
                {
                    var query = from itm in dc.MMS_SaleReturnContent
                                where itm.ID == item.DetDetail.ID
                                select itm;
                    BatchEvaluate.Eval(item.DetDetail, query.First());
                }
                else if (item.OprType == OperateType.otDelete)
                {
                    dc.MMS_SaleReturnDetail.DeleteOnSubmit(item.DetDetail);
                }
            }
            dc.SubmitChanges();
            return obj.Content.ID;
        }

        public bool UpdateSaleReturnDetail(MMS_SaleReturnDetail saleRetDet)
        {
            var query = from item in dc.MMS_SaleReturnContent
                        where item.ID == saleRetDet.ID
                        select item;

            BatchEvaluate.Eval(saleRetDet, query.First());
            dc.SubmitChanges();
            return true;
        }
    }
}