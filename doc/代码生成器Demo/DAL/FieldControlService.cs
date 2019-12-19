using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class FieldControlService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="FieldControl">FieldControl实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(FieldControl model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Menu_Id",model.Menu_Id),
                new SqlParameter ("@FieldControl_Name",model.FieldControl_Name),
                new SqlParameter ("@FieldControl_DataPKValue",model.FieldControl_DataPKValue),
                new SqlParameter ("@FieldControl_Onclick",model.FieldControl_Onclick),
                new SqlParameter ("@FieldControl_Order",model.FieldControl_Order),
                new SqlParameter ("@FieldControl_Css",model.FieldControl_Css)
            };
           return Helper.ExecuteNonQuery("FieldControl_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="FieldControl">FieldControl实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(FieldControl model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Menu_Id",model.Menu_Id),
                new SqlParameter ("@FieldControl_Name",model.FieldControl_Name),
                new SqlParameter ("@FieldControl_DataPKValue",model.FieldControl_DataPKValue),
                new SqlParameter ("@FieldControl_Onclick",model.FieldControl_Onclick),
                new SqlParameter ("@FieldControl_Order",model.FieldControl_Order),
                new SqlParameter ("@FieldControl_Css",model.FieldControl_Css)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("FieldControl_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="FieldControl">FieldControl实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(FieldControl model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@FieldControl_ID",model.FieldControl_ID),
                new SqlParameter ("@Menu_Id",model.Menu_Id),
                new SqlParameter ("@FieldControl_Name",model.FieldControl_Name),
                new SqlParameter ("@FieldControl_DataPKValue",model.FieldControl_DataPKValue),
                new SqlParameter ("@FieldControl_Onclick",model.FieldControl_Onclick),
                new SqlParameter ("@FieldControl_Order",model.FieldControl_Order),
                new SqlParameter ("@FieldControl_Css",model.FieldControl_Css)
            };
           return Helper.ExecuteNonQuery("FieldControl_Change",param);
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
                new SqlParameter ("@FieldControl_ID",Id)
            };
           return Helper.ExecuteNonQuery ("FieldControl_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<FieldControl> selectAll()
        {
            List<FieldControl> list = new List<FieldControl>();
            FieldControl model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("FieldControl_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new FieldControl();
                    model.FieldControl_ID = dr["FieldControl_ID"].ToString();
                    if (DBNull.Value!=dr["Menu_Id"])
                        model.Menu_Id = dr["Menu_Id"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Name"])
                        model.FieldControl_Name = dr["FieldControl_Name"].ToString();
                    if (DBNull.Value!=dr["FieldControl_DataPKValue"])
                        model.FieldControl_DataPKValue = dr["FieldControl_DataPKValue"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Onclick"])
                        model.FieldControl_Onclick = dr["FieldControl_Onclick"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Order"])
                        model.FieldControl_Order = dr["FieldControl_Order"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Css"])
                        model.FieldControl_Css = dr["FieldControl_Css"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>FieldControl实体类对象</returns>
        public FieldControl selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@FieldControl_ID",Id)
            };
            FieldControl model = new FieldControl();
            using (SqlDataReader dr = Helper.ExecuteReader("FieldControl_SelectById", param))
            {
                if (dr.Read())
                {
                    model.FieldControl_ID = dr["FieldControl_ID"].ToString();
                    if (DBNull.Value!=dr["Menu_Id"])
                        model.Menu_Id = dr["Menu_Id"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Name"])
                        model.FieldControl_Name = dr["FieldControl_Name"].ToString();
                    if (DBNull.Value!=dr["FieldControl_DataPKValue"])
                        model.FieldControl_DataPKValue = dr["FieldControl_DataPKValue"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Onclick"])
                        model.FieldControl_Onclick = dr["FieldControl_Onclick"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Order"])
                        model.FieldControl_Order = dr["FieldControl_Order"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Css"])
                        model.FieldControl_Css = dr["FieldControl_Css"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>FieldControl实体类对象</returns>
        public List<FieldControl> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<FieldControl> list = new List<FieldControl>();
            FieldControl model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("FieldControl_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new FieldControl();
                    model.FieldControl_ID = dr["FieldControl_ID"].ToString();
                    if (DBNull.Value!=dr["Menu_Id"])
                        model.Menu_Id = dr["Menu_Id"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Name"])
                        model.FieldControl_Name = dr["FieldControl_Name"].ToString();
                    if (DBNull.Value!=dr["FieldControl_DataPKValue"])
                        model.FieldControl_DataPKValue = dr["FieldControl_DataPKValue"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Onclick"])
                        model.FieldControl_Onclick = dr["FieldControl_Onclick"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Order"])
                        model.FieldControl_Order = dr["FieldControl_Order"].ToString();
                    if (DBNull.Value!=dr["FieldControl_Css"])
                        model.FieldControl_Css = dr["FieldControl_Css"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

