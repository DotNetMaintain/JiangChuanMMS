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
using System.Web.SessionState;

namespace RM.Web.Ajax
{
    /// <summary>
    /// Delivery_Button 的摘要说明
    /// </summary>
    public class Delivery_Button : IHttpHandler, IRequiresSessionState
    {

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";

            string Action = context.Request["action"];
            int OutNum=0;
            string Num = context.Request["num"];
            var text = context.Request["key"];
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
                        List<MMS_PurchaseDetail> Listpurdetail = PurchaseService.Instance.GetDetailPriceList(purPlanDetail.ProductCode,purPlanDetail.Price);
                        Listpurdetail.Select(item=>item.Quantity-item.UseQuantity>0);
                        Listpurdetail.OrderBy(item => item.PurchaseBillCode);

                        if (Num.Trim() == "")
                        {
                            OutNum = purPlanDetail.Quantity;
                        }
                        else
                        {
                         OutNum=Convert.ToInt32(Num);
                        }

                        foreach (MMS_PurchaseDetail purdetail in Listpurdetail)
                        {
                            Int32 i = 0;
                            if (purdetail.Quantity - purdetail.UseQuantity >= OutNum - purPlanDetail.CheckQuantity && OutNum - purPlanDetail.CheckQuantity > 0)
                            {

                                i = OutNum - Convert.ToInt32(purPlanDetail.CheckQuantity);
                                purdetail.UseQuantity = purdetail.UseQuantity + (OutNum - Convert.ToInt32(purPlanDetail.CheckQuantity));
                                PurchaseService.Instance.UpdateInfoDetail(purdetail);
                                purPlanDetail.CheckQuantity = OutNum;
                                PurchasePlanService.Instance.UpdateInfoDetail(purPlanDetail);
                                

                                MMS_Delivery_Detail deliverDetail = new MMS_Delivery_Detail();
                                deliverDetail.PurchaseBillCode = purPlanDetail.PurchaseBillCode;
                                deliverDetail.ProductCode = purPlanDetail.ProductCode;
                                deliverDetail.Lot = purdetail.Lot;
                                deliverDetail.Quantity = i;
                                deliverDetail.Price = purdetail.Price;
                              //  deliverDetail.Operator = RequestSession.GetSessionUser().UserAccount.ToString();
                                deliverDetail.OperatorDate = DateTime.Now;
                                DeliveryInfoService.Instance.InsertInfo(deliverDetail);

                                break;
                            }
                            else
                            {
                                if (purdetail.Quantity - purdetail.UseQuantity>0)
                                {

                                    if ((OutNum - purPlanDetail.CheckQuantity) > (purdetail.Quantity - purdetail.UseQuantity))
                                    {
                                        i = Convert.ToInt32(purdetail.Quantity) - Convert.ToInt32(purdetail.UseQuantity);
                                        purPlanDetail.CheckQuantity = Convert.ToInt32(purPlanDetail.CheckQuantity) + (Convert.ToInt32(purdetail.Quantity) - Convert.ToInt32(purdetail.UseQuantity));
                                        PurchasePlanService.Instance.UpdateInfoDetail(purPlanDetail);
                                        purdetail.UseQuantity = purdetail.Quantity;
                                        PurchaseService.Instance.UpdateInfoDetail(purdetail);
                                       
                                    }
                                                                      
                                


                                 MMS_Delivery_Detail deliverDetail = new MMS_Delivery_Detail();
                                 deliverDetail.PurchaseBillCode = purPlanDetail.PurchaseBillCode;
                                 deliverDetail.ProductCode = purPlanDetail.ProductCode;
                                 deliverDetail.Lot = purdetail.Lot;
                                 deliverDetail.Quantity = i;
                                 deliverDetail.Price = purdetail.Price;
                                // deliverDetail.Operator = HttpContext.Current.Session["UserName"].ToString();
                                 deliverDetail.OperatorDate = DateTime.Now;
                                 DeliveryInfoService.Instance.InsertInfo(deliverDetail);
                                   
                                }
                               
                            }



                            

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



        
    }
}