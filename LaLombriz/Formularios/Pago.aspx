<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="LaLombriz.Formularios.Pago" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--SweetAlert-->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <!--API-->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>

    <!--Bootstrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <link href="../Estilos/Pago.css" rel="stylesheet" />

    <title>Pago - LaLombriz</title>
</head>
<body>
    <form runat="server">
        <div runat="server" id="detailOrder" class="detailCartStyle">
            <div class='cartProductsContainer'>  
                <div id="title-cart-container">
                    <h4>Verifica tu pedido</h4>
                </div>
                <asp:Literal runat="server" ID="tbProductsOrder"></asp:Literal>
             </div>
         </div>
    </form>
    <div id="sellOption">
        <form action="https://localhost:44393/api/myApi/Pago" method="post">
            <script
                src="https://www.mercadopago.com.mx/integrations/v1/web-tokenize-checkout.js"
                data-public-key="TEST-0f250235-1165-43d7-be1c-4c8078ecd7e8"
                data-transaction-amount="100">
            </script>
            <!--Verificar el monto-->
            <input runat="server" type="hidden" name="transactionAmount" id="transactionAmount" value="" />
            <input runat="server" type="hidden" name="correo" id="correoUser" value="" />
            <input runat="server" type="hidden" name="description" id="description" value="Prueba pago" />
        </form>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js" ></script>

</body>
</html>
