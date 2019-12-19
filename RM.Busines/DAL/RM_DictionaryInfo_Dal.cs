using RM.Busines.DAO;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetEncrypt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using RM.Common.DotNetConfig;

namespace RM.Busines.DAL
{
    partial class Base_DictionaryInfo : RM_DictionaryInfo_IDAO
    {

        private static readonly object _Lock = new object();
        private  RM_DBDataContext dc;
        private static RM_DictionaryInfo_IDAO _Instance;

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static RM_DictionaryInfo_IDAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new Base_DictionaryInfo();
                        }
                    }
                }

                return _Instance;
            }
        }


        public Base_DictionaryInfo()
        {
            dc = new RM_DBDataContext(ConfigHelper.GetAppSettings("SqlServer_RM_DB"));
        }



        public List<Base_DictionaryInfo> GetAllInfo()
        {
            return dc.Base_DictionaryInfo.Select(itm => itm).ToList();
            //return dc.Base_DictionaryInfo.Select(itm => itm).ToList();
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

        public List<Base_DictionaryInfo> GetListByDictType(string TypeCode,bool T)
        {
            return dc.Base_DictionaryInfo.Where(itm => itm.TypeCode == TypeCode).ToList();
        }



     

     

        public Dictionary<string, string> GetListByDictType(string TypeCode)
        {
            List<Base_DictionaryInfo> dictList = GetListByDictType(TypeCode,true);
            Dictionary<string, string> resu = new Dictionary<string, string>();
            foreach (Base_DictionaryInfo dictObj in dictList)
            {
                resu.Add(dictObj.ValueCode, dictObj.ValueName);
            }
            return resu;
        }

    }
}