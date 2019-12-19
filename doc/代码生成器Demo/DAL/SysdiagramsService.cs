using System;
using System.Collections.Generic;
using Models;
using DBHelp;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SysdiagramsService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Sysdiagrams">Sysdiagrams实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool add(Sysdiagrams model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@name",model.Name),
                new SqlParameter ("@principal_id",model.Principal_id),
                new SqlParameter ("@diagram_id",model.Diagram_id),
                new SqlParameter ("@version",model.Version),
                new SqlParameter ("@definition",model.Definition)
            };
           return Helper.ExecuteNonQuery("Sysdiagrams_Add",param);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Sysdiagrams">Sysdiagrams实体对象</param>
        /// <returns>int值,返回自增ID</returns>
        public int addReturnId(Sysdiagrams model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@name",model.Name),
                new SqlParameter ("@principal_id",model.Principal_id),
                new SqlParameter ("@diagram_id",model.Diagram_id),
                new SqlParameter ("@version",model.Version),
                new SqlParameter ("@definition",model.Definition)
            };
           return Convert.ToInt32(Helper.ExecuteScalar ("Sysdiagrams_AddReturnId",param));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Sysdiagrams">Sysdiagrams实体对象</param>
        /// <returns>bool值,判断是否操作成功</returns>
        public bool change(Sysdiagrams model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@name",model.Name),
                new SqlParameter ("@principal_id",model.Principal_id),
                new SqlParameter ("@diagram_id",model.Diagram_id),
                new SqlParameter ("@version",model.Version),
                new SqlParameter ("@definition",model.Definition)
            };
           return Helper.ExecuteNonQuery("Sysdiagrams_Change",param);
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
                new SqlParameter ("@name",Id)
            };
           return Helper.ExecuteNonQuery ("Sysdiagrams_Delete",param);
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <returns>list集合</returns>
        public List<Sysdiagrams> selectAll()
        {
            List<Sysdiagrams> list = new List<Sysdiagrams>();
            Sysdiagrams model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Sysdiagrams_SelectAll", null))
            {
                while (dr.Read())
                {
                    model = new Sysdiagrams();
                    model.Name = dr["name"].ToString();
                    model.Principal_id= Convert.ToInt32(dr["principal_id"]);
                    model.Diagram_id= Convert.ToInt32(dr["diagram_id"]);
                    if (DBNull.Value!=dr["version"])
                        model.Version= Convert.ToInt32(dr["version"]);
                    if (DBNull.Value!=dr["definition"])
                        model.Definition = dr["definition"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id">主键Id</param>
        /// <returns>Sysdiagrams实体类对象</returns>
        public Sysdiagrams selectById(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@name",Id)
            };
            Sysdiagrams model = new Sysdiagrams();
            using (SqlDataReader dr = Helper.ExecuteReader("Sysdiagrams_SelectById", param))
            {
                if (dr.Read())
                {
                    model.Name = dr["name"].ToString();
                    model.Principal_id= Convert.ToInt32(dr["principal_id"]);
                    model.Diagram_id= Convert.ToInt32(dr["diagram_id"]);
                    if (DBNull.Value!=dr["version"])
                        model.Version= Convert.ToInt32(dr["version"]);
                    if (DBNull.Value!=dr["definition"])
                        model.Definition = dr["definition"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 通过条件查询
        /// </summary>
        /// <param name="WhereString">查询条件</param>
        /// <returns>Sysdiagrams实体类对象</returns>
        public List<Sysdiagrams> selectByWhere(string WhereString)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter ("@where",WhereString)
            };
            List<Sysdiagrams> list = new List<Sysdiagrams>();
            Sysdiagrams model = null;
            using (SqlDataReader dr = Helper.ExecuteReader("Sysdiagrams_SelectByWhere", param))
            {
                while (dr.Read())
                {
                    model = new Sysdiagrams();
                    model.Name = dr["name"].ToString();
                    model.Principal_id= Convert.ToInt32(dr["principal_id"]);
                    model.Diagram_id= Convert.ToInt32(dr["diagram_id"]);
                    if (DBNull.Value!=dr["version"])
                        model.Version= Convert.ToInt32(dr["version"]);
                    if (DBNull.Value!=dr["definition"])
                        model.Definition = dr["definition"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

