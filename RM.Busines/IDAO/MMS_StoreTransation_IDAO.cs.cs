using RM.Common.DotNetCode;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RM.Busines.DAO
{
    interface MMS_StoreTransation_IDAO
    {

        DataTable Load_StoreTransationList();

        DataTable GetStoreTransationPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);

        DataTable GetStoreTransation(StringBuilder SqlWhere, IList<SqlParam> IList_param);

        DataTable GetStorePurchasePage(StringBuilder SqlWhere);

    }
}
