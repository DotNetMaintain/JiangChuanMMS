using System;
using System.Collections.Generic;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class DeptInfoService : IDeptInfo
    {
        private static IDeptInfo _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IDeptInfo Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new DeptInfoService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly DeptInfoDao dao;

        public DeptInfoService()
        {
            dao = new DeptInfoDao();
        }

        #region IClientInfo 成员

        public List<Base_Organization> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        public string InsertInfo(Base_Organization info)
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

        public bool UpdateInfo(Base_Organization info)
        {
            return dao.UpdateInfo(info);
        }

        public bool DeleteInfo(string id)
        {
            return dao.DeleteInfo(id);
        }

        public Base_Organization GetInfo(string id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region IClientInfo 成员

        public Base_Organization GetClientInfoByCode(string clientCode)
        {
            return dao.GetClientInfoByCode(clientCode);
        }

        #endregion

        #region IClientInfo Members

        public List<Base_Organization> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize)
        {
            return dao.GetAllInfo(helpCode, CurrentPageIndex, PageSize);
        }

        #endregion
    }
}