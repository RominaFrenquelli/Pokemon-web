<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pokemon_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>HOLA</h1>
    <p>Este es tu lugar Pokemon...</p>
    <div class="row row-cols-1 row-cols-md-3 g-4">
       <%-- <%
            foreach (Modelos.Pokemon poke in listaPokemon)
            {%>
                <div class="col">
                    <div class="card">
                        <img src="<%: poke.UrlImagen %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%: poke.Nombre %></h5>
                            <p class="card-text"><%:poke.Descripcion %></p>
                            <a href="Detalle.Pokemon.aspx?id=<%: poke.Id %>">ver detalle</a>
                        </div>
                    </div>
                </div>
        <%  }%>--%>

        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%#Eval("UrlImagen") %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <a href="Detalle.Pokemon.aspx?id=<%#Eval("Id") %>">ver detalle</a>
                            <asp:Button ID="btnEjemplo" runat="server" Text="Ejemplo" cssclass="btn btn-primary" CommandArgument='<%#Eval("Id") %>' CommandName="PokemonId" OnClick="btnEjemplo_Click"/>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>


</asp:Content>
