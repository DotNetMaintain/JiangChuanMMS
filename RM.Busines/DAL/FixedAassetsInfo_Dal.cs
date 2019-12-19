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

namespace RM.Busines.DAL
{
    public  class FixedAassetsInfo_Dal : FixedAassetsInfo_IDAO
    {
        public FixedAassetsInfo_Dal()
		{}

        public DataTable Load_FixedAassetsInfoList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM dbo.MMS_FixedAassets WHERE 1 =1 ORDER BY FA_Name ASC");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }


        public DataTable GetFixedAassetsInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from MMS_FixedAassets U where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "FA_PurDate", "Desc", pageIndex, pageSize, ref count);
        }

        public DataTable GetFixedAassetsInfo(StringBuilder SqlWhere, IList<SqlParam> IList_param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from dbo.MMS_FixedAassets where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, IList_param.ToArray<SqlParam>());
        }

    }
}
