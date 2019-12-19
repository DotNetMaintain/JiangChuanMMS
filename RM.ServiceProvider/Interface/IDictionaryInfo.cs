using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;


namespace RM.ServiceProvider.Interface
{
    public interface IDictionaryInfo
    {
        List<Base_DictionaryInfo> GetAllInfo();

        int InsertInfo(Base_DictionaryInfo info);

        bool UpdateInfo(Base_DictionaryInfo info);

        bool DeleteInfo(int id);

        Base_DictionaryInfo GetInfo(int id);

        Dictionary<string, string> GetListByDictType(string TypeCode);
    }
}