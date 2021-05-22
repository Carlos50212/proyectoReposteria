<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="LaLombriz.Formularios.Pago" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>

    <link href="../Estilos/Pago.css" rel="stylesheet" />

    <title>Pago - LaLombriz</title>
</head>
<body>
    <form action="https://localhost:44393/api/myApi/Pago" method="post">
      <script
        src="https://www.mercadopago.com.mx/integrations/v1/web-tokenize-checkout.js"
        data-public-key="TEST-0f250235-1165-43d7-be1c-4c8078ecd7e8"
        data-transaction-amount="10.00">
      </script>
         <input type="hidden" name="transactionAmount" id="transactionAmount" value="5" />
         <input type="hidden" name="correo" id="correoUser" value="carlosruiz50212@gmail.com" />
         <input type="hidden" name="description" id="description" value="Prueba pago"/>
    </form>
</body>
</html>
