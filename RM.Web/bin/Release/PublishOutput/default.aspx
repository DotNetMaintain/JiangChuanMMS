<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="RM.Web._default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/><title><%--<%=RealName + " (" + DepName + ")" %> - <%=comname %>--%></title>
<link href="../Themes/manage/style_d.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../Themes/manage/js/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../Themes/manage/js/themes/icon.css" />
<script type="text/javascript" src="../Themes/manage/js/jquery.js"></script>
<script type="text/javascript" src="../Themes/manage/js/jquery.easyui.min.js"></script>
<script type="text/javascript" src='../Themes/manage/js/outlook2.js'> </script>  

<script type="text/javascript">

$("document").ready(function(){
InitLeftMenu();
$('#tabs').tabs('add',{
title:'我的工作台',
iconCls: 'icon icon-home',
content:createFrame('DeskTop.aspx')
});
tabClose();
tabCloseEven();
setInterval("$('#timebox').html(new Date().toLocaleString())",500);
showOnline();
setInterval(showOnline, 1000*120);
<asp:Literal runat=server ID=message_div Visible=true>
var t = $("#notice_time").val();
showMessage();
setInterval(showMessage, t);
</asp:Literal>
});

var _menus = {
'menus': [{
	'menuid': '1',
	'icon': 'icon-img',
	'menuname': '我的快捷导航',
	'menus': [{
		'menuid': '11',
		'menuname': '我的工作台',
		'icon': 'icon-home',
		'url': 'DeskTop.aspx'
	},
	{
		'menuid': '12',
		'menuname': '所有资讯',
		'icon': 'icon-paste',
		'url': '/manage/News/News_List.aspx'
	},
	{
		'menuid': '13',
		'menuname': '收件箱(<%=mail_sjx %>)',
		'icon': 'icon-data3',
		'url': '/manage/Common/Mail_List.aspx?fid=0'
	},
	{
		'menuid': '14',
		'menuname': '我的日程',
		'icon': 'icon-calendar',
		'url': '/manage/CalendarMemo/CalendarMemo.aspx'
	},
	{
		'menuid': '15',
		'menuname': '我的流程',
		'icon': 'icon-html',
		'url': '/manage/flow/Flow_List.aspx?action=verify'
	},
	{
		'menuid': '16',
		'menuname': '我的文档',
		'icon': 'icon-folder',
		'url': '/manage/doc/Doc_List.aspx?action=mydoc'
	},
	{
		'menuid': '17',
		'menuname': '我的公文',
		'icon': 'icon-et',
		'url': '/Manage/gov/Gov_Recipient.aspx?action=verify'
	},
	<asp:Literal runat=server ID=sys_div3 Visible=false>
	{
		'menuid': '18',
		'menuname': '即时通讯',
		'icon': 'icon-phone',
		'url': '/Lesktop/Default.aspx?auto=true'
	},
	{
		'menuid': '17511',
		'menuname': '我的论坛',
		'icon': 'icon-color',
		'url': '/bbs/index.aspx'
	},		
	</asp:Literal>
	{
		'menuid': '19',
		'menuname': '个人资料',
		'icon': 'icon-user2',
		'url': '/manage/Common/User_InfoEdit.aspx'
	},
    {
		'menuid': '18001',
		'menuname': '工作汇报',
		'icon': 'icon-com',
		'url': '/Manage/Common/WorkLog_List.aspx?action=mydoc'
	},
	{
		'menuid': '19001',
		'menuname': '我的考勤',
		'icon': 'icon-log',
		'url': '/Manage/Attend/WorkAttendAdd.aspx?type=1'
	},
	{
		'menuid': '100',
		'menuname': '安全退出',
		'icon': 'icon-pc',
		'url': '/manage/LoginOut.aspx?action=menu'
	}
	]
},
{
	'menuid': '2',
	'icon': 'icon-paste',
	'menuname': '我的资讯',
	'menus': [{
		'menuid': '21',
		'menuname': '所有资讯',
		'icon': 'icon-paste',
		'url': '/manage/News/News_List.aspx'
	}
<%=news_type %>
	    ,
        {
	'menuid': '23001',
	'icon': 'icon-ppt',
	'menuname': '电子档案',
	'child': [{
		'menuid': '23002',
		'menuname': '电子档案查询',
		'icon': 'icon-template',
		'url': '/Manage/Paper/PaperList.aspx'
	        },
	        {
		'menuid': '23003',
		'menuname': '档案分类导航',
		'icon': 'icon-import',
		'url': '/Manage/Paper/TypeList_ForView.aspx'
	        }]
        }
	]
}
,
{
	'menuid': '3',
	'icon': 'icon-mail',
	'menuname': '我的邮件',
	'menus': [{
		'menuid': '31',
		'menuname': '发送新邮件',
		'icon': 'icon-addnew',
		'url': '/manage/Common/Mail_Manage.aspx'
	}
	,
	{
		'menuid': '32',
		'menuname': '收件箱(<%=mail_sjx %>)',
		'icon': 'icon-data3',
		'url': '/manage/Common/Mail_List.aspx?fid=0'
	}
	,
	{
		'menuid': '33',
		'menuname': '草稿箱(<%=mail_cgx %>)',
		'icon': 'icon-guestbook',
		'url': '/manage/Common/Mail_List.aspx?fid=1'
	}
	,
	{
		'menuid': '34',
		'menuname': '发件箱(<%=mail_fjx %>)',
		'icon': 'icon-data1',
		'url': '/manage/Common/Mail_List.aspx?fid=2'
	}
	,
	{
		'menuid': '35',
		'menuname': '垃圾箱(<%=mail_ljx %>)',
		'icon': 'icon-data6',
		'url': '/manage/Common/Mail_List.aspx?fid=3'
	}		
    ,
	{
		'menuid': '36',
		'menuname': '发送短信',
		'icon': 'icon-phone',
		'url': '/manage/sms/SMS_Send.aspx'
	}	

	]
}
,
{
	'menuid': '500001',
	'icon': 'icon-task',
	'menuname': '工作任务',
	'menus': [
    {
		'menuid': '500002',
		'menuname': '新建工作任务',
		'icon': 'icon-insert',
		'url': '/manage/Tasks/Task_Manage.aspx'
	},    
    {
		'menuid': '500003',
		'menuname': '任务分类导航',
		'icon': 'icon-import',
		'url': '/manage/Tasks/TypeList_ForView.aspx'
	}, 
    {
		'menuid': '500004',
		'menuname': '所有参与任务',
		'icon': 'icon-folder',
		'url': '/manage/Tasks/Task_List.aspx?type=all'
	}, 
    {
		'menuid': '500005',
		'menuname': '我执行的任务',
		'icon': 'icon-filesave',
		'url': '/manage/Tasks/Task_List.aspx?type=exeute'
	}, 
    {
		'menuid': '500006',
		'menuname': '我管理的任务',
		'icon': 'icon-notice',
		'url': '/manage/Tasks/Task_List.aspx?type=manage'
	}, 
    {
		'menuid': '500007',
		'menuname': '我创建的任务',
		'icon': 'icon-upload',
		'url': '/manage/Tasks/Task_List.aspx?type=create'
	}
    ]
}
,
{
	'menuid': '4',
	'icon': 'icon-html',
	'menuname': '工作流程',
	'menus': [{
		'menuid': '41',
		'menuname': '新建工作流程',
		'icon': 'icon-addnew',
		'url': '/Manage/flow/Flow_Manage.aspx'
	}
    ,
	{
		'menuid': '46',
		'menuname': '流程分类导航',
		'icon': 'icon-import',
		'url': '/Manage/flow/TypeList_ForView.aspx'
	}
	,
	{
		'menuid': '42',
		'menuname': '我的批阅',
		'icon': 'icon-html',
		'url': '/Manage/flow/Flow_List.aspx?action=verify'
	}
	,
	{
		'menuid': '43',
		'menuname': '已经批阅',
		'icon': 'icon-filesave',
		'url': '/Manage/flow/Flow_List.aspx?action=verified'
	}
	,
	{
		'menuid': '44',
		'menuname': '我的申请',
		'icon': 'icon-all',
		'url': '/Manage/flow/Flow_List.aspx?action=apply'
	}
	,
	{
		'menuid': '45',
		'menuname': '抄送呈报',
		'icon': 'icon-template',
		'url': '/Manage/flow/Flow_List.aspx?action=view'
	}
	
	]
}
,
{
	'menuid': '5',
	'icon': 'icon-calendar',
	'menuname': '我的日程',
	'menus': [{
		'menuid': '51',
		'menuname': '我的日程',
		'icon': 'icon-calendar',
		'url': '/manage/CalendarMemo/CalendarMemo.aspx'
	},
	{
		'menuid': '52',
		'menuname': '下属日程',
		'icon': 'icon-calendar1',
		'url': '/Manage/Common/MyMemo.aspx'
	},
	{
		'menuid': '53',
		'menuname': '导出日程',
		'icon': 'icon-do',
		'url': '/Manage/Common/Memo_General.aspx'
	},
    {
		'menuid': '54',
		'menuname': '工作汇报',
		'icon': 'icon-com',
		'url': '/Manage/Common/WorkLog_List.aspx?action=mydoc'
	}
	
	]
}
,
{
	'menuid': '6',
	'icon': 'icon-ett',
	'menuname': '我的公文',
	'menus': [{
		'menuid': '61',
		'menuname': '收文管理',
		'icon': 'icon-task',
		'child': [{
			'menuid': '611',
			'menuname': '公文签收',
			'icon': 'icon-et',
			'url': '/Manage/gov/Gov_Recipient.aspx?action=verify'
		},
		{
			'menuid': '612',
			'menuname': '已签收',
			'icon': 'icon-filesave',
			'url': '/Manage/gov/Gov_Recipient.aspx?action=verified'
		},
		{
			'menuid': '613',
			'menuname': '已归档',
			'icon': 'icon-download',
			'url': '/Manage/gov/Gov_Recipient.aspx?action=archived'
		}]
	},
	{
		'menuid': '62',
		'menuname': '发文管理',
		'icon': 'icon-template',
		'child': [{
			'menuid': '621',
			'menuname': '发文拟稿',
			'icon': 'icon-addnew',
			'url': '/Manage/gov/gov_Manage.aspx'
		},
		{
			'menuid': '625',
			'menuname': '公文分类导航',
			'icon': 'icon-import',
			'url': '/Manage/gov/TypeList_ForView.aspx'
		},
		{
			'menuid': '622',
			'menuname': '我的审核',
			'icon': 'icon-password',
			'url': '/Manage/gov/Gov_List.aspx?action=verify'
		},
		{
			'menuid': '623',
			'menuname': '已经审核',
			'icon': 'icon-filesave',
			'url': '/Manage/gov/Gov_List.aspx?action=verified'
		},
		{
			'menuid': '624',
			'menuname': '我的发文',
			'icon': 'icon-all',
			'url': '/Manage/gov/Gov_List.aspx?action=apply'
		}
		]
	}]
}
,
{
	'menuid': '7',
	'icon': 'icon-vss',
	'menuname': '沟通与分享',
	'menus': [{
		'menuid': '71',
		'menuname': '机构快速导航',
		'icon': 'icon-import',
		'url': '/Manage/Common/DepGuide.aspx'
	},
	{
		'menuid': '72',
		'menuname': '在线用户',
		'icon': 'icon-usergroup',
		'url': '/Manage/Common/User_OnLine.aspx'
	},
	{
		'menuid': '73',
		'menuname': '我的文档',
		'icon': 'icon-sys',
		'child': [{
			'menuid': '731',
			'menuname': '新建上传',
			'icon': 'icon-upload',
			'url': '/Manage/doc/Doc_Manage.aspx'
		},
		{
			'menuid': '732',
			'menuname': '我的文档',
			'icon': 'icon-folder',
			'url': '/Manage/doc/Doc_List.aspx?action=mydoc'
		},
		{
			'menuid': '733',
			'menuname': '同事共享',
			'icon': 'icon-sharedir',
			'url': '/Manage/doc/Doc_List.aspx?action=shared'
		},
		{
			'menuid': '734',
			'menuname': '文件分类',
			'icon': 'icon-template',
			'url': '/Manage/doc/DocType_List.aspx'
		}]
	},
	{
		'menuid': '74',
		'menuname': '我的投票',
		'icon': 'icon-app',
		'url': '/Manage/Common/Vote_List.aspx'
	},	
	<asp:Literal runat=server ID=sys_div4 Visible=false>
	{
		'menuid': '75',
		'menuname': '我的论坛',
		'icon': 'icon-color',
		'url': '/bbs/index.aspx'
	},
	</asp:Literal>
	{
		'menuid': '76',
		'menuname': '即时通讯',
		'icon': 'icon-phone',
		'url': '/Lesktop/Default.aspx?auto=true'
	}]
}
,
{
	'menuid': '8',
	'icon': 'icon-app',
	'menuname': '我的工具',
	'menus': [{
		'menuid': '80',
		'menuname': '个人考勤',
		'icon': 'icon-new2',
		'child': [{
			'menuid': '801',
			'menuname': '我的考勤',
			'icon': 'icon-log',
			'url': '/Manage/Attend/WorkAttendAdd.aspx?type=1'
		},
		{
			'menuid': '802',
			'menuname': '我的考勤记录',
			'icon': 'icon-new1',
			'url': '/Manage/Attend/WorkAttendList.aspx?type=1'
		}]		
	},
    {
		'menuid': '81',
		'menuname': '会议管理',
		'icon': 'icon-task',
		'url': '/Manage/Common/Meeting_List.aspx'
	},
	{
		'menuid': '82',
		'menuname': '记事便笺',
		'icon': 'icon-pen',
		'url': '/Manage/Common/NoteBook_List.aspx'
	},
	{
		'menuid': '83',
		'menuname': '我的通讯录',
		'icon': 'icon-pub',
		'child': [{
			'menuid': '831',
			'menuname': '员工通讯录',
			'icon': 'icon-table',
			'url': '/Manage/Common/PublicAddrBook.aspx'
		},
		{
			'menuid': '832',
			'menuname': '组织通讯录',
			'icon': 'icon-theme',
			'url': '/Manage/Common/PublicAddrBook_Dep.aspx'
		},
		{
			'menuid': '833',
			'menuname': '个人通讯录',
			'icon': 'icon-template',
			'url': '/Manage/Common/PrivateAddrBook.aspx'
		}]
	},
	{
		'menuid': '84',
		'menuname': '我的客户',
		'icon': 'icon-users',
		'child': [{
			'menuid': '841',
			'menuname': '新建客户',
			'icon': 'icon-addnew',
			'url': '/Manage/crm/CRM_Manage.aspx'
		},
		{
			'menuid': '842',
			'menuname': '我的客户列表',
			'icon': 'icon-users',
			'url': '/Manage/crm/CRM_List.aspx?action=mycrm'
		},
		{
			'menuid': '843',
			'menuname': '协同共享客户',
			'icon': 'icon-users2',
			'url': '/Manage/crm/CRM_List.aspx?action=shared'
		},
		{
			'menuid': '844',
			'menuname': '所有客户接触',
			'icon': 'icon-sitemap',
			'url': '/Manage/crm/CRM_AllContact.aspx'
		},
		{
			'menuid': '845',
			'menuname': '供应商管理',
			'icon': 'icon-data4',
			'url': '/Manage/crm/CRM_Sup_List.aspx'
		}]
	},
	{
		'menuid': '85',
		'menuname': '便捷小工具',
		'icon': 'icon-globe',
		'child': [{
			'menuid': '851',
			'menuname': '网址大全',
			'icon': 'icon-ppt',
			'url': '/DK_Css/WZ/index.htm'
		},
		{
			'menuid': '852',
			'menuname': '科学计算器',
			'icon': 'icon-part',
			'url': '/manage/utils/calar/jsq.htm'
		},
		{
			'menuid': '853',
			'menuname': '多功能万年历',
			'icon': 'icon-calendar1',
			'url': '/manage/utils/calar/Calendar.htm'
		},
		{
			'menuid': '854',
			'menuname': '历史上的今天',
			'icon': 'icon-com',
			'url': '/dk_css/history/history.aspx'
		},
		{
			'menuid': '855',
			'menuname': '网络搜索大全',
			'icon': 'icon-preview',
			'url': '/dk_css/search/index.htm'
		}]
	},
	{
		'menuid': '86',
		'menuname': '个人资料',
		'icon': 'icon-user2',
		'url': '/manage/Common/User_InfoEdit.aspx'
	},
	{
		'menuid': '87',
		'menuname': '个人登陆记录',
		'icon': 'icon-user1',
		'url': '/manage/Common/User_LoginList.aspx'
	}]
}

<%=power_menu %>

]};



