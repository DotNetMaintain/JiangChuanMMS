using RM.Busines.DAO;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetEncrypt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RM.Busines.DAL
{
    public class MMS_MaterialInfo_Dal : MMS_MaterialInfo_IDAO
    {

        public MMS_MaterialInfo_Dal()
		{}

        #region Model

        private string _Material_ID; // 物料编号 
        private string _Material_Code; // 物料编码 
        private string _Material_Type; // 物料类型 
        private string _Material_Name; // 物料名称 
        private string _Material_CommonlyName; // 物料常用名 
        private string _Material_Specification; // 物料规格 
        private string _Material_Unit; // 物料单位 
        private string _Material_Supplier; // 生产厂家 
        private string _Material_Pic; // 物料图片路径 
        private string _Material_ApplyAttr; // 物料申领类型 
        private string _Material_Attr01; // 物料属性 
        private string _Material_Attr02; //  
        private string _Material_Attr03; //  
        private string _Material_Attr04; //  
        private DateTime _CreateDate; // 创建时间 
        private string _CreateUserId; // 创建人员ID 
        private DateTime _ModifyDate; // 修改时间 
        private string _ModifyUserId; // 修改人员 

        /// <summary>
        ///物料编号
        /// </summary>  
        public string Material_ID
        {
            get { return _Material_ID; }
            set
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    throw new Exception("Material_ID不能为空");
                }
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_ID字段长度不能超过50");
                }
                _Material_ID = value;
            }
        }

        /// <summary>
        ///物料编码
        /// </summary>  
        public string Material_Code
        {
            get { return _Material_Code; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_Code字段长度不能超过50");
                }
                _Material_Code = value;
            }
        }

        /// <summary>
        ///物料类型
        /// </summary>  
        public string Material_Type
        {
            get { return _Material_Type; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_Type字段长度不能超过50");
                }
                _Material_Type = value;
            }
        }

        /// <summary>
        ///物料名称
        /// </summary>  
        public string Material_Name
        {
            get { return _Material_Name; }
            set
            {
                if (value.ToString().Length > 200)
                {
                    throw new Exception("Material_Name字段长度不能超过200");
                }
                _Material_Name = value;
            }
        }

        /// <summary>
        ///物料常用名
        /// </summary>  
        public string Material_CommonlyName
        {
            get { return _Material_CommonlyName; }
            set
            {
                if (value.ToString().Length > 200)
                {
                    throw new Exception("Material_CommonlyName字段长度不能超过200");
                }
                _Material_CommonlyName = value;
            }
        }

        /// <summary>
        ///物料规格
        /// </summary>  
        public string Material_Specification
        {
            get { return _Material_Specification; }
            set
            {
                if (value.ToString().Length > 200)
                {
                    throw new Exception("Material_Specification字段长度不能超过200");
                }
                _Material_Specification = value;
            }
        }

        /// <summary>
        ///物料单位
        /// </summary>  
        public string Material_Unit
        {
            get { return _Material_Unit; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_Unit字段长度不能超过50");
                }
                _Material_Unit = value;
            }
        }

        /// <summary>
        ///生产厂家
        /// </summary>  
        public string Material_Supplier
        {
            get { return _Material_Supplier; }
            set
            {
                if (value.ToString().Length > 200)
                {
                    throw new Exception("Material_Supplier字段长度不能超过200");
                }
                _Material_Supplier = value;
            }
        }

        /// <summary>
        ///物料图片路径
        /// </summary>  
        public string Material_Pic
        {
            get { return _Material_Pic; }
            set
            {
                if (value.ToString().Length > 200)
                {
                    throw new Exception("Material_Pic字段长度不能超过200");
                }
                _Material_Pic = value;
            }
        }

        /// <summary>
        ///物料申领类型
        /// </summary>  
        public string Material_ApplyAttr
        {
            get { return _Material_ApplyAttr; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_ApplyAttr字段长度不能超过50");
                }
                _Material_ApplyAttr = value;
            }
        }

        /// <summary>
        ///物料属性
        /// </summary>  
        public string Material_Attr01
        {
            get { return _Material_Attr01; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_Attr01字段长度不能超过50");
                }
                _Material_Attr01 = value;
            }
        }

        /// <summary>
        ///
        /// </summary>  
        public string Material_Attr02
        {
            get { return _Material_Attr02; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_Attr02字段长度不能超过50");
                }
                _Material_Attr02 = value;
            }
        }

        /// <summary>
        ///
        /// </summary>  
        public string Material_Attr03
        {
            get { return _Material_Attr03; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_Attr03字段长度不能超过50");
                }
                _Material_Attr03 = value;
            }
        }

        /// <summary>
        ///
        /// </summary>  
        public string Material_Attr04
        {
            get { return _Material_Attr04; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("Material_Attr04字段长度不能超过50");
                }
                _Material_Attr04 = value;
            }
        }

        /// <summary>
        ///创建时间
        /// </summary>  
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (value.ToString().Length > 8)
                {
                    throw new Exception("CreateDate字段长度不能超过8");
                }
                _CreateDate = value;
            }
        }

        /// <summary>
        ///创建人员ID
        /// </summary>  
        public string CreateUserId
        {
            get { return _CreateUserId; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("CreateUserId字段长度不能超过50");
                }
                _CreateUserId = value;
            }
        }

        /// <summary>
        ///修改时间
        /// </summary>  
        public DateTime ModifyDate
        {
            get { return _ModifyDate; }
            set
            {
                if (value.ToString().Length > 8)
                {
                    throw new Exception("ModifyDate字段长度不能超过8");
                }
                _ModifyDate = value;
            }
        }

        /// <summary>
        ///修改人员
        /// </summary>  
        public string ModifyUserId
        {
            get { return _ModifyUserId; }
            set
            {
                if (value.ToString().Length > 50)
                {
                    throw new Exception("ModifyUserId字段长度不能超过50");
                }
                _ModifyUserId = value;
            }
        }


        #endregion Model


        #region  Method


        public MMS_MaterialInfo_Dal (string Material_ID)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("Select * from dbo.MMS_MaterialInfo where ");
        strSql.Append("Material_ID=@Material_ID ");
       // strSql.Append("and DeleteMark!=0");
        SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@Material_ID", Material_ID),
			};
        DataTable dt_MaterialInfo= DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);

        if (dt_MaterialInfo.Rows[0]["Material_ID"] != null && dt_MaterialInfo.Rows[0]["Material_ID"].ToString() != "")
        {
            this.Material_ID = dt_MaterialInfo.Rows[0]["Material_ID"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_Code"] != null && dt_MaterialInfo.Rows[0]["Material_Code"].ToString() != "")
        {
            this.Material_Code = dt_MaterialInfo.Rows[0]["Material_Code"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_Type"] != null && dt_MaterialInfo.Rows[0]["Material_Type"].ToString() != "")
        {
            this.Material_Type = dt_MaterialInfo.Rows[0]["Material_Type"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_Name"] != null && dt_MaterialInfo.Rows[0]["Material_Name"].ToString() != "")
        {
            this.Material_Name = dt_MaterialInfo.Rows[0]["Material_Name"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_CommonlyName"] != null && dt_MaterialInfo.Rows[0]["Material_CommonlyName"].ToString() != "")
        {
            this.Material_CommonlyName = dt_MaterialInfo.Rows[0]["Material_CommonlyName"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_Specification"] != null && dt_MaterialInfo.Rows[0]["Material_Specification"].ToString() != "")
        {
            this.Material_Specification = dt_MaterialInfo.Rows[0]["Material_Specification"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_Unit"] != null && dt_MaterialInfo.Rows[0]["Material_Unit"].ToString() != "")
        {
            this.Material_Unit = dt_MaterialInfo.Rows[0]["Material_Unit"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_Supplier"] != null && dt_MaterialInfo.Rows[0]["Material_Supplier"].ToString() != "")
        {
            this.Material_Supplier = dt_MaterialInfo.Rows[0]["Material_Supplier"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_Pic"] != null && dt_MaterialInfo.Rows[0]["Material_Pic"].ToString() != "")
        {
            this.Material_Pic = dt_MaterialInfo.Rows[0]["Material_Pic"].ToString().Trim();
        }
        if (dt_MaterialInfo.Rows[0]["Material_ApplyAttr"] != null && dt_MaterialInfo.Rows[0]["Material_ApplyAttr"].ToString() != "")
        {
            this.Material_ApplyAttr = dt_MaterialInfo.Rows[0]["Material_ApplyAttr"].ToString().Trim();
        }


           



    }


        public DataTable Load_MaterialInfoList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM dbo.MMS_MaterialInfo WHERE 1 =1 ORDER BY Material_Name ASC");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }


        public DataTable GetMaterialInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from MMS_MaterialInfo U where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "Desc", pageIndex, pageSize, ref count);
        }

        public DataTable GetMaterialInfo(StringBuilder SqlWhere, IList<SqlParam> IList_param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from dbo.MMS_MaterialInfo where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, IList_param.ToArray<SqlParam>());
        }


        public DataTable GetMaterialTypeInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (select mt.MaterialType_ID typeid,mi.* from MMS_MaterialType mt inner join MMS_MaterialInfo mi on mt.MaterialType_Name=substring(mi.Material_Type,1,CharIndex ('-',material_type)-1) where mi.DeleteMark='1') U where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "Material_Name", "Desc", pageIndex, pageSize, ref count);
        }

        public DataTable GetStorageMaterialInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (select mms.materialtype_id,mms.Material_ID,mms.material_name,mms.Material_CommonlyName,Material_Specification,Material_Unit,Material_Supplier,Material_Type,Material_Pic,mms.DeleteMark,pur.*,mms.Material_SafetyStock,mms.modifydate  CreateDate from (select ma.material_id,ma.material_type,ma.material_name,ma.material_commonlyname,ma.material_specification,
ma.material_unit,ma.material_supplier,ma.material_pic,ma.material_comm,ma.material_applyattr,ma.material_attr01,
ma.material_attr02,ma.material_attr03,ma.material_attr04,ma.material_safetystock,ma.createdate,ma.createuserid,
ma.modifydate,ma.modifyuserid,ma.deletemark,matype.materialtype_id from dbo.MMS_MaterialInfo ma inner join  dbo.MMS_MaterialType matype
on substring(ma.material_type,0,charindex('-',ma.material_type)-1)=matype.materialtype_name) mms inner join (
                        select pur.productcode,(isnull(pur.qua,0)-isnull(purplan.qua,0)) as qua,pur.price from (select productcode,sum(quantity) as  qua,price from MMS_PurchaseDetail group by productcode,price) pur
                        left join (select * from (select productcode,sum(quantity) as qua,price from (
select detail.* from MMS_PurchasePlanContent plancontent 
inner join MMS_PurchasePlanDetail  detail
on plancontent.PurchaseBillCode=detail.PurchaseBillCode
where plancontent.paymode in ('1','2') and detail.AuditFlag is NULL) de
group by productcode,price) purplan)purplan
                        on pur.ProductCode=purplan.ProductCode and pur.price=purplan.price) pur
                        on mms.material_id=pur.productcode
                        ) U where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "Desc", pageIndex, pageSize, ref count);
        }


        public DataTable GetStorageMaterialInfo(StringBuilder SqlWhere, IList<SqlParam> IList_param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from (select mms.materialtype_id,mms.Material_ID,mms.material_name,mms.Material_CommonlyName,Material_Specification,Material_Unit,Material_Supplier,Material_Type,pur.*,mms.modifydate  CreateDate from dbo.MMS_MaterialInfo mms inner join (
                        select pur.productcode,(isnull(pur.qua,0)-isnull(purplan.qua,0)) as qua,pur.price from (select productcode,sum(quantity) as  qua,price from MMS_PurchaseDetail group by productcode,price) pur
                        left join (select * from (select productcode,sum(quantity) as qua,price from (
select detail.* from MMS_PurchasePlanContent plancontent 
inner join MMS_PurchasePlanDetail  detail
on plancontent.PurchaseBillCode=detail.PurchaseBillCode
where plancontent.paymode in ('1','2') and detail.AuditFlag is NULL) de
group by productcode,price) purplan)purplan
                        on pur.ProductCode=purplan.ProductCode and pur.price=purplan.price) pur
                        on mms.material_id=pur.productcode
                        where (qua>0 or qua is null)) U where 1 =1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, IList_param.ToArray<SqlParam>());
        }


        public bool Add_MaterialAllotMember(string[] pkVal, string Material_ID)
        {
            bool result;
            try
            {
                StringBuilder[] sqls = new StringBuilder[pkVal.Length + 1];
                object[] objs = new object[pkVal.Length + 1];
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.Append("Delete From MMS_MaterialInfo Where Material_ID =@Material_ID");
                SqlParam[] parm = new SqlParam[]
				{
					new SqlParam("@Material_ID", Material_ID)
				};
                sqls[0] = sbDelete;
                objs[0] = parm;
                int index = 1;
                for (int i = 0; i < pkVal.Length; i++)
                {
                    string item = pkVal[i];
                    if (item.Length > 0)
                    {
                        StringBuilder sbadd = new StringBuilder();
                        sbadd.Append("Insert into MMS_MaterialInfo(");
                        sbadd.Append("UserRole_ID,User_ID,Roles_ID,CreateUserId,CreateUserName");
                        sbadd.Append(")Values(");
                        sbadd.Append("@UserRole_ID,@User_ID,@Material_ID,@CreateUserId,@CreateUserName)");
                        SqlParam[] parmAdd = new SqlParam[]
						{
							new SqlParam("@UserRole_ID", CommonHelper.GetGuid),
							new SqlParam("@User_ID", item),
							new SqlParam("@Material_ID", Material_ID),
							new SqlParam("@CreateUserId", RequestSession.GetSessionUser().UserId),
							new SqlParam("@CreateUserName", RequestSession.GetSessionUser().UserName)
						};
                        sqls[index] = sbadd;
                        objs[index] = parmAdd;
                        index++;
                    }
                }
                result = (DataFactory.SqlDataBase().BatchExecuteBySql(sqls, objs) >= 0);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        #endregion Method



    }
}
