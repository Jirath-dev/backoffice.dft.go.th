<%@ Control language="vb" Inherits="YourCompany.Modules.RadModuleTest.ViewRadModuleTest" AutoEventWireup="false" Explicit="True" Codebehind="ViewRadModuleTest.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
ตัวอย่าง Module ที่ใช้ RadGrid. (อย่าลืม 1. ต้องกำหนดค่า <span id="dnn_ctr322_EditModuleControl_plSupportsPartialRendering_lblLabel">
    Supports Partial Rendering? ของ User Control ที่จะให้ Support Ajax ด้วย ไม่อย่างนั้นจะ
    Error 2. ลบ Web.Config ใน Module
    3. ถ้ามีภาษาไทยให้กำหนด Advanced Save Options ให้กำหนดค่าเป็น Unicode (UTF-8 with signature)- Codepage 65001 </span>) จะต้องแก้ไข ConnectionString
ใน Web.Config ที่ชื่อ NorthwindConnectionString1 เพื่อชี้ไปยังที่เก็บ Database อีกครั้ง<br />
&nbsp;<telerik:radajaxmanagerproxy id="RadAjaxManagerProxy1" runat="server"><AjaxSettings>
<telerik:AjaxSetting AjaxControlID="RadGrid1"><UpdatedControls>
<telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
</UpdatedControls>
</telerik:AjaxSetting>
</AjaxSettings>
</telerik:radajaxmanagerproxy>
<telerik:radgrid id="RadGrid1" runat="server" allowpaging="True" allowsorting="True"
    datasourceid="SqlDataSource1" gridlines="None">
<MasterTableView DataKeyNames="ProductID" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" CellSpacing="-1">
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
<Columns>
<telerik:GridBoundColumn DataField="ProductID" ReadOnly="True" HeaderText="ProductID" SortExpression="ProductID" UniqueName="ProductID" DataType="System.Int32"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" UniqueName="ProductName"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="SupplierID" HeaderText="SupplierID" SortExpression="SupplierID" UniqueName="SupplierID" DataType="System.Int32"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="CategoryID" HeaderText="CategoryID" SortExpression="CategoryID" UniqueName="CategoryID" DataType="System.Int32"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="QuantityPerUnit" HeaderText="QuantityPerUnit" SortExpression="QuantityPerUnit" UniqueName="QuantityPerUnit"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice" DataType="System.Decimal"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="UnitsInStock" HeaderText="UnitsInStock" SortExpression="UnitsInStock" UniqueName="UnitsInStock" DataType="System.Int16"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="UnitsOnOrder" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder" UniqueName="UnitsOnOrder" DataType="System.Int16"></telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="ReorderLevel" HeaderText="ReorderLevel" SortExpression="ReorderLevel" UniqueName="ReorderLevel" DataType="System.Int16"></telerik:GridBoundColumn>
<telerik:GridCheckBoxColumn DataField="Discontinued" HeaderText="Discontinued" SortExpression="Discontinued" UniqueName="Discontinued" DataType="System.Boolean"></telerik:GridCheckBoxColumn>
</Columns>
</MasterTableView>
</telerik:radgrid>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString1 %>"
    DeleteCommand="DELETE FROM [Products] WHERE [ProductID] = @ProductID" InsertCommand="INSERT INTO [Products] ([ProductName], [SupplierID], [CategoryID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued]) VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)"
    ProviderName="<%$ ConnectionStrings:NorthwindConnectionString1.ProviderName %>"
    SelectCommand="SELECT [ProductID], [ProductName], [SupplierID], [CategoryID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued] FROM [Products]"
    UpdateCommand="UPDATE [Products] SET [ProductName] = @ProductName, [SupplierID] = @SupplierID, [CategoryID] = @CategoryID, [QuantityPerUnit] = @QuantityPerUnit, [UnitPrice] = @UnitPrice, [UnitsInStock] = @UnitsInStock, [UnitsOnOrder] = @UnitsOnOrder, [ReorderLevel] = @ReorderLevel, [Discontinued] = @Discontinued WHERE [ProductID] = @ProductID">
    <DeleteParameters>
        <asp:Parameter Name="ProductID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="ProductName" Type="String" />
        <asp:Parameter Name="SupplierID" Type="Int32" />
        <asp:Parameter Name="CategoryID" Type="Int32" />
        <asp:Parameter Name="QuantityPerUnit" Type="String" />
        <asp:Parameter Name="UnitPrice" Type="Decimal" />
        <asp:Parameter Name="UnitsInStock" Type="Int16" />
        <asp:Parameter Name="UnitsOnOrder" Type="Int16" />
        <asp:Parameter Name="ReorderLevel" Type="Int16" />
        <asp:Parameter Name="Discontinued" Type="Boolean" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="ProductName" Type="String" />
        <asp:Parameter Name="SupplierID" Type="Int32" />
        <asp:Parameter Name="CategoryID" Type="Int32" />
        <asp:Parameter Name="QuantityPerUnit" Type="String" />
        <asp:Parameter Name="UnitPrice" Type="Decimal" />
        <asp:Parameter Name="UnitsInStock" Type="Int16" />
        <asp:Parameter Name="UnitsOnOrder" Type="Int16" />
        <asp:Parameter Name="ReorderLevel" Type="Int16" />
        <asp:Parameter Name="Discontinued" Type="Boolean" />
        <asp:Parameter Name="ProductID" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>

