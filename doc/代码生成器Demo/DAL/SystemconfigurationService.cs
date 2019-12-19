using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SystemconfigurationService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Systemconfiguration">Systemconfiguration实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Systemconfiguration model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Config_MenuType",model.Config_MenuType),
                new SqlParameter ("@Config_Path",model.Config_Path)
            };
           return Helper.ExecuteNonQuery("Systemconfiguration_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Systemconfiguration">Systemconfiguration实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Systemconfiguration model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Config_MenuType",model.Config_MenuType),
                new SqlParameter ("@Config_Path",model.Config_Path)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Systemconfiguration_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Systemconfiguration">Systemconfiguration实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Systemconfiguration model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Config_ID",model.Config_ID),
                new SqlParameter ("@Config_MenuType",model.Config_MenuType),
                new SqlParameter ("@Config_Path",model.Config_Path)
            };
           return Helper.ExecuteNonQuery("Systemconfiguration_Change",param);
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
                new SqlParameter ("@Config_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Systemconfiguration_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Systemconfiguration> selectAll()
        {
            List<Systemconfiguration> list = new List<Systemconfiguration>();
            Systemconfiguration model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Systemconfiguration_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Systemconfiguration();
                    model.Config_ID = dr["Config_ID"].ToString();
                    if (DBNull.Value!=dr["Config_MenuType"])
                        model.Config_MenuType = dr["Config_MenuType"].ToString();
                    if (DBNull.Value!=dr["Config_Path"])
                        model.Config_Path = dr["Config_Path"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Systemconfiguration实体类对象</returns>
        public Systemconfiguration selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Config_ID",Id)
            };
            Systemconfiguration model = new Systemconfiguration();
            using (SqlDataReader dr = Helper.ExecuteReader("Systemconfiguration_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Config_ID = dr["Config_ID"].ToString();
                    if (DBNull.Value!=dr["Config_MenuType"])
                        model.Config_MenuType = dr["Config_MenuType"].ToString();
                    if (DBNull.Value!=dr["Config_Path"])
                        model.Config_Path = dr["Config_Path"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Systemconfiguration实体类对象</returns>
        public List<Systemconfiguration> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Systemconfiguration> list = new List<Systemconfiguration>();
            Systemconfiguration model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Systemconfiguration_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Systemconfiguration();
                    model.Config_ID = dr["Config_ID"].ToString();
                    if (DBNull.Value!=dr["Config_MenuType"])
                        model.Config_MenuType = dr["Config_MenuType"].ToString();
                    if (DBNull.Value!=dr["Config_Path"])
                        model.Config_Path = dr["Config_Path"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

