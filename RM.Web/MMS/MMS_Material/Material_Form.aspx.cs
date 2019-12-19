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

namespace RM.Web.MMS.MMS_Material
{
	public class Material_Form : PageBase
	{
		public StringBuilder str_allUserInfo = new StringBuilder();
		public StringBuilder str_seleteUserInfo = new StringBuilder();
		private RM_UserInfo_IDAO user_idao = new RM_UserInfo_Dal();
		private RM_System_IDAO system_idao = new RM_System_Dal();
		private string _key;
        private string _Material_Type;
        private string _Status;
		private int index_TreeNode = 0;
		protected HtmlForm form1;
		protected HtmlInputHidden User_ID_Hidden;
        protected HtmlInputHidden material_type_hidden;
        protected HtmlInputHidden material_status_hidden;
        protected HtmlInputText Material_Pic;
        protected HtmlInputText Material_Code;
        protected HtmlSelect Material_Type;
        protected HtmlInputText Material_Name;
        protected HtmlInputText Material_CommonlyName;
        protected HtmlInputText Material_Specification;
        protected HtmlInputText Material_Unit;
        protected HtmlInputText Material_Supplier;
        protected HtmlInputText Material_Comm;
        protected HtmlInputFile MaterialPic_ID_Hidden;
        protected HtmlSelect Material_Attr01;

        protected HtmlSelect DeleteMark;
		protected LinkButton Save;
		protected void Page_Load(object sender, EventArgs e)
		{
			this._key = base.Request["key"];

            this._Material_Type = base.Request["Material_Type"];
         
		
			if (!base.IsPostBack)
			{

                this.InitMaterialAttr();
                this.InitMaterialStatus();

                this.InitParentId();
                if (!string.IsNullOrEmpty(this._Material_Type))
                {
                    this.Material_Type.Value = this._Material_Type;
                }
                if (!string.IsNullOrEmpty(this._Status))
                {
                    this.DeleteMark.Value = this._Status;
                }

                if (!string.IsNullOrEmpty(this._key))
                {
                    this.InitData();
                }

			}
		}




		private void InitData()
		{
            
            Hashtable ht = DataFactory.SqlDataBase().GetHashtableById("MMS_MaterialInfo", "Material_ID", this._key);
            if (string.IsNullOrEmpty(this._key))
            {
                ht["Material_Code"] = BatchEvaluate.GeneralCode();
            }
            
            if (ht.Count > 0 && ht != null)
			{
				ControlBindHelper.SetWebControls(this.Page, ht);
			}

            foreach (DictionaryEntry element in ht)
            {

                string name = (string)element.Key;
                if (name == "MATERIAL_TYPE")
                {
                    this._Material_Type = (string)element.Value.ToString().Trim();
                    
                }

                if (name == "DELETEMARK")
                {
                    this._Status = (string)element.Value.ToString().Trim();

                }
               

            } 
               
           

            

		}
        private void InitMaterialTypeId()
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
            ControlBindHelper.BindHtmlSelect(dt, this.Material_Type, "ValueName", "ValueName", "物资类型");
		}


