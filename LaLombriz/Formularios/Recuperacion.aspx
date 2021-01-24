<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperacion.aspx.cs" Inherits="LaLombriz.Formularios.Recuperacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recuperación - LaLombriz</title>
    <!--Bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!--SweetAlert-->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <link href="../Estilos/RecoverStyles.css" rel="stylesheet" />
</head>
<body>
    <form id="recoverForm" runat="server">
        <div class="mainContainer">
            <div class="boxContainer" id="recoverPass" runat="server">
                <h2>Recupera tu contraseña</h2>
                <div class="form-floating mb-3" style="margin-top: 50px;">
                    <asp:TextBox runat="server" ID="txtCorreo" class="form-control" placeholder="name@example.com" ReadOnly="true"></asp:TextBox>
                    <label for="txtCorreoI">Correo electrónico</label>
                </div>
                <div class="form-floating mb-3">
                    <asp:TextBox runat="server"  ID="txtNewContrasenia" TextMode="Password" class="form-control" placeholder="contraseña"></asp:TextBox>
                    <label for="txtNewContrasenia">Nueva contraseña</label>
                </div>
                <div class="form-floating mb-3">
                    <asp:TextBox runat="server" ID="txtConfirmPass" TextMode="Password"  class="form-control" placeholder="contraseña"></asp:TextBox>
                    <label for="txtConfirmPass">Repite tu contraseña</label>
                    
                </div>
                <div id="recoverContainer">
                    <asp:Button runat="server" ID="btnChangePass" Text="Confirmar"  OnClick="btnChangePassOnClick" class="btn btn-primary"/>
                </div>
            </div>
            <div id="wrongUrl" runat="server" style="display:none;">
                <h1>Ops...</h1>
                <img id="imgWrongUrl" src="../Recursos/stop.png" alt="wrong url"/>
                <h2>Url incorrecta</h2>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js" ></script>
</body>
</html>
