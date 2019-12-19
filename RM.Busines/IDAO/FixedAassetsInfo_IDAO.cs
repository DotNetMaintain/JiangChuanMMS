using RM.Common.DotNetCode;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RM.Busines.DAO
{
    public  interface FixedAassetsInfo_IDAO
    {
        DataTable Load_FixedAassetsInfoList();

        DataTable GetFixedAassetsInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);

        DataTable GetFixedAassetsInfo(StringBuilder SqlWhere, IList<SqlParam> IList_param);

    }
}
