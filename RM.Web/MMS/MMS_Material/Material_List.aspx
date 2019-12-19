<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Material_List.aspx.cs" Inherits="RM.Web.MMS.MMS_Material.Material_List" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物资明细</title>
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
        var Material_ID = '';

        function GetClickTableValue() {  //jquery获取复选框值
            $('table tr').not('#td').click(function () {

                var chk_value = [];

                $('input[name="checkbox"]:checked').each(function () {
                    chk_value.push($(this).val());
                });
                Material_ID=chk_value[0];   
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
        //Material_ID = chk_value[0];
    });
}





//        function GetClickTableValue() {
//            $('table tr').not('#td').click(function () {
//                $(this).find('td').each(function (i) {
//                    var chk_value =[];    
//                  $('input[id="Material_ID"]:checked').each(function(){    
//                   chk_value.push($(this).val());   
//                    alert($(this).val(Material_ID));
//                    if (i == 2) {
//                        Material_ID = $(this).text();
//                    }
//                });
//            });
//            $("#dnd-example").treeTable({
//                initialState: "expanded" //collapsed 收缩 expanded展开的
//            });
//        }
        //新增
        function add() {
            var url = "/MMS/MMS_Material/Material_Form.aspx?Material_ID=" + Material_ID;
            top.openDialog(url, 'Material_Form', '物资信息 - 添加', 700, 335, 50, 50);
        }
        //编辑
        function edit() {
            var key = Material_ID;
            if (IsEditdata(key)) {
                var url = "/MMS/MMS_Material/Material_Form.aspx?key=" + key;
                top.openDialog(url, 'Material_Form', '物资信息 - 编辑', 700, 335, 50, 50);
            }
        }
        //删除
        function Delete() {
            var key = Material_ID;
            if (IsDelData(key)){
//                var isExistparm = 'action=IsExist&tableName=MMS_MaterialInfo&pkName=Material_ID&pkVal=' + key;
//                if (IsExist_Data('/Ajax/Common_Ajax.ashx', isExistparm) > 0) {
//                    showWarningMsg("该数据被关联,0 行受影响！");
//                    return false;
//                }
                var delparm = 'action=Virtualdelete&module=物资管理&tableName=MMS_MaterialInfo&pkName=Material_ID&pkVal=' + key;
                delConfig('/Ajax/Common_Ajax.ashx', delparm)
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
            物资信息列表
        </div>
    </div>
    <div class="btnbarcontetn">
        <div style="float: left;">
            <select id="Searchwhere" class="Searchwhere" runat="server">
                <option value="MATERIAL_TYPE">物料类型</option>
                <option value="MATERIAL_NAME">物料名称</option>
                <option value="MATERIAL_COMMONLYNAME">物料简称</option>
                <option value="MATERIAL_SUPPLIER">生产厂商</option>
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
                        物资类型
                    </td>
                    <td style="width: 80px; text-align: center;">
                        物资编号
                    </td>
                    <td style="width: 100px; text-align: center;">
                        物资名称
                    </td>
                    <td style="width: 100px; text-align: center;">
                        物料简称
                    </td>
                    <td style="width: 200px; text-align: center;">
                        物资规格
                    </td>
                    <td style="width: 50px; text-align: center;">
                        物资单位
                    </td>
                    
                    <td style="width: 100px; text-align: center;">
                        生产厂家
                    </td>
                    <td style="width: 100px; text-align: center;">
                        物资图片
                    </td>
                     <td style="width: 100px; text-align: center;">
                        是否启用状态
                    </td>
                      <td style="width: 100px; text-align: center;">
                        安全库存
                    </td>
                    <td style="width: 100px; text-align: center;">
                        备注
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
                                <asp:Label ID="Material_CommonlyName" runat="server" Text='<%#Eval("Material_CommonlyName")%>'></asp:Label>
                            </td>
                            <td style="width: 200px; text-align: center;">
                                <%#Eval("Material_Specification")%>
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <asp:Label ID="Material_Unit" runat="server" Text='<%#Eval("Material_Unit")%>'></asp:Label>
                            </td>
                             <td style="width: 100px; text-align: center;">
                                <asp:Label ID="Material_Supplier" runat="server" Text='<%#Eval("Material_Supplier")%>'></asp:Label>
                            </td>

                           
                             <td style="width: 100px; height :50px; text-align: center;">
                              <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Material_Pic")%>' />
                            </td>
                              <td style="width: 50px; text-align: center;">
                                <asp:Label ID="lblDeleteMark" runat="server" Text='<%#Eval("DeleteMark")%>'></asp:Label>
                            </td>
                            <td style="width: 100px; text-align: center;">
                                <asp:Label ID="Material_SafetyStock" runat="server" Text='<%#Eval("Material_SafetyStock")%>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("Material_Comm")%>
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

