<%@ Control Language="vb" Inherits="NTI.Modules.DS2COReportSummary.ViewDS2COReportSummary"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDS2COReportSummary.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="GridData" />
                <telerik:AjaxUpdatedControl ControlID="lblTitle" />
                <telerik:AjaxUpdatedControl ControlID="ddlYear" />
                <telerik:AjaxUpdatedControl ControlID="ddlmonth" />   
                <telerik:AjaxUpdatedControl ControlID="RadChart1" />
                <telerik:AjaxUpdatedControl ControlID="RadChart2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="GridData">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="GridData" />
                <telerik:AjaxUpdatedControl ControlID="ddlYear" />
                <telerik:AjaxUpdatedControl ControlID="ddlmonth" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnSearch">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="GridData" />
                <telerik:AjaxUpdatedControl ControlID="lblTitle" />
                <telerik:AjaxUpdatedControl ControlID="RadChart1" />
                <telerik:AjaxUpdatedControl ControlID="RadChart2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<table style="width: 100%" cellpadding="3" cellspacing="3">
    <tr>
        <td>
            &nbsp;
            <table id="tblRefList" runat="server" border="0" cellpadding="1" cellspacing="0"
                class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp; รายงานการขอหนังสือรับรอง ด้วย Digital Signature&nbsp;</font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;
                                                            </td>
                                                        </telerik:RadCodeBlock>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="m-frm-nav">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="m-frm-hdr">
                                <td style="padding:5px;">
                                    <div style="margin-top: 10px;margin-bottom:10px;border-bottom:1px dotted #666666;padding-bottom:12px;">
                                        <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                            <tr>
                                                <td>
                                                    <font class="FormLabel">&nbsp;ปี:&nbsp;</font>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlYear" runat="server" Filter="StartsWith" MarkFirstMatch="True"
                                                        Skin="Web20" width="80" Font-Names="Tahoma" Font-Size="10pt">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <font class="FormLabel">&nbsp;เดือน:&nbsp;</font>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlMonth" runat="server" Filter="StartsWith" MarkFirstMatch="True"
                                                        Skin="Web20" width="120" Font-Names="Tahoma" Font-Size="10pt">
                                                        <items>
                        <telerik:RadComboBoxItem Text="มกราคม" Value="01" />
                        <telerik:RadComboBoxItem Text="กุมภาพันธ์" Value="02" />
                        <telerik:RadComboBoxItem Text="มีนาคม" Value="03" />
                        <telerik:RadComboBoxItem Text="เมษายน" Value="04" />
                        <telerik:RadComboBoxItem Text="พฤษภาคม" Value="05" />
                        <telerik:RadComboBoxItem Text="มิถุนายน" Value="06" />
                        <telerik:RadComboBoxItem Text="กรกฎาคม" Value="07" />
                        <telerik:RadComboBoxItem Text="สิงหาคม" Value="08" />
                        <telerik:RadComboBoxItem Text="กันยายน" Value="09" />
                        <telerik:RadComboBoxItem Text="ตุลาคม" Value="10" />
                        <telerik:RadComboBoxItem Text="พฤศจิกายน" Value="11" />
                        <telerik:RadComboBoxItem Text="ธันวาคม" Value="12" />
                        <telerik:RadComboBoxItem Text="ทุกเดือน" Value="00" />
                    </items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="font-size:10pt;margin:5px;">สรุปรายงานจำนวนการขอหนังสือสำคัญฯ 
                                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label><div style="width:200px;float:right;text-align:right;display:block;margin-top:-20px;">
                                            <asp:Button runat="server" Text="บันทึกเป็นไฟล์ Excel" 
                                                style="border:1px solid #cccccc;font-size:10ptpadding:3px;" 
                                                CausesValidation="false" ID="btnExport" ></asp:Button></div></div>
                                    <telerik:RadGrid ID="GridData" runat="server" AllowPaging="True"
                                                                GridLines="None" PageSize="20" Skin="Office2007" TabIndex="-1" Width="100%" ShowFooter="True" ShowStatusBar="True" AllowSorting="True">
                                                                <MasterTableView  AutoGenerateColumns="False" ClientDataKeyNames="site_id"
                                                                    DataKeyNames="site_id" NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%"  PagerStyle-FirstPageToolTip="หน้าแรก" PagerStyle-LastPageToolTip="หน้าสุดท้าย"  PagerStyle-NextPageToolTip="หน้าต่อไป" PagerStyle-PageSizeLabelText="แสดงหน้าละ" PagerStyle-PrevPageToolTip="ก่อนหน้านี้" PagerStyle-PagerTextFormat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
                                                                    <Columns>
                                                                    <telerik:GridTemplateColumn HeaderText="&nbsp;" ReadOnly="True"
                                                                            SortExpression="site_name" UniqueName="site_name">
                            <ItemTemplate>
                                <asp:HyperLink ID="linkView" runat="server">
                                <asp:Image ID="imbEditCPA"  ImageUrl="~/images/view.gif" runat="server" ToolTip='<%# Eval("site_name") %>'
                                         />
                                </asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle width="20px" />
                        </telerik:GridTemplateColumn>
                                                                         <telerik:GridBoundColumn DataField="site_id" HeaderText="รหัส" ReadOnly="True"
                                                                            SortExpression="site_id" UniqueName="site_id">
                                                                            <ItemStyle Font-Names="Tahoma" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                        </telerik:GridBoundColumn>
                                                                        
                                                                        <telerik:GridBoundColumn DataField="site_name" FooterText="รวม" HeaderText="สถานที่ออกหนังสือรับรอง" ReadOnly="True"
                                                                            SortExpression="site_name" UniqueName="site_name">
                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Aggregate="Sum"  HeaderStyle-Font-Bold="true" FooterStyle-Font-Bold="true" DataFormatString="{0:#,###,##0}" DataField="TotalWeb" HeaderText="ขอผ่านเว็บไซต์" ReadOnly="True"
                                                                            SortExpression="TotalWeb" UniqueName="TotalWeb">
                                                                            <ItemStyle HorizontalAlign="Center"/>
                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Aggregate="Sum" DataField="TotalXML" HeaderText="ขอด้วยไฟล์ XML" HeaderStyle-Font-Bold="true" FooterStyle-Font-Bold="true" DataFormatString="{0:#,###,##0}" ReadOnly="True"
                                                                            SortExpression="TotalXML" UniqueName="TotalXML">
                                                                            <ItemStyle HorizontalAlign="Center"/>
                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                         <telerik:GridBoundColumn Aggregate="Sum" DataField="Total" HeaderText="รวม" HeaderStyle-Font-Bold="true" FooterStyle-Font-Bold="true" DataFormatString="{0:#,###,##0}" ReadOnly="True"
                                                                            SortExpression="Total" UniqueName="Total">
                                                                            <ItemStyle HorizontalAlign="Center"/>
                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Bold="true"  VerticalAlign="Middle" Height="24px" />
                                                                <ClientSettings EnableRowHoverStyle="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                            <div style="font-size:9pt; color:#666666; font-weight:normal;margin:10px;margin-bottom:20px;">
                                                            หมายเหตุ: ข้อมูลรายงานนี้จะมีการสรุปข้อมูลสถิติในฐานข้อมูลทุกๆ สิ้นเดือน
                                                            </div>
           <div style="text-align:center">                                                 
         <telerik:RadChart ID="RadChart1" runat="server" Width="850px"
           AutoTextWrap="true"  Skin="LightGreen">
          <Legend>
               <Appearance GroupNameFormat="Total"></Appearance>
          </Legend>
          <PlotArea>
               <XAxis DataLabelsColumn="site_name"></XAxis>
          </PlotArea>
          <Series>
              <telerik:ChartSeries DataYColumn="Total" Appearance-TextAppearance-TextProperties-Font="Tahoma" Name="จำนวนหนังสือสำคัญฯ"></telerik:ChartSeries>
          </Series>     
          <ChartTitle>
             <TextBlock Text="กราฟรายงานจำนวนการส่งข้อมูลหนังสือสำคัญฯ แยกตามสาขาสำนักงาน">
               </TextBlock>
          </ChartTitle>
     </telerik:RadChart>
   <br/>
   <telerik:RadChart ID="RadChart2" runat="server" Height="333px" Skin="LightGreen"

          Width="700px"  AutoLayout="true" AutoTextWrap="true" Appearance-LegendDisplayMode="ItemLabels">

          <Series>

               <telerik:ChartSeries Name="หนังสือรับรอง" Type="Pie" DataYColumn="Total">

                    <Appearance LegendDisplayMode="ItemLabels">

                    </Appearance>

               </telerik:ChartSeries>

          </Series>
<ChartTitle>
             <TextBlock Text="สรุปจำนวนโดยแยกประเภทการส่งผ่านเว็บ และ ส่งผ่านไฟล์ข้อมูล XML">
               </TextBlock>
          </ChartTitle>
     </telerik:RadChart>
                             <p>&nbsp;</p>                               
                      </div>                                      
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
