using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class RoleRightsService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="RoleRights">RoleRights实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(RoleRights model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_ID",model.Roles_ID),
                new SqlParameter ("@Menu_Id",model.Menu_Id)
            };
           return Helper.ExecuteNonQuery("RoleRights_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="RoleRights">RoleRights实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(RoleRights model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_ID",model.Roles_ID),
                new SqlParameter ("@Menu_Id",model.Menu_Id)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("RoleRights_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="RoleRights">RoleRights实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(RoleRights model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@RoleRight_ID",model.RoleRight_ID),
                new SqlParameter ("@Roles_ID",model.Roles_ID),
                new SqlParameter ("@Menu_Id",model.Menu_Id)
            };
           return Helper.ExecuteNonQuery("RoleRights_Change",param);
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
                new SqlParameter ("@RoleRight_ID",Id)
            };
           return Helper.ExecuteNonQuery ("RoleRights_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<RoleRights> selectAll()
        {
            List<RoleRights> list = new List<RoleRights>();
            RoleRights model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("RoleRights_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new RoleRights();
                    model.RoleRight_ID = dr["RoleRight_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["Menu_Id"])
                        model.Menu_Id = dr["Menu_Id"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>RoleRights实体类对象</returns>
        public RoleRights selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@RoleRight_ID",Id)
            };
            RoleRights model = new RoleRights();
            using (SqlDataReader dr = Helper.ExecuteReader("RoleRights_SelectById", param))
            {
                if (dr.Read())
                {
                    model.RoleRight_ID = dr["RoleRight_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["Menu_Id"])
                        model.Menu_Id = dr["Menu_Id"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>RoleRights实体类对象</returns>
        public List<RoleRights> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<RoleRights> list = new List<RoleRights>();
            RoleRights model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("RoleRights_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new RoleRights();
                    model.RoleRight_ID = dr["RoleRight_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["Menu_Id"])
                        model.Menu_Id = dr["Menu_Id"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

