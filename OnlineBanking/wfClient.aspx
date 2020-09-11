<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfClient.aspx.cs" MasterPageFile="~/Site.master" Inherits="wfClient" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
    <asp:Label ID="lblClient" runat="server" Text="Client:"></asp:Label>
    <asp:Label ID="lblClientName" runat="server"></asp:Label>
        <asp:GridView ID="gvClient" runat="server" 
            AutoGenerateColumns="False" AutoGenerateSelectButton="True"  
            onselectedindexchanged="gvClient_SelectedIndexChanged" 
            style="margin-top: 12px"
            >
            <Columns>
                <asp:BoundField DataField="AccountNumber" HeaderText="Account Number" />
                <asp:BoundField DataField="Notes" HeaderText="Account Notes" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" 
                    DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblExceptionMsg" runat="server" ForeColor="#FF3300">To display Error/Exception Message</asp:Label>
    </div>
    </asp:Content>