<%@ Page language="c#" Codebehind="ADuserDetailModify.aspx.cs" AutoEventWireup="false" Inherits="PTIntranetSGPS.UserManagent.sss2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
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
	
	function validPass() {
		if (document.getElementById('txtPassword').value == "") {
			alert("Para actualizar insira a password que usa para entrar no posto de trabalho.");
			
		} else {
			SGPSADSearchResultList.submit();
		}
	}
	
	// captures the ENTER key pressed in the input 
	function Enter() {
//		var keyCode = evt.which ? evt.which : evt.keyCode;
//		if (keyCode == 13) {
		validPass();
		return NoEnter();
//		} 
	}

	function NoEnter() {
		return !(window.event); 
	}
	
		</script>
		<LINK rel="stylesheet" type="text/css" href="/SGPStools/css/insapo.css">
	</HEAD>
	<body bgcolor="#ffffff" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form id="SGPSADSearchResultList" method="post" runat="server">
			<!-- Header da Intranet Corporate PT -->
			<!-- WebBlock Header Intranet -->
			<%if (isError == 0 && isSuccess == 0) {%>
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablecanais">
				<tr>
					<td valign="top" width="100%">
						<table border="0">
							<tr>
								<td><asp:Label id="Label3" runat="server" Width="109px" Height="22px" ForeColor="#032B5E">Nome Completo:</asp:Label>
								</td>
								<td colspan="3"><asp:TextBox id="txtNomeCompleto" runat="server" Width="444px" ReadOnly="True"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label1" runat="server">Departamento</asp:Label>
								</td>
								<td colspan="3"><asp:TextBox id="txtDepartamento" runat="server" Width="263px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label18" runat="server" Width="56px" Height="22px" ForeColor="#032B5E">Domínio:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtDominio" runat="server" Width="444px" ReadOnly="True"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label6" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Empresa:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtEmpresa" runat="server" Width="263px" ReadOnly="True"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label8" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Telefone 1:</asp:Label>
								</td>
								<td><asp:TextBox id="txtTelefone1" runat="server" Width="148px"></asp:TextBox>
								</td>
								<td><asp:Label id="Label7" runat="server" Width="78px" Height="22px" ForeColor="#032B5E">Telefone 2:</asp:Label>
								</td>
								<td><asp:TextBox id="txtTelefone2" runat="server" Width="203px" Enabled="False"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label10" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Telemóvel:</asp:Label>
								</td>
								<td>
									<asp:TextBox id="txtTelemovel" runat="server" Width="148px" BackColor="White" Enabled="False"></asp:TextBox>
								</td>
								<td><asp:Label id="Label9" runat="server" Width="73px" Height="22px" ForeColor="#032B5E">Fax:</asp:Label>
								</td>
								<td><asp:TextBox id="txtFax" runat="server" Width="203px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label11" runat="server" Width="93px" Height="22px" ForeColor="#032B5E">Email:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtEmail" runat="server" Width="261px" ReadOnly="True"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label12" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Categoria:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtCategoria" runat="server" Width="261px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label13" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Local Trabalho:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtLocalTrabalho" runat="server" Width="444px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label id="Label14" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Morada:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtMorada" runat="server" Width="444px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label17" runat="server" Width="102px" Height="22px" ForeColor="#032B5E">Observações:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtObservacoes" runat="server" Width="444px" Rows="3" TextMode="MultiLine" Height="41px" Enabled="False"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td><asp:Label id="Label19" runat="server" Width="113px" Height="22px" ForeColor="#032B5E">Data Última Act.:</asp:Label>
								</td>
								<td><asp:TextBox id="txtDataUltimaActualizacao" runat="server" ReadOnly="True"></asp:TextBox>
								</td>
								<td colspan="2" align="right">&nbsp;
								</td>
							</tr>
							<tr>
								<td colspan="4">
									<hr>
								</td>
							</tr>
							<tr>
								<td colspan="4">
									<asp:Label id="Label2" runat="server" ForeColor="#032B5E" Height="22px" Width="240px">Para actualizar insira a password que usa para entrar no posto de trabalho:</asp:Label>
									<asp:TextBox id="txtPassword" runat="server" Width="187px" Height="24px" TextMode="Password"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td colspan="4" align="left">
									<span id="btnACT" runat="server">
										<INPUT type="button" class="button" value="Actualizar" style="WIDTH: 85px; HEIGHT: 24px" onclick="validPass()" size="20">
									</span>
									&nbsp;<INPUT type="button" class="button" value="Voltar" style="WIDTH: 85px; HEIGHT: 24px" onclick="OnBack()" size="20">
								</td>
							</tr>
							<tr>
								<td colspan="4">
									<p class="texto">Esta actualização demorará alguns minutos a ser visível.</p>
								</td>
							</tr>
							<tr>
								<td colspan="4">
									<hr>
								</td>
							</tr>
							<tr>
								<td colspan="4">
									<p class="texto">
										Para mais esclarecimentos, contacte <a href="mailto:dccc@telecom.pt">dccc@telecom.pt</a></p>
								</td>
							</tr>
						</table>
						<!-- END Search results -->
						<!-- END of Search Results List : PEDRO COLOCA O TEU CODE AQUI FIM -->
					</td>
				</tr>
			</table>
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
						Actualização de dados não é possível
						<br>
						ou
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
