<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroAlumno.aspx.cs" Inherits="GestionEscuela.Maestros.RegistroAlumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registro de Alumno</h2>
    <div class="form-group">
        <label for="ddlUsuarios">Seleccione Usuario (Estudiante):</label>
        <asp:DropDownList ID="ddlUsuarios" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="form-group">
        <label for="txtIdEstudiante">ID Estudiante:</label>
        <asp:TextBox ID="txtIdEstudiante" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtMatricula">Matrícula:</label>
        <asp:TextBox ID="txtMatricula" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtCurp">CURP:</label>
        <asp:TextBox ID="txtCurp" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="ddlGrupos">Seleccione Grupo:</label>
        <asp:DropDownList ID="ddlGrupos" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnRegistrar" CssClass="btn btn-primary" runat="server" Text="Registrar Estudiante" OnClick="btnRegistrar_Click" />

    <div class="form-group">
        <asp:GridView ID="gvEstudiante" CssClass="table" runat="server" AutoGenerateColumns="False" Visible="False" OnRowCommand="gvEstudiante_RowCommand">
            <Columns>
                <asp:BoundField DataField="idEstudiante" HeaderText="ID Estudiante" />
                <asp:BoundField DataField="matricula" HeaderText="Matrícula" />
                <asp:BoundField DataField="curp" HeaderText="CURP" />
                <asp:BoundField DataField="idGrupo" HeaderText="ID Grupo" />
                <asp:BoundField DataField="calif1ero" HeaderText="1ero" />
                <asp:BoundField DataField="calif2do" HeaderText="2do" />
                <asp:BoundField DataField="calif3ero" HeaderText="3ero" />
                <asp:BoundField DataField="promedioGeneral" HeaderText="Promedio" />
                <asp:ButtonField CommandName="Edit" Text="Editar" />
                <asp:ButtonField CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>

    <h2>Asignación de Tutor</h2>
    <div class="form-group">
        <label for="ddlTutores">Seleccione Tutor:</label>
        <asp:DropDownList ID="ddlTutores" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnAsignarTutor" CssClass="btn btn-primary" runat="server" Text="Asignar Tutor" OnClick="btnAsignarTutor_Click" />

    <div class="form-group">
        <asp:GridView ID="gvTutores" CssClass="table" runat="server" AutoGenerateColumns="False" Visible="False" OnRowCommand="gvTutores_RowCommand">
            <Columns>
                <asp:BoundField DataField="idTutor" HeaderText="ID Tutor" />
                <asp:BoundField DataField="relacion" HeaderText="Relación" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:ButtonField CommandName="Edit" Text="Editar" />
                <asp:ButtonField CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

