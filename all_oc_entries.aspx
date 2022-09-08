<%@ Page Title="All OC Entries" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="all_oc_entries.aspx.cs" Inherits="AlankarNewDesign.all_oc_entries" %>

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

    <script src="js/jquery.js"></script>

    <style type="text/css">
        .band {

    font-size: 21px;
    font-weight: 700;
    line-height: 1;
    color: #000;
    text-shadow: 0 1px 0 #fff;
    filter: alpha(opacity=20);
    opacity: .6;
}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">OC Entries</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblFlag" runat="server" Visible="false" Text=""></asp:Label>
        </center>
        <div class="col-sm--2 col-sm-offset-11" style="margin-right:40px; padding-bottom:10px;">
            <a href="new_oc_entry.aspx" class="btn btn-sm btn-success">Add New Oc</a>
        </div>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:20px; padding-left:10px;  overflow:auto; padding-right:10px;">
         <asp:GridView ID="GridOCENTRY" DataKeyNames="OC_NO" HorizontalAlign="left" Width="1150px" CssClass="GridView1 table table-bordered table-hover table-responsive" AutoGenerateColumns="False"  runat="server" Font-Size="10px" Font-Names="Verdana">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No">
                                      <ItemTemplate>
                                          <%# Container.DataItemIndex +1 %>
                                      </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:BoundField DataField="ID"  Visible="false"  HeaderText="ID"  >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                    </asp:BoundField>
                     <asp:TemplateField  HeaderText="OC NO">
                        <ItemTemplate>
                            <asp:Label ID="lblOCNO" runat="server" Text='<%#Eval("OC_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField  HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("OCDT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField  HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label ID="lblCustCode" runat="server" Text='<%#Eval("Party") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                 
                 
                  <%--  <asp:BoundField DataField="PARTY_CODE" HeaderStyle-Font-Bold="false" HeaderText="PARTY CODE" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                    </asp:BoundField>--%>

                     <asp:TemplateField  HeaderText="Item Code" >
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ITEM_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <%--  <asp:BoundField DataField="ITEM_CODE" HeaderStyle-Font-Bold="false" HeaderText="ITEM CODE" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                    </asp:BoundField>--%>

                    <asp:TemplateField HeaderText="PO" Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblPoNo"  runat="server" Text='<%# Eval("PONO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    




                     <asp:TemplateField HeaderText="PO DATE" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPoDate"  runat="server" Text='<%# Eval("PO_DATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="RATE">
                        <ItemTemplate>
                            <asp:Label ID="lblRate"  runat="server" Text='<%# Eval("NETPRICE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField  HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%#Eval("OCQTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField Visible="false"  HeaderText="Tool Type">
                        <ItemTemplate>
                            <asp:Label ID="lblToolType" Width="200px" runat="server" Text='<%#Eval("TOOLTYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="TOOLTYPE" HeaderStyle-Font-Bold="false" HeaderText="TOOL TYPE" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                    </asp:BoundField>--%>

                     <asp:TemplateField Visible="false"  HeaderText="Sub Type">
                        <ItemTemplate>
                            <asp:Label ID="lblSubType" runat="server" Text='<%#Eval("MATCHTYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="FOCNO" HeaderText="FOC" />
                   <%-- <asp:BoundField  DataField="MATCHTYPE" HeaderStyle-Font-Bold="false" HeaderText="MATCH TYPE">
