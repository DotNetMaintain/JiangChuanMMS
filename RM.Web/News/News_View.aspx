<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_View.aspx.cs" Inherits="RM.Web.News.News_View" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/><title>资讯内容</title>
<link type="text/css" href="../Themes/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet" media="all" />
<link type="text/css" href="../Themes/manage/style_s.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
<script type="text/javascript" src="../Themes/manage/include/common.js"></script>   
    </head>
<body >
    <form id="form2" runat="server">
<div id="interface_inside">
  <div id="interface_quick">
    <div class="interface_quick_left">您现在的操作 >> 我的资讯 >> 资讯内容</div>
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
    
    <div style="background-color:#fff;">
    
<TABLE style="BORDER-COLLAPSE: collapse" id=AutoNumber4 border=0 cellSpacing=0 
borderColor=#111111 cellPadding=0 width="100%" height=1>
  <TBODY>
  <TR height=30>
    <TD  bgColor=#c0d9e6 background="" width="3%"><FONT 
      color=#003366 size=3><IMG src="../Themes/manage/images/031.gif" width=16 
      height=16></FONT></TD>
    <TD  bgColor=#c0d9e6 background="">
    <span style="font-weight:bold;" runat=server id=top></span>
      </TD></TR></TBODY></TABLE>
<TABLE style="BORDER-COLLAPSE: collapse" id=AutoNumber1 border=0 cellSpacing=0 
borderColor=#111111 cellPadding=0 width="100%" >
  <TBODY>
  <TR>
    <TD colSpan=2 style="WIDTH: 100%; HEIGHT: 3px;">
      <P align=center></P></TD></TR>
  <TR>
    <TD style="WIDTH: 100%; HEIGHT: 40px; color:#ff0000;font-size:13pt; font-weight:bolder;"
     colSpan=2 align=center>
     <span runat=server id=NewsTitle></span>
     </TD></TR>

  <TR>
    <TD style="WIDTH: 100%; COLOR: #0066cc"  height="19" colSpan="2"  align="center">
发布人：<span runat=server id=Creator></span> &nbsp;&nbsp;<span runat=server id=addtime></span>
<br>接收人：<span runat=server id=Deps></span>
    </TD>
    </TR>
    
  <TR>
    <TD style="COLOR: #0066cc;" 
    align=middle></TD>
    <TD ></TD></TR>
  <TR>
    <TD style="COLOR: #0066cc; "  
    vAlign=top align=left ></TD>
    <TD style="WORD-BREAK: break-all; vertical-align:text-top;">
    
      <div runat=server id=Notes style="min-height:220px;_height:220px; margin-bottom:10px;margin-top:8px;margin-left:35px; margin-right:30px;border:1px solid #dddddd; padding:20px 20px 20px 20px; background-color: #EEF6FC;">

      </div>
      
      </TD></TR>
      
  <TR>
    <TD style="COLOR: #0066cc"  height=24 
    vAlign=top align=left></TD>
    <TD style="WORD-BREAK: break-all">
    <div style="margin-left:50px; margin-right:20px;">
    <%=fjs %>
    </div>
    </TD></TR>      
      
  <TR>
    <TD style="COLOR: #0066cc" 
    vAlign=top align=left></TD>
    <TD style="HEIGHT: 18px; WORD-BREAK: break-all" ></TD></TR>
  <TR>
    <TD style="COLOR: #0066cc"  height=20 
    vAlign=top align=middle ></TD>
    <TD style="HEIGHT: 20px"  height=20>
    <div style="margin-left:50px; margin-right:20px;">
    <a style="margin-right:4px; border:1px solid #ccd1dc; background:#edfcfe; padding:0px 3px 2px; float:left; " href="javascript:history.back()">返回上一页</a>
    <a style="margin-left:4px; border:1px solid #ccd1dc; background:#edfcfe; padding:0px 3px 2px; float:left; " href='News_View_Print.aspx?nid=<%=Request.QueryString["nid"] %>' target=_blank>我要打印</a> 
    <a style="margin-left:8px; border:1px solid #ccd1dc; background:#edfcfe; padding:0px 3px 2px; float:left; " href='News_ReadList.aspx?nid=<%=Request.QueryString["nid"] %>' target=_blank>已阅用户一览表</a>    
    </div>
    </TD></TR>
  <TR>
    <TD  height=14 colSpan=2 
      align=left>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD></TR></TBODY></TABLE>&nbsp; 
<BR><BR>
<TABLE border=0 cellSpacing=0 cellPadding=0 width="100%">
  <TBODY>
  <TR>
    <TD style="WIDTH: 312px">
</TD>
    <TD align=right>
    <IMG src="../images/endbott.gif" width=279 
      height=25 style="margin-right:30px;">
      </TD></TR></TBODY></TABLE>

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

