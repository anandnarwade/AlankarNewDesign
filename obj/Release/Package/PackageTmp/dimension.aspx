<%@ Page Title="Dimensions" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="dimension.aspx.cs" Inherits="AlankarNewDesign.dimension" %>

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
        <h4 style="color:#1e6099;">DIMENSIONS MASTER</h4>
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px; padding-bottom:10px; font-size:12px; font-family:sans-serif;">
        <div class="col-sm-1">
            Dimension
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="txtDimention" CssClass="input-sm form-control" runat="server" Font-Size="11px" required=""></asp:TextBox>
        </div>
         <div class="col-sm-1">
            Unit
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="txtUnit" CssClass="input-sm form-control" runat="server" Font-Size="11px"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            Sequence
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="txtSequence" Width="50px" CssClass="input-sm form-control" runat="server" Font-Size="11px"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtSequence" FilterType="Numbers" runat="server" />
        </div>
        <div class="col-sm-3" style="padding-top:3px;">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="SAVE" Font-Size="11px" OnClick="btnSave_Click"  />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-sm btn-danger" Text="CANCEL" Font-Size="11px" OnClick="btnCancel_Click"  />
        </div>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top: 20px;">
        <asp:GridView ID="GridDimention"  AutoGenerateColumns="False" CssClass="GridView1 table table-striped table-bordered table-hover dataTable no-footer dtr-inline" runat="server" Font-Size="11px" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">

            <Columns>
                <asp:TemplateField HeaderText="SR. NO" HeaderStyle-Width="50px" HeaderStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Font-Bold="true"></HeaderStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="SEQUENCE" HeaderStyle-Width="70px" HeaderText="SEQUENCE" HeaderStyle-Font-Bold="true">

                    <HeaderStyle Font-Bold="true"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ID" Visible="false" HeaderStyle-Font-Bold="true">
                    <HeaderStyle Font-Bold="true"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DIMENTION" HeaderText="DIMENTION" HeaderStyle-Font-Bold="true">
                    <HeaderStyle Font-Bold="true"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="UNIT" HeaderText="UNIT" HeaderStyle-Font-Bold="true">

                    <HeaderStyle Font-Bold="true"></HeaderStyle>
                </asp:BoundField>


                <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="90px" HeaderStyle-Font-Bold="true">
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
                                    <asp:LinkButton ID="lnkUpdate" Font-Size="11px" runat="server" CommandArgument='<%#Eval("ID") %>' OnClick="lnkUpdate_Click" ><i class="glyphicon glyphicon-edit"></i> EDIT</asp:LinkButton>
                                </li>
                                <li class="divider"></li>
                                <li>
                                    <asp:LinkButton ID="lnkDelete" Font-Size="11px" runat="server" CommandArgument='<%#Eval("ID") %>'  OnClick="lnkDelete_Click" ><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>
                                </li>



                            </ul>
                        </div>
                        
                    </ItemTemplate>

                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle Font-Bold="True" />

            <RowStyle HorizontalAlign="Left" VerticalAlign="Middle"></RowStyle>

        </asp:GridView>
    </div>
</asp:Content>
