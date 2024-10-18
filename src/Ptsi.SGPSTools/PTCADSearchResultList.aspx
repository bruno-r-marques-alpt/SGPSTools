<%@ Page language="c#" Codebehind="PTCADSearchResultList.aspx.cs" AutoEventWireup="false" Inherits="SGPStools.Templates.PTCADSearchResultList" %>
<%@ Register TagPrefix="uc1" TagName="ADList" Src="UserControls/ADList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<%
//***************************************************************************** 
// File:			SGPSADSearchResultList.aspx
// Author:			Megamedia (NPC & SMP)
// Purpose:			Results Page of AD Search
// Type:			ASPX Web Form
// Template MCMS:	n/a
// *****************************************************************************
%>
		<LINK href="/SGPStools/css/css.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="SGPSADSearchResultList" method="post" runat="server">
			<!-- Header da Intranet Corporate PT -->
			<!-- WebBlock Header Intranet -->
			<table cellSpacing="0" cellPadding="0" width="625" border="0">
				<tr>
					<td valign="top" align="left" width="500">
						<!-- Start of Search Results List : PEDRO COLOCA O TEU CODE AQUI INICIO -->
						<!--table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td>&nbsp;&nbsp;</td>
								<td class="texto" align="middle"><asp:literal id="LTResult" Runat="server"></asp:literal></td>
								<td>&nbsp;&nbsp;</td>
							</tr>
						</table-->
						<table cellSpacing="2" cellPadding="1" width="500" align="center" border="0">
							<uc1:adlist id="ADList1" runat="server"></uc1:adlist></table>
						<table cellSpacing="0" cellPadding="0" align="center" border="0">
							<tr>
								<td>&nbsp;&nbsp;</td>
								<td class="titulos" align="middle"><asp:literal id="LTTotalRegistos" Runat="server"></asp:literal></td>
								<td>&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td class="titulos" align="middle" colSpan="3">
									<p class="linksnav">Clique no nome da pessoa para ver os detalhes.</p>
								</td>
							</tr>
							<tr>
								<td><asp:literal id="LTPrevious" Runat="server"></asp:literal></td>
								<td><asp:literal id="LTPages" Runat="server"></asp:literal></td>
								<td><asp:Literal ID="LTNext" Runat="server" /></td>
							</tr>
						</table>
						<!-- END of Search Results List : PEDRO COLOCA O TEU CODE AQUI FIM -->
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="625" border="0">
				<tr>
					<td width="100%" valign="top">
						<!-- Footer da Intranet PTSGPS -->
						<!-- Footer da Intranet Corporate PT -->
						<P></P>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
