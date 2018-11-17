<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KlijentiTasks.aspx.cs" Inherits="Advokati.Web.Report.KlijentiTasks1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Izveštaj - klijenti</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>      
            <br/>          
            <div style="width: 260px; margin-bottom: 30px">
                <div style="float: left; width: 200px; margin-left: 10px">
                   <asp:DropDownList ID="DropDownList1" class="form-control" runat="server" DataSourceID="SqlDataSource1" DataTextField="Naziv" DataValueField="Id"></asp:DropDownList>
                   <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:AdvokatiConnectionString %>' SelectCommand="SELECT Id, Naziv FROM Klijents"></asp:SqlDataSource>
                </div>
                <div style="float: left; width: 40px; margin-left: 10px">
                   <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Prikaži" OnClick="btnSubmit_Click" />
                </div>
            </div>
            <br/>
            <br/>
            
        
            <rsweb:ReportViewer ID="ReportViewer1" Width="900" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                <LocalReport ReportPath="Report\ReportKlijenti.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource Name="DataSet1" DataSourceId="SqlDataSource2"></rsweb:ReportDataSource>
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>

            <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:AdvokatiConnectionString %>' SelectCommand="SpGetKlijentTasks" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:FormParameter FormField="DropDownList1" DefaultValue="1" Name="id" Type="Int32"></asp:FormParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
    <a href="http://localhost:12494/Advokats" style="margin-left: 10px">Nazad</a>
</body>
</html>
