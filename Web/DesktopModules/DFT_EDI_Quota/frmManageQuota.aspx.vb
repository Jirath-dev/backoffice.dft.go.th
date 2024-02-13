Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class frmManageQuota
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            rdpTSKDate.SelectedDate = Today
            Call LoadTranTypeStock()
            If Request.QueryString("action") = "new" Then
                btnInsert.Visible = True
                btnUpdate.Visible = False
            ElseIf Request.QueryString("action") = "edit" Then
                'Call SetForm(CInt(Request.QueryString("TSK_ID")))
                btnInsert.Visible = False
                btnUpdate.Visible = True
            End If
        End If
    End Sub

    Private Sub LoadTranTypeStock()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT * FROM TransTypeStock WHERE TTS_Form = 'ALL' Order By TTS_Code"
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlTranTypeStock.DataSource = ds.Tables(0)
                ddlTranTypeStock.DataTextField = "TTS_Desc"
                ddlTranTypeStock.DataValueField = "TTS_CODE"
                ddlTranTypeStock.DataBind()
            End If

            ddlTranTypeStock.Items.Insert(0, New ListItem("กรุณาเลือกรายการ", 0))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try
            Dim result As Integer
            result = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "s_Insert_TransStock_NewDS", _
            New SqlParameter("@TSK_YEAR", Left(FunctionUtility.DMY2YMD(rdpTSKDate.SelectedDate.Value), 4)), _
            New SqlParameter("@invh_run_auto", "&nbsp;"), _
            New SqlParameter("@invd_run_auto", "&nbsp;"), _
            New SqlParameter("@TSK_Date", CommonUtility.Get_DateTime(rdpTSKDate.SelectedDate.Value)), _
            New SqlParameter("@TSK_Type1", CommonUtility.Get_StringValue(lblTTS_DC.Text)), _
            New SqlParameter("@TTS_Code", CommonUtility.Get_StringValue(ddlTranTypeStock.SelectedValue)), _
            New SqlParameter("@TSK_Desc", CommonUtility.Get_StringValue(ddlTranTypeStock.SelectedItem.Text)), _
            New SqlParameter("@unit_code", "KGS"), _
            New SqlParameter("@TSK_Debit", CommonUtility.Get_Decimal(txtTSK_Value.Value)), _
            New SqlParameter("@TSK_Credit", CommonUtility.Get_Decimal(0)), _
            New SqlParameter("@TSK_Amount", CommonUtility.Get_Decimal(0)), _
            New SqlParameter("@company_taxno", "&nbsp;"), _
            New SqlParameter("@CompanyName_En", "&nbsp;"), _
            New SqlParameter("@reference_code2", "&nbsp;"), _
            New SqlParameter("@Quantity", CommonUtility.Get_Decimal(0)), _
            New SqlParameter("@ReferenceDesc1", "&nbsp;"), _
            New SqlParameter("@ReferenceDesc2", "&nbsp;"), _
            New SqlParameter("@user_id", CommonUtility.Get_StringValue(Session("ssUserName"))))

            If result = 1 Then
                ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
            Else
                lbl_ErrMSG.Text = "ไม่สามารถบันทึกข้อมูลได้"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub

    Protected Sub ddlTranTypeStock_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTranTypeStock.SelectedIndexChanged
        If ddlTranTypeStock.SelectedValue <> "0" Then
            Dim dr As SqlDataReader
            Dim strCommand As String
            strCommand = "SELECT * FROM TransTypeStock WHERE TTS_CODE = '" & ddlTranTypeStock.SelectedItem.Value & "'"
            dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
            If dr.Read() Then
                lblTTS_DC.Text = CommonUtility.Get_StringValue(dr.Item("TTS_DC"))
                dr.Close()
            End If
        Else
            lblTTS_DC.Text = ""
        End If
    End Sub
End Class