using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class TallysService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Tallys">Tallys实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Tallys model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Sort_ID",model.Sort_ID),
                new SqlParameter ("@Tallys_Time",model.Tallys_Time),
                new SqlParameter ("@Payment",model.Payment),
                new SqlParameter ("@Tallys_Detail",model.Tallys_Detail),
                new SqlParameter ("@Tallys_Price",model.Tallys_Price),
                new SqlParameter ("@Tallys_CreateUserID",model.Tallys_CreateUserID)
            };
           return Helper.ExecuteNonQuery("Tallys_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Tallys">Tallys实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Tallys model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Sort_ID",model.Sort_ID),
                new SqlParameter ("@Tallys_Time",model.Tallys_Time),
                new SqlParameter ("@Payment",model.Payment),
                new SqlParameter ("@Tallys_Detail",model.Tallys_Detail),
                new SqlParameter ("@Tallys_Price",model.Tallys_Price),
                new SqlParameter ("@Tallys_CreateUserID",model.Tallys_CreateUserID)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Tallys_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Tallys">Tallys实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Tallys model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Tallys_ID",model.Tallys_ID),
                new SqlParameter ("@Sort_ID",model.Sort_ID),
                new SqlParameter ("@Tallys_Time",model.Tallys_Time),
                new SqlParameter ("@Payment",model.Payment),
                new SqlParameter ("@Tallys_Detail",model.Tallys_Detail),
                new SqlParameter ("@Tallys_Price",model.Tallys_Price),
                new SqlParameter ("@Tallys_CreateUserID",model.Tallys_CreateUserID)
            };
           return Helper.ExecuteNonQuery("Tallys_Change",param);
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
                new SqlParameter ("@Tallys_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Tallys_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Tallys> selectAll()
        {
            List<Tallys> list = new List<Tallys>();
            Tallys model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Tallys_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Tallys();
                    model.Tallys_ID = dr["Tallys_ID"].ToString();
                    if (DBNull.Value!=dr["Sort_ID"])
                        model.Sort_ID = dr["Sort_ID"].ToString();
                    if (DBNull.Value!=dr["Tallys_Time"])
                        model.Tallys_Time = dr["Tallys_Time"].ToString();
                    if (DBNull.Value!=dr["Payment"])
                        model.Payment = dr["Payment"].ToString();
                    if (DBNull.Value!=dr["Tallys_Detail"])
                        model.Tallys_Detail = dr["Tallys_Detail"].ToString();
                    if (DBNull.Value!=dr["Tallys_Price"])
                        model.Tallys_Price = dr["Tallys_Price"].ToString();
                    if (DBNull.Value!=dr["Tallys_CreateUserID"])
                        model.Tallys_CreateUserID = dr["Tallys_CreateUserID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Tallys实体类对象</returns>
        public Tallys selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Tallys_ID",Id)
            };
            Tallys model = new Tallys();
            using (SqlDataReader dr = Helper.ExecuteReader("Tallys_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Tallys_ID = dr["Tallys_ID"].ToString();
                    if (DBNull.Value!=dr["Sort_ID"])
                        model.Sort_ID = dr["Sort_ID"].ToString();
                    if (DBNull.Value!=dr["Tallys_Time"])
                        model.Tallys_Time = dr["Tallys_Time"].ToString();
                    if (DBNull.Value!=dr["Payment"])
                        model.Payment = dr["Payment"].ToString();
                    if (DBNull.Value!=dr["Tallys_Detail"])
                        model.Tallys_Detail = dr["Tallys_Detail"].ToString();
                    if (DBNull.Value!=dr["Tallys_Price"])
                        model.Tallys_Price = dr["Tallys_Price"].ToString();
                    if (DBNull.Value!=dr["Tallys_CreateUserID"])
                        model.Tallys_CreateUserID = dr["Tallys_CreateUserID"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Tallys实体类对象</returns>
        public List<Tallys> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Tallys> list = new List<Tallys>();
            Tallys model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Tallys_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Tallys();
                    model.Tallys_ID = dr["Tallys_ID"].ToString();
                    if (DBNull.Value!=dr["Sort_ID"])
                        model.Sort_ID = dr["Sort_ID"].ToString();
                    if (DBNull.Value!=dr["Tallys_Time"])
                        model.Tallys_Time = dr["Tallys_Time"].ToString();
                    if (DBNull.Value!=dr["Payment"])
                        model.Payment = dr["Payment"].ToString();
                    if (DBNull.Value!=dr["Tallys_Detail"])
                        model.Tallys_Detail = dr["Tallys_Detail"].ToString();
                    if (DBNull.Value!=dr["Tallys_Price"])
                        model.Tallys_Price = dr["Tallys_Price"].ToString();
                    if (DBNull.Value!=dr["Tallys_CreateUserID"])
                        model.Tallys_CreateUserID = dr["Tallys_CreateUserID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

