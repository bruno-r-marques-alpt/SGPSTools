<%@ Page language="c#" Codebehind="NFADSearchResultList.aspx.cs" AutoEventWireup="false" Inherits="SGPStools.Templates.NFADSearchResultList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Resultados da Pesquisa de Contactos</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<LINK href="/SGPStools/css/IntranetNF.css" type=text/css rel=stylesheet >
  </HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="NFConteudoIframeQueryString" method="post" runat="server">
			<!-- Header Intranet --><br>
			<table cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
				<tr>

					<td vAlign="top" align="left" width="100%" >
						<!-- BEGIN Search results -->
						<table border="0" cellpadding="0" cellspacing="0" align="center">
							<tr>
								<td>&nbsp;&nbsp;</td>
								<td align="middle"><asp:Literal ID="LTResult" Runat="server" /></td>
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;</td>
								<td align="middle"><asp:Literal ID="LTCriteria" Runat="server" /></td>
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;</td>
								<td align="middle"><asp:Literal ID="LTTotalRegistos" Runat="server" /></td>
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td colspan="3">&nbsp;</td>
							</tr>
						</table>
						<table border="0" width="100%" cellpadding="0" cellspacing="0">
							<asp:Repeater ID="ListADListResults" Runat="server">
								<ItemTemplate>
									<tr>
										<td width="5%">&nbsp;</td>
										<td width="5%" valign="top">&nbsp;</td>
										<td width="85%" valign="top">
											<table border="0" width="100%" cellpadding="0" cellspacing="0">
												<tr>
													<td width="150px" class="text"><b>Nome:</b></td>
										
													<td class="text">
													<%# DataBinder.Eval(Container.DataItem, "Name")%>
													</td>
												</tr>
												<tr>
													<td class="text"><b>Departamento:</b></td>
													<td class="text"><%# DataBinder.Eval(Container.DataItem, "Department")%></td>
												</tr>
												<tr>
													<td class="text"><b>Email:</b></td>
													<td class="text"><%# DataBinder.Eval(Container.DataItem, "Email")%></td>
												</tr>
												<tr>
													<td class="text"><b>Contacto:</b></td>
													<td class="text"><%# DataBinder.Eval(Container.DataItem, "Contact")%></td>
												</tr>
												
											</table>
										</td>
										<td width="5%">&nbsp;</td>
									</tr>
									<tr>
										<td colspan="4"><hr noshade width="80%" size="1">
										</td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</table>
						<table border="0" cellpadding="0" cellspacing="0" align="center">
							<tr>
								<td><asp:Literal ID="LTPrevious" Runat="server" /></td>
								<td>&nbsp;&nbsp;</td>
								<td><asp:Literal ID="LTPages" Runat="server" /></td>
								<td>&nbsp;&nbsp;</td>
								<td><asp:Literal ID="LTNext" Runat="server" /></td>
							</tr>
						</table>
						<!-- END Search results -->
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2"><img height=40 src="<%# ImagePath %>dot.gif" width=1></td>
				</tr>
<!--				<tr>
					<td vAlign="top" bgColor="#1062aa" colSpan="2"><img height=16 src="<%# ImagePath %>dot.gif" width=1></td>
				</tr>
-->
<!--				<tr>
					<td colspan="2" valign="top" bgcolor="#d4e400"><img height=2 src="<%# ImagePath %>dot.gif" width=1></td>
				</tr>
-->
			</table>
		</form>
	</body>
</HTML>
