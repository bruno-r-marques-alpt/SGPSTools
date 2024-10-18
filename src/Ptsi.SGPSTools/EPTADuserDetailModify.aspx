<%@ Page language="c#" Codebehind="EPTADuserDetailModify.aspx.cs" AutoEventWireup="false" Inherits="PTIntranetSGPS.UserManagent.EPTADuserDetailModify" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Modificar Utilizador</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<script language="javascript">
	
		function OnBack() 
		{
			window.history.back();
		}
	
		function validPass() 
		{
			if (document.getElementById('txtPassword').value == "") 
			{
				alert("Para actualizar insira a password que usa para entrar no posto de trabalho.");
			} 
			else 
			{				
				(document.forms["EPTADuserDetailModify"]).submit();
				
				//EPTADuserDetailModify.submit();
			}
		}
		
		function Enter() 
		{
			validPass();
			return NoEnter();
		}

		function NoEnter() 
		{
			return !(window.event); 
		}	
		</script>
		<LINK rel="stylesheet" type="text/css" href="/SGPSTools/css/styles_ept.css">
	</HEAD>
	<BODY>
		<form id="EPTADuserDetailModify" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td class="fd_pop_links"><img src="/SGPSTools/img/lg_ept_pop.gif" alt="" width="66" height="23" hspace="0" vspace="0"
							border="0"></td>
				</tr>
			</table>
			<%if (isError == 0 && isSuccess == 0) {%>
			<table width="100%">
				<tr>
					<td valign="top" class="pos_tab_pop">
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="bd_tabs_main_conteudo">
							<tr>
								<td class="tit_mapa">
									<b>Edição do Detalhe do Utilizador</b>
								</td>
							</tr>
						</table>
						<table width="100%" border="0" height="359" class="bd_tabs_main_conteudo">
							<tr>
								<td class="txt_cont" nowrap>
									<asp:Label Font-Bold="True" id="Label3" runat="server">Nome Completo:&nbsp;</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtNomeCompleto" Cssclass="for_gen_dis" runat="server" Width="444px" Columns="60"
										ReadOnly="True" MaxLength="150"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont" nowrap><asp:Label Font-Bold="True" id="Label1" runat="server">Departamento:</asp:Label>
								</td>
								<td colspan="3"><asp:TextBox CssClass="for_gen" id="txtDepartamento"  Columns=60 runat="server" Width="263px" MaxLength="100"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont" nowrap><asp:Label Font-Bold="True" id="Label18" runat="server">Domínio:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox id="txtDominio" cssclass="for_gen_dis" runat="server" Width="444px" ReadOnly="True"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont">
									<asp:Label id="Label6" runat="server" Font-Bold="True">Empresa:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox cssclass="for_gen_dis" id="txtEmpresa" runat="server" Width="263px" ReadOnly="True"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont"><asp:Label id="Label8" runat="server" Font-Bold="True">Telefone 1:</asp:Label></td>
								<td><asp:TextBox id="txtTelefone1" cssclass="for_gen" runat="server" Width="148px" MaxLength="15"></asp:TextBox></td>
								<td class="txt_cont"><asp:Label id="Label7" runat="server" Font-Bold="True">Telefone 2:</asp:Label></td>
								<td colspan="3"><asp:TextBox cssclass="for_gen" id="txtTelefone2" runat="server" Width="148px" Enabled="False"
										MaxLength="15"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="txt_cont"><asp:Label id="Label10" runat="server" Font-Bold="True">Telemóvel:</asp:Label>
								</td>
								<td>
									<asp:TextBox id="txtTelemovel" CssClass="for_gen" runat="server" Width="148px" Enabled="False"
										MaxLength="15"></asp:TextBox>
								</td>
								<td class="txt_cont"><asp:Label id="Label9" runat="server" Font-Bold="True">Fax:</asp:Label>
								</td>
								<td><asp:TextBox cssclass="for_gen" id="txtFax" runat="server" Width="148px" MaxLength="15"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont" height="24"><asp:Label id="Label11" runat="server" Font-Bold="True">Email:</asp:Label>
								</td>
								<td colspan="3" height="24">
									<asp:TextBox cssclass="for_gen_dis" id="txtEmail" Columns=60 runat="server" Width="261px" ReadOnly="True"
										MaxLength="150"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont">
									<asp:Label id="Label12" runat="server" Font-Bold="True">Categoria:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox cssclass="for_gen" id="txtCategoria" runat="server" Columns="60" Width="261px" MaxLength="100"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont"><asp:Label id="Label13" runat="server" Font-Bold="True">Local Trabalho:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox cssclass="for_gen" id="txtLocalTrabalho" runat="server" Width="444px" MaxLength="100"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont">
									<asp:Label id="Label14" runat="server" Font-Bold="True">Morada:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox cssclass="for_gen" id="txtMorada" runat="server" Width="444px" Columns=60 MaxLength="150"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont"><asp:Label id="Label17" runat="server" Font-Bold="True">Observações:</asp:Label>
								</td>
								<td colspan="3">
									<asp:TextBox cssclass="for_gen" id="txtObservacoes" runat="server" Width="444px" Columns="60"
										Rows="3" TextMode="MultiLine" Height="41px" Enabled="False" MaxLength="250"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="txt_cont"><asp:Label id="Label19" runat="server" Font-Bold="True">Data Últ Act:</asp:Label></td>
								<td><asp:TextBox id="txtDataUltimaActualizacao" cssclass="for_gen" runat="server" ReadOnly="True"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td></td>
								<td colspan="3">
									<span class="txt_cont">
										<asp:Label id="Label2" runat="server" Height="22px" Width="240px">Para actualizar insira a password que usa para entrar no posto de trabalho:</asp:Label>
									</span>
									<asp:TextBox CssClass="for_gen" id="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Para actualizar os seus dados deverá introduzir a sua password."
										ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td></td>
								<td colspan="3">
									<p class="txt_cont">Esta actualização demorará alguns minutos a ser visível.</p>
								</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td></td>
								<td colspan="3">
									<p class="txt_cont">
										Para mais esclarecimentos, contacte <a href="mailto:dcc@telecom.pt">dcc@telecom.pt</a></p>
								</td>
							</tr>
							<tr>
								<td colspan="4" align="right">
									<span id="btnACT" runat="server">
										<asp:ValidationSummary id="ValidationSum" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
										<asp:LinkButton id="lnkButton" CssClass="lk_bot_SMS" runat="server">Actualizar</asp:LinkButton>
									</span>
									&nbsp; <a class="lk_bot_SMS" href="javascript:OnBack();">Voltar</a>
								</td>
							</tr>
						</table>
						<!-- END Search results -->
						<!-- END of Search Results List : PEDRO COLOCA O TEU CODE AQUI FIM -->
					</td>
				</tr>
			</table>
			<%} else if (isError == 1) {%>
			<table width="584" height="492" class="bd_tabs_main_conteudo" style="MARGIN: 9px">
				<tr>
					<td valign="top" width="100%" height="100%" class="txt_cont" align="center">
						<br>
						<b>Não é possível consultar os detalhes deste utilizador. </b>
						<br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="center">
						<a class="lk_bot_SMS" href="javascript:OnBack();">Voltar</a>
					</td>
				</tr>
			</table>
			<%} else if (isError == 2) {%>
			<table width="584" height="492" class="bd_tabs_main_conteudo" style="MARGIN: 9px">
				<tr>
					<td class="txt_cont" valign="top" width="100%" height="100%" align="center">
						<br>
						<b>Actualização de dados não é possível</b>
						<br>
						ou
						<br>
						<b>Password invalida.</b><br>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="center">
						<a class="lk_bot_SMS" href="javascript:OnBack();">Voltar</a>
					</td>
				</tr>
			</table>
			<%} else if (isSuccess == 1) {%>
			<table width="584" height="492" class="bd_tabs_main_conteudo" style="MARGIN: 9px">
				<tr>
					<td class="txt_cont" valign="top" width="100%" height="100%" align="center">
						<b>O pedido de actualização dos seus dados foi lançado com sucesso no servidor.<br>
							Será efectuado dentro de alguns minutos.</b>
					</td>
				</tr>
				<tr>
					<td align="center">
						<a class="lk_bot_SMS" href="javascript:OnBack();">Voltar</a>
					</td>
				</tr>
			</table>
			<%}%>
		</form>
	</BODY>
</HTML>
