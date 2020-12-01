<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CCAV.aspx.cs" Inherits="IAIWebApp.CCAV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://fonts.googleapis.com/css?family=Arvo" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="assets/InnerCSS/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>

    <style>
        .img {
            position: fixed;
            top: 50%;
            left: 50%;
            /* bring your own prefixes */
            transform: translate(-50%, -50%);
        }

        .lt {
            font-size: 15px;
            font-weight: bold;
            margin-left: -50px;
        }

        .lh {
            background-color: black;
            text-align: center;
        }

        table {
            visibility: hidden
        }
    </style>
    <script>
        window.onload = function () {
            var d = new Date().getTime();
            document.getElementById("tid").value = d;
        };
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#nonseamless").submit();
        });
    </script>
</head>
<body>
    <div class="lh">
        <img src="assets/OuterCSS/img/IAmInterviewedLogo.png" style="height: 50px;">
    </div>
    <div class="img">
        <img src="assets/InnerCSS/global/img/ajax-loading.gif"><br>
        <br>
        <br>
        <span class="lt">Please wait, Page Loding ...</span>

    </div>

    <form id="nonseamless" method="post" name="customerData" action="ccavRequestHandler.aspx">
        <table width="40%" height="100" border='1' align="center">
            <tr>
                <td>Parameter Name:</td>
                <td>Parameter Value:</td>
            </tr>
            <tr>
                <td colspan="2">Compulsory information</td>
            </tr>
            <tr>
                <td>TID	:</td>
                <td>
                    <input type="text" name="tid" id="tid" readonly /></td>
            </tr>
            <tr>
                <td>Merchant Id</td>
                <td>
                    <input type="text" name="merchant_id" id="merchant_id" value="72840" /></td>
            </tr>
            <tr>
                <td>Order Id</td>
                <td>
                    <input type="text" name="order_id" runat="server" id="order_id" /></td>
            </tr>
            <tr>
                <td>Amount</td>
                <td>
                    <input type="text" name="amount" value="1490.00" /></td>
            </tr>
            <tr>
                <td>Currency</td>
                <td>
                    <input type="text" name="currency" value="INR" /></td>
            </tr>
            <tr>
                <td>Redirect URL</td>
                <td>
                    <input type="text" name="redirect_url" value="https://iaminterviewed.com/ccavResponse/Index" /></td>
            </tr>
            <tr>
                <td>Cancel URL</td>
                <td>
                    <input type="text" name="cancel_url" value="https://iaminterviewed.com/ccavResponse/Index" /></td>
            </tr>
            <tr>
                <td colspan="2">Billing information(optional):</td>
            </tr>
            <tr>
                <td>Billing Name</td>
                <td>
                    <input type="text" name="billing_name" runat="server" id="billing_name" /></td>
            </tr>
            <tr>
                <td>Billing Address:</td>
                <td>
                    <input type="text" name="billing_address" value="Paymenttowardsiaminterviewed.com" /></td>
            </tr>
            <tr>
                <td>Billing City:</td>
                <td>
                    <input type="text" name="billing_city" value="Bangalore" /></td>
            </tr>
            <tr>
                <td>Billing State:</td>
                <td>
                    <input type="text" name="billing_state" value="Karnataka" /></td>
            </tr>
            <tr>
                <td>Billing Zip:</td>
                <td>
                    <input type="text" name="billing_zip" value="560043" /></td>
            </tr>
            <tr>
                <td>Billing Country:</td>
                <td>
                    <input type="text" name="billing_country" value="India" /></td>
            </tr>
            <tr>
                <td>Billing Tel:</td>
                <td>
                    <input type="text" name="billing_tel" runat="server" id="billing_tel" /></td>
            </tr>
            <tr>
                <td>Billing Email:</td>
                <td>
                    <input type="text" name="billing_email" runat="server" id="billing_email" /></td>
            </tr>
            <tr>
                <td colspan="2">Shipping information(optional):</td>
            </tr>
            <tr>
                <td>Shipping Name</td>
                <td>
                    <input type="text" name="delivery_name" value="IAmInterviewed" /></td>
            </tr>
            <tr>
                <td>Shipping Address:</td>
                <td>
                    <input type="text" name="delivery_address" value="Paymenttowardsiaminterviewed.com" /></td>
            </tr>
            <tr>
                <td>shipping City:</td>
                <td>
                    <input type="text" name="delivery_city" value="Bangalore" /></td>
            </tr>
            <tr>
                <td>shipping State:</td>
                <td>
                    <input type="text" name="delivery_state" value="Karnataka" /></td>
            </tr>
            <tr>
                <td>shipping Zip:</td>
                <td>
                    <input type="text" name="delivery_zip" value="560043" /></td>
            </tr>
            <tr>
                <td>shipping Country:</td>
                <td>
                    <input type="text" name="delivery_country" value="India" /></td>
            </tr>
            <tr>
                <td>Shipping Tel:</td>
                <td>
                    <input type="text" name="delivery_tel" value="8050065578" /></td>
            </tr>
            <tr>
                <td>Merchant Param1</td>
                <td>
                    <input type="text" name="merchant_param1" value="additionalInfo." /></td>
            </tr>
            <tr>
                <td>Merchant Param2</td>
                <td>
                    <input type="text" name="merchant_param2" value="additionalInfo." /></td>
            </tr>
            <tr>
                <td>Merchant Param3</td>
                <td>
                    <input type="text" name="merchant_param3" value="additionalInfo." /></td>
            </tr>
            <tr>
                <td>Merchant Param4</td>
                <td>
                    <input type="text" name="merchant_param4" value="additionalInfo." /></td>
            </tr>
            <tr>
                <td>Merchant Param5</td>
                <td>
                    <input type="text" name="merchant_param5" value="additional Info." /></td>
            </tr>
            <tr>
                <td>Promo Code</td>
                <td>
                    <input type="text" name="promo_code" /></td>
            </tr>
            <tr>
                <td>Customer Id:</td>
                <td>
                    <input type="text" name="customer_identifier" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input type="submit" value="Checkout" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
