<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_ReadList.aspx.cs" Inherits="RM.Web.News.News_ReadList" %>
<%@ Register Src="~/UserControl/PageControl.ascx" TagName="Page" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/><title>已阅用户列表</title>
<link type="text/css" href="../Themes/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet" media="all" />
<link type="text/css" href="../Themes/manage/style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
<script type="text/javascript" src="../Themes/manage/include/common.js"></script>    
</head>
<body >
    <form id="form2" runat="server">
<div id="interface_inside">
  <div id="interface_quick">
    <div class="interface_quick_left">您现在的操作 >> 我的资讯 >> 已阅用户列表</div>
    <div class="interface_quick_right">
     <div class="interface_quick_right">
    <a href="javascript:void(0)" onclick="javascript:window.location.replace(window.location.href);"><img style="vertical-align:middle;" src="../Themes/manage/images/reload.png" /><strong>刷新</strong></a>
    &nbsp; &nbsp;
    <a href="javascript:history.back();"><img style="vertical-align:middle;" src="../Themes/manage/images/ico_up1.gif" /><strong>后退</strong></a>  
    </div>
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
<%=news_menu %>
	  </td>
	  <td style="text-align:right">
      <asp:Panel ID="panLogin" runat="server" DefaultButton="search_bt">
	  <input type="text" name="keyword"  maxlength=10  id="keyword" />
	  <asp:Button runat=server ID=search_bt CssClass="textbutton" OnClick=Search_Btn Text=搜索 /></asp:Panel>	
	  </td>
	</tr>
  </thead>
</table>
<br />            
  
    
<div id="PanelDefault">
	
<table class="table">
<thead>
	<tr>
		<th width="10">
		</th>
		<td width="40"><span style="margin-left:15px;">序</span></td>
			<td width="160">阅读人员</td>
			<td width="210">所属部门</td>
			<td width="135">状态</td>
			<td width="200">首次查阅时间</td>
	</tr>
</thead>
 
<asp:Repeater runat=server ID=rpt ><ItemTemplate>
	<tr>
		<th></th>
		<td><span style="margin-left:10px;"><%# Container.ItemIndex+1 %></span></td>
		<td style="font-weight:bold;">
		<%# Eval("RealName") %>
		</td>	
		<td>
		<%# Eval("DepName")%>
		</td>
		<td>
		<%# Convert.ToBoolean(Eval("et3")) ? "<span style='color:#006600;font-weight:bold;'>√ 已读</span>" : "× 未读"%>
		</td>
	    <td>
            <%# Eval("et4") %>
		</td>	
	</tr>
 </ItemTemplate></asp:Repeater>
 
</table>

<table class="table">
<tr>
	<td class="page">
	<span style="float:left;" id=num runat=server></span>
	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<uc1:Page ID="Page2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
</table>

<br />
 
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

