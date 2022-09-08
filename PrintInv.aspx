<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="PrintInv.aspx.cs" Inherits="AlankarNewDesign.PrintInv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type = "text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800,overflow:auto;');
            printWindow.document.write('<html><head><title> <%= lblInvNO.Text %> </title>');
            printWindow.document.write('<link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>');
            printWindow.document.write('<style> .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th { padding: 0px;padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px; line-height: 0.429; vertical-align: top;  border-top: 1px solid #120505; line-height:15px; }</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <style>
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
    padding: 0px;
        padding-top: 0px;
        padding-right: 0px;
        padding-bottom: 0px;
        padding-left: 0px;
    line-height: 0.429;
    vertical-align: top;
    border-top: 1px solid #120505;
}
        .logocol {
            color:#2b17be;
        }
    </style>

    <%-- <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
</script>--%>









</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <div class="col-sm-4">
             <h4  class="logo">INVOICE</h4>
        </div>
        <div class="col-sm-4 col-sm-offset-9" style="padding:5px; float:left;">
            <a href="all_invoice.aspx" class="btn btn-xs btn-primary"><i class="glyphicon glyphicon-list"></i> Back to List</a>
            <a href="create_invoice.aspx" class="btn btn-xs btn-primary" ><i class="glyphicon glyphicon-pencil"></i> Create Invoice</a>
        </div>




       <%-- <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblPartyCode" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblccId" runat="server" Visible="false" Text=""></asp:Label>

        </center>--%>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px;">
        <asp:Panel ID="pnlContents" runat="server">
         <table class="table" style="border:1px solid black; ">
                <!--1st Tr-->
                <tr>
                    <td>
                        <div class="row" style="text-align:start;">
                            <div class="col-sm-8" style="vertical-align: top; text-align: left; padding-bottom: 3px; padding-left: 17px; padding-top: 2px; width:70%;">
                                <img src="img/StripBlue.png"  width="112px" height="19px;" />
                                <%--<img src="GST%20Invoice%20Template4_files/GST%20Invoice%20Template4_files/StripBlue.png" width="112px" height="19px;" />--%>
                              <div style="font-size: 24px; font-family: Calibri; margin-top:30px; margin-left:30px; float:right;"><b style="margin-right:80px; color:#2b17be;"> <span style="color:#2b17be;">TAX INVOICE</span>  </b></div>
                                <div style="padding-top:40px; padding-left:40px;">
                                    <table>

                                        <tr style="padding: 5px;">
                                            <td style="text-align: left; padding-right: 8px; height: 25px; ">
                                                <b style="font-size: 18px; ">Invoice No.  </b>
                                            </td>
                                            <td style="height:25px;">
                                                <b>:</b>
                                            </td>
                                            <td style="height: 25px; padding-left: 10px; float: left; ">
                                                <div style="padding-top:2px;">
                                                     <asp:Label ID="lblInvNO" runat="server" Font-Bold="true" Font-Size="19px"></asp:Label>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr style="padding: 5px;">
                                            <td style="text-align: left; padding-right: 8px; height: 25px;">
                                                <div style="font-size: 12px;"><strong>Date</strong> </div>
                                            </td>
                                            <td  style="height:25px;">
                                                <b>:</b>
                                            </td>
                                            <td style="height: 25px; padding-left: 10px; float: left;">
                                                <div style="padding-top:6px;">
                                                    <asp:Label ID="lblInvDate" runat="server" Font-Size="12px" Font-Bold="True" Text=""></asp:Label>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr style="padding: 5px;">
                                            <td style="text-align: left; padding-right: 8px; height: 25px;">
                                                <div style="font-size: 12px;"><b>Issuing Time  </b></div>
                                            </td>
                                            <td  style="height:25px;">
                                                <b>:</b>
                                            </td>
                                            <td style="height: 25px; padding-left: 10px; float: left; ">
                                                <div style="padding-top:6px;">
                                                    <asp:Label ID="lblIssueTime" Font-Bold="true" runat="server" Font-Size="12px" Text=""></asp:Label>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr style="padding: 5px;">
                                            <td style="text-align: left; height: 15px; padding-right: 8px;">
                                                <div style="font-size: 12px;"><b>Removing Time  </b></div>
                                            </td>
                                            <td  style="height:25px;">
                                                <b>:</b>
                                            </td>
                                            <td style="height: 25px; padding-left: 10px; float: left;">
                                                <div style="padding-top:6px;">
                                                     <asp:Label ID="lblRemovingTime" Font-Bold="true" runat="server" Font-Size="12px" Text=""></asp:Label>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr style="padding: 5px; margin-top: 5px;">
                                            <td style="text-align: left; height: 25px; padding-right: 8px;">
                                                <div style="font-size: 12px;"><b>Transportation Mode </b></div>
                                            </td>
                                            <td  style="height:25px;">
                                                <b>:</b>
                                            </td>
                                            <td style="height: 25px; padding-left: 10px; float: left;">
                                                <div style="padding-top:6px;">
                                                    <asp:Label ID="lblTrasMode" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:Label>
                                                </div>

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="col-sm-2" style="float: right; padding-top: 2px; padding-right: 17px; width:10%; vertical-align:top;">
                                <img src="img/ALANKARlogoblue.png" style="float: right;" width="100px" height="107px"  />
                                <%--<img src="GST%20Invoice%20Template4_files/GST%20Invoice%20Template3_1061_image002.png" style="float: right;" width="100px" height="107px" />--%>
                            </div>
                        </div>

                    </td>

                </tr>

                <!--2nd Tr-->

                <tr>
                <td style="border-top:1px solid white;padding-left:40px;">
                    <div class="row" style="margin-top:10px; font-size:12px;">
                        <table style="border:1px solid black;" >
                            <tr>
                                <td style="border-right:1px solid black;" >
                                     <div class="form-check mb-2 mr-sm-2 mb-sm-0" style=" font-size:12px; padding-left:20px; padding-right:20px;">


                                        <label class="form-check-label" style="color:blue;">
                                            <input class="form-check-input" type="checkbox">
                                            Original for Receipent

                                        </label>

                                    &nbsp;&nbsp;</div>
                                </td>
                                <td style="padding-left:20px; border-right:1px solid black; padding-right:20px;">
                                       <div class="form-check mb-2 mr-sm-2 mb-sm-0" style=" font-size:12px;">
                                        <label class="form-check-label" style="color:red;">
                                            <input class="form-check-input" type="checkbox">
                                            Duplicate for Transporter

                                        </label>

                                    &nbsp;&nbsp;</div>
                                </td>
                                <td style="padding-left:20px; border-right:1px solid black; padding-right:20px;">
                                      <div class="form-check mb-2 mr-sm-2 mb-sm-0" style=" font-size:12px;">
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Triplicate for Supplier

                                        </label>

                                    &nbsp;&nbsp;</div>
                                </td>
                                <td style="padding-left:20px; padding-right:20px;">
                                       <div class="form-check mb-2 mr-sm-2 mb-sm-0" style=" font-size:12px;">
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Extra Copy

                                        </label>

                                    &nbsp;&nbsp;</div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>

                <tr>

                    <td style="border-top:1px solid white;">
                        <div class="row" style="margin-top:20px;">
                            <div class="col-sm-12"  style="padding-left:40px; padding-right:40px;">
                                <table style="border: 1px solid black; width: 100%; height:130px;">
                                    <tr >
                                        <td style="border-right: 1px solid Grey; width:55%;">
                                            <table style="padding-bottom: 10px;">
                                                <tr style="padding-top: 5px;">
                                                    <td>
                                                        <div class="row" style="padding-top: 0px; line-break: auto; line-height: 10px; padding-left: 30px; padding-right: 20px;">
                                                            <div style="font-size: 14px; padding-top: 5px;">Details of Receiver/Billed To :</div>
                                                            <div style="font-size: 12px; line-height: 15px; padding-top: 10px;">
                                                                <asp:Label ID="lblAddress" runat="server" Font-Size="14px" Font-Bold="true" Text=""></asp:Label> <br />
                                                                  <asp:Label ID="lblSlAdd" runat="server" Font-Size="12px" Width="200px" Text=""> </asp:Label>
                                                            </div>
                                                         <%--   <div style="font-size: 12px; padding-top: 5px;">

                                                            </div>--%>
                                                            <div style="font-size: 12px; line-height: 15px; padding-top:1px;">
                                                                  <asp:Label ID="lblPin" Font-Bold="true"  runat="server" Font-Size="14px" Text=""> </asp:Label><br />
                                                            </div>

                                                            <b style="font-size: 10px;"></b>
                                                            <br />
                                                            <div style="font-size: 14px; padding-bottom: 10px;">
                                                                GST Tin No. :
                                                                <asp:Label ID="lblGSTNO" runat="server" Font-Bold="true" Font-Size="14px"  Text=""></asp:Label>
                                                            </div>
                                                            <div style="font-size: 10px;"></div>
                                                        </div>




                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>

                                        </td>


                                        <td style="width:45%;">
                                            <table style="font-size: 12px; width: 100%; margin-top:-25px; ">
                                                <tr style="border-bottom: 1px solid grey; padding-bottom: 10px; height: 59px; padding-top:20px;">
                                                    <td style="width:33%; padding-top:20px; line-height: 15px; padding-left:5px; padding-right:5px; ">
                                                        <%--<div class="row" style="padding: 10px;  line-height: 0px; padding-left: 20px; width:75px;"></div>--%>
                                                        <b style="font-size: 13px;">Vendor Code </b>
                                                    </td>
                                                    <td style="padding-top:20px; width:2%;">
                                                        <b>:</b>
                                                    </td>
                                                    <td style=" width:65%; line-height: 15px; padding-left:5px; padding-right:5px; padding-top:20px;">
                                                        <div class="row" style="padding: 10px; line-break: auto; line-height: 0px; float:left;">
                                                            <asp:Label ID="lblVenderCode" runat="server" Font-Size="13px" Font-Bold="true"  Text=""></asp:Label>
                                                        </div>

                                                    </td>

                                                </tr>
                                                <tr style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid Grey; margin-bottom: 10px; height: 50px;">
                                                    <td style="width:33%; line-height: 15px; padding-left:5px; padding-right:15px;">
                                                      <%--  <div class="row" style="padding: 10px;  line-height: 5px; "></div>--%>

                                                        <b style="font-size: 13px;">P. O. No.</b>
                                                    </td>
                                                    <td style=" width:2%;">
                                                        <b>:</b>
                                                    </td>
                                                    <td style=" width:65%; line-height: 15px; padding-left:5px; padding-right:5px;">
                                                        <div class="row" style="line-height: 15px; padding-left:10px; padding-right:5px; float:left; ">
                                                             <asp:Label ID="lblPoNO" runat="server" Font-Size="13px" Text=""></asp:Label>
                                                        </div>

                                                    </td>

                                                </tr>
                                                <tr style="padding-top: 5px; height: 30px;">
                                                    <td style="width: 33%;">
                                                        <div class="row" style="padding: 10px; line-break: auto; line-height: 5px; padding-left: 20px;"><b style="font-size: 13px;">P.O. Date</b></div>
                                                    </td>
                                                    <td style="width:2%;">
                                                        <b>:</b>
                                                    </td>
                                                    <td style="padding-left: 5px; padding-right: 5px; width:65%; ">
                                                        <div class="row" style="line-height: 15px; padding-left:10px; padding-right:5px; float:left; ">
                                                             <asp:Label ID="lblPodate" runat="server" Font-Size="13px"  Text=""></asp:Label>
                                                        </div>

                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>

                                       <%-- <td></td>--%>


                                    </tr>
                                </table>
                            </div>

                            <%--<div  style="font-size: 12px; float:right; padding-left:20px; line-height:5px; width:200px;">
                                <div class="row">




                                </div>
                            </div>--%>
                        </div>
                    </td>
                </tr>

             <!--3rd TR-->

             <tr>
                 <td  style="border-top:1px solid white;">
                     <div class="row" style="padding-left:40px; padding-right:40px; padding-top:20px;">

                         <table class="table" style="border: 1px solid black; height:270px;">

                                <tr style="height:30px;">
                                    <td style="padding: 10px; width: 50%; border-right: 1px solid grey; border-bottom: 1px solid grey;">
                                        <b style="font-size: 12px;">Name of the Product
                                        </b>
                                    </td>
                                    <td style="padding: 10px; width: 15%;  text-align:center; border-right: 1px solid grey; border-bottom: 1px solid grey;">
                                        <b style="font-size: 12px;">HSN/SAC Code
                                        </b>
                                    </td>
                                    <td style="padding: 10px; width: 5%;  text-align:center; border-right: 1px solid grey; border-bottom: 1px solid grey;">
                                        <b style="font-size: 12px;">QTY.
                                        </b>
                                    </td>
                                    <td style="padding: 10px; width: 5%;  text-align:center; border-right: 1px solid grey; border-bottom: 1px solid grey;">
                                        <b style="font-size: 12px;">Unit
                                        </b>
                                    </td>
                                    <td style="padding: 10px; width: 13%; text-align:center; border-right: 1px solid grey; border-bottom: 1px solid grey;">
                                        <b style="font-size: 12px;">Rate
                                        </b>
                                    </td>
                                    <td style="padding: 10px; width: 12%; text-align:center; border-right: 1px solid grey; border-bottom: 1px solid grey;">
                                        <b style="font-size: 12px;">Value
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 0px; border-right: 1px solid grey;">



                                        <table style="font-size: 12px;">
                                            <tr>
                                                <td style="width: 100px; padding-left: 11px; height: 19px; padding-top:20px;">
                                                    <b style="font-size:14px;">Item Code</b>
                                                </td>
                                                <td style="padding-top:20px;">:
                                                </td>
                                                <td style="padding-left:5px; padding-right:5px; padding-top:20px;">
                                                    <asp:Label ID="lblitemCode" runat="server" Font-Size="14px"  Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; padding-left: 10px; height: 19px; padding-top:20px;">
                                                    <b style="font-size:14px;">Drawing No.</b>
                                                </td>
                                                <td style="padding-top:20px;">:
                                                </td>
                                                <td style="padding-left:5px; padding-top:20px; padding-right:5px;"><asp:Label ID="lblDrgNO" runat="server" Font-Size="14px"  Text=""></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="width: 100px; padding-left: 10px; height: 19px; vertical-align:sub; padding-top:20px;">
                                                    <b style="font-size:14px;">Description </b>
                                                </td>
                                                <td style="vertical-align:sub; padding-top:20px;">:
                                                </td>
                                                <td>
                                                    <div style="line-height: 15px; padding-left:5px; padding-right:5px; padding-top:20px;">
                                                        <asp:Label ID="lblDescription" runat="server" Font-Bold="true" Font-Size="14px" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td style="width: 100px; padding-left: 10px; height: 19px; vertical-align:sub; padding-top:20px;">
                                                    <b style="font-size:14px;">Note </b>
                                                </td>
                                                <td style="vertical-align:sub; padding-top:20px;">:
                                                </td>
                                                <td>
                                                    <div style="line-height: 15px; padding-left:5px; padding-right:5px; padding-top:20px;">
                                                        <asp:Label ID="lblNote" runat="server" Font-Size="14px" Text=""></asp:Label>
                                                    </div>
                                                </td>

                                            </tr>



                                        </table>

                                    </td>
                                    <td style="border-right: 1px solid grey;">
                                        <div style="padding: 10px;  text-align:center;">
                                            <asp:Label ID="lblHSN_Code" runat="server" Font-Size="14px" Font-Bold="true" Text=""></asp:Label>
                                        </div>

                                    </td>
                                    <td style="border-right: 1px solid grey;">
                                        <div style="padding: 10px;  text-align:center;">
                                            <asp:Label ID="lblQty" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                        </div>

                                    </td>
                                    <td style="border-right: 1px solid grey;">
                                        <div style="padding: 10px;  text-align:center;">
                                            <asp:Label ID="lblUnit" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                        </div>

                                    </td>
                                    <td style="border-right: 1px solid grey; text-align:center;">
                                        <div style="padding: 10px;  text-align:center;">
                                            <asp:Label ID="lblRate" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                        </div>

                                    </td>
                                    <td style="border-right: 1px solid grey; text-align:center;">
                                        <div style="padding: 10px;  text-align:center;">
                                            <asp:Label ID="lblAmount" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                        </div>

                                    </td>
                                </tr>
                            </table>

                     </div>






                 </td>
             </tr>


             <!--3rd TR end-->


             <!---4th tr-->


             <tr>
                 <td style="border-top:1px solid white;">
                     <div class="row" style="padding-left:40px; padding-right:40px; padding-top:10px;">

                         <table class="table" style="border:1px solid white;  margin-left:15px;">
                                <tr>
                                    <td style="border-top:1px solid white;  padding-right:30px; width:45%;">
                                        <div class="row" style="border:1px solid black; padding-left:10px; padding-right:20px; ">
                                           <h6><b>Terms & Conditions:</b></h6>
                                            <div class="row" style=" padding-right:10px; padding-left: 10px; font-size: 12px; line-height: 25px; ">
                                                1) I / We hereby certify that my/our registration certificate under the Goods and Service Taxt Act, 2017 is in force on the date on which the sale of the goods specified in this "Tax Invoice" is made by me/us and that the transaction of supply coverd.
                                            </div>
                                             <div class="row" style=" padding-right:10px; padding-left:10px; font-size:12px; line-height:25px;">
                                            2) Subject to Aurangabad Juridiction.
                                        </div>
                                        </div>



                                    </td>
                                    <td style="border:1px solid white; width:50%;">
                                        <div style=" padding-top:3px; margin-right:12px;">
                                            <table class="table" style="border-top:1px solid white; border-right:1px solid grey; border-bottom:1px solid grey;">
                                            <tr style="border-left:1px solid black; border-right:1px solid black;">
                                                <td style="border: 1px solid grey; border-left:1px solid grey; border-right: 1px solid grey; border-top:1px solid white; width:0.2%;"></td>
                                                <td style=" height: 20px; border-right: 1px solid grey; padding: 5px; font-size: 12px; border-left: 1px solid grey; width:55%;">
                                                    <div style="line-height: 15px;"><b>Add Packing & Forwarding %</b>		</div>

                                                </td>




                                                <td style=" height: 19px; border-right: 1px solid grey; padding: 5px; width:18%;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblPackingper" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                                <td style=" height: 20px; padding: 5px; width:30%;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblPackingAmount" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                            </tr>


                                            <tr style="font-size: 11px;border-left:1px solid black; border-right:1px solid black; ">
                                                <td style="border: 1px solid white; border-right: 1px solid grey;"></td>
                                                <td style="width: 190px; height: 20px; border-right: 1px solid grey; padding: 5px; font-size: 12px; border-left: 1px solid grey;">
                                                    <div style="line-height: 15px;"><b>Add Freight </b>		</div>

                                                </td>




                                                <td style="width: 70px; height: 19px; border-right: 1px solid grey; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblFreightPer" runat="server" Visible="false" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                                <td style="width: 70px; height: 19px; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblFreightAmount" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                            </tr>



                                            <tr style="font-size: 11px;">
                                                <td style="border: 1px solid white; border-right: 1px solid grey;"></td>
                                                <td style="width: 190px; height: 20px; padding: 5px; font-size: 12px; border-left: 1px solid grey;">
                                                    <div style="line-height: 15px;"><b>Taxable Amount </b></div>

                                                </td>


                                                <td></td>

                                                <td style="width: 70px; height: 20px; border-bottom: 1px solid grey; border-left: 1px solid grey; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblTaxableAmount" runat="server" Font-Size="12px" Text="" Font-Bold="true"></asp:Label>
                                                    </div>

                                                </td>
                                            </tr>



                                            <tr style="border: 1px solid white; height: 20px;">
                                                <td></td>
                                                <td></td>
                                                <td></td>


                                            </tr>




                                            <tr style="font-size: 11px;">
                                                <td style="border: 1px solid white; border-right: 1px solid grey;"></td>
                                                <td style="width: 190px; height: 20px; padding: 5px; font-size: 12px; border-left: 1px solid grey;">
                                                    <div style="line-height: 15px;"><b>Add CGST %</b>		</div>

                                                </td>



                                                <td style="width: 70px; height: 19px; border-right: 1px solid grey; border-left: 1px solid grey; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblCGstper" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                                <td style="width: 70px; height: 20px; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblCgst" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                            </tr>


                                            <tr style="font-size: 11px;">
                                                <td style="border: 1px solid white; border-right: 1px solid grey;"></td>
                                                <td style="width: 190px; height: 20px; padding: 5px; font-size: 12px; border-left: 1px solid grey;">
                                                    <div style="line-height: 15px;"><b>Add SGST %</b>		</div>

                                                </td>




                                                <td style="width: 70px; height: 20px; border-right: 1px solid grey; border-left: 1px solid grey; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblSgstper" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                                <td style="width: 70px; height: 19px; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblSgst" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                            </tr>




                                            <tr style="font-size: 11px;">
                                                <td style="border: 1px solid white; border-right: 1px solid grey;"></td>
                                                <td style="width: 190px; height: 20px; padding: 5px; font-size: 12px; border-left: 1px solid grey;">
                                                    <div style="line-height: 15px;"><b>Add IGST %</b>		</div>

                                                </td>




                                                <td style="width: 70px; height: 19px; border-right: 1px solid grey; border-left: 1px solid grey; font-size: 12px; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblIgstPer" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                                <td style="width: 70px; height: 19px; padding: 5px;">
                                                    <div style="float: right;">
                                                        <asp:Label ID="lblIgst" runat="server" Font-Size="12px" Font-Bold="true" Text=""></asp:Label>
                                                    </div>

                                                </td>
                                            </tr>






                                                 <tr style="font-size: 11px; border-bottom:1px solid black;">
                                                <td style="border-bottom: 1px solid black; border-right: 1px solid grey;"></td>
                                                <td style="width: 190px; height: 20px; padding: 5px; font-size: 12px; border-left: 1px solid grey;">
                                                    <div style="line-height: 15px;"><b>Total Amount</b>		</div>

                                                </td>




                                                <td style="width: 70px; height: 19px; border-right: 1px solid grey; font-size: 12px; padding: 5px;">
                                                    <div style="float: right;">

                                                    </div>

                                                </td>
                                                <td style="width: 70px; height: 19px; padding: 5px;">
                                                    <div style="float: right; font-size:14px;">
                                                     &#x20B9;    <asp:Label ID="lblfinalAmount" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
                                                    </div>

                                                </td>
                                            </tr>


                                        </table>

                                        </div>

                                    </td>
                                </tr>

                            </table>



                     </div>

                     <div class="row" style="padding-top: 0px; padding-left: 40px; padding-right: 10px; padding-bottom: 10px;">
                         <div class="col-sm-12" style="font-size: 14px;">
                             <b>TOTAL AMOUNT</b> (In Words) :<asp:Label ID="lblFinalAmountinWords" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
                         </div>

                          <div class="col-sm-12" style="font-size: 12px; padding-top:10px;" id="Note" runat="server" visible ="false">
                         <%--    <b>NOTE</b> :<asp:Label ID="lblNote" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>--%>
                         </div>



                     </div>

                 </td>
             </tr>


             <!--4th tr end-->

             <!--5th tr-->

             <tr>
                 <td >

                      <table class="table" style="padding-left:30px; padding-top:10px; padding-right:20px; margin-bottom:5px; color:#2b17be;">
                          <tr>
                              <td style="height:15px; padding:10px;padding-left:25px; font-size:12px; border:1px solid white; ">
                                  <div class="row" style="padding-left:10px;">
                                      <b>GST Tin No.: 27AABCA9423B1Z2</b>
                                  </div>
                                  <div class="row" style="padding-left:10px; padding-top:10px;">
                                      <b style="margin-top:25px;">STATE CODE : 27</b>
                                  </div>
                                  <div class="row" style="padding-left:10px; padding-top:10px;">
                                  </div>
                                  <div class="row" style="padding-left: 10px; padding-top: 10px;">
                                      <b>Company&#39;s PAN : AABCA9423B </b>
                                  </div>
                                  <div class="row" style="padding-left:10px; padding-top:10px;">
                                      <b>CIN No:- U29220MH1988PTC050083 </b>
                                  </div>
                                  <div class="row" style="padding-left:10px; padding-top:10px;">
                                      <b>SSI No:-27019200229 PART II DT. 10-09-2009 </b>
                                  </div>
                              </td>
                              <td style="height:15px; padding:10px;padding-left:25px; font-size:12px; border:1px solid white; ">
                                  <div class="row" style="padding-left:10px; padding-top:10px;">
                                      <b style="padding-left:30px;">For alankar tools pvt.ltd. </b>
                                  </div>
                                  <div style="height:60px;">
                                  </div>
                                  <div class="row" style="padding-left:45px; padding-top:0px;">
                                      <b>Authorisied Signatory </b>
                                  </div>
                              </td>
                              <td style="float:right; border:1px solid white; ">
                                  <div class="row" style="padding-left:20px; float:right; margin-right:15px;">
                                      <div class="row" style="border-right:19px solid #2b17be; vertical-align:bottom;">
                                          <div class="row" style="padding-left: 5px; padding-right: 10px; padding-top: 20px; font-size: 14px; font-family: Arial; line-height: 5px;">
                                              <b style="font-size: 14px;">alankar tools pvt ltd </b>
                                              <div class="row" style="padding-left: 10px; padding-top: 10px; padding-right: 20px; font-size: 11px; font-family: Arial;">
                                                  <b>H-5/29, MIDC, Chikalthana </b>
                                              </div>
                                              <div class="row" style="padding-left: 10px; padding-top: 10px; padding-right: 20px; font-size: 11px; font-family: Arial;">
                                                  <b>Aurangabad -- 431006. </b>
                                              </div>
                                              <div class="row" style="padding-left: 10px; padding-top: 10px; padding-right: 20px; font-size: 11px; font-family: Arial;">
                                                  <b>Ph- 0240- 2954055 </b>
                                              </div>           
                                              <div class="row" style="padding-left: 10px; padding-top: 10px; padding-right: 30px; font-size: 11px; font-family: Arial;">
                                                  <b>Email-- contact@alankartools.com </b>
                                              </div>
                                              <div class="row" style="padding-left: 10px; padding-top: 10px; padding-right: 20px; font-size: 11px; font-family: Arial;">
                                                  <b>Ph- 0240- 2954055</b>
                                              </div>
                                              <div class="row" style="padding-left: 10px; padding-top: 10px; padding-right: 20px; font-size: 11px; font-family: Arial;">
                                                  <b>Web -- www.alankartools.com </b>
                                              </div>
                                          </div>
                                      </div>
                                  </div>
                              </td>
                          </tr>
                      </table>

                 </td>
             </tr>

             <!--5th tr end-->

            </table>
        </asp:Panel>
         <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />
        <asp:Button ID="btnPDF" runat="server" Text="PDF" OnClick="btnPDF_Click"  />
       <%-- <asp:Button ID="printPdf" runat="server" Text="PDF" OnClick="printPdf_Click" />--%>
    </div>
</asp:Content>
