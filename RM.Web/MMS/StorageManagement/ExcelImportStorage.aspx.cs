using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using RM.Web.App_Code;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using RM.Common.DotNetFile;
using RM.Common.DotNetBean;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;


namespace RM.Web.MMS.StorageManagement
{
    public partial class ExcelImportStorage : PageBase
    {
        /// <summary>
        ///   session实体
        /// </summary>
        private TPurchase _TPurchase
        {
            get
            {
                if (Session["TPurchase"] == null)
                {
                    return new TPurchase { Content = new MMS_PurchaseContent() };
                }
                else
                {
                    return (TPurchase)(Session["TPurchase"]);
                }
            }
            set { Session["TPurchase"] = value; }
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Import_Click(object sender, EventArgs e)
        {

            // 检查导入文件
            if (!CheckFile())
            {
                return;
            }
            //如果上传文件夹不存在  创建文件夹
            string sDir = System.Configuration.ConfigurationManager.AppSettings["UpLoad"].ToString();
            string downLoad = System.Configuration.ConfigurationManager.AppSettings["UpLoad"].ToString();
            ///上传路径
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }
            if (!Directory.Exists(downLoad))
            {
                Directory.CreateDirectory(downLoad);
            }
            // 获得导入文件路径  转换格式 f Full date & time  December 10, 2002 10:11 PM 
            string suffix = fileImport.PostedFile.FileName.Substring(fileImport.PostedFile.FileName.LastIndexOf(".") + 1);
            string filePath = sDir + DateTime.Now.ToFileTime().ToString("f") + "."+suffix;
            fileImport.PostedFile.SaveAs(filePath);   //获得客户端上传的内容并保存到filePath指定的路径
           

           
            //////////////显示进度/////////////////////////////////////////////////////////////////////////////
            DateTime startTime = System.DateTime.Now;
            DateTime endTime = System.DateTime.Now;

            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(1000);

            string jsBlock;
            // 处理完成
            jsBlock = "<script>BeginTrans('正在加载数据，请耐心等待...');</script>";
            Response.Write(jsBlock);
            Response.Flush();

            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);//获取准备导入文件的文件名
             suffix = fileName.Substring(fileName.LastIndexOf(".") + 1);//获取准备导入文件的后缀名

            System.Threading.Thread.Sleep(200);
                   
            int maxrows = 0;//用来记录需要加载的数据总行数
            bool err = false;//用来记录加载状态
            int errcount = 0;//用来记录加载错误行数
            
