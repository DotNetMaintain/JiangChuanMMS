using System;
using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class PurchaseDao
    {
        private readonly RMDataContext dc;

        public PurchaseDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        /// <summary>
        ///   获得所有入库单列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_PurchaseContent> GetAllInfo()
        {
            return dc.MMS_PurchaseContent.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }


        /// <summary>
        ///   获得所有入库单列表
        /// </summary>
        /// <returns> </returns>
        public MMS_PurchaseDetail GetDetail(int id)
        {
            return dc.MMS_PurchaseDetail.Where(itm => itm.ID == id).FirstOrDefault();
        }


        public List<MMS_PurchaseDetail> GetDetailList(string  MaterialCode)
        {
            return dc.MMS_PurchaseDetail.Where(itm => itm.ProductCode == MaterialCode).ToList();
        }

        public List<MMS_PurchaseDetail> GetDetailPriceList(string MaterialCode,double Price)
        {
            return dc.MMS_PurchaseDetail.Where(itm => itm.ProductCode == MaterialCode && itm.Price == Price && itm.Quantity>itm.UseQuantity).ToList();
        }


        /// <summary>
        ///   插入入库单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_PurchaseContent info)
        {
            dc.MMS_PurchaseContent.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        /// <summary>
        ///   修改入库单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_PurchaseContent info)
        {
            var query = from item in dc.MMS_PurchaseContent
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }



        public bool UpdateInfoDetail(MMS_PurchaseDetail info)
        {
            try
            {
                var query = from item in dc.MMS_PurchaseDetail
                            where item.ID == info.ID
                            select item;

                BatchEvaluate.Eval(info, query.First());
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message );
            }
           
            return true;
        }




        /// <summary>
        ///   删除入库单
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_PurchaseContent
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                if (query.First().AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + query.First().PurchaseBillCode);
                }
                var qry = dc.MMS_PurchaseDetail.Where(itm => itm.PurchaseBillCode == query.First().PurchaseBillCode);
                dc.MMS_PurchaseDetail.DeleteAllOnSubmit(qry);
                dc.MMS_PurchaseContent.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public MMS_PurchaseContent GetInfo(int id)
        {
            return dc.MMS_PurchaseContent.Where(itm => itm.ID == id).FirstOrDefault();
        }

        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_PurchaseContent info)
        {
            int cnt1 =
                dc.MMS_PurchaseContent.Where(itm => itm.PurchaseBillCode == info.PurchaseBillCode && itm.ID != info.ID).
                    Count();
            if (cnt1 > 0)
            {
                return "入库单据号重复";
            }
            return "";
        }

        /// <summary>
        ///   根据主键值获取入库单实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public TPurchase GetPurchase(int id)
        {
            TPurchase resu = new TPurchase(); //创建入库单实体
            resu.OprType = OperateType.otNone;
            //取入库单主内容信息赋值给实体
            resu.Content = dc.MMS_PurchaseContent.Where(itm => itm.ID == id).First();
            //取入库单货品明细信息
            List<MMS_PurchaseDetail> tempList =
                dc.MMS_PurchaseDetail.Where(itm => itm.PurchaseBillCode == resu.Content.PurchaseBillCode).ToList();
            foreach (MMS_PurchaseDetail item in tempList)
            {
                TPurchaseDetail TDetail = new TPurchaseDetail();
                TDetail.OprType = OperateType.otNone;
                TDetail.DetDetail = item;
                resu.Detail.Add(TDetail); //将入库单货品添加到实体的入库货品列表
            }
            return resu;
        }

        /// <summary>
        ///   保存入库单
        /// </summary>
        /// <param name="obj"> 自定义入库单实体 </param>
        /// <returns> 入库单主表ID号 </returns>
        public int SavePurchase(TPurchase obj)
        {
            if (obj.OprType == OperateType.otInsert) //传入的是插入操作标志
            {
                dc.MMS_PurchaseContent.InsertOnSubmit(obj.Content); //执行插入操作
            }
            else if (obj.OprType == OperateType.otUpdate) //传入的是修改操作标志
            {
                var query = from item in dc.MMS_PurchaseContent
                            where item.ID == obj.Content.ID
                            select item;
                BatchEvaluate.Eval(obj.Content, query.First()); //调用Eval方法进行实体间赋值
            }
            else if (obj.OprType == OperateType.otDelete) //传入的是删除操作标志
            {
                dc.MMS_PurchaseContent.DeleteOnSubmit(obj.Content); //执行删除操作
            }

            foreach (TPurchaseDetail item in obj.Detail) //遍历采购计划货品明细
            {
                if (item.OprType == OperateType.otInsert) //传入的是插入操作标志
                {
                    dc.MMS_PurchaseDetail.InsertOnSubmit(item.DetDetail); //执行插入操作
                }
                else if (item.OprType == OperateType.otUpdate) //传入的是修改操作标志
                {
                    var query = from itm in dc.MMS_PurchaseDetail
                                where itm.ID == item.DetDetail.ID
                                select itm;
                    BatchEvaluate.Eval(item.DetDetail, query.First()); //执行修改操作
                }
                else if (item.OprType == OperateType.otDelete) //传入的是删除操作标志
                {
                    dc.MMS_PurchaseDetail.DeleteOnSubmit(item.DetDetail); //执行删除操作
                }
            }
            dc.SubmitChanges(); //最后提交操作
            return obj.Content.ID;
        }

        /// <summary>
        ///   获得货品的采购单价
        /// </summary>
        /// <param name="clientCode"> </param>
        /// <param name="productCode"> </param>
        /// <returns> </returns>
        public double GetLastPurchasePrice(string clientCode, string productCode)
        {
            var query = from content in dc.MMS_PurchaseContent
                        where content.Provider == clientCode
                        join detail in dc.MMS_PurchaseDetail
                            on content.PurchaseBillCode equals detail.PurchaseBillCode
                        where detail.ProductCode == productCode
                        select new
                            {
                                id = detail.ID,
                                price = detail.Price
                            };
            //如果有此客户该货品的采购记录
            if (string.IsNullOrEmpty(clientCode) == false && query.Count() > 0)
            {
                //获得该客户最后一次采购单价
                int maxId = query.Max(itm => itm.id);
                return dc.MMS_PurchaseDetail.Single(itm => itm.ID == maxId).Price;
            }
            else //如果无此客户该货品的采购记录
            {
                var qry = dc.MMS_PurchaseDetail.Where(itm => itm.ProductCode == productCode);
                if (qry.Count() > 0) //是否有该货品的采购记录
                {
                    int maxNum = qry.Max(itm => itm.ID); //取该货品最后一次采购单价
                    return dc.MMS_PurchaseDetail.Single(itm => itm.ID == maxNum).Price;
                }
                else //该货品未采购过
                {
                    //从商品代码表中取最低价
                    return dc.MMS_ProductInfo.First(itm => itm.ProductCode == productCode).MinimumPrice ?? 0;
                }
            }
        }
    }
}