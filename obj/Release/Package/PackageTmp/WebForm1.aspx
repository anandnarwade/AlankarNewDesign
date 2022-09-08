<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AlankarNewDesign.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row" style="padding-top:20PX;">
                <div class="col-lg-10">
                    <h1 class="page-header">Dashboard</h1>
                </div>
         <div class="col-lg-2">
             <asp:Button ID="btnMail" runat="server" CssClass="btn btn-sm btn-default" Text="Trigger Mail" OnClick="btnMail_Click"  />
         </div>
                <!-- /.col-lg-12 -->
            </div>


    <div class="row">
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-comments fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblAllOc" runat="server" Text=""></asp:Label></div>
                            <div>All OC</div>
                        </div>
                    </div>
                </div>
                <a href="all_oc_entries.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>

                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-red">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-tasks fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblAllNotIssue" runat="server" Text=""></asp:Label></div>
                            <div>Not Issue!</div>
                        </div>
                    </div>
                </div>
                <a href="all_oc_entries.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-yellow">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-shopping-cart fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblIssue" runat="server" Text=""></asp:Label></div>
                            <div>Issue!</div>
                        </div>
                    </div>
                </div>
                <a href="all_oc_entries.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-green">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-support fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge">
                                <asp:Label ID="lblDispatched" runat="server" Text=""></asp:Label></div>
                            <div>Dispatched!</div>
                        </div>
                    </div>
                </div>
                <a href="all_invoice.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col-md-12">

          <!---to grid nahi he to ek min me deto--->

            <div class="col-sm-12" id="bookingDiv" runat="server" visible="false">
                <asp:GridView ID="GridBooking" CssClass="table table-bordered table-condensed table-hover"
                    runat="server" AutoGenerateColumns="false" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex +1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="TOOLTYPE" HeaderText="TOOL TYPE" />

                        <asp:TemplateField HeaderText="W1 QTY">
                            <ItemTemplate>
                                <asp:Label ID="lblW1Qty" runat="server" Text='<% #Eval("W1QTY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="W1 VALUE">
                            <ItemTemplate>
                                <asp:Label ID="lblW1Value" runat="server" Text='<% #Eval("W1VAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="W2 QTY">
                            <ItemTemplate>
                                <asp:Label ID="lblW2Qty" runat="server" Text='<%# Eval("W2QTY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="W2 VALUE">
                            <ItemTemplate>
                                <asp:Label ID="lblW2Value" runat="server" Text='<%# Eval("W2VAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="W3 QTY">
                            <ItemTemplate>
                                <asp:Label ID="lblW3Qty" runat="server" Text='<% #Eval("W3QTY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="W3 VALUE">
                            <ItemTemplate>
                                <asp:Label ID="lblW3Value" runat="server" Text='<%# Eval("W3VAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="W4 QTY">
                            <ItemTemplate>
                                <asp:Label ID="lblW4Qty" runat="server" Text='<%# Eval("W4QTY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="W4 VALUE">
                            <ItemTemplate>
                                <asp:Label ID="lblW4Value" runat="server" Text='<%#Eval("W4VAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="TOTAL QTY">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalQTY" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TOTAL VALUE">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalVAL" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                 <asp:GridView ID="GridDispatched" CssClass="table table-bordered table-condensed table-hover"
                runat="server" AutoGenerateColumns="false" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TOOLTYPE" HeaderText="TOOL TYPE" />

                    <asp:TemplateField HeaderText="W1 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW1Qty" runat="server" Text='<% #Eval("W1QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W1 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW1Value" runat="server" Text='<% #Eval("W1VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="W2 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW2Qty" runat="server" Text='<%# Eval("W2QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W2 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW2Value" runat="server" Text='<%# Eval("W2VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="W3 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW3Qty" runat="server" Text='<% #Eval("W3QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W3 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW3Value" runat="server" Text='<%# Eval("W3VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="W4 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW4Qty" runat="server" Text='<%# Eval("W4QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W4 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW4Value" runat="server" Text='<%#Eval("W4VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="TOTAL QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalQTY" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TOTAL VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalVAL" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
           

           
        </div>


    </div>


</asp:Content>
