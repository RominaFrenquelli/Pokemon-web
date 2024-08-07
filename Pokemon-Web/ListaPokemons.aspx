<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaPokemons.aspx.cs" Inherits="Pokemon_Web.ListaPokemons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Lista de Pokemons</h1>
    <div class="row">
        <div class="col-6">
            <div class="input-group mb-3">
                <span class="input-group-text">Buscar</span>
                <asp:TextBox runat="server" ID="txtFiltrar" CssClass="form-control" aria-describedby="btnBuscar" AutoPostBack="true" OnTextChanged="txtFiltrar_TextChanged" />
                <button class="btn btn-outline-secondary" type="button" id="btnBuscar">
                    <img src="images/lupa.png" alt="Buscar" /></button>
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado" CssClass="" ID="chkFiltroAvanzado" runat="server" AutoPostBack="true" OnCheckedChanged="chkFiltroAvanzado_CheckedChanged" />
            </div>
        </div>
    </div>
    <%if (FiltroAvanzado)
        {%>
    <div class="row">
        <div class="col-3">
            <div class="input-group mb-3">
                <asp:Label Text="Campo" runat="server" CssClass="input-group-text" Style="background-color: red; color: white; border: 3px solid black" />
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-select" ID="ddlCampo" Style="background-color: white; border: 2px solid black" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" >
                    <asp:ListItem Text="Nombre" />
                    <asp:ListItem Text="Numero" />
                    <asp:ListItem Text="Tipo" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="input-group mb-3">
                <asp:Label Text="Criterio" runat="server" CssClass="input-group-text" Style="background-color: red; color: white; border: 3px solid black" />
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-select" ID="ddlCriterio" Style="background-color: white; border: 2px solid black;"></asp:DropDownList>
            </div>
        </div>

        <div class="col-3">
            <div class="input-group mb-3">
                <asp:Label Text="Filtro" runat="server" CssClass="input-group-text" Style="background-color: red; color: white; border: 3px solid black" />
                <asp:TextBox ID="txtFiltroAvanzado" runat="server" CssClass="form-control" Style="background-color: white; border: 2px solid black;" />
            </div>
        </div>
        <div class="col-3">
            <div class="input-group mb-3">
                <asp:Label Text="Estado" runat="server" CssClass="input-group-text" Style="background-color: red; color: white; border: 3px solid black" />
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-select" ID="ddlEstado" Style="background-color: white; border: 2px solid black;">
                    <asp:ListItem Text="Todos" />
                    <asp:ListItem Text="Activo" />
                    <asp:ListItem Text="Inactivo" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="mb-3">
            <asp:Button ID="btnBuscarFiltro" runat="server" Text="Buscar Filtro" CssClass="btn btn-secondary" OnClick="btnBuscarFiltro_Click" />
        </div>
    </div>
    <%}%>
    <asp:GridView ID="dgvPokemons" runat="server" DataKeyNames="Id" CssClass="table" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvPokemons_SelectedIndexChanged" OnPageIndexChanging="dgvPokemons_PageIndexChanging" AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Numero" DataField="Numero" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
            <asp:BoundField HeaderText="Debilidad" DataField="Debilidad.Descripcion" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
            <asp:CommandField HeaderText="Accion" ShowSelectButton="True" SelectText="✍️" />
        </Columns>
    </asp:GridView>
    <a href="FormularioPokemon.aspx" class="btn btn-primary">Agregar</a>

</asp:Content>
