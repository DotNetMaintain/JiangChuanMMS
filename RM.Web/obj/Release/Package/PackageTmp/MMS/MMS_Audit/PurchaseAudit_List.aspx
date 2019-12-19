<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseAudit_List.aspx.cs" Inherits="RM.Web.MMS.MMS_Audit.PurchaseAudit_List" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物资采购审核</title>

 
    <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/Validator/JValidator.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/artDialog.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/iframeTools.source.js" type="text/javascript"></script>
    <link href="/Themes/Scripts/TreeView/treeview.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/TreeView/treeview.pack.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
    <script type="text/javascript">
        //初始化
        $(function () {
            divresize(63);
            FixedTableHeader("#dnd-example", $(window).height() - 91);
            GetClickTableValue();
        })



        var data = [];

        function GetClickTableValue() {  //jquery获取复选框值
            $('table tr').not('#td').click(function () {

                var chk_value = [];

                $('input[name="checkbox"]:checked').each(function () {
                    chk_value.push($(this).val());
                });
                data = chk_value;
            });

        }



        //新增
        function audit() {

            // var JsonStr = JSON.stringify(data);
            auditConfig('/Ajax/AuditPurchase_Button.ashx', data)


        }

        //取消退回审核
        function OpenClose() {
            auditReturnConfig('/Ajax/AuditPurchaseReturn_Button.ashx', data)
        }

        


    </script>
    <style type="text/css">
        .style1
        {
            width: 642px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div class="btnbartitle">
        <div>
            物资采购审核列表
        </div>
    </div>
    <div class="btnbarcontetn">
       
        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

        

    </div>
    <div class="div-body ">
    <table class="grid">
       
                <thead>
                <tr>
                    <td style="width: 20px; text-align: left;">
                        <label id="checkAllOff" onclick="CheckAllLine()" title="全选">
                            &nbsp;</label>
                    </td>
                    <td style="width: 60px; text-align: center;">
                        采购单号
                    </td>
                   
                     <td style="width: 80px; text-align: center;">
                        采购部门
                    </td>
                    <td style="width: 150px; text-align: center;">
                        采购人工号
                    </td>
                    <td style="width: 100px; text-align: center;">
                        采购日期
                    </td>
                    <td style="width: 100px; text-align: center;">
                        审核关卡
                    </td>

                     <td style="width: 50px; text-align: center;">
                        详细信息
                    </td>
                                       
                </tr>
            </thead>
           

          

            <tbody>
                <asp:Repeater ID="rp_Item" runat="server" OnItemDataBound="rp_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 20px; text-align: left;">
                            <input type="checkbox" value="<%#Eval("ID")%>" name="checkbox" />
                             
                            </td>
                            <td style="width: 60px; text-align: center;">
                                <%#Eval("PurchaseBillCode")%>
                            </td>
                          
                            <td style="width: 100px; text-align: center;">
                                <%#Eval("DeptName")%>
                            </td>
                            <td style="width: 200px; text-align: center;">
                               <%#Eval("Operator")%>
                            </td>
                            <td style="width: 100px; text-align: center;">
                               <%#Eval("PurchaseDate")%>
                            </td>
                            <td style="width: 100px; text-align: center;">
                               <%#Eval("PayMode")%>
                            </td>
                            <td style="width: 50px; text-align: center;">
                             <a href="../MMS_Purchase/Purchase_Form.aspx?Audit=True&&ID=<%#Eval("ID")%>" target=_blank >详细信息</a>
                            </td>
                            
                        </tr>
                    </ItemTemplate>
            
                    <FooterTemplate>
                        <% if (rp_Item != null)
                           {
                               if (rp_Item.Items.Count == 0)
                               {
                                   Response.Write("<tr><td colspan='10' style='color:red;text-align:center'>没有找到您要的相关数据！</td></tr>");
                               }
                           } %>
                    </FooterTemplate>
                </asp:Repeater>
            </tbody>


       
    </table>

     

    </div>
    </form>
</body>
</html>









