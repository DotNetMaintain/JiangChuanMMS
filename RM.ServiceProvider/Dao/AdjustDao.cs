using System;
using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class AdjustDao
    {
        private readonly RMDataContext dc;

        public AdjustDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<MMS_AdjustContent> GetAllInfo()
        {
            return dc.MMS_AdjustContent.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }

        public int InsertInfo(MMS_AdjustContent info)
        {
            dc.MMS_AdjustContent.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        public bool UpdateInfo(MMS_AdjustContent info)
        {
            var query = from item in dc.MMS_AdjustContent
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_AdjustContent
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                if (query.First().AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + query.First().AdjustBillCode);
                }
                var qry = dc.MMS_AdjustDetail.Where(itm => itm.AdjustBillCode == query.First().AdjustBillCode);
                dc.MMS_AdjustDetail.DeleteAllOnSubmit(qry);
                dc.MMS_AdjustContent.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public MMS_AdjustContent GetInfo(int id)
        {
            return dc.MMS_AdjustContent.Where(itm => itm.ID == id).FirstOrDefault();
        }

        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_AdjustContent info)
        {
            int cnt1 =
                dc.MMS_AdjustContent.Where(itm => itm.AdjustBillCode == info.AdjustBillCode && itm.ID != info.ID).Count();
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
        public TAdjust GetAdjust(int id)
        {
            TAdjust resu = new TAdjust();
            resu.OprType = OperateType.otNone;
            resu.Content = dc.MMS_AdjustContent.Where(itm => itm.ID == id).First();

            List<MMS_AdjustDetail> tempList =
                dc.MMS_AdjustDetail.Where(itm => itm.AdjustBillCode == resu.Content.AdjustBillCode).ToList();
            foreach (MMS_AdjustDetail item in tempList)
            {
                TAdjustDetail TDetail = new TAdjustDetail();
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
        public int SaveAdjust(TAdjust obj)
        {
            if (obj.OprType == OperateType.otInsert)
            {
                dc.MMS_AdjustContent.InsertOnSubmit(obj.Content);
            }
            else if (obj.OprType == OperateType.otUpdate)
            {
                var query = from item in dc.MMS_AdjustContent
                            where item.ID == obj.Content.ID
                            select item;
                BatchEvaluate.Eval(obj.Content, query.First());
            }
            else if (obj.OprType == OperateType.otDelete)
            {
                dc.MMS_AdjustContent.DeleteOnSubmit(obj.Content);
            }

            foreach (TAdjustDetail item in obj.Detail)
            {
                if (item.OprType == OperateType.otInsert)
                {
                    dc.MMS_AdjustDetail.InsertOnSubmit(item.DetDetail);
                }
                else if (item.OprType == OperateType.otUpdate)
                {
                    var query = from itm in dc.MMS_AdjustDetail
                                where itm.ID == item.DetDetail.ID
                                select itm;
                    BatchEvaluate.Eval(item.DetDetail, query.First());
                }
                else if (item.OprType == OperateType.otDelete)
                {
                    dc.MMS_AdjustDetail.DeleteOnSubmit(item.DetDetail);
                }
            }
            dc.SubmitChanges();
            return obj.Content.ID;
        }
    }
}