<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DictionaryInput.aspx.cs" Inherits="RM.Web.RMBase.SysDictionaryManage.DictionaryInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <base   target="_self">
        <title>系统配置--数据字典录入</title>
        <style type="text/css">
            .style1 { text-align: right; }

            .style2 { text-align: left; }

            .style3 { text-align: center; }
        </style>
    </head>
    <body>
        <form id="form2" runat="server">
            <div>
                <table style="border-style: groove; border-width: thin" width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="style1">
                                        <asp:Literal ID="Literal1" runat="server" Text ="字典值代码"></asp:Literal>
                                    </td>
                                    <td class="style2">
                                        <asp:TextBox ID="txtValueCode" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ErrorMessage="字典值代码必填" ControlToValidate="txtValueCode" Display="Dynamic"></asp:RequiredFieldValidator>
                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Literal ID="Literal2" runat="server" Text="字典值名称"></asp:Literal>
                                    </td>
                                    <td class="style2">
                        
                                        <asp:TextBox ID="txtValueName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                    ErrorMessage="字典值名称必填" ControlToValidate="txtValueName" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class ="style3">
                            <table>
                                <tr>
                                    <td><asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click"/></td>
                                    <td>
                                        <asp:Button ID="btnReturn" runat="server" Text="返回" UseSubmitBehavior="false"
                                                    ValidationGroup="empty" OnClientClick=" window.close(); " /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>    
            </div>
        </form>
    </body>
</html>
