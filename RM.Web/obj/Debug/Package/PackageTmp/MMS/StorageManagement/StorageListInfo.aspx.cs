using RM.Busines;
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

namespace RM.Web.MMS.StorageManagement
{
    public partial class StorageListInfo : PageBase
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
        private TPurchase _TPurchase
        {
            get
            {
                if (Session["TPurchase"] == null)
                {
                    return new TPurchase { Content = new MMS_PurchaseContent() };
                }
                else
                {
                    return (TPurchase)(Session["TPurchase"]);
                }
            }
            set { Session["TPurchase"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                BindDict("Unit", ddlUnit); //将计量单位下拉框绑定字典
                if (Request.QueryString["ID"] == null) //入库单录入页面
                {
                    _TPurchase = null;
                    ClearTextBox(); //清除入库单相关服务器控件的内容
                }
                else //入库单修改页面
                {
                    id = Request.QueryString["ID"];
                    //调用业务规则层-入库单服务类方法获得要修改的实体
                    _TPurchase = PurchaseService.Instance.GetPurchase(Convert.ToInt32(id));
                    ModelToTextBox(_TPurchase); //将入库单实体赋值给对应的服务器控件
                }
                LoadData(); //加载GridView数据
                EntryDetailInputPage(false); //切换到入库单页面
                if (Request.QueryString["Audit"] != null || Request.QueryString["Query"] != null)
                {
                    SetReadOnly(); //如果是审核或查询页面调用的，设置所有输入控件只读
                }
                // lblOperator.Text = LoginManager.GetUserName(Context.User.Identity.Name);
            }
        }


        /// <summary>
        ///   控件设置只读方法
        /// </summary>
        private void SetReadOnly()
        {
            txtInvoiceCode.ReadOnly = true;
            txtPurchaseBillCode.ReadOnly = true;
            txtPurchaseDate.ReadOnly = true;
            // txtPurchaseManName.ReadOnly = true;

            //imgPurchaseDate.Visible = false;
            // btnAdd.Visible = false;
            btnSave.Visible = true;
            btnSelectProvider.Visible = false;
            //dgvInfo.Columns[dgvInfo.Columns.Count - 1].Visible = false;
            //dgvInfo.Columns[dgvInfo.Columns.Count - 2].Visible = false;
        }

        /// <summary>
        ///   数量改变事件
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "" && txtPrice.Text != "")
            {
                int qty;
                double pri;
                if (int.TryParse(txtQuantity.Text, out qty) && double.TryParse(txtPrice.Text, out pri))
                {
                    txtAmount.Text = Math.Round(qty * pri, 2).ToString("#.##");
                }
            }
        }

        /// <summary>
        ///   单价改变事件
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "" && txtPrice.Text != "")
            {
                txtAmount.Text = (Convert.ToInt32(txtQuantity.Text) * Convert.ToDouble(txtPrice.Text)).ToString("#.##");
            }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                TPurchase info = _TPurchase;
                if (info.Detail.Count == 0)
                {
                    Response.Write("<Script>window.alert('请先录入明细信息!')</Script>");
                    return;
                }
                TextBoxToModel(info);
                info.Content.Operator = Context.User.Identity.Name;
                info.Content.OperateDate = DateTime.Now;
                if (string.IsNullOrEmpty(id)) //插入
                {
                    info.OprType = OperateType.otInsert;
                }
                else //修改
                {
                    info.OprType = OperateType.otUpdate;
                }
                int tempId = PurchaseService.Instance.SavePurchase(info);
                //PurchaseService.Instance.SavePurchase(info);
                if (tempId > 0)
                {
                    _TPurchase = PurchaseService.Instance.GetPurchase(tempId);
                    id = tempId.ToString();
                    ModelToTextBox(_TPurchase);
                    LoadData();
                }
                Response.Write("<Script>window.alert('保存成功!')</Script>");
                ClearTextBox();
                dgvInfo.DataSource = null;
                dgvInfo.DataBind();
                _TPurchase = null; 
                
            }
        }

        private void TextBoxToModel(TPurchase info)
        {
            info.Content.AuditFlag = false;
            info.Content.InvoiceCode = txtInvoiceCode.Text;
            info.Content.Provider = hidProvider.Value;
            info.Content.PurchaseBillCode = txtPurchaseBillCode.Text;
            if (!string.IsNullOrEmpty(txtInvoiceDate.Text))
            {
                info.Content.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
            }

            if (!string.IsNullOrEmpty(txtPurchaseDate.Text))
            {
                info.Content.PurchaseDate = Convert.ToDateTime(txtPurchaseDate.Text);
            }
            info.Content.CheckMan = RequestSession.GetSessionUser().UserAccount.ToString();

        }

        private void ClearTextBox()
        {
            txtInvoiceCode.Text = ""; //发票号
            txtPurchaseBillCode.Text = BatchEvaluate.GeneralCode(); //入库单号
            txtPurchaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //入库日期

            txtProviderName.Text = ""; //供应商
            hidProvider.Value = "";
            hidProviderName.Value = "";

            // txtPurchaseManName.Text = ""; //经办人
            hidPurchaseMan.Value = "";
            hidPurchaseManName.Value = "";
            lblStorageUser.Text = RequestSession.GetSessionUser().UserAccount.ToString();
            lblTotalAmount.Text = "";
            lblTotalQuantity.Text = "";

            id = string.Empty;

        }

        private void ModelToTextBox(TPurchase info)
        {
            txtInvoiceCode.Text = info.Content.InvoiceCode; //发票号
            if (!string.IsNullOrEmpty(info.Content.Provider)) //供应商
            {
                hidProvider.Value = info.Content.Provider;
                Base_DictionaryInfo tempClient = DictionaryInfoService.Instance.GetInfo(Convert.ToInt32(info.Content.Provider));
             //   Base_ClientInfo tempClient = ClientInfoService.Instance.GetClientInfoByCode(info.Content.Provider);
                if (tempClient != null)
                {
                    txtProviderName.Text = tempClient.ValueName;
                    hidProviderName.Value = tempClient.ValueCode;
                }
            }
            txtPurchaseBillCode.Text = info.Content.PurchaseBillCode; //入库单号
            txtPurchaseDate.Text = info.Content.PurchaseDate.ToString(); //入库日期
            txtInvoiceDate.Text = info.Content.InvoiceDate.ToString();
            if (!string.IsNullOrEmpty(info.Content.CheckMan)) //经办人
            {
                lblStorageUser.Text = RequestSession.GetSessionUser().UserAccount.ToString();
                // hidPurchaseMan.Value = info.Content.CheckMan;
                //EmployeeInfo tempEmployee = EmployeeInfoService.Instance.GetEmployeeInfoByCode(info.Content.CheckMan);
                //if (tempEmployee != null)
                //{
                //    txtPurchaseManName.Text = tempEmployee.Name;
                //    hidPurchaseManName.Value = tempEmployee.Name;
                //}
            }

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
                Response.Redirect(@"~/Purchase/PurchaseStockAudit.aspx");
            }
            else if (Request.QueryString["Query"] != null) //如果是查询页面调用
            {
                Response.Redirect(@"~/MMS/StorageManagement/InStorage_Query.aspx");
            }
            else
            {
                Response.Redirect(@"~/Purchase/PurchaseStockList.aspx"); //返回采购计划管理页面
            }
        }

        /// <summary>
        ///   选择供应商
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void btnSelectProvider_Click(object sender, EventArgs e)
        {
            txtProviderName.Text = hidProviderName.Value;
        }

        /// <summary>
        ///   选择经办人
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void btnSelectEmployee_Click(object sender, EventArgs e)
        {
            //  txtPurchaseManName.Text = hidPurchaseManName.Value;
        }

        #endregion

        #region Detail列表

        private void LoadData()
        {
            TPurchase infoList = _TPurchase;
            //调用业务层返回货品信息列表
            List<MMS_MaterialInfo> productList = MaterialInfoService.Instance.GetAllInfo();

            //将入库单货品与货品信息关联
            var query = from info in infoList.Detail
                        where info.OprType != OperateType.otDelete
                        join product in productList
                            on info.DetDetail.ProductCode equals product.Material_ID.ToString()
                        //  where info.DetDetail.Quantity-info.DetDetail.UseQuantity>0
                        select new
                        {
                            Id = info.DetDetail.ID,
                            info.DetDetail.PurchaseBillCode,
                            product.Material_ID,
                            product.Material_Name,
                            product.Material_Specification,
                            Unit = product.Material_Unit,
                            info.DetDetail.Quantity,
                            info.DetDetail.Price,
                            info.DetDetail.SeldPrice,
                            info.DetDetail.Lot,
                            info.DetDetail.ValidDate,
                            Amount = info.DetDetail.Quantity * info.DetDetail.Price
                        };
            dgvInfo.DataKeyNames = new[] { "Id" }; //设置GridView数据主键
            dgvInfo.DataSource = query.ToList(); //设置GridView数据源
            dgvInfo.DataBind();
            //计算数量及金额汇总信息
            lblTotalQuantity.Text = infoList.Detail.Sum(itm => itm.DetDetail.Quantity).ToString("#");
            lblTotalAmount.Text = infoList.Detail.Sum(itm => itm.DetDetail.Quantity * itm.DetDetail.Price).ToString("#.##");
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
                    GridView grid = (GridView)e.CommandSource; //取当前操作的GridView
                    int tempId = Convert.ToInt32(grid.DataKeys[index].Value);
                    detailId = tempId.ToString();
                    TPurchaseDetail detail = _TPurchase.Detail.FirstOrDefault(itm => itm.DetDetail.ID == tempId);
                    ModelToDetailTextBox(detail); //将实体赋值给对应的服务器控件 
                    EntryDetailInputPage(true); //切换到货品录入页面


                }
                else if (e.CommandName == "Del")
                {
                    int index = Convert.ToInt32(e.CommandArgument); //取GridView行索引
                    GridView grid = (GridView)e.CommandSource; //取当前操作的GridView
                    int id = Convert.ToInt32(grid.DataKeys[index].Value); //取GridView主键值

                    TPurchase temp = _TPurchase;
                    TPurchaseDetail tempDetail = temp.Detail.First(itm => itm.DetDetail.ID == id);
                    if (tempDetail.OprType == OperateType.otInsert) //如果是新插入的直接将其删除
                    {
                        temp.Detail.Remove(tempDetail);
                    }
                    else //如是不是新插入的置删除标志
                    {
                        tempDetail.OprType = OperateType.otDelete;
                    }
                    _TPurchase = temp;
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
            TPurchase info = _TPurchase;
            if (string.IsNullOrEmpty(detailId)) //插入操作
            {
                //创建入库货品实例
                TPurchaseDetail tinfoDetail = new TPurchaseDetail();
                tinfoDetail.DetDetail = new MMS_PurchaseDetail();
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
                _TPurchase = info;

                ClearDetailTextBox(); //清除入库货品服务器控件内容
                LoadData(); //加载Gridview数据
            }
            else //编辑操作
            {
                //根据入库货品ID取实体
                TPurchaseDetail tinfoDetail = info.Detail.First(itm => itm.DetDetail.ID == Convert.ToInt32(detailId));
                DetailTextBoxToModel(tinfoDetail); //将服务器控件赋给实体
                if (tinfoDetail.OprType != OperateType.otInsert) //如果是新插入的仍保留插入状态
                {
                    tinfoDetail.OprType = OperateType.otUpdate;
                }
                _TPurchase = info;
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
            txtPrice.Text = ""; //单价
            txtSeldPrice.Text = ""; //单价
            txtAmount.Text = ""; //金额
            hidProductCode.Value = ""; //货品代码
            hidProductName.Value = ""; //货品名称
        }

        private void ModelToDetailTextBox(TPurchaseDetail tinfo)
        {
            BindDict("Unit", ddlUnit); //将计量单位下拉框绑定字典
            //调用业务层方法取货品信息实体
            MMS_MaterialInfo product = MaterialInfoService.Instance.GetProductInfoByCode(tinfo.DetDetail.ProductCode);
            MMS_PurchaseDetail detail = tinfo.DetDetail;
            txtProductCode.Text = detail.ProductCode; //货品代码
            txtShortName.Text = product.Material_Name; //货品简称
            txtSpecs.Text = product.Material_Specification; //规格
            txtLot.Text = detail.Lot.ToString();                //批号
            txtValidDate.Text = detail.ValidDate.ToString();    //有效期
            txtProvider.Text = product.Material_Supplier.ToString(); //生产厂同
            ddlUnit.Text = product.Material_Unit; //计量单位
            txtQuantity.Text = detail.Quantity.ToString(); //数量
            txtPrice.Text = detail.Price.ToString(); //单价
            txtSeldPrice.Text = detail.SeldPrice.ToString();
            txtAmount.Text = (detail.Quantity * detail.Price).ToString(); //金额=数量*单价
        }

        private void DetailTextBoxToModel(TPurchaseDetail tinfo)
        {
            MMS_PurchaseDetail detail = tinfo.DetDetail;
            detail.PurchaseBillCode = txtPurchaseBillCode.Text; //入库单号
            detail.ProductCode = txtProductCode.Text; //货品代码
            detail.Lot = txtLot.Text.ToString();                //批号
            detail.ValidDate = txtValidDate.Text;    //有效期
            if (!string.IsNullOrEmpty(txtQuantity.Text)) //数量
            {
                detail.Quantity = Convert.ToInt32(txtQuantity.Text);
            }
            if (!string.IsNullOrEmpty(txtPrice.Text)) //单价
            {
                detail.Price = Convert.ToDouble(txtPrice.Text);
            }
            if (!string.IsNullOrEmpty(txtSeldPrice.Text)) //出货单价
            {
                detail.SeldPrice = Convert.ToDouble(txtSeldPrice.Text.ToString());
            }
        }

        /// <summary>
        ///   列表页及录入页切换
        /// </summary>
        /// <param name="isEntry"> 为真切换至录入页,为假切换至列表页 </param>
        private void EntryDetailInputPage(bool isEntry)
        {
            //pnlContent.Visible = !isEntry; //采购计划单面板
            // pnlDetail.Visible = isEntry; //采购货品面板
        }

        /// <summary>
        ///   选择货品
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void btnSelectProduct_Click(object sender, EventArgs e)
        {
            BindDict("Unit", ddlUnit); //将计量单位下拉框绑定字典
            txtProductCode.Text = hidProductCode.Value;
            txtShortName.Text = hidProductName.Value;
            MMS_MaterialInfo prod = MaterialInfoService.Instance.GetProductInfoByCode(txtProductCode.Text);
            //调用业务层的方法取货品信息实体
            // MMS_ProductInfo prod = ProductInfoService.Instance.GetProductInfoByCode(txtProductCode.Text);
            if (prod != null)
            {
                txtSpecs.Text = prod.Material_Specification; //规格
                ddlUnit.SelectedItem.Text = prod.Material_Unit; //计量单位
                txtProvider.Text = prod.Material_Supplier;   //生产厂商
                //单价
                //txtPrice.Text =
                //    PurchaseService.Instance.GetLastPurchasePrice(hidProvider.Value, txtProductCode.Text).ToString();
            }
        }

        #endregion


    }
}


