using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Role_DetailService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Role_Detail">Role_Detail实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Role_Detail model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@Roles_ID",model.Roles_ID)
            };
           return Helper.ExecuteNonQuery("Role_Detail_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Role_Detail">Role_Detail实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Role_Detail model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@Roles_ID",model.Roles_ID)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Role_Detail_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Role_Detail">Role_Detail实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Role_Detail model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@RolesDet_ID",model.RolesDet_ID),
                new SqlParameter ("@User_ID",model.User_ID),
                new SqlParameter ("@Roles_ID",model.Roles_ID)
            };
           return Helper.ExecuteNonQuery("Role_Detail_Change",param);
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
                new SqlParameter ("@RolesDet_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Role_Detail_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Role_Detail> selectAll()
        {
            List<Role_Detail> list = new List<Role_Detail>();
            Role_Detail model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Role_Detail_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Role_Detail();
                    model.RolesDet_ID = dr["RolesDet_ID"].ToString();
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Role_Detail实体类对象</returns>
        public Role_Detail selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@RolesDet_ID",Id)
            };
            Role_Detail model = new Role_Detail();
            using (SqlDataReader dr = Helper.ExecuteReader("Role_Detail_SelectById", param))
            {
                if (dr.Read())
                {
                    model.RolesDet_ID = dr["RolesDet_ID"].ToString();
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Role_Detail实体类对象</returns>
        public List<Role_Detail> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Role_Detail> list = new List<Role_Detail>();
            Role_Detail model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Role_Detail_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Role_Detail();
                    model.RolesDet_ID = dr["RolesDet_ID"].ToString();
                    if (DBNull.Value!=dr["User_ID"])
                        model.User_ID = dr["User_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

