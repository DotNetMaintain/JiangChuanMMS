using RM.Common.DotNetCode;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace RM.Busines.DAO
{
    public interface MMS_MaterialInfo_IDAO
    {
      
        DataTable  Load_MaterialInfoList();

        DataTable GetMaterialInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);

        DataTable GetMaterialInfo(StringBuilder SqlWhere, IList<SqlParam> IList_param);
      
    }
}
