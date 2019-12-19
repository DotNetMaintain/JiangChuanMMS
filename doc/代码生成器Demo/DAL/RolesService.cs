using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class RolesService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Roles">Roles实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Roles model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_Name",model.Roles_Name),
                new SqlParameter ("@Roles_CreatedTime",model.Roles_CreatedTime),
                new SqlParameter ("@Roles_Remark",model.Roles_Remark),
                new SqlParameter ("@Roles_Status",model.Roles_Status),
                new SqlParameter ("@Roles_PanelID",model.Roles_PanelID)
            };
           return Helper.ExecuteNonQuery("Roles_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Roles">Roles实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Roles model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_Name",model.Roles_Name),
                new SqlParameter ("@Roles_CreatedTime",model.Roles_CreatedTime),
                new SqlParameter ("@Roles_Remark",model.Roles_Remark),
                new SqlParameter ("@Roles_Status",model.Roles_Status),
                new SqlParameter ("@Roles_PanelID",model.Roles_PanelID)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Roles_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Roles">Roles实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Roles model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_ID",model.Roles_ID),
                new SqlParameter ("@Roles_Name",model.Roles_Name),
                new SqlParameter ("@Roles_CreatedTime",model.Roles_CreatedTime),
                new SqlParameter ("@Roles_Remark",model.Roles_Remark),
                new SqlParameter ("@Roles_Status",model.Roles_Status),
                new SqlParameter ("@Roles_PanelID",model.Roles_PanelID)
            };
           return Helper.ExecuteNonQuery("Roles_Change",param);
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
                new SqlParameter ("@Roles_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Roles_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Roles> selectAll()
        {
            List<Roles> list = new List<Roles>();
            Roles model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Roles_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Roles();
                    model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_Name"])
                        model.Roles_Name = dr["Roles_Name"].ToString();
                    if (DBNull.Value!=dr["Roles_CreatedTime"])
                        model.Roles_CreatedTime= Convert.ToDateTime(dr["Roles_CreatedTime"]);
                    if (DBNull.Value!=dr["Roles_Remark"])
                        model.Roles_Remark = dr["Roles_Remark"].ToString();
                    if (DBNull.Value!=dr["Roles_Status"])
                        model.Roles_Status = dr["Roles_Status"].ToString();
                    if (DBNull.Value!=dr["Roles_PanelID"])
                        model.Roles_PanelID = dr["Roles_PanelID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Roles实体类对象</returns>
        public Roles selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_ID",Id)
            };
            Roles model = new Roles();
            using (SqlDataReader dr = Helper.ExecuteReader("Roles_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_Name"])
                        model.Roles_Name = dr["Roles_Name"].ToString();
                    if (DBNull.Value!=dr["Roles_CreatedTime"])
                        model.Roles_CreatedTime= Convert.ToDateTime(dr["Roles_CreatedTime"]);
                    if (DBNull.Value!=dr["Roles_Remark"])
                        model.Roles_Remark = dr["Roles_Remark"].ToString();
                    if (DBNull.Value!=dr["Roles_Status"])
                        model.Roles_Status = dr["Roles_Status"].ToString();
                    if (DBNull.Value!=dr["Roles_PanelID"])
                        model.Roles_PanelID = dr["Roles_PanelID"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Roles实体类对象</returns>
        public List<Roles> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Roles> list = new List<Roles>();
            Roles model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Roles_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Roles();
                    model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_Name"])
                        model.Roles_Name = dr["Roles_Name"].ToString();
                    if (DBNull.Value!=dr["Roles_CreatedTime"])
                        model.Roles_CreatedTime= Convert.ToDateTime(dr["Roles_CreatedTime"]);
                    if (DBNull.Value!=dr["Roles_Remark"])
                        model.Roles_Remark = dr["Roles_Remark"].ToString();
                    if (DBNull.Value!=dr["Roles_Status"])
                        model.Roles_Status = dr["Roles_Status"].ToString();
                    if (DBNull.Value!=dr["Roles_PanelID"])
                        model.Roles_PanelID = dr["Roles_PanelID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

