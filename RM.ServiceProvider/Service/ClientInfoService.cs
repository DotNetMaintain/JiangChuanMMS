using System;
using System.Collections.Generic;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Service
{
    public class ClientInfoService : IClientInfo
    {
        private static IClientInfo _Instance;

        private static readonly object _Lock = new object();

        #region Sington

        /// <summary>
        ///   返回类单一实例的方法
        /// </summary>
        public static IClientInfo Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new ClientInfoService();
                        }
                    }
                }

                return _Instance;
            }
        }

        #endregion

        private readonly ClientInfoDao dao;

        public ClientInfoService()
        {
            dao = new ClientInfoDao();
        }

        #region IClientInfo 成员

        public List<Base_ClientInfo> GetAllInfo()
        {
            return dao.GetAllInfo();
        }

        public int InsertInfo(Base_ClientInfo info)
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

        public bool UpdateInfo(Base_ClientInfo info)
        {
            return dao.UpdateInfo(info);
        }

        public bool DeleteInfo(int id)
        {
            return dao.DeleteInfo(id);
        }

        public Base_ClientInfo GetInfo(int id)
        {
            return dao.GetInfo(id);
        }

        #endregion

        #region IClientInfo 成员

        public Base_ClientInfo GetClientInfoByCode(string clientCode)
        {
            return dao.GetClientInfoByCode(clientCode);
        }

        #endregion

        #region IClientInfo Members

        public List<Base_ClientInfo> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize)
        {
            return dao.GetAllInfo(helpCode, CurrentPageIndex, PageSize);
        }

        #endregion
    }
}