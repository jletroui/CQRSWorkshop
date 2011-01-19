<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CustomerItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List of customers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul>
        <% foreach (var item in Model)
           { %>
            <li>
                <%= item.Name %>
                <% if (item.CanRent)
                   {%>
                   <%= Html.ActionLink("Rent a video", "Rent", new { customerId = item.Id })%>
                <% } %>
                <% if (item.CanReturn)
                   {%>
                   <%= Html.ActionLink("Return a video", "Return", new { customerId = item.Id })%>
                <% } %>
            </li>    
        <% } %>
    </ul>
    <% using (Html.BeginForm("Register", "Customer"))
       { %>
        Register new customer with name <%= Html.TextBox("name") %> <input type="submit" value="Register New Customer" />
    <% } %>
</asp:Content>
