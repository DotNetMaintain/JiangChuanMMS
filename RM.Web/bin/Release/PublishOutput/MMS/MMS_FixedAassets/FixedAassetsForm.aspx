<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixedAassetsForm.aspx.cs" Inherits="RM.Web.MMS.MMS_FixedAassets.FixedAassetsForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>固定资产新增设置</title>

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
     <script src="/Themes/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
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
            if (document.getElementById("FA_Img").value == "") {
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
            var file = document.getElementById("FA_Img_hidden");
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
                    document.getElementById("FA_Img").innerText = msg;
                    alert("上传成功!");

                }
            };

            // 将options传给ajaxForm
            $('#form1').ajaxSubmit(options);


        }


        function FA_Type_onclick() {
            var FA_Type_value = document.getElementById("FA_Type").value;
            document.getElementById("FA_Type_hidden").value = FA_Type_value;

        }

        function FA_Status_onclick() {
            var FA_Status_value = document.getElementById("FA_Status").value;
            document.getElementById("FA_Status_hidden").value = FA_Status_value;
        }

    </script>
</head>

<body>
    <form id="form1" runat="server" >
     <input id="User_ID_Hidden" type="hidden" runat="server" />
      <input id="FA_Img_hidden" type="hidden" runat="server" />
       <input id="FA_Type_hidden" type="hidden" runat="server" />
        <input id="FA_Status_hidden" type="hidden" runat="server" />
    <div class="frmtop">
        <table id="table1" style="padding: 0px; margin: 0px; height: 100%;" cellpadding="0" cellspacing="0">
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
                <select id="FA_Type" class="select" runat="server" style="width: 86.5%" onclick="return FA_Type_onclick()">
                </select>
            </td>
        </tr>
        <tr>
            <th>
                固定资产编码：
            </th>
            <td>
                <input id="FA_Code" runat="server" type="text" class="txt" datacol="yes" err="固定资产编码"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>
        <tr>
            <th>
                固定资产名称：
            </th>
            <td>
            <input id="FA_Name" runat="server" type="text" class="txt" datacol="yes" err="固定资产名称"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>
        <tr>
            <th>
                固定资产规格：
            </th>
            <td>
            <input id="FA_Spec" runat="server" type="text" class="txt" datacol="yes" err="固定资产规格"
                    checkexpession="NotNull" style="width: 85%" />
               
            </td>
        </tr>
        <tr>
            <th>
                物资单位：
            </th>
            <td>
                <input id="FA_Unit" runat="server" type="text" class="txt" datacol="yes" err="物资单位"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>
        <tr>
            <th>
                使用部门：
            </th>
            <td>
                <input id="FA_UseDept" runat="server" type="text" class="txt" datacol="yes" err="使用部门"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>
        <tr>
            <th>
                使用人：
            </th>
            <td>
                <input id="FA_User" runat="server" type="text" class="txt" datacol="yes" err="使用人"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>

        <tr>
            <th>
                存放地点：
            </th>
            <td>
                <input id="FA_Place" runat="server" type="text" class="txt" datacol="yes" err="存放地点"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>

        <tr>
            <th>
                存放地点编码：
            </th>
            <td>
                <input id="FA_PlaceCode" runat="server" type="text" class="txt" datacol="yes" err="存放地点编码"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>

        <tr>
            <th>
                数量：
            </th>
            <td>
                <input id="FA_Number" runat="server" type="text" class="txt" datacol="yes" err="数量"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>

        <tr>
            <th>
                金额：
            </th>
            <td>
                <input id="FA_Price" runat="server" type="text" class="txt" datacol="yes" err="金额"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>

        <tr>
            <th>
                购买日期：
            </th>
            <td>
                <input id="FA_PurDate" runat="server" type="text" class="txt" datacol="yes" err="购买日期"
                    checkexpession="NotNull" style="width: 85%" onclick="WdatePicker({el:$dp.$('FA_PurDate')})"  />
            </td>
        </tr>

        <tr>
            <th>
                使用年限：
            </th>
            <td>
                <input id="FA_UseDate" runat="server" type="text" class="txt" datacol="yes" err="使用年限"
                    checkexpession="NotNull" style="width: 85%" />
            </td>
        </tr>

        <tr>  
        <th>
                物资图片：
            </th>
            <td >  
            <input id="FA_Img" runat="server" type="text" class="txt" datacol="yes" err="固定资产图片"
                    checkexpession="NotNull" style="width: 85%" />
             <input id="inputFA_Img" type="file" runat="server" />
             &nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="resets" name="resets" value="还原" onclick="clearFileInput()" />
               <input type="button" name="resets" value="上传保存图片" onclick="upLoadFile()" /> 


            </td>  
        </tr> 
        <tr>  
        <th>
                固定资状态：
            </th>
            <td >  
             <select id="FA_Status" class="select" runat="server" style="width: 86.5%" onclick="return FA_Status_onclick()">
                </select>
             
            </td>  
        </tr>   
       
         
        <tr>  
        <th>
                备注：
            </th>
            <td >  
               
                <input id="FA_Comment" runat="server" type="text" class="txt" datacol="yes" err="备注" style="width: 85%" />

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


