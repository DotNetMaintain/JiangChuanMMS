<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QJHomeIndex.aspx.cs" Inherits="RM.Web.Frame.QJHomeIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="refresh" content="360">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>我的桌面</title>
    <link rel="stylesheet" type="text/css" href="../Themes/manage/Info_aspx_files/css.css">
    <script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
    <script type="text/javascript" src="../Themes/manage/include/common.js"></script>
    <script type="text/javascript" src="../Themes/manage/js/popup/popup.js"></script>
    <script type="text/javascript">
        function changePwd() {
            var pop = new Popup({ contentType: 1, scrollType: 'yes', isReloadOnClose: false, width: 535, height: 300 });
            pop.setContent("contentUrl", "/Manage/Common/User_PwdEdit.aspx");
            pop.setContent("title", "个人密码修改");
            pop.build();
            pop.show();
        }
        function personView() {
            var pop = new Popup({ contentType: 1, scrollType: 'yes', isReloadOnClose: false, width: 585, height: 465 });
            pop.setContent("contentUrl", '/manage/Sys/User_View.aspx?uid=<%=Uid %>');
            pop.setContent("title", "个人资料预览");
            pop.build();
            pop.show();
        }
    </script>
</head>
<body>
    <div class='divv'>
        <table cellspacing="0" cellpadding="0" width="100%" align="center" style="border-bottom: #bbdde5 1px solid;
            border-left: #bbdde5 1px solid; border-top: #bbdde5 1px solid; border-right: #bbdde5 1px solid;">
            <tbody>
                <tr class="info1">
                    <td style="padding-left: 8px" height="28" width="58%">
                        您好,<strong><%--<%=UserName+" ("+RealName+")" %>--%></strong><span style="padding-left: 8px">欢迎您!</span>
                        <span style="padding-left: 8px">[ <span style="color: #2a7aca; font-weight: bold;
                            cursor: hand;"><a href="#@" onclick="personView()">查看资料</a></span> ] [ <span style="color: #2a7aca;
                                font-weight: bold; cursor: hand;"><a href="#@" onclick="changePwd()">修改密码</a></span>
                            ] [ <span style="color: #2a7aca; font-weight: bold; cursor: hand;"><a href="Common/DepGuide.aspx">
                                机构导航</a></span> ] </span>
                    </td>
                    <td style="width: 42%;">
                        <asp:Panel runat="server" ID="TipsState" Visible="false">
                            <script type="text/javascript">
