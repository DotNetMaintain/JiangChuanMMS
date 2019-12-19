using System.Configuration;
using RM.Common.DotNetConfig;

namespace RM.ServiceProvider.Dao
{
    public class ConnectionManager
    {
        public static string ConnectionString
        {
            get { return ConfigHelper.GetAppSettings("SqlServer_RM_DB"); }

            //get { return ConfigurationManager.ConnectionStrings["SqlServer_RM_DB"].ToString(); }
        }
    }
}