<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfTransaction.aspx.cs" MasterPageFile="~/Site.master" Inherits="wfTransaction" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <asp:Label ID="lblAccNumber" runat="server" Text="Account Number:"></asp:Label>
    <asp:Label ID="lblAccNumber1" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblBalance" runat="server" Text="Balance:"></asp:Label>
    <asp:Label ID="lblBalance1" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblTransType" runat="server" Text="Transaction Type"></asp:Label>
    <asp:DropDownList ID="ddlTransType" runat="server" 
        onselectedindexchanged="ddlTransType_SelectedIndexChanged" 
        AutoPostBack="True">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="lblAmount" runat="server" Text="Amount: "></asp:Label>
    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
    <asp:RangeValidator ID="rngAmount" runat="server" ControlToValidate="txtAmount" 
        ErrorMessage="Please enter a value between 0.01 and 10000" MaximumValue="10000" 
        MinimumValue="0.01" Type="Double"></asp:RangeValidator>
    <br />
    <br />
    <asp:Label ID="lblTo" runat="server" Text="To:"></asp:Label>
    <asp:DropDownList ID="ddlPayee" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Complete Transaction" 
        onclick="btnSubmit_Click" />
    <br />
    <br />
    <asp:Label ID="lblExceptionMsg" runat="server" 
        Text="[To display error/exception message]" BackColor="White" 
        ForeColor="Red"></asp:Label>
    <br />
    <br />
    
</asp:Content>
