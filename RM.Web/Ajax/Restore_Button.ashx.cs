using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using RM.ServiceProvider;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Service;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;

namespace RM.Web.Ajax
{
    /// <summary>
    /// Delivery_Button 的摘要说明
    /// </summary>
    public class Restore_Button : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            
            var text = context.Request["data"];

            try
            {

                if (text != null)
                {
                    string[] str_data = text.Split(',');
                    foreach (string str in str_data)
                    {

                        string str_replace = str.Replace('\"', ' ');
                        str_replace = str_replace.Replace('[', ' ');
                        str_replace = str_replace.Replace(']', ' ');
                        str_replace = str_replace.Trim();
                        MMS_PurchasePlanDetail purPlanDetail = PurchasePlanService.Instance.GetInfoDetail(Convert.ToInt32(str_replace));
                        purPlanDetail.AuditFlag = "0";  //退货处理
                        purPlanDetail.OperatorDate = DateTime.Now;
                        //purPlanDetail.
                        PurchasePlanService.Instance.UpdateInfoDetail(purPlanDetail);
                      

                        }


                   }

          
                else
                {

                    context.Session.Abandon();
                    context.Session.Clear();
                    context.Response.Write(1);
                    context.Response.End();

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
           



        }





        public static string GetJSON<T>(object obj)
        {
            string result = String.Empty;
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}