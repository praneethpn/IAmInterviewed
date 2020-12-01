<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ccavRequestHandler.aspx.cs" Inherits="IAIWebApp.ccavRequestHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="assets/InnerCSS/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#nonseamless").submit();
            });
       </script>
</head>
<body>
    <form id="nonseamless" method="post" name="redirect" action="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction"> 
        <input type="hidden" id="encRequest" name="encRequest" runat="server"/>
        <input type="hidden" name="access_code" id="access_code" runat="server"/>
    </form>
</body>
</html>
