<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_Manage.aspx.cs" Inherits="RM.Web.News.News_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/><title>资讯编辑</title>
<script type="text/javascript" src="../Themes/manage/js/validator.js"></script>
<link type="text/css" href="../Themes/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet" media="all" />
<link type="text/css" href="../Themes/manage/style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
<script type="text/javascript" src="../Themes/manage/include/common.js"></script>   
    
    <link rel="stylesheet" href="../Themes/manage/KindEditor4/themes/default/default.css" />
	<link rel="stylesheet" href="../Themes/manage/KindEditor4/plugins/code/prettify.css" />
	<script type="text/javascript" charset="utf-8" src="../Themes/manage/KindEditor4/kindeditor.js"></script>
	<script type="text/javascript" charset="utf-8" src="../Themes/manage/KindEditor4/lang/zh_CN.js"></script>
	<script type="text/javascript" charset="utf-8" src="../Themes/manage/KindEditor4/plugins/code/prettify.js"></script>
    
	<script type="text/javascript">
	    KindEditor.ready(function (K) {
	        var editor1 = K.create('#Bodys', {
	            cssPath: '../Themes/manage/KindEditor4/plugins/code/prettify.css',
	            uploadJson: '../Themes/manage/KindEditor4/upload_json.aspx',
	            fileManagerJson: '../Themes/manage/KindEditor4/file_manager_json.aspx'
	        });
	        prettyPrint();
	    });
</script>

    <script type="text/javascript" src="../Themes/manage/js/popup/popup.js"></script>
    <script type="text/javascript">
        function fAddAttach() {
            var gAttchHTML = '<div><input type="file" name="attachfile"><input type="button" name="Submit" value=" 删除 " id="btnDeleteReadAttach" /></div><span></span>';
            var Attach = document.getElementById("dvReadAttach");
            var spnList = Attach.getElementsByTagName("SPAN");
            var spn = document.createElement("DIV");
            spn.innerHTML = gAttchHTML;
            spn.childNodes[0].name = "attachfile[]" + spnList.length;
            Attach.appendChild(spn); //增加gAttchHTML
            fGetObjInputById(spn, "btnDeleteReadAttach").onclick = function () { fDeleteAttach(this); };
            document.getElementById("aAddAttach").innerHTML = "继续添加附件";
            Attach.style.display = "";
        }

        function fGetObjInputById(obj, id) {
            var inputList = obj.getElementsByTagName("INPUT");
            for (var i = 0; i < inputList.length; i++) {
                if (inputList[i].id == id) {
                    return inputList[i];
                }
            }
            return null;
        }

        function fDeleteAttach(obj) {
            obj.parentNode.parentNode.parentNode.removeChild(obj.parentNode.parentNode);
            var Attach = document.getElementById("dvReadAttach");
            var spnList = Attach.getElementsByTagName("SPAN");
            if (spnList.length == 0) {
                document.getElementById("aAddAttach").innerHTML = "添加附件";
                Attach.style.display = "none";
            }
            else {
                document.getElementById("aAddAttach").innerHTML = "继续添加附件";
            }
        }

        function ShowIframe() {
            var users = document.getElementById("userlist_dep");
            var pop = new Popup({ contentType: 1, scrollType: 'no', isReloadOnClose: false, width: 468, height: 395 });
            if (users.value == "") {
                pop.setContent("contentUrl", "../Themes/manage/Utils/SelectDep.aspx");
            }
            else {
                var ids = "";
                var objs = users.value.split(',');
                for (var i = 0; i < objs.length; i++) {
                    if (objs[i].replace(/^\s*(.*?)[\s\n]*$/g, '$1') != "") {
                        ids += objs[i].split('#')[1] + ";";
                    }
                }

                pop.setContent("contentUrl", "/Manage/Utils/SelectDep.aspx?v=" + ids);
            }
            pop.setContent("title", "发布资讯 - 部门选择");
            pop.build();
            pop.show();
        }    
</script>     
    
    
</head>
<body >
    <form id="form2" runat="server" onsubmit="return Validator.Validate(this.form,1);" enctype="multipart/form-data">
<div id="interface_inside">
  <div id="interface_quick">
    <div class="interface_quick_left">您现在的操作 >> 系统管理 >> 资讯管理</div>
    <div class="interface_quick_right">
    <a href="javascript:void(0)" onclick="javascript:window.location.replace(window.location.href);"><img style="vertical-align:middle;" src="../Themes/manage/images/reload.png" /><strong>刷新</strong></a>
    &nbsp; &nbsp;
    <a href="javascript:history.back();"><img style="vertical-align:middle;" src="../Themes/manage/images/ico_up1.gif" /><strong>后退</strong></a>  
    </div>
    <div class="clearboth"></div>
  </div>
  <div id="interface_main">
    <div id="tabs_config" class="tabsbox">
      

      <div class="clearboth"></div>
      
      
      <!-- 模块 -->

        <table width="100%" border="0" cellspacing="0" cellpadding="0" >
          <tr>
            <td>
            
 <div id="config_basic1" class="tabs_wrapper">
<div class="tabs_main" align="left">  
<div id="PanelConfig">

<table class="table subsubmenu">
  <thead>
	<tr>
	  <td>
<a href="/manage/news/News_Manage.aspx" >发布资讯</a>
	  </td>
	  <td style="text-align:right">

	  </td>
	</tr>
  </thead>
</table>
<br />            

