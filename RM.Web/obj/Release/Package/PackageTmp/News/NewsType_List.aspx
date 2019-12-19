<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsType_List.aspx.cs" Inherits="RM.Web.News.NewsType_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/><title>分类列表</title>
<link type="text/css" href="../Themes/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet" media="all" />
<link type="text/css" href="../Themes/manage/style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
<script type="text/javascript" src="../Themes/manage/include/common.js"></script>
<script type="text/javascript" src="../Themes/manage/js/validator.js"></script>     
</head>
<body >
    <form id="form2" runat="server">
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
	  <a href="NewsType_List.aspx">资讯分类列表</a>
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
		<th width="50">
		<input type="checkbox" name=ckb class="checkall">
		</th>
		<td width="50"><span style="margin-left:25px;">序</span></td>
			<td width="250">分类名称</td>
			<td width="115">备注</td>
			<td width="210">排序</td>
			<td width="115">管理</td>
	</tr>
</thead>
 
 <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
	<tr>
		<th width="50"><input runat=server id=chk type="checkbox" value=<%#Eval("id") %> class="checkdelete"></th>
		<td><span style="margin-left:25px;"><%# Container.ItemIndex+1 %></span></td>
		<td>
			<span style="color:#666; font-weight:bold;"><%#Eval("TypeName")%></span>
		</td>	
		<td style="color:#ff0000;">
		<%#Eval("Notes")%>
		</td>
		<td>
		<%#Eval("Orders")%>
		</td>
        <td class="manage">
 			<a href=NewsType_Manage.aspx?tid=<%#Eval("id") %> class="show">编辑</a>
			<asp:LinkButton runat=server ID=lb_del OnClick=Del_Btn class="delete" title="将同时删除该分类下所有资讯,确定删除吗?">删除</asp:LinkButton>             
        </td>
	</tr>
 </ItemTemplate></asp:Repeater>
 
</table>
    
<table class="table">
<tr>
	<td class="page">
	<span style="float:left;" id="num" runat="server"></span>
	</td>
</tr>
</table>      
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

