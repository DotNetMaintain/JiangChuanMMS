<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliveryList.aspx.cs" Inherits="RM.Web.MMS.StorageManagement.DeliveryList" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> 物资发货列表</title>
</head>
<body>
    <form id="form1" runat="server">

    <div class="btnbartitle">
        <div>
           物资发货列表
        </div>
    </div>
    <div class="btnbarcontetn">
        <div style="float: left;">
            <select id="Searchwhere" class="Searchwhere" runat="server">
                <option value="Material_Name">物资名称</option>
                <option value="DeptName">领料部门</option>
                <option value="User_Name">领料单号</option>
            </select>
            <input type="text" id="txt_Search" class="txtSearch SearchImg" runat="server" style="width: 100px;" />
            <asp:LinkButton ID="lbtSearch" runat="server" class="button green" OnClick="lbtSearch_Click"><span class="icon-botton"
            style="background: url('../../Themes/images/Search.png') no-repeat scroll 0px 4px;">
        </span>查 询</asp:LinkButton>
        </div>
        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

        <div class="div-body">
        <table id="table1" class="grid" singleselect="true">
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
                          
                          <asp:BoundField DataField="Material_Code" HeaderText="申领部门" SortExpression="Material_Code" />
                          <asp:BoundField DataField="Material_Name" HeaderText="申领单号" SortExpression="Material_Name" />
                          <asp:BoundField DataField="Material_Specification" HeaderText="物资编码" SortExpression="Material_Specification" />
                            <asp:BoundField DataField="Unit" HeaderText="物资名称" SortExpression="Unit" />
                            <asp:BoundField DataField="Quantity" HeaderText="物资规格"SortExpression="Quantity" />
                            <asp:BoundField DataField="Price" HeaderText="物资单位" SortExpression="Price" />
                            <asp:BoundField DataField="Amount" HeaderText="物资厂家" SortExpression="Price" />
                            <asp:BoundField DataField="Amount" HeaderText="申领数量" SortExpression="Price" />
                            <asp:BoundField DataField="Amount" HeaderText="物资批号" SortExpression="Price" />

                        </Columns> 
                    </asp:GridView>
        </td>
        </tr>
        </table>
        </div>


    </div>
    </form>
</body>
</html>
