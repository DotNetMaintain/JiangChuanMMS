using RM.Busines;
using RM.Busines.DAL;
using RM.Busines.DAO;
using RM.Common.DotNetBean;
using RM.Common.DotNetCode;
using RM.Common.DotNetUI;
using RM.Web.App_Code;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using RM.ServiceProvider;
using RM.ServiceProvider.Service;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Interface;
using RM.ServiceProvider.Dao;
using RM.ServiceProvider.Enum;
using System.Data;
using System.Text;

namespace RM.Web.MMS.MMS_Purchase
{
    public partial class Purchase_Form : PageBase
    {
        private string id
    {
        get
        {
            if (ViewState["id"] == null || ViewState["id"].ToString() == "")
            {
                return "";
            }
            else
            {
                return ViewState["id"].ToString();
            }
        }
        set { ViewState["id"] = value; }
    }

    private string detailId
    {
        get
        {
            if (ViewState["detailId"] == null || ViewState["detailId"].ToString() == "")
            {
                return "";
            }
            else
            {
                return ViewState["detailId"].ToString();
            }
        }
        set { ViewState["detailId"] = value; }
    }

    /// <summary>
    ///   session实体
    /// </summary>
    private TPurchaseIndent _TPurchaseIndent
    {
        get
        {
            if (Session["TPurchaseIndent"] == null)
            {
                return new TPurchaseIndent { Content = new MMS_PurchaseIndentContent() };
            }
            else
            {
                return (TPurchaseIndent)(Session["TPurchaseIndent"]);
            }
        }
        set { Session["TPurchaseIndent"] = value; }
    }

    RM_UserInfo_IDAO user_idao = new RM_UserInfo_Dal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDict("Unit", ddlUnit); //将计量单位下拉框绑定字典

