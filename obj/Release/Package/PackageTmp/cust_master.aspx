<%@ Page Title="Customer Master" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="cust_master.aspx.cs" Inherits="AlankarNewDesign.cust_master" %>

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
        <h4 style="color:#1e6099;">CUSTMERS DETAILS</h4>
      
          
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblPartyCode" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblccId" runat="server" Visible="false" Text=""></asp:Label>
            
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px;">
        <ajaxToolkit:TabContainer ID="Tabs" runat="server">
            <ajaxToolkit:TabPanel runat="server" ID="panel1" TabIndex="0" HeaderText="Custmer Info">
                <ContentTemplate>
                   
                            <div class="row" style="padding-top:5px; font-size:10px;">
                                <div class="col-sm-1" >
                                    <b>Cust Code</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPartyCode" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <b>Cust_Name</b>
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtPartyName" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <b>Short_Name</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtShortName" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                                  
                            </div>
                    <div class="row" style="padding-top:5px; font-size:10px;">

                         <div class="col-sm-1">
                                    <b>Vend_Code</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtVENDCODE" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                          <div class="col-sm-1" runat="server" id="Ecclbl" visible="false">
                                    <b>ECC NO</b>
                                </div>
                                <div class="col-sm-2" runat="server" id="Ecctxt" visible="false">
                                    <asp:TextBox ID="txtECCNO" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                          <div class="col-sm-1">
                                    <b>GSTIN NO</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCSTNO" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                        <div class="col-sm-1">
                                    <b>Phone</b>
                                </div>
                                <div class="col-sm-2">
                                    
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="input-sm form-control" MaxLength="12" Font-Size="11px"></asp:TextBox>
                                   <%-- <ajaxToolkit:MaskedEditExtender ID="maskPhone" runat="server" Mask="9999-999999" TargetControlID="txtPhone" />--%>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
  TargetControlID="txtPhone" />
                                </div>
                          <div class="col-sm-1">
                                    <b>Email</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtFax" runat="server" CssClass="input-sm form-control" Font-Size="11px" TextMode="Email"></asp:TextBox>
                                </div>

                    </div>
                    <div class="row" style="padding-top:5px; font-size:10px;">
                         <div class="col-sm-1">
                                    <b>Full Add</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtFL_ADD" TextMode="MultiLine" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                         <div class="col-sm-1">
                                    <b>Division</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtSL_ADD" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                          <div class="col-sm-1">
                                    <b>City</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                          <div class="col-sm-1">
                                    <b>PIN</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPin" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                </div>
                    </div>
                     <div class="row" style="border:1px solid #E7E7E7; margin-left:05px; margin-right:05px; margin-top:10px;"></div>
                    <div class="row" style="padding-top:10px;">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-3">
                            
                            <asp:Button ID="btnPartySave" runat="server" Font-Bold="true" Font-Size="12px" Text="SAVE" CssClass="btn btn-xs btn-info" OnClick="btnPartySave_Click"  />
                            <asp:Button ID="btnNext" runat="server" Font-Bold="true" Font-Size="12px" Text="Next" CssClass="btn btn-xs btn-warning" OnClick="btnNext_Click"  />
                        </div>
                    </div>
                       
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="panel2" TabIndex="1" HeaderText="Contact Person">
                <ContentTemplate>
                    <div class="row" style="padding-top:5px; font-size:10px;">
                        <div class="col-sm-1">
                          <b>Name</b>  
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                        </div>
                         <div class="col-sm-1">
                          <b>Dept</b>  
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                        </div>
                         <div class="col-sm-1">
                          <b>Designation</b>  
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                        </div>
                         <div class="col-sm-1">
                          <b>Mobile</b>  
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px; font-size:10px;">
                          <div class="col-sm-1">
                          <b>Email</b>  
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                        </div>
                         <div class="col-sm-1">
                          <b>Landline</b>  
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtlandline" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                        </div>

                    </div>
                     <div class="row" style="border:1px solid #E7E7E7; margin-left:05px; margin-right:05px; margin-top:10px;"></div>
                    <div class="row" style="padding-top:10px;">
                        <div class="col-sm-1">

                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="btnSaveContact" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-info" Text="SAVE" OnClick="btnSaveContact_Click"  />
                            <asp:Button ID="btnAddnew" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-success" Text="Add new contact" />
                        </div>
                    </div>
                     <div class="row" style="border:1px solid #E7E7E7; margin-left:05px; margin-right:05px; margin-top:10px;"></div>
                    <div class="row" style="padding-top:10px; padding-left:20px;">
                        <b>Contact Persons</b>
                    </div>
                    <div class="row" style="padding-top:20px; padding-left:30px; padding-right:30px;">

                        <asp:GridView ID="GRIDContactPerson" Width="800px"  AutoGenerateColumns="false" CssClass="" runat="server" Font-Names="verdana" Font-Size="11px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No" HeaderStyle-Font-Bold="false">
                                        <ItemTemplate>
                                            <asp:Label ID="serno" runat="server" Text='<%#Container.DataItemIndex +1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CC_ID" Visible="false" HeaderStyle-Font-Bold="false" />
                                    <asp:BoundField DataField="PARTY_CODE" Visible="True" HeaderText="C Code" HeaderStyle-Font-Bold="false" />
                                    <asp:BoundField DataField="CC_NAME" HeaderText="Name" HeaderStyle-Font-Bold="false" />
                                   <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" HeaderStyle-Font-Bold="false" />
                                    <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" HeaderStyle-Font-Bold="false" />
                                    <asp:BoundField DataField="EMAIL"  HeaderText="Email" HeaderStyle-Font-Bold="false"/>
                                    <asp:BoundField DataField="MOBILE" HeaderText="Mobile" HeaderStyle-Font-Bold="false" />
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
                                                <asp:LinkButton ID="LinkButton3"   Font-Size="11px"  CommandArgument='<%#Eval("CC_ID") %>' runat="server" OnClick="LinkButton3_Click" ><i class="glyphicon glyphicon-edit">  </i> EDIT </asp:LinkButton>
                                               
                                            </li>
                                            <li class="divider"></li>
                                            <li>
                                                <%--<asp:HyperLink ID="HyperLink11" runat="server"><i class="glyphicon glyphicon-trash"></i> Delete</asp:HyperLink>--%>
                                                <asp:LinkButton Font-Size="11px" ID="LinkButton1" runat="server"  CommandArgument='<%#Eval ("CC_ID") %>' OnClick="LinkButton1_Click"   ><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>
                                            </li>

                                           
                                           



                                        </ul>
                                    </div>
                                    </ItemTemplate>

<HeaderStyle Font-Bold="False"></HeaderStyle>
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
</asp:Content>
