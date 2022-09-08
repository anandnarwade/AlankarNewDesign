<%@ Page Title="Issue Load Report" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="IssueLoadRpt.aspx.cs" Inherits="AlankarNewDesign.IssueLoadRpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-6">
            <h4>Work in process</h4>
        </div>
    </div>
    <hr />
    <div class="row" style="padding-top:10px; padding-left:20px; padding-right:20px;">
        <table class="GridView1 table table-bordered table-hover table-responsive dataTable no-footer table-striped">
            <tr style="background-color:slategray; color:white;">
                <td  style="background-color:slategray; color:white;">

                </td>
                <td>
                    <asp:HyperLink ID="lblDrill" runat="server" ForeColor="White" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblReamer" runat="server"  ForeColor="White" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblEndMill" runat="server"  ForeColor="White" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblMillingCutter" runat="server"  ForeColor="White" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblBoaringBar" runat="server"  ForeColor="White" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblCbore" runat="server"  ForeColor="White" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblFlatTool" runat="server"  ForeColor="White" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <b style="font-size:12px;">Total</b>
                </td>
            </tr>
             <tr>
                <td  style="background-color:slategray; color:white;">
                  
                    <asp:HyperLink ID="lblStageSeq10" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                    
                </td>
                <td>
                
                    <asp:HyperLink ID="lbl_s_10_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                   
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_10_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                    
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_10_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                    
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_10_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                     
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_10_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                    
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_10_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                    
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_10_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                    
                </td>
                <td>
                   <asp:HyperLink ID="lbl_s10_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq20" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_20_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_20_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_20_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_20_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_20_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_20_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_20_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                      <asp:HyperLink ID="lbl_s20_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq30" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_30_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_30_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_30_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_30_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_30_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_30_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_30_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                     <asp:HyperLink ID="lbl_s30_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>

            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq40" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_40_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_40_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_40_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_40_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_40_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_40_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_40_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s40_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>

            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq50" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_50_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_50_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_50_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_50_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_50_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_50_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_50_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                   <asp:HyperLink ID="lbl_s50_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>


            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq60" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_60_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_60_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_60_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_60_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_60_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_60_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_60_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s60_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>


            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq70" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_70_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_70_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_70_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_70_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_70_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_70_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_70_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    
                   <asp:HyperLink ID="lbl_s70_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>

            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq80" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_80_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_80_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_80_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_80_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_80_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_80_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_80_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s80_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>


            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq90" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_90_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_90_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_90_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_90_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_90_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_90_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_90_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s90_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>

            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq100" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_100_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_100_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_100_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_100_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_100_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_100_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_100_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s100_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>


            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq110" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_110_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_110_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_110_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_110_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_110_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_110_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_110_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                   <asp:HyperLink ID="lbl_s110_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>

            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq120" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_120_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_120_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_120_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_120_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_120_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_120_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_120_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                      <asp:HyperLink ID="lbl_s120_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>

            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq130" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_130_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_130_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_130_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_130_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_130_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_130_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_130_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                     <asp:HyperLink ID="lbl_s130_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>

            <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="lblStageSeq140" runat="server"  ForeColor="White" Text="" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_140_t_10" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_140_t_20" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_140_t_30" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_140_t_40" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_140_t_50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_140_t_60" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s_140_t_70" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lbl_s140_Total" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
            </tr>



             <tr>
                <td  style="background-color:slategray; color:white;">
                    <asp:HyperLink ID="Total" runat="server"  ForeColor="White" Text="Total" Font-Bold="true" Font-Size="12px" ></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblTotalTool10"  runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblTotalTool20"   runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblTotalTool30"  runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblTotalTool40"  runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblTotalTool50" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblTotalTool60"   runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td>
                    <asp:HyperLink ID="lblTotalTool70"  runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:HyperLink>
                </td>
                <td style="background-color:cornsilk;">
                    <asp:HyperLink ID="lblGrandTotal"  runat="server" Font-Bold="true" Font-Size="15px" Text=""></asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
