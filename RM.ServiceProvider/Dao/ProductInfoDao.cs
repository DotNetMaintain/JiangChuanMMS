using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    /// <summary>
    ///   货品信息数据访问类
    /// </summary>
    internal class ProductInfoDao
    {
        private readonly RMDataContext dc;

        /// <summary>
        ///   构造函数，创建LINQ to SQL数据上下文类的实例
        /// </summary>
        public ProductInfoDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        /// <summary>
        ///   获得所有的货品信息列表
        /// </summary>
        /// <returns> 货品信息列表 </returns>
        public List<MMS_ProductInfo> GetAllInfo()
        {
            return dc.MMS_ProductInfo.Select(itm => itm).ToList();
        }

        /// <summary>
        ///   插入货品信息
        /// </summary>
        /// <param name="info"> 货品信息实体 </param>
        /// <returns> 返回插入记录的自增主键值 </returns>
        public int InsertInfo(MMS_ProductInfo info)
        {
            dc.MMS_ProductInfo.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        /// <summary>
        ///   更新货品信息
        /// </summary>
        /// <param name="info"> 货品信息实体 </param>
        /// <returns> 更新成功返回true </returns>
        public bool UpdateInfo(MMS_ProductInfo info)
        {
            var query = from item in dc.MMS_ProductInfo
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        /// <summary>
        ///   删除货品信息
        /// </summary>
        /// <param name="id"> 货品信息主键 </param>
        /// <returns> 删除成功返回true </returns>
        public bool DeleteInfo(int id)
        {
            var query = from item in dc.MMS_ProductInfo
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                dc.MMS_ProductInfo.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        /// <summary>
        ///   根据主键返回货品信息实体
        /// </summary>
        /// <param name="id"> 货品信息主键 </param>
        /// <returns> 货品信息实体 </returns>
        public MMS_ProductInfo GetInfo(int id)
        {
            return dc.MMS_ProductInfo.Where(itm => itm.ID == id).FirstOrDefault();
        }

        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(MMS_ProductInfo info)
        {
            //查询货品信息表中货品代码与新添加或修改的货品代码相同且主键不同(新添加的货品ID为空)的记录数
            int cnt1 = dc.MMS_ProductInfo.Where(itm => itm.ProductCode == info.ProductCode && itm.ID != info.ID).Count();
            if (cnt1 > 0) //如果记录数大于0表示货品代码重复
            {
                return "代码重复";
            }
            //查询货品信息表中货品简称与新添加或修改的货品简称相同且主键不同的记录数
            int cnt2 = dc.MMS_ProductInfo.Where(itm => itm.ShortName == info.ShortName && itm.ID != info.ID).Count();
            if (cnt2 > 0) //如果记录数大于0表示货品简称重复
            {
                return "简称重复";
            }
            return "";
        }

        /// <summary>
        ///   根据货品代码获得货品信息实体
        /// </summary>
        /// <param name="productCode"> 货品代码 </param>
        /// <returns> 货品信息实体 </returns>
        public MMS_ProductInfo GetProductInfoByCode(string productCode)
        {
            return dc.MMS_ProductInfo.Where(itm => itm.ProductCode == productCode).FirstOrDefault();
        }

        /// <summary>
        ///   根据货品助记码获得所有货品信息当前页的信息
        /// </summary>
        /// <param name="helpCode"> 货品助记码 </param>
        /// <param name="CurrentPageIndex"> 当前页索引 </param>
        /// <param name="PageSize"> 页尺寸 </param>
        /// <returns> 当前页信息 </returns>
        public List<MMS_ProductInfo> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize)
        {
            if (CurrentPageIndex > 0) //如果当前页索引大于0
            {
                if (string.IsNullOrEmpty(helpCode)) //货品信息助记码为空
                {
                    //只返回当前页的货品信息列表
                    return
                        dc.MMS_ProductInfo.Select(itm => itm).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
                }
                else
                {
                    //只返回指定助记码且当前页的货品信息列表
                    return
                        dc.MMS_ProductInfo.Where(itm => itm.HelpCode.Contains(helpCode)).Skip((CurrentPageIndex - 1)*
                                                                                          PageSize).Take(PageSize).
                            ToList();
                }
            }
            else
            {
                int cnt = 0;
                if (string.IsNullOrEmpty(helpCode)) //货品信息助记码为空
                {
                    cnt = dc.MMS_ProductInfo.Select(itm => itm).Count(); //统计所有货品信息的记录数
                }
                else
                {
                    //统计指定助记码货品信息的记录数
                    cnt = dc.MMS_ProductInfo.Where(itm => itm.HelpCode == helpCode).Count();
                }
                //返回货品信息列表(只有一行且ID成员存放统计的记录数)
                return new List<MMS_ProductInfo> {new MMS_ProductInfo {ID = cnt}};
            }
        }
    }
}