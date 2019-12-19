using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Visits_StatisticService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Visits_Statistic">Visits_Statistic实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Visits_Statistic model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@Visits_Time",model.Visits_Time),
                new SqlParameter ("@Visits_IP",model.Visits_IP)
            };
           return Helper.ExecuteNonQuery("Visits_Statistic_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Visits_Statistic">Visits_Statistic实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Visits_Statistic model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@Visits_Time",model.Visits_Time),
                new SqlParameter ("@Visits_IP",model.Visits_IP)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Visits_Statistic_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Visits_Statistic">Visits_Statistic实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Visits_Statistic model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Visits_Statistic_ID",model.Visits_Statistic_ID),
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@Visits_Time",model.Visits_Time),
                new SqlParameter ("@Visits_IP",model.Visits_IP)
            };
           return Helper.ExecuteNonQuery("Visits_Statistic_Change",param);
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
                new SqlParameter ("@Visits_Statistic_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Visits_Statistic_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Visits_Statistic> selectAll()
        {
            List<Visits_Statistic> list = new List<Visits_Statistic>();
            Visits_Statistic model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Visits_Statistic_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Visits_Statistic();
                    model.Visits_Statistic_ID= Convert.ToInt32(dr["Visits_Statistic_ID"]);
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID= Convert.ToInt32(dr["User_ID"]);
                    if (DBNull.Value!=dr["Visits_Time"])
                        model.Visits_Time= Convert.ToDateTime(dr["Visits_Time"]);
                    if (DBNull.Value!=dr["Visits_IP"])
                        model.Visits_IP = dr["Visits_IP"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Visits_Statistic实体类对象</returns>
        public Visits_Statistic selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Visits_Statistic_ID",Id)
            };
            Visits_Statistic model = new Visits_Statistic();
            using (SqlDataReader dr = Helper.ExecuteReader("Visits_Statistic_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Visits_Statistic_ID= Convert.ToInt32(dr["Visits_Statistic_ID"]);
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID= Convert.ToInt32(dr["User_ID"]);
                    if (DBNull.Value!=dr["Visits_Time"])
                        model.Visits_Time= Convert.ToDateTime(dr["Visits_Time"]);
                    if (DBNull.Value!=dr["Visits_IP"])
                        model.Visits_IP = dr["Visits_IP"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Visits_Statistic实体类对象</returns>
        public List<Visits_Statistic> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Visits_Statistic> list = new List<Visits_Statistic>();
            Visits_Statistic model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Visits_Statistic_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Visits_Statistic();
                    model.Visits_Statistic_ID= Convert.ToInt32(dr["Visits_Statistic_ID"]);
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID= Convert.ToInt32(dr["User_ID"]);
                    if (DBNull.Value!=dr["Visits_Time"])
                        model.Visits_Time= Convert.ToDateTime(dr["Visits_Time"]);
                    if (DBNull.Value!=dr["Visits_IP"])
                        model.Visits_IP = dr["Visits_IP"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

