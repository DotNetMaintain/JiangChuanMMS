using RM.Common.DotNetCode;
using System.Collections.Generic;
using System.Data;
using System.Text;
using RM.ServiceProvider;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;




namespace RM.Busines.DAL
{
    public interface RM_DictionaryInfo_IDAO
    {
        List<Base_DictionaryInfo> GetAllInfo();

        int InsertInfo(Base_DictionaryInfo info);

        bool UpdateInfo(Base_DictionaryInfo info);

        bool DeleteInfo(int id);

        Base_DictionaryInfo GetInfo(int id);

        Dictionary<string, string> GetListByDictType(string TypeCode);

    }
}
