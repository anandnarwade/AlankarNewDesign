<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="AlankarNewDesign.Invoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice</title>
  <%--  <link href="vendor/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
    <style>
       h3{
           color:#2121b7;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;">
    <div style="text-align:center; border:1px solid black; width:100%;">
        <table style="width:100%; font-size:10px;">
            <tr>
                <td style="width: 35%; float:left; vertical-align:top; margin-top:-1px; padding-top:1px; ">
                    <div
                        style="text-align: left; padding-top: 1px; padding-left: 2px; width: 30%; height:30%;">
                        <img src="img/StripBlue.png" style="width: 112px; height: 25px;" />
                        <div style="padding-top:10px;">
                             <table style="padding-left:20px;">
                        <tr >
                            <td style="font-size:14px; float:left;">Invoice No</td>
                            <td>
                                :
                            </td>
                            <td style="float:left;">

                            </td>
                        </tr>
                         <tr >
                            <td  style="float:left;">Issuing Time</td>
                            <td>
                                :
                            </td>
                            <td  style="float:left;">

                            </td>
                        </tr>
                         <tr  >
                            <td  style="float:left;">Removing Time</td>
                            <td>
                                :
                            </td>
                            <td  style="float:left;">

                            </td>
                        </tr>
                         <tr >
                            <td   style="float:left;">Transportation Mode</td>
                            <td>
                                :
                            </td>
                            <td  style="float:left;">

                            </td>
                        </tr>
                    </table>
                        </div>
                    </div>


                </td>
                <td style="width:30%; text-align: left;">
                    <h3>TAX INVOICE</h3>
                </td>
                <td style="width:35%; float:right; margin-top:-155px; padding-right:2px;">
                      <div style="text-align:right; padding-top:1px; vertical-align:top; padding-right:1px;">
                <img src="img/ALANKARlogoblue.png" style="width:70px; height:107px;" />
            </div>
                </td>
            </tr>
        </table>
        <hr />
        <table style="width:100%; font-size:12px;">
            <tr>
                <td style="width:50%;">

                </td>
            </tr>
        </table>


    </div>
    </form>
</body>
</html>
