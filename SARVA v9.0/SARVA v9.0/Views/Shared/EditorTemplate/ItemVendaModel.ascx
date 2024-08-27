<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemVendaModel.ascx.cs" Inherits="SARVA.Views.Shared.EditorTemplate.ItemVendaModel" %>

<tr>
    <td>
        <%= Html.TextBoxFor(x => x.string1) %>
    </td>
    <td>
        <%= Html.TextBoxFor(x => x.string2) %>
    </td>
</tr>
