<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Requisition_Info.aspx.cs" Inherits="RM.Web.MMS.MMS_Requisition.Requisition_Info" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物资领用表单</title>

 
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
            treeAttrCss();
            $("#UserInfolist").height(270);
            $("#UserInfolistright").height(273);
            $("#UserInfolist").hide();
            //SubmitCheckForRC();
        })
        //双击添加员工
        function addUserInfo(userName, userID, Organization_Name) {
            var IsExist = true;
            $("#table1 tbody tr").each(function (i) {
                if ($(this).find('td:eq(0)').html() == userName) {
                    IsExist = false;
                    return false;
                }
            })
            if (IsExist == true) {
                $("#table1 tbody").append("<tr ondblclick='$(this).remove()'><td>" + userName + "</td><td>" + Organization_Name + "</td><td  style='display:none'>" + userID + "</td></tr>");
                publicobjcss();
            } else {
                showWarningMsg("【" + userName + "】员工已经存在");
            }
        }
        //面板切换
        function TabPanel(id) {
            if (id == 1) {
                $(".frm").show();
                $("#UserInfolist").hide();
            } else if (id == 2) {
                $(".frm").hide();
                $("#UserInfolist").show();
                //固定表头
                FixedTableHeader("#table1", $(window).height() - 127);
            }
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


    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div class="btnbartitle">
        <div>
            物资领用表单
        </div>
    </div>
     <div class="btnbarcontetn">

        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

    </div>

    <div>
    <asp:Panel ID="pnlContent" runat="server"  CssClass ="width100">
          <table>
               <tr>
                 <td class="textAlignRight">
                                领料单据号</td>
                            <td>
                                <asp:Label ID="lblRequisitionFormCode" runat="server" ></asp:Label>
                              
                            </td>
                            <td class="textAlignRight">
                                领料日期</td>
                            <td>
                                <asp:Label ID="lblRequisitionDate" runat="server" ></asp:Label></td>
                            <td class="textAlignRight">
                                领料部门</td>
                            <td>
                                <asp:Label ID="lblRequisitionDept" runat="server" ></asp:Label></td>
                             <td class="textAlignRight">
                                领料人</td>
                            <td>
                                <asp:Label ID="lblRequisitionUser" runat="server" ></asp:Label></td>
                        </tr>
                    </table>
 
    </asp:Panel>

     <div class="div-body">
         <table id="table1" class="grid" singleselect="true">
            <thead>
                <tr>
                    <td style="width: 20px; text-align: left;">
                        <label id="checkAllOff" onclick="CheckAllLine()" title="全选">
                            &nbsp;</label>
                    </td>
                    <td style="width: 60px; text-align: center;">
                        领料单号
                    </td>
                    <td style="width: 80px; text-align: center;">
                        领料部门
                    </td>
                    <td style="width: 100px; text-align: center;">
                        领料人
                    </td>
                    <td style="width: 100px; text-align: center;">
                        领料日期
                    </td>
                    <td style="width: 200px; text-align: center;">
                        领料审批人
                    </td>
                    <td style="width: 50px; text-align: center;">
                        领料单状态
                    </td>
                    
                    <td style="width: 100px; text-align: center;">
                        备注
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rp_Item" runat="server" >
                    <ItemTemplate>
                        <tr>
                            <td style="width: 20px; text-align: left;">
                                <input type="checkbox" value="<%#Eval("Material_ID")%>" name="checkbox" />
                            </td>
                            <td style="width: 60px; text-align: center;">
                                <%#Eval("Material_Type")%>
                            </td>
                            <td style="width: 80px; text-align: center;">
                                <a href="javascript:void()">
                                    <%#Eval("Material_Code")%></a>
                            </td>
                            <td style="width: 100px; text-align: center;">
                                <%#Eval("Material_Name")%>
                            </td>
                            <td style="width: 100px; text-align: center;">
                                <asp:Label ID="lblUser_Sex" runat="server" Text='<%#Eval("Material_CommonlyName")%>'></asp:Label>
                            </td>
                            <td style="width: 200px; text-align: center;">
                                <%#Eval("Material_Specification")%>
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="lblDeleteMark" runat="server" Text='<%#Eval("Material_Unit")%>'></asp:Label>
                            </td>
                            
                            <td>
                                <%#Eval("Material_ApplyAttr")%>
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
   
    </div>
    </form>
</body>
</html>
