using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Tally_SortService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Tally_Sort">Tally_Sort实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Tally_Sort model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Sort_Name",model.Sort_Name),
                new SqlParameter ("@Sort_Order",model.Sort_Order),
                new SqlParameter ("@Sort_PanelID",model.Sort_PanelID)
            };
           return Helper.ExecuteNonQuery("Tally_Sort_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Tally_Sort">Tally_Sort实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Tally_Sort model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Sort_Name",model.Sort_Name),
                new SqlParameter ("@Sort_Order",model.Sort_Order),
                new SqlParameter ("@Sort_PanelID",model.Sort_PanelID)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Tally_Sort_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Tally_Sort">Tally_Sort实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Tally_Sort model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Sort_ID",model.Sort_ID),
                new SqlParameter ("@Sort_Name",model.Sort_Name),
                new SqlParameter ("@Sort_Order",model.Sort_Order),
                new SqlParameter ("@Sort_PanelID",model.Sort_PanelID)
            };
           return Helper.ExecuteNonQuery("Tally_Sort_Change",param);
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
                new SqlParameter ("@Sort_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Tally_Sort_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Tally_Sort> selectAll()
        {
            List<Tally_Sort> list = new List<Tally_Sort>();
            Tally_Sort model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Tally_Sort_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Tally_Sort();
                    model.Sort_ID = dr["Sort_ID"].ToString();
                    if (DBNull.Value!=dr["Sort_Name"])
                        model.Sort_Name = dr["Sort_Name"].ToString();
                    if (DBNull.Value!=dr["Sort_Order"])
                        model.Sort_Order = dr["Sort_Order"].ToString();
                    if (DBNull.Value!=dr["Sort_PanelID"])
                        model.Sort_PanelID = dr["Sort_PanelID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Tally_Sort实体类对象</returns>
        public Tally_Sort selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Sort_ID",Id)
            };
            Tally_Sort model = new Tally_Sort();
            using (SqlDataReader dr = Helper.ExecuteReader("Tally_Sort_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Sort_ID = dr["Sort_ID"].ToString();
                    if (DBNull.Value!=dr["Sort_Name"])
                        model.Sort_Name = dr["Sort_Name"].ToString();
                    if (DBNull.Value!=dr["Sort_Order"])
                        model.Sort_Order = dr["Sort_Order"].ToString();
                    if (DBNull.Value!=dr["Sort_PanelID"])
                        model.Sort_PanelID = dr["Sort_PanelID"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Tally_Sort实体类对象</returns>
        public List<Tally_Sort> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Tally_Sort> list = new List<Tally_Sort>();
            Tally_Sort model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Tally_Sort_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Tally_Sort();
                    model.Sort_ID = dr["Sort_ID"].ToString();
                    if (DBNull.Value!=dr["Sort_Name"])
                        model.Sort_Name = dr["Sort_Name"].ToString();
                    if (DBNull.Value!=dr["Sort_Order"])
                        model.Sort_Order = dr["Sort_Order"].ToString();
                    if (DBNull.Value!=dr["Sort_PanelID"])
                        model.Sort_PanelID = dr["Sort_PanelID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

