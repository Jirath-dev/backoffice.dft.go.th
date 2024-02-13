Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class rptF04_Report_07
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim index As Integer = 0

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        index = index + 1
        txtIndex.Text = CType(index, String)
        txtPrintDate.Text = "วันที่พิมพ์ : " & Convert.ToDateTime(Now).ToString("D", New System.Globalization.CultureInfo("th-TH"))
        txtApprove_Date.Text = Convert.ToDateTime(txtApprove_Date.Text).ToString("d", New System.Globalization.CultureInfo("en-GB"))

        txtInvoice_No.Text = GetInvoice_No(txtInvoice_No.Text)


    End Sub

    Private Sub GroupHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupHeader1.Format
        index = 0
    End Sub

    Function GetInvoice_No(ByVal _invh_run_auto As String) As String
        Try
            Dim isFirst As Boolean = True
            Dim strInvoice As String = ""
            Dim strCommand As String
            strCommand = "Select * From form_header_edi Where invh_run_auto = '" & _invh_run_auto & "'"
            Dim dr As SqlDataReader
            dr = SqlHelper.ExecuteReader(strConn, CommandType.Text, strCommand)
            If dr.HasRows() Then
                dr.Read()

                If Not (dr.Item("invoice_no1").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no1")) = "") Then
                    If isFirst Then
                        strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no1"))
                        isFirst = False
                    Else
                        strInvoice &= "," & CommonUtility.Get_StringValue(dr.Item("invoice_no1"))
                    End If
                End If

                If Not (dr.Item("invoice_no2").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no2")) = "") Then
                    If isFirst Then
                        strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no2"))
                        isFirst = False
                    Else
                        strInvoice &= "," & CommonUtility.Get_StringValue(dr.Item("invoice_no2"))
                    End If
                End If

                If Not (dr.Item("invoice_no3").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no3")) = "") Then
                    If isFirst Then
                        strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no3"))
                        isFirst = False
                    Else
                        strInvoice &= "," & CommonUtility.Get_StringValue(dr.Item("invoice_no3"))
                    End If
                End If

                If Not (dr.Item("invoice_no4").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no4")) = "") Then
                    If isFirst Then
                        strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no4"))
                        isFirst = False
                    Else
                        strInvoice &= "," & CommonUtility.Get_StringValue(dr.Item("invoice_no4"))
                    End If
                End If

                If Not (dr.Item("invoice_no5").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no5")) = "") Then
                    If isFirst Then
                        strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no5"))
                        isFirst = False
                    Else
                        strInvoice &= "," & CommonUtility.Get_StringValue(dr.Item("invoice_no5"))
                    End If
                End If

                Return strInvoice
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
