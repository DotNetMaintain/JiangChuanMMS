using RM.Busines;
using RM.Busines.DAL;
using RM.Busines.DAO;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetData;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using RM.ServiceProvider;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;
using System.Collections.Generic;

namespace RM.Web.MMS.MMS_FixedAassets
{
	public partial class FixedAassetsForm : PageBase
	{
		public StringBuilder str_allUserInfo = new StringBuilder();
		public StringBuilder str_seleteUserInfo = new StringBuilder();
		private RM_UserInfo_IDAO user_idao = new RM_UserInfo_Dal();
		private RM_System_IDAO system_idao = new RM_System_Dal();
		private string _key;
        private string _FA_Type;
        private string _Status;
		private int index_TreeNode = 0;
	
		protected void Page_Load(object sender, EventArgs e)
		{
			this._key = base.Request["key"];
           
			if (!base.IsPostBack)
			{
                this.InitFATypeId();
                this.InitFAStatus();
                if (!string.IsNullOrEmpty(this._FA_Type))
                {
                    this.FA_Type.Value = this._FA_Type;
                }
                if (!string.IsNullOrEmpty(this._Status))
                {
                    this.FA_Status.Value = this._Status;
                }
                if (!string.IsNullOrEmpty(this._key))
                {
                    this.InitData();
                }
			}
		}




		private void InitData()
		{

            Hashtable ht = DataFactory.SqlDataBase().GetHashtableById("MMS_FixedAassets", "id", this._key);
           
            
            if (ht.Count > 0 && ht != null)
			{
				ControlBindHelper.SetWebControls(this.Page, ht);
			}

            foreach (DictionaryEntry element in ht)
            {

                string name = (string)element.Key;
                if (name == "FA_Type")
                {
                    this._FA_Type = (string)element.Value.ToString().Trim();
                    
                }

                if (name == "FA_Status")
                {
                    this._Status = (string)element.Value.ToString().Trim();

                }
               

            } 
               
           

            

		}
        private void InitFATypeId()
		{
           
            DataTable dt = this.system_idao.InitMaterialType();
			if (!string.IsNullOrEmpty(this._key))
			{
				if (DataTableHelper.IsExistRows(dt))
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
                        if (dt.Rows[i]["ValueCode"].ToString() == this._key)
						{
							dt.Rows.RemoveAt(i);
						}
					}
				}
			}
            ControlBindHelper.BindHtmlSelect(dt, this.FA_Type, "ValueName", "ValueName", "物资类型");
		}


        private void InitFAStatus()
        {

            DataTable dt = this.system_idao.InitMaterialStatus("状态");
            if (!string.IsNullOrEmpty(this._key))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ValueCode"].ToString() == this._key)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            ControlBindHelper.BindHtmlSelect(dt, this.FA_Status, "ValueName", "ValueCode", "物资状态");
        }







	
		
		protected void Save_Click(object sender, EventArgs e)
		{
            
			string guid = CommonHelper.GetGuid;
			Hashtable ht = new Hashtable();
			ht = ControlBindHelper.GetWebControls(this.Page);

            ht["FA_PurDate"] = Convert.ToDateTime(ht["FA_PurDate"].ToString());

            if (!string.IsNullOrEmpty(ht["FA_Type_hidden"].ToString().Trim()))
            {
                ht["FA_Type"] = ht["FA_Type_hidden"];
            }
            if (!string.IsNullOrEmpty(ht["FA_Status_hidden"].ToString().Trim()))
            {
                ht["FA_Status"] = ht["FA_Status_hidden"];
            }
           
			ht.Remove("User_ID_Hidden");
            ht.Remove("FA_Img_Hidden");

           

            

            if (!string.IsNullOrEmpty(this._key))
			{
				guid = this._key;
				ht["ModifyDate"] = DateTime.Now;
				ht["ModifyUserId"] = RequestSession.GetSessionUser().UserId;

           
			}


            ht.Remove("FA_Type_hidden");
            ht.Remove("FA_Status_hidden");
            ht.Remove("FA_Img_hidden");

            bool IsOk = DataFactory.SqlDataBase().Submit_AddOrEdit("dbo.MMS_FixedAassets", "id", this._key, ht);
			if (IsOk)
			{
					ShowMsgHelper.AlertMsg("操作成功！");
            }
			else
				{
					ShowMsgHelper.Alert_Error("操作失败！");
				}
			
		}




	}
}
