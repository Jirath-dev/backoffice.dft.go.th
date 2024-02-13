Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports System
Imports System.Data
Imports System.Collections
Imports System.ComponentModel
Imports System.IO

Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration

<WebService(Namespace:="http://BackOffice.dft.go.th/WebServices/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DataService
    Inherits System.Web.Services.WebService

    Dim SecurityCode As String = "B3307CC2-9FD8-40c6-8A9F-A79535DCBFB9"
    Dim strConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Function CheckSecurityCode(ByVal RequestSecurity As String) As Boolean
        If RequestSecurity = SecurityCode Then
            Return True
        Else
            Return False
        End If
    End Function

    <WebMethod(Description:="ฟังก์ชั่นสำหรับดึงข้อมูลเกี่ยวกับบริษัท")> _
    Public Function GetCompanyInfo(ByVal CardID As String, ByVal SecurityCode As String) As DataSet
        Try
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_check_All_NewDS", New SqlParameter("@card_id", CardID))
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
	
	
    <WebMethod(Description:="ฟังก์ชั่นสำหรับดึงข้อมูลรายการฟอร์มที่ผ่านการอนุมัติแล้วเพื่อสั่งพิมพ์")> _
       Public Function GetRequestFormList(ByVal CompanyTax As String, ByVal SiteID As String, ByVal CardID As String, ByVal OptPrint As Integer, ByVal SecurityCode As String) As DataSet
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "vi_form_edi_getForPrint_NewDS", _
                    New SqlParameter("@COMPANY_TAXNO", CompanyTax), _
                    New SqlParameter("@FORM_TYPE", "ALL"), _
                    New SqlParameter("@FROM_DATE", "20000101"), _
                    New SqlParameter("@TO_DATE", "25001231"), _
                    New SqlParameter("@INVOICE_NO", ""), _
                    New SqlParameter("@DISPLAY_FLAG", OptPrint), _
                    New SqlParameter("@SITE_ID", SiteID))
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    <WebMethod(Description:="ฟังก์ชั่นสำหรับดึงข้อมูลรายละเอียดสินค้า")> _
        Public Function GetDataPreview(ByVal ReferenceNo As String, ByVal SiteID As String, ByVal SecurityCode As String) As DataSet
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "vi_form4_edi_printFormBar_NewDS", _
                        New SqlParameter("@INVH_RUN_AUTO", ReferenceNo), _
                        New SqlParameter("@SITE_ID", SiteID))
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


	
End Class
