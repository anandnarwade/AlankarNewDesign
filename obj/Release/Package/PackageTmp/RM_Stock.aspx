<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="RM_Stock.aspx.cs" Inherits="AlankarNewDesign.RM_Stock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="js/jquery-ui.css" rel="stylesheet" />
    <style>
         div.ui-datepicker{
 font-size:12px;
}

         .WindowsStyle .ajax__combobox_inputcontainer .ajax__combobox_textboxcontainer input
    {
        margin: 0;
        border: solid 1px #7F9DB9;
        border-right: 0px none;
        padding: 1px 0px 0px 5px;
        font-size: 13px;
        height: 18px;
        position: relative;
    }
    .WindowsStyle .ajax__combobox_inputcontainer .ajax__combobox_buttoncontainer button
    {
        margin: 0;
        padding: 0;
        background-image: url(windows-arrow.gif);
        background-position: top left;
        border: 0px none;
        height: 21px;
        width: 21px;
    }
    .WindowsStyle .ajax__combobox_itemlist
    {
        border-color: #7F9DB9;
    }
    </style>
     <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
</script>

    <script type="text/javascript">


          $(document).ready(function() {
              $("#ContentPlaceHolder1_ComboRm_ComboRm_TextBox").addClass('input-sm form-control');
          });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row" style="padding-top: 10PX;">
        <div class="col-sm-4">
            <h4 style="color: #1e6099;">RM INVENTORY</h4>
        </div>

        <div class="col-sm-6">
            <center>
                   <div class="alert alert-danger alert-dismissable" runat="server" id="dismiss" visible="false">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                               <asp:Label ID="lblMessage"  runat="server" Visible="false" Text=""></asp:Label>
                    </div>


        </center>

        </div>

    </div>
    <hr  style="margin-top:2px;" />
    <div class="row" style="padding-top:10px; font-family:Verdana; font-size:11px;">
        <div class="form-inline" style="padding-left:20px;">
            <div class="form-group">
                <div><b>Raw Material</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <ajaxToolkit:ComboBox ID="ComboRm" AutoPostBack="true"  AutoCompleteMode="SuggestAppend" runat="server" OnSelectedIndexChanged="ComboRm_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboRm" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="form-group">
                <div><b>Add Stock</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txtAddStock" Width="90px" AutoPostBack="true" Font-Size="11px" runat="server" TextMode="Number" CssClass="input-sm form-control"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtAddStock" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
             <div class="form-group">
                <div><b>Min Stock</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txtMinStock" Width="90px" AutoPostBack="true" Font-Size="11px" runat="server" TextMode="Number" CssClass="input-sm form-control"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtMinStock" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>

             <div class="form-group">
                <div><b>Max Stock</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtMaxStock" Width="90px" AutoPostBack="true" Font-Size="11px" runat="server" TextMode="Number" CssClass="input-sm form-control"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtMaxStock" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
             <div class="form-group">
                <div><b>Safety Stock</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txtSafetyStock" Width="90px" AutoPostBack="true" Font-Size="11px" runat="server" TextMode="Number" CssClass="input-sm form-control"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtSafetyStock" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
              <div class="form-group">
                <div><b>Batch No</b></div>
                <div>
                    <asp:TextBox ID="txtBatchNo" Font-Size="11px" runat="server"  CssClass="input-sm form-control" Required=""></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <div><b>Received Date</b></div>
                <div>
                    <asp:TextBox ID="txtReceivedDate" Width="90px" Font-Size="11px" runat="server"  CssClass="input-sm form-control datepicker"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div></div>
                <div style="padding-top:15px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="SAVE" OnClick="btnSave_Click"  />
                </div>
            </div>
        </div>
        <hr style="margin-top:10px;" />
        <div class="row" style="padding:20px;">
            <asp:GridView ID="GridStock" runat="server" CssClass="table table-bordered table-hover GridView1 dataTable no-footer" AutoGenerateColumns="false" Font-Size="11px" OnRowDataBound="GridStock_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No">
                        <ItemTemplate>
                              <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                            <asp:Label ID="lblStockId" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblRmId" runat="server" Text='<%#Eval("rm_id") %>' Visible="false"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Raw Material">
                        <ItemTemplate>
                            <asp:Label ID="lblRmName" runat="server" Text='<%#Eval("RM_NAME") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Total Stock">
                        <ItemTemplate>
                            <asp:Label ID="lblStock" runat="server" Text='<%#Eval("stock") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Min Stock">
                        <ItemTemplate>
                            <asp:Label ID="lblMinStock" runat="server" Text='<% #Eval("min_stock") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Safety Stock">
                        <ItemTemplate>
                            <asp:Label ID="lblSafetyStock" runat="server" Text='<%#Eval("safety_stock") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Max Stock">
                        <ItemTemplate>
                            <asp:Label ID="lblMaxStock" runat="server" Text='<%#Eval("max_stock") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Batch No">
                        <ItemTemplate>
                            <asp:Label ID="lblBatchNo" runat="server" Text='<%#Eval("batch_no") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Rec. Date">
                        <ItemTemplate>
                            <asp:Label ID="lblReceivedDate" runat="server" Text='<%#Eval("received_date") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Click To Delete!" ForeColor="Red" CommandArgument='<%#Eval("id") %>' OnClick="lnkDelete_Click" ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>






      <script src="js/jquery.js"></script>

    <script src="js/jquery-ui.js"></script>

     <script>
         $(".datepicker").datepicker({
             dateFormat: "dd-mm-yy"
         });
</script>
</asp:Content>