function showOnline()
{
$.ajax({
type:"GET",
url:"Utils/online.ashx",
dataType: 'html',
success:function(data){
if(data!=""){
$('#online').html(data);
}
}}); 
}

function showMessage() {
var p = $("#stay_time").val();
$.ajax({
type:"GET",
url:"Utils/message.ashx",
dataType: 'html',
data: 'uid='+ $("#uid").val(), 
success:function(data){
if(data!="1"){
$.messager.show({
    title: '小秘书 信息提示',
    msg: data,
    timeout: p,
    width: 225,
    height: 135,
    showType: 'slide'
});
document.all.music.src="m.wav";
}
}});
}

</script>	
	  
</head>
<body class="easyui-layout" style="overflow-y: hidden" fit="true" scroll="no">
<bgsound src="#" id="music" loop="1" autostart="true">
<input type=hidden runat=server id=uid />
<input type=hidden runat=server id=stay_time value='180000' />
<input type=hidden runat=server id=notice_time value='1200000' />
<asp:Panel runat=server ID=dxbbs_div Visible=false>
<iframe src='/bbs/LoginForm.aspx?u=<%=Uid %>' frameborder="0" scrolling="no" width="0" height="0"></iframe>
</asp:Panel>
<input type=hidden id=sxtmp />
<noscript>
<div style=" position:absolute; z-index:100000; height:2046px;top:0px;left:0px; width:100%; background:white; text-align:center;">
    <img src="../Themes/manage/images/noscript.gif" alt='抱歉，请开启脚本支持！' />
