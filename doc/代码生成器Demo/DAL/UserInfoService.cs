using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UserInfoService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="UserInfo">UserInfo实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(UserInfo model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Organization_ID",model.Organization_ID),
                new SqlParameter ("@User_Account",model.User_Account),
                new SqlParameter ("@User_Pwd",model.User_Pwd),
                new SqlParameter ("@User_Name",model.User_Name),
                new SqlParameter ("@User_Spelling",model.User_Spelling),
                new SqlParameter ("@User_Sex",model.User_Sex),
                new SqlParameter ("@User_Email",model.User_Email),
                new SqlParameter ("@User_Tel",model.User_Tel),
                new SqlParameter ("@User_Telphone",model.User_Telphone),
                new SqlParameter ("@User_CreatedTime",model.User_CreatedTime),
                new SqlParameter ("@User_Post",model.User_Post),
                new SqlParameter ("@User_PostRemark",model.User_PostRemark),
                new SqlParameter ("@User_Operator",model.User_Operator),
                new SqlParameter ("@User_RoleValue",model.User_RoleValue),
                new SqlParameter ("@User_Remark",model.User_Remark),
                new SqlParameter ("@User_Status",model.User_Status)
            };
           return Helper.ExecuteNonQuery("UserInfo_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="UserInfo">UserInfo实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(UserInfo model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Organization_ID",model.Organization_ID),
                new SqlParameter ("@User_Account",model.User_Account),
                new SqlParameter ("@User_Pwd",model.User_Pwd),
                new SqlParameter ("@User_Name",model.User_Name),
                new SqlParameter ("@User_Spelling",model.User_Spelling),
                new SqlParameter ("@User_Sex",model.User_Sex),
                new SqlParameter ("@User_Email",model.User_Email),
                new SqlParameter ("@User_Tel",model.User_Tel),
                new SqlParameter ("@User_Telphone",model.User_Telphone),
                new SqlParameter ("@User_CreatedTime",model.User_CreatedTime),
                new SqlParameter ("@User_Post",model.User_Post),
                new SqlParameter ("@User_PostRemark",model.User_PostRemark),
                new SqlParameter ("@User_Operator",model.User_Operator),
                new SqlParameter ("@User_RoleValue",model.User_RoleValue),
                new SqlParameter ("@User_Remark",model.User_Remark),
                new SqlParameter ("@User_Status",model.User_Status)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("UserInfo_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="UserInfo">UserInfo实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(UserInfo model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@Organization_ID",model.Organization_ID),
                new SqlParameter ("@User_Account",model.User_Account),
                new SqlParameter ("@User_Pwd",model.User_Pwd),
                new SqlParameter ("@User_Name",model.User_Name),
                new SqlParameter ("@User_Spelling",model.User_Spelling),
                new SqlParameter ("@User_Sex",model.User_Sex),
                new SqlParameter ("@User_Email",model.User_Email),
                new SqlParameter ("@User_Tel",model.User_Tel),
                new SqlParameter ("@User_Telphone",model.User_Telphone),
                new SqlParameter ("@User_CreatedTime",model.User_CreatedTime),
                new SqlParameter ("@User_Post",model.User_Post),
                new SqlParameter ("@User_PostRemark",model.User_PostRemark),
                new SqlParameter ("@User_Operator",model.User_Operator),
                new SqlParameter ("@User_RoleValue",model.User_RoleValue),
                new SqlParameter ("@User_Remark",model.User_Remark),
                new SqlParameter ("@User_Status",model.User_Status)
            };
           return Helper.ExecuteNonQuery("UserInfo_Change",param);
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
                new SqlParameter ("@User_ID",Id)
            };
           return Helper.ExecuteNonQuery ("UserInfo_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<UserInfo> selectAll()
        {
            List<UserInfo> list = new List<UserInfo>();
            UserInfo model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("UserInfo_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new UserInfo();
                    model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["Organization_ID"])
                        model.Organization_ID = dr["Organization_ID"].ToString();
                    if (DBNull.Value!=dr["User_Account"])
                        model.User_Account = dr["User_Account"].ToString();
                    if (DBNull.Value!=dr["User_Pwd"])
                        model.User_Pwd = dr["User_Pwd"].ToString();
                    if (DBNull.Value!=dr["User_Name"])
                        model.User_Name = dr["User_Name"].ToString();
                    if (DBNull.Value!=dr["User_Spelling"])
                        model.User_Spelling = dr["User_Spelling"].ToString();
                    if (DBNull.Value!=dr["User_Sex"])
                        model.User_Sex = dr["User_Sex"].ToString();
                    if (DBNull.Value!=dr["User_Email"])
                        model.User_Email = dr["User_Email"].ToString();
                    if (DBNull.Value!=dr["User_Tel"])
                        model.User_Tel = dr["User_Tel"].ToString();
                    if (DBNull.Value!=dr["User_Telphone"])
                        model.User_Telphone = dr["User_Telphone"].ToString();
                    if (DBNull.Value!=dr["User_CreatedTime"])
                        model.User_CreatedTime = dr["User_CreatedTime"].ToString();
                    if (DBNull.Value!=dr["User_Post"])
                        model.User_Post = dr["User_Post"].ToString();
                    if (DBNull.Value!=dr["User_PostRemark"])
                        model.User_PostRemark = dr["User_PostRemark"].ToString();
                    if (DBNull.Value!=dr["User_Operator"])
                        model.User_Operator = dr["User_Operator"].ToString();
                    if (DBNull.Value!=dr["User_RoleValue"])
                        model.User_RoleValue = dr["User_RoleValue"].ToString();
                    if (DBNull.Value!=dr["User_Remark"])
                        model.User_Remark = dr["User_Remark"].ToString();
                    if (DBNull.Value!=dr["User_Status"])
                        model.User_Status = dr["User_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>UserInfo实体类对象</returns>
        public UserInfo selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_ID",Id)
            };
            UserInfo model = new UserInfo();
            using (SqlDataReader dr = Helper.ExecuteReader("UserInfo_SelectById", param))
            {
                if (dr.Read())
                {
                    model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["Organization_ID"])
                        model.Organization_ID = dr["Organization_ID"].ToString();
                    if (DBNull.Value!=dr["User_Account"])
                        model.User_Account = dr["User_Account"].ToString();
                    if (DBNull.Value!=dr["User_Pwd"])
                        model.User_Pwd = dr["User_Pwd"].ToString();
                    if (DBNull.Value!=dr["User_Name"])
                        model.User_Name = dr["User_Name"].ToString();
                    if (DBNull.Value!=dr["User_Spelling"])
                        model.User_Spelling = dr["User_Spelling"].ToString();
                    if (DBNull.Value!=dr["User_Sex"])
                        model.User_Sex = dr["User_Sex"].ToString();
                    if (DBNull.Value!=dr["User_Email"])
                        model.User_Email = dr["User_Email"].ToString();
                    if (DBNull.Value!=dr["User_Tel"])
                        model.User_Tel = dr["User_Tel"].ToString();
                    if (DBNull.Value!=dr["User_Telphone"])
                        model.User_Telphone = dr["User_Telphone"].ToString();
                    if (DBNull.Value!=dr["User_CreatedTime"])
                        model.User_CreatedTime = dr["User_CreatedTime"].ToString();
                    if (DBNull.Value!=dr["User_Post"])
                        model.User_Post = dr["User_Post"].ToString();
                    if (DBNull.Value!=dr["User_PostRemark"])
                        model.User_PostRemark = dr["User_PostRemark"].ToString();
                    if (DBNull.Value!=dr["User_Operator"])
                        model.User_Operator = dr["User_Operator"].ToString();
                    if (DBNull.Value!=dr["User_RoleValue"])
                        model.User_RoleValue = dr["User_RoleValue"].ToString();
                    if (DBNull.Value!=dr["User_Remark"])
                        model.User_Remark = dr["User_Remark"].ToString();
                    if (DBNull.Value!=dr["User_Status"])
                        model.User_Status = dr["User_Status"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>UserInfo实体类对象</returns>
        public List<UserInfo> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<UserInfo> list = new List<UserInfo>();
            UserInfo model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("UserInfo_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new UserInfo();
                    model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["Organization_ID"])
                        model.Organization_ID = dr["Organization_ID"].ToString();
                    if (DBNull.Value!=dr["User_Account"])
                        model.User_Account = dr["User_Account"].ToString();
                    if (DBNull.Value!=dr["User_Pwd"])
                        model.User_Pwd = dr["User_Pwd"].ToString();
                    if (DBNull.Value!=dr["User_Name"])
                        model.User_Name = dr["User_Name"].ToString();
                    if (DBNull.Value!=dr["User_Spelling"])
                        model.User_Spelling = dr["User_Spelling"].ToString();
                    if (DBNull.Value!=dr["User_Sex"])
                        model.User_Sex = dr["User_Sex"].ToString();
                    if (DBNull.Value!=dr["User_Email"])
                        model.User_Email = dr["User_Email"].ToString();
                    if (DBNull.Value!=dr["User_Tel"])
                        model.User_Tel = dr["User_Tel"].ToString();
                    if (DBNull.Value!=dr["User_Telphone"])
                        model.User_Telphone = dr["User_Telphone"].ToString();
                    if (DBNull.Value!=dr["User_CreatedTime"])
                        model.User_CreatedTime = dr["User_CreatedTime"].ToString();
                    if (DBNull.Value!=dr["User_Post"])
                        model.User_Post = dr["User_Post"].ToString();
                    if (DBNull.Value!=dr["User_PostRemark"])
                        model.User_PostRemark = dr["User_PostRemark"].ToString();
                    if (DBNull.Value!=dr["User_Operator"])
                        model.User_Operator = dr["User_Operator"].ToString();
                    if (DBNull.Value!=dr["User_RoleValue"])
                        model.User_RoleValue = dr["User_RoleValue"].ToString();
                    if (DBNull.Value!=dr["User_Remark"])
                        model.User_Remark = dr["User_Remark"].ToString();
                    if (DBNull.Value!=dr["User_Status"])
                        model.User_Status = dr["User_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

