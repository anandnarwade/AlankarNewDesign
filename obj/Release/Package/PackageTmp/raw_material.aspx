<%@ Page Title="Raw Material Master" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="raw_material.aspx.cs" Inherits="AlankarNewDesign.raw_material" %>
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
        <h4 style="color:#1e6099;">RAW MATERIAL</h4>
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
      <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px; font-size:12px;">
        <div class="col-sm-1">
            RM Name
        </div>
        <div class="col-sm-2">
               <asp:TextBox ID="txtRawMaterial"  runat="server" required="" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div>
         <div class="col-sm-1">
            Unit
        </div>
        <div class="col-sm-2">
               <asp:TextBox ID="txtunit"  runat="server" required="" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div> <div class="col-sm-1">
            Sequence
        </div>
        <div class="col-sm-2">
               <asp:TextBox ID="txtSequence"  runat="server" required="" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            Market_Rate
        </div>
        <div class="col-sm-2">
               <asp:TextBox ID="txtMarketPrice"  runat="server" required="" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row" style="padding-top:10px; font-size:12px;">
        <div class="col-sm-1">
            Description
        </div>
        <div class="col-sm-2">
             <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-1"></div>
        <div class="col-sm-3">
            <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="btn btn-sm btn-info" Font-Size="12px" Font-Bold="true" OnClick="btnSave_Click"  />
            <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger" Font-Size="12px" Font-Bold="true" OnClick="Button1_Click"  />
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">
        <asp:GridView ID="GridRM" RowStyle-HorizontalAlign="Center" CssClass="GridView1 table table-responsive table-bordered table-hover" runat="server" AutoGenerateColumns="False" Font-Size="11px" HorizontalAlign="Center">
            <AlternatingRowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="40px" >
                    <ItemTemplate>
                        <asp:Label ID="lblSrNO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>

                  
                </asp:TemplateField>
                <asp:BoundField  HeaderStyle-Width="40px" DataField="SEQUENCE" HeaderText="SEQ">
                  
                </asp:BoundField>
                <asp:BoundField  DataField="RM_ID" Visible="false">
                   


                </asp:BoundField>
                <asp:BoundField  DataField="RM_NAME" HeaderText="Raw Material">
                   
                </asp:BoundField>
                <asp:BoundField DataField="RM_DESCRIPTION" HeaderText="Description">
                 
                </asp:BoundField>
                <asp:BoundField  DataField="RM_UNIT" HeaderText="Unit">
                   
                </asp:BoundField>
                <asp:BoundField DataField="MARKET_RATE" HeaderText="Rate" />

                <asp:TemplateField HeaderText="ACTION" >
                    <ItemTemplate>
                        <div class="btn-group">
                            <asp:LinkButton ID="LinkButton2" Font-Size="11px" aria-expanded="false" class="dropdown-toggle" data-toggle="dropdown" runat="server">Action</asp:LinkButton>
                            <%-- <button aria-expanded="false" type="button" class="btn  btn-sm btn-info dropdown-toggle" data-toggle="dropdown">
                                        Action
                                                           <span class="caret"></span>
                                        <span class="sr-only">Toggle Dropdown</span>
                                    </button>--%>
                            <ul class="dropdown-menu" role="menu">
                                <li>
                                    <asp:LinkButton Font-Size="11px" ID="lnkUpdate" runat="server" CommandArgument='<%#Eval("RM_ID") %>' OnClick="lnkUpdate_Click"><i class="glyphicon glyphicon-edit"></i> EDIT</asp:LinkButton>
                                </li>
                                <li class="divider"></li>
                                <li>
                                    <asp:LinkButton ID="lnkDelete" Font-Size="11px" runat="server" CommandArgument='<%#Eval("RM_ID") %>' OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>
                                </li>

                            </ul>
                        </div>
                    </ItemTemplate>


                    <HeaderStyle Font-Bold="False"></HeaderStyle>


                </asp:TemplateField>
            </Columns>







        </asp:GridView>
    </div>
</asp:Content>