<HeaderStyle Font-Bold="False"></HeaderStyle>
                    </asp:BoundField>--%>

                   
                 <%--   <asp:BoundField DataField="OCQTY" HeaderStyle-Font-Bold="false" HeaderText="QUANTITY" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                    </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="ACTION" >
                        <ItemTemplate>
                             <div class="btn-group">
                                 <asp:LinkButton ID="LinkButton2" aria-expanded="false" class="dropdown-toggle" data-toggle="dropdown" runat="server">Action</asp:LinkButton>
                                       <%-- <button aria-expanded="false" type="button" class="btn  btn-sm btn-link dropdown-toggle" data-toggle="dropdown">
                                            Action
                                                           <span class="caret"></span>
                                            <span class="sr-only">Toggle Dropdown</span>
                                        </button>--%>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <asp:HyperLink ID="Recall_Link10" Font-Size="11px" runat="server" NavigateUrl='<%#Eval ("OC_NO", "new_oc_entry.aspx?OC_NO={0}&Action=UPDATE") %>'><i class="glyphicon glyphicon-edit"></i> EDIT</asp:HyperLink>
                                            </li>
                                            <li class="divider"></li>
                                            <li>
                                                <%--<asp:HyperLink ID="HyperLink11" runat="server"><i class="glyphicon glyphicon-trash"></i> Delete</asp:HyperLink>--%>
                                               <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="glyphicon glyphicon-trash" CommandArgument='<%#Eval ("OC_NO") %>' >Delete</asp:LinkButton>--%>
                                                <asp:HyperLink ID="LnkQtyUpdation" Font-Size="11px" runat="server" NavigateUrl='<%#Eval("OC_NO", "quantity_updation.aspx?OC_NO={0}&Action=QTYUP") %>'><i class="glyphicon glyphicon-export"></i> QUANTITY UPDATION</asp:HyperLink>
                                            </li>

                                            <li class="divider"></li>
                                            <li>
                                               <asp:HyperLink ID="schedulelink" Font-Size="11px" runat="server" NavigateUrl='<%#Eval("OC_NO", "schedule.aspx?OC_NO={0}&Action=SC") %>'><i class="glyphicon glyphicon-calendar"></i> SCHEDULE</asp:HyperLink>
                                            </li>
                                             <li class="divider"></li>
                                           <%-- <li>
                                                <asp:LinkButton ID="lnkStop" ForeColor="black" CssClass="glyphicon glyphicon-stop" text="Stop"  Font-Size="14px" runat="server"  CommandArgument='<%#Eval("OC_NO") %>' OnClick="lnkStop_Click" ></asp:LinkButton>
                                            </li>
                                            <li class="divider">--%>

                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Click To Delete" CommandArgument='<%#Eval("OC_NO") %>' OnClick="lnkDelete_Click" ><i class="glyphicon glyphicon-trash"></i> Delete</asp:LinkButton>

                                                <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" TargetControlID="lnkDelete" ConfirmText="Do you want to Delete?" />

                                            </li>
                                           
                                            <li>
                                                <asp:LinkButton ID="lnkClose" Visible="false" runat="server" ToolTip="Click To Close OC" CommandArgument='<%#Eval("OC_NO") %>' OnClick="lnkClose_Click" ><i class="glyphicon glyphicon-off"></i> Close</asp:LinkButton>


                                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkClose"
                                                    ConfirmText="Do you want to close this OC?" ConfirmOnFormSubmit="true" />
                                            </li>

                                            <li class="divider"></li>
                                            <li>

                                               
                                                <a  id="btnClose" class="cl" style="cursor:pointer;" data-Val="<%#Eval("OC_NO")%>" data-toggle="modal" data-target="#exampleModal">
                                                  <i class="glyphicon glyphicon-off"></i>  CLOSE OC
                                                </a>


                                            </li>



                                            <li class="divider"></li>
                                            <li>


                                                <a id="btnStart" class="cl" style="cursor: pointer;" data-val="<%#Eval("OC_NO")%>"
                                                    data-toggle="modal" data-target="#exampleModal2">
                                                    <i class="glyphicon glyphicon-off"></i>START OC
                                                </a>


                                            </li>

                                        </ul>
                                    </div>
                        </ItemTemplate>


                    </asp:TemplateField>
                </Columns>
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>
    </div>
    <asp:Label ID="lblOcDt" runat="server" Text="" Visible="false"></asp:Label>
     <div class="container" style="font-family:Verdana; font-size:11px; background-color:wheat;">
                      <asp:LinkButton ID="lnkFake2" runat="server"></asp:LinkButton>
         <ajaxToolkit:ModalPopupExtender ID="STOP_POPUP" runat="server" PopupControlID="PanelOCStop" TargetControlID="lnkFake2"></ajaxToolkit:ModalPopupExtender>

                     <asp:Panel ID="PanelOCStop" CssClass="jumbotron txt" runat="server">
                        <div class="row" style="padding-top:30px;">
                            <div class="col-sm-3">
                                <b style="font-size:11px;">OC No</b>
                            </div>
                            <div class="col-sm-1">
                                <b>
                                    :
                                </b>
                            </div>
                            <div class="col-sm-2" ">
                                <asp:TextBox ID="txtOcStopId" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="row" style="padding-top:20px;">
                             <div class="col-sm-3">
                                 <b style="font-size:11px;">
                                     Remarks
                                 </b>
                             </div>
                             <div class="col-sm-1">
                                 <b>
                                     :
                                 </b>
                             </div>
                             <div class="col-sm-2">
                                 <asp:TextBox ID="txtStopOcRemarks" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="row" style="padding-top:20px;">
                             <div class="col-sm-3"></div>
                            
                                 <asp:Button ID="btnStop" runat="server" CssClass="btn btn-sm btn-danger" Text="STOP" OnClick="btnStop_Click"   />
                           
                           
                                 <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-sm btn-danger" Text="EXIT" OnClick="btnCancel_Click"  />
                             
                         </div>
                     </asp:Panel>
         </div>




  <%--  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
        Launch demo modal
    </button>--%>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title band" id="exampleModalLabel"> Do you want to close this OC </h5><br />
                    <h5>OC NO : <span id="GetVal" class="band"></span></h5>
             
                </div>
                <div class="modal-body">

                    <div id="msg" class="alert alert-success">
                       OC Close Successfully. Please Refresh page.
                           
                    </div>

                    <span id="test"></span>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnNo" class="btn btn-secondary" data-dismiss="modal">No</button>
                    <button type="button" id="OcClose" onclick="_OcClose()" class="btn btn-primary">Yes</button>
                    <button type="button" id="btnclose" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel2"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title band" id="exampleModalLabel2">Do you want to Start this OC
                    </h5>
                    <br />
                    <h5>OC NO : <span id="OCNO" class="band"></span></h5>

                </div>
                <div class="modal-body">

                    <div id="msg2" class="alert alert-success">
                        OC Start Successfully. Please Refresh page.
                           
                    </div>

                    

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnNo_" class="btn btn-secondary" data-dismiss="modal">No</button>
                    <button type="button" id="OcStart" onclick="_OcStart()" class="btn btn-primary">Yes</button>
                    <button type="button" id="btnEsc" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>




    <script type="text/javascript">
        $(document).ready(function () {
          

            
            $("#<%= GridOCENTRY.ClientID%> .cl").on("click", function () {
               
                $("#msg").hide();
                $("#msg2").hide();

                $("#btnclose").hide();
                $("#btnEsc").hide();
                var ocNo = $(this).attr("data-val");

                $("#GetVal").text(ocNo);
                $("#OCNO").text(ocNo);
                $("#OcKr").text(ocNo);

                console.log("OCNo :" + ocNo);
            });


           
          
        });

        function OnSuccess(response) {
            //alert(response.d);

            console.log('Success');
            $("#msg").show();
            $("#OcClose").hide();
            $("#btnclose").show();
            $("#btnNo").hide();
  
        }


        function OnSuccess2(response) {
            //alert(response.d);

            console.log('Success');
            $("#msg2").show();
            $("#OcStart").hide();
            $("#btnEsc").show();
            $("#btnNo_").hide();

        }



        function _OcClose() {
            value = $("#GetVal").text();
            console.log(value);
            $.ajax({
                type: "POST",
                url: "all_oc_entries.aspx/CloseOc",
                data: '{OcNo : "' + value + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });

        };


        function _OcStart() {
            value = $("#OCNO").text();
            console.log(value);
            $.ajax({
                type: "POST",
                url: "all_oc_entries.aspx/StartOc",
                data: '{OcNo : "' + value + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess2,
                failure: function (response) {
                    alert(response.d);
                }
            });

        };

    </script>
</asp:Content>
