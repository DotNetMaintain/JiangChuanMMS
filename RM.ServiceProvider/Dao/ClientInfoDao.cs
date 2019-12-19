using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class ClientInfoDao
    {
        private readonly RMDataContext dc;

        public ClientInfoDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<Base_ClientInfo> GetAllInfo()
        {
            return dc.Base_ClientInfo.Select(itm => itm).ToList();
        }

        public int InsertInfo(Base_ClientInfo info)
        {
            dc.Base_ClientInfo.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.ID;
        }

        public bool UpdateInfo(Base_ClientInfo info)
        {
            var query = from item in dc.Base_ClientInfo
                        where item.ID == info.ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(int id)
        {
            var query = from item in dc.Base_ClientInfo
                        where item.ID == id
                        select item;
            if (query.Count() > 0)
            {
                dc.Base_ClientInfo.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public Base_ClientInfo GetInfo(int id)
        {
            return dc.Base_ClientInfo.Where(itm => itm.ID == id).FirstOrDefault();
        }


        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(Base_ClientInfo info)
        {
            int cnt1 = dc.Base_ClientInfo.Where(itm => itm.ClientCode == info.ClientCode && itm.ID != info.ID).Count();
            if (cnt1 > 0)
            {
                return "代码重复";
            }
            int cnt2 = dc.Base_ClientInfo.Where(itm => itm.ShortName == info.ShortName && itm.ID != info.ID).Count();
            if (cnt2 > 0)
            {
                return "简称重复";
            }
            return "";
        }

        public Base_ClientInfo GetClientInfoByCode(string clientCode)
        {
            return dc.Base_ClientInfo.FirstOrDefault(itm => itm.ClientCode == clientCode);
        }

        public List<Base_ClientInfo> GetAllInfo(string helpCode, int CurrentPageIndex, int PageSize)
        {
            if (CurrentPageIndex > 0)
            {
                if (string.IsNullOrEmpty(helpCode))
                {
                    return
                        dc.Base_ClientInfo.Select(itm => itm).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
                }
                else
                {
                    return
                        dc.Base_ClientInfo.Where(itm => itm.HelpCode.Contains(helpCode)).Skip((CurrentPageIndex - 1)*PageSize)
                            .Take(PageSize).ToList();
                }
            }
            else
            {
                int cnt = 0;
                if (string.IsNullOrEmpty(helpCode))
                {
                    cnt = dc.Base_ClientInfo.Select(itm => itm).Count();
                }
                else
                {
                    cnt = dc.Base_ClientInfo.Where(itm => itm.HelpCode == helpCode).Count();
                }
                return new List<Base_ClientInfo> {new Base_ClientInfo {ID = cnt}};
            }
        }
    }
}