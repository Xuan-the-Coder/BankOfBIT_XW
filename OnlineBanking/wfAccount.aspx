<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfAccount.aspx.cs" MasterPageFile="~/Site.master" Inherits="wfAccount" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
    <asp:Label ID="lblClient" runat="server" Text="Client: "></asp:Label>
    <asp:Label ID="lblClientName" runat="server"></asp:Label>
    <br />
        <asp:Label ID="lblAccountNumber" runat="server" Text="Account Number:"></asp:Label>
        <asp:Label ID="lblNumber" runat="server"></asp:Label>
        <asp:Label ID="lblBalance" runat="server" Text="Balance:"></asp:Label>
        <asp:Label ID="lblBalanceAmount" runat="server"></asp:Label>
    <br />
        <asp:GridView ID="gvAccount" runat="server" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="DateCreated" HeaderText="Date" />
                <asp:BoundField DataField="TransactionTypeId" HeaderText="Transaction Type" />
                <asp:BoundField DataField="Deposit" HeaderText="Amount In" 
                    DataFormatString="{0:C}" />
                <asp:BoundField DataField="Withdrawal" HeaderText="Amount Out" 
                    DataFormatString="{0:C}" />
                <asp:BoundField DataField="Notes" HeaderText="Details" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkClicked">Pay Bills and Transfer Funds</asp:LinkButton>
        <br />
        <asp:Label ID="lblExceptionMsg" runat="server" ForeColor="#FF3300">To display Error/Exception Message</asp:Label>
    </div>
    </asp:Content>
