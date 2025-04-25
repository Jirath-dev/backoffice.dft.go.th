Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient

Partial Public Class ViewAttachSheet
    Inherits System.Web.UI.Page

    Dim strConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadAttachSheetData()
        End If
    End Sub

    Protected Sub LoadAttachSheetData()
        Dim ds As DataSet

        ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_Get_AttachSheet_File_NewDS", _
                     New SqlParameter("@Invh_run_auto", Request.QueryString("id")))
        If ds.Tables(0).Rows.Count > 0 Then
            lblCompanyName.Text = ds.Tables(0).Rows(0).Item("Company_name").ToString()
            lblInvoice.Text = "INVOICE NO.: " & ds.Tables(0).Rows(0).Item("Invoice_no").ToString()
            Dim Total_FOB As String
            If ds.Tables(0).Rows(0).Item("isThirdInvoice").ToString() = 1 Then
                Total_FOB = ds.Tables(0).Rows(0).Item("Total_Third_FOB").ToString()
                GridData.MasterTableView.Columns(6).Visible = True
                GridData.MasterTableView.Columns(5).Visible = False
            Else
                Total_FOB = ds.Tables(0).Rows(0).Item("Total_Fob").ToString()
                GridData.MasterTableView.Columns(5).Visible = True
                GridData.MasterTableView.Columns(6).Visible = False
            End If
            lblTotal.Text = "จำนวนรายการทั้งหมด = " & ds.Tables(0).Rows(0).Item("Total_Rec").ToString() & " รายการ<br/>" & _
                          "Total Net Weight = " & ds.Tables(0).Rows(0).Item("Total_Quantity").ToString() & " KGM<br/>" & _
                          "Total FOB = " & Total_FOB & " USD"


            GridData.DataSource = LoadData()
            GridData.DataBind()
        Else
            Response.Write("<font style=""color:#f00000;font-size:10pt;"">ไม่พบข้อมูล Attach Sheet</font>")
        End If
    End Sub

    Protected Function LoadData() As DataTable
        Try
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_Get_AttachSheet_Detail_NewDS", _
                         New SqlParameter("@Invh_run_auto", Request.QueryString("id")))

            Return ds.Tables(0)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function


    Private Sub GridData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridData.NeedDataSource
        GridData.DataSource = LoadData()
    End Sub

    Private Sub rgRequestForm_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles GridData.PageIndexChanged
        GridData.CurrentPageIndex = e.NewPageIndex
        GridData.DataSource = LoadData()
        GridData.DataBind()
    End Sub

    Function ConvertPercent(ByVal _Data As String) As String
        Return _Data & "%"
    End Function

    Function ConvertOrigin(ByVal _Origin As Object) As String
        Try
            If CDec(_Origin) Then
                Return CDec(_Origin).ToString("#,##0.00") & " %"
            Else
                Return """" & _Origin & """"
            End If
        Catch ex As Exception
            Return """" & _Origin & """"
        End Try
    End Function

End Class