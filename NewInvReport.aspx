<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewInvReport.aspx.cs" Inherits="AlankarNewDesign.NewInvReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice Report</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
 
    <link href="js/jquery-ui.css" rel="stylesheet" />

    <!-- Bootstrap Core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="vendor/metisMenu/metisMenu.min.css" rel="stylesheet" />


    <!-- DataTables CSS -->
    <%-- <link href="dbTable/datatables.min.css" rel="stylesheet" />--%>
    <link href="dbTable/Buttons-1.5.6/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="vendor/datatables-plugins/dataTables.bootstrap.css" rel="stylesheet" />

    <!-- DataTables Responsive CSS -->
    <link href="vendor/datatables-responsive/dataTables.responsive.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="vendor/morrisjs/morris.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <br />
        <br />
        <a href="WebForm1.aspx" class="btn btn-primary">Back</a>
        <br />
        <br />
        <asp:TextBox ID="txtDateFrom"  CssClass="datepicker" ValidationGroup="rpt"
            placeholder="From Date"
            AutoComplete="false" runat="server"></asp:TextBox>

        <asp:TextBox ID="txtToDate"  CssClass="datepicker" ValidationGroup="rpt"
            placeholder="To Date"
            AutoComplete="false"
            runat="server"></asp:TextBox>

        <asp:Button ID="btnSearch" Text="Searh" ValidationGroup="rpt" runat="server" OnClick="btnSearch_Click"  />

        <br />
        <br />

        <asp:GridView ID="Grid" runat="server" CssClass="table table-sm table-condensed table-bordered table-hover GridView1"
            AutoGenerateColumns="false" OnRowDataBound="Grid_RowDataBound" ShowFooter="true" >
            <Columns>

                <asp:TemplateField HeaderText="Sr.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                    <FooterTemplate>
                        <b>Total</b>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate>
                        
                      
                        <asp:LinkButton ID="lnkCust" runat="server" CommandArgument='<%#Eval("PARTY_CODE") %>' ToolTip="Click to view details" OnClick="lnkCust_Click"  > <%#Eval("Customer") %> </asp:LinkButton>

                    </ItemTemplate>
                   
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="No. of Invoices">
                    <ItemTemplate>
                        <asp:Label ID="lblInv" runat="server" Text='<%#Eval("INVOICES") %>'></asp:Label>

                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblNoOfInv" runat="server" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="% in Total Invoices">
                    <ItemTemplate>
                        <asp:Label ID="lblInvPer" runat="server" ></asp:Label>

                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:Label ID="lblNoOfInvPerF" runat="server" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>

              

                <asp:TemplateField HeaderText="Invoices Value">
                    <ItemTemplate>
                        <asp:Label ID="lblInVal" runat="server" Text='<%#Eval("INVAL") %>'></asp:Label>

                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:Label ID="lblInvValF" runat="server" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="% in Total Value">
                    <ItemTemplate>
                        <asp:Label ID="lblInValPer" runat="server"></asp:Label>

                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:Label ID="lblInValPerF" runat="server" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>

    </div>
    </form>

    <script src="Scripts/jquery-3.3.1.js"></script>
    <script src="js/jquery-ui.js"></script>

    <script src="dbTable/DataTables-1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="dbTable/Buttons-1.5.6/js/dataTables.buttons.min.js"></script>
    <script src="dbTable/Buttons-1.5.6/js/buttons.flash.min.js"></script>
    <script src="dbTable/JSZip-2.5.0/jszip.min.js"></script>
    <script src="dbTable/pdfmake-0.1.36/pdfmake.min.js"></script>
    <script src="dbTable/pdfmake-0.1.36/vfs_fonts.js"></script>
    <script src="dbTable/Buttons-1.5.6/js/buttons.html5.min.js"></script>
    <script src="dbTable/Buttons-1.5.6/js/buttons.print.js"></script>
    <!-- Custom Theme JavaScript -->
    <script src="dist/js/sb-admin-2.js"></script>


   <%-- <script src="js/jquery.js"></script>--%>

  


    <script>
    //$(document).ready(function() {
    //    $('.GridView1').DataTable({
    //        responsive: false
    //    });
    //});

    $(document).ready(function () {
        $('.GridView1').DataTable({
            "ordering": false,
            "scrollY":        "400px",
            "scrollCollapse": true,
            "paging":         false,
            lengthMenu: [
       [10, 25, 50, -1],
       ['10 rows', '25 rows', '50 rows', 'Show all']
            ],
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]      

            
        });


        $(".datepicker").datepicker({
            changeYear: true,
            changeMonth: true,
            dateFormat: "yy-mm-dd"
        });
    });
    </script>

</body>
</html>
