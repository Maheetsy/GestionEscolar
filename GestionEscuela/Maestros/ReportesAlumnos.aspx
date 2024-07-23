<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportesAlumnos.aspx.cs" Inherits="GestionEscuela.Maestros.ReportesAlumnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Reportes de Alumnos</h2>

    <div class="row">
        <div class="col-md-6 form-group">
            <label for="ddlGrupos">Seleccione Materia:</label>
            <asp:DropDownList ID="ddlMaterias" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMaterias_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="col-md-6 form-group">
            <label for="ddlEstudiantes">Seleccione Estudiante:</label>
            <asp:DropDownList ID="ddlEstudiantes" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstudiantes_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <div>
        <h3>Reportes del Estudiante</h3>
        <asp:GridView ID="gvReportes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="idReporte" HeaderText="ID Reporte" />
                <asp:BoundField DataField="detalle" HeaderText="Detalle" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("idReporte") %>' CssClass="btn btn-primary" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# Eval("idReporte") %>' CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div>
        <h3>Agregar Nuevo Reporte</h3>
        <div class="form-group">
            <label for="txtIdReporte">ID Reporte:</label>
            <asp:TextBox ID="txtIdReporte" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtDetalle">Detalle:</label>
            <asp:TextBox ID="txtDetalle" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="50" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtFecha">Fecha:</label>
            <asp:TextBox ID="txtFecha" CssClass="form-control" runat="server"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="ceFecha" runat="server" TargetControlID="txtFecha" Format="yyyy-MM-dd"></ajaxToolkit:CalendarExtender>
        </div>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
    </div>

    <style>
        .form-group {
            margin-bottom: 15px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .btn {
            padding: 10px 15px;
            font-size: 14px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        .btn-danger {
            background-color: #dc3545;
            color: white;
        }

        .btn-danger:hover {
            background-color: #c82333;
        }

        .btn-success {
            background-color: #28a745;
            color: white;
        }

        .btn-success:hover {
            background-color: #218838;
        }

        h2, h3 {
            color: #343a40;
        }

        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }

        .table {
            width: 100%;
            margin-bottom: 1rem;
            background-color: white;
        }

        .table th,
        .table td {
            padding: 0.75rem;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
        }

        .table thead th {
            vertical-align: bottom;
            border-bottom: 2px solid #dee2e6;
        }

        .table tbody + tbody {
            border-top: 2px solid #dee2e6;
        }

        .table-bordered {
            border: 1px solid #dee2e6;
        }

        .table-bordered th,
        .table-bordered td {
            border: 1px solid #dee2e6;
        }

        .table-bordered thead th,
        .table-bordered thead td {
            border-bottom-width: 2px;
        }

        .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0, 0, 0, 0.05);
        }
    </style>
</asp:Content>
