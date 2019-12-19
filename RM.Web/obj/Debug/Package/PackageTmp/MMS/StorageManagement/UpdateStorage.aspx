<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateStorage.aspx.cs" Inherits="RM.Web.MMS.StorageManagement.UpdateStorage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/Themes/Scripts/TreeView/treeview.css" rel="stylesheet" type="text/css" />
    <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
    <script>
function OpenClose() {
    art.dialog.close();
}
    </script>
</head>
    
<body>
    <form id="form1" runat="server">
        <div style="margin-left:20px;margin-top:20px;">
            申请数量:<input id="quantity" runat="server" type="text" err="申请数量"/>
        </div>
    <div class="frmbottom" style="margin-top:170px;">
        <asp:LinkButton ID="Save" runat="server" class="l-btn" 
            OnClick="Save_Click"><span class="l-btn-left">
            <img src="/Themes/Images/disk.png" alt="" />保 存</span></asp:LinkButton>
       <%-- <a id="Close" class="l-btn" href="javascript:void(0)" onclick="OpenClose();"><span
            class="l-btn-left">
            <img src="/Themes/Images/cancel.png" alt="" />关 闭</span></a>--%>
    </div>
    </form>
</body>
</html>
