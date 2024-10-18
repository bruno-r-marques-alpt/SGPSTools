<%@ Page language="c#" Codebehind="EPTADuserDetail.aspx.cs" AutoEventWireup="false" Inherits="PTIntranetSGPS.UserManagent.EPTADuserDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	<title>Detalhe do Utilizador</title>
	<script language="javascript">
		function OnExit() {
			window.close();
		}

		function CopyData2Clipboard() {
			if( confirm("Copiar o texto ?\n\n" + Text2Copy.innerText) )
				window.clipboardData.setData("Text",Text2Copy.innerText);		
		}
		</script>
		<LINK rel="stylesheet" type="text/css" href="/SGPSTools/css/styles_ept.css">
	</HEAD>
	<body>
		<form id="EPTADuserDetail" method="post" runat="server">
			<table width="100%" cellpadding=0 cellspacing=0>
				<tr>
					<td  class="fd_pop_links"><img src="/SGPSTools/img/lg_ept_pop.gif" alt="" width="66" height="23" hspace="0" vspace="0" border="0"></td>
				</tr>
			</table>				
			<%if (isError == 0 && isSuccess == 0) {%>				
				<table width=100%">					
					<tr>					
						<td valign="top" class="pos_tab_pop">	
							<table width="100%" border="0" cellpadding="0" cellspacing="0" class="bd_tabs_main_conteudo">
								<tr>
									<td class="tit_mapa">
										<b>Detalhe do Utilizador</b>
									</td>
								</tr>
							</table>														
							<div id="Text2Copy">
								<table width="100%" border="0" height="399"  class="bd_tabs_main_conteudo">
									<tr>
										<td class="txt_cont" width="113"><asp:Label  id="Label3" runat="server">Nome Completo:</asp:Label>
										</td>
										<td width="400" class="txt_cont" colspan="3">
											<asp:Label id="txtNomeCompleto" runat="server" Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td class="txt_cont" width="113"><asp:Label id="Label1" runat="server">Departamento:</asp:Label>
										</td>
										<td width="400" class="txt_cont" colspan="3">
											<asp:Label id="txtDepartamento" runat="server" Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td class="txt_cont" width="113"><asp:Label CssClass="txt_cont" id="Label18" runat="server" >Domínio:</asp:Label></td>
										<td width="400" class="txt_cont" colspan="3">
											<asp:Label id="txtDominio" runat="server" Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td width="113" class="txt_cont" >
											<asp:Label id="Label6" runat="server">Empresa:</asp:Label>
										</td>
										<td colspan="3" width="400" class="txt_cont">
											<asp:Label id="txtEmpresa" runat="server" Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td width="113" class="txt_cont"><asp:Label id="Label8" runat="server">Telefone 1:</asp:Label></td>
										<td class="txt_cont"  width="100"><asp:Label id="txtTelefone1"  Font-Bold=True runat="server" ReadOnly="True"></asp:Label></td>
										<td width="113" class=txt_cont><asp:Label  id="Label7" runat="server" >Telefone 2:</asp:Label></td>
										<td class=txt_cont width="100"><asp:Label id="txtTelefone2" runat="server" Font-Bold=True ReadOnly="True"></asp:Label></td>
									</tr>
									<tr>
										<td class=txt_cont width="113"><asp:Label id="Label10" runat="server">Telemóvel:</asp:Label></td>
										<td class=txt_cont width="100"><asp:Label id="txtTelemovel" runat="server" Font-Bold=True ReadOnly="True"></asp:Label></td>
										<td class=txt_cont width="113"><asp:Label CssClass="txt_cont" id="Label9" runat="server">Fax:</asp:Label></td>
										<td class=txt_cont width="100"><asp:Label id="txtFax" runat="server" Font-Bold=True ReadOnly="True"></asp:Label></td>
									</tr>
									<tr>
										<td width="113" class=txt_cont><asp:Label id="Label11" runat="server">Email:</asp:Label></td>
										<td colspan="3"  width="400" class=txt_cont>
											<asp:Label id="txtEmail" runat="server" Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td width="113" class=txt_cont>
											<asp:Label id="Label12" CssClass="txt_cont" runat="server">Categoria:</asp:Label>
										</td>
										<td colspan="3"  width="400" class=txt_cont>
											<asp:Label id="txtCategoria" runat="server" Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
										<tr>
										<td width="113" class=txt_cont><asp:Label id="Label13" runat="server" >Local Trabalho:</asp:Label>
										</td>
										<td colspan="3"  width="400" class=txt_cont>
											<asp:Label id="txtLocalTrabalho" runat="server" Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td width="113" class=txt_cont>
											<asp:Label id="Label14" runat="server">Morada:</asp:Label>
										</td>
										<td colspan="3" class=txt_cont>
											<asp:Label  width="400" id="txtMorada" runat="server"  Font-Bold=True ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td width="113" class=txt_cont><asp:Label id="Label17" runat="server">Observações:</asp:Label>
										</td>
										<td colspan="3"  width="400" class=txt_cont>
											<asp:Label id="txtObservacoes" runat="server" Font-Bold=True  Rows="3" TextMode="MultiLine" ReadOnly="True"></asp:Label>
										</td>
									</tr>
									<tr>
										<td width="113" class=txt_cont><asp:Label id="Label19" runat="server">Data Última Act.:</asp:Label>
										</td>
										<td colspan="3"  width="400" class=txt_cont><asp:Label id="txtDataUltimaActualizacao" Font-Bold=True  runat="server" ReadOnly="True"></asp:Label>
										</td>
										<td colspan="2" align="right">&nbsp;
										</td>
									</tr>
								</table>
							</div>
							<table width="100%" border="0" class="bd_tabs_main_conteudo">
								<tr>
									<td align="left">
										<table width="100%" height="100%" border="0">
											<tr>
												<td width="90" align="left" valign="middle">													
													<a class="lk_bot_SMS" href="javascript:window.location='EPTADuserDetailModify.aspx?<%=Request.QueryString.ToString()%>'" >Editar&nbsp;</a>	
												</td>																																	
												<td align="left" valign="middle"><span class="txt_cont">Clique aqui para actualizar os seus dados.</span></td>
											</tr>
										</table>
									</td>
									<td align="center" valign="middle" rowspan="2">																			
										<a class="lk_bot_SMS" href="javascript:OnExit();">Sair</a>&nbsp;&nbsp;		
									</td>
								</tr>
								<span id=spanCopy runat=server>
								<tr>
									<td valign="middle">
										<table width="100%" height="100%" border="0">
											<tr>
												<td width="90" align="left" valign="middle">											
													<a class="lk_bot_SMS" onclick="CopyData2Clipboard();" href="javascript:">Copiar</a>												
												</td>											
												<td align="left" valign="middle"><span class="txt_cont"> Clique aqui para copiar estas informações.</span></td>
											</tr>
										</table>
									</td>
								</tr>
								</span>
							</table>
						</td>	
					</tr>
				</table>			
			</TD>
		</TR>
	</TABLE>
	<%} else if (isError == 1) {%>
	<table width=584 height=492 class="bd_tabs_main_conteudo" style="MARGIN: 9px" >	
		<tr>
			<td class="txt_cont" valign="middle" height=100% width=100% align="center">
				<br><b>
				Não é possível consultar os detalhes deste utilizador.</b><br>
			</td>
		</tr>		
		<tr>
			<td valign=top align="center">
				<a class="lk_bot_SMS" onclick="OnExit();" href="javascript:">Sair</a>	
			</td>
		</tr>
		<tr>
			<td height=100%>&nbsp;
			</td>
		</tr>
	</table>
	<%} else if (isError == 2) {%>
	<table width=584 height=492 class="bd_tabs_main_conteudo" style="MARGIN: 9px" >	
		<tr>
			<td class="txt_cont" valign="middle" height=100% width=100% align="center">
				<br><b>Password invalida.</b><br>
			</td>
		</tr>
		<tr>
			<td align="center">
				<a class="lk_bot_SMS" onclick="OnExit();" href="javascript:">Sair</a>	
			</td>
		</tr>
		<tr>
			<td height=100%>&nbsp;
			</td>
		</tr>
	</table>
	<%} else if (isSuccess == 1) {%>
	<table width=584 height=492 class="bd_tabs_main_conteudo" style="MARGIN: 9px" >	
		<tr>
			<td class="txt_cont" valign="middle" height=100% width=100% align="center">
				<br>
				<b>
				O pedido de actualização dos seus dados foi lançado com sucesso no servidor.<br>
				Será efectuado dentro de alguns minutos.
				</b>
				<br>
			</td>
		</tr>	
		<tr>
			<td align="center">
				<a class="lk_bot_SMS" onclick="OnExit();" href="javascript:">Sair</a>	
			</td>
		</tr>
		<tr>
			<td height=100%>&nbsp;
			</td>
		</tr>
	</table>
	<%}%>
</form>
</body>
</HTML>
