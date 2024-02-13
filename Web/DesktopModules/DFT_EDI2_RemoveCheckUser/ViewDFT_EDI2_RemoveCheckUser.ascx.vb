Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace NTI.Modules.DFT_EDI2_RemoveCheckUser

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_EDI2_RemoveCheckUser class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_EDI2_RemoveCheckUser
        Inherits Entities.Modules.PortalModuleBase

        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        ''' <summary>
        ''' ฟังก์ชั่นสำหรับลบรายชื่อออกจากแบบคำขอฟอร์ม โดยค้นหาตามเลขที่อ้างอิง
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnSave1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave1.Click
            Try
                Dim ret As Integer
                'update site id
                ret = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_edi_removeCheckDocUser_NewDS2", _
                                                New SqlParameter("@INVH_RUN_AUTO", txtRefNo.Text.Trim()), _
                                                New SqlParameter("@USERNAME", txtUserName1.Text), _
                                                New SqlParameter("@ACTION", 1))
                'show data
                If ret > 0 Then
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Completed - บันทึกข้อมูลเรียบร้อยแล้ว');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Error - ไม่สามารถบันทึกข้อมูลได้');", True)
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' ฟังก์ชั่นสำหรับลบรายชื่อออกจากแบบคำขอฟอร์ม โดยค้นหาตามชื่อเจ้าหน้าที่ที่ค้างอยู่
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnSave2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave2.Click
            Try
                Dim ret As Integer
                'update site id
                ret = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_edi_removeCheckDocUser_NewDS2", _
                                                New SqlParameter("@INVH_RUN_AUTO", ""), _
                                                New SqlParameter("@USERNAME", txtUserName2.Text), _
                                                New SqlParameter("@ACTION", 2))
                'show data
                If ret > 0 Then
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Completed - บันทึกข้อมูลเรียบร้อยแล้ว');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Error - ไม่สามารถบันทึกข้อมูลได้');", True)
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnInvCancle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInvCancle.Click
            Try
                Dim ret As Integer
                'update site id
                ret = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_edi_cancelInvoice_NewDS2", _
                            New SqlParameter("@COMPANY_TAXNO", txtTaxNo.Text.Trim()), _
                            New SqlParameter("@INVOICE_NO", txtInvoiceNo.Text.Trim()))
                'show data
                If ret > 0 Then
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Completed - บันทึกข้อมูลเรียบร้อยแล้ว');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Error - ไม่สามารถบันทึกข้อมูลได้');", True)
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
