<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateCustomFields.aspx.cs" Inherits="SitefinityWebApp.Pages.MakeMeMeta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="ScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadSkinManager runat="server" Skin="Metro"></telerik:RadSkinManager>
    <div>
  
    <h2>Create A Meta Type</h2>
    <p>This type will now support custom fields</p>
    <telerik:RadComboBox runat="server" ID="TypesComboBox" Width="200px" ></telerik:RadComboBox>
      
    <telerik:RadButton ID="Button1" runat="server" Text="Well Do it" onclick="Button1_Click" />
    <br />
    <asp:Literal
        ID="MetaTypeResult" runat="server"></asp:Literal>
    </div>
    <div>
        <h2>Create A Meta Field</h2>
        <p>This type will then have a field with the name you give it</p>
    <telerik:RadTextBox ID="TextBox1" Placeholder="Enter name of custom field" runat="server"></telerik:RadTextBox>
     <telerik:RadComboBox runat="server" ID="ClrTypesBox" Width="200px" ></telerik:RadComboBox>
    <Telerik:RadButton ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Create My CustomField" />
        <br />
            <asp:Literal
        ID="MetaFieldResult" runat="server"></asp:Literal>


    </div>
    <asp:Panel runat="server" ID="SubscriberTest">
    <div>
    <h2>Experiment with the custom field of the subscriber</h2>
    <p>Do this after you have made sure that the type is meta and it has a field called company</p>
        <telerik:RadTextBox runat="server" ID="ComboBoxValueField" Placeholder="Enter Value"></telerik:RadTextBox>
        <telerik:RadComboBox ID="SubscriberList" runat="server" DataTextField="FirstName" DataValueField="Id"></telerik:RadComboBox>
        <telerik:RadButton runat="server" ID="ChangeCustomField" 
            Text="Change Subscriber Custom field" onclick="ChangeCustomField_Click"></telerik:RadButton>
    </div>

    <asp:Label runat="server" ID="SubscriberField"></asp:Label>
    </asp:Panel>
    </form>
    
</body>
</html>
