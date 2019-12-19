<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DictionaryManager.aspx.cs" Inherits="RM.Web.RMBase.SysDictionaryManage.DictionaryManager" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>
    <head id="Head1" runat="server">
    <title>数据字典表单</title>
     <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/Validator/JValidator.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/artDialog.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/iframeTools.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>

    <style type="text/css">
        .style3 { text-align: center; }
    </style>

    <script language="javascript" type="text/javascript">
        // <!CDATA[
        function btnSelect_onclick(DictionaryInfo_ID) {
            obj = new Object();

            code = document.getElementById("<%= hidDictType.ClientID %>").value;
            var url = "";
            if (DictionaryInfo_ID == undefined || DictionaryInfo_ID == null) {
                url = "/RMBase/SysDictionaryManage/DictionaryInput.aspx?code=" + code;
            } else {
                url = "DictionaryInput.aspx?code=" + code + "&DictionaryInfo_ID=" + DictionaryInfo_ID;
            }
            resu = window.showModalDialog(url, obj, "dialogWidth=360px;dialogHeight=240px; status:no;scroll:no;resizable:no;");
            if (resu != null) {
            }
        }
        // ]]>


        
             //新增
             function add() {
                 var url = "/RMBase/SysDictionaryManage/DictionaryTypeInput.aspx";
                 top.openDialog(url, 'DictionaryTypeInput', '数据字典表单 - 添加', 700, 335, 50, 50);
             }
             //编辑
             function edit() {
                 var key = StorageForm_ID;
                 if (IsEditdata(key)) {
                     var url = "/MMS/StorageManagement/StorageForm.aspx?key=" + key;
                     top.openDialog(url, 'StorageForm', '进货单 - 编辑', 700, 335, 50, 50);
                 }
             }
             //删除
             function Delete() {
                 var key = StorageForm_ID;
                 if (IsDelData(key)) {
                     var delparm = 'action=Virtualdelete&module=物资管理&tableName=MMS_MaterialInfo&pkName=StorageForm_ID&pkVal=' + key;
                     delConfig('/Ajax/Common_Ajax.ashx', delparm)
                 }
             }




</script>    
</head>
<body>
<form id="form1" runat="server">
<div class="btnbarcontetn">

        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <input id="hidDictType" type="hidden" runat ="server" />
    <table style="border-style: groove; border-width: thin">
        <tr>
            <td>
                <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Width="400" 
                            onnextbuttonclick="Wizard1_NextButtonClick" 
                            onfinishbuttonclick="Wizard1_FinishButtonClick" 
                            onsidebarbuttonclick="Wizard1_SideBarButtonClick">
                    <WizardSteps>
                        <asp:WizardStep ID="WizardStep1" runat="server" Title="第一步:选择字典类别">
                            <asp:ListBox ID="lbDictType" runat="server" Width = "260" Height="180"></asp:ListBox>
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="第二步:编辑字典项目">
                            <table style="border-style: groove; border-width: thin" width="260">
                                <tr>
                                    <td>
                                        <asp:GridView ID="dgvInfo" runat="server" AutoGenerateColumns="False" Width="100%" 
                                                      DataKeyNames="DictionaryInfo_ID" EmptyDataText="没有可显示的数据记录。" 
                                                      onrowcommand="dgvInfo_RowCommand" AllowPaging="True" 
                                                      onpageindexchanging="dgvInfo_PageIndexChanging" 
                                                      onrowdatabound="dgvInfo_RowDataBound"
                                                      EmptyDataTableCssClass="datagridTable" EmptyDataTitleRowCssClass="GvTitlebg" EmptyDataContentRowCssClass="datagridContentRow">
                                            <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" 
                                                           LastPageText="末页" NextPageText="下一页" PreviousPageText="上一页" />
                                            <Columns>
                                                <asp:BoundField DataField="DictionaryInfo_ID" HeaderText="DictionaryInfo_ID" ReadOnly="True" 
                                                                SortExpression="DictionaryInfo_ID" Visible="false" />
                                                <asp:BoundField DataField="ValueCode" HeaderText="字典项" 
                                                                SortExpression="ValueCode" />
                                                <asp:BoundField DataField="ValueName" HeaderText="字典值" 
                                                                SortExpression="ValueName" />
                                                <asp:ButtonField CommandName="Edi" Text="详细信息" />
                                                <asp:ButtonField CommandName="Del" Text="删除" />
                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>
                                <tr>
                                    <td class ="style3">
                                        <asp:Button ID="btnAdd" runat="server" Text="添加" OnClientClick=" btnSelect_onclick(); " OnClick="btnAdd_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard> 
            
            </td>
        </tr>
    </table>    
</form>
</body>
</html>