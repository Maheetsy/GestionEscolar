<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarDocente.aspx.cs" Inherits="GestionEscuela.Directivo.RegistrarDocente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registro de Docente</h2>
    <div class="form-group">
        <label for="ddlUsuarios">Seleccione Usuario (Docente):</label>
        <asp:DropDownList ID="ddlUsuarios" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnRegistrar" CssClass="btn btn-primary" runat="server" Text="Registrar Docente" OnClick="btnRegistrar_Click" />

    <div class="form-group">
        <asp:GridView ID="gvDocentes" CssClass="table" runat="server" AutoGenerateColumns="False" Visible="False" OnRowCommand="gvDocentes_RowCommand">
            <Columns>
                <asp:BoundField DataField="idUsuario" HeaderText="ID Usuario" />
                <asp:BoundField DataField="idDocente" HeaderText="ID Docente" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:ButtonField CommandName="Edit" Text="Editar" />
                <asp:ButtonField CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>

    <h2>Asignación de Departamento</h2>
    <div class="form-group">
        <label for="ddlDepartamentos">Seleccione Departamento:</label>
        <asp:DropDownList ID="ddlDepartamentos" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnAsignarDepartamento" CssClass="btn btn-primary" runat="server" Text="Asignar Departamento" OnClick="btnAsignarDepartamento_Click" />

    <div class="form-group">
        <asp:GridView ID="gvDepartamentos" CssClass="table" runat="server" AutoGenerateColumns="False" Visible="False" OnRowCommand="gvDepartamentos_RowCommand">
            <Columns>
                <asp:BoundField DataField="idDocente" HeaderText="ID Docente" />
                <asp:BoundField DataField="idDepar" HeaderText="ID Departamento" />
                <asp:BoundField DataField="nombreDepartamento" HeaderText="Nombre Departamento" />
                <asp:ButtonField CommandName="Edit" Text="Editar" />
                <asp:ButtonField CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
