<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="GestionEscuela.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <main aria-labelledby="title" class="container mt-5">
        <h2 id="title"><%: Title %></h2>
        <p class="text-danger">
            <asp:Literal runat="server" ID="ErrorMessage" />
        </p>
        <h4>Crear una nueva cuenta</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" HeaderText="Por favor corrija los siguientes errores:" />
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 col-form-label">Correo Electrónico</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger" ErrorMessage="El campo correo electrónico es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 col-form-label">Contraseña</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="El campo contraseña es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 col-form-label">Confirmar Contraseña</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="El campo confirmar contraseña es obligatorio." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="La contraseña y la confirmación de la contraseña no coinciden." />
            </div>
        </div>
        <div class="form-group row"><br /><br />
            <asp:Label runat="server" AssociatedControlID="Nombre" CssClass="col-md-2 col-form-label">Nombre</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Nombre" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Nombre" CssClass="text-danger" ErrorMessage="El campo nombre es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="ApPaterno" CssClass="col-md-2 col-form-label">Apellido Paterno</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ApPaterno" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ApPaterno" CssClass="text-danger" ErrorMessage="El campo apellido paterno es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="ApMaterno" CssClass="col-md-2 col-form-label">Apellido Materno</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ApMaterno" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ApMaterno" CssClass="text-danger" ErrorMessage="El campo apellido materno es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="FechaNac" CssClass="col-md-2 col-form-label">Fecha de Nacimiento</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FechaNac" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FechaNac" CssClass="text-danger" ErrorMessage="El campo fecha de nacimiento es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="Direccion" CssClass="col-md-2 col-form-label">Dirección</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Direccion" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Direccion" CssClass="text-danger" ErrorMessage="El campo dirección es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="Telefono" CssClass="col-md-2 col-form-label">Teléfono</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Telefono" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Telefono" CssClass="text-danger" ErrorMessage="El campo teléfono es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="ddlRoles" CssClass="col-md-2 col-form-label">Rol</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ddlRoles" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRoles" CssClass="text-danger" InitialValue="0" ErrorMessage="El campo rol es obligatorio." />
            </div>
        </div>
        <div class="form-group row">
            <div class="offset-md-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Registrar" CssClass="btn btn-primary" />
            </div>
        </div>
    </main>
</asp:Content>
