<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="quantity_updation.aspx.cs" Inherits="AlankarNewDesign.quantity_updation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
</script>
    <style type="text/css">
        .col-sm-2 {
    width:9.667%;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">Quantity Updation</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" Font-Size="15px" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblFlag" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblToolType" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblSubToolType" runat="server" Visible="false" Text=""></asp:Label>

        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:12px;">
        <div class="col-sm-1" style="padding-top:3px;">
            <b>OC NO</b><b style="color:red;">*</b>
        </div>
        <div class="col-sm-2">
             <asp:TextBox id="txtocno" CssClass="input-sm form-control" Font-Bold="true"  runat="server" Font-Size="11px" AutoPostBack="true" OnTextChanged="txtocno_TextChanged" required=""></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="GetTagNames" TargetControlID="txtocno" MinimumPrefixLength="1" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" runat="server"></ajaxToolkit:AutoCompleteExtender>
        </div>
        <div class="col-sm-2">
            <b>Item Code:</b>
        </div>
        <div class="col-sm-2">
             <asp:Label ID="lblItemCode" Font-Bold="true"  runat="server" Font-Size="11px" Text=""></asp:Label>
        </div>
        <div class="col-sm-1">
            <b>DRG No :</b>
        </div>
        <div class="col-sm-2" style="text-align:left;">
            <asp:Label ID="lblDrgNO" Font-Bold="true" runat="server" Font-Size="11px" Text=""></asp:Label>
        </div>
        <div class="col-sm-1">
           <b>Qty :</b> 
        </div>
        <div class="col-sm-1" style="text-align:left;">
             <asp:Label ID="lblQuantity" Font-Bold="true" runat="server" Font-Size="11px" Text=""></asp:Label>
        </div>
    </div>
    <div class="row" style="padding-top:10px; font-size:12px;">
        <div class="col-sm-1">
            <b>Customer: </b>
        </div>
        <div class="col-sm-4">
             <asp:Label ID="lblCustName" Font-Bold="true" runat="server" Text="" Font-Size="11px"></asp:Label>
        </div>
        <div class="col-sm-1"></div>
        <div class="col-sm-4">
            <asp:Label ID="lblCloseOcMsg" runat="server" Font-Size="Larger" Font-Bold="true" CssClass="label label-danger"></asp:Label>
        </div>
    </div>
    <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px; font-size:11px;">
        <div class="col-sm-1">
            <asp:Label ID="lblNotIssue" runat="server" Font-Size="11px" Font-Bold="true" CssClass="label label-danger" Text="NOT ISSUE"></asp:Label>
        </div>
        <div class="col-sm-1">
            <asp:TextBox ID="txtNosIssue" runat="server" CssClass="input-sm form-control" Font-Size="13px" Font-Bold="true" ForeColor="Red" ReadOnly="true"></asp:TextBox>
        </div>
         <div class="col-sm-1">
            <asp:Label ID="lblIssue" runat="server" Font-Size="11px" Font-Bold="true" CssClass="label label-primary" Text="ISSUE"></asp:Label>
        </div>
        <div class="col-sm-1">
            <asp:TextBox ID="txtIssue" runat="server" CssClass="input-sm form-control" Font-Size="13px" Font-Bold="true" ForeColor="#337ab7" ReadOnly="true"></asp:TextBox>
        </div>
         <div class="col-sm-1">
            <asp:Label ID="lblCleaning" runat="server" Font-Size="11px" Font-Bold="true" CssClass="label label-warning" Text="CLEANING"></asp:Label>
        </div>
        <div class="col-sm-1">
            <asp:TextBox ID="txtCleaning" runat="server" CssClass="input-sm form-control" Font-Size="13px" Font-Bold="true" ForeColor="#f0ad4e" ReadOnly="true"></asp:TextBox>
        </div>
         <div class="col-sm-1">
            <asp:Label ID="lblDispatched" runat="server" Font-Size="11px" Font-Bold="true" CssClass="label label-success" Text="DISPATCHED"></asp:Label>
        </div>
        <div class="col-sm-1">
            <asp:TextBox ID="txtdispatched" runat="server" CssClass="input-sm form-control" Font-Size="13px" Font-Bold="true" ForeColor="#5cb85c" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px; font-size:11px;">
        <ajaxToolkit:TabContainer ID="TabsNotIssue"   runat="server">
            <ajaxToolkit:TabPanel TabIndex="0" ID="panelNOtIssue" runat="server" HeaderText="NOT ISSUE">
                <ContentTemplate>
                    <div class="row" style="font-size:10px; padding-left:30px; padding-right:30px;">
                        <asp:DataList Font-Size="10px" RepeatColumns="5" RepeatDirection="Horizontal" CssClass="row" ID="DataListNotIssue" runat="server">
                            <ItemTemplate>


                                <div class="row" style=" width:230px;">
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">

                                        <asp:Label ID="lblStage" runat="server" Font-Size="11px" Width="100px" Font-Bold="true" Text='<%#Eval("STAGE") %>'></asp:Label>

                                    </div>
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">

                                        <asp:TextBox ID="txtStages" Width="70px" CssClass="input-sm form-control" Font-Size="11px" placeholder='<%#Eval("STAGE") %>' runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtStages" FilterType="Numbers" runat="server" />

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>

                         <asp:DataList Font-Size="10px" RepeatColumns="5" RepeatDirection="Horizontal" CssClass="row" ID="DataListNotIssueUpdate" runat="server">
                            <ItemTemplate>


                                <div class="row" style=" width:230px;">
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">
                                         <asp:Label ID="lblId" runat="server" Font-Size="11px" Visible="false" Text='<%#Eval("ID") %>'></asp:Label>
                                        <asp:Label ID="lblStage" runat="server" Font-Size="11px" Width="100px" Font-Bold="true" Text='<%#Eval("STAGE") %>'></asp:Label>

                                    </div>
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">

                                        <asp:TextBox ID="txtStages" Width="70px" CssClass="input-sm form-control" Font-Size="11px" Text='<%#Eval("VALUE") %>' runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtStages" FilterType="Numbers" runat="server" />

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
     <div class="row" style="padding-top:10px; font-size:11px;">
        <ajaxToolkit:TabContainer ID="TabContainer1"   runat="server">
            <ajaxToolkit:TabPanel TabIndex="0" ID="TabIssue" runat="server" HeaderText="ISSUE">
                <ContentTemplate>
                    <div class="row" style="font-size:10px; padding-left:30px; padding-right:30px;">
                        <asp:DataList Font-Size="10px" RepeatColumns="5" RepeatDirection="Horizontal" CssClass="row" ID="DataListIssue" runat="server">
                            <ItemTemplate>


                                <div class="row" style=" width:230px;">
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">

                                        <asp:Label ID="lblStage" runat="server" Font-Size="11px" Width="100px" Font-Bold="true" Text='<%#Eval("STAGE") %>'></asp:Label>

                                    </div>
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">

                                        <asp:TextBox ID="txtStages" Width="70px" CssClass="input-sm form-control" Font-Size="11px" placeholder='<%#Eval("STAGE") %>' runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtStages" FilterType="Numbers" runat="server" />

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>

                        <asp:DataList Font-Size="10px" RepeatColumns="5" RepeatDirection="Horizontal" CssClass="row" ID="DataListIssueUpdate" runat="server">
                            <ItemTemplate>


                                <div class="row" style=" width:230px;">
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">
                                         <asp:Label ID="lblId" runat="server" Font-Size="11px" Visible="false" Text='<%#Eval("ID") %>'></asp:Label>
                                        <asp:Label ID="lblStage" runat="server" Font-Size="11px" Width="100px" Font-Bold="true" Text='<%#Eval("STAGE") %>'></asp:Label>

                                    </div>
                                    <div class="col-sm-6" style="padding: 3px; text-align: right;">

                                        <asp:TextBox ID="txtStages" Width="70px" CssClass="input-sm form-control" Font-Size="11px"  Text='<%#Eval("VALUE") %>' runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtStages" FilterType="Numbers" runat="server" />

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-1"></div>
        <div class="col-sm-3">
            <asp:Button ID="btnSAVE" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-info" Text="SAVE" OnClick="btnSAVE_Click"  />
            <asp:LinkButton ID="btnClear" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-danger" Text="Cancel" OnClick="btnClear_Click" ></asp:LinkButton>
        </div>
    </div>

       <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
</asp:Content>
