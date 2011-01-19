<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CustomerMoviesModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Rent a movie
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Rent a movie for <%= Model.CustomerName %></h2>

    <ul>
        <% foreach (var item in Model.Medias)
           { %>
            <li>
                <%= item.Title %>
                <% using (Html.BeginForm("DoRent", "Customer"))
                   { %>
                   <%= Html.Hidden("customerId", Model.CustomerId)%>
                   <%= Html.Hidden("mediaId", item.Id)%>
                   <input type="submit" value="Rent this movie" />
                <% } %>
            </li>    
        <% } %>
    </ul>


</asp:Content>
