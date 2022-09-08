<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NearByResult.aspx.cs" Inherits="AlankarNewDesign.NearByResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Near By Report</title>
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
    <div>

        <asp:GridView ID="GridView1" Font-Size="12px" AutoGenerateColumns="false" CssClass="GridView1 table table-bordered table-condensed table-hover" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataField="OC_NO" HeaderText="OC NO"/>
                <asp:BoundField DataField="Customer" HeaderText="Customer" />
                <asp:BoundField DataField="TOOL_SUB_TYPE" HeaderText="Tool Sub Type" />
                <asp:BoundField DataField="OCQTY" HeaderText="OC Qty" />
                <asp:BoundField DataField="LAST INV QTY" HeaderText="Last INV Qty" />
                <asp:BoundField DataField="LAST INV Date" HeaderText="Last INV Date" />
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
