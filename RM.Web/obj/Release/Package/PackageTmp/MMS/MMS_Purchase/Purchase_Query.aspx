<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchase_Query.aspx.cs" Inherits="RM.Web.MMS.MMS_Purchase.Purchase_Query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购单查询</title> 
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
  
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        function showQueryCondition() {
            var obj = new Object();
            obj.EmployeeField = "emp.EmployeeCode";
            obj.EmployeeCondition = "=";
            obj.ClientField = "client.ClientCode";
            obj.ClientCondition = "=";
            obj.ProductField = "product.ProductCode";
            obj.ProductCondition = "=";
            obj.WarehouseField = ""; //house.WareHouseCode
            obj.WarehouseCondition = ""; //=
            obj.BeginDateField = "a.PurchaseDate";
            obj.BeginDateCondition = ">=";
            obj.EndDateField = "a.PurchaseDate";
            obj.EndDateCondition = "<=";
            obj.BillCodeField = "a.PurchaseBillCode";
            obj.BillCodeCondition = "=";

            resu = window.showModalDialog("../Query/CommonQuery.aspx", obj, "dialogWidth=400px;dialogHeight=350px; status:no;scroll:no;resizable:no;");
            if (resu != null) {
                document.getElementById("<%= hidQueryCondition.ClientID %>").value = resu;
            } else {
                return false;
            }
        }
        // ]]>
</script> 
</head>  
    
    <body>
    <form id="form2" runat="server">
       <div class="div-body">
    <table class="example" id="dnd-example">
        <tr>
            <td>
                <table class ="width100">
                    <tr>
                        <td>
                            <cc1:GridView ID="dgvInfo" runat="server" AutoGenerateColumns="False" 
                                          DataKeyNames="ID" EmptyDataText="没有可显示的数据记录。" 
                                          onrowcommand="dgvInfo_RowCommand" 
                                          onpageindexchanging="dgvInfo_PageIndexChanging" 
                                          onrowdatabound="dgvInfo_RowDataBound" ShowFooter="True"
                                          CssClass="grid"
                                          EmptyDataTableCssClass="datagridTable" EmptyDataTitleRowCssClass="GvTitlebg" EmptyDataContentRowCssClass="datagridContentRow">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                                                    SortExpression="ID" Visible ="false" />
                                    <asp:BoundField DataField="PurchaseBillCode" HeaderText="采购单号" 
                                                    SortExpression="PurchaseBillCode" />
                                     <asp:BoundField DataField="DeptName" HeaderText="采购部门" 
                                                    SortExpression="DeptName" />
                                    <asp:BoundField DataField="PurchaseMan" HeaderText="采购人" 
                                                    SortExpression="PurchaseMan" />
                                    <asp:BoundField DataField="PurchaseDate" HeaderText="采购日期" 
                                                    SortExpression="PurchaseDate" DataFormatString="{0:yyyy-MM-dd}"/>
                                                                       
                                    <asp:ButtonField CommandName="Edi" Text="详细信息" />
                                </Columns>
                            </cc1:GridView>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:OurPager ID="OurPager1" runat="server" onpagechanged="OurPager1_PageChanged" />                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class ="textAlignCenter">
                <input id="hidQueryCondition" type="hidden" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnQueryCondition" runat="server" Text="查询条件" 
                                        OnClientClick=" return showQueryCondition(); " 
                                        onclick="btnQueryCondition_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
    </form>
</body>
</html>
