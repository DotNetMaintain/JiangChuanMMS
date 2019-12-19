<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectStorageMaterial.aspx.cs" Inherits="RM.Web.RMBase.SysBaseInfo.SelectStorageMaterial" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <base   target="_self">
        <title>物资库存数选择</title>

          
   <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/Validator/JValidator.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/artDialog.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/iframeTools.source.js" type="text/javascript"></script>
    <link href="/Themes/Scripts/TreeView/treeview.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/TreeView/treeview.pack.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>

        <style type="text/css">
            .style1 { text-align: right; }

            .width100 { width: 100%; }
        </style>
        <script language="javascript" type="text/javascript">
            // <!CDATA[

            function btnSelect_onclick(code, name, price, usequantity) {
                resu = new Object();
                resu.code = code;
                resu.name = name;
                resu.price = price;
                resu.usequantity = usequantity;
                window.returnValue = resu;
                window.close();
            }

            function btnClose_onclick() {
                window.close();
            }

            // ]]>
</script>    
    
    </head>
    <body>
        <form id="form2" runat="server">

         <div class="btnbartitle">
        <div>
            选择客户列表
        </div>
    </div>
     <div class="btnbarcontetn">

        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

    </div>

            <div>
                <table style="border-style: groove; border-width: thin"  class ="width100">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="style1">助记码</td>
                                    
                                    <td>
                                        <asp:TextBox ID="txtHelpCode" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>        
                    </tr>
                    <tr>
                        <td>
                            <table  class ="width100">
                                <tr>
                                    <td>
                                        <cc1:GridView ID="dgvInfo" runat="server" AutoGenerateColumns="False"  Width="100%"
                                                      DataKeyNames="Material_Code" EmptyDataText="没有可显示的数据记录。" 
                                                      AllowPaging="false" 
                                                      onpageindexchanging="dgvInfo_PageIndexChanging" 
                                                      onrowdatabound="dgvInfo_RowDataBound" onrowcommand="dgvInfo_RowCommand"
                                                      EmptyDataTableCssClass="datagridTable" EmptyDataTitleRowCssClass="GvTitlebg" EmptyDataContentRowCssClass="datagridContentRow">
                                            <Columns>
                                                <asp:BoundField DataField="Material_ID" HeaderText="物资代码" 
                                                                SortExpression="Material_ID" />
                                                <asp:BoundField DataField="Material_Name" HeaderText="物资名称" 
                                                                SortExpression="Material_Name" />
                                                <asp:BoundField DataField="Material_Specification" HeaderText="物资规格" 
                                                                SortExpression="Material_Specification" />
                                                <asp:BoundField DataField="Material_Type" HeaderText="物资类型" 
                                                                SortExpression="Material_Type" />
                                                <asp:BoundField DataField="Material_Unit" HeaderText="物资单位" 
                                                                SortExpression="Material_Unit" />
                                               <asp:BoundField DataField="Material_Supplier" HeaderText="物资生产厂商" 
                                                                SortExpression="Material_Supplier" />
                                                <asp:BoundField DataField="qua" HeaderText="可用数量" 
                                                                SortExpression="qua" />
                                                 <asp:BoundField DataField="price" HeaderText="进货价格" 
                                                                SortExpression="price" />
                                                <asp:ButtonField Text="选择" />
                                            </Columns>
                                        </cc1:GridView>
                            
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:ourpager ID="OurPager1" runat="server" onpagechanged="OurPager1_PageChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnClose" runat="server" Text="关闭" 
                                        OnClientClick = " return btnClose_onclick() " UseSubmitBehavior="False"/>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </body>
</html>