<table class="table1">
<thead>
<tr>
	<td><a href="#" class="helpall">[?]</a></td>
	<td>
		<asp:Button runat="server" class="button" ID="Button1" Text='保存' OnClick="Save_Btn" OnClientClick="return Validator.Validate(this.form,1);" />
	&nbsp;&nbsp;<input type="reset" class="button" value='重置' />
	</td>
</tr>
</thead>
<tr>
	<th style="width:140px; font-weight:bold;">&nbsp;* 标 题：<a href="#" class="help">[?]</a></th>
	<td><span class="note">请输入 标题.</span>
	<input runat="server" name="NewsTitle" dataType="Require" msg="标题不能为空" type="text" value="" id="NewsTitle" size=97 /></td>
</tr>
<tr>
	<th style="width:140px; font-weight:bold;">&nbsp;资讯分类：<a href="#" class="help">[?]</a></th>
	<td><span class="note">请选择 资讯分类.</span>
    <asp:DropDownList ID="TypeID" runat="server" style="width:610px;"></asp:DropDownList>
	</td>
</tr>
<tr>
	<th style="width:140px; font-weight:bold;">&nbsp;发布范围：<a href="#" class="help">[?]</a></th>
	<td><span class="note">请选择 发布范围</span>
	<select runat="server" id="sel" style="width:610px;" onchange='javascript:if(this.value=="1"){document.getElementById("tr").style.display=""}else{document.getElementById("tr").style.display="none";document.getElementById("userlist_dep").value="";document.getElementById("namelist_dep").value="";} '>
	<option value="0" selected>发送全体人员</option>
	<option value="1">我要自行选择接收部门</option>
	</select>
	</td>
</tr>
<tr id="tr" runat="server" style="display:none;">
	<th style="width:140px; font-weight:bold;">&nbsp;接收部门：<a href="#" class="help">[?]</a></th>
	<td><span class="note">请选择 接收人：点击右侧 [接收部门] 按钮, 发送全体人员请留空.</span>
	<input runat="server" type="hidden" id="userlist_dep" name="userlist_dep" value="" />
	<input runat="server" size=71 name="namelist_dep" type="text" id="namelist_dep" readonly="readonly"  />	
	<input type="button" value="接收部门" class="textbutton" onclick="ShowIframe()" />
	</td>
</tr>
<tr>
	<th style="width:140px; font-weight:bold;">* 正文&nbsp;<a href="#" class="help">[?]</a></th>
	<td>
	<span class="note">请输入 正文内容. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Shift+Enter 换行, Enter 段落</span>
	<textarea id="Bodys"  style="width:80%;height:300px;visibility:hidden;" runat="server" enableviewstate="false"></textarea>
	</td>
</tr>
<tr >
	<th style="width:140px; font-weight:bold;">桌面显示&nbsp;<a href="#" class="help">[?]</a></th>
	<td>
	<span class="note">该条资讯是否 在我的工作台桌面显示</span>
          <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="ComID">
          <asp:ListItem Selected="True" Text='在工作台桌面显示' Value='0'></asp:ListItem>
          <asp:ListItem Text='不在工作台桌面显示' Value='1'></asp:ListItem>
          </asp:RadioButtonList> (提示：工作台显示 最新的7条资讯信息，较旧的资讯将被新的资讯 挤出工作台) 
	</td>
</tr>
<tr >
	<th style="width:140px; font-weight:bold;">手机提醒&nbsp;<a href="#" class="help">[?]</a></th>
	<td>
	<span class="note">该条资讯是否 发送手机短信通知资讯接收人</span>
    <asp:CheckBox runat="server" ID="IsSms" Checked="false" /> (是否发送手机短信通知资讯接收人)
	</td>
</tr>
<tr >
	<th style="width:140px; font-weight:bold;">附件&nbsp;<a href="#" class="help">[?]</a></th>
	<td>
	<span class="note">可批量添加附件,附件总大小上限 50M. </span>
		&nbsp;&nbsp;<span style="font-weight:bold;"><a href="javascript:fAddAttach();" id="aAddAttach">添加附件</a></span>
		<br />
		<div runat="server" id="dvReadAttach" style="float:left;"></div>
	</td>
</tr>

<asp:Panel ID="Attachword" runat="server" Visible="false">
<tr >
	<th style="width:140px; font-weight:bold;">已添加附件&nbsp;<a href="#" class="help">[?]</a></th>
	<td>
	<span class="note">勾选复选框 编辑已添加附件 </span>
		&nbsp;&nbsp;<div style="text-align:left; float:left;">
		<asp:Repeater runat="server" ID="rpt"><ItemTemplate>
		<input runat="server" id="chk" type="checkbox" checked="checked" value=<%#Eval("Tmp1") %> />
		<%#Eval("Tmp2") %> &nbsp;&nbsp;&nbsp;&nbsp;<a href='../Themes/manage/Utils/Download.aspx?destFileName=<%# Server.UrlEncode(Eval("Tmp3")+"") %>' ><img src='/img/mail_attachment.gif' />下载附件</a>
		
		<br>
	    </ItemTemplate></asp:Repeater></div>
	</td>
</tr>
</asp:Panel>

<tr>
	<th>&nbsp;</th>
	<td>
	<asp:Button runat="server" class="button" ID="save_bt" Text='保存' OnClick="Save_Btn" OnClientClick="return Validator.Validate(this.form,1);" />
	&nbsp;&nbsp;<input type="reset" class="button" value='重置' />
</td>
</tr>

</table>

                  </div></div>
            </div></td>
          </tr>
        </table>

      <!-- 模块 -->

    </div>
  </div>
</div>

    </form>
</body>
</html>

