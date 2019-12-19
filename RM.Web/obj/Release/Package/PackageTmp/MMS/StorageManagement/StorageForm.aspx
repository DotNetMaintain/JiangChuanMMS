<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StorageForm.aspx.cs" Inherits="RM.Web.MMS.StorageManagement.StorageForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物资入库表单</title>

    
    
    <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/Validator/JValidator.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/artDialog.source.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/artDialog/iframeTools.source.js" type="text/javascript"></script>
    <link href="/Themes/Scripts/TreeView/treeview.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/TreeView/treeview.pack.js" type="text/javascript"></script>
    <script src="/Themes/Scripts/FunctionJS.js" type="text/javascript"></script>
    <script type="text/javascript">

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


       

        function selectMaterial() {
            obj = new Object();
            resu = window.showModalDialog("/MMS/MMS_Material/Material_List.aspx", obj, "dialogWidth=600px;dialogHeight=460px; status:no;scroll:no;resizable:no;");
            if (resu != null) {
                                document.getElementById("Material_Type").value = resu.Material_Type;
                                document.getElementById("Material_Code").value = resu.Material_Code;
                                document.getElementById("Material_Name").value = resu.Material_Name;
                                document.getElementById("Material_CommonlyName").value = resu.Material_CommonlyName;
                                document.getElementById("Material_Specification").value = resu.Material_Specification;
                                document.getElementById("Material_Unit").value = resu.Material_Unit;
                                document.getElementById("Material_Supplier").value = resu.Material_Supplier;
                                document.getElementById("MaterialPic_Name").value = resu.MaterialPic_Name;
                
            }
        }



    </script>
</head>
<body>
    <form id="form2" runat="server">
     <input id="User_ID_Hidden" type="hidden" runat="server" />
      <input id="Material_Pic" type="hidden" runat="server" />
       <input id="Material_id" type="hidden" runat="server" />
    <div class="frmtop">
        <table id="table2" style="padding: 0px; margin: 0px; height: 100%;" cellpadding="0" cellspacing="0">
            <tr>
            <td>
           
            </td>
                <td id="menutab" style="vertical-align: bottom;">
                 
                    <div id="tab0" class="Tabsel" onclick="GetTabClick(this);TabPanel(1)">
                        基本信息</div>
                   
                </td>
            </tr>
        </table>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" class="frm">
    <tr>
            <th>
                物资类型：
            </th>
            <td>
                <input id="Material_Type" runat="server" type="text" class="txt" datacol="yes" err="物资类型"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
             <th>
                物资编码：
            </th>
            <td>
                <input id="Material_Code" runat="server" type="text" class="txt" datacol="yes" err="物资编码"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>
        <tr>
           
        </tr>
        <tr>
            <th>
                物资通用名：
            </th>
            <td>
            <input id="Material_Name" runat="server" type="text" class="txt" datacol="yes" err="物资通用名"
             checkexpession="NotNull" /> &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_FindMaterial" runat="server"
                        Text="..."  OnClientClick="selectMaterial();" style="width:10%"/>
            </td>
            <th>
                物资常用名：
            </th>
            <td>
            <input id="Material_CommonlyName" runat="server" type="text" class="txt" datacol="yes" err="物资常用名"
                    checkexpession="NotNull" style="width: 85%" />
               
            </td>
        </tr>
      
        <tr>
            <th>
                物资规格：
            </th>
            <td>
                <input id="Material_Specification" runat="server" type="text" class="txt" datacol="yes" err="物资规格"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
            <th>
                物资单位：
            </th>
            <td>
                <input id="Material_Unit" runat="server" type="text" class="txt" datacol="yes" err="物资单位"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
            
        </tr>
       
        <tr> 
         <th>
                生产厂家：
            </th>
            <td>
                <input id="Material_Supplier" runat="server" type="text" class="txt" datacol="yes" err="生产厂家"
                    checkexpession="NotNull" style="width: 85%" />
            </td> 
        <th>
                物资图片：
            </th>
            <td >  
                <asp:Image ID="MaterialPic_Name" runat="server" />
             
            </td>  
        </tr>  

         <tr> 
         <th>
                批号：
            </th>
            <td>
                <input id="Lot_id" runat="server" type="text" class="txt" datacol="yes" err="批号"
                    checkexpession="NotNull" style="width: 85%" />
            </td> 
        <th>
                有效期：
            </th>
            <td >  
                <input id="ValidDate" runat="server" type="text" class="txt" datacol="yes" err="有效期"
                    checkexpession="NotNull" style="width: 85%" />
             
            </td>  
        </tr>  
         <tr> 
         <th>
                入库量：
            </th>
            <td>
                <input id="Purchase_Num" runat="server" type="text" class="txt" datacol="yes" err="入库量"
                    checkexpession="NotNull" style="width: 85%" />
            </td> 
        <th>
                入库价格：
            </th>
            <td >  
                <input id="Purchase_Price" runat="server" type="text" class="txt" datacol="yes" err="入库价格"
                    checkexpession="NotNull" style="width: 85%" />
             
            </td>  
        </tr> 

       
    </table>
  
    <div class="frmbottom">
        <asp:LinkButton ID="Save" runat="server" class="l-btn" OnClientClick="return CheckValid();"
            OnClick="Save_Click"><span class="l-btn-left">
            <img src="/Themes/Images/disk.png" alt="" />保 存</span></asp:LinkButton>
        <a id="Close" class="l-btn" href="javascript:void(0)" onclick="OpenClose();"><span
            class="l-btn-left">
            <img src="/Themes/Images/cancel.png" alt="" />关 闭</span></a>
    </div>
    </form>
</body>
</html>

