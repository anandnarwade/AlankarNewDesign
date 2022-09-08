<%@ Page Title="Employees" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="employees.aspx.cs" Inherits="AlankarNewDesign.employees" %>
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
     <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">EMPLOYEES</h4>
      
            <asp:Button ID="btnAddnew" runat="server" Text="Add New" CssClass="btn btn-sm btn-primary" OnClick="btnAddnew_Click"  />
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px;">
        <asp:GridView ID="GridEMP" CssClass="table table-bordered table-condensed table-hover table-responsive GridView1" AutoGenerateColumns="False" runat="server" Font-Size="11px" HorizontalAlign="Left">
                      
                        <Columns>
                           <%-- <asp:TemplateField>
                                <ItemTemplate>
                                   
                                        <asp:HyperLink CssClass="glyphicon glyphicon-edit" ID="HyperLink1" NavigateUrl='<%#Eval ("EMP_CODE", "AT_EmployeeMaster.aspx?EMP_CODE={0}") %>' runat="server"></asp:HyperLink>
                                     <asp:LinkButton ID="LinkButton1" runat="server" CssClass="glyphicon glyphicon-trash" ForeColor="Red"></asp:LinkButton>
                                   
                                </ItemTemplate>
                               
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Sr No" ItemStyle-Width="70px"    HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom">
                                <ItemTemplate>
                                    <asp:Label ID="SrNO"  runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                            <asp:BoundField DataField="EMP_CODE" ItemStyle-Width="100px" HeaderText="Emp Code" >
                        
                            </asp:BoundField>
                            <asp:BoundField DataField="Employee Name"  ItemStyle-Width="150px" HeaderText="Employee Name" >
                         
                            </asp:BoundField>
                            <asp:BoundField DataField="SEX" HeaderText="Gender" ItemStyle-Width="80px"  >
                           
                            </asp:BoundField>
                            <asp:BoundField DataField="MARITIAL_STATUS" ItemStyle-Width="120px"  HeaderText="Marital Status"  >
                        
                            </asp:BoundField>
                            <asp:BoundField DataField="DOB" ItemStyle-Width="100px" HeaderText="DOB"  >
                          
                            </asp:BoundField>
                            <asp:BoundField DataField="DOJ" ItemStyle-Width="100px" HeaderText="DOJ"  >
                         
                            </asp:BoundField>
                            <asp:BoundField DataField="DOL" HeaderText="DOL" Visible="false" >
                           
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" Visible="true" >
                                <ItemTemplate>
                                    <div class="btn-group">
                                        <asp:LinkButton ID="LinkButton2" aria-expanded="false" class="dropdown-toggle" data-toggle="dropdown" runat="server">Action</asp:LinkButton>
                                        <%--<button aria-expanded="false" type="button" class="btn  btn-sm btn-info dropdown-toggle" data-toggle="dropdown">
                                            Action
                                                           <span class="caret"></span>
                                            <span class="sr-only">Toggle Dropdown</span>
                                        </button>--%>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <asp:HyperLink ID="Recall_Link10" Font-Size="11px" runat="server" NavigateUrl='<%#Eval ("EMP_CODE", "emp_master.aspx?EMP_CODE={0}&Action=UPDATE") %>'><i class="glyphicon glyphicon-edit"></i> EDIT</asp:HyperLink>
                                            </li>
                                            <li class="divider"></li>
                                            <li>
                                                <%--<asp:HyperLink ID="HyperLink11" runat="server"><i class="glyphicon glyphicon-trash"></i> Delete</asp:HyperLink>--%>
                                                <asp:LinkButton ID="lnkDelete" Font-Size="11px" runat="server" CssClass="" CommandArgument='<%#Eval ("EMP_CODE") %>' OnClick="lnkDelete_Click"  > <i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>
                                            </li>

                                            <li class="divider"></li>



                                        </ul>
                                    </div>

                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                        </Columns>  
                      
                    </asp:GridView>
    </div>
</asp:Content>