            if (Request.QueryString["ID"] == null) //入库单录入页面
            {
                _TPurchaseIndent = null;
                ClearTextBox(); //清除入库单相关服务器控件的内容
            }
            else //入库单修改页面
            {
                id = Request.QueryString["ID"];
                //调用业务规则层-入库单服务类方法获得要修改的实体
                _TPurchaseIndent = PurchaseIndentService.Instance.GetPurchaseIndent(Convert.ToInt32(id));
                ModelToTextBox(_TPurchaseIndent); //将入库单实体赋值给对应的服务器控件
            }
            LoadData(); //加载GridView数据
            EntryDetailInputPage(false); //切换到入库单页面
            if (Request.QueryString["Audit"] != null || Request.QueryString["Query"] != null)
            {
                SetReadOnly(); //如果是审核或查询页面调用的，设置所有输入控件只读
            }
          
        }
    }


    /// <summary>
    ///   控件设置只读方法
    /// </summary>
    private void SetReadOnly()
    {

        txtPurchasePlanBillCode.ReadOnly = true;
        txtDeptName.ReadOnly = true;
        txtPurchasePlanDate.ReadOnly = true;
        imgPurchasePlanDate.Visible = false;
        btnSave.Visible = false;  
        dgvInfo.Columns[dgvInfo.Columns.Count - 1].Visible = false;
    }

    

   
    #region Content相关

    /// <summary>
    ///   字典绑定通用方法
    /// </summary>
    /// <param name="typeCode"> </param>
    /// <param name="ddl"> </param>
    private void BindDict(string typeCode, DropDownList ddl)
    {
        //根据字典类型取字典项
        Dictionary<string, string> dictList = DictionaryInfoService.Instance.GetListByDictType(typeCode);
        ddl.DataSource = dictList; //设置下拉框的数据源
        ddl.DataTextField = "Value";
        ddl.DataValueField = "Key";
        ddl.DataBind(); //下拉框数据绑定
    }


    private DataTable  findDeptManage()
    {

        string sql = @"select traff.User_ID,traff.User_Code,traff.User_Account,traff.User_Name,traff.Organization_Name,manageuserinfo.User_Account as Organization_Manager,assmanageuserinfo.User_Account as Organization_AssistantManager from 
                    (select userinfo.User_ID,userinfo.User_Code,userinfo.User_Account,userinfo.User_Name,org.Organization_Name,
                    org.Organization_Manager,org.Organization_AssistantManager
                    from dbo.Base_UserInfo userinfo 
                    inner join Base_StaffOrganize staff 
                    on  userinfo.user_id=staff.user_id
                    inner join dbo.Base_Organization org on staff.Organization_ID=org.Organization_ID) traff
                    inner join dbo.Base_UserInfo manageuserinfo on traff.Organization_Manager=manageuserinfo.User_Name
					inner join dbo.Base_UserInfo assmanageuserinfo on traff.Organization_AssistantManager=assmanageuserinfo.User_Name
                    where traff.User_Account='{0}'";
       sql = string.Format(sql,RequestSession.GetSessionUser().UserAccount.ToString());
       DataTable dt_DeptInfo = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(sql));
       
       return dt_DeptInfo;
    }



        private string FindAuditUser(List<TPurchaseIndentDetail> lstproductcode)
        {
            string AuditUser = string.Empty;
            string lstmaterial = string.Empty;
            int i = 0;
            foreach (TPurchaseIndentDetail tp in lstproductcode)
            {
                i = i + 1;
                if (i == lstproductcode.Count)
                {
                    lstmaterial = lstmaterial + "'" + tp.DetDetail.ProductCode + "'";
                }
                else
                {
                    lstmaterial = lstmaterial + "'" + tp.DetDetail.ProductCode + "',";
                }
                
            }
            string sql = @"select distinct approvallevel FROM  MMS_MaterialInfo mms
inner join [dbo].MMS_MaterialType mt
on substring(mms.Material_Type,1,charindex('-',mms.Material_Type)-1)=mt.materialtype_name
where mms.Material_ID in ({0})";
            sql = string.Format(sql, lstmaterial);
            DataTable dt_DeptInfo = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(sql));

            if (dt_DeptInfo != null && dt_DeptInfo.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_DeptInfo.Rows)
                {
                    AuditUser = AuditUser + dr["approvallevel"].ToString() + ",";
                }
            }

            return AuditUser;
        }

        private string FindListAuditUser(string productcode)
        {
            string lsuser = string.Empty;

            string sql = @"select distinct MMS.Material_Type,approvallevel FROM  MMS_MaterialInfo mms
inner join [dbo].MMS_MaterialType mt
on substring(mms.Material_Type,1,charindex('-',mms.Material_Type)-1)=mt.materialtype_name
where mms.Material_ID={0}";
            sql = string.Format(sql, productcode);
            DataTable dt_DeptInfo = DataFactory.SqlDataBase().GetDataTableBySQL(new StringBuilder(sql));

            if (dt_DeptInfo != null && dt_DeptInfo.Rows.Count > 0)
            {
                lsuser = dt_DeptInfo.Rows[0]["approvallevel"].ToString();
            }


            return lsuser;
        }

        protected void btnSave_Click(object sender, EventArgs e)
    {


        if (Page.IsValid)
        {
            TPurchaseIndent info = _TPurchaseIndent;

            if (info.Detail.Count == 0)
            {
                Response.Write("<Script>window.alert('请先录入明细信息!')</Script>");
                return;
            }
            TextBoxToModel(info);
            info.Content.Operator =RequestSession.GetSessionUser().UserId.ToString();
            info.Content.Provider = FindAuditUser(info.Detail).Trim();               //第一关审核人员
            info.Content.PurchaseMan = findDeptManage().Rows[0]["Organization_AssistantManager"].ToString().Trim();   //最后审核人员
            info.Content.DeptName = txtDeptName.Text.ToString(); 
            info.Content.OperateDate = DateTime.Now;
            if (string.IsNullOrEmpty(id)) //插入
            {
                info.OprType = OperateType.otInsert;
            }
            else //修改
            {
                info.OprType = OperateType.otUpdate;
            }
            int tempId =PurchaseIndentService.Instance.SavePurchaseIndent(info);
                //PurchasePlanService.Instance.SavePurchase(info);
            if (tempId > 0)
            {
                _TPurchaseIndent = PurchaseIndentService.Instance.GetPurchaseIndent(tempId);
                id = tempId.ToString();
                ModelToTextBox(_TPurchaseIndent);
                LoadData();
            }
            Response.Write("<Script>window.alert('保存成功!')</Script>");
        }
    }

    private void TextBoxToModel(TPurchaseIndent info)
    {
        info.Content.AuditFlag = false;
        info.Content.PurchaseBillCode = txtPurchasePlanBillCode.Text;
        if (!string.IsNullOrEmpty(txtPurchasePlanDate.Text))
        {
            info.Content.PurchaseDate = Convert.ToDateTime(txtPurchasePlanDate.Text);
        }

    }

    private void ClearTextBox()
    {
       
        txtPurchasePlanBillCode.Text = BatchEvaluate.GeneralCode(); //入库单号
        txtPurchasePlanDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //入库日期
        txtDeptName.Text = "";
        lblOperator.Text = RequestSession.GetSessionUser().UserName.ToString(); ; //领料人员
       
    }

    private void ModelToTextBox(TPurchaseIndent info)
    {
       
        if (!string.IsNullOrEmpty(info.Content.Provider)) //供应商
        {
            Base_ClientInfo tempClient = ClientInfoService.Instance.GetClientInfoByCode(info.Content.Provider);
            if (tempClient != null)
            {
                
            }
        }
        txtPurchasePlanBillCode.Text = info.Content.PurchaseBillCode; //入库单号
        txtPurchasePlanDate.Text = info.Content.PurchaseDate.ToString(); //入库日期
        lblOperator.Text = user_idao.GetUserInfo(info.Content.Operator).Rows[0]["User_Name"].ToString();
      
    }

    /// <summary>
    ///   跳转到入库单管理页面
    /// </summary>
    /// <param name="sender"> </param>
    /// <param name="e"> </param>
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Audit"] != null) //如果是审核页面调用
        {
            Response.Redirect(@"~/MMS/MMS_Audit/Audit_List.aspx");
        }
        else if (Request.QueryString["Query"] != null) //如果是查询页面调用
        {
            Response.Redirect(@"~/MMS/MMS_Requisition/Requisition_Query.aspx");
        }
        else
        {
            Response.Redirect(@"~/Purchase/PurchaseStockList.aspx"); //返回采购计划管理页面
        }
    }

   

    #endregion

    #region Detail列表

    private void LoadData()
    {
        TPurchaseIndent infoList = _TPurchaseIndent;
        //调用业务层返回货品信息列表
        List<MMS_MaterialInfo> productList = MaterialInfoService.Instance.GetAllInfo();
        //调用业务层返回字典信息列表
        List<Base_DictionaryInfo> dictList = DictionaryInfoService.Instance.GetAllInfo();
        //将入库单货品与货品信息、字典保关联
        var query = from info in infoList.Detail
                    where info.OprType != OperateType.otDelete
                    join product in productList
                        on info.DetDetail.ProductCode equals product.Material_ID.ToString()
                    select new
                        {
                            Id = info.DetDetail.ID,
                            info.DetDetail.PurchaseBillCode,
                            product.Material_ID,
                            product.Material_Name,
                            product.Material_Specification,
                            product.Material_Supplier,
                            Unit = product.Material_Unit,
                            info.DetDetail.Quantity,
                            info.DetDetail.Price,
                            Amount = info.DetDetail.Quantity*info.DetDetail.Price,
                            info.DetDetail.Memo
                        };
        dgvInfo.DataKeyNames = new[] { "Id" }; //设置GridView数据主键
        dgvInfo.DataSource = query.ToList(); //设置GridView数据源
        dgvInfo.DataBind();
        //计算数量及金额汇总信息
        lblTotalQuantity.Text = infoList.Detail.Sum(itm => itm.DetDetail.Quantity).ToString("#");
       
    }

    /// <summary>
    ///   GridView行命令事件，点击GridView行按钮时触发的事件
    /// </summary>
    /// <param name="sender"> </param>
    /// <param name="e"> </param>
    protected void dgvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandName)) //判断命令名是否为空
        {
            if (e.CommandName == "Edi") //如果触发的是详细信息按钮事件
            {
                int index = Convert.ToInt32(e.CommandArgument); //取GridView行索引
                GridView grid = (GridView) e.CommandSource; //取当前操作的GridView
                int tempId = Convert.ToInt32(grid.DataKeys[index].Value);
                detailId = tempId.ToString();
                TPurchaseIndentDetail detail = _TPurchaseIndent.Detail.FirstOrDefault(itm => itm.DetDetail.ID == tempId);
                ModelToDetailTextBox(detail); //将实体赋值给对应的服务器控件 
                EntryDetailInputPage(true); //切换到货品录入页面

               
            }
            else if (e.CommandName == "Del")
            {
                int index = Convert.ToInt32(e.CommandArgument); //取GridView行索引
                GridView grid = (GridView) e.CommandSource; //取当前操作的GridView
                int id = Convert.ToInt32(grid.DataKeys[index].Value); //取GridView主键值

                TPurchaseIndent temp = _TPurchaseIndent;
                TPurchaseIndentDetail tempDetail = temp.Detail.First(itm => itm.DetDetail.ID == id);
                if (tempDetail.OprType == OperateType.otInsert) //如果是新插入的直接将其删除
                {
                    temp.Detail.Remove(tempDetail);
                }
                else //如是不是新插入的置删除标志
                {
                    tempDetail.OprType = OperateType.otDelete;
                }
                _TPurchaseIndent = temp;
                LoadData();
            }
            else if (e.CommandName == "Page")
            {
                LoadData();
            }
        }
    }

    /// <summary>
    ///   添加按钮单击事件
    /// </summary>
    /// <param name="sender"> </param>
    /// <param name="e"> </param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        detailId = ""; //编辑时detailId为编辑行id
        //ClearDetailTextBox();
        //EntryDetailInputPage(true); //切换到货品录入页面
    }

    /// <summary>
    ///   GridView页改变事件
    /// </summary>
    /// <param name="sender"> </param>
    /// <param name="e"> </param>
    protected void dgvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        (sender as GridView).PageIndex = e.NewPageIndex; //指定GridView新页索引
        (sender as GridView).DataBind(); //GridView数据源绑定
    }

    /// <summary>
    ///   GridView行绑定事件
    /// </summary>
    /// <param name="sender"> </param>
    /// <param name="e"> </param>
    protected void dgvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //如果是数据行
        {
            GridView grid = sender as GridView; //取当前操作的GridView
            //为GridView数据行的删除按钮添加删除确认对话框
            ((LinkButton)(e.Row.Cells[grid.Columns.Count - 1].Controls[0])).Attributes.Add("onclick",
                                                                                            "return confirm('确认删除?');");
        }
    }

    #endregion

    #region Detail录入

    protected void btnOK_Click(object sender, EventArgs e)
    {
        TPurchaseIndent info = _TPurchaseIndent;
        if (string.IsNullOrEmpty(detailId)) //插入操作
        {
            //创建入库货品实例
            TPurchaseIndentDetail tinfoDetail = new TPurchaseIndentDetail();
            tinfoDetail.DetDetail = new MMS_PurchaseIndentDetail();
            if (info.Detail.Count > 0) //新插入的以-1开始,以后渐减
            {
                //设置新录入入库货品的主键ID，以-1开始,以后渐减
                int minId = info.Detail.Min(itm => itm.DetDetail.ID);
                if (minId < 0)
                    tinfoDetail.DetDetail.ID = minId - 1;
                else
                    tinfoDetail.DetDetail.ID = -1;
            }
            else //该入库单没有货品信息
            {
                tinfoDetail.DetDetail.ID = -1;
            }
            DetailTextBoxToModel(tinfoDetail); //将入库货品赋值给实体
            tinfoDetail.OprType = OperateType.otInsert;
            info.Detail.Add(tinfoDetail); //将操作实体添加到入库货品集合中
            _TPurchaseIndent = info;

            ClearDetailTextBox(); //清除入库货品服务器控件内容
            LoadData(); //加载Gridview数据
        }
        else //编辑操作
        {
            //根据入库货品ID取实体
            TPurchaseIndentDetail tinfoDetail = info.Detail.First(itm => itm.DetDetail.ID == Convert.ToInt32(detailId));
            DetailTextBoxToModel(tinfoDetail); //将服务器控件赋给实体
            if (tinfoDetail.OprType != OperateType.otInsert) //如果是新插入的仍保留插入状态
            {
                tinfoDetail.OprType = OperateType.otUpdate;
            }
            _TPurchaseIndent = info;
            LoadData(); //加载GridView数据
            EntryDetailInputPage(false); //切换到入库单录入页面
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        detailId = "";
        
        EntryDetailInputPage(false); //切换到入库单录入页面
    }

    private void ClearDetailTextBox()
    {
        txtProductCode.Text = ""; //货品代码
        txtShortName.Text = ""; //货品简称
        txtSpecs.Text = ""; //规格型号
        ddlUnit.SelectedIndex = 0; //计量单位
        txtQuantity.Text = ""; //数量
      
        txtVendor.Text = "";     //生产厂商
       

        hidProductCode.Value = ""; //货品代码
        hidProductName.Value = ""; //货品名称
    }

    private void ModelToDetailTextBox(TPurchaseIndentDetail tinfo)
    {
        //调用业务层方法取货品信息实体
        MMS_MaterialInfo  Material=MaterialInfoService .Instance .GetProductInfoByCode (tinfo.DetDetail.ProductCode);
        MMS_PurchaseIndentDetail detail = tinfo.DetDetail;
        txtProductCode.Text = detail.ProductCode; //货品代码
        txtShortName.Text = Material.Material_Name; //货品简称
        txtSpecs.Text = Material.Material_Specification; //规格
        ddlUnit.Text = Material.Material_Unit ; //计量单位
        txtVendor.Text = Material.Material_Supplier;  //供应商
        txtQuantity.Text = detail.Quantity.ToString(); //数量
       
    }

    private void DetailTextBoxToModel(TPurchaseIndentDetail tinfo)
    {
        MMS_PurchaseIndentDetail detail = tinfo.DetDetail;
        detail.PurchaseBillCode = txtPurchasePlanBillCode.Text; //入库单号
        detail.ProductCode = txtProductCode.Text; //货品代码
        if (!string.IsNullOrEmpty(txtQuantity.Text)) //数量
        {
            detail.Quantity = Convert.ToInt32(txtQuantity.Text);
        }
        detail.Memo = txtComm.Text.ToString().Trim();
        detail.AuditAccount = FindListAuditUser(txtProductCode.Text.ToString()).ToString();

        }

    /// <summary>
    ///   列表页及录入页切换
    /// </summary>
    /// <param name="isEntry"> 为真切换至录入页,为假切换至列表页 </param>
    private void EntryDetailInputPage(bool isEntry)
    {
        
    }

    /// <summary>
    ///   选择货品
    /// </summary>
    /// <param name="sender"> </param>
    /// <param name="e"> </param>
    protected void btnSelectProduct_Click(object sender, EventArgs e)
    {
        txtProductCode.Text = hidProductCode.Value;
        txtShortName.Text = hidProductName.Value;
        //调用业务层的方法取货品信息实体
        MMS_MaterialInfo Material = MaterialInfoService.Instance.GetProductInfoByCode(txtProductCode.Text);
  
        if (Material != null)
        {
            txtSpecs.Text = Material.Material_Specification; //规格
            ddlUnit.SelectedItem.Text  = Material.Material_Unit ; //计量单位
            txtVendor.Text = Material.Material_Supplier;  //厂商

          
        }
    }

    #endregion

    protected void btnSelectDept_Click(object sender, EventArgs e)
    {
        txtDeptName.Text = hidDeptName.Value;
    }

    
    }
}


