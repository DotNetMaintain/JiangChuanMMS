<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchase_Info.aspx.cs" Inherits="RM.Web.MMS.MMS_Purchase.Purchase_Info" %>
<%@ Register Src="../../UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/LoadButton.ascx" TagName="LoadButton" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购申请表单</title>

 
    <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/Validator/JValidator.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/artDialog.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/iframeTools.source.js" type="text/javascript"></script>
    <link href="/Themes/Scripts/TreeView/treeview.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/TreeView/treeview.pack.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
     <script src="/Themes/Scripts/LodopFuncs.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        $(function () {
            divresize(63);
            FixedTableHeader("#dnd-example", $(window).height() - 91);
            GetClickTableValue();
        })




        var data = [];
        var LODOP;

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
        function allotButton() {

          //  var JsonStr = JSON.stringify(data);
            deliveryConfig('/Ajax/PurchasePrint.ashx', data)


        }

        //退库还原按钮
        function restore() {

            //var JsonStr = JSON.stringify(data);
            restoreConfig('/Ajax/Restore_Button.ashx', data)


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


        function printer() {
            prn1_preview();
            deliveryConfig('/Ajax/PurchasePrint.ashx', data)
        }



        function prn1_preview() {
            CreateOneFormPage();
            LODOP.PREVIEW();
        };

        function CreateOneFormPage() {
            LODOP = getLodop();
            LODOP.PRINT_INIT("");

            //            LODOP.ADD_PRINT_TEXT(50, 20, 760, 580, "江川社区出库单");
            //            LODOP.SET_PRINT_TEXT_STYLE(1, "宋体", 18, 1, 0, 0, 1);
            //            LODOP.ADD_PRINT_HTM(88, 20, 670, 600, document.getElementById("table1").innerHTML);
            var mydate = new Date();
            var strBodyStyle = "<style>table{ border-collapse:collapse; width:100%; border:3px blue solid}td{ border:1px solid #cccccc;}</style>";
            var strFormHtml = strBodyStyle;// + "<body>" + document.getElementById("Div1").innerHTML + "</body>"
//            LODOP.ADD_PRINT_TEXT(50, 250, 980, 580, "上海闵行江川社区卫生服务中心 采购申请单");
//            LODOP.SET_PRINT_TEXT_STYLE(1, "黑体", 20, 1, 0, 0, 1);

            LODOP.ADD_PRINT_TEXT(15, 350, 2500, 30, "上海闵行江川社区卫生服务中心 采购申请单");
            LODOP.SET_PRINT_STYLEA(1, "FontSize", 13);
            LODOP.SET_PRINT_STYLEA(1, "Bold", 1);

            LODOP.ADD_PRINT_TEXT(50, 15, 100, 20, "采购单号");
            LODOP.SET_PRINT_STYLEA(2, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(2, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(50, 140, 100, 20, "申请部门");
            LODOP.SET_PRINT_STYLEA(3, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(3, "Bold", 1);


            LODOP.ADD_PRINT_TEXT(50, 350, 100, 20, "物资名称");
            LODOP.SET_PRINT_STYLEA(5, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(5, "Bold", 1);


            LODOP.ADD_PRINT_TEXT(50, 550, 100, 20, "物资单位");
            LODOP.SET_PRINT_STYLEA(7, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(7, "Bold", 1);

            LODOP.ADD_PRINT_TEXT(50, 650, 100, 20, "数量");
            LODOP.SET_PRINT_STYLEA(7, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(7, "Bold", 1);

            LODOP.ADD_PRINT_TEXT(50, 750, 100, 20, "备注");
            LODOP.SET_PRINT_STYLEA(8, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(8, "Bold", 1);


            LODOP.ADD_PRINT_LINE(72, 14, 73, 2100, 0, 1);







             LODOP.ADD_PRINT_HTM(88, 20, 1050, 600, strFormHtml);
            var iCurLine = 80;
            for (var j = 0; j < data.length; j++) {
                if (document.getElementById(data[j]).checked) {

                    LODOP.ADD_PRINT_TEXT(iCurLine, 15, 120, 20, document.getElementById("PurchaseBillCode" + data[j]).value);
                    LODOP.ADD_PRINT_TEXT(iCurLine, 149, 100, 20, document.getElementById("DeptName" + data[j]).value);
                  //  LODOP.ADD_PRINT_TEXT(iCurLine, 349, 100, 20, document.getElementById("Material_Type" + data[j]).value);
                    LODOP.ADD_PRINT_TEXT(iCurLine, 250, 280, 20, document.getElementById("Material_Name" + data[j]).value);
                   // LODOP.ADD_PRINT_TEXT(iCurLine, 550, 100, 20, document.getElementById("Material_Specification" + data[j]).value);
                    LODOP.ADD_PRINT_TEXT(iCurLine, 550, 100, 20, document.getElementById("Material_Unit" + data[j]).value);
                    LODOP.ADD_PRINT_TEXT(iCurLine, 650, 100, 20, document.getElementById("Quantity" + data[j]).value);
                    LODOP.ADD_PRINT_TEXT(iCurLine, 750, 200, 20, document.getElementById("Memo" + data[j]).value);
                 //   LODOP.ADD_PRINT_TEXT(iCurLine, 850, 100, 20, document.getElementById("PurchaseDate" + data[j]).value);

                   
                    iCurLine = iCurLine + 25;
                    LODOP.ADD_PRINT_LINE(iCurLine-5, 14, iCurLine-5, 2100, 0, 1);
                }
            }

            
            LODOP.ADD_PRINT_TEXT(iCurLine, 50, 950, 681, "申请人：");
            LODOP.SET_PRINT_STYLEA(11, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(11, "Bold", 1);
                    //    LODOP.SET_PRINT_TEXT_STYLE(10, "宋体", 12, 1, 0, 0, 1);

                        LODOP.ADD_PRINT_TEXT(iCurLine, 140, 950, 681, document.getElementById("hidApplyName").value);
                        LODOP.SET_PRINT_STYLEA(12, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(12, "Bold", 1);

                        LODOP.ADD_PRINT_TEXT(iCurLine, 220, 950, 681, "审核人：");
                        LODOP.SET_PRINT_STYLEA(13, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(13, "Bold", 1);

                        LODOP.ADD_PRINT_TEXT(iCurLine, 300, 950, 681, document.getElementById("hidAuditName").value);
                        LODOP.SET_PRINT_STYLEA(14, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(14, "Bold", 1);

                        LODOP.ADD_PRINT_TEXT(iCurLine, 380, 950, 681, "领导审核：");
                        LODOP.SET_PRINT_STYLEA(15, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(15, "Bold", 1);

                        LODOP.ADD_PRINT_TEXT(iCurLine, 780, 950, 681, "打印日期：" + mydate.toLocaleDateString());
                        LODOP.SET_PRINT_STYLEA(16, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(16, "Bold", 1);
            //  LODOP.PREVIEW();
        };	

    </script>


    <script type="text/javascript"  src="../../Themes/Scripts/CheckActivX.js"></script>

<OBJECT  ID="LODOP" CLASSID="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" WIDTH=0 HEIGHT=0> </OBJECT> 

<script type ="text/javascript" >    CheckLodop();</script>

</head>
<body>
    <form id="form2" runat="server">
    <div class="btnbartitle">
        <div>
            物资入库管理
        </div>
    </div>
     <div class="btnbarcontetn">

    
        <div style="text-align: right">
            <uc2:LoadButton ID="LoadButton1" runat="server" />
        </div>

    </div>

   <div id="formContent">
                        发票号码：<asp:TextBox ID="txtInvoiceID" runat="server"></asp:TextBox>
                         领料部门：<asp:DropDownList ID="ddlDeptName" runat="server" 
                            onselectedindexchanged="ddlDeptName_SelectedIndexChanged"></asp:DropDownList>
      
                         发料状态：<asp:DropDownList ID="ddlStates" runat="server"></asp:DropDownList>

                            <input id="hidApplyName" type="hidden" runat="server" />
                            <input id="hidAuditName" type="hidden" runat="server" />

       <asp:Button ID="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click" />
      

</div>


    <div id="Content" class="div-body">
     <div id="Div1" class="div-body">
      <table id="table1" class="grid" >
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
                        物资类型
                    </td>
                    <td style="width: 100px; text-align: center;">
                        物资名称
                    </td>
                    <td style="width: 100px; text-align: center;">
                        物资规格
                    </td>

                     <td style="width: 50px; text-align: center;">
                        物资单位
                    </td>
                     <td style="width: 100px; text-align: center;">
                        备注
                    </td>
                    <td style="width: 20px; text-align: center;">
                        数量
                    </td>
                    <td  style="width: 80px; text-align: center;">
                        采购日期
                    </td>
                   
                </tr>
            </thead>
           

       

            <tbody>
                <asp:Repeater ID="rp_Item" runat="server" OnItemDataBound="rp_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 20px; text-align: left;">
                                <input type="checkbox" id="<%#Eval("ID")%>" value="<%#Eval("ID")%>" name="checkbox" />
                            </td>
                            <td style="width: 60px; text-align: center;">
                             <input type="text" id="PurchaseBillCode<%#Eval("ID")%>" value="<%#Eval("PurchaseBillCode")%>" name="<%#Eval("PurchaseBillCode")%>" />
                               
                            </td>
                          
                            <td style="width: 100px; text-align: center;">
                             <input type="text" id="DeptName<%#Eval("ID")%>" value="<%#Eval("DeptName")%>" name="<%#Eval("DeptName")%>" />
                               
                            </td>
                            <td style="width: 200px; text-align: center;">
                            <input type="text" id="Material_Type<%#Eval("ID")%>" value="<%#Eval("Material_Type")%>" name="<%#Eval("Material_Type")%>" />
                               
                            </td>
                            <td style="width: 100px; text-align: center;">
                              <input type="text" id="Material_Name<%#Eval("ID")%>" value="<%#Eval("Material_Name")%>" name="<%#Eval("Material_Name")%>" />
                               
                            </td>
                            <td style="width: 100px; text-align: center;">
                              <input type="text" id="Material_Specification<%#Eval("ID")%>" value="<%#Eval("Material_Specification")%>" name="<%#Eval("Material_Specification")%>" />
                               
                            </td>
                            <td style="width: 50px; text-align: center;">
                              <input type="text" id="Material_Unit<%#Eval("ID")%>" value="<%#Eval("Material_Unit")%>" name="<%#Eval("Material_Unit")%>" />
                                
                            </td>
                             <td style="width: 100px; text-align: center;">
                               <input type="text" id="Memo<%#Eval("ID")%>" value="<%#Eval("Memo")%>" name="<%#Eval("Memo")%>" />
                                
                            </td>
                             <td style="width: 100px; text-align: center;">
                               <input type="text" id="Quantity<%#Eval("ID")%>" value="<%#Eval("Quantity")%>" name="<%#Eval("Quantity")%>" />
                                
                            </td>
                           
                             <td>
                               <input type="text" id="PurchaseDate<%#Eval("ID")%>" value="<%#Eval("PurchaseDate")%>" name="<%#Eval("PurchaseDate")%>" />
                               
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
        <uc1:PageControl ID="PageControl1" runat="server" />
      
    </div>
   
    </form>
</body>
</html>

