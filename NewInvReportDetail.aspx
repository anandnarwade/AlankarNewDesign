<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewInvReportDetail.aspx.cs" Inherits="AlankarNewDesign.NewInvReportDetail" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <br />
        <br />
        <a href="NewInvReport.aspx" class="btn btn-primary">Back</a>
        <br />
        <br />
    
        <asp:GridView ID="GridRpt" runat="server" CssClass="table table-sm table-condensed table-bordered table-hover GridView1"
            AutoGenerateColumns="false" OnRowDataBound="GridRpt_RowDataBound">

            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Invoice No">
                    <ItemTemplate>
                        <a href="#"><%#Eval("inv_no") %></a>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Invoice Date">
                    <ItemTemplate>
                        <asp:Label id="lblInvDate" runat="server" Text='<%#Eval("inv_date") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="OC No">
                    <ItemTemplate>
                        <asp:Label ID="lblOcNo" runat="server" Text='<%#Eval("oc_no") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Invoice QTY">
                    <ItemTemplate>
                        <asp:Label ID="lblInvQty" runat="server" Text='<%#Eval("inv_qty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblTotAmt" runat="server" Text='<%#Eval("total_amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Amount %">
                    <ItemTemplate>
                        <asp:Label ID="lblTotAmtPer" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
    </form>


    <script src="Scripts/jquery-3.3.1.js"></script>


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


    <script>
    //$(document).ready(function() {
    //    $('.GridView1').DataTable({
    //        responsive: false
    //    });
    //});

    $(document).ready(function () {
        $('.GridView1').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
        });
    });
    </script>



</body>
</html>
