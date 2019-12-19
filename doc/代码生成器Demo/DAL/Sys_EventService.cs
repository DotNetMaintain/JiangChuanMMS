using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Sys_EventService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Sys_Event">Sys_Event实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Sys_Event model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@E_From",model.E_From),
                new SqlParameter ("@E_A_AppName",model.E_A_AppName),
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@User_Name",model.User_Name),
                new SqlParameter ("@E_Type",model.E_Type),
                new SqlParameter ("@E_IP",model.E_IP),
                new SqlParameter ("@E_Record",model.E_Record),
                new SqlParameter ("@E_DateTime",model.E_DateTime)
            };
           return Helper.ExecuteNonQuery("Sys_Event_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Sys_Event">Sys_Event实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Sys_Event model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@E_From",model.E_From),
                new SqlParameter ("@E_A_AppName",model.E_A_AppName),
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@User_Name",model.User_Name),
                new SqlParameter ("@E_Type",model.E_Type),
                new SqlParameter ("@E_IP",model.E_IP),
                new SqlParameter ("@E_Record",model.E_Record),
                new SqlParameter ("@E_DateTime",model.E_DateTime)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Sys_Event_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Sys_Event">Sys_Event实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Sys_Event model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@E_ID",model.E_ID),
                new SqlParameter ("@E_From",model.E_From),
                new SqlParameter ("@E_A_AppName",model.E_A_AppName),
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@User_Name",model.User_Name),
                new SqlParameter ("@E_Type",model.E_Type),
                new SqlParameter ("@E_IP",model.E_IP),
                new SqlParameter ("@E_Record",model.E_Record),
                new SqlParameter ("@E_DateTime",model.E_DateTime)
            };
           return Helper.ExecuteNonQuery("Sys_Event_Change",param);
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
                new SqlParameter ("@E_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Sys_Event_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Sys_Event> selectAll()
        {
            List<Sys_Event> list = new List<Sys_Event>();
            Sys_Event model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Sys_Event_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Sys_Event();
                    model.E_ID = dr["E_ID"].ToString();
                    if (DBNull.Value!=dr["E_From"])
                        model.E_From = dr["E_From"].ToString();
                    if (DBNull.Value!=dr["E_A_AppName"])
                        model.E_A_AppName = dr["E_A_AppName"].ToString();
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["User_Name"])
                        model.User_Name = dr["User_Name"].ToString();
                    if (DBNull.Value!=dr["E_Type"])
                        model.E_Type = dr["E_Type"].ToString();
                    if (DBNull.Value!=dr["E_IP"])
                        model.E_IP = dr["E_IP"].ToString();
                    if (DBNull.Value!=dr["E_Record"])
                        model.E_Record = dr["E_Record"].ToString();
                    if (DBNull.Value!=dr["E_DateTime"])
                        model.E_DateTime = dr["E_DateTime"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Sys_Event实体类对象</returns>
        public Sys_Event selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@E_ID",Id)
            };
            Sys_Event model = new Sys_Event();
            using (SqlDataReader dr = Helper.ExecuteReader("Sys_Event_SelectById", param))
            {
                if (dr.Read())
                {
                    model.E_ID = dr["E_ID"].ToString();
                    if (DBNull.Value!=dr["E_From"])
                        model.E_From = dr["E_From"].ToString();
                    if (DBNull.Value!=dr["E_A_AppName"])
                        model.E_A_AppName = dr["E_A_AppName"].ToString();
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["User_Name"])
                        model.User_Name = dr["User_Name"].ToString();
                    if (DBNull.Value!=dr["E_Type"])
                        model.E_Type = dr["E_Type"].ToString();
                    if (DBNull.Value!=dr["E_IP"])
                        model.E_IP = dr["E_IP"].ToString();
                    if (DBNull.Value!=dr["E_Record"])
                        model.E_Record = dr["E_Record"].ToString();
                    if (DBNull.Value!=dr["E_DateTime"])
                        model.E_DateTime = dr["E_DateTime"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Sys_Event实体类对象</returns>
        public List<Sys_Event> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Sys_Event> list = new List<Sys_Event>();
            Sys_Event model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Sys_Event_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Sys_Event();
                    model.E_ID = dr["E_ID"].ToString();
                    if (DBNull.Value!=dr["E_From"])
                        model.E_From = dr["E_From"].ToString();
                    if (DBNull.Value!=dr["E_A_AppName"])
                        model.E_A_AppName = dr["E_A_AppName"].ToString();
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["User_Name"])
                        model.User_Name = dr["User_Name"].ToString();
                    if (DBNull.Value!=dr["E_Type"])
                        model.E_Type = dr["E_Type"].ToString();
                    if (DBNull.Value!=dr["E_IP"])
                        model.E_IP = dr["E_IP"].ToString();
                    if (DBNull.Value!=dr["E_Record"])
                        model.E_Record = dr["E_Record"].ToString();
                    if (DBNull.Value!=dr["E_DateTime"])
                        model.E_DateTime = dr["E_DateTime"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

