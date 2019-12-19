<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixedAassetsList.aspx.cs" Inherits="RM.Web.MMS.MMS_FixedAassets.FixedAassetsList" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>物资明细 </title>

      
    <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/TreeTable/jquery.treeTable.js" type="text/javascript"></script>
    <link href="/Themes/Scripts/TreeTable/css/jquery.treeTable.css" rel="stylesheet"
        type="text/css" />
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            divresize(63);
            FixedTableHeader("#dnd-example", $(window).height() - 91);
            GetClickTableValue();
            GetClickTablePassValue()
        })
        /**
        获取table TD值
        主键ID
        column:列名
        **/
        var id = '';

        function GetClickTableValue() {  //jquery获取复选框值
            $('table tr').not('#td').click(function () {

                var chk_value = [];

                $('input[name="checkbox"]:checked').each(function () {
                    chk_value.push($(this).val());
                });
                id = chk_value[0];
            });

        }


        function GetClickTablePassValue() {

            $('table tr').not('#td').click(function () {
                var chk_value = [];
                $('input[name="checkbox"]:checked').each(function () {
                    chk_value.push($(this).val());
                    resu = new Object();
                    resu.Material_Code = chk_value[0];
                    // resu.name = "Material_Code";
                    window.returnValue = resu;
                    window.close();

                });

            });
        }


      

        function printer(){
            var url = "../../Print/Print.aspx?id='" + id+"'";
            top.openDialog(url, 'Print', '固定资产信息 - 打印', 700, 335, 50, 50); 

        } 


     
        //新增
        function add() {
            var url = "/MMS/MMS_FixedAassets/FixedAassetsForm.aspx?id=" + id;
            top.openDialog(url, 'FixedAassetsForm', '固定资产信息 - 添加', 700, 335, 50, 50);
        }
        //编辑
        function edit() {
            var key = id;
            if (IsEditdata(key)) {
                var url = "/MMS/MMS_FixedAassets/FixedAassetsForm.aspx?key=" + key;
                top.openDialog(url, 'FixedAassetsForm', '固定资产信息 - 编辑', 700, 335, 50, 50);
            }
        }
        //删除
        function Delete() {
            var key = id;
            if (IsDelData(key)) {

                var delparm = 'action=Delete&module=固定资产&tableName=MMS_FixedAassets&pkName=id&pkVal=' + key;
                delConfig('/Ajax/Common_Ajax.ashx', delparm);
            }
        }





        function btnSelect_onclick(code, name) {
            resu = new Object();
            resu.code = code;
            resu.name = name;
            window.returnValue = resu;
            window.close();
        }


       

    </script>
</head>
<body>
    <form id="form1" runat="server">


    <div class="btnbartitle">
        <div>
            固定资产列表
        </div>
    </div>
    <div class="btnbarcontetn">
        <div style="float: left;">
            <select id="Searchwhere" class="Searchwhere" runat="server">
                <option value="FA_Type">固定资产类型</option>
                <option value="FA_Name">固定资产名称</option>
                <option value="FA_Spec">固定资产型号</option>
                <option value="FA_UseDept">固定资产使用部门</option>
                <option value="FA_User">固定资产使用人</option>
                <option value="FA_Place">固定资产存放地点</option>
                 <option value="FA_Status">固定资产状态</option>
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
                    <td style="width: 20px; text-align: left;">
                        <label id="checkAllOff" onclick="CheckAllLine()" title="全选">
                            &nbsp;</label>
                    </td>
                    <td style="width: 60px; text-align: center;">
                        编号
                    </td>
                    <td style="width: 80px; text-align: center;">
                        类型
                    </td>
                    <td style="width: 100px; text-align: center;">
                       名称
                    </td>
                    <td style="width: 100px; text-align: center;">
                        型号
                    </td>
                    <td style="width: 200px; text-align: center;">
                        单位
                    </td>
                    <td style="width: 50px; text-align: center;">
                        使用部门
                    </td>
                    
                    <td style="width: 100px; text-align: center;">
                        使用人
                    </td>
                     <td style="width: 100px; text-align: center;">
                        存放地点
                    </td>
                      <td style="width: 100px; text-align: center;">
                        存放地点编码
                    </td>
                    <td style="width: 100px; text-align: center;">
                        价格
                    </td>
                     <td style="width: 100px; text-align: center;">
                        数量
                    </td>
                     <td style="width: 100px; text-align: center;">
                        购买日期
                    </td>
                     <td style="width: 100px; text-align: center;">
                        使用年限
                    </td>
                      <td style="width: 100px; text-align: center;">
                        状态
                    </td>
                      <td style="width: 100px; text-align: center;">
                        物资图片
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rp_Item" runat="server" OnItemDataBound="rp_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 20px; text-align: left;">
                                <input type="checkbox" value="<%#Eval("id")%>" name="checkbox" />
                            </td>
                            <td style="width: 60px; text-align: center;">
                                <%#Eval("FA_Code")%>
                            </td>
                            <td style="width: 80px; text-align: center;">
                                <a href="javascript:void()">
                                    <%#Eval("FA_Type")%></a>
                            </td>
                            <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_Name")%>
                            </td>
                            <td style="width: 100px; text-align: center;">
                               <%#Eval("FA_Spec")%>
                            </td>
                            <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_Unit")%>
                            </td>
                            <td style="width: 50px; text-align: center;">
                                 <%#Eval("FA_UseDept")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_User")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_Place")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_PlaceCode")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_Price")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_Number")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_PurDate")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_UseDate")%>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <%#Eval("FA_Status")%>
                                
                            </td>
                           
                             <td style="width: 100px; height :50px; text-align: center;">
                              <asp:Image ID="lbl_FA_Img" runat="server" ImageUrl='<%#Eval("FA_Img")%>' />
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

      <uc1:PageControl ID="PageControl1" runat="server" />

    </div>
   
    </form>
</body>
</html>


