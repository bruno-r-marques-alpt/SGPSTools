<%@ Page language="c#" Codebehind="ADPTCUserDetail.aspx.cs" AutoEventWireup="false" Inherits="SGPStools.ADPTCDetail" %>
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
		<script language="javascript">
	function OnBack() {
		window.history.back();
	}
	
	
	function CopyData2Clipboard() {
		if( confirm("Copiar o texto ?\n\n" + Text2Copy.innerText) )
			window.clipboardData.setData("Text",Text2Copy.innerText);
		
	}
		</script>
		<LINK rel="stylesheet" type="text/css" href="/SGPStools/css/css.css">
	</HEAD>
	<body bgcolor="#ffffff" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form id="ADPTCDetail" method="post" runat="server">
			<!-- Header da Intranet Corporate PT -->
			<!-- WebBlock Header Intranet -->
			<%if (isError == 0 && isSuccess == 0) {%>
			<table border="0" cellpadding="0" cellspacing="0" class="tablecanais" height="449">
				<tr>
					<td valign="top" width="100%">
						<div id="Text2Copy">
							<table border="0" height="457">
								<tr>
									<td width="113" valign="center" class="text"><asp:Label id="Label3" runat="server" Width="123px" Height="13px" CssClass="text">Nome Completo:</asp:Label>
									</td>
									<td colspan="3" valign="center" class="text"><asp:Label id="txtNomeCompleto" runat="server" Width="431px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113" valign="center" class="text"><asp:Label id="Label1" runat="server" CssClass="text" Height="15px">Departamento:</asp:Label>
									</td>
									<td colspan="3" valign="center" class="text"><asp:Label id="txtDepartamento" runat="server" Width="263px" ReadOnly="True" Height="15px"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113" valign="center" class="text"><asp:Label id="Label18" runat="server" Width="56px" Height="13px" CssClass="text">Domínio:</asp:Label>
									</td>
									<td colspan="3" valign="center" class="text">
										<asp:Label id="txtDominio" runat="server" Width="431px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113" valign="center" class="text">
										<asp:Label id="Label6" runat="server" Width="102px" Height="12px">Empresa:</asp:Label>
									</td>
									<td colspan="3" valign="center" class="text">
										<asp:Label id="txtEmpresa" runat="server" Width="263px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113" valign="center" class="text" height="27"><asp:Label id="Label8" runat="server" Width="102px" Height="16px">Telefone 1:</asp:Label>
									</td>
									<td valign="center" class="text" height="27"><asp:Label id="txtTelefone1" runat="server" Width="158px" ReadOnly="True"></asp:Label>
									</td>
									<td valign="center" class="text" height="27"><asp:Label id="Label7" runat="server" Width="78px" Height="13px" CssClass="text">Telefone 2:</asp:Label>
									</td>
									<td valign="center" class="text" height="27"><asp:Label id="txtTelefone2" runat="server" Width="182px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113" valign="center" class="text"><asp:Label id="Label10" runat="server" Width="102px" Height="15px">Telemóvel:</asp:Label>
									</td>
									<td class="text" valign="center"><asp:Label id="txtTelemovel" runat="server" Width="158px" ReadOnly="True"></asp:Label>
									</td>
									<td class="text" valign="center"><asp:Label id="Label9" runat="server" Width="73px" Height="12px" CssClass="text">Fax:</asp:Label>
									</td>
									<td class="text" valign="center"><asp:Label id="txtFax" runat="server" Width="181px" ReadOnly="True" CssClass="text"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113" valign="center" class="text"><asp:Label id="Label11" runat="server" Width="93px" Height="12px">Email:</asp:Label>
									</td>
									<td valign="center" colspan="3" class="text">
										<asp:Label id="txtEmail" runat="server" Width="261px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td valign="center" width="113" class="text">
										<asp:Label id="Label12" runat="server" Width="102px" Height="14px">Categoria:</asp:Label>
									</td>
									<td valign="center" colspan="3" class="text">
										<asp:Label id="txtCategoria" runat="server" Width="261px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td valign="center" width="113" class="text"><asp:Label id="Label13" runat="server" Width="108px" Height="14px">Local Trabalho:</asp:Label>
									</td>
									<td valign="center" class="text" colspan="3">
										<asp:Label id="txtLocalTrabalho" runat="server" Width="431px" ReadOnly="True" CssClass="text">txtLocalTrabalho</asp:Label>
									</td>
								</tr>
								<tr>
									<td valign="center" width="113">
										<asp:Label id="Label14" runat="server" Width="102px" Height="12px" CssClass="text">Morada:</asp:Label>
									</td>
									<td valign="center" class="text" colspan="3">
										<asp:Label id="txtMorada" runat="server" Width="430px" ReadOnly="True" CssClass="text"></asp:Label>
									</td>
								</tr>
								<tr>
									<td valign="center" width="113"><asp:Label id="Label17" runat="server" Width="102px" Height="40px" CssClass="text">Observações:</asp:Label>
									</td>
									<td valign="center" colspan="3">
										<asp:Label id="txtObservacoes" runat="server" Width="431px" Rows="3" TextMode="MultiLine" Height="41px" ReadOnly="True" CssClass="text"></asp:Label>
									</td>
								</tr>
								<tr>
									<td valign="center" width="113"><asp:Label id="Label19" runat="server" Width="120px" Height="12px" CssClass="text">Data Última Act.:</asp:Label>
									</td>
									<td valign="center"><asp:Label id="txtDataUltimaActualizacao" runat="server" ReadOnly="True" CssClass="text"></asp:Label>
									</td>
									<td valign="center" colspan="2" align="right">&nbsp;
									</td>
								</tr>
								<tr>
									<td colspan="4">
										<hr>
									</td>
								</tr>
							</table>
						</div>
						<table border="0" width="100%">
							<!--tr>
								<td align="left">
									<table width="100%" height="100%" border="0">
										<tr>
											<td width="90" align="left" valign="center"><INPUT type="button" class="button" value="Editar" style="WIDTH: 85px; HEIGHT: 24px" onclick="window.location='ADuserDetailModify.aspx?<%=Request.QueryString.ToString()%>'" size="20"></td>
											<td align="left" valign="center"><span class="texto">Clique aqui para actualizar os seus dados.</span></td>
										</tr>
									</table>
								</td>
								<td align="middle" valign="center" rowspan="2">
									<INPUT type="button" class="button" value="Voltar" style="WIDTH: 85px; HEIGHT: 24px" onclick="OnBack()" size="20">
								</td>
							</tr-->
							<tr>
								<td valign="center">
									<table width="100%" height="100%" border="0">
										<tr>
											<td width="90" align="left" valign="center"><INPUT type="button" class="button" value="Copiar" style="WIDTH: 85px; HEIGHT: 24px" onclick="CopyData2Clipboard()" size="20"></td>
											<td align="left" valign="center"><span class="texto"> Clique aqui para copiar estas informações.</span></td>
										</tr>
									</table>
								</td>
								<td align="middle" valign="center" rowspan="2">
									<INPUT type="button" class="button" value="Voltar" style="WIDTH: 85px; HEIGHT: 24px" onclick="OnBack()" size="20">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!-- END Search results -->
			<!-- END of Search Results List : PEDRO COLOCA O TEU CODE AQUI FIM -->
			</TD></TR></TABLE>
			<%} else if (isError == 1) {%>
			<table width="625" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
				<tr>
					<td valign="top" width="625" align="middle">
						<br>
						Não é possível consultar os detalhes deste utilizador.<br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="middle">
						<INPUT type="button" class="button" value="Voltar" style="WIDTH: 85px" onclick="OnBack()" size="20">
					</td>
				</tr>
			</table>
			<%} else if (isError == 2) {%>
			<table width="625" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
				<tr>
					<td valign="top" width="625" align="middle">
						<br>
						Password invalida.<br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="middle">
						<INPUT type="button" value="Voltar" class="button" style="WIDTH: 85px" onclick="OnBack()" size="20">
					</td>
				</tr>
			</table>
			<%} else if (isSuccess == 1) {%>
			<table width="625" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
				<tr>
					<td valign="top" width="625" align="middle">
						<br>
						O pedido de actualização dos seus dados foi lançado com sucesso no servidor.<br>
						Será efectuado dentro de alguns minutos.<br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="middle">
						<INPUT type="button" class="button" value="Voltar" style="WIDTH: 85px" onclick="OnBack()" size="20">
					</td>
				</tr>
			</table>
			<%}%>
		</form>
	</body>
</HTML>
