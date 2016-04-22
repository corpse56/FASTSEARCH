<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default"   %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href = "css/my.css" type = "text/css" rel = "stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div style="text-align:center; font-size:50px; padding:5px 50px 50px 50px;">
    Единый поиск по всем каталогам <br/> Библиотеки Иностранной Литературы
    </div><br />
    <div style="text-align:center">
        <asp:TextBox ID="TextBox1" runat="server" Width = "700px" Font-Size = "30px" ></asp:TextBox> &nbsp
        <asp:Button ID="Button1" runat="server" Text="Найти" Font-Size = "23px" 
            onclick="Button1_Click" />
    </div>

    
    <div >
    <br /><br />
    <asp:Panel id = "MainSearchResults" runat="server" style="height:40px; width:100%; padding::10px 0px 10px 0px;background-color:#000000;">
            <asp:Label ID="lMSR" runat="server" Text="" ForeColor="White" Font-Bold="true" Font-Size="30px"></asp:Label>
            
        </asp:Panel>
    <br /><br /><br />
        <asp:Panel id = "bjvvv_colapsediv" runat="server" style="height:60px; width:100%; padding:10px 0px 10px 0px;background-color:#98FB98;">
            <asp:LinkButton ID="lbBJVVV" runat="server" Font-Size="23px">Результаты поиска в базе основного фонда. </asp:LinkButton> 
            &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Image ID="Image1" runat="server" />
            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            
        </asp:Panel>
        
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" 
            TargetControlID="pBJVVV"
            CollapsedSize="0"
            ExpandedSize="700"
            Collapsed="True"
            ExpandControlID="bjvvv_colapsediv"
            CollapseControlID="bjvvv_colapsediv"
            AutoCollapse="False"
            AutoExpand="False"
            ScrollContents="True"
            TextLabelID="Label1"
            CollapsedText="Показать..."
            ExpandedText="Свернуть" 
            ImageControlID="Image1"
            ExpandedImage="~/images/collapse_blue.jpg"
            CollapsedImage="~/images/expand_blue.jpg"> 
        </asp:CollapsiblePanelExtender>
        <asp:Panel ID="pBJVVV" runat="server"   Class = "HideHorizontalScrollBar" style = "overflow-x: hidden;">
        
         <asp:GridView ID="gvBJVVV" runat="server"  Width = "100%"
        AutoGenerateColumns = "False"  BorderWidth="3px"
         BorderStyle="Solid" BorderColor = "Black"
            RowStyle-Wrap ="true"  Font-Size = "20px" CellPadding="3" 
            AllowPaging = "true"  PageSize = "20" 
        onpageindexchanging="gvBJVVV_PageIndexChanging"      >
            
<RowStyle Wrap="True"></RowStyle>
            <Columns>
                
                <asp:BoundField HeaderText="Уровень">
                <HeaderStyle BackColor="Silver" />
                </asp:BoundField>

                <asp:BoundField HeaderText="Автор" >
                <HeaderStyle BackColor="Silver" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Заглавие" >
                <HeaderStyle BackColor="Silver"  />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDetails" runat="server" CommandName="details" 
                            Text='<%#Eval("TIT")%>'  CommandArgument='<%#Eval("IDMAIN")%>' >
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="Место издания" >
                <HeaderStyle BackColor="Silver" />
                </asp:BoundField>
                
                <asp:BoundField HeaderText="Издательство" >
                <HeaderStyle BackColor="Silver" />
                </asp:BoundField>

                <asp:BoundField HeaderText="Дата издания" >
                <HeaderStyle BackColor="Silver" />
                </asp:BoundField>
                <asp:BoundField HeaderText="E-lib" >
                <HeaderStyle BackColor="Silver" />
                </asp:BoundField>
            </Columns>
            
        </asp:GridView>  
    </asp:Panel>

        <br />
  </div>
        <!--<div style="width:300px;height:auto; float:right; display:inline;padding:20px 20px 20px 20px; border-style:solid; border-width:thin; background-color:InactiveBorder; margin:20px 20px 20px 20px;" >
    <div style="text-align:center;font-size:19px">
        Памятка
    </div>
    <p>1. Знак "-" перед словом исключает результаты поиска с этим словом</p>
    <p>2. Можно набирать часть слова</p>
    </div>-->
    </ContentTemplate>
    </asp:UpdatePanel>
    
              
    </form>
</body>
</html>
