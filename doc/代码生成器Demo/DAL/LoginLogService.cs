using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class LoginLogService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="LoginLog">LoginLog实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(LoginLog model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_Account",model.User_Account),
                new SqlParameter ("@LoginLog_BuilTime",model.LoginLog_BuilTime),
                new SqlParameter ("@LoginLog_IP",model.LoginLog_IP),
                new SqlParameter ("@LoginLog_Status",model.LoginLog_Status),
                new SqlParameter ("@User_id",model.User_id)
            };
           return Helper.ExecuteNonQuery("LoginLog_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="LoginLog">LoginLog实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(LoginLog model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_Account",model.User_Account),
                new SqlParameter ("@LoginLog_BuilTime",model.LoginLog_BuilTime),
                new SqlParameter ("@LoginLog_IP",model.LoginLog_IP),
                new SqlParameter ("@LoginLog_Status",model.LoginLog_Status),
                new SqlParameter ("@User_id",model.User_id)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("LoginLog_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="LoginLog">LoginLog实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(LoginLog model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@LoginLog_ID",model.LoginLog_ID),
                new SqlParameter ("@User_Account",model.User_Account),
                new SqlParameter ("@LoginLog_BuilTime",model.LoginLog_BuilTime),
                new SqlParameter ("@LoginLog_IP",model.LoginLog_IP),
                new SqlParameter ("@LoginLog_Status",model.LoginLog_Status),
                new SqlParameter ("@User_id",model.User_id)
            };
           return Helper.ExecuteNonQuery("LoginLog_Change",param);
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
                new SqlParameter ("@LoginLog_ID",Id)
            };
           return Helper.ExecuteNonQuery ("LoginLog_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<LoginLog> selectAll()
        {
            List<LoginLog> list = new List<LoginLog>();
            LoginLog model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("LoginLog_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new LoginLog();
                    model.LoginLog_ID= Convert.ToInt32(dr["LoginLog_ID"]);
                    model.User_Account = dr["User_Account"].ToString();
                    model.LoginLog_BuilTime= Convert.ToDateTime(dr["LoginLog_BuilTime"]);
                    model.LoginLog_IP = dr["LoginLog_IP"].ToString();
                    model.LoginLog_Status= Convert.ToInt32(dr["LoginLog_Status"]);
                    if (DBNull.Value!=dr["User_id"])
                        model.User_id= Convert.ToInt32(dr["User_id"]);
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>LoginLog实体类对象</returns>
        public LoginLog selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@LoginLog_ID",Id)
            };
            LoginLog model = new LoginLog();
            using (SqlDataReader dr = Helper.ExecuteReader("LoginLog_SelectById", param))
            {
                if (dr.Read())
                {
                    model.LoginLog_ID= Convert.ToInt32(dr["LoginLog_ID"]);
                    model.User_Account = dr["User_Account"].ToString();
                    model.LoginLog_BuilTime= Convert.ToDateTime(dr["LoginLog_BuilTime"]);
                    model.LoginLog_IP = dr["LoginLog_IP"].ToString();
                    model.LoginLog_Status= Convert.ToInt32(dr["LoginLog_Status"]);
                    if (DBNull.Value!=dr["User_id"])
                        model.User_id= Convert.ToInt32(dr["User_id"]);
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>LoginLog实体类对象</returns>
        public List<LoginLog> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<LoginLog> list = new List<LoginLog>();
            LoginLog model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("LoginLog_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new LoginLog();
                    model.LoginLog_ID= Convert.ToInt32(dr["LoginLog_ID"]);
                    model.User_Account = dr["User_Account"].ToString();
                    model.LoginLog_BuilTime= Convert.ToDateTime(dr["LoginLog_BuilTime"]);
                    model.LoginLog_IP = dr["LoginLog_IP"].ToString();
                    model.LoginLog_Status= Convert.ToInt32(dr["LoginLog_Status"]);
                    if (DBNull.Value!=dr["User_id"])
                        model.User_id= Convert.ToInt32(dr["User_id"]);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

