<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchase_Form.aspx.cs" Inherits="RM.Web.MMS.MMS_Purchase.Purchase_Form" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title>采购表单</title>
  <style type="text/css">
		#divSCA
        {
            position: absolute;
            width: 500px;
            height: 300px;
            font-size: 12px;
            background: #fff;
            border: 0px solid #000;
            z-index: 10001;
            display: none;
        }
        
        
            .datable {background-color: #C8E1FB; color:#333333; font-size:12px;}
.datable tr {height:20px;}
.datable .lup {background-color: #C8E1FB;font-size: 12px;color: #014F8A;}
.datable .lup th {border-top: 1px solid #FFFFFF; border-left: 1px solid #FFFFFF;font-weight: normal;}
.datable .lupbai {background-color: #FFFFFF;}
.datable .trnei {background-color: #F2F9FF;}
.datable td {border-top: 1px solid #FFFFFF; border-left: 1px solid #FFFFFF;}

       
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



            function selectdept() {
                obj = new Object();
                resu = window.showModalDialog("../../RMBase/SysBaseInfo/SelectDept.aspx", obj, "dialogWidth=600px;dialogHeight=460px; ");
                if (resu != null) {
                    document.getElementById("hidDeptName").value = resu.name;
                    document.getElementById("hidDeptCode").value = resu.code;
                }
            }

            function selectMaterial() {
                obj = new Object();
                resu = window.showModalDialog("../../RMBase/SysBaseInfo/SelectMaterial.aspx", obj, "dialogWidth=600px;dialogHeight=460px; ");
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



            //获取表单值
            function CheckValid() {
                if (!CheckDataValid('#form1')) {
                    return false;
                }
                var item_value = "";
                $("#table1 tbody  tr").each(function (i) {
                    item_value += $(this).find('td:eq(2)').html() + ",";
                })
                $("#User_ID_Hidden").val(item_value);
                if (!confirm('确认要保存此操作吗？')) {
                    return false;
                }
            }





            //清空File控件的值，并且预览处显示默认的图片
            function clearFileInput() {
                var form = document.createElement('form');
                document.body.appendChild(form);
                //记住file在旧表单中的的位置
                var file = document.getElementById("MaterialPic_ID_Hidden");
                var pos = file.nextSibling;
                form.appendChild(file);
                form.reset(); //通过reset来清空File控件的值
                document.getElementById("colspan").appendChild(file);
                document.body.removeChild(form);
                //在预览处显示图片 这是在浏览器支持滤镜的情况使用的
                document.getElementById("idImg").style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src='images/abshiu.jpg'";
                //这是是火狐里面显示默认图片的
                if (navigator.userAgent.indexOf('Firefox') >= 0) {
                    $("#idImg").attr('src', 'images/abshiu.jpg');
                }
            }

          

</script> 


</head>

<body>
    <form id="form2" runat="server">

     <div class="btnbartitle">
        <div>
            物资采购表单
        </div>
    </div>
     <div class="btnbarcontetn">

        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pnlContent" runat="server"  CssClass ="width100">
        <table  class ="width100">
            <tr>
                <td>
                    <table class="example" id="dnd-example">
                        <tr>
                            <td class="style2">
                                采购单据号</td>
                            <td>
                                <asp:TextBox ID="txtPurchasePlanBillCode" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ErrorMessage="单据号必填" ControlToValidate="txtPurchasePlanBillCode" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>

                                 <td class="style2">
                                采购部门</td>
                            <td>
                                <input id="hidDeptCode" type="hidden" runat="server"/>
                                <input id="hidDeptName" type="hidden" runat="server"/>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upnlDeptName" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDeptName" runat="server" ReadOnly ="true"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSelectDept" EventName="click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSelectDept" runat="server" Text="选择" 
                                                        OnClientClick =" return selectdept() " onclick="btnSelectDept_Click" 
                                                        ValidationGroup="empty"/>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                        ErrorMessage="部门必填" ControlToValidate="txtDeptName" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>

                          <td class="style2">
                                采购日期</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPurchasePlanDate" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgPurchasePlanDate" runat="server"  ImageUrl="~/Themes/Images/calendar.gif" OnClientClick ="WdatePicker({el:'txtPurchaseDate',dateFmt:'yyyy-MM-dd HH:mm:ss'})"/>
                                          
                                        </td>
                                        <td>
                                            </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                        ErrorMessage="经办日期必填" ControlToValidate="txtPurchasePlanDate" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                               <asp:TextBox ID="txtMaterialName" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
                                               
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
                                  CssClass="grid">
                      
                      <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" 
                                       LastPageText="末页" NextPageText="下一页" PreviousPageText="上一页" />
                      
                       <AlternatingRowStyle CssClass="trnei" />

                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                                            SortExpression="ID" Visible="false" />
                            <asp:BoundField DataField="PurchaseBillCode" HeaderText="PurchaseBillCode" 
                                            SortExpression="PurchaseBillCode" Visible="false" />
                          
                          <asp:BoundField DataField="Material_ID" HeaderText="物料编码" SortExpression="Material_ID" />
                          <asp:BoundField DataField="Material_Name" HeaderText="物料名称" SortExpression="Material_Name" />
                          <asp:BoundField DataField="Material_Specification" HeaderText="物料规格" SortExpression="Material_Specification" />
                           <asp:BoundField DataField="Material_Supplier" HeaderText="物料生产厂商" SortExpression="Material_Specification" />
                            <asp:BoundField DataField="Unit" HeaderText="单位" SortExpression="Unit" />
                            <asp:BoundField DataField="Quantity" HeaderText="数量" 
                                            SortExpression="Quantity" />
                                            <asp:BoundField DataField="Memo" HeaderText="备注" 
                                            SortExpression="Memo" />
                            <asp:ButtonField CommandName="Del" Text="删除" HeaderText="编辑" />
                        </Columns> 
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table  class="example" id="dnd-example">
                        <tr>
                            <td>采购人:<asp:Label ID ="lblOperator" runat="server" Text = ""></asp:Label></td>
                            
                             <td>数量合计:<asp:Label ID ="lblTotalQuantity" runat="server" Text = "0"></asp:Label></td>
                                       
                                      
                                    </tr>
                                </table>
                            </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td><asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" OnClientClick="return CheckValid();"/></td>
                            <td>
                                <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" 
                                            ValidationGroup="empty" /></td>

                                         
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>


  <div id="divSCA" >
    <asp:Panel ID="pnlDetail" runat="server">
        <table  class="example" >
            <tr>
                <td>
                    <asp:UpdatePanel ID="upnlProductCode" runat="server">
                        <ContentTemplate>
                            <table width="100%" class="example">
                                <tr>
                                    <td class="style2">
                            
                                        物资代码</td>
                                    <td>
                                        <table class="example">
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
                                    <td >
                        
                                        物资规格</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtSpecs" runat="server" Enabled ="false"></asp:TextBox>
                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                        
                                        单位</td>
                                    <td>

                                        <asp:DropDownList ID="ddlUnit" runat="server" Enabled="false">
                                        </asp:DropDownList>
                        
                                    </td>
                                </tr>
                                 <tr>
                                    <td >
                        
                                        生产厂商</td>
                                    <td>
                        
                                        <asp:TextBox ID="txtVendor" runat="server" Enabled ="false"></asp:TextBox>
                        
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                        
                                        数量</td>
                                    <td>
                                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                        
                                        <asp:CompareValidator 
                                            ID="CompareValidatorValidInt" runat="server" ControlToValidate="txtQuantity" 
                                            ErrorMessage="请输入正确的数字格式" Operator="DataTypeCheck" Type="Integer" 
                                            Display="Dynamic"></asp:CompareValidator>
                                    </td>
                                </tr>
                               
                                 <tr>
                                    <td>
                        
                                        备注</td>
                                    <td>
                                        <asp:TextBox ID="txtComm" runat="server"></asp:TextBox>
                                        
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
