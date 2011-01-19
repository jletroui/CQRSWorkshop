<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CustomerMoviesModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Return a movie
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Rent a movie for <%= Model.CustomerName %></h2>

    <ul>
        <% foreach (var item in Model.RentedMedias)
           { %>
            <li>
                <%= item.MediaTitle %>
                <% using (Html.BeginForm("DoReturn", "Customer"))
                   { %>
                   <%= Html.Hidden("customerId", Model.CustomerId)%>
                   <%= Html.Hidden("mediaId", item.MediaId)%>
                   <input type="submit" value="Return this movie" />
                <% } %>
            </li>    
        <% } %>
    </ul>
</asp:Content>
