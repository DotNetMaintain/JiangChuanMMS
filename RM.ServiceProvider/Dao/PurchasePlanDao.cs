using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class PurchasePlanDao
    {
        private readonly RMDataContext dc;

        public PurchasePlanDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        /// <summary>
        ///   获得所有采购计划单列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_PurchasePlanContent> GetAllInfo()
        {
            return dc.MMS_PurchasePlanContent.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }

        /// <summary>
        ///   插入采购计划单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_PurchasePlanContent info)
        {
            dc.MMS_PurchasePlanContent.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        /// <summary>
        ///   修改采购计划单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_PurchasePlanContent info)
        {
            var query = from item in dc.MMS_PurchasePlanContent
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }





        public bool UpdateInfoDetail(MMS_PurchasePlanDetail info)
        {
            var query = from item in dc.MMS_PurchasePlanDetail
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }



        /// <summary>
        ///   删除采购计划单
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_PurchasePlanContent
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                if (query.First().AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + query.First().PurchaseBillCode);
                }
                var qry = dc.MMS_PurchasePlanDetail.Where(itm => itm.PurchaseBillCode == query.First().PurchaseBillCode);
                dc.MMS_PurchasePlanDetail.DeleteAllOnSubmit(qry);
                dc.MMS_PurchasePlanContent.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        /// <summary>
        ///   根据主键值获取采购计划单实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public MMS_PurchasePlanContent GetInfo(int id)
        {
            return dc.MMS_PurchasePlanContent.Where(itm => itm.ID == id).FirstOrDefault();
        }



        public MMS_PurchasePlanDetail GetInfoDetail(int id)
        {
            return dc.MMS_PurchasePlanDetail.Where(itm => itm.ID == id).FirstOrDefault();
        }


        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_PurchasePlanContent info)
        {
            int cnt1 =
                dc.MMS_PurchasePlanContent.Where(itm => itm.PurchaseBillCode == info.PurchaseBillCode && itm.ID != info.ID).
                    Count();
            if (cnt1 > 0)
            {
                return "代码重复";
            }
            return "";
        }

        /// <summary>
        ///   根据主键值获得采购计划单组合实体
        /// </summary>
        /// <param name="id"> 实体id号 </param>
        /// <returns> 自定义组合实体 </returns>
        public TPurchasePlan GetPurchasePlan(int id)
        {
            TPurchasePlan resu = new TPurchasePlan(); //创建采购计划单实体
            resu.OprType = OperateType.otNone;
            //取采购计划主内容信息赋值给实体
            resu.Content = dc.MMS_PurchasePlanContent.Where(itm => itm.ID == id).First();
            //取采购计划货品明细信息
            List<MMS_PurchasePlanDetail> tempList =
                dc.MMS_PurchasePlanDetail.Where(itm => itm.PurchaseBillCode == resu.Content.PurchaseBillCode).ToList();
            foreach (MMS_PurchasePlanDetail item in tempList)
            {
                TPurchasePlanDetail TPlanDetail = new TPurchasePlanDetail();
                TPlanDetail.OprType = OperateType.otNone;
                TPlanDetail.DetDetail = item;
                resu.Detail.Add(TPlanDetail); //将采购计划货品添加到实体的采购货品列表            
            }
            return resu;
        }

        /// <summary>
        ///   保存采购计划单组合实体
        /// </summary>
        /// <param name="obj"> 实体id号 </param>
        /// <returns> 自定义组合实体 </returns>
        public int SavePurchasePlan(TPurchasePlan obj)
        {
            if (obj.OprType == OperateType.otInsert) //传入的是插入操作标志
            {
                dc.MMS_PurchasePlanContent.InsertOnSubmit(obj.Content); //执行插入操作
            }
            else if (obj.OprType == OperateType.otUpdate) //传入的是修改操作标志
            {
                var query = from item in dc.MMS_PurchasePlanContent
                            where item.ID == obj.Content.ID
                            select item;
                BatchEvaluate.Eval(obj.Content, query.First()); //调用Eval方法进行实体间赋值
            }
            else if (obj.OprType == OperateType.otDelete) //传入的是删除操作标志
            {
                dc.MMS_PurchasePlanContent.DeleteOnSubmit(obj.Content); //执行删除操作
            }

            foreach (TPurchasePlanDetail item in obj.Detail) //遍历采购计划货品明细
            {
                if (item.OprType == OperateType.otInsert) //传入的是插入操作标志
                {
                    dc.MMS_PurchasePlanDetail.InsertOnSubmit(item.DetDetail); //执行插入操作
                }
                else if (item.OprType == OperateType.otUpdate) //传入的是修改操作标志
                {
                    var query = from itm in dc.MMS_PurchasePlanDetail
                                where itm.ID == item.DetDetail.ID
                                select itm;
                    BatchEvaluate.Eval(item.DetDetail, query.First()); //执行修改操作
                }
                else if (item.OprType == OperateType.otDelete) //传入的是删除操作标志
                {
                    dc.MMS_PurchasePlanDetail.DeleteOnSubmit(item.DetDetail); //执行删除操作
                }
            }
            try
            {
                dc.SubmitChanges(ConflictMode.ContinueOnConflict); //最后提交操作
            }
            catch (ChangeConflictException e)
            {
                foreach (ObjectChangeConflict occ in dc.ChangeConflicts)
                {
                    occ.Resolve(RefreshMode.KeepChanges);
                }
            }
            return obj.Content.ID; //返回新添加计划单的主键值
        }
    }
}