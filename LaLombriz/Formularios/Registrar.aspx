<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrar.aspx.cs" Inherits="LaLombriz.Formularios.CrearCuenta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Crear cuenta - LaLombriz</title>

    <!--Bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!--SweetAlert-->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <link href="/Estilos/RegistrarStyles.css" rel="stylesheet" />
    <script src="/JS/LoginJS.js"></script>
</head>
<body>
    <form id="newAccount" runat="server">
        <div id="fondoRegistrar">
            <div id="create-container">
                <div id="header-create">
                    <h5>Registrarse</h5>
                </div>
                <div id="content-create">
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtUserName" onkeypress="return FiltroLetras(event);" class="form-control" placeholder="Nombre de usuario"></asp:TextBox>
                        <label for="txtUserName">Nombre de usuario</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtUserEmail" onkeypress="return Correo(event);" class="form-control" placeholder="name@example.com"></asp:TextBox>
                        <label for="txtUserEmail">Correo electrónico</label>
                        <span id="validador"></span>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtUserPhone" onkeypress="return FiltroNumeros(event);" class="form-control" placeholder="Número telefónico"></asp:TextBox>
                        <label for="txtUserPhone">Número telefónico</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtUserPasswd" TextMode="Password" class="form-control" placeholder="Contraseña"></asp:TextBox>
                        <label for="txtUserPasswd">Contraseña</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtConfirmPasswd" TextMode="Password" class="form-control" placeholder="Contraseña"></asp:TextBox>
                        <label for="txtConfirmPasswd">Confirmar contraseña</label>
                    </div>
                </div>
                <div id="footer-create">
                    <asp:Button runat="server" ID="btnBack" Text="Regresar" class="btn btn-secondary" OnClick="btnBackOnClick"/>
                    <asp:Button runat="server" ID="btnCreate" Text="Crear" class="btn btn-primary" OnClick="btnCrearOnClick"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
