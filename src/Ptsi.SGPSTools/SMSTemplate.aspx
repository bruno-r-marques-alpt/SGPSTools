<%@ Page language="c#" Codebehind="SMSTemplate.aspx.cs" AutoEventWireup="false" Inherits="SGPStools.Templates.SMSTemplate" %>
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
		<LINK rel="stylesheet" type="text/css" href="/SGPStools/css/insapo.css">
			<script language="javascript">
						
		function IsNumber(value)
		{
			var i;
			
			if(value == null || value == "")
				return false;
			
			for(i=0;i<value.length;i++)
				if(value.charAt(i)<'0' || value.charAt(i)>'9')
					return false;					
			return true;
		}	
		
		
			
function contador(input,counterInput) 

{

   var contadorMax = 125;

   var charLeft = contadorMax;

   if(input.value) 

   {

     charLeft = charLeft - input.value.length;

     if (charLeft<0){

         input.value = input.value.slice(0,contadorMax);

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
                var mydv = document.all["dvSendInfo"];
				mydv.style.visibility = 'visible';
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
			if (field.value.length > smsMgsCount) 
			{
				field.value = field.value.substring(0, smsMgsCount);
				document.all.counter2.value = smsMgsCount - field.value.length + "  Caracteres disponíveis";
			}			
			else 
			{
				document.all.counter2.value = smsMgsCount - field.value.length + "  Caracteres disponíveis";
			}
		}	
		
		function OnBack() {
			window.history.back();
		}
		
            </script>
	</HEAD>
	<body onload="getSMSData()" text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="SMSTemplate" method="post" runat="server">
			<table width="625" border="0">
				<tr>
					<td valign="top">
						<%if (statePage==0) {%>
						<table width="625" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
							<tr>
								<td align="left">																
								</td>
								<td align="left">Caracteres Disponíveis:&nbsp;<input class="textfieldDisabled" size="3" maxlength="3" readonly="true" type="text" name="counter2" id="counter2" value='<%=MAXSIZE%>'>									
								</td>
							</tr>
							<tr>
								<td colspan="2">&nbsp;</td>
							</tr>
							<tr>
								<td align="left">Número de Destino:
								</td>
								<td align="left"><input class="textfield" id="numDestino" name="numDestino" value='<%# nr_tel%>' style="WIDTH: 288px; HEIGHT: 22px" type="text" size="42">
								</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td align="left" valign="top">Mensagem:
								</td>
								<td align="left">
									<table width="100%" border="0" cellspacing="0" cellpadding="0">
										<tr id="imgTR">
											<td align="left">												
												<TEXTAREA id="msg" name="msg" class="textfield" rows="7" cols="52" onblur="contador(this,SMSTemplate.counter2);" onkeyup="contador(this,SMSTemplate.counter2);" rows="5" wrap="soft"></TEXTAREA>
											</td>
											<td align="center" width="100%">
												<div id="dvSendInfo" STYLE="visibility:hidden">
													<img src="img/sms.gif" align="center">
													<br>
													<font size="2"><b>A enviar mensagem...</b></font>
												</div>
											</td>
										</tr>
									</table>
									<!--<input id="msg" name="msg" class="textfield" style="WIDTH: 289px; HEIGHT: 110px" type="text"  maxLength="160" size="42"> -->
								</td>
							</tr>
							<tr>
								<td align="middle" colspan="2">
									<INPUT id="SearchUsersButton" class="button" type="button" value="Enviar" style="width: 85" onclick="ValidateUserSearchCriteria()"></td>
							</tr>
						</table>
						<%} else if (statePage==1) {%>
						<TABLE border="0" cellspacing="0" cellpadding="0" class="tablecanais" width="455">
							<tr>
								<td align="center">
									A mensagem foi enviada com sucesso para o servidor de sms.
								</td>
							</tr>
							<tr>
								<td>&nbsp;
								</td>
							</tr>
							<tr>
								<td align="center">
									<INPUT type="button" class="button" value="Voltar" style="width: 85" onclick="OnBack()"
										size="">
								</td>
							</tr>
						</TABLE>
						<%} else if (statePage==2) {%>
						<table width="455" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
							<tr>
								<td align="center">
									Não foi possível enviar a mensagem para o servidor de sms,<br>
									Pedimos desculpa pelo incómodo!
								</td>
							</tr>
							<tr>
								<td>&nbsp;
								</td>
							</tr>
							<tr>
								<td align="center">
									<INPUT type="button" class="button" value="Voltar" style="width: 85" onclick="OnBack()"
										size="">
								</td>
							</tr>
						</table>
						<%} else if (statePage==4) {%>
						<table width="455" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
							<tr>
								<td align="center">
									Não lhe é permitido enviar SMS's, devido a não se encontrar
									<br>
									registado no directório corporativo.
								</td>
							</tr>
							<tr>
								<td>&nbsp;
								</td>
							</tr>
							<tr>
								<td align="center">
									<INPUT type="button" class="button" value="Voltar" style="width: 85" onclick="OnBack()"
										size="">
								</td>
							</tr>
						</table>
						<%} else if (statePage==3) {%>
						<table width="455" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
							<tr>
								<td align="center">
									Não lhe é permitido enviar SMS's,
									<br>
									devido ao número inserido não se encontrar registado no directório corporativo.
								</td>
							</tr>
							<tr>
								<td>&nbsp;
								</td>
							</tr>
							<tr>
								<td align="center">
									<INPUT type="button" class="button" value="Voltar" style="width: 85" onclick="OnBack()"
										size="">
								</td>
							</tr>
						</table>
						<%} else if (statePage==5) {%>
						<table width="455" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
							<tr>
								<td align="center">
									O serviço de envio de SMS só está disponível para colaboradores que já migraram 
									para o Exchange Corporativo.
									<br>
									O envio de mensagens só é possível para telemóveis registados no directório de 
									contactos.
								</td>
							</tr>
							<tr>
								<td align="center">
									<INPUT type="button" class="button" value="Continuar" style="width: 85px" onclick='JavaScript:document.location="SMSTemplate.aspx?warningFalse=1"'
										size=""> &nbsp;&nbsp;&nbsp;
									<INPUT type="button" class="button" value="Voltar" style="width: 85px" onclick="OnBack()"
										size="">
								</td>
							</tr>
							<tr>
								<td>&nbsp;
								</td>
							</tr>
						</table>
						<%}%>
						<!-- END Search results -->
					</td>
				</tr>
				</table>
			<input type="hidden" id="maxChars" name="maxChars" value=<%=MAXSIZE%>>
		</form>
	</body>
</HTML>
