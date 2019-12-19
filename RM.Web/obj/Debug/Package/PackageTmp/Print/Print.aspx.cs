using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Text;
using RM.Busines;
using RM.Busines.DAL;
using RM.Busines.DAO;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using RM.Web;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetEncrypt;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace RM.Web.Print
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            try
            {
                string mExportFileName = string.Empty;
                DataTable dt = GetPrintData(id);
                ReportDocument rptdoc_head = new ReportDocument();

                if (dt.Rows.Count > 0)
                {
                    // 蹲郎砞﹚
                    rptdoc_head.Load(Server.MapPath("") + @"\rpt\FixedAassetsInfo.rpt");
                     mExportFileName = ConfigurationSettings.AppSettings["reportPath"] + "PrintBarcodeHead_" +  "_" + Session.SessionID + ".pdf";

                    rptdoc_head.SetDataSource(dt);

                    // 砞﹚蹲隔畖の郎
                    DiskFileDestinationOptions df = new DiskFileDestinationOptions();
                    df.DiskFileName = mExportFileName;
                    rptdoc_head.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                    rptdoc_head.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                    rptdoc_head.ExportOptions.DestinationOptions = df;
                    string strIP = Request.ServerVariables["LOCAL_ADDR"].ToString();
                    string  ReportPath = "http://" + strIP + Request.ApplicationPath + "Print/pdf/PrintBarcodeHead_" + "_" + Session.SessionID + ".pdf";
                    //诀嘿
                    string strPrinter = "";
                   
                            try
                            {
                                rptdoc_head.PrintOptions.PrinterName = strPrinter;
                                rptdoc_head.PrintToPrinter(1, true, 0, 0);
                                rptdoc_head.Dispose();
                                Response.Write("<script>window.close();</script>");

                               
                            }
                            catch
                            {
                                rptdoc_head.Export();
                                rptdoc_head.Dispose();
                                Response.Write("<script>window.open('" + ReportPath + "','_blank','resizable,scrollbars=no,menubar=no,toolbar=no,location=no,status=no',false);</script> ");
                            }
                        
                       
                    
                   


                }


               
                #region 
                DateTime dtTime = DateTime.Now;
                dtTime = dtTime.AddHours(-1);
                DirectoryInfo di = new DirectoryInfo(ConfigurationSettings.AppSettings["reportPath"]);
                if (di != null)
                {
                    foreach (FileSystemInfo fsi in di.GetFiles())
                    {
                        if (fsi.CreationTime < dtTime)
                            fsi.Delete();
                    }
                }
                #endregion
            }
            catch (Exception E)
            {
                throw E;
            }

        }

        private DataTable  GetPrintData(string idlist)
        {
            DataTable dt = new DataTable();
            string strSql = @"SELECT * FROM   MMS_FixedAassets WHERE id IN ({0})";
            strSql = string.Format(strSql, idlist);
            dt = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(strSql));

            return dt;
        }


    }
}