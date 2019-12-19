using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class DictionaryInfoDao
    {
        private readonly RMDataContext dc;

        public DictionaryInfoDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<Base_DictionaryInfo> GetAllInfo()
        {
            return dc.Base_DictionaryInfo.Select(itm => itm).ToList();
        }

        public int InsertInfo(Base_DictionaryInfo info)
        {
            dc.Base_DictionaryInfo.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.DictionaryInfo_ID;
        }

        public bool UpdateInfo(Base_DictionaryInfo info)
        {
            var query = from item in dc.Base_DictionaryInfo
                        where item.DictionaryInfo_ID == info.DictionaryInfo_ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(int id)
        {
            var query = from item in dc.Base_DictionaryInfo
                        where item.DictionaryInfo_ID == id
                        select item;
            if (query.Count() > 0)
            {
                dc.Base_DictionaryInfo.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public Base_DictionaryInfo GetInfo(int id)
        {
            return dc.Base_DictionaryInfo.Where(itm => itm.DictionaryInfo_ID == id).FirstOrDefault();
        }


        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(Base_DictionaryInfo info)
        {
            int cnt1 =
                dc.Base_DictionaryInfo.Where(
                    itm => itm.TypeCode == info.TypeCode && itm.ValueCode == info.ValueCode && itm.DictionaryInfo_ID != info.DictionaryInfo_ID).Count();
            if (cnt1 > 0)
            {
                return "字典类型码重复";
            }
            int cnt2 =
                dc.Base_DictionaryInfo.Where(
                    itm => itm.TypeCode == info.TypeCode && itm.ValueName == info.ValueName && itm.DictionaryInfo_ID != info.DictionaryInfo_ID).Count();
            if (cnt2 > 0)
            {
                return "字典代码重复";
            }
            return "";
        }

        public List<Base_DictionaryInfo> GetListByDictType(string TypeCode)
        {
            return dc.Base_DictionaryInfo.Where(itm => itm.TypeCode == TypeCode).ToList();
        }
    }
}