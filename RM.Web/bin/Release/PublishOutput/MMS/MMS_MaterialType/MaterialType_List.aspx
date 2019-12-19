<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialType_List.aspx.cs" Inherits="RM.Web.MMS.MMS_MaterialType.MaterialType_List" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物资类型信息列表</title>
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
        })
        /**
        获取table TD值
        主键ID
        column:列名
        **/
        var MaterialType_ID = '';
        function GetClickTableValue() {
            $('table tr').not('#td').click(function () {
                $(this).find('td').each(function (i) {
                    if (i == 2) {
                        MaterialType_ID = $(this).text();
                    }
                });
            });
            $("#dnd-example").treeTable({
                initialState: "expanded" //collapsed 收缩 expanded展开的
            });
        }
        //新增
        function add() {
            var url = "/MMS/MMS_MaterialType/MaterialType_Form.aspx?ParentId=" + MaterialType_ID;
            top.openDialog(url, 'MaterialType_Form', '物料类别信息 - 添加', 700, 335, 50, 50);
        }
        //编辑
        function edit() {
            var key = MaterialType_ID;
            if (IsEditdata(key)) {
                var url = "/MMS/MMS_MaterialType/MaterialType_Form.aspx?key=" + key;
                top.openDialog(url, 'MaterialType_Form', '物料类别信息 - 编辑', 700, 335, 50, 50);
            }
        }
        //删除
        function Delete() {
            var key = MaterialType_ID;
            if (IsDelData(key)) {
                var isExistparm = 'action=IsExist&tableName=MMS_MaterialType&pkName=ParentId&pkVal=' + key;
                if (IsExist_Data('/Ajax/Common_Ajax.ashx', isExistparm) > 0) {
                    showWarningMsg("该数据被关联,0 行受影响！");
                    return false;
                }
                var delparm = 'action=Virtualdelete&module=物料类型管理&tableName=MMS_MaterialType&pkName=MaterialType_ID&pkVal=' + key;
                delConfig('/Ajax/Common_Ajax.ashx', delparm)
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="btnbartitle">
        <div>
            物资类型信息列表
        </div>
    </div>
    <div class="btnbarcontetn">
        <div style="float: left;">
        </div>
        <div style="text-align: right">
            <uc1:LoadButton ID="LoadButton1" runat="server" />
        </div>
    </div>
    <div class="div-body">
        <table class="example" id="dnd-example">
            <thead>
                <tr>
                <td style="width: 380px;padding-left:200px;">
                        物资类型名称
                    </td>
                    <td style="width:100px; padding-left: 50px;">
                        物资类型编码
                    </td>
                  
                </tr>
            </thead>
            <tbody>
                <%=str_tableTree.ToString()%>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>

