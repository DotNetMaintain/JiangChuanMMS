using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IClientInfo
    {
        List<Base_ClientInfo> GetAllInfo();

        int InsertInfo(Base_ClientInfo info);

        bool UpdateInfo(Base_ClientInfo info);

        bool DeleteInfo(int id);

        Base_ClientInfo GetInfo(int id);

        Base_ClientInfo GetClientInfoByCode(string clientCode);

        List<Base_ClientInfo> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize);
    }
}