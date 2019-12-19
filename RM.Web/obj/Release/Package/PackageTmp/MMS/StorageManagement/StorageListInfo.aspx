<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StorageListInfo.aspx.cs" Inherits="RM.Web.MMS.StorageManagement.StorageListInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title>入库表单</title>
  <style type="text/css">
		#divSCA
        {
            position: absolute;
            width: 700px;
            height: 500px;
            font-size: 12px;
            background: #fff;
            border: 0px solid #000;
            z-index: 10001;
            display: none;
        }
       
	</style>
    
   <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/jquery.divbox.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/Validator/JValidator.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/artDialog.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/iframeTools.source.js" type="text/javascript"></script>
    <link href="/Themes/Scripts/TreeView/treeview.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/TreeView/treeview.pack.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
  
        <script  type="text/javascript">



            function selectClient() {
                obj = new Object();
                resu = window.showModalDialog("../../RMBase/SysBaseInfo/SelectClient.aspx", obj, "dialogWidth=600px;dialogHeight=460px; status:no;resizable:no;");
                if (resu != null) {
                    document.getElementById("hidProviderName").value = resu.name;
                    document.getElementById("hidProvider").value = resu.code;
                }
            }

            function selectMaterial() {
                obj = new Object();
                resu = window.showModalDialog("../../RMBase/SysBaseInfo/SelectMaterialOrg_Index.aspx", obj, "dialogWidth=600px;dialogHeight=460px; status:no;resizable:no;");
                if (resu != null) {
                    document.getElementById("hidProductCode").value = resu.code;
                    document.getElementById("hidProductName").value = resu.name;


                }
            }


            function openDiv() {
                $("#divSCA").OpenDiv();
            }

            function closeDiv() {
                $("#divSCA").CloseDiv();
            }




            //编辑
            function edit() {
                var key = StorageForm_ID;
                if (IsEditdata(key)) {
                    var url = "/MMS/StorageManagement/StorageForm.aspx?key=" + key;
                    top.openDialog(url, 'StorageForm', '进货单 - 编辑', 700, 335, 50, 50);
                }
            }

            //导入
            function inport() {

                var url = "/MMS/StorageManagement/ExcelImportStorage.aspx";
                top.openDialog(url, 'ExcelImportStorage', '进货单 - 导入', 700, 335, 50, 50);

            }




</script> 


</head>

