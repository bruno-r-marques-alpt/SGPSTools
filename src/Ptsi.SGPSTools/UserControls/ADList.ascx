<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ADList.ascx.cs" Inherits="SGPStools.UserControls.ADList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<!-- BEGIN Javascript utils -->
<script language="javascript">
	<!--
	function AbrirJanela(opcao, target, mywidth, myheight, myscrollbars) {
		window.open(opcao,target,"toolbar=yes,location=no,directories=no,status=no,menubar=no,scrollbars=" + myscrollbars + ",resizable=no,copyhistory=no,width=" + mywidth + ",height=" + myheight);
		return true;
	}
	// -->
</script>
<!-- ENDJavascript utils -->

<asp:Repeater ID="ListAD" Runat="server">
	<ItemTemplate>
		<%# DataBinder.Eval(Container.DataItem, "strRow")%>
	</ItemTemplate>
</asp:Repeater>

<tr>
	<td colspan=<%=numCols%>>&nbsp;
	</td>
</tr>	

<tr>
	<td align="center" class="texto" colspan=<%=numCols%>>
		<a class='linksverde' href="<%=linkAnterior%>">Anterior</a>
		&nbsp;&nbsp;Pág.&nbsp;<%=currPage%> de <%=numTotalPag%>&nbsp;&nbsp;
		<a class='linksverde' href="<%=linkSeguinte%>">Seguinte</a>
	</td>
</tr>

<input type=hidden id=strCri value=<%=strCretiriaTMP%>>