var marqueecontent = new Array();
<%=script %>
var marqueeInterval = new Array(); var marqueeId = 0; var marqueeDelay = 3000; var marqueeHeight = 17;
function initMarquee() {var str = marqueecontent[0];
document.write('<div id="marqueeBox" style="float:left;margin: 0px; font-weight:bold; line-height: 140%; text-align:center;overflow:hidden;width:98%;height:' + marqueeHeight + 'px" onmouseover="clearInterval(marqueeInterval[0])" onmouseout="marqueeInterval[0]=setInterval(\'startMarquee()\',marqueeDelay)"><div>' + str + '</div></div>'); marqueeId++;
marqueeInterval[0] = setInterval("startMarquee()", marqueeDelay);}
function startMarquee() {
var str = marqueecontent[marqueeId];marqueeId++;
if (marqueeId >= marqueecontent.length) marqueeId = 0;
if (document.getElementById("marqueeBox").childNodes.length == 1) {
var nextLine = document.createElement('DIV');nextLine.innerHTML = str;
document.getElementById("marqueeBox").appendChild(nextLine);} else {
document.getElementById("marqueeBox").childNodes[0].innerHTML = str;
document.getElementById("marqueeBox").appendChild(document.getElementById("marqueeBox").childNodes[0]);
document.getElementById("marqueeBox").scrollTop = 0;}
clearInterval(marqueeInterval[1]); marqueeInterval[1] = setInterval("scrollMarquee()", 20);}
function scrollMarquee() {document.getElementById("marqueeBox").scrollTop++;
if (document.getElementById("marqueeBox").scrollTop % marqueeHeight == (marqueeHeight - 1)) { clearInterval(marqueeInterval[1]);}} initMarquee(); 
                            </script>
                        </asp:Panel>
                    </td>
                </tr>
            </tbody>
        </table>
        <table style="margin-top: 7px;" cellspacing="0" cellpadding="0" width="100%" align="center">
            <tbody>
                <tr>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tbody>
                                <tr>
                                    <td valign="top" width="51%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="../Themes/manage/Info_aspx_files/link_3.gif" width="7"
                                                        nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000;">&nbsp;我的资讯</span>
                                                    </td>
                                                    <td height="21" background="Info_aspx_files/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="../Themes/manage/Info_aspx_files/link_3_1.gif">
                                                    </td>
                                                    <td height="100">
                                                        <table style="padding-left: 4px;" border="0" cellspacing="0" cellpadding="0" width="100%"
                                                            align="center">
                                                            <tbody>
                                                              <%--  <%=qyzx %>--%>
                                                                <tr>
                                                                    <td align="right" class="t2">
                                                                        <span style="margin-right: 15px;"><a href="/manage/News/News_List.aspx">更多工作资讯</a></span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="../Themes/manage/Info_aspx_files/a.gif" width="1" height="1">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" width="49%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="../Themes/manage/Info_aspx_files/link_3.gif" width="7"
                                                        nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000;">&nbsp;统计信息 </span>
                                                    </td>
                                                    <td height="21" background="../Themes/manage/Info_aspx_files/link_4.gif" width="7"
                                                        nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="../Themes/manage/Info_aspx_files/link_3_1.gif">
                                                    </td>
                                                    <td height="120">
                                                        <table style="padding-left: 4px;" border="0" cellspacing="0" cellpadding="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td height="24">
                                                                        <img src='../Themes/manage/img/al.gif' style='border: 0px; height: 12px;' />
                                                                        <a href="../News/News_List.aspx" title='最近三个月有 <%=news_num %> 条资讯'>『我的资讯』：(<span
                                                                            style='color: #ff0000'>
                                                                           <%-- <%=news_num %>--%>
                                                                        </span>)</a> &nbsp;&nbsp;&nbsp;&nbsp; <a href="/manage/Tasks/Task_List.aspx?type=all"
                                                                            title='需执行的任务(<%=exe_num %>) / 需管理的任务(<%--<%=man_num %>--%>)'>『工作任务』：(
                                                                            <%--<%=exe_num %>--%>/<font color='#ff0000'><%--<%=man_num%>--%></font> )</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        <img src='../Themes/manage/img/al.gif' style='border: 0px; height: 12px;' />
                                                                        <a href="/manage/Common/Mail_List.aspx?fid=0" title='未读邮件(<%=mails_num1 %>) / 所有收件(<%=mails_num2 %>)'>
                                                                            『内部邮件』：( <span style='color: #ff0000'>
                                                                                <%--<%=mails_num1 %>--%></span>/<%--<%=mails_num2 %>--%>
                                                                            )</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href='/manage/Common/Mail_Manage.aspx' title='点击撰写新邮件'>撰写新邮件</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        <img src='../Themes/manage/img/al.gif' style='border: 0px; height: 12px;' />
                                                                        <a href="/manage/flow/Flow_List.aspx?action=verify" title='待我批阅的流程(<%=flows_num1 %>) / 我申请的流程(<%=flows_num2 %>)'>
                                                                            『工作流程』：( <span style='color: #ff0000'>
                                                                               <%-- <%=flows_num1 %>--%></span>/<%--<%=flows_num2 %>--%>
                                                                            )</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href='/manage/flow/Flow_Manage.aspx' title='点击创建新的工作流程'>新建工作流程</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        <img src='../Themes/manage/img/al.gif' style='border: 0px; height: 12px;' />
                                                                        <a href="/manage/gov/Gov_List.aspx?action=verify" title='总计有 <%=shared_num %> 件需要我批阅的公文'>
                                                                            『公文批阅』：( <span style='color: #ff0000'>
                                                                               <%-- <%=shared_num %>--%></span> )</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href='/manage/gov/Gov_Recipient.aspx?action=verify'
                                                                                    title='总计有 <%=shared_num2 %> 件需要我签收的公文'> 『公文签收』：( <span style='color: #ff0000'>
                                                                                       <%-- <%=shared_num2 %>--%></span> )</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        <img src='../Themes/manage/img/al.gif' style='border: 0px; height: 12px;' />
                                                                        <a href="/manage/CalendarMemo/CalendarMemo.aspx" title='我最近2天 总计有 <%=calendar_num %> 个最近日程'>
                                                                            『最近日程』：( <span style='color: #ff0000'>
                                                                               <%-- <%=calendar_num %>--%></span> )</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href='/manage/CalendarMemo/CalendarMemo.aspx'
                                                                                    title='点击管理我的工作日程'>我的日程</a>&nbsp;&nbsp;&nbsp;<a href='/manage/Common/MyMemo.aspx'
                                                                                        title='点击查看我下属的工作日程汇报'>下属日程汇报</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        <img src='../Themes/manage/img/al.gif' style='border: 0px; height: 12px;' />
                                                                        <a href="/manage/doc/Doc_List.aspx?action=mydoc" title='总计有 <%=mydoc_num %>个文档'>『我的文档』：(
                                                                            <span style='color: #ff0000'>
                                                                                <%--<%=mydoc_num %>--%></span> )</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href='/manage/Paper/PaperList.aspx'
                                                                                    title='电子档案'>『电子档案』</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        <img src='../Themes/manage/img/al.gif' style='border: 0px; height: 12px;' />
                                                                        <a href="/bbs/index.aspx" title='今天总计有 <%=forum_num %>个论坛帖子'>『我的论坛』：( <span style='color: #ff0000'>
                                                                           <%-- <%=forum_num%>--%></span> )</a> &nbsp;&nbsp;&nbsp; <a href="../Themes/manage/Common/WorkLog_List.aspx?action=mydoc">
                                                                                工作汇报</a> &nbsp; <a href="../Themes/manage/Common/WorkLog_List.aspx?action=shared">他人汇报</a>
                                                                        &nbsp; <a href="../Themes/manage/Common/WorkLog_Manage.aspx">新建汇报</a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right" class="t2">
                                                                        <span style="margin-right: 15px;"><a href="/Manage/Common/Meeting_List.aspx">会议管理</a>&nbsp;&nbsp;
                                                                            <a href="/manage/Common/NoteBook_List.aspx">记事便笺</a>&nbsp;&nbsp; <a href="/manage/Common/PublicAddrBook.aspx">
                                                                                通讯录</a>&nbsp;&nbsp; <a href="/Manage/Common/Vote_List.aspx">我的投票</a>&nbsp;&nbsp;
                                                                            <a href="/Manage/Attend/WorkAttendAdd.aspx?type=1">我的考勤</a>&nbsp;&nbsp; </span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="../Themes/manage/Info_aspx_files/a.gif" width="1" height="1">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="8" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" width="51%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="../Themes/manage/Info_aspx_files/link_3.gif" width="7"
                                                        nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000;">&nbsp;我的邮件</span>
                                                    </td>
                                                    <td height="21" background="../Themes/manage/Info_aspx_files/link_4.gif" width="7"
                                                        nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="../Themes/manage/Info_aspx_files/link_3_1.gif">
                                                        <div style="height: 1px">
                                                        </div>
                                                    </td>
                                                    <td valign="top">
                                                        <table style="padding-left: 4px;" border="0" cellspacing="0" cellpadding="0" width="100%"
                                                            align="center">
                                                            <tbody>
                                                               <%-- <%=wdyj %>--%>
                                                                <tr>
                                                                    <td align="right" class="t2">
                                                                        <span style="margin-right: 15px;"><a href="/manage/Common/Mail_List.aspx?fid=0">收件箱</a>&nbsp;&nbsp;
                                                                            <a href="/manage/Common/Mail_List.aspx?fid=1">草稿箱</a>&nbsp;&nbsp; <a href="/manage/Common/Mail_List.aspx?fid=2">
                                                                                发件箱</a>&nbsp;&nbsp; <a href="/manage/Common/Mail_Manage.aspx">发送新邮件</a>&nbsp;&nbsp;
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="../Themes/manage/Info_aspx_files/a.gif" width="1" height="1">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" width="49%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="../Themes/manageInfo_aspx_files/link_3.gif" width="7"
                                                        nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000;">&nbsp;工作流程</span>
                                                    </td>
                                                    <td height="21" background="../Themes/manage/Info_aspx_files/link_4.gif" width="7"
                                                        nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="../Themes/manage/Info_aspx_files/link_3_1.gif">
                                                    </td>
                                                    <td valign="top">
                                                        <table style="padding-left: 4px;" border="0" cellspacing="0" cellpadding="0" width="100%"
                                                            align="center">
                                                            <tbody>
                                                              <%--  <%=wdsp %>--%>
                                                                <tr>
                                                                    <td align="right" class="t2">
                                                                        <span style="margin-right: 15px;"><a href="/manage/flow/Flow_List.aspx?action=verify">
                                                                            我的批阅</a>&nbsp;&nbsp; <a href="/manage/flow/Flow_List.aspx?action=verified">已经批阅</a>&nbsp;&nbsp;
                                                                            <a href="/manage/flow/Flow_List.aspx?action=apply">我的申请</a>&nbsp;&nbsp; <a href="/manage/flow/Flow_List.aspx?action=view">
                                                                                抄送呈报</a> </span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td background="../Themes/manage/Info_aspx_files/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="../Themes/manage/Info_aspx_files/a.gif" width="1" height="1">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</body>
</html>
