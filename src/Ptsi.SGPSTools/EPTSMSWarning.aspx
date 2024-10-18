<%@ Page language="c#" Codebehind="EPTSMSWarning.aspx.cs" AutoEventWireup="false" Inherits="SGPStools.EPTSMSWarning" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SMSWarning</title>
		<LINK rel="stylesheet" type="text/css" href="/SGPStools/css/insapo.css">
		<script language="javascript" type="text/javascript">
		function OnBack() {
			window.history.back();
		}  
		</script>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="SMSWarning" method="post" runat="server">
			<table width="455" border="0" cellspacing="0" cellpadding="0" class="tablecanais">
				<tr>
					<td align="middle">
						O serviço de envio de SMS só está disponível para telemóveis registados.
					</td>
				</tr>
				<tr>
					<td align="middle">
						<INPUT type="button" class="button" value="Seguinte" style="WIDTH: 85px" onclick='javaScript:document.location="EPTSMSTemplate.aspx?warningFalse=1&amp;<%=Request.QueryString.ToString()%>"' size="20">
						<!--&nbsp;&nbsp;&nbsp;<INPUT type="button" class="button" value="Voltar" style="width: 85" onclick="OnBack()" size="">-->
					</td>
				</tr>
				<tr>
					<td>&nbsp;
					</td>
				</tr>
				<!--				<tr>
					<td align=center>
						<INPUT type="button" class="button" value="Voltar" style="width: 85" onclick="OnBack()" size="">
					</td>
				</tr>
-->
			</table>
		</form>
	</body>
</HTML>
