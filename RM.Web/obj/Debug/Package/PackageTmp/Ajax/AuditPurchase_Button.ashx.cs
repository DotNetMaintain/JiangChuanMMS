using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using RM.Busines;
using RM.Busines.DAL;
using RM.Busines.DAO;
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
    public class AuditPurchase_Button : IHttpHandler, IRequiresSessionState
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

                if (text != null && !string.IsNullOrEmpty(text.ToString()))
                {
                    string[] str_data = text.Split(',');
                    foreach (string str in str_data)
                    {

                        string str_replace = str.Replace('\"', ' ');
                        str_replace = str_replace.Replace('[', ' ');
                        str_replace = str_replace.Replace(']', ' ');
                        str_replace = str_replace.Trim();

                        //调用业务层方法取采购计划单列表

                        MMS_PurchaseIndentContent infoList = PurchaseIndentService.Instance.GetInfo(Convert.ToInt16(str_replace));

                    
                            if (infoList.PayMode == "1" || infoList.PayMode == "" || infoList.PayMode ==null)
                        {
                            PurchaseIndentService.Instance.AuditPurchaseIndent(Convert.ToInt32(str_replace), true,
                                                                          "end","");
                        }
                        else
                        {
                            PurchaseIndentService.Instance.AuditPurchaseIndent(Convert.ToInt32(str_replace), true,
                                                                          context.User.Identity.Name,"");
                        }
                       

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