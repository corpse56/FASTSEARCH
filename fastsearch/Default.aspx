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
<br />
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID = "UpdatePanel1">
                    <ProgressTemplate>
                        Вывод результатов...
                    </ProgressTemplate>
                    </asp:UpdateProgress>
    
    <div >
    <br /><br />
    <asp:Panel id = "MainSearchResults" runat="server" style="height:40px; width:100%; padding::10px 0px 10px 0px;background-color:#000000;">
            <asp:Label ID="lMSR" runat="server" Text="" ForeColor="White" Font-Bold="true" Font-Size="30px"></asp:Label>
    </asp:Panel>
    
<!--================================================================BJVVV==============================================================================-->
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
<!--================================================================REDKOSTJ==============================================================================-->
    <br /><br /><br />
        <asp:Panel id = "redkostj_colapsediv" runat="server" style="height:60px; width:100%; padding:10px 0px 10px 0px;background-color:#98FB98;">
            <asp:LinkButton ID="lbREDKOSTJ" runat="server" Font-Size="23px">Результаты поиска в базе редкой книги. </asp:LinkButton> 
            &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Image ID="Image2" runat="server" />
            <br />
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            
        </asp:Panel>
        
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" 
            TargetControlID="pREDKOSTJ"
            CollapsedSize="0"
            ExpandedSize="700"
            Collapsed="True"
            ExpandControlID="redkostj_colapsediv"
            CollapseControlID="redkostj_colapsediv"
            AutoCollapse="False"
            AutoExpand="False"
            ScrollContents="True"
            TextLabelID="Label2"
            CollapsedText="Показать..."
            ExpandedText="Свернуть" 
            ImageControlID="Image2"
            ExpandedImage="~/images/collapse_blue.jpg"
            CollapsedImage="~/images/expand_blue.jpg"> 
        </asp:CollapsiblePanelExtender>
        <asp:Panel ID="pREDKOSTJ" runat="server"   Class = "HideHorizontalScrollBar" style = "overflow-x: hidden;">
        
         <asp:GridView ID="gvREDKOSTJ" runat="server"  Width = "100%"
        AutoGenerateColumns = "False"  BorderWidth="3px"
         BorderStyle="Solid" BorderColor = "Black"
            RowStyle-Wrap ="true"  Font-Size = "20px" CellPadding="3" 
            AllowPaging = "true"  PageSize = "20" onpageindexchanging="gvREDKOSTJ_PageIndexChanging" 
              >
            
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
        <!--================================================================BJACC==============================================================================-->
    <br /><br /><br />
        <asp:Panel id = "bjacc_colapsediv" runat="server" style="height:60px; width:100%; padding:10px 0px 10px 0px;background-color:#98FB98;">
            <asp:LinkButton ID="lbBJACC" runat="server" Font-Size="23px">Результаты поиска в базе Центра Американской Культуры. </asp:LinkButton> 
            &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Image ID="Image3" runat="server" />
            <br />
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            
        </asp:Panel>
        
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" 
            TargetControlID="pBJACC"
            CollapsedSize="0"
            ExpandedSize="700"
            Collapsed="True"
            ExpandControlID="bjacc_colapsediv"
            CollapseControlID="bjacc_colapsediv"
            AutoCollapse="False"
            AutoExpand="False"
            ScrollContents="True"
            TextLabelID="Label3"
            CollapsedText="Показать..."
            ExpandedText="Свернуть" 
            ImageControlID="Image3"
            ExpandedImage="~/images/collapse_blue.jpg"
            CollapsedImage="~/images/expand_blue.jpg"> 
        </asp:CollapsiblePanelExtender>
        <asp:Panel ID="pBJACC" runat="server"   Class = "HideHorizontalScrollBar" style = "overflow-x: hidden;">
        
         <asp:GridView ID="gvBJACC" runat="server"  Width = "100%"
        AutoGenerateColumns = "False"  BorderWidth="3px"
         BorderStyle="Solid" BorderColor = "Black"
            RowStyle-Wrap ="true"  Font-Size = "20px" CellPadding="3" 
            AllowPaging = "true"  PageSize = "20" onpageindexchanging="gvBJACC_PageIndexChanging"  
              >
            
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
        <!--================================================================MARDGANI==============================================================================-->
    <br /><br /><br />
        <asp:Panel id = "mardgani_colapsediv" runat="server" style="height:60px; width:100%; padding:10px 0px 10px 0px;background-color:#98FB98;">
            <asp:LinkButton ID="lbMARDGANI" runat="server" Font-Size="23px">Результаты поиска в базе Марджани. </asp:LinkButton> 
            &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Image ID="Image4" runat="server" />
            <br />
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
            
        </asp:Panel>
        
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" 
            TargetControlID="pMARDGANI"
            CollapsedSize="0"
            ExpandedSize="700"
            Collapsed="True"
            ExpandControlID="mardgani_colapsediv"
            CollapseControlID="mardgani_colapsediv"
            AutoCollapse="False"
            AutoExpand="False"
            ScrollContents="True"
            TextLabelID="Label4"
            CollapsedText="Показать..."
            ExpandedText="Свернуть" 
            ImageControlID="Image4"
            ExpandedImage="~/images/collapse_blue.jpg"
            CollapsedImage="~/images/expand_blue.jpg"> 
        </asp:CollapsiblePanelExtender>
        <asp:Panel ID="pMARDGANI" runat="server"   Class = "HideHorizontalScrollBar" style = "overflow-x: hidden;">
        
         <asp:GridView ID="gvMARDGANI" runat="server"  Width = "100%"
        AutoGenerateColumns = "False"  BorderWidth="3px"
         BorderStyle="Solid" BorderColor = "Black"
            RowStyle-Wrap ="true"  Font-Size = "20px" CellPadding="3" 
            AllowPaging = "true"  PageSize = "20" onpageindexchanging="gvMARDGANI_PageIndexChanging"  
              >
            
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
         <!--================================================================BJFCC==============================================================================-->
    <br /><br /><br />
        <asp:Panel id = "bjfcc_colapsediv" runat="server" style="height:60px; width:100%; padding:10px 0px 10px 0px;background-color:#98FB98;">
            <asp:LinkButton ID="lbBJFCC" runat="server" Font-Size="23px">Результаты поиска в базе Французского Культурного Центра. </asp:LinkButton> 
            &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Image ID="Image5" runat="server" />
            <br />
            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
            
        </asp:Panel>
        
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server" 
            TargetControlID="pBJFCC"
            CollapsedSize="0"
            ExpandedSize="700"
            Collapsed="True"
            ExpandControlID="bjfcc_colapsediv"
            CollapseControlID="bjfcc_colapsediv"
            AutoCollapse="False"
            AutoExpand="False"
            ScrollContents="True"
            TextLabelID="Label5"
            CollapsedText="Показать..."
            ExpandedText="Свернуть" 
            ImageControlID="Image5"
            ExpandedImage="~/images/collapse_blue.jpg"
            CollapsedImage="~/images/expand_blue.jpg"> 
        </asp:CollapsiblePanelExtender>
        <asp:Panel ID="pBJFCC" runat="server"   Class = "HideHorizontalScrollBar" style = "overflow-x: hidden;">
        
         <asp:GridView ID="gvBJFCC" runat="server"  Width = "100%"
        AutoGenerateColumns = "False"  BorderWidth="3px"
         BorderStyle="Solid" BorderColor = "Black"
            RowStyle-Wrap ="true"  Font-Size = "20px" CellPadding="3" 
            AllowPaging = "true"  PageSize = "20" onpageindexchanging="gvBJFCC_PageIndexChanging"  
              >
            
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
            <!--================================================================BRIT_SOVET==============================================================================-->
    <br /><br /><br />
        <asp:Panel id = "britsovet_colapsediv" runat="server" style="height:60px; width:100%; padding:10px 0px 10px 0px;background-color:#98FB98;">
            <asp:LinkButton ID="lbBRIT_SOVET" runat="server" Font-Size="23px">Результаты поиска в базе Британского Совета. </asp:LinkButton> 
            &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Image ID="Image6" runat="server" />
            <br />
            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            
        </asp:Panel>
        
        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server" 
            TargetControlID="pBRIT_SOVET"
            CollapsedSize="0"
            ExpandedSize="700"
            Collapsed="True"
            ExpandControlID="britsovet_colapsediv"
            CollapseControlID="britsovet_colapsediv"
            AutoCollapse="False"
            AutoExpand="False"
            ScrollContents="True"
            TextLabelID="Label6"
            CollapsedText="Показать..."
            ExpandedText="Свернуть" 
            ImageControlID="Image6"
            ExpandedImage="~/images/collapse_blue.jpg"
            CollapsedImage="~/images/expand_blue.jpg"> 
        </asp:CollapsiblePanelExtender>
        <asp:Panel ID="pBRIT_SOVET" runat="server"   Class = "HideHorizontalScrollBar" style = "overflow-x: hidden;">
        
         <asp:GridView ID="gvBRIT_SOVET" runat="server"  Width = "100%"
        AutoGenerateColumns = "False"  BorderWidth="3px"
         BorderStyle="Solid" BorderColor = "Black"
            RowStyle-Wrap ="true"  Font-Size = "20px" CellPadding="3" 
            AllowPaging = "true"  PageSize = "20" onpageindexchanging="gvBRIT_SOVET_PageIndexChanging"  
              >
            
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
    </asp:Panel>           <br />
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
