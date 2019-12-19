using System;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using RM.Busines;
using System.Text;
using System.Web.UI.WebControls;

namespace RM.Web.MMS.StorageManagement
{
    public partial class StorageQuery : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {


            DataTable dt = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), ttbInStorageID.Text.ToString().Trim(), ttbInvoiceCode.Text.ToString().Trim(), ttbMateriay_type.Text.ToString().Trim(), ttbMateriay_Name.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_StorageQuery.rpt"));
            reportDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = reportDoc;


            if (!base.IsPostBack)
            {
                init_Page();
            }
        }
        private void init_Page()
        {
            ddlMaterialType.Items.Insert(0, new ListItem("", "-1"));
            ddlMaterialType.Items.Insert(1, new ListItem("卫生材料", "0"));
            ddlMaterialType.Items.Insert(1, new ListItem("低值易耗", "1"));
            ddlMaterialType.Items.Insert(1, new ListItem("其他材料", "2"));


        }

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            DataTable dt_serach = dt_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim(), ttbInStorageID.Text.ToString().Trim(), ttbInvoiceCode.Text.ToString().Trim(), ttbMateriay_type.Text.ToString().Trim(), ttbMateriay_Name.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_StorageQuery.rpt"));
            reportDoc.SetDataSource(dt_serach);
            CrystalReportViewer1.ReportSource = reportDoc;

        }


        protected DataTable dt_Query(string startdate, string enddate, string instorage, string invoicecode, string materialtype, string materialname)
        {
            string sqlcondition = string.Empty;
            switch (ddlMaterialType.SelectedValue)
            {
                case "-1":
                    sqlcondition = sqlcondition + "";
                    break;
                case "0":
                    sqlcondition = sqlcondition + "AND Material_Attr01='0' ";
                    break;
                case "1":
                    sqlcondition = sqlcondition + "AND Material_Attr01='1' ";
                    break;
                case "2":
                    sqlcondition = sqlcondition + "AND Material_Attr01='2' ";
                    break;
                default:
                    break;
            };
            string strSql =
                @"select * from (select mmscontent.PurchaseBillCode,mmscontent.invoicecode,material.Material_Attr01,material.Material_Type,detail.ProductCode,material.Material_Name,material.Material_Specification
                    ,material.Material_Unit,Client.valuename as  Material_Supplier,detail.Lot,detail.Price,detail.Quantity,(detail.Price*detail.Quantity) amount,
                    mmscontent.PurchaseDate,mmscontent.invoicedate
                    from dbo.MMS_PurchaseContent as mmscontent
INNER JOIN (select * from Base_DictionaryInfo where (TypeName = '客户分类')) Client on mmscontent.Provider=Client.DictionaryInfo_ID
                     inner join dbo.MMS_PurchaseDetail as detail on mmscontent.PurchaseBillCode=detail.PurchaseBillCode
                     LEFT JOIN dbo.MMS_MaterialInfo material ON detail.ProductCode=material.Material_ID) content where 1=1";

            string sqlwhere = string.Empty;
            if (!string.IsNullOrEmpty(startdate))
            {
                sqlwhere = @"and PurchaseDate>='" + startdate + "'";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                sqlwhere = sqlwhere + @"and PurchaseDate<='" + enddate + "'";
            }
            if (!string.IsNullOrEmpty(instorage))
            {
                sqlwhere = sqlwhere + @"and PurchaseBillCode like '%" + instorage + "%'";
            }
            if (!string.IsNullOrEmpty(invoicecode))
            {
                sqlwhere = sqlwhere + @"and invoicecode like '%" + invoicecode + "%'";
            }
            if (!string.IsNullOrEmpty(materialtype))
            {
                sqlwhere = sqlwhere + @"and Material_Type like '%" + materialtype + "%'";
            }
            if (!string.IsNullOrEmpty(materialname))
            {
                sqlwhere = sqlwhere + @"and Material_Name like '%" + materialname + "%'";
            }

            strSql = strSql + sqlwhere+ sqlcondition;
            DataSet ds = DataFactory.SqlDataBase().GetDataSetBySQL(new StringBuilder(strSql));
            dt = ds.Tables[0];
            return dt;
        }
    }
}