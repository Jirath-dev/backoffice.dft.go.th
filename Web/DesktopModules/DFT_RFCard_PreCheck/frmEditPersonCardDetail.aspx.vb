Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class frmEditPersonCardDetail
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Dim strTMPConn As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("action") = "edit" Then
                txtcard_id.Text = CommonUtility.Get_StringValue(Request.QueryString("keycode1")) 'CardNo
                txtCompany_Taxno.Text = CommonUtility.Get_StringValue(Request.QueryString("keycode2")) 'TaxNo
                Page.Title = "รายละเอียดรายการบัตร"
                btnUpdate.Visible = False
                Call SetForm()
            End If
        End If
    End Sub

    Private Sub SetForm()
        Try
            Dim dr As SqlDataReader
            'dr = SqlHelper.ExecuteReader(strConn, CommandType.Text, "select * from P_Rfcard where card_id='" & txtcard_id.Text & "'")
            dr = SqlHelper.ExecuteReader(strTMPConn, CommandType.Text, "select * from P_Rfcard where card_id='" & txtcard_id.Text & "'")
            If dr.HasRows() Then
                dr.Read()
                Me.txtCompany_Taxno.Text = CommonUtility.Get_StringValue(dr.Item("company_taxno"))
                Me.txtCompany_BranchNo.Text = CommonUtility.Get_StringValue(dr.Item("company_branchNo"))
                Me.txtCompanyName.Text = CommonUtility.Get_StringValue(dr.Item("commit_name"))
                Me.txtForm_ID.Text = CommonUtility.Get_StringValue(dr.Item("Form_ID"))
                Me.txtcard_id.Text = CommonUtility.Get_StringValue(dr.Item("card_id"))
                Me.txtAuthName_Thai.Text = CommonUtility.Get_StringValue(dr.Item("AuthName_Thai"))
                Me.txtAuthPersonID.Text = CommonUtility.Get_StringValue(dr.Item("AuthPersonID"))
                Me.txtAuthName.Text = CommonUtility.Get_StringValue(dr.Item("AuthName"))
                Me.txtAuthAddress.Text = CommonUtility.Get_StringValue(dr.Item("AuthAddress"))
                Me.txtAuthTel.Text = CommonUtility.Get_StringValue(dr.Item("AuthTel"))

                Me.rcbCardType.SelectedValue = CommonUtility.Get_StringValue(dr.Item("card_type"))
                Me.rcbSite.SelectedValue = CommonUtility.Get_StringValue(dr.Item("Site_ID"))

                Select Case CommonUtility.Get_StringValue(dr.Item("card_level"))
                    Case "กรรมการผู้จัดการ"
                        'chkCard_level.SelectedValue = "กรรมการผู้จัดการ"
                    Case "ผู้รับมอบอำนาจ ข้อ 1-2"
                        chkCard_level.Items(0).Selected = True
                        chkCard_level.Items(1).Selected = True
                    Case "ผู้รับมอบอำนาจ ข้อ 1-3"
                        chkCard_level.Items(0).Selected = True
                        chkCard_level.Items(1).Selected = True
                        chkCard_level.Items(2).Selected = True
                    Case "ผู้รับมอบอำนาจ ข้อ 1-4"
                        chkCard_level.Items(0).Selected = True
                        chkCard_level.Items(1).Selected = True
                        chkCard_level.Items(2).Selected = True
                        chkCard_level.Items(3).Selected = True
                    Case "ผู้รับมอบอำนาจ ข้อ 1-5", "ผู้รับมอบอำนาจ ข้อ 1-6"
                        chkCard_level.Items(0).Selected = True
                        chkCard_level.Items(1).Selected = True
                        chkCard_level.Items(2).Selected = True
                        chkCard_level.Items(3).Selected = True
                        chkCard_level.Items(4).Selected = True
                End Select
                'Me.rcbCard_level.SelectedValue = CommonUtility.Get_StringValue(dr.Item("card_level"))

                Me.rdpStartDate.SelectedDate = CommonUtility.Get_DateTime(dr.Item("Start_Date"))
                Me.rdpExpireDate.SelectedDate = CommonUtility.Get_DateTime(dr.Item("expire_date"))

                If CommonUtility.Get_StringValue(dr.Item("active_flag")) = "Z" Then
                    Me.chkActiveFlag.Checked = False
                ElseIf CommonUtility.Get_StringValue(dr.Item("active_flag")) = "C" Then
                    Me.chkActiveFlag.Checked = True
                End If

                'txtUserName.Text = CommonUtility.Get_StringValue(dr.Item("UserName"))
                dr.Close()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim ds As New DataSet
            Dim active_flag As String
            If chkActiveFlag.Checked = True Then
                active_flag = "C"
            Else
                active_flag = "Z"
            End If
            ds = SqlHelper.ExecuteDataset(strTMPConn, CommandType.StoredProcedure, "pl_EditCard", _
            New SqlParameter("@card_id", CommonUtility.Get_StringValue(txtcard_id.Text)), _
            New SqlParameter("@company_taxno", CommonUtility.Get_StringValue(txtCompany_Taxno.Text)), _
            New SqlParameter("@company_branchNo", CommonUtility.Get_Int32(txtCompany_BranchNo.Text)), _
            New SqlParameter("@commit_name", CommonUtility.Get_StringValue(txtAuthName.Text)), _
            New SqlParameter("@expire_date", CommonUtility.Get_DateTime(rdpExpireDate.SelectedDate.Value)), _
            New SqlParameter("@password", ""), _
            New SqlParameter("@active_flag", CommonUtility.Get_StringValue(active_flag)), _
            New SqlParameter("@rem_gsp", "Z"), _
            New SqlParameter("@card_statusflag", "Z"), _
            New SqlParameter("@card_level", CommonUtility.Get_StringValue(GetCardLevel())), _
            New SqlParameter("@card_type", CommonUtility.Get_StringValue(rcbCardType.SelectedValue)), _
            New SqlParameter("@AuthName", CommonUtility.Get_StringValue(txtAuthName.Text)), _
            New SqlParameter("@AuthName_Thai", CommonUtility.Get_StringValue(txtAuthName_Thai.Text)), _
            New SqlParameter("@AuthPersonID", CommonUtility.Get_StringValue(txtAuthPersonID.Text)), _
            New SqlParameter("@AuthAddress", CommonUtility.Get_StringValue(txtAuthAddress.Text)), _
            New SqlParameter("@AuthTel", CommonUtility.Get_StringValue(txtAuthTel.Text)), _
            New SqlParameter("@AuthPosition", "Z"), _
            New SqlParameter("@AuthLevel", "Z"), _
            New SqlParameter("@Export_ID", "EX_ID"), _
            New SqlParameter("@Comment", CommonUtility.Get_StringValue(txtAuthName.Text)), _
            New SqlParameter("@Start_Date", CommonUtility.Get_DateTime(rdpStartDate.SelectedDate.Value)), _
            New SqlParameter("@Site_ID", CommonUtility.Get_StringValue(rcbSite.SelectedValue)))

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("retSTATUS") = "0" Then
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function GetCardLevel() As String
        Try
            If chkCard_level.Items(4).Selected = True Then
                Return "ผู้รับมอบอำนาจ ข้อ 1-5"
            End If

            If chkCard_level.Items(3).Selected = True Then
                Return "ผู้รับมอบอำนาจ ข้อ 1-4"
            End If

            If chkCard_level.Items(2).Selected = True Then
                Return "ผู้รับมอบอำนาจ ข้อ 1-3"
            End If

            If chkCard_level.Items(1).Selected = True Then
                Return "ผู้รับมอบอำนาจ ข้อ 1-2"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
End Class