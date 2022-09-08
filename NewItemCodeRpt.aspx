<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewItemCodeRpt.aspx.cs" Inherits="AlankarNewDesign.NewItemCodeRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item code Report</title>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="container-fluid">
            <br />
            <br />
            <a href="WebForm1.aspx" class="btn btn-primary">Back</a>
            <br />
            <br />
            <div class="row" style="padding-top: 10px; padding-left: 20px; padding-right: 20px;">


                <div class="form-inline">

                    <div class="form-group">
                        <div><b>Customer</b></div>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <ajaxToolkit:ComboBox ID="ComboBoxParty" ClientIDMode="Static" Width="300px" Font-Size="12px"
                                        AutoCompleteMode="Suggest"
                                        AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboBoxParty_SelectedIndexChanged" >
                                    </ajaxToolkit:ComboBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ComboBoxParty" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>

                    </div>


                    <div class="form-group">
                        <div><b>Item Code</b></div>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <ajaxToolkit:ComboBox ID="ComboItemCode" AutoCompleteMode="Suggest" AutoPostBack="true"
                                        runat="server">
                                    </ajaxToolkit:ComboBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ComboItemCode" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="form-group">
                        <div><b>Report Type</b></div>
                        <div>
                            <asp:DropDownList ID="ddmRptType" runat="server" required="">
                                <asp:ListItem Value="" Text="Select"></asp:ListItem>

                                <asp:ListItem Value="0" Text="Booking"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Dispatch"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="form-group">
                        <div></div>
                        <div style="padding-top: 20px;">
                            <asp:Button ID="btnSearch" ClientIDMode="Static" runat="server" CssClass="btn btn-primary"
                                Text="Search" OnClick="btnSearch_Click" />

                        </div>
                    </div>


                </div>


            </div>

            <br />
            <br />

            <asp:GridView ID="Grid" runat="server" CssClass="table table-sm table-condensed table-bordered table-hover GridView1"
                AutoGenerateColumns="TRUE">
               
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