                if (suffix == "xlsx")
                {
                    DataTable dt = ExcelHelper.InputFromExcel(filePath, "Sheet1");
                    for (int i = 0; i < dt.Rows.Count; i++)
                     {
                        maxrows++;
                    }
                   
                    try
                    {
                       
                        for (int j = 0; j < dt.Rows.Count; j++)//循环向数据库中插入excel数据
                        {
                            if (string.IsNullOrEmpty(dt.Rows[j][0].ToString()))
                            {
                                jsBlock = "<script>EndTrans('第" + j.ToString() + "行数据写入错误。');</script>";
                                Response.Write(jsBlock);
                                Response.Flush();
                                err = true;
                                errcount++;
                            }
                            
                            TextBoxToModel(dt.Rows[j]);

                            System.Threading.Thread.Sleep(1000);
                            float cposf = 0;
                            cposf = 100 * (j + 1) / maxrows;
                            int cpos = (int)cposf;
                            jsBlock = "<script>SetPorgressBar('已加载到第" + (j + 1).ToString() + "条','" + cpos.ToString() + "');</script>";
                            Response.Write(jsBlock);
                            Response.Flush();
                        }
                       
                    }
                    catch (Exception ex)
                    {
                       
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + ex.Message + "');</script>");
                    }
                   
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "alert('请选择Excel文件!');", true);
                }
            
            if (!err)//加载中并没有出现错误
            {
                // 处理完成
                jsBlock = "<script>EndTrans('处理完成。');</script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            else
            {
                jsBlock = "<script>EndTrans('共有" + maxrows.ToString() + "条数据需要加载，其中 有" + errcount.ToString() + "条数据录入错误！');</script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            System.Threading.Thread.Sleep(1000);

            endTime = DateTime.Now;//录入完成所用时间
            TimeSpan ts1 = new TimeSpan(startTime.Ticks);
            TimeSpan ts2 = new TimeSpan(endTime.Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration(); //取开始时间和结束时间两个时间差的绝对值
            String spanTime = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒";
            jsBlock = "<script>SetTimeInfo('加载完成，共用时" + spanTime + "');</script>";
            Response.Write(jsBlock);
            Response.Flush();

        }




        /// <summary>
        /// 检查导入文件是否正确
        /// </summary>
        /// <returns>true:文件正确 false:文件错误</returns>
        private bool CheckFile()
        {
            string filePath = fileImport.PostedFile.FileName;
            // 导入文件是否存在
            if (filePath.Equals(""))
            {

                

               // ShowMessage("请选择导入文件。");
                return false;
            }

            // 文件类型是否为xls
            String fileExtension = Path.GetExtension(filePath).ToString().ToLower();
            if (!fileExtension.Equals(".xls") && !fileExtension.Equals(".xlsx"))
            {
               // ShowMessage("只能导入EXCEL文件。");
                return false;
            }
            return true;
        }


        private void TextBoxToModel(DataRow dr)
        {
            TPurchase info = _TPurchase;
            TPurchaseDetail tinfoDetail = new TPurchaseDetail();
            tinfoDetail.DetDetail = new MMS_PurchaseDetail();
            //TPurchaseDetail tinfoDetail = _TPurchase.Detail[0]; 
            string BillCode = dr["PurchaseBillCode"].ToString().Trim();
            int BillId=0;
            if (BillId==0)
            {

                info.Content.AuditFlag = false;
                info.Content.InvoiceCode = dr["InvoiceCode"].ToString().Trim();
                info.Content.Provider = dr["Provider"].ToString().Trim();
                info.Content.PurchaseBillCode = dr["PurchaseBillCode"].ToString();
                if (!string.IsNullOrEmpty(dr["InvoiceDate"].ToString().Trim()))
                {
                    info.Content.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString().Trim());
                }

                if (!string.IsNullOrEmpty(dr["InvoiceCode"].ToString().Trim()))
                {
                    info.Content.InvoiceCode = dr["InvoiceCode"].ToString().Trim();
                }
                info.Content.PurchaseDate = DateTime.Now;
                info.Content.CheckMan = RequestSession.GetSessionUser().UserAccount.ToString();
                info.Content.Operator = Context.User.Identity.Name;
                info.Content.OperateDate = DateTime.Now;
                info.OprType = OperateType.otInsert;

               

            }

            MMS_PurchaseDetail detail = tinfoDetail.DetDetail;
            detail.PurchaseBillCode = dr["PurchaseBillCode"].ToString(); //入库单号
            detail.ProductCode = dr["ProductCode"].ToString(); //货品代码
            detail.Lot = dr["Lot"].ToString();                //批号
            detail.ValidDate = dr["ValidDate"].ToString();    //有效期
            if (!string.IsNullOrEmpty(dr["Quantity"].ToString())) //数量
            {
                detail.Quantity = Convert.ToInt32(dr["Quantity"].ToString());
            }
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) //单价
            {
                detail.Price = Convert.ToDouble(dr["Price"].ToString());
            }

            tinfoDetail.DetDetail = detail;
            tinfoDetail.OprType = OperateType.otInsert;
            info.Detail.Add(tinfoDetail); //将操作实体添加到入库货品集合中

            int tempId = PurchaseService.Instance.SavePurchase(info);
        }


    }




    



}