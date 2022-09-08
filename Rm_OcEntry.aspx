<%@ Page Title="RM OC Entry" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="Rm_OcEntry.aspx.cs" Inherits="AlankarNewDesign.Rm_OcEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="js/jquery-ui.css" rel="stylesheet" />
    <style>
         div.ui-datepicker{
 font-size:12px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="row" style="padding-top: 10PX;">
        <div class="col-sm-4">
            <h4 style="color: #1e6099;">Raw Material</h4>
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
                <div><b>OC No</b></div>
                <div>
                    <asp:TextBox ID="txtOcNo" runat="server" AutoPostBack="true" Font-Size="11px" CssClass="input-sm form-control" OnTextChanged="txtOcNo_TextChanged" ></asp:TextBox>
                     <ajaxToolkit:AutoCompleteExtender ID="autoOc" runat="server"  ServiceMethod="GetTagNames" TargetControlID="txtOcNo" MinimumPrefixLength="1" CompletionInterval="500" CompletionSetCount="10" EnableCaching="false"></ajaxToolkit:AutoCompleteExtender>
                </div>
            </div>
            <div class="form-group">
                <div><b>Raw Material</b></div>
                <div>
                    <ajaxToolkit:ComboBox ID="ComboRm" AutoCompleteMode="SuggestAppend" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComboRm_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                </div>
            </div>
            <div class="form-group">
                <div><b>Qty</b> <b style="color:red">*</b>  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Qty!" ForeColor="Red" ControlToValidate="txtQty"></asp:RequiredFieldValidator></div>
                <div>
                    <asp:TextBox ID="txtQty" runat="server" TextMode="Number" CssClass="input-sm form-control " Font-Size="11px"></asp:TextBox>

                </div>
            </div>
            <div class="form-group">
                <div><b>Date</b></div>
                <div>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm form-control datepicker"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div></div>
                <div style="padding-top:15px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="SAVE" OnClick="btnSave_Click"  />
                </div>
            </div>
        </div>
        <div class="form-inline" style="padding-left:20px; padding-top:20px; font-family:Verdana; font-size:11px;">
            <div class="col-sm-2">
                <div> <b >Total :</b> <asp:Label ID="lblTotal" Font-Bold="true" runat="server" Text="" Font-Size="11px"></asp:Label></div>
            </div>
             <div class="col-sm-2">
                <div><b> Min Qty :</b>  <asp:Label ID="lblMinQty" Font-Bold="true" runat="server" Text="" Font-Size="11px"></asp:Label></div>
            </div>
             <div class="col-sm-2">
                <div><b>Max Qty :</b>  <asp:Label ID="lblMaxQty" Font-Bold="true" runat="server" Text="" Font-Size="11px"></asp:Label></div>
            </div>
             <div class="col-sm-2">
                <div><b>Safety Qty :</b>  <asp:Label ID="lblSafatyQty" Font-Bold="true" runat="server" Text="" Font-Size="11px"></asp:Label></div>
            </div>
        </div>

    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding:20px; font-family:Verdana; font-size:11px;">
        <asp:GridView ID="GridSTockUsed" CssClass="table table-bordered table-hover GridView1 dataTable no-footer" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>

                        <asp:Label ID="lblRmId" runat="server" Text='<%#Eval("RM_ID") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblSRNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Raw Material">
                    <ItemTemplate>
                         <asp:Label ID="lblRmName" runat="server" Text='<%#Eval("RM_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Narration">
                    <ItemTemplate>
                         <asp:Label ID="lblNarration" runat="server" Text='<%#Eval("oc_no") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Plus">
                    <ItemTemplate>
                         <asp:Label ID="lblStockPlus" runat="server" Text='<%#Eval("stock_plus") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Minus">
                    <ItemTemplate>
                         <asp:Label ID="lblStockMinus" runat="server" Text='<%#Eval("stock_minus") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                         <asp:Label ID="lblDate" runat="server" Text='<%#Eval("entryDate") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Batch No">
                    <ItemTemplate>
                         <asp:Label ID="lblBatchNo" runat="server" Text='<%#Eval("batchno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                         <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Red" ToolTip="Click To Delete!" CommandArgument='<%#Eval("id") %>' OnClick="lnkDelete_Click" ><i class="glyphicon glyphicon-trash"></i> Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>



     <script src="js/jquery.js"></script>

    <script src="js/jquery-ui.js"></script>

     <script>
         $(".datepicker").datepicker({
             dateFormat: "dd-mm-yy"
         });
</script>
</asp:Content>