        private void InitMaterialStatus()
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
            ControlBindHelper.BindHtmlSelect(dt, this.DeleteMark, "ValueName", "ValueCode", "物资状态");
        }



        private void InitMaterialAttr()
        {

            DataTable dt = this.system_idao.InitMaterialStatus("物资属性");
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
            ControlBindHelper.BindHtmlSelect(dt, this.Material_Attr01, "ValueName", "ValueCode", "物资属性");
        }




        public void InitUserRole()
		{
			if (!string.IsNullOrEmpty(this._key))
			{
				DataTable dt = this.system_idao.InitUserRole(this._key);
				if (DataTableHelper.IsExistRows(dt))
				{
					foreach (DataRow drv in dt.Rows)
					{
						this.str_seleteUserInfo.Append(string.Concat(new object[]
						{
							"<tr ondblclick='$(this).remove()'><td>",
							drv["User_Name"],
							"</td><td>",
							drv["Organization_Name"],
							"</td><td  style='display:none'>",
							drv["User_ID"],
							"</td></tr>"
						}));
					}
				}
			}
		}
		public void InitUserInfo()
		{
			this.InitUserRole();
			DataTable dt_Org = this.user_idao.Load_StaffOrganizeList();
			foreach (DataRowView drv in new DataView(dt_Org)
			{
				RowFilter = "ParentId = '0'"
			})
			{
				DataTable GetNewData = DataTableHelper.GetNewDataTable(dt_Org, "ParentId = '" + drv["Organization_ID"].ToString() + "'");
				if (DataTableHelper.IsExistRows(GetNewData))
				{
					this.str_allUserInfo.Append("<li>");
					this.str_allUserInfo.Append("<div>" + drv["Organization_Name"].ToString() + "</div>");
					this.str_allUserInfo.Append(this.GetTreeNode(drv["Organization_ID"].ToString(), drv["Organization_Name"].ToString(), dt_Org, "1"));
					this.str_allUserInfo.Append("</li>");
				}
			}
		}


        private void InitParentId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MaterialType_ID,\r\n                            MaterialType_Name+' - '+CASE ParentId WHEN '0' THEN '父节' ELSE  '子节' END AS MaterialType_Name\r\n                            FROM MMS_MaterialType WHERE DeleteMark = 1 ORDER BY SortCode ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(this._key))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["MaterialType_ID"].ToString() == this._key)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            ControlBindHelper.BindHtmlSelect(dt, this.Material_Type, "MaterialType_Name", "MaterialType_Name", "物资类型 - 父节");
        }


		public string GetTreeNode(string parentID, string parentName, DataTable dtNode, string status)
		{
			StringBuilder sb_TreeNode = new StringBuilder();
			DataTable GetNewData = new DataTable();
			DataView dv = new DataView(dtNode);
			dv.RowFilter = "ParentId = '" + parentID + "'";
			if (dv.Count > 0)
			{
				if (this.index_TreeNode == 0)
				{
					sb_TreeNode.Append("<ul>");
				}
				else
				{
					sb_TreeNode.Append("<ul style='display: none'>");
				}
				foreach (DataRowView drv in dv)
				{
					GetNewData = DataTableHelper.GetNewDataTable(dtNode, "ParentId = '" + drv["Organization_ID"].ToString() + "'");
					if (drv["isUser"].ToString() == "0")
					{
						if (DataTableHelper.IsExistRows(GetNewData))
						{
							sb_TreeNode.Append("<li>");
							sb_TreeNode.Append("<div>" + drv["Organization_Name"] + "</div>");
							sb_TreeNode.Append(this.GetTreeNode(drv["Organization_ID"].ToString(), drv["Organization_Name"].ToString(), dtNode, "2"));
							sb_TreeNode.Append("</li>");
						}
					}
					else
					{
						if (status != "1")
						{
							sb_TreeNode.Append("<li>");
							sb_TreeNode.Append(string.Concat(new object[]
							{
								"<div ondblclick=\"addUserInfo('",
								drv["Organization_Name"],
								"','",
								drv["Organization_ID"],
								"','",
								parentName,
								"')\">"
							}));
							sb_TreeNode.Append("<img src=\"/Themes/Images/user_mature.png\" width=\"16\" height=\"16\" />" + drv["Organization_Name"].ToString() + "</div>");
							sb_TreeNode.Append("</li>");
						}
					}
				}
				sb_TreeNode.Append("</ul>");
			}
			this.index_TreeNode++;
			return sb_TreeNode.ToString();
		}
		protected void Save_Click(object sender, EventArgs e)
		{
            
			string guid = CommonHelper.GetGuid;
			Hashtable ht = new Hashtable();
			ht = ControlBindHelper.GetWebControls(this.Page);

            if (!string.IsNullOrEmpty(ht["material_type_hidden"].ToString().Trim()))
            {
                ht["Material_Type"] = ht["material_type_hidden"];
            }
            if (!string.IsNullOrEmpty(ht["material_status_hidden"].ToString().Trim()))
            {
                ht["DeleteMark"] = ht["material_status_hidden"];
            }

            if (ht["Material_Attr01"].ToString().Trim() == "")
            {
                ShowMsgHelper.Alert_Error("物资属性未选择！");
                return;
            }
            ht.Remove("User_ID_Hidden");
            ht.Remove("MaterialPic_ID_Hidden");
            ht.Remove("Material_Pic_hidden");
           

            //if (this.MaterialTy(peId.Value == "")
            //{
            //    ht["ParentId"] = "0";
            //}

            

            if (!string.IsNullOrEmpty(this._key))
			{
				guid = this._key;
				ht["ModifyDate"] = DateTime.Now;
				ht["ModifyUserId"] = RequestSession.GetSessionUser().UserId;

               

				//ht["ModifyUserName"] = RequestSession.GetSessionUser().UserName;
			}
			else
			{
               
                //ht["Material_ID"] = guid;
				ht["CreateUserId"] = RequestSession.GetSessionUser().UserId;
                //ht["CreateUserName"] = RequestSession.GetSessionUser().UserName;
			}

            ht.Remove("material_type_hidden");
            ht.Remove("material_status_hidden");

            bool IsOk = DataFactory.SqlDataBase().Submit_AddOrEdit("dbo.MMS_MaterialInfo", "Material_ID", this._key, ht);
			if (IsOk)
			{
				string str = this.User_ID_Hidden.Value;
				if (!string.IsNullOrEmpty(str))
				{
					str = this.User_ID_Hidden.Value.Substring(0, this.User_ID_Hidden.Value.Length - 1);
				}
				bool IsAllto = this.system_idao.Add_RoleAllotMember(str.Split(new char[]
				{
					','
				}), guid);
				if (IsAllto)
				{
					ShowMsgHelper.AlertMsg("操作成功！");
				}
				else
				{
					ShowMsgHelper.Alert_Error("操作失败！");
				}
			}
			else
			{
				ShowMsgHelper.Alert_Error("操作失败！");
			}
		}




	}
}
