using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using RM.Busines;

namespace RM.Web.Frame
{
    public class MainDefault : System.Web.UI.Page
    {
        protected HtmlForm form1;
        protected HtmlGenericControl htmlMenuPanel;
        protected HtmlGenericControl htmlMenuSelect;
        protected string sys_name_cn = string.Empty;
        protected string sys_name_en = string.Empty;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            Get_sys_name();
        }

        private void Get_sys_name()
        {
            StringBuilder SqlWhere = new StringBuilder();
            SqlWhere.Append("SELECT * from base_sysname where name_status=1");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(SqlWhere);
            sys_name_cn = dt.Rows[0]["name_cn"].ToString();
            sys_name_en = dt.Rows[0]["name_en"].ToString();
        }


    }
}