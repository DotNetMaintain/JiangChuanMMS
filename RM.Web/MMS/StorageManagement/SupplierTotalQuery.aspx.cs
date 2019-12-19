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
    public partial class SupplierTotalQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = ds_Query(ttbStartDate.Text.ToString().Trim(), ttbEndDate.Text.ToString().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_SupplierTotalQuery.rpt"));
            reportDoc.SetDataSource(ds.Tables[0]);
            string TEXT_OBJECT_NAME = @"BeginDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = ttbStartDate.Text.ToString().Trim();

            string TEXT_OBJECT_NAME2 = @"EndDate";
            TextObject text2;
            text2 = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME2] as TextObject;
            text2.Text = ttbEndDate.Text.ToString().Trim();



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
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataSet ds=ds_Query(ttbStartDate .Text .ToString().Trim(),ttbEndDate.Text .ToString ().Trim());
            ReportDocument reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath("Rpt_SupplierTotalQuery.rpt"));
            reportDoc.SetDataSource(ds.Tables[0]);
            string TEXT_OBJECT_NAME = @"BeginDate";
            TextObject text;
            text = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME] as TextObject;
            text.Text = ttbStartDate.Text.ToString().Trim();

            string TEXT_OBJECT_NAME2 = @"EndDate";
            TextObject text2;
            text2 = reportDoc.ReportDefinition.ReportObjects[TEXT_OBJECT_NAME2] as TextObject;
            text2.Text = ttbEndDate.Text.ToString().Trim();



            CrystalReportViewer1.ReportSource = reportDoc;
        }


        protected DataSet ds_Query(string begindate,string enddate)
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
            string str = @"select PurCon.PurchaseDate,client.ValueName as Material_Supplier,purcon.InvoiceCode as InvoiceCode,(detail.Quantity* detail.price) as Amount,MMS_MaterialInfo.Material_Attr01 as Material_Attr01 from MMS_PurchaseContent PurCon INNER JOIN  MMS_PurchaseDetail detail
                             on PurCon.PurchaseBillCode=detail.PurchaseBillCode
                            inner join dbo.MMS_MaterialInfo on detail.ProductCode=MMS_MaterialInfo.Material_ID
                            LEFT JOIN (select * from Base_DictionaryInfo where (TypeName = '客户分类')) Client on PurCon.Provider=Client.DictionaryInfo_ID
                             where 1=1";
            if (!string.IsNullOrEmpty(begindate)&& !string.IsNullOrEmpty(enddate)) {
                str += " and PurchaseDate>='{0}' and PurchaseDate<='{1}'";
                str = string.Format(str + sqlcondition, begindate, enddate);
            }
            else if(!string.IsNullOrEmpty(begindate))
            {
                str += " and PurchaseDate>='{0}'";
                str = string.Format(str + sqlcondition, begindate);
            }
            else if (!string.IsNullOrEmpty(enddate))
            {
                str += " and PurchaseDate<='{1}'";
                str = string.Format(str + sqlcondition, enddate);
            }
            else
            {
                str = string.Format(str + sqlcondition, begindate, enddate);
            }
            DataSet ds = DataFactory.SqlDataBase().GetDataSetBySQL(new StringBuilder(str));
            return ds;
        
        }



    }
}