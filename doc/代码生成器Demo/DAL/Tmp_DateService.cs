using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Tmp_DateService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Tmp_Date">Tmp_Date实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Tmp_Date model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@i_value",model.I_value)
            };
           return Helper.ExecuteNonQuery("Tmp_Date_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Tmp_Date">Tmp_Date实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Tmp_Date model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@i_value",model.I_value)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Tmp_Date_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Tmp_Date">Tmp_Date实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Tmp_Date model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@i_key",model.I_key),
                new SqlParameter ("@i_value",model.I_value)
            };
           return Helper.ExecuteNonQuery("Tmp_Date_Change",param);
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
                new SqlParameter ("@i_key",Id)
            };
           return Helper.ExecuteNonQuery ("Tmp_Date_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Tmp_Date> selectAll()
        {
            List<Tmp_Date> list = new List<Tmp_Date>();
            Tmp_Date model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Tmp_Date_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Tmp_Date();
                    model.I_key= Convert.ToInt32(dr["i_key"]);
                    if (DBNull.Value!=dr["i_value"])
                        model.I_value= Convert.ToInt32(dr["i_value"]);
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Tmp_Date实体类对象</returns>
        public Tmp_Date selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@i_key",Id)
            };
            Tmp_Date model = new Tmp_Date();
            using (SqlDataReader dr = Helper.ExecuteReader("Tmp_Date_SelectById", param))
            {
                if (dr.Read())
                {
                    model.I_key= Convert.ToInt32(dr["i_key"]);
                    if (DBNull.Value!=dr["i_value"])
                        model.I_value= Convert.ToInt32(dr["i_value"]);
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Tmp_Date实体类对象</returns>
        public List<Tmp_Date> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Tmp_Date> list = new List<Tmp_Date>();
            Tmp_Date model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Tmp_Date_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Tmp_Date();
                    model.I_key= Convert.ToInt32(dr["i_key"]);
                    if (DBNull.Value!=dr["i_value"])
                        model.I_value= Convert.ToInt32(dr["i_value"]);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

