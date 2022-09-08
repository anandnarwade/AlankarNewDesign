<%@ Page Title="All Invoices" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="all_invoice.aspx.cs" Inherits="AlankarNewDesign.all_invoice" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <div class="col-sm-4">
            <h4 style="color:#1e6099;">INVOICE</h4>
        </div>
        <div class="col-sm-4 col-sm-offset-10" style="padding:5px;">
              <a href="create_invoice.aspx" class="btn btn-xs btn-primary" ><i class="glyphicon glyphicon-pencil"></i> Create Invoice</a>
        </div>
        
      
          
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblPartyCode" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblccId" runat="server" Visible="false" Text=""></asp:Label>
            
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px; padding-left:10px; padding-right:10px;">
        <asp:GridView ID="GridInv" CssClass="table table-bordered table-hover GridView1" AutoGenerateColumns="false" runat="server" Font-Size="11px">
            <Columns>
                <asp:TemplateField HeaderText="Sr.No">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex +1 %>' Font-Size="11px" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id" HeaderText="ID" Visible="false" />
                 <asp:BoundField DataField="oc_no" HeaderText="OC NO" />
                 <asp:BoundField DataField="inv_no" HeaderText="INVOICE NO" />
                 <asp:BoundField DataField="inv_date" HeaderText="INV DATE" />
                 <asp:BoundField DataField="inv_qty" HeaderText="Qty" />
                <asp:BoundField DataField="PARTY_CODE" HeaderText="CUST CODE" />
                 <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="90px" HeaderStyle-Font-Bold="true">
                    <ItemTemplate>
                        <div class="btn-group">
                            <asp:LinkButton ID="LinkButton2" Width="70px" Font-Size="11px" aria-expanded="false" class="dropdown-toggle" data-toggle="dropdown" runat="server">Action</asp:LinkButton>
                            <%-- <button aria-expanded="false" type="button" class="btn  btn-sm btn-info dropdown-toggle" data-toggle="dropdown">
                                        Action
                                                           <span class="caret"></span>
                                        <span class="sr-only">Toggle Dropdown</span>
                                    </button>--%>
                            <ul class="dropdown-menu" role="menu">
                                <li>
                                     <asp:HyperLink Font-Size="11px" ToolTip="Click To Edit" ID="Recall_Link10" runat="server" NavigateUrl='<%#Eval ("inv_no", "create_invoice.aspx?inv_no={0}&Action=UPDATE") %>'><i class="glyphicon glyphicon-edit"></i> <b>EDIT</b></asp:HyperLink>
                                </li>
                                <li class="divider"></li>
                                <li>
                                   <asp:HyperLink Font-Size="11px" ID="HyperLink1" runat="server" ToolTip="Click To Print" NavigateUrl='<%#Eval ("inv_no", "PrintInv.aspx?inv_no={0}&Action=UPDATE") %>'><i class="glyphicon glyphicon-duplicate"></i> <b>PRINT</b></asp:HyperLink>
                                </li>
                                 <li class="divider"></li>
                                <li>
                                    <asp:LinkButton ID="lnkInvDelete" runat="server" Font-Size="11px" CommandArgument='<%#Eval("inv_no") %>' ToolTip="Click To Delete" OnClick="lnkInvDelete_Click" ><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" TargetControlID="lnkInvDelete" ConfirmOnFormSubmit="true" ConfirmText="Do you want to Delete?" runat="server" />
                                </li>


                            </ul>
                        </div>
                        
                    </ItemTemplate>

                   
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
