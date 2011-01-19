<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MediaItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of medias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul>
        <% foreach (var item in Model)
           { %>
            <li><%= item.Title %></li>    
        <% } %>
    </ul>
    <% using (Html.BeginForm("Add", "Media"))
       { %>
        Add new media with title <%= Html.TextBox("title") %> <input type="submit" value="Add New Media" />
    <% } %>
</asp:Content>
