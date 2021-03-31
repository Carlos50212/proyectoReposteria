<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LaLombriz.Formularios.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Iniciar Sesión - LaLombriz</title>
    <!--Bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!--SweetAlert-->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <link href="../Estilos/LoginStyles.css" rel="stylesheet" />

</head>
<body>
    <form id="login" runat="server">
        <video autoplay="autoplay" muted="muted" loop="loop" id="loginVideo">
            <source src="../Recursos/login/login.mp4" type="video/mp4"/>
        </video>
        <div id="login-shadow">
            <div id="login-container">
                <div id="header-login">
                    <h5>Iniciar sesión</h5>
                </div>
                <div id="content-login">
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtCorreoLogin" class="form-control" placeholder="name@example.com"></asp:TextBox>
                        <label for="txtCorreoLogin">Correo electrónico</label>
                        <span id="validador2"></span>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtPasswdLogin" TextMode="Password" class="form-control" placeholder="contraseña"></asp:TextBox>
                        <label for="txtPasswdLogin">Contraseña</label>
                    </div>
                    <div id="options-login">
                        <p>
                            <button type="button" style="border: none; color: #0d6efd; background-color: transparent;" data-bs-toggle="modal" data-bs-target="#recoverPass" data-bs-dismiss="modal">Olvidé mi contraseña</button>
                        </p>
                        <p>
                            <label>¿No tiene una cuenta?</label>
                            <asp:Button runat="server" style="border: none; color: #0d6efd; background-color: transparent;" id="btnNewAccount" OnClick="btnCreateOnClick" Text="Crear una"/>
                        </p>
                    </div>
                </div>
                <div id="footer-login">
                    <asp:Button runat="server" ID="btnIngresar" Text="Ingresar" class="btn btn-primary"/>
                </div>
            </div>
        </div>

        <!--Modal Recuperar Contraseña-->
        <div class="modal fade" id="recoverPass" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="recoverPassTitle">Recuperar contraseña</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-floating mb-3">
                            <asp:TextBox runat="server" ID="txtRecoverPass" onkeypress="return CorreoTres(event);" class="form-control" placeholder="name@example.com" ></asp:TextBox>
                            <label for="txtRecoverPass">Correo electrónico</label>
                            <span id="validador3"></span>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <asp:Button runat="server" ID="btnRecover" Text="Recuperar" class="btn btn-primary"/>
                    </div>
                </div>
            </div>
        </div>

    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js" ></script>

    <script src="../JS/LoginJS.js"></script>

</body>
</html>
