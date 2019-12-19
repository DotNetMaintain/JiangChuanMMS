using RM.Common.DotNetUI;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RM.Web.MMS.StorageManagement
{
    public partial class UpdateStorage : System.Web.UI.Page
    {
        private string _key;
        protected void Page_Load(object sender, EventArgs e)
        {
            this._key = base.Request["data"];
            var str = _key.Split(',');
            if (!Page.IsPostBack)
            {
                quantity.Value = str[1];
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            //领料单明细详情
            var str = _key.Split(',');
            if (!string.IsNullOrEmpty(_key))
            {
                //领料单明细详情
                MMS_PurchasePlanDetail purPlanDetail = PurchasePlanService.Instance.GetInfoDetail(Convert.ToInt32(str[0]));
                purPlanDetail.OperatorDate = DateTime.Now;
                try
                {
                    if (purPlanDetail.CheckQuantity == 0 && string.IsNullOrEmpty(purPlanDetail.AuditFlag))
                    {
                        purPlanDetail.Quantity = Convert.ToInt32(quantity.Value);
                        //purPlanDetail.
                        if (Convert.ToInt32(quantity.Value) != Convert.ToInt32(str[1]))
                        {
                                PurchasePlanService.Instance.UpdateInfoDetail(purPlanDetail);
                                ShowMsgHelper.AlertMsg("操作成功！");
                        }
                    }
                    else
                    {
                        ShowMsgHelper.Alert_Error("未发货状态才能修改数量！");
                    }
                }
                catch (Exception ex)
                {
                    ShowMsgHelper.Alert_Error("请输入正确的数字！");
                }
            }
        }
    }
}