<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Material_Form.aspx.cs" Inherits="RM.Web.MMS.MMS_Material.Material_Form" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物资设置表单</title>

    <style>
        
        .perview {width:600px;background:#fff;font-size:12px; border-collapse:collapse;}
.perview td, .perview th {padding:5px;border:1px solid #ccc;}
.perview th {background-color:#f0f0f0; height:20px;}
.perview a:link, .perview a:visited, .perview a:hover, .perview a:active {color:#00F;}
.perview table{ width:100%;border-collapse:collapse;}
        
/*file样式*/
#idPicFile {
	width:80px;height:20px;overflow:hidden;position:relative;
	background:url(http://www.cnblogs.com/images/cnblogs_com/cloudgamer/169629/o_addfile.jpg) center no-repeat;
}
#idPicFile input {
	font-size:20px;cursor:pointer;
	position:absolute;right:0;bottom:0;
	filter:alpha(opacity=0);opacity:0;
	outline:none;hide-focus:expression(this.hideFocus=true);
}
</style>
    
    <link href="/Themes/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="/Themes/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <%--<script src="<%:Url.Content("~/Themes/Scripts/jquery-1.8.2.min.js")%>" type="text/javascript"></script>
    <link href="<%:Url.Content("~/Themes/Scripts/imgareaselect/CSS/imgareaselect-default.css") %>" rel="stylesheet" />
    <script src="<%:Url.Content("~/Themes/Scripts/imgareaselect/jquery.imgareaselect.pack.js")%>" type="text/javascript"></script>--%>

<script type="text/javascript" src="/Themes/Scripts/imgareaselect/CJL.0.1.min.js"></script>
<script type="text/javascript" src="/Themes/Scripts/imgareaselect/ImagePreview.js"></script>
<script type="text/javascript" src="/Themes/Scripts/imgareaselect/jquery-1.5.js"></script>
<script type="text/javascript" src="/Themes/Scripts/imgareaselect/jquery.form.js"></script>
   


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
            if (document.getElementById("inputMaterial_Pic").value != "" && document.getElementById("Material_Pic").value == "")
            {
                alert("未点击'上传保存图片'按钮!");
               return false;
            }

            if (!confirm('确认要保存此操作吗？')) {
                return false;
            }
        }





        //清空File控件的值，并且预览处显示默认的图片
        function clearFileInput() {
            var form = document.createElement('form');
            document.body.appendChild(form);
            //记住file在旧表单中的的位置
            var file = document.getElementById("MaterialPic_ID_Hidden");
            var pos = file.nextSibling;
            form.appendChild(file);
            form.reset(); //通过reset来清空File控件的值
            document.getElementById("colspan").appendChild(file);
            document.body.removeChild(form);
            //在预览处显示图片 这是在浏览器支持滤镜的情况使用的
            document.getElementById("idImg").style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src='images/abshiu.jpg'";
            //这是是火狐里面显示默认图片的
            if (navigator.userAgent.indexOf('Firefox') >= 0) {
                $("#idImg").attr('src', 'images/abshiu.jpg');
            }
        }
        function upLoadFile() {
            var options = {
                type: "POST",
                url: '../../Ajax/Files.ashx',
                success: function showResponse(msg) {
                    //alert("上传成功!");
                    document.getElementById("Material_Pic").innerText = msg;
                    alert("上传成功!");
                    
                }
            };

            // 将options传给ajaxForm
            $('#form2').ajaxSubmit(options);
           

        }








        function Material_Type_onclick() {
            var material_type_value =document.getElementById("Material_Type").value;
            document.getElementById("material_type_hidden").value = material_type_value;

        }

        function DeleteMark_onclick() {
            var DeleteMark_value =document.getElementById("DeleteMark").value;
            document.getElementById("material_status_hidden").value = DeleteMark_value;
        }

    </script>
</head>

<body>
    <form id="form2" runat="server" >
     <input id="User_ID_Hidden" type="hidden" runat="server" />
      <input id="Material_Pic_hidden" type="hidden" runat="server" />
       <input id="material_type_hidden" type="hidden" runat="server" />
        <input id="material_status_hidden" type="hidden" runat="server" />
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
    <div class="div-body">
    <table border="0" cellpadding="0" cellspacing="0" class="frm">
    <tr>
            <th>
                物资类型：
            </th>
            <td>
                <select id="Material_Type" class="select" runat="server" style="width: 86.5%" onclick="return Material_Type_onclick()">
                </select>
            </td>
        </tr>
       
        <tr>
            <th>
                物资名称：
            </th>
            <td>
            <input id="Material_Name" runat="server" type="text" class="txt" datacol="yes" err="物资通用名"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>
        <tr>
            <th>
                物资简称：
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
        </tr>
        <tr>
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
        </tr>

        <tr>  
        <th>
                物资图片：
            </th>
            <td >  
            <input id="Material_Pic" runat="server" type="text" class="txt" datacol="yes" err="物资图片"
                    checkexpession="NotNull" style="width: 85%" />
             <input id="inputMaterial_Pic" type="file" runat="server" />
             &nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="resets" name="resets" value="还原" onclick="clearFileInput()" />
               <input type="button" name="resets" value="上传保存图片" onclick="upLoadFile()" /> 


            </td>  
        </tr> 
        <tr>  
        <th>
                是否已停用：
            </th>
            <td >  
             <select id="DeleteMark" class="select" runat="server" style="width: 86.5%" onclick="return DeleteMark_onclick()">
                </select>
             
            </td>  
        </tr>   
          <tr>  
        <th>
                安全库存：
            </th>
            <td >  
             <input id="Material_SafetyStock" runat="server" type="text" class="txt" datacol="yes" err="安全库存"
                    checkexpession="NotNull" style="width: 85%" />
             
            </td>  
        </tr>   
         <tr>  
        <th>
                物资属性：
            </th>
            <td >  
               <select id="Material_Attr01" class="select" runat="server" style="width: 86.5%" >
                </select>
                 
            </td>  
        </tr>    
        <tr>  
        <th>
                备注：
            </th>
            <td >  
               
                <input id="Material_Comm" runat="server" type="text" class="txt" datacol="yes" err="备注"
                    checkexpession="NotNull" style="width: 85%" />

            </td>  
        </tr>  
    </table>
    </div>
    
    <div class="frmbottom">
        <asp:LinkButton ID="Save" runat="server" class="l-btn" OnClientClick="return CheckValid();"
            OnClick="Save_Click" ><span class="l-btn-left">
            <img src="/Themes/Images/disk.png" alt="" />保 存</span></asp:LinkButton>
        <a id="Close" class="l-btn" href="javascript:void(0)" onclick="OpenClose();" ><span
            class="l-btn-left">
            <img src="/Themes/Images/cancel.png" alt="" />关 闭</span></a>
    </div>
    </form>
</body>
</html>

