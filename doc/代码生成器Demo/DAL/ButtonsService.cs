using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ButtonsService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Buttons">Buttons实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Buttons model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Botton_Name",model.Botton_Name),
                new SqlParameter ("@Botton_Title",model.Botton_Title),
                new SqlParameter ("@Botton_Img",model.Botton_Img),
                new SqlParameter ("@Botton_Code",model.Botton_Code),
                new SqlParameter ("@Botton_Order",model.Botton_Order),
                new SqlParameter ("@Botton_Remak",model.Botton_Remak),
                new SqlParameter ("@Botton_Type",model.Botton_Type),
                new SqlParameter ("@Botton_Status",model.Botton_Status)
            };
           return Helper.ExecuteNonQuery("Buttons_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Buttons">Buttons实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Buttons model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Botton_Name",model.Botton_Name),
                new SqlParameter ("@Botton_Title",model.Botton_Title),
                new SqlParameter ("@Botton_Img",model.Botton_Img),
                new SqlParameter ("@Botton_Code",model.Botton_Code),
                new SqlParameter ("@Botton_Order",model.Botton_Order),
                new SqlParameter ("@Botton_Remak",model.Botton_Remak),
                new SqlParameter ("@Botton_Type",model.Botton_Type),
                new SqlParameter ("@Botton_Status",model.Botton_Status)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Buttons_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Buttons">Buttons实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Buttons model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Botton_ID",model.Botton_ID),
                new SqlParameter ("@Botton_Name",model.Botton_Name),
                new SqlParameter ("@Botton_Title",model.Botton_Title),
                new SqlParameter ("@Botton_Img",model.Botton_Img),
                new SqlParameter ("@Botton_Code",model.Botton_Code),
                new SqlParameter ("@Botton_Order",model.Botton_Order),
                new SqlParameter ("@Botton_Remak",model.Botton_Remak),
                new SqlParameter ("@Botton_Type",model.Botton_Type),
                new SqlParameter ("@Botton_Status",model.Botton_Status)
            };
           return Helper.ExecuteNonQuery("Buttons_Change",param);
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
                new SqlParameter ("@Botton_ID",Id)
            };
           return Helper.ExecuteNonQuery ("Buttons_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Buttons> selectAll()
        {
            List<Buttons> list = new List<Buttons>();
            Buttons model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Buttons_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Buttons();
                    model.Botton_ID = dr["Botton_ID"].ToString();
                    if (DBNull.Value!=dr["Botton_Name"])
                        model.Botton_Name = dr["Botton_Name"].ToString();
                    if (DBNull.Value!=dr["Botton_Title"])
                        model.Botton_Title = dr["Botton_Title"].ToString();
                    if (DBNull.Value!=dr["Botton_Img"])
                        model.Botton_Img = dr["Botton_Img"].ToString();
                    if (DBNull.Value!=dr["Botton_Code"])
                        model.Botton_Code = dr["Botton_Code"].ToString();
                    if (DBNull.Value!=dr["Botton_Order"])
                        model.Botton_Order = dr["Botton_Order"].ToString();
                    if (DBNull.Value!=dr["Botton_Remak"])
                        model.Botton_Remak = dr["Botton_Remak"].ToString();
                    if (DBNull.Value!=dr["Botton_Type"])
                        model.Botton_Type = dr["Botton_Type"].ToString();
                    if (DBNull.Value!=dr["Botton_Status"])
                        model.Botton_Status = dr["Botton_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Buttons实体类对象</returns>
        public Buttons selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Botton_ID",Id)
            };
            Buttons model = new Buttons();
            using (SqlDataReader dr = Helper.ExecuteReader("Buttons_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Botton_ID = dr["Botton_ID"].ToString();
                    if (DBNull.Value!=dr["Botton_Name"])
                        model.Botton_Name = dr["Botton_Name"].ToString();
                    if (DBNull.Value!=dr["Botton_Title"])
                        model.Botton_Title = dr["Botton_Title"].ToString();
                    if (DBNull.Value!=dr["Botton_Img"])
                        model.Botton_Img = dr["Botton_Img"].ToString();
                    if (DBNull.Value!=dr["Botton_Code"])
                        model.Botton_Code = dr["Botton_Code"].ToString();
                    if (DBNull.Value!=dr["Botton_Order"])
                        model.Botton_Order = dr["Botton_Order"].ToString();
                    if (DBNull.Value!=dr["Botton_Remak"])
                        model.Botton_Remak = dr["Botton_Remak"].ToString();
                    if (DBNull.Value!=dr["Botton_Type"])
                        model.Botton_Type = dr["Botton_Type"].ToString();
                    if (DBNull.Value!=dr["Botton_Status"])
                        model.Botton_Status = dr["Botton_Status"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Buttons实体类对象</returns>
        public List<Buttons> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Buttons> list = new List<Buttons>();
            Buttons model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Buttons_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Buttons();
                    model.Botton_ID = dr["Botton_ID"].ToString();
                    if (DBNull.Value!=dr["Botton_Name"])
                        model.Botton_Name = dr["Botton_Name"].ToString();
                    if (DBNull.Value!=dr["Botton_Title"])
                        model.Botton_Title = dr["Botton_Title"].ToString();
                    if (DBNull.Value!=dr["Botton_Img"])
                        model.Botton_Img = dr["Botton_Img"].ToString();
                    if (DBNull.Value!=dr["Botton_Code"])
                        model.Botton_Code = dr["Botton_Code"].ToString();
                    if (DBNull.Value!=dr["Botton_Order"])
                        model.Botton_Order = dr["Botton_Order"].ToString();
                    if (DBNull.Value!=dr["Botton_Remak"])
                        model.Botton_Remak = dr["Botton_Remak"].ToString();
                    if (DBNull.Value!=dr["Botton_Type"])
                        model.Botton_Type = dr["Botton_Type"].ToString();
                    if (DBNull.Value!=dr["Botton_Status"])
                        model.Botton_Status = dr["Botton_Status"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

