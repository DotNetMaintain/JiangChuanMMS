using System.Collections.Generic;
using System.Linq;
using RM.ServiceProvider.Model;

namespace RM.ServiceProvider.Dao
{
    internal class DeptInfoDao
    {
        private readonly RMDataContext dc;

        public DeptInfoDao()
        {
            dc = new RMDataContext(ConnectionManager.ConnectionString);
        }

        public List<Base_Organization> GetAllInfo()
        {
            return dc.Base_Organization.Select(itm => itm).ToList();
        }

        public string InsertInfo(Base_Organization info)
        {
            dc.Base_Organization.InsertOnSubmit(info);
            dc.SubmitChanges();
            return info.Organization_ID;
        }

        public bool UpdateInfo(Base_Organization info)
        {
            var query = from item in dc.Base_Organization
                        where item.Organization_ID  == info.Organization_ID
                        select item;

            BatchEvaluate.Eval(info, query.First());
            dc.SubmitChanges();
            return true;
        }

        public bool DeleteInfo(string id)
        {
            var query = from item in dc.Base_Organization
                        where item.Organization_ID == id
                        select item;
            if (query.Count() > 0)
            {
                dc.Base_Organization.DeleteOnSubmit(query.First());
                dc.SubmitChanges();
            }
            return true;
        }

        public Base_Organization GetInfo(string id)
        {
            return dc.Base_Organization.Where(itm => itm.Organization_ID == id).FirstOrDefault();
        }


        /// <summary>
        ///   验证记录中是否有重复值
        /// </summary>
        /// <param name="info"> 实体 </param>
        /// <returns> 如果不重复返回"" </returns>
        public string ValidateRepeat(Base_Organization info)
        {
            int cnt1 = dc.Base_Organization.Where(itm => itm.Organization_Code == info.Organization_Code && itm.Organization_ID  != info.Organization_ID).Count();
            if (cnt1 > 0)
            {
                return "部门代码重复";
            }
            int cnt2 = dc.Base_Organization.Where(itm => itm.Organization_Name  == info.Organization_Name && itm.Organization_ID != info.Organization_ID).Count();
            if (cnt2 > 0)
            {
                return "部门名称重复";
            }
            return "";
        }

        public Base_Organization GetClientInfoByCode(string DeptCode)
        {
            return dc.Base_Organization.FirstOrDefault(itm => itm.Organization_Code == DeptCode);
        }

        public List<Base_Organization> GetAllInfo(string Organization_Name, int CurrentPageIndex, int PageSize)
        {
            if (CurrentPageIndex > 0)
            {
                if (string.IsNullOrEmpty(Organization_Name))
                {
                    return
                        dc.Base_Organization.Select(itm => itm).Skip((CurrentPageIndex - 1)*PageSize).Take(PageSize).ToList();
                }
                else
                {
                    return
                        dc.Base_Organization.Where(itm => itm.Organization_Name.Contains(Organization_Name)).Skip((CurrentPageIndex - 1) * PageSize)
                            .Take(PageSize).ToList();
                }
            }
            else
            {
                int cnt = 0;
                if (string.IsNullOrEmpty(Organization_Name))
                {
                    cnt = dc.Base_Organization.Select(itm => itm).Count();
                }
                else
                {
                    cnt = dc.Base_Organization.Where(itm => itm.Organization_Name == Organization_Name).Count();
                }
                return new List<Base_Organization> {new Base_Organization {Organization_ID = cnt.ToString()}};
            }
        }
    }
}