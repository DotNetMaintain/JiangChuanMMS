<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelImportStorage.aspx.cs" Inherits="RM.Web.MMS.StorageManagement.ExcelImportStorage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>从EXCEL中导入库存</title>

    
    
    <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/Validator/JValidator.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/artDialog.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/iframeTools.source.js" type="text/javascript"></script>
    <link href="/Themes/Scripts/TreeView/treeview.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/TreeView/treeview.pack.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="frmtop">
       <table border="0" cellpadding="0" cellspacing="0" class="frm">
             <tr>
            <th>
                EXCEL文件选择：
            </th>
            <td>
                <input id="fileImport" runat="server" type="file" />
                <asp:Button  ID="Import" runat="server" Text="导入" onclick="Import_Click" />
            </td>
            
        </tr>
        
        </table>
    </div>
    </form>
</body>
</html>
