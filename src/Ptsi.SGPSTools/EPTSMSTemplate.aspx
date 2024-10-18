<%@ Page language="c#" Codebehind="EPTSMSTemplate.aspx.cs" AutoEventWireup="false" Inherits="SGPStools.Templates.EPTSMSTemplate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>Envio de SMS</title>
	<link rel="stylesheet" type="text/css" href="/SGPSTools/css/styles_ept.css">
	<script type="text/javascript" language="javascript">
	    function AlertMessage(msg) {
	        alert(msg);
	    }

	    function IsNumber(value) {
	        var i;

	        if (value == null || value == "")
	            return false;

	        for (i = 0; i < value.length; i++)
	            if (value.charAt(i) < '0' || value.charAt(i) > '9')
	                return false;
	        return true;
	    }

	    function contador(input, counterInput) {
	        var contadorMax = 125;
	        var charLeft = contadorMax;

	        if (input.value) {
	            charLeft = charLeft - input.value.length;

	            if (charLeft < 0) {
	                input.value = input.value.slice(0, contadorMax);
	                charLeft = 0;
	            }
	        }
	        counterInput.value = charLeft;
	    }

	    function ValidateUserSearchCriteria() {
	        var strNum = document.getElementById('numDestino').value;
	        if (document.getElementById('numDestino').value == "" || document.getElementById('numDestino').value == null) {
	            alert("Por favor preencha o número de telefone de destino");
            } else if (document.getElementById('msg').value == "" || document.getElementById('msg').value == null) {
	            alert("Por favor preencha o campo de mensagem.");
	        } else if (IsNumber(document.getElementById('numDestino').value) == false) {
	            alert("Por favor insira um número de telefone válido.");
	        } else if (document.getElementById('numDestino').value.length > 9) {
	            alert("Por favor insira um número de telefone válido.");
	        } else if (strNum.charAt(0) != '9' || strNum.charAt(1) != '6') {
	            alert("Por favor insira um número de telefone da rede MEO");
	        } else {
                //var mydv = document.getElementById('dvSendInfo');
	            //mydv.style.visibility = 'visible';
	            window.document.forms[0].submit();
	        }
	    }

	    function getSMSData() {
	        var nr_tel = "<%=Request.QueryString["nr_tel"]%>";
		    if (nr_tel != "" && nr_tel != null) {
		        if (window.document.getElementById('numDestino') != null) {
		            window.document.getElementById('numDestino').value = nr_tel;
		        }
		    }
		}

		function textCounter(field) {
            smsMgsCount = document.getElementById('maxChars').value;
		    if (field.value.length > smsMgsCount) {
		        field.value = field.value.substring(0, smsMgsCount);
                document.getElementById('counter2').value = smsMgsCount - field.value.length + "  Caracteres disponíveis";
		    }
		    else {
                document.getElementById('counter2').value = smsMgsCount - field.value.length + "  Caracteres disponíveis";
		    }
		}

		function OnBack() {
		    window.history.back();
		}
    </script>