<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" SupportsPartialRendering="True" runat="server">
    </asp:ScriptManager>

     <div class="btnbarcontetn">

    
        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

    </div>

    <asp:Panel ID="pnlContent" runat="server"  CssClass ="width100">
        <table  class ="width100">
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td class="style2">
                                单据号</td>
                            <td>
                                <asp:TextBox ID="txtPurchaseBillCode" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ErrorMessage="单据号必填" ControlToValidate="txtPurchaseBillCode" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                           
                          
                            <td>
                                入库人员</td>
                                <td> 
                                <input id="hidPurchaseMan" type="hidden" runat="server" />
                                <input id="hidPurchaseManName" type="hidden" runat="server" />
                                <table>
                                    <tr>
                                        <td>
                                          <asp:Label ID="lblStorageUser" runat="server"></asp:Label>
                                            
                                        </td>
                                        <td>
                                           </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table></td>
                                 <td class="style2">
                                经办日期</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPurchaseDate" runat="server" AutoCompleteType="Disabled" onfocus="WdatePicker({dateFmt: 'yyyy-MM-dd 00:00:00' })"></asp:TextBox>
                                        </td>
                                        <td>
                                           
                                          
                                        </td>
                                        <td>
                                            </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                                        ErrorMessage="经办日期必填" ControlToValidate="txtPurchaseDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                
                                    
                                    
                            </td>

                        </tr>
                        <tr>

                         <td class="style2">
                                发票号</td>
                            <td>
                                <asp:TextBox ID="txtInvoiceCode" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                    ControlToValidate="txtInvoiceCode" Display="Dynamic" ErrorMessage="发票号必填"></asp:RequiredFieldValidator>
                            </td>
                            <td class="style2">
                                供应商</td>
                            <td>
                                <input id="hidProvider" type="hidden" runat="server"/>
                                <input id="hidProviderName" type="hidden" runat="server"/>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upnlProviderName" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtProviderName" runat="server" ></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSelectProvider" EventName="click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSelectProvider" runat="server" Text="选择" 
                                                        OnClientClick =" return selectClient() " onclick="btnSelectProvider_Click" 
                                                        ValidationGroup="empty"/>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                        ErrorMessage="供应商必填" ControlToValidate="txtProviderName" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            
                            </td>
                           
                           <td>
                           物资名称
                           </td>
                           <td>
                                <input id="Hidden1" type="hidden" runat="server"/>
                                <input id="Hidden2" type="hidden" runat="server"/>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSelectProvider" EventName="click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                         
                                                <input type="button" value="选择" onclick="openDiv()"/>
                                       
                                           
                                        </td>
                                        <td>
                                           
                                        </td>
                                    </tr>
                                </table>
                           </td>
                        </tr>

                        <tr>
                        
                          <td class="style2">
                                发票日期</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" AutoCompleteType="Disabled" onfocus="WdatePicker({dateFmt: 'yyyy-MM-dd 00:00:00' })"></asp:TextBox>
                                        </td>
                                        <td>
                                           
                                          
                                        </td>
                                        <td>
                                            </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                </table>
                                
                                    
                                    
                            </td>
                        </tr>



                    </table>
                </td>
            </tr>
           
            <tr>
                <td>
                    <asp:GridView ID="dgvInfo" runat="server"  Width="100%" AutoGenerateColumns="false"
                                  DataKeyNames="ID" EmptyDataText="没有可显示的数据记录。" 
                                  onrowcommand="dgvInfo_RowCommand" AllowPaging="True" 
                                  onpageindexchanging="dgvInfo_PageIndexChanging" 
                                  onrowdatabound="dgvInfo_RowDataBound"
                                  EmptyDataTableCssClass="datagridTable" EmptyDataTitleRowCssClass="GvTitlebg" EmptyDataContentRowCssClass="datagridContentRow">
                      
                      <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" 
                                       LastPageText="末页" NextPageText="下一页" PreviousPageText="上一页" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                                            SortExpression="ID" Visible="false" />
                            <asp:BoundField DataField="PurchaseBillCode" HeaderText="PurchaseBillCode" 
                                            SortExpression="PurchaseBillCode" Visible="false" />
                          
                          <asp:BoundField DataField="Material_ID" HeaderText="物料编码" SortExpression="Material_ID" />
                          <asp:BoundField DataField="Material_Name" HeaderText="物料名称" SortExpression="Material_Name" />
                          <asp:BoundField DataField="Material_Specification" HeaderText="物料规格" SortExpression="Material_Specification" />
                            <asp:BoundField DataField="Unit" HeaderText="单位" SortExpression="Unit" />
                            <asp:BoundField DataField="Lot" HeaderText="批号" SortExpression="Unit" />
                            <asp:BoundField DataField="ValidDate" HeaderText="有效期" SortExpression="Unit" />
                            <asp:BoundField DataField="Quantity" HeaderText="数量" 
                                            SortExpression="Quantity" />
                            <asp:BoundField DataField="Price" HeaderText="进货单价" SortExpression="Price" />
                            <asp:BoundField DataField="Amount" HeaderText="金额" SortExpression="Price" />
                            <asp:ButtonField CommandName="Del" Text="删除" />
                        </Columns> 
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="width100">
                        <tr>
                            <td>制单人</td>
                            <td>
                                <asp:Label ID ="lblOperator" runat="server" Text = ""></asp:Label>
                            </td>
                            <td class="style2">
                                <table>
                                    <tr>
                                        <td>数量合计:</td>
                                        <td>
                                            <asp:Label ID ="lblTotalQuantity" runat="server" Text = "0"></asp:Label>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                        </td>
                                        <td>金额合计:</td>
                                        <td>
                                            <asp:Label ID ="lblTotalAmount" runat="server" Text ="0.00"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td><asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click"/></td>
                            <td>
                                <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" 
                                            ValidationGroup="empty" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>


  <div id="divSCA">
    <asp:Panel ID="pnlDetail" runat="server">
        <table style="border-style: groove; border-width: thin">
            <tr>
                <td>
                    <asp:UpdatePanel ID="upnlProductCode" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td class="style2">
                            
                                        物资代码</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <input id="hidProductCode" type="hidden" runat="server"/>
                                                    <input id="hidProductName" type="hidden" runat="server"/>
                                                    <asp:TextBox ID="txtProductCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSelectProduct" runat="server" Text="选择" 
                                                                OnClientClick =" return selectMaterial() " onclick="btnSelectProduct_Click" 
                                                                ValidationGroup="empty"/>
                                                </td>
                                                <td>
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                        
                                        物资名称</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtShortName" runat="server" Enabled ="false"></asp:TextBox>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                        
                                        规格</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtSpecs" runat="server" Enabled ="false"></asp:TextBox>
                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                        
                                        单位</td>
                                    <td>
                        
                                        <asp:DropDownList ID="ddlUnit" runat="server" Enabled ="false">
                                        </asp:DropDownList>
                        
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="style2">
                        
                                        生产厂商</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtProvider" runat="server" Enabled ="false"></asp:TextBox>
                        
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="style2">
                        
                                        批号</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtLot" runat="server"></asp:TextBox>
                        
                                    </td>
                                </tr>
                                 <tr>
                                  <td class="style2">
                        
                                        有效期</td>
                                  
                                  <td>
                                            <asp:TextBox ID="txtValidDate" runat="server" onfocus="WdatePicker({dateFmt: 'yyyy-MM-dd 00:00:00' })" ></asp:TextBox>
                                        
                                          
                                          
                                        </td>
                                        
                                </tr>
                                <tr>
                                    <td class="style2">
                        
                                        数量</td>
                                    <td>
                                        <asp:TextBox ID="txtQuantity" runat="server" AutoPostBack="True" 
                                                     ontextchanged="txtQuantity_TextChanged"></asp:TextBox>
                                        
                                        <asp:CompareValidator 
                                            ID="CompareValidatorValidInt" runat="server" ControlToValidate="txtQuantity" 
                                            ErrorMessage="请输入正确的数字格式" Operator="DataTypeCheck" Type="Integer" 
                                            Display="Dynamic"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                        
                                        进货单价</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtPrice" runat="server" AutoPostBack="True" 
                                                     ontextchanged="txtPrice_TextChanged" ></asp:TextBox>
                                         <asp:CompareValidator 
                                            ID="CompareValidatorFloat" runat="server" ControlToValidate="txtQuantity" 
                                            ErrorMessage="请输入正确的数字格式" Operator="DataTypeCheck" Type="Double" 
                                            Display="Dynamic"></asp:CompareValidator>
                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                        
                                        金额</td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Enabled ="false"></asp:TextBox>
                                    </td>
                                </tr>

                                 <tr>
                                    <td class="style2">
                        
                                        出货单价</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtSeldPrice" runat="server" AutoPostBack="True" ></asp:TextBox>
                        
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class ="style3">
                    <table>
                        <tr>
                            <td><asp:Button ID="btnOK" runat="server" Text="确定" onclick="btnOK_Click"/></td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="取消" ValidationGroup="empty" 
                                            onclick="btnCancel_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>    
        
    </asp:Panel>
 </div>
    </form>
</body>
</html>