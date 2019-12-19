<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StorageQuery.aspx.cs" Inherits="RM.Web.MMS.StorageManagement.StorageQuery" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入库单明细查询</title>
     <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="btnbartitle">
        <div>
            入库单明细查询
        </div>
    </div>
     <div class="btnbarcontetn">
        <div style="float: left;">
            查询日期:
            <asp:TextBox ID="ttbStartDate" runat="server"  class="txt"  style="width: 115px;"
                onfocus="WdatePicker({dateFmt: 'yyyy-MM-dd 00:00:00' })" ></asp:TextBox>
            至
             <asp:TextBox ID="ttbEndDate" runat="server"  class="txt"  style="width: 115px;"
                onfocus="WdatePicker({dateFmt: 'yyyy-MM-dd 00:00:00' })" ></asp:TextBox>

            入库单号：
             <asp:TextBox ID="ttbInStorageID" runat="server"  class="txt"  style="width: 115px;"></asp:TextBox>
             发票号：
               <asp:TextBox ID="ttbInvoiceCode" runat="server"  class="txt"  style="width: 115px;"></asp:TextBox>
             物料类型：
               <asp:TextBox ID="ttbMateriay_type" runat="server"  class="txt"  style="width: 115px;"></asp:TextBox>
             物料名称：
               <asp:TextBox ID="ttbMateriay_Name" runat="server"  class="txt"  style="width: 115px;"></asp:TextBox>

            <asp:LinkButton ID="lbtSearch" runat="server" class="button green" 
                onclick="lbtSearch_Click"><span class="icon-botton"
            style="background: url('/Themes/images/Search.png') no-repeat scroll 0px 4px;">
        </span>查 询</asp:LinkButton>
        </div>
    </div>
     <div class="div-body">
       
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
          
      </div>


    </form>
    

</body>
</html>
