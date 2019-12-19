<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_View_Print.aspx.cs" Inherits="RM.Web.News.News_View_Print" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/>
<title>打印预览--通知正文</title>
<script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
<script type="text/javascript" src="../Themes/manage/include/common.js"></script> 
<style type="text/css">
html { width: 760; height:100%; }
body { margin: 0; padding: 0;  font-size: 12px; font-family: tahoma,verdana; background: #ffffff; }
</style>
</head>
<body >
<TABLE width="760" height="100%" border=0 align="center" cellPadding=0 cellSpacing=0 bgcolor="#FFFFFF" style="WORD-BREAK: break-all">
	<TR>
		<TD style="WIDTH: 100%; HEIGHT: 20px;"><span runat=server id=top></span></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 100%; HEIGHT: 40px; color:#ff0000;font-size:13pt; font-weight:bolder;" align=center><span runat=server id=NewsTitle></span></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 100%; COLOR: #0066cc" align=center>接收人：<span runat=server id=Deps></span> &nbsp;&nbsp;<span runat=server id=addtime></span> &nbsp;&nbsp;发布人：<span runat=server id=Creator></span></TD>
    </TR>
	<TR>
		<TD style="WORD-BREAK: break-all; vertical-align:text-top;"><div runat=server id=Notes style="min-height:220px;_height:220px; margin-bottom:10px;margin-top:8px;margin-left:5px; margin-right:5px;border:1px solid #dddddd; padding:10px;font-size:14px;"></div></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 30px" align="right"><A style="margin-right:5px; border:1px solid #ccd1dc; background:#edfcfe; padding:0px 3px 2px;font-size:14px;" href="javascript:window.print()">&nbsp;打印本文</A>　　<A style="margin-right:5px; border:1px solid #ccd1dc; background:#edfcfe; padding:0px 3px 2px;font-size:14px; " href="javascript:window.close()">&nbsp;关闭窗口</A></TD>
	</TR>
</TABLE>

</body>
</html>