</div></noscript>

<div id="loading-mask" style="position:absolute;top:0px; left:0px; width:100%; height:100%; background:#D2E0F2; z-index:20000">
<div id="pageloading" style="position:absolute; top:50%; left:50%; margin:-120px 0px 0px -120px; text-align:center;  border:2px solid #8DB2E3; width:200px; height:40px;  font-size:14px;padding:10px; font-weight:bold; background:#fff; color:#15428B;"> 
    <img src="../Themes/manage/images/loading.gif" align="absmiddle" /> 网络加载中,请稍候...</div>
</div>

    <div region="north" split="true" border="false" style="vertical-align:middle;overflow: hidden; height: 30px;
        background: url(../Themes/manage/images/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%;
        line-height: 20px;color: #fff; font-family: Verdana, 微软雅黑,黑体">

<span id="interface_bt1"><a href='LoginOut.aspx'><img src="../Themes/manage/images/logout.gif" onclick="return confirm('确认：您确认要安全注销，退出登录吗？')" border="0" title="安全注销，退出登陆" align="absmiddle"  /></a></span>
<span id="interface_bt2"><a onclick=addTab('个人资料','/manage/Common/User_InfoEdit.aspx','icon-user2') href='#'><img src="../Themes/manage/images/menubox_memberico.gif" border="0"  title="个人设置" align="absmiddle"  /></a></span>
<span id="interface_bt8"><a onclick=addTab('我的日程','/manage/CalendarMemo/CalendarMemo.aspx','icon-calendar') href='#'><img src="../Themes/manage/images/calendar.png" border="0" title="我的日程" align="absmiddle"  /></a></span>
<span id="interface_bt9"><a onclick=addTab('即时通讯','/Lesktop/Default.aspx?auto=true','icon-phone') href='#'><img src="../Themes/manage/images/phone.gif" border="0" title="即时通讯" align="absmiddle"  /></a></span>
<span id="interface_bt4"><a onclick=addTab('收件箱','/manage/Common/Mail_List.aspx?fid=0','icon-data3') href='#'><img src="../Themes/manage/images/mail.gif" border="0" title="我的邮件" align="absmiddle"  /></a></span>
<span id="interface_bt7"><a onclick=addTab('所有资讯','/manage/News/News_List.aspx','icon-paste') href='#'><img src="../Themes/manage/images/news.gif" border="0" title="我的资讯" align="absmiddle"  /></a></span>
<span id="interface_bt5"><a onclick=addTab('我的工作台','DeskTop.aspx','icon-home') href='#'><img src="../Themes/manage/images/ico_home.gif" border="0" title="我的工作台" align="absmiddle"  /></a></span>
<span id="interface_bt10"><a href='default.aspx'><img src="../Themes/manage/images/imageedit.gif" onclick="return confirm('确认：您确认要切换到 经典风格吗？')" border="0" title="切换经典风格" align="absmiddle"  /></a></span>

        <span style="padding-left:10px; font-size: 15px; font-weight:bold; "><img src="../Themes/manage/images/user_logo.png" width="20" height="20" align="absmiddle" /> &nbsp;<%-- <%=RealName %> (<%=DepName %>--%>)</span>
    </div>
    <div region="south" split="true" style="height: 30px; background: #D2E0F2; ">
        <div class="footer">
		<div id="timebox" style='float:right;text-align:right;width:385px;padding-right:20px;'>&nbsp;</div>
		<div id="online" style='float:left;text-align:left;width:160px;padding-left:20px;'><a onclick=addTab('在线用户','Common/User_OnLine.aspx','icon-home') href='#'>在线用户：<strong>0</strong> 人</a></div>
		</div>
    </div>
    <div region="west" split="true"  title="功能菜单" style="width:170px;" id="west">
			<div id="nav">
		<!--  导航内容 -->
			</div>

    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y:hidden">
        <div id="tabs" class="easyui-tabs"  fit="true" border="false" >
        <!--<div title='隐藏层(勿删)'></div>-->
		</div>
    </div>
    
    

	<div id="mm" class="easyui-menu" style="width:150px;">
		<div id="mm-tabupdate">刷新选项卡</div>
		<div class="menu-sep"></div>
		<div id="mm-tabclose">关闭</div>
		<div id="mm-tabcloseall">全部关闭</div>
		<div id="mm-tabcloseother">除此之外全部关闭</div>
		<div class="menu-sep"></div>
		<div id="mm-tabcloseright">当前页右侧全部关闭</div>
		<div id="mm-tabcloseleft">当前页左侧全部关闭</div>
		<div class="menu-sep"></div>
		<div id="mm-exit">退出</div>
	</div>


</body>
</html>
