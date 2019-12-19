using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class FieldControlDetService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="FieldControlDet">FieldControlDet实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(FieldControlDet model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_ID",model.Roles_ID),
                new SqlParameter ("@FieldControl_ID",model.FieldControl_ID)
            };
           return Helper.ExecuteNonQuery("FieldControlDet_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="FieldControlDet">FieldControlDet实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(FieldControlDet model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@Roles_ID",model.Roles_ID),
                new SqlParameter ("@FieldControl_ID",model.FieldControl_ID)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("FieldControlDet_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="FieldControlDet">FieldControlDet实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(FieldControlDet model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@FieldControlDet_ID",model.FieldControlDet_ID),
                new SqlParameter ("@Roles_ID",model.Roles_ID),
                new SqlParameter ("@FieldControl_ID",model.FieldControl_ID)
            };
           return Helper.ExecuteNonQuery("FieldControlDet_Change",param);
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
                new SqlParameter ("@FieldControlDet_ID",Id)
            };
           return Helper.ExecuteNonQuery ("FieldControlDet_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<FieldControlDet> selectAll()
        {
            List<FieldControlDet> list = new List<FieldControlDet>();
            FieldControlDet model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("FieldControlDet_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new FieldControlDet();
                    model.FieldControlDet_ID = dr["FieldControlDet_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["FieldControl_ID"])
                        model.FieldControl_ID = dr["FieldControl_ID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>FieldControlDet实体类对象</returns>
        public FieldControlDet selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@FieldControlDet_ID",Id)
            };
            FieldControlDet model = new FieldControlDet();
            using (SqlDataReader dr = Helper.ExecuteReader("FieldControlDet_SelectById", param))
            {
                if (dr.Read())
                {
                    model.FieldControlDet_ID = dr["FieldControlDet_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["FieldControl_ID"])
                        model.FieldControl_ID = dr["FieldControl_ID"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>FieldControlDet实体类对象</returns>
        public List<FieldControlDet> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<FieldControlDet> list = new List<FieldControlDet>();
            FieldControlDet model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("FieldControlDet_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new FieldControlDet();
                    model.FieldControlDet_ID = dr["FieldControlDet_ID"].ToString();
                    if (DBNull.Value!=dr["Roles_ID"])
                        model.Roles_ID = dr["Roles_ID"].ToString();
                    if (DBNull.Value!=dr["FieldControl_ID"])
                        model.FieldControl_ID = dr["FieldControl_ID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

