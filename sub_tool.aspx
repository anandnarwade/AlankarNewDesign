<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="sub_tool.aspx.cs" Inherits="AlankarNewDesign.sub_tool" %>

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
        <h4 style="color:#1e6099;">SUB TOOL TYPE MASTER</h4>
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblUp" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="Label1" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px; font-size:12px;">
        <div class="form-inline">
            <div class="form-group" style="display: inline-grid;">
                <div>
                    <b>Main Type</b>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddmMainToolType"  OnSelectedIndexChanged="ddmMainToolType_SelectedIndexChanged" AutoPostBack="true" runat="server" Font-Size="11px" Height="25px"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddmMainToolType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group" style="display:inline-grid;">
                <div style="padding-left:15px;">
                    <b>Sub Type</b>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtSubType" placeholder="Enter Sub Tool" runat="server" required="" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display:inline-grid;">
                <div style="padding-left:15px;">
                    <b>Sequecne</b>
                </div>
                <div class="col-sm-2">
                      <asp:TextBox ID="txtSequence" placeholder="Sequence" runat="server" Font-Size="11px" CssClass="input-sm form-control" Width="70px"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtSequence" FilterType="Numbers" runat="server" />
                </div>
            </div>
            <div class="form-group" style="display:inline-grid">
                <div  style="padding-left:15px;">
                    <b>HSN CODE</b>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtHsnCode" placeholder="Enter HSN Code" runat="server" Font-Size="11px" CssClass="input-sm form-control" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display:initial; padding-top:10px;">
                 <asp:Button ID="btnSave" runat="server" Text="SAVE" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-info" OnClick="btnSave_Click"  />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="true"  Font-Size="12px" CssClass="btn btn-xs btn-danger" OnClick="btnCancel_Click"  />
            </div>
             

        </div>
       
       
      
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:20px;">
          <asp:GridView ID="GridTool" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive table-hover GridView1" runat="server" Font-Size="11px">
                        <Columns>
                            <asp:TemplateField HeaderText="SR.NO" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="SEQ." HeaderStyle-Width="50px" DataField="SEQUENCE"  />
                            <asp:BoundField DataField="ID" Visible="false"/>
                            <asp:BoundField DataField="TOOL_TYPE" Visible="false" HeaderText="TOOL TYPE"  />
                            <asp:BoundField DataField="MAIN_TYPE" HeaderText="MAIN TYPE"  />
                            <asp:BoundField DataField="SUB_TYPE" HeaderText="SUB TYPE"  />
                            <asp:BoundField DataField="HSN_NO" HeaderText="HSN CODE" />
                              <asp:TemplateField HeaderText="ACTION">
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
                                            <asp:LinkButton Font-Size="11px" ID="lnkUpdate" runat="server" CommandArgument= '<%#Eval("ID") %>' OnClick="lnkUpdate_Click" ><i class="glyphicon glyphicon-edit"></i> EDIT</asp:LinkButton>    
                                        </li>
                                        <li class="divider"></li>
                                        <li>
                                            <asp:LinkButton Font-Size="11px" ID="lnkDelete" runat="server" CommandArgument= '<%#Eval("ID") %>'  OnClick="lnkDelete_Click"  ><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>  
                                        </li>

                                       

                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="False" />
                        <RowStyle HorizontalAlign="Center" />
                    </asp:GridView>
    </div>
</asp:Content>
