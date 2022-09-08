<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="ItemCodeRpt.aspx.cs" Inherits="AlankarNewDesign.ItemCodeRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .code {
    font-family: Consolas,Menlo,Monaco,Lucida Console,Liberation Mono,DejaVu Sans Mono,Bitstream Vera Sans Mono,Courier New,monospace,sans-serif;
    background-color: #eff0f1;
}

        /*.table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
    padding: 8px;
    line-height: 1.42857143;
    vertical-align: top;
    border-top: 1px solid #fff0;
}*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top: 10px;">
        <div class="col-sm-6">
            <h4>Item Code Report</h4>
        </div>
    </div>
    <hr style="margin-top: 2px;" />
    <div class="row" style="padding-top: 10px; padding-left: 20px; padding-right: 20px;">


        <div class="form-inline">

            <div class="form-group">
                <div><b>Customer</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <ajaxtoolkit:combobox id="ComboBoxParty" ClientIDMode="Static" width="300px" font-size="12px" autocompletemode="Suggest"
                                autopostback="true" runat="server" onselectedindexchanged="ComboBoxParty_SelectedIndexChanged"></ajaxtoolkit:combobox>
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
                                runat="server"></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboItemCode" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>


            <div class="form-group">
                <div></div>
                <div style="padding-top: 20px;">
                    <asp:Button ID="btnSearch" ClientIDMode="Static" runat="server" CssClass="btn btn-primary" Text="Search"  OnClick="btnSearch_Click"  />

                </div>
            </div>


        </div>


    </div>

      <hr />

    <div class="row" style="padding-top:20px; padding-left:10px;">

        <div class="col-sm-6">

            <b>FOC Details</b>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td>
                        <label class="" style="font-size: 14px;">FOC</label>
                    </td>
                    <td>
                        <asp:Label ID="lblFoc" CssClass="code" Font-Size="14px" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="" style="font-size: 14px;">Description</label>
                    </td>
                    <td>
                        <asp:Label ID="lblDescription" CssClass="code" Font-Size="14px" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>



        <div class="col-sm-6">

            <b>Last Supply</b>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td>
                        <label class="" style="font-size: 14px;">INVOICE No.</label>
                    </td>
                    <td>
                        <asp:Label ID="lblInvNo" CssClass="code" Font-Size="14px" runat="server"></asp:Label>
                    </td>

                    <td>
                        <label class="" style="font-size: 14px;">OC No</label>
                        
                    </td>
                    <td>
                        <asp:Label ID="lblOcNo" CssClass="code" Font-Size="14px" runat="server"></asp:Label>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="" style="font-size: 14px;">INVOICE QTY</label>
                    </td>
                    <td>
                        <asp:Label ID="lblInvQty" CssClass="code" Font-Size="14px" runat="server"></asp:Label>
                    </td>
                    <td>
                        <label class="" style="font-size: 14px;">INVOICE Date</label>
                    </td>
                    <td>
                        <asp:Label ID="lblInvDate" CssClass="code" Font-Size="14px" runat="server"></asp:Label>
                    </td>

                   
                </tr>
                <tr >
                    <td>
                        <label class="" style="font-size: 14px;">OC Date</label>
                    </td>
                    <td>
                        <asp:Label ID="lblOcDt" CssClass="code" Font-Size="14px" runat="server"></asp:Label>
                    </td>
                    <td colspan="2">

                    </td>
                </tr>
            </table>

        </div>

      

    </div>
    <div class="row" style="padding-top: 10px; padding-left: 20px; padding-right: 20px;">

        <asp:RadioButton ID="rdoAll" AutoPostBack="true" runat="server" GroupName="rpt" Text="All" OnCheckedChanged="rdoAll_CheckedChanged"  />
        <asp:RadioButton ID="rdoPending" AutoPostBack="true" runat="server" GroupName="rpt" Text="Pending" OnCheckedChanged="rdoPending_CheckedChanged"  />

        <asp:GridView ID="Grid" CssClass="table table-bordered table-condensed table-responsive" runat="server"></asp:GridView>

    </div>

   

    <script src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">
      
    </script>
</asp:Content>
