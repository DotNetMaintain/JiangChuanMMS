using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class OrganizationService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Organization">Organization实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Organization model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Organization_Name",model.Organization_Name),
                new SqlParameter ("@Organization_User_Name",model.Organization_User_Name),
                new SqlParameter ("@Organization_Tel",model.Organization_Tel),
                new SqlParameter ("@Organization_Telphone",model.Organization_Telphone),
                new SqlParameter ("@Organization_Fax",model.Organization_Fax),
                new SqlParameter ("@Organization_Code",model.Organization_Code),
                new SqlParameter ("@Organization_Address",model.Organization_Address),
                new SqlParameter ("@Organization_PanelID",model.Organization_PanelID),
                new SqlParameter ("@Organization_Order",model.Organization_Order),
                new SqlParameter ("@Organization_CreatedTime",model.Organization_CreatedTime),
                new SqlParameter ("@Organization_Remark",model.Organization_Remark),
                new SqlParameter ("@Organization_Status",model.Organization_Status)
            };
           return Helper.ExecuteNonQuery("Organization_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Organization">Organization实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Organization model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Organization_Name",model.Organization_Name),
                new SqlParameter ("@Organization_User_Name",model.Organization_User_Name),
                new SqlParameter ("@Organization_Tel",model.Organization_Tel),
                new SqlParameter ("@Organization_Telphone",model.Organization_Telphone),
                new SqlParameter ("@Organization_Fax",model.Organization_Fax),
                new SqlParameter ("@Organization_Code",model.Organization_Code),
                new SqlParameter ("@Organization_Address",model.Organization_Address),
                new SqlParameter ("@Organization_PanelID",model.Organization_PanelID),
                new SqlParameter ("@Organization_Order",model.Organization_Order),
                new SqlParameter ("@Organization_CreatedTime",model.Organization_CreatedTime),
                new SqlParameter ("@Organization_Remark",model.Organization_Remark),
                new SqlParameter ("@Organization_Status",model.Organization_Status)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Organization_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Organization">Organization实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Organization model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Organization_ID",model.Organization_ID),
                new SqlParameter ("@Organization_Name",model.Organization_Name),
                new SqlParameter ("@Organization_User_Name",model.Organization_User_Name),
                new SqlParameter ("@Organization_Tel",model.Organization_Tel),
                new SqlParameter ("@Organization_Telphone",model.Organization_Telphone),
                new SqlParameter ("@Organization_Fax",model.Organization_Fax),
                new SqlParameter ("@Organization_Code",model.Organization_Code),
                new SqlParameter ("@Organization_Address",model.Organization_Address),
                new SqlParameter ("@Organization_PanelID",model.Organization_PanelID),
                new SqlParameter ("@Organization_Order",model.Organization_Order),
                new SqlParameter ("@Organization_CreatedTime",model.Organization_CreatedTime),
                new SqlParameter ("@Organization_Remark",model.Organization_Remark),
                new SqlParameter ("@Organization_Status",model.Organization_Status)
            };
           return Helper.ExecuteNonQuery("Organization_Change",param);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool delete(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Organization_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Organization_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Organization> selectAll()
        {
            List<Organization> list = new List<Organization>();
            Organization model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Organization_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Organization();
                    model.Organization_ID = dr["Organization_ID"].ToString();
                    if (DBNull.Value!=dr["Organization_Name"])
                        model.Organization_Name = dr["Organization_Name"].ToString();
                    if (DBNull.Value!=dr["Organization_User_Name"])
                        model.Organization_User_Name = dr["Organization_User_Name"].ToString();
                    if (DBNull.Value!=dr["Organization_Tel"])
                        model.Organization_Tel = dr["Organization_Tel"].ToString();
                    if (DBNull.Value!=dr["Organization_Telphone"])
                        model.Organization_Telphone = dr["Organization_Telphone"].ToString();
                    if (DBNull.Value!=dr["Organization_Fax"])
                        model.Organization_Fax = dr["Organization_Fax"].ToString();
                    if (DBNull.Value!=dr["Organization_Code"])
                        model.Organization_Code = dr["Organization_Code"].ToString();
                    if (DBNull.Value!=dr["Organization_Address"])
                        model.Organization_Address = dr["Organization_Address"].ToString();
                    if (DBNull.Value!=dr["Organization_PanelID"])
                        model.Organization_PanelID = dr["Organization_PanelID"].ToString();
                    if (DBNull.Value!=dr["Organization_Order"])
                        model.Organization_Order = dr["Organization_Order"].ToString();
                    if (DBNull.Value!=dr["Organization_CreatedTime"])
                        model.Organization_CreatedTime = dr["Organization_CreatedTime"].ToString();
                    if (DBNull.Value!=dr["Organization_Remark"])
                        model.Organization_Remark = dr["Organization_Remark"].ToString();
                    if (DBNull.Value!=dr["Organization_Status"])
                        model.Organization_Status = dr["Organization_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Organization实体类对象</returns>
        public Organization selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Organization_ID",Id)
            };
            Organization model = new Organization();
            using (SqlDataReader dr = Helper.ExecuteReader("Organization_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Organization_ID = dr["Organization_ID"].ToString();
                    if (DBNull.Value!=dr["Organization_Name"])
                        model.Organization_Name = dr["Organization_Name"].ToString();
                    if (DBNull.Value!=dr["Organization_User_Name"])
                        model.Organization_User_Name = dr["Organization_User_Name"].ToString();
                    if (DBNull.Value!=dr["Organization_Tel"])
                        model.Organization_Tel = dr["Organization_Tel"].ToString();
                    if (DBNull.Value!=dr["Organization_Telphone"])
                        model.Organization_Telphone = dr["Organization_Telphone"].ToString();
                    if (DBNull.Value!=dr["Organization_Fax"])
                        model.Organization_Fax = dr["Organization_Fax"].ToString();
                    if (DBNull.Value!=dr["Organization_Code"])
                        model.Organization_Code = dr["Organization_Code"].ToString();
                    if (DBNull.Value!=dr["Organization_Address"])
                        model.Organization_Address = dr["Organization_Address"].ToString();
                    if (DBNull.Value!=dr["Organization_PanelID"])
                        model.Organization_PanelID = dr["Organization_PanelID"].ToString();
                    if (DBNull.Value!=dr["Organization_Order"])
                        model.Organization_Order = dr["Organization_Order"].ToString();
                    if (DBNull.Value!=dr["Organization_CreatedTime"])
                        model.Organization_CreatedTime = dr["Organization_CreatedTime"].ToString();
                    if (DBNull.Value!=dr["Organization_Remark"])
                        model.Organization_Remark = dr["Organization_Remark"].ToString();
                    if (DBNull.Value!=dr["Organization_Status"])
                        model.Organization_Status = dr["Organization_Status"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Organization实体类对象</returns>
        public List<Organization> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Organization> list = new List<Organization>();
            Organization model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Organization_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Organization();
                    model.Organization_ID = dr["Organization_ID"].ToString();
                    if (DBNull.Value!=dr["Organization_Name"])
                        model.Organization_Name = dr["Organization_Name"].ToString();
                    if (DBNull.Value!=dr["Organization_User_Name"])
                        model.Organization_User_Name = dr["Organization_User_Name"].ToString();
                    if (DBNull.Value!=dr["Organization_Tel"])
                        model.Organization_Tel = dr["Organization_Tel"].ToString();
                    if (DBNull.Value!=dr["Organization_Telphone"])
                        model.Organization_Telphone = dr["Organization_Telphone"].ToString();
                    if (DBNull.Value!=dr["Organization_Fax"])
                        model.Organization_Fax = dr["Organization_Fax"].ToString();
                    if (DBNull.Value!=dr["Organization_Code"])
                        model.Organization_Code = dr["Organization_Code"].ToString();
                    if (DBNull.Value!=dr["Organization_Address"])
                        model.Organization_Address = dr["Organization_Address"].ToString();
                    if (DBNull.Value!=dr["Organization_PanelID"])
                        model.Organization_PanelID = dr["Organization_PanelID"].ToString();
                    if (DBNull.Value!=dr["Organization_Order"])
                        model.Organization_Order = dr["Organization_Order"].ToString();
                    if (DBNull.Value!=dr["Organization_CreatedTime"])
                        model.Organization_CreatedTime = dr["Organization_CreatedTime"].ToString();
                    if (DBNull.Value!=dr["Organization_Remark"])
                        model.Organization_Remark = dr["Organization_Remark"].ToString();
                    if (DBNull.Value!=dr["Organization_Status"])
                        model.Organization_Status = dr["Organization_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

