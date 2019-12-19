using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SysMenusService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="SysMenus">SysMenus实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(SysMenus model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Menu_Name",model.Menu_Name),
                new SqlParameter ("@Menu_Title",model.Menu_Title),
                new SqlParameter ("@Menu_Img",model.Menu_Img),
                new SqlParameter ("@Menu_Type",model.Menu_Type),
                new SqlParameter ("@Menu_Order",model.Menu_Order),
                new SqlParameter ("@Menu_Url",model.Menu_Url),
                new SqlParameter ("@Menu_PanelID",model.Menu_PanelID),
                new SqlParameter ("@Menu_Status",model.Menu_Status)
            };
           return Helper.ExecuteNonQuery("SysMenus_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="SysMenus">SysMenus实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(SysMenus model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Menu_Name",model.Menu_Name),
                new SqlParameter ("@Menu_Title",model.Menu_Title),
                new SqlParameter ("@Menu_Img",model.Menu_Img),
                new SqlParameter ("@Menu_Type",model.Menu_Type),
                new SqlParameter ("@Menu_Order",model.Menu_Order),
                new SqlParameter ("@Menu_Url",model.Menu_Url),
                new SqlParameter ("@Menu_PanelID",model.Menu_PanelID),
                new SqlParameter ("@Menu_Status",model.Menu_Status)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("SysMenus_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="SysMenus">SysMenus实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(SysMenus model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Menu_Id",model.Menu_Id),
                new SqlParameter ("@Menu_Name",model.Menu_Name),
                new SqlParameter ("@Menu_Title",model.Menu_Title),
                new SqlParameter ("@Menu_Img",model.Menu_Img),
                new SqlParameter ("@Menu_Type",model.Menu_Type),
                new SqlParameter ("@Menu_Order",model.Menu_Order),
                new SqlParameter ("@Menu_Url",model.Menu_Url),
                new SqlParameter ("@Menu_PanelID",model.Menu_PanelID),
                new SqlParameter ("@Menu_Status",model.Menu_Status)
            };
           return Helper.ExecuteNonQuery("SysMenus_Change",param);
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
                new SqlParameter ("@Menu_Id",Id)
            };
           return Helper.ExecuteNonQuery ("SysMenus_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<SysMenus> selectAll()
        {
            List<SysMenus> list = new List<SysMenus>();
            SysMenus model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("SysMenus_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new SysMenus();
                    model.Menu_Id = dr["Menu_Id"].ToString();
                    if (DBNull.Value!=dr["Menu_Name"])
                        model.Menu_Name = dr["Menu_Name"].ToString();
                    if (DBNull.Value!=dr["Menu_Title"])
                        model.Menu_Title = dr["Menu_Title"].ToString();
                    if (DBNull.Value!=dr["Menu_Img"])
                        model.Menu_Img = dr["Menu_Img"].ToString();
                    if (DBNull.Value!=dr["Menu_Type"])
                        model.Menu_Type = dr["Menu_Type"].ToString();
                    if (DBNull.Value!=dr["Menu_Order"])
                        model.Menu_Order = dr["Menu_Order"].ToString();
                    if (DBNull.Value!=dr["Menu_Url"])
                        model.Menu_Url = dr["Menu_Url"].ToString();
                    if (DBNull.Value!=dr["Menu_PanelID"])
                        model.Menu_PanelID = dr["Menu_PanelID"].ToString();
                    if (DBNull.Value!=dr["Menu_Status"])
                        model.Menu_Status = dr["Menu_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>SysMenus实体类对象</returns>
        public SysMenus selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Menu_Id",Id)
            };
            SysMenus model = new SysMenus();
            using (SqlDataReader dr = Helper.ExecuteReader("SysMenus_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Menu_Id = dr["Menu_Id"].ToString();
                    if (DBNull.Value!=dr["Menu_Name"])
                        model.Menu_Name = dr["Menu_Name"].ToString();
                    if (DBNull.Value!=dr["Menu_Title"])
                        model.Menu_Title = dr["Menu_Title"].ToString();
                    if (DBNull.Value!=dr["Menu_Img"])
                        model.Menu_Img = dr["Menu_Img"].ToString();
                    if (DBNull.Value!=dr["Menu_Type"])
                        model.Menu_Type = dr["Menu_Type"].ToString();
                    if (DBNull.Value!=dr["Menu_Order"])
                        model.Menu_Order = dr["Menu_Order"].ToString();
                    if (DBNull.Value!=dr["Menu_Url"])
                        model.Menu_Url = dr["Menu_Url"].ToString();
                    if (DBNull.Value!=dr["Menu_PanelID"])
                        model.Menu_PanelID = dr["Menu_PanelID"].ToString();
                    if (DBNull.Value!=dr["Menu_Status"])
                        model.Menu_Status = dr["Menu_Status"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>SysMenus实体类对象</returns>
        public List<SysMenus> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<SysMenus> list = new List<SysMenus>();
            SysMenus model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("SysMenus_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new SysMenus();
                    model.Menu_Id = dr["Menu_Id"].ToString();
                    if (DBNull.Value!=dr["Menu_Name"])
                        model.Menu_Name = dr["Menu_Name"].ToString();
                    if (DBNull.Value!=dr["Menu_Title"])
                        model.Menu_Title = dr["Menu_Title"].ToString();
                    if (DBNull.Value!=dr["Menu_Img"])
                        model.Menu_Img = dr["Menu_Img"].ToString();
                    if (DBNull.Value!=dr["Menu_Type"])
                        model.Menu_Type = dr["Menu_Type"].ToString();
                    if (DBNull.Value!=dr["Menu_Order"])
                        model.Menu_Order = dr["Menu_Order"].ToString();
                    if (DBNull.Value!=dr["Menu_Url"])
                        model.Menu_Url = dr["Menu_Url"].ToString();
                    if (DBNull.Value!=dr["Menu_PanelID"])
                        model.Menu_PanelID = dr["Menu_PanelID"].ToString();
                    if (DBNull.Value!=dr["Menu_Status"])
                        model.Menu_Status = dr["Menu_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

