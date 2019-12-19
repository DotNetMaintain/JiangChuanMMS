<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsType_Manage.aspx.cs" Inherits="RM.Web.News.NewsType_Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/><title>资讯分类</title>
<link type="text/css" href="../Themes/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet" media="all" />
<link type="text/css" href="../Themes/manage/style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
<script type="text/javascript" src="../Themes/manage/include/common.js"></script>
<script type="text/javascript" src="../Themes/manage/js/validator.js"></script>  
</head>
<body >
    <form id="form2" runat="server" onsubmit="return Validator.Validate(this.form,3);">
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
<div id="Div1">             
    
<div id="PanelConfig">
<table class="table subsubmenu">
  <thead>
	<tr>
	  <td>
	  <a href="NewsType_Manage.aspx">添加资讯分类</a>
	  </td>
	  <td style="text-align:right">

	  </td>
	</tr>
  </thead>
</table>
<br /> 
<table class="table">
<thead>
<tr>
	<td colspan="2"><a href="#" class="helpall">[?]</a></td>
</tr>
</thead>
<tr>
	<th style="width:140px; font-weight:bold;">&nbsp;* 分类名称：<a href="#" class="help">[?]</a></th>
	<td><span class="note">请输入 分类名称 如：企业公告、规章制度、公共资源 等等.</span>
	<input runat=server name="TypeName" dataType="Require" msg="分类名称不能为空" type="text" value="" id="TypeName" style="width:504px;" /></td>
</tr>
<tr>
	<th style="width:140px; font-weight:bold;">&nbsp;* 排 序：<a href="#" class="help">[?]</a></th>
	<td><span class="note">请输入 排序.</span>
	<input runat=server name="Orders" dataType="Integer" msg="排序为整数" type="text" value="0" id="Orders" style="width:504px;" /></td>
</tr>
<tr>
	<th style="width:140px; font-weight:bold;">&nbsp;备注：<a href="#" class="help">[?]</a></th>
	<td><span class="note">请输入 备注. (选填)</span>
	<textarea runat=server style="BACKGROUND-IMAGE: url(/img/line.gif); LINE-HEIGHT: 22px; padding-left:10px; background-repeat:repeat;" name=Notes id=Notes rows=4 cols=70></textarea>
	</td>
</tr>

</table>

<br />
<div style="text-align:center;">
	<asp:Button runat=server class="button" ID=save_bt Text='保存' OnClick=Save_Btn OnClientClick="return Validator.Validate(this.form,3);" />
	&nbsp;&nbsp;<input type="reset" class="button" value='重置' />
</div>

    </div>
    
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
