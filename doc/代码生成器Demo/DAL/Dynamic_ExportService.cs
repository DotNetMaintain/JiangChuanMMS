using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Dynamic_ExportService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Dynamic_Export">Dynamic_Export实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Dynamic_Export model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Dynamic_Export_Name",model.Dynamic_Export_Name),
                new SqlParameter ("@Dynamic_Export_Titlebar",model.Dynamic_Export_Titlebar),
                new SqlParameter ("@Dynamic_Export_DataSource",model.Dynamic_Export_DataSource),
                new SqlParameter ("@Dynamic_Export_ClmName",model.Dynamic_Export_ClmName),
                new SqlParameter ("@Dynamic_Export_FieldName",model.Dynamic_Export_FieldName),
                new SqlParameter ("@Dynamic_Export_ClmType",model.Dynamic_Export_ClmType),
                new SqlParameter ("@Dynamic_Export_Clmwidth",model.Dynamic_Export_Clmwidth),
                new SqlParameter ("@Dynamic_Export_Order",model.Dynamic_Export_Order),
                new SqlParameter ("@Dynamic_Export_Mome",model.Dynamic_Export_Mome)
            };
           return Helper.ExecuteNonQuery("Dynamic_Export_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Dynamic_Export">Dynamic_Export实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Dynamic_Export model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Dynamic_Export_Name",model.Dynamic_Export_Name),
                new SqlParameter ("@Dynamic_Export_Titlebar",model.Dynamic_Export_Titlebar),
                new SqlParameter ("@Dynamic_Export_DataSource",model.Dynamic_Export_DataSource),
                new SqlParameter ("@Dynamic_Export_ClmName",model.Dynamic_Export_ClmName),
                new SqlParameter ("@Dynamic_Export_FieldName",model.Dynamic_Export_FieldName),
                new SqlParameter ("@Dynamic_Export_ClmType",model.Dynamic_Export_ClmType),
                new SqlParameter ("@Dynamic_Export_Clmwidth",model.Dynamic_Export_Clmwidth),
                new SqlParameter ("@Dynamic_Export_Order",model.Dynamic_Export_Order),
                new SqlParameter ("@Dynamic_Export_Mome",model.Dynamic_Export_Mome)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Dynamic_Export_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Dynamic_Export">Dynamic_Export实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Dynamic_Export model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Dynamic_Export_ID",model.Dynamic_Export_ID),
                new SqlParameter ("@Dynamic_Export_Name",model.Dynamic_Export_Name),
                new SqlParameter ("@Dynamic_Export_Titlebar",model.Dynamic_Export_Titlebar),
                new SqlParameter ("@Dynamic_Export_DataSource",model.Dynamic_Export_DataSource),
                new SqlParameter ("@Dynamic_Export_ClmName",model.Dynamic_Export_ClmName),
                new SqlParameter ("@Dynamic_Export_FieldName",model.Dynamic_Export_FieldName),
                new SqlParameter ("@Dynamic_Export_ClmType",model.Dynamic_Export_ClmType),
                new SqlParameter ("@Dynamic_Export_Clmwidth",model.Dynamic_Export_Clmwidth),
                new SqlParameter ("@Dynamic_Export_Order",model.Dynamic_Export_Order),
                new SqlParameter ("@Dynamic_Export_Mome",model.Dynamic_Export_Mome)
            };
           return Helper.ExecuteNonQuery("Dynamic_Export_Change",param);
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
                new SqlParameter ("@Dynamic_Export_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Dynamic_Export_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Dynamic_Export> selectAll()
        {
            List<Dynamic_Export> list = new List<Dynamic_Export>();
            Dynamic_Export model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Dynamic_Export_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Dynamic_Export();
                    model.Dynamic_Export_ID = dr["Dynamic_Export_ID"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Name"])
                        model.Dynamic_Export_Name = dr["Dynamic_Export_Name"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Titlebar"])
                        model.Dynamic_Export_Titlebar = dr["Dynamic_Export_Titlebar"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_DataSource"])
                        model.Dynamic_Export_DataSource = dr["Dynamic_Export_DataSource"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_ClmName"])
                        model.Dynamic_Export_ClmName = dr["Dynamic_Export_ClmName"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_FieldName"])
                        model.Dynamic_Export_FieldName = dr["Dynamic_Export_FieldName"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_ClmType"])
                        model.Dynamic_Export_ClmType = dr["Dynamic_Export_ClmType"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Clmwidth"])
                        model.Dynamic_Export_Clmwidth = dr["Dynamic_Export_Clmwidth"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Order"])
                        model.Dynamic_Export_Order = dr["Dynamic_Export_Order"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Mome"])
                        model.Dynamic_Export_Mome = dr["Dynamic_Export_Mome"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Dynamic_Export实体类对象</returns>
        public Dynamic_Export selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Dynamic_Export_ID",Id)
            };
            Dynamic_Export model = new Dynamic_Export();
            using (SqlDataReader dr = Helper.ExecuteReader("Dynamic_Export_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Dynamic_Export_ID = dr["Dynamic_Export_ID"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Name"])
                        model.Dynamic_Export_Name = dr["Dynamic_Export_Name"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Titlebar"])
                        model.Dynamic_Export_Titlebar = dr["Dynamic_Export_Titlebar"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_DataSource"])
                        model.Dynamic_Export_DataSource = dr["Dynamic_Export_DataSource"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_ClmName"])
                        model.Dynamic_Export_ClmName = dr["Dynamic_Export_ClmName"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_FieldName"])
                        model.Dynamic_Export_FieldName = dr["Dynamic_Export_FieldName"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_ClmType"])
                        model.Dynamic_Export_ClmType = dr["Dynamic_Export_ClmType"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Clmwidth"])
                        model.Dynamic_Export_Clmwidth = dr["Dynamic_Export_Clmwidth"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Order"])
                        model.Dynamic_Export_Order = dr["Dynamic_Export_Order"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Mome"])
                        model.Dynamic_Export_Mome = dr["Dynamic_Export_Mome"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Dynamic_Export实体类对象</returns>
        public List<Dynamic_Export> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Dynamic_Export> list = new List<Dynamic_Export>();
            Dynamic_Export model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Dynamic_Export_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Dynamic_Export();
                    model.Dynamic_Export_ID = dr["Dynamic_Export_ID"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Name"])
                        model.Dynamic_Export_Name = dr["Dynamic_Export_Name"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Titlebar"])
                        model.Dynamic_Export_Titlebar = dr["Dynamic_Export_Titlebar"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_DataSource"])
                        model.Dynamic_Export_DataSource = dr["Dynamic_Export_DataSource"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_ClmName"])
                        model.Dynamic_Export_ClmName = dr["Dynamic_Export_ClmName"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_FieldName"])
                        model.Dynamic_Export_FieldName = dr["Dynamic_Export_FieldName"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_ClmType"])
                        model.Dynamic_Export_ClmType = dr["Dynamic_Export_ClmType"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Clmwidth"])
                        model.Dynamic_Export_Clmwidth = dr["Dynamic_Export_Clmwidth"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Order"])
                        model.Dynamic_Export_Order = dr["Dynamic_Export_Order"].ToString();
                    if (DBNull.Value!=dr["Dynamic_Export_Mome"])
                        model.Dynamic_Export_Mome = dr["Dynamic_Export_Mome"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