</head>
<body onload="getSMSData()">
<form id="SMSTemplate" method="post" runat="server">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td  class="fd_pop_links2"><img src="/SGPSTools/img/new/intranet.myaltice.jfif" alt="" hspace="0" vspace="0" border="0"></td>
		</tr>
	</table>
	<asp:Literal ID="ltrDebugHD" Runat="server"></asp:Literal>
	<table border="0">
		<tr>
			<td valign="top" class="pos_tab_pop">						
				<%if (statePage==0) {%>
				<table width="100%" border="0" cellpadding="0" cellspacing="0" class="bd_tabs_main_conteudo">
					<tr>
						<td class="tit_mapa"><b>Envio de SMS</b></td>
					</tr>
				</table>
				<table width="100%" border="0" cellspacing="0" cellpadding="0" class="bd_tabs_main_conteudo">
					<tr style="padding-bottom: 10px;">								
						<td colspan="2" class="txt_adi_lk">																
							<b>Caracteres Disponíveis</b>&nbsp;<input class="for_gen" size="3" maxlength="3" value="125" readonly type="text" name="counter2" id="counter2">
						</td>
					</tr>
					<tr style="padding-bottom: 3px;">
						<td align="left" nowrap class="txt_adi_lk">Número de Destino:</td>
						<td>
							<table width="100%" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td align="left" width="100%">
                                        &nbsp;<input runat="server" class="for_gen" id="numDestino" name="numDestino" value="<%#nr_tel%>" type="text" size="42" />
										<asp:RequiredFieldValidator Display="Dynamic" ID=rfvNumDestino Runat="server" ErrorMessage="Não inseriu nº de telemóvel de destino" ControlToValidate="numDestino"></asp:RequiredFieldValidator> 	
										<asp:RegularExpressionValidator Display="Dynamic" ID="regexNumDestino1" Runat="server" ControlToValidate="numDestino" ErrorMessage="O telemóvel que inseriu não é válido" ValidationExpression="\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
										<asp:RegularExpressionValidator Display="Dynamic" ID="regexNumDestino2" Runat="server" ControlToValidate="numDestino" ErrorMessage="O telemóvel que inseriu não é da MEO" ValidationExpression="^9[2|6]\d\d\d\d\d\d\d$"></asp:RegularExpressionValidator>
									</td>
									<td align="right" nowrap style="padding-right: 42px;"><asp:CheckBox ID="chkMail" Runat="server" Text="Indicar e-mail na mensagem" TextAlign="Right" Checked="False" CssClass="for_gen"></asp:CheckBox></td>
								</tr>
							</table>
						</td>
					</tr>													
					<tr>
						<td class="txt_adi_lk" align="left" valign="top">Mensagem:</td>
						<td valign="top" align="left">
							<table width="100%" border="0" cellspacing="0" cellpadding="0">
								<tr id="imgTR">
									<td align="left" valign="top" class="for_ta">												
										<textarea runat="server" class="for_ta" id="msg" name="msg" cols="90" onblur="contador(this,SMSTemplate.counter2);" onkeyup="contador(this,SMSTemplate.counter2);" rows="8" wrap="soft"></textarea>
									</td>
									<td align="left" width="55">
										&nbsp;<asp:RequiredFieldValidator ID="rfvMsg" Runat="server" ControlToValidate="msg" Display="Dynamic" ErrorMessage="Por favor insira a mensagem que pretende enviar"></asp:RequiredFieldValidator>																							
									</td>
								</tr>
							</table>									
						</td>
					</tr>
					<tr>
						<td align="right" colspan="2" style="padding-top:4px; padding-bottom:4px; padding-right: 42px;">									
							<asp:LinkButton id="SearchUsersButton" CssClass="lk_bot_SMS2" Runat="server">Enviar</asp:LinkButton>
						</td>
					</tr>
				</table>
				<%} else if (statePage==SUCCESS) {%>
				<table height="210" width="655" border="0" cellspacing="0" cellpadding="0" class="bd_tabs_main_conteudo">
					<tr>
						<td align="center" class="txt_adi_lk"><b>A mensagem foi enviada com sucesso para o servidor de sms.</b></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td align="center"><a class="lk_bot_SMS2" href="javascript:OnBack();">Voltar</a></td>
					</tr>
				</table>
				<%} else if (statePage==ERR_PROBLEM_SENDING_SMS_TO_GATEWAY) {%>
				<table height="210" width="655" border="0" cellspacing="0" cellpadding="0" class="bd_tabs_main_conteudo">
					<tr>
						<td align="center" class="txt_adi_lk"><b>Não foi possível enviar a mensagem para o servidor de sms,<br>Pedimos desculpa pelo incómodo!</b></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td align="center"><a class="lk_bot_SMS2" href="javascript:OnBack();">Voltar</a></td>
					</tr>
				</table>
				<%} else if (statePage==ERR_SENDER_NOT_REGISTERED_IN_AD) {%>
				<table height="210" width="655" border="0" cellspacing="0" cellpadding="0" class="bd_tabs_main_conteudo">
					<tr>
						<td align="center" class=txt_adi_lk><b>Não lhe é permitido enviar SMS's, pois o seu número de telefone não se encontra<br>registado no directório corporativo (AD).</b></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td align="center"><a class="lk_bot_SMS2" href="javascript:OnBack();">Voltar</a></td>
					</tr>
				</table>
				<%} else if (statePage==ERR_RECEIVER_NOT_REGISTERED_IN_AD) {%>
				<table height="210" width="655" border="0" cellspacing="0" cellpadding="0" class="bd_tabs_main_conteudo">
					<tr>
						<td align="center" class="txt_adi_lk"><b>Não lhe é permitido enviar SMS's, pois o número de destino inserido<br> não se encontra registado no directório corporativo (AD).</b></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td align="center"><a class="lk_bot_SMS2" href="javascript:OnBack();">Voltar</a></td>
					</tr>
				</table>
				<%} else if (statePage==ERR_REGISTER_IN_CONTACT_DIRECTORY_FIRST) {%>
				<table height="210" width="655" border="0" cellspacing="0" cellpadding="0" class="bd_tabs_main_conteudo">
					<tr>
						<td align="center" class="txt_adi_lk">
							<b>O envio de mensagens só é possível para telemóveis registados no directório de contactos.<br><br>
							Caso não consiga receber mensagens através deste serviço,<br> insira o seu número de telemóvel na ficha de contactos do Portal myaltice<br><br>
							(Pesquise pelo seu nome e edite os seus detalhes)</b>
						</td>
					</tr>
					<tr>
						<td align="center">																			
							<a class="lk_bot_SMS2" href='javascript:document.location="EPTSMSTemplate.aspx?warningFalse=1";'>Continuar</a>&nbsp;&nbsp;&nbsp;
							<a class="lk_bot_SMS2" href="javascript:OnBack();">Voltar</a>										
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
				</table>
				<%}%>
				<!-- END Search results -->
			</td>
		</tr>
	</table>
	<input type="hidden" id="maxChars" name="maxChars" value='<%=MAXSIZE%>'> 
	<asp:ValidationSummary id="ValidationSum" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
</form>
</body>
</html>