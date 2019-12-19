using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IDeptInfo
    {
        List<Base_Organization> GetAllInfo();

        string InsertInfo(Base_Organization info);

        bool UpdateInfo(Base_Organization info);

        bool DeleteInfo(string id);

        Base_Organization GetInfo(string id);

        Base_Organization GetClientInfoByCode(string clientCode);

        List<Base_Organization> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize);
    }
}