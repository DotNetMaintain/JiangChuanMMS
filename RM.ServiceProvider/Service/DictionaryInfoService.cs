using System;
using System.Collections.Generic;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;


namespace RM.ServiceProvider.Service
{
    public class DictionaryInfoService : IDictionaryInfo
    {
        private static IDictionaryInfo _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IDictionaryInfo Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new DictionaryInfoService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly DictionaryInfoDao dao;

        public DictionaryInfoService()
        {
            dao = new DictionaryInfoDao();
        }

        #region IDictionaryInfo 成员

        public List<Base_DictionaryInfo> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        public int InsertInfo(Base_DictionaryInfo info)
        {
            string msg = dao.ValidateRepeat(info);
            if (msg == "")
            {
                return dao.InsertInfo(info);
            }
            else
            {
                throw new Exception(msg);
            }
        }

        public bool UpdateInfo(Base_DictionaryInfo info)
        {
            return dao.UpdateInfo(info);
        }

        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        public Base_DictionaryInfo GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region IDictionaryInfo Members

        public Dictionary<string, string> GetListByDictType(string TypeCode)
        {
            List<Base_DictionaryInfo> dictList = dao.GetListByDictType(TypeCode);
            Dictionary<string, string> resu = new Dictionary<string, string>();
            foreach (Base_DictionaryInfo dictObj in dictList)
            {
                resu.Add(dictObj.ValueCode, dictObj.ValueName);
            }
            return resu;
        }

        #endregion
    }
}