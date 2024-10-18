<%@ Page language="c#" Codebehind="ADuserDetail.aspx.cs" AutoEventWireup="false" Inherits="PTIntranetSGPS.UserManagent.sss" %>
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
		<LINK rel="stylesheet" type="text/css" href="/SGPStools/css/insapo.css">
	</HEAD>
	<body bgcolor="#ffffff" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form id="SGPSADSearchResultList" method="post" runat="server">
			<!-- Header da Intranet Corporate PT -->
			<!-- WebBlock Header Intranet -->
			<%if (isError == 0 && isSuccess == 0) {%>
			<table border="0" cellpadding="0" cellspacing="0" class="tablecanais" height="449">
				<tr>
					<td valign="top" width="100%">
						<div id="Text2Copy">
							<table border="0" height="457">
								<tr>
									<td width="113"><asp:Label id="Label3" runat="server" Width="123px" Height="22px" ForeColor="#032B5E">Nome Completo:</asp:Label>
									</td>
									<td colspan="3"><asp:Label id="txtNomeCompleto" runat="server" Width="431px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label1" runat="server">Departamento:</asp:Label>
									</td>
									<td colspan="3"><asp:Label id="txtDepartamento" runat="server" Width="263px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label18" runat="server" Width="56px" Height="22px" ForeColor="#032B5E">Dom�nio:</asp:Label>
									</td>
									<td colspan="3">
										<asp:Label id="txtDominio" runat="server" Width="431px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113">
										<asp:Label id="Label6" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Empresa:</asp:Label>
									</td>
									<td colspan="3">
										<asp:Label id="txtEmpresa" runat="server" Width="263px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label8" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Telefone 1:</asp:Label>
									</td>
									<td><asp:Label id="txtTelefone1" runat="server" Width="158px" ReadOnly="True"></asp:Label>
									</td>
									<td><asp:Label id="Label7" runat="server" Width="78px" Height="22px" ForeColor="#032B5E">Telefone 2:</asp:Label>
									</td>
									<td><asp:Label id="txtTelefone2" runat="server" Width="182px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label10" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Telem�vel:</asp:Label>
									</td>
									<td><asp:Label id="txtTelemovel" runat="server" Width="158px" ReadOnly="True"></asp:Label>
									</td>
									<td><asp:Label id="Label9" runat="server" Width="73px" Height="22px" ForeColor="#032B5E">Fax:</asp:Label>
									</td>
									<td><asp:Label id="txtFax" runat="server" Width="181px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label11" runat="server" Width="93px" Height="22px" ForeColor="#032B5E">Email:</asp:Label>
									</td>
									<td colspan="3">
										<asp:Label id="txtEmail" runat="server" Width="261px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113">
										<asp:Label id="Label12" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Categoria:</asp:Label>
									</td>
									<td colspan="3">
										<asp:Label id="txtCategoria" runat="server" Width="261px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label13" runat="server" Width="108px" Height="22px" ForeColor="#032B5E">Local Trabalho:</asp:Label>
									</td>
									<td colspan="3">
										<asp:Label id="txtLocalTrabalho" runat="server" Width="431px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113">
										<asp:Label id="Label14" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Morada:</asp:Label>
									</td>
									<td colspan="3">
										<asp:Label id="txtMorada" runat="server" Width="430px" ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label17" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Observa��es:</asp:Label>
									</td>
									<td colspan="3">
										<asp:Label id="txtObservacoes" runat="server" Width="431px" Rows="3" TextMode="MultiLine" Height="41px"
											ReadOnly="True"></asp:Label>
									</td>
								</tr>
								<tr>
									<td width="113"><asp:Label id="Label19" runat="server" Width="120px" Height="22px" ForeColor="#032B5E">Data �ltima Act.:</asp:Label>
									</td>
									<td><asp:Label id="txtDataUltimaActualizacao" runat="server" ReadOnly="True"></asp:Label>
									</td>
									<td colspan="2" align="right">&nbsp;
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
							<tr>
								<td align="left">
									<table width="100%" height="100%" border="0">
										<tr>
											<td width="90" align="left" valign="middle"><INPUT type="button" class="button" value="Editar" style="WIDTH: 85px; HEIGHT: 24px" onclick="window.location='ADuserDetailModify.aspx?<%=Request.QueryString.ToString()%>'" size="20"></td>
											<td align="left" valign="middle"><span class="texto">Clique aqui para actualizar os seus dados.</span></td>
										</tr>
									</table>
								</td>
								<td align="center" valign="middle" rowspan="2">
									<INPUT id="btnVoltar" runat="server" type="button" class="button" value="Voltar" style="WIDTH: 85px; HEIGHT: 24px" onclick="OnBack()"
										size="20">
								</td>
							</tr>
							<tr>
								<td valign="middle">
									<table width="100%" height="100%" border="0">
										<tr>
											<td width="90" align="left" valign="middle"><INPUT type="button" class="button" value="Copiar" style="WIDTH: 85px; HEIGHT: 24px" onclick="CopyData2Clipboard()"
													size="20"></td>
											<td align="left" valign="middle"><span class="texto"> Clique aqui para copiar estas informa��es.</span></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
				</tr>
			</table>
			<!-- END Search results -->
			<!-- END of Search Results List : PEDRO COLOCA O TEU CODE AQUI FIM --> 
			</TD></TR></TABLE>
			<%} else if (isError == 1) {%>
			<table width="625" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
				<tr>
					<td valign="top" width="625" align="center">
						<br>
						N�o � poss�vel consultar os detalhes deste utilizador.<br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="center">
						<INPUT type="button" class="button" value="Voltar" style="WIDTH: 85px" onclick="OnBack()"
							size="20">
					</td>
				</tr>
			</table>
			<%} else if (isError == 2) {%>
			<table width="625" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
				<tr>
					<td valign="top" width="625" align="center">
						<br>
						Password invalida.<br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="center">
						<INPUT type="button" value="Voltar" class="button" style="WIDTH: 85px" onclick="OnBack()"
							size="20">
					</td>
				</tr>
			</table>
			<%} else if (isSuccess == 1) {%>
			<table width="625" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
				<tr>
					<td valign="top" width="625" align="center">
						<br>
						O pedido de actualiza��o dos seus dados foi lan�ado com sucesso no servidor.<br>
						Ser� efectuado dentro de alguns minutos.<br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="center">
						<INPUT type="button" class="button" value="Voltar" style="WIDTH: 85px" onclick="OnBack()"
							size="20">
					</td>
				</tr>
			</table>
			<%}%>
		</form>
	</body>
</HTML>
