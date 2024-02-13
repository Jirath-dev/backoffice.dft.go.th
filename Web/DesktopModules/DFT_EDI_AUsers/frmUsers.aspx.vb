Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class frmUsers
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'txtTariffCode.Focus()
        'If Not IsPostBack Then
        '    tblRollver.Visible = False
        '    tblMyRollver.Visible = False
        '    LoadCountry(dropcheck_asset_country)
        '    LoadCountry(dropasset_shares_country)
        '    If Request.QueryString("action") = "new" Then
        '        Page.Title = "เพิ่มรายการสินค้า"
        '        lblHeader.Text = "เพิ่มรายการสินค้า"
        '        btnInsert.Visible = True
        '        btnSave.Visible = False
        '        txtInvHRunAuto.Text = Session("ssInvHRunAuto")
        '        dropUnitCode2.SelectedValue = "KGS"
        '    ElseIf Request.QueryString("action") = "view" Then
        '        txtInvHRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("InvHRunAuto"))
        '        txtInvDRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("InvDRunAuto"))
        '        Page.Title = "แสดงรายการสินค้า"
        '        lblHeader.Text = "แสดงรายการสินค้า"
        '        btnInsert.Visible = False
        '        btnSave.Visible = False
        '        Call SetForm()
        '    ElseIf Request.QueryString("action") = "edit" Then
        '        txtInvHRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("InvHRunAuto"))
        '        txtInvDRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("InvDRunAuto"))
        '        Page.Title = "แก้ไขรายการสินค้า"
        '        lblHeader.Text = "แก้ไขรายการสินค้า"
        '        btnInsert.Visible = False
        '        btnSave.Visible = True
        '        Call SetForm()
        '    End If
        'End If
    End Sub

End Class