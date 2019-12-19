<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectMaterialOrg_List.aspx.cs" Inherits="RM.Web.RMBase.SysBaseInfo.SelectMaterialOrg_List" %>

<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>物资信息</title>
     <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/jquery.pullbox.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
     <script type="text/javascript">
         //回车键
         document.onkeydown = function (e) {
             if (!e) e = window.event; //火狐中是 window.event
             if ((e.keyCode || e.which) == 13) {
                 var obtnSearch = document.getElementById("lbtSearch");
                 obtnSearch.click();
             }
         }
         $(function () {
             $(".div-body").PullBox({ dv: $(".div-body"), obj: $("#table1").find("tr") });
             divresize(90);
             FixedTableHeader("#table1", $(window).height() - 118);




         })
         //添加
         function add() {

             var name;
             var spec;
             var vendor;
        
             $("#table1").find(":checkbox:checked").each(function () {

                 var $td = $(this).parents('tr').children('td');


                 name = $td.eq(1).text();
                 spec = $td.eq(2).text();
                 vendor = $td.eq(5).text();

             });

             obj = new Object();

             var key = CheckboxValue();
            
             if (IsEditdata(key)) {
                 resu = new Object();
                 resu.code = key;
                 resu.name = name;
                 resu.spec = spec;
                 resu.vendor = vendor;
                 window.returnValue = resu;
               
                 window.close();

              
             }


         }
        
         //锁 定
         function lock() {
             var key = CheckboxValue();
             if (IsEditdata(key)) {
                 var parm = 'action=lock&user_ID=' + key;
                 showConfirmMsg('注：您确认要【锁 定】当前选中用户吗？', function (r) {
                     if (r) {
                         getAjax('UserInfo.ashx', parm, function (rs) {
                             if (parseInt(rs) > 0) {
                                 showTipsMsg("锁定成功！", 2000, 4);
                                 windowload();
                             }
                             else {
                                 showTipsMsg("<span style='color:red'>锁定失败，请稍后重试！</span>", 4000, 5);
                             }
                         });
                     }
                 });
             }
         }
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div class="btnbartitle">
        <div>
            物资列表
        </div>
    </div>
    <div class="btnbarcontetn">
        <div style="float: left;">
            <select id="Searchwhere" class="Searchwhere" runat="server">
                <option value="Material_CommonlyName">物资简称</option>
                <option value="material_name">物资名称</option>
                
            </select>
            <input type="text" id="txt_Search" class="txtSearch SearchImg" runat="server" style="width: 100px;" />
            <asp:LinkButton ID="lbtSearch" runat="server" class="button green" OnClick="lbtSearch_Click"><span class="icon-botton"
            style="background: url('../../Themes/images/Search.png') no-repeat scroll 0px 4px;">
        </span>查 询</asp:LinkButton>
        </div>
        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>
    </div>
    <div class="div-body">
        <table id="table1" class="grid" singleselect="true">
            <thead>
                <tr>
                    <td style="width: 25px; text-align: left;">
                        <label id="checkAllOff" onclick="CheckAllLine()" title="全选">
                         </label>
                    </td>
                    <td style="width: 230px; text-align: center;">&nbsp;
                        物料名称
                    </td>
                    <td style="width: 105px; text-align: center;">&nbsp;
                        物料规格
                    </td>
                     <td style="width: 40px; text-align: center;">&nbsp;
                        单位
                    </td>
                   
                    <td style="width: 255px; text-align: center;">&nbsp;
                        物料类型
                    </td>
                    <td style="width: 110px; text-align: center;">&nbsp;
                        生产厂商
                    </td>
                                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rp_Item" runat="server" OnItemDataBound="rp_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 20px; text-align: left;">
                                <input type="checkbox" value="<%#Eval("Material_ID")%>" name="checkbox" />
                            </td>
                               <td style="width: 180px; text-align: center;">
                                <%#Eval("material_name")%>
                            </td>
                            <td style="width: 80px; text-align: center;">
                                <a href="javascript:void()">
                                    <%#Eval("Material_Specification")%></a>
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <%#Eval("Material_Unit")%>
                            </td>
                           <td style="width: 200px; text-align: center;">
                                <%#Eval("Material_Type")%>
                            </td>
                            <td style="width: 200px; text-align: center;">
                                <%#Eval("Material_Supplier")%>
                            </td>
                            
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <% if (rp_Item != null)
                           {
                               if (rp_Item.Items.Count == 0)
                               {
                                   Response.Write("<tr><td colspan='8' style='color:red;text-align:center'>没有找到您要的相关数据！</td></tr>");
                               }
                           } %>
                    </FooterTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <uc1:PageControl ID="PageControl1" runat="server" />
    </form>
</body>
</html>

