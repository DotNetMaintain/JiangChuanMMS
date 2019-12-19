using System;
using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class SaleDao
    {
        private readonly RMDataContext dc;

        public SaleDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        /// <summary>
        ///   获得所有销售单列表
        /// </summary>
        /// <returns> </returns>
        public List<MMS_SaleContent> GetAllInfo()
        {
            return dc.MMS_SaleContent.Where(itm => itm.AuditFlag == false || itm.AuditFlag == null).ToList();
        }

        /// <summary>
        ///   插入销售单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public int InsertInfo(MMS_SaleContent info)
        {
            dc.MMS_SaleContent.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        /// <summary>
        ///   修改销售单
        /// </summary>
        /// <param name="info"> </param>
        /// <returns> </returns>
        public bool UpdateInfo(MMS_SaleContent info)
        {
            var query = from item in dc.MMS_SaleContent
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        /// <summary>
        ///   删除销售单
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_SaleContent
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                if (query.First().AuditFlag == true)
                {
                    throw new Exception("该单据已经审核" + query.First().SaleBillCode);
                }
                var qry = dc.MMS_SaleContent.Where(itm => itm.SaleBillCode == query.First().SaleBillCode);
                dc.MMS_SaleContent.DeleteAllOnSubmit(qry);
                dc.MMS_SaleContent.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        /// <summary>
        ///   根据主键值获取销售单主内容实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public MMS_SaleContent GetInfo(int id)
        {
            return dc.MMS_SaleContent.Where(itm => itm.ID == id).FirstOrDefault();
        }

        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_SaleContent info)
        {
            int cnt1 = dc.MMS_SaleContent.Where(itm => itm.SaleBillCode == info.SaleBillCode && itm.ID != info.ID).Count();
            if (cnt1 > 0)
            {
                return "代码重复";
            }
            return "";
        }

        /// <summary>
        ///   根据主键值获取销售单组合实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public TSale GetSale(int id)
        {
            TSale resu = new TSale();
            resu.OprType = OperateType.otNone;
            resu.Content = dc.MMS_SaleContent.Where(itm => itm.ID == id).First();

            List<MMS_SaleDetail> tempList =
                dc.MMS_SaleDetail.Where(itm => itm.SaleBillCode == resu.Content.SaleBillCode).ToList();
            foreach (MMS_SaleDetail item in tempList)
            {
                TSaleDetail TDetail = new TSaleDetail();
                TDetail.OprType = OperateType.otNone;
                TDetail.DetDetail = item;
                resu.Detail.Add(TDetail);
            }
            return resu;
        }

        /// <summary>
        ///   保存销售单
        /// </summary>
        /// <param name="obj"> 自定义销售单实体 </param>
        /// <returns> 销售单ID号 </returns>
        public int SaveSale(TSale obj)
        {
            if (obj.OprType == OperateType.otInsert) //传入的是插入操作标志
            {
                dc.MMS_SaleContent.InsertOnSubmit(obj.Content); //执行插入操作
            }
            else if (obj.OprType == OperateType.otUpdate) //传入的是修改操作标志
            {
                var query = from item in dc.MMS_SaleContent
                            where item.ID == obj.Content.ID
                            select item;
                BatchEvaluate.Eval(obj.Content, query.First()); //调用Eval方法进行实体间赋值
            }
            else if (obj.OprType == OperateType.otDelete) //传入的是删除操作标志
            {
                dc.MMS_SaleContent.DeleteOnSubmit(obj.Content); //执行删除操作
            }

            foreach (TSaleDetail item in obj.Detail) //遍历采购计划货品明细
            {
                if (item.OprType == OperateType.otInsert) //传入的是插入操作标志
                {
                    dc.MMS_SaleDetail.InsertOnSubmit(item.DetDetail); //执行插入操作
                }
                else if (item.OprType == OperateType.otUpdate) //传入的是修改操作标志
                {
                    var query = from itm in dc.MMS_SaleDetail
                                where itm.ID == item.DetDetail.ID
                                select itm;
                    BatchEvaluate.Eval(item.DetDetail, query.First()); //执行修改操作
                }
                else if (item.OprType == OperateType.otDelete) //传入的是删除操作标志
                {
                    dc.MMS_SaleDetail.DeleteOnSubmit(item.DetDetail); //执行删除操作
                }
            }
            dc.SubmitChanges(); //最后提交操作
            return obj.Content.ID;
        }

        public bool UpdateSaleDetail(MMS_SaleDetail saleDet)
        {
            var query = from item in dc.MMS_SaleDetail
                        where item.ID == saleDet.ID
                        select item;

            BatchEvaluate.Eval(saleDet, query.First());
            dc.SubmitChanges();
            return true;
        }


        /// <summary>
        ///   获得最后一次销售单价
        /// </summary>
        /// <param name="clientCode"> </param>
        /// <param name="productCode"> </param>
        /// <returns> </returns>
        public double GetLastSalePrice(string clientCode, string productCode)
        {
            var query = from content in dc.MMS_SaleContent
                        where content.ClientCode == clientCode
                        join detail in dc.MMS_SaleDetail
                            on content.SaleBillCode equals detail.SaleBillCode
                        where detail.ProductCode == productCode
                        select new
                            {
                                id = detail.ID,
                                price = detail.Price
                            };
            //该客户曾经购买过该商品
            if (string.IsNullOrEmpty(clientCode) == false && query.Count() > 0)
            {
                int maxId = query.Max(itm => itm.id);
                //取该客户最后一次购买单价
                return dc.MMS_SaleDetail.Single(itm => itm.ID == maxId).Price;
            }
            else //该客户没有购买过该商品
            {
                //取该货品最后一次销售单价
                var qry = dc.MMS_SaleDetail.Where(itm => itm.ProductCode == productCode);
                if (qry.Count() > 0)
                {
                    int maxNum = qry.Max(itm => itm.ID);
                    return dc.MMS_SaleDetail.Single(itm => itm.ID == maxNum).Price;
                }
                else //该货品没有销售过
                {
                    //取货品信息表中的建议价
                    MMS_ProductInfo prod = dc.MMS_ProductInfo.FirstOrDefault(itm => itm.ProductCode == productCode);
                    if (prod != null)
                    {
                        return prod.AdrisePrice ?? 0;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}