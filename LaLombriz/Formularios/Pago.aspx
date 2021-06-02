<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="LaLombriz.Formularios.Pago" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--SweetAlert-->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <!--API-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    

    <!--Bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <link href="/Estilos/Pago.css" rel="stylesheet" />

    <title>Pago - LaLombriz</title>
</head>
<body>
    <form runat="server">
        <div runat="server" id="detailOrder" class="detailCartStyle">
            <div class='cartProductsContainer'>  
                <div id="title-cart-container">
                    <h4> <asp:Literal runat="server" ID="mensajeInicio"> </asp:Literal> </h4>
                </div>
                <asp:Literal runat="server" ID="tbProductsOrder"></asp:Literal>
             </div>
         </div>
    </form>
    <div id="sell-container">
        <div id="sellOption">
            <form action="https://lalombriz.azurewebsites.net/api/myApi/Pago" method="post">
                <asp:Literal runat="server" ID="pagoscript"></asp:Literal>
                <input runat="server" type="hidden" name="transactionAmount" id="transactionAmount" value="100" />
                <input runat="server" type="hidden" name="correo" id="correo" value="" />
                <input type="hidden" name="description" id="description" value="Compra LaLombriz" />
            </form>
        </div>
    </div>
    
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js" ></script>
</body>
</html>
