<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="dimension_restriction.aspx.cs" Inherits="AlankarNewDesign.dimension_restriction" %>
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
        <h4 style="color:#1e6099;">DIMENSION RESTRICTION</h4>
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
     <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px; padding-bottom:10px;">
        <div class="col-sm-1" style="padding-top:5px;">Dimension</div>
        <div class="col-sm-2" style="padding-top:5px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:DropDownList ID="DdmDimension" runat="server" CssClass="input-sm form-control" Height="25px" Font-Size="11px" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DdmDimension" />
                </Triggers>
            </asp:UpdatePanel>
           
        </div>
        <div class="col-sm-1" style="padding-top:5px;">
            Value
        </div>
        <div class="col-sm-2" style="padding-top:5px;">
            <asp:TextBox ID="txtValue" runat="server" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3" style="padding-top:5px;">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-info" Text="SAVE" OnClick="btnSave_Click"  />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-sm btn-danger" Text="Cancel" OnClick="btnCancel_Click"  />
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
        </div>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px; width:600px;">
        <asp:GridView ID="GridDir" Width="600px" CssClass="GridView1 table table-bordered table-hover table-responsive" AutoGenerateColumns="false" runat="server" Font-Size="11px">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No" HeaderStyle-Font-Bold="false" HeaderStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label  ID="lblSrNo" runat="server"  Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" Visible="false" />
                  
                    <asp:BoundField DataField="DIMENSION" HeaderText="Dimension" HeaderStyle-Font-Bold="false" />
                    <asp:BoundField DataField="Value" HeaderText="Value" HeaderStyle-Font-Bold="false" />
                 
                   
                      <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="90px"  HeaderStyle-Font-Bold="false">
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
                                             <asp:LinkButton ID="lnkEdit" Font-Size="11px" runat="server"  CommandArgument='<%#Eval("ID") %>' OnClick="lnkEdit_Click"  ><i  class="glyphicon glyphicon-edit"></i> EDIT</asp:LinkButton>
                                        </li>
                                        <li class="divider"></li>
                                        <li>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Font-Size="11px" CommandArgument='<%#Eval("ID") %>' OnClick="lnkDelete_Click"   ><i class="glyphicon glyphicon-trash"> </i> DELETE</asp:LinkButton>
                                        </li>

                                       

                                    </ul>
                                </div>
                            </ItemTemplate>
                           </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </div>
</asp:Content>
