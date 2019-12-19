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
    public   class MMS_MaterialTypeInfo_Dal : MMS_MaterialTypeInfo_IDAO
    {

       public DataTable GetMaterialTypeList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM MMS_MaterialType WHERE DeleteMark = 1 ORDER BY SortCode ASC");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }
    }
}
