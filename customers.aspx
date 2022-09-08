<%@ Page Title="Customers" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="customers.aspx.cs" Inherits="AlankarNewDesign.customers" %>
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
        <h4 style="color:#1e6099;">ALL CUSTMORS</h4>

            <asp:Button ID="btnAddnew" runat="server" Text="Add New" CssClass="btn btn-sm btn-primary" OnClick="btnAddnew_Click"  />

        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px;">
          <asp:GridView ID="GridMasterParty" AutoGenerateColumns="False" CssClass="GridView1 table table-responsive table-bordered table-hover" runat="server" Font-Size="11px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PARTY_CODE" HeaderText="CUSTOMER CODE" HeaderStyle-Font-Bold="false" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PARTY_NAME" HeaderText="CUSTOMER NAME" HeaderStyle-Font-Bold="false" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CITY" HeaderText="CITY" HeaderStyle-Font-Bold="false" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GSTIN" HeaderText="GSTIN" />
                                <asp:BoundField DataField="PHONE" HeaderText="PHONE" HeaderStyle-Font-Bold="false" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ACTION" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                         <div class="btn-group">
                                             <asp:LinkButton ID="LinkButton2" aria-expanded="false" class="dropdown-toggle" data-toggle="dropdown" runat="server">Action</asp:LinkButton>
                                       <%-- <button aria-expanded="false" type="button" class="btn  btn-sm btn-info dropdown-toggle" data-toggle="dropdown">
                                            Action
                                                           <span class="caret"></span>
                                            <span class="sr-only">Toggle Dropdown</span>
                                        </button>--%>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <asp:HyperLink Font-Size="11px" ID="Recall_Link10" runat="server" NavigateUrl='<%#Eval ("PARTY_CODE", "cust_master.aspx?PARTY_CODE={0}&Action=UPDATE") %>'><i class="glyphicon glyphicon-edit"></i> EDIT</asp:HyperLink>
                                            </li>
                                            <li class="divider"></li>
                                            <li>
                                                <%--<asp:HyperLink ID="HyperLink11" runat="server"><i class="glyphicon glyphicon-trash"></i> Delete</asp:HyperLink>--%>
                                                <asp:LinkButton Font-Size="11px" ID="LinkButton1" runat="server"  CommandArgument='<%#Eval ("PARTY_CODE") %>' OnClick="LinkButton1_Click"  ><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>
                                            </li>






                                        </ul>
                                    </div>
                                    </ItemTemplate>

<HeaderStyle Font-Bold="False"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle HorizontalAlign="Center" />
                        </asp:GridView>
    </div>

</asp:Content>
