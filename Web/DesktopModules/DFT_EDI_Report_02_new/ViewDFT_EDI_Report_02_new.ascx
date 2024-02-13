<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_EDI_Report_02_new.ViewDFT_EDI_Report_02_new" AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_Report_02_new.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager runat="server" id="RadAjaxManager1">
    <ajaxsettings>
           <telerik:AjaxSetting AjaxControlID="rblSystemType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rblReceiptType" />
                <telerik:AjaxUpdatedControl ControlID="rblReportType" />
               
            </UpdatedControls>
            </telerik:AjaxSetting>
              
      </ajaxsettings>

</telerik:RadAjaxManager>
<table cellspacing="5" width="100%">
    <tr>
        <td>
            <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;รายงานตรวจสอบการออกใบเสร็จรับเงิน&#160;&#160;</font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">&#160;&#160;&#160;&#160;
                                                            </td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="m-frm-nav"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="m-frm-hdr">
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">ระบบ :&nbsp;</font>
                                                        </td>
                                                        <td align="left" valign="top">

                                                            <asp:RadioButtonList ID="rblSystemType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                                                <asp:ListItem Text="หนังสือรับรอง" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="ใบอนุญาต" Value="1"></asp:ListItem>
                                                                 <asp:ListItem Text="ระบบบัตร/ขึ้นทะเบียน" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--<asp:RadioButtonList ID="rblSystem" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="หนังสือรับรอง" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="ใบอนุญาต" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="ระบบบัตร/ขึ้นทะเบียน" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <font class="FormLabel"><span style="color: red;">ปิดให้บริการชั่วคราว !</span> <span style="color: blue;">[ขณะนี้เป็นระบบ หนังสือรับรอง]</span>  &nbsp;</font>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">สถานที่ :&nbsp;</font>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <%--<asp:CheckBox ID="CheckAllSite" runat="server" AutoPostBack="True" Font-Names="Tahoma"
                                                                Font-Size="10pt" Text="ทุกสาขา" Height="25px"/>--%>
                                                            <telerik:RadComboBox ID="ddlSiteID" runat="server" AllowCustomText="True" DropDownWidth="300px"
                                                                EmptyMessage="-----กรุณาลือกสถานที่-----" MaxHeight="200px" Width="220px" Filter="Contains">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">ประเภทใบเสร็จ :&nbsp;</font>
                                                        </td>
                                                        <td valign="top" align="left" style="height: 30px">
                                                            <asp:RadioButtonList ID="rblReceiptType" runat="server" RepeatDirection="Horizontal" AutoPostBack="False">
                                                                <asp:ListItem Text="ใบเสร็จเขียว" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="ใบเสร็จเหลือง" Value="1"></asp:ListItem>


                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">สรุปยอดตาม :&nbsp;</font>
                                                        </td>
                                                        <td valign="top" align="left" style="height: 30px">
                                                            <asp:RadioButtonList ID="rblReportType" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="สรุปยอดใบเสร็จแยกตามฟอร์ม" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="สรุปรายละเอียดการออกใบเสร็จรับเงิน" Value="1"></asp:ListItem>

                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">ตั้งแต่วันที่ :&nbsp;</font>
                                                        </td>
                                                        <td align="left" style="height: 40px;" valign="middle">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td valign="top" align="left" style="height: 30px; width: 15%;">
                                                                        <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                                                            Width="170px" Font-Names="Tahoma" Font-Size="10pt" Skin="Outlook">
                                                                            <calendar skin="Outlook" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                                                                                viewselectortext="x" daynameformat="FirstTwoLetters" firstdayofweek="Sunday"
                                                                                showrowheaders="False">
                                                                            </calendar>
                                                                            <datepopupbutton hoverimageurl="" imageurl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                    <td valign="top" align="left" style="height: 30px; width: 3%;">
                                                                        <font class="FormLabel">ถึง&nbsp;&nbsp;</font>
                                                                    </td>
                                                                    <td valign="top" align="left" style="height: 30px; width: 15%;">
                                                                        <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                                            Width="170px" Font-Names="Tahoma" Font-Size="10pt" Skin="Outlook">
                                                                            <calendar skin="Outlook" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                                                                                viewselectortext="x" daynameformat="FirstTwoLetters" firstdayofweek="Sunday"
                                                                                showrowheaders="False">
                                                                            </calendar>
                                                                            <datepopupbutton hoverimageurl="" imageurl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                    <td valign="top" align="left" style="height: 30px; width: 67%;"></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 100px; height: 30px" valign="top"></td>
                                                        <td align="left" style="height: 40px;" valign="middle">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 15%">
                                                                        <asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา"></asp:Button>
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="150px" Visible="False" />
                                                                    </td>
                                                                    <td style="width: 70%"></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lbltotalRecipt_c" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lbltotalRecipt_z" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                            
                            <tr id="trType1" runat="server">
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgReceiptSummary" runat="server" AllowPaging="True" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="23" ShowFooter="True" Visible="False">
                                        <mastertableview autogeneratecolumns="False" cellspacing="-1" width="100%" nomasterrecordstext="ไม่พบรายการที่ทำการค้นหา">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ประเภทฟอร์ม" ReadOnly="True"
                                                SortExpression="FORM_NAME" UniqueName="FORM_NAME">
                                                 <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                 </telerik:GridBoundColumn>
                                                   <%-- <telerik:GridTemplateColumn HeaderText="จำนวน (ชุด)" UniqueName="TemplateColumn">
                                                         <ItemTemplate>
                                                               <asp:Label ID="lblSent_FormName" runat="server" Text='<%#ckeck_total(Eval("FORM_TYPE"), Eval("AMT"),Eval("NUM"))%>'></asp:Label>
                                                          </ItemTemplate>
                                                          <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                                                    </telerik:GridTemplateColumn>--%>
                                                    <telerik:GridNumericColumn DataField="NUM2" ReadOnly="True" HeaderText="จำนวน (ชุด)"
                                                         SortExpression="NUM2" UniqueName="NUM2" DataType="System.Decimal" DataFormatString="{0:F0}" Aggregate="Sum" FooterText="รวม : ">
                                                         <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" Width="150px"  />
                                                           <FooterStyle Font-Names="Tahoma" Width="150px" HorizontalAlign="Right" Font-Size="10pt" />
                                                           <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                      </telerik:GridNumericColumn>
                                                     <telerik:GridNumericColumn DataField="AMT" ReadOnly="True" HeaderText="จำนวนเงิน"
                                                         SortExpression="AMT" UniqueName="AMT" Aggregate="Sum" FooterText="รวม : ">
                                                         <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" Width="150px" />
                                                           <FooterStyle Font-Names="Tahoma" Width="150px" HorizontalAlign="Right" Font-Size="10pt" />
                                                           <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                      </telerik:GridNumericColumn>
                                            </Columns>
                                        </mastertableview>
                                        <headerstyle horizontalalign="Center" font-names="Tahoma" font-size="10pt" font-underline="False" />
                                        <clientsettings enablerowhoverstyle="True"> <Selecting AllowRowSelect="True" />
                                         </clientsettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr id="trType2" runat="server">
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgReceiptDetailSummary" runat="server" AllowPaging="True" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" ShowFooter="True" Visible="False">
                                        <mastertableview autogeneratecolumns="False" cellspacing="-1" width="100%" nomasterrecordstext="ไม่พบรายการที่ทำการค้นหา">
                                                                <GroupByExpressions>
                                                                    <telerik:GridGroupByExpression>
                                                                        <SelectFields>
                                                                            <telerik:GridGroupByField FieldAlias="FORM_NAME" FieldName="FORM_NAME" HeaderText="ประเภทฟอร์ม"
                                                                                FormatString=""></telerik:GridGroupByField>
                                                                        </SelectFields>
                                                                        <GroupByFields>
                                                                            <telerik:GridGroupByField FieldName="FORM_NAME" SortOrder="Descending" FieldAlias="FORM_NAME"
                                                                                FormatString=""></telerik:GridGroupByField>
                                                                        </GroupByFields>
                                                                    </telerik:GridGroupByExpression>
                                                                </GroupByExpressions>
                                                                <Columns>
                                                                    <telerik:GridNumericColumn DataField="bill_no" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                                                                        SortExpression="bill_no" UniqueName="bill_no" Aggregate="Count" FooterText="รวม : ">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </telerik:GridNumericColumn>
                                                                    <telerik:GridBoundColumn DataField="BILL_DATE" HeaderText="วันที่ออกใบเสร็จ" ReadOnly="True"
                                                                        SortExpression="BILL_DATE" UniqueName="BILL_DATE" DataFormatString="{0:d}">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                        ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridNumericColumn DataField="amt" ReadOnly="True" HeaderText="จำนวนเงิน"
                                                                        SortExpression="amt" UniqueName="amt" Aggregate="Sum" FooterText="รวม : ">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                        <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </telerik:GridNumericColumn>
                                                                    <telerik:GridBoundColumn DataField="RECEIPT_NAME" HeaderText="ชื่อนิติบุคคล" ReadOnly="True"
                                                                        SortExpression="RECEIPT_NAME" UniqueName="RECEIPT_NAME">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                    </telerik:GridBoundColumn>
                                                                </Columns>
                                                            </mastertableview>
                                        <headerstyle horizontalalign="Center" font-names="Tahoma" font-size="10pt" font-underline="False" />
                                        <clientsettings enablerowhoverstyle="True">
                                                                <Selecting AllowRowSelect="True" />
                                                            </clientsettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr id="trType3" runat="server">
                        <td>
                            <telerik:RadGrid ID="rgReportList" runat="server" AllowPaging="false" AllowSorting="True"
                                Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ShowStatusBar="True"
                                Skin="Office2007" TabIndex="-1" Width="100%" ShowFooter="true" Visible="False">
                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="ReceiptNo"
                                    DataKeyNames="ReceiptNo" NoMasterRecordsText="ไม่มีรายการคำร้องที่ผ่านารตรวจสอบความถูกต้องของเอกสาร"
                                    Width="100%" ShowGroupFooter="true">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Active_Flag" FieldName="Active_Flag" FormatString=""
                                                    HeaderText="สถานะใบเสร็จรับเงิน"></telerik:GridGroupByField>
                                                
                                            </SelectFields>
                                           
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Active_Flag" HeaderText="" FieldAlias="Active_Flag"
                                                    FormatString=""></telerik:GridGroupByField>
                                               
                                            </GroupByFields>
                                            
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ReceiptNo" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                                            SortExpression="ReceiptNo" UniqueName="ReceiptNo">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Ref_No" HeaderText="Reference No." ReadOnly="True"
                                            SortExpression="Ref_No" UniqueName="Ref_No">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Company_Taxno" HeaderText="เลขประจำตัวผู้เสียภาษี"
                                            SortExpression="Company_Taxno" UniqueName="Company_Taxno">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CompanyName_Th" HeaderText="ชื่อนิติบุคคล (ไทย)"
                                            SortExpression="CompanyName_Th" UniqueName="CompanyName_Th">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COMPANY" HeaderText="กรรมการ" ReadOnly="True"
                                            SortExpression="COMPANY" UniqueName="COMPANY">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PERSON" HeaderText="ผู้รับมอบ" ReadOnly="True"
                                            SortExpression="PERSON" UniqueName="PERSON">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="Card_Count" ReadOnly="True" HeaderText="รวม (บัตร)"
                                            SortExpression="Card_Count" UniqueName="Card_Count" Aggregate="Sum" FooterText="รวม : "
                                            DataFormatString="{0:N0}">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                            <FooterStyle Font-Names="Tahoma" HorizontalAlign="Center" Font-Size="10pt" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Amount" ReadOnly="True" HeaderText="ราคารวม"
                                            SortExpression="Amount" UniqueName="Amount" Aggregate="Sum" FooterText="รวม : "
                                            DataFormatString="{0:N2}">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                            <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridNumericColumn>
                                    </Columns>
                                </MasterTableView>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                <ClientSettings EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>



