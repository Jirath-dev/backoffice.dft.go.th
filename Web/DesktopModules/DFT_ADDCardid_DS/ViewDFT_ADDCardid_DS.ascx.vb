'
' DotNetNukeฎ - http://www.dotnetnuke.com
' Copyright (c) 2002-2013
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Namespace YourCompany.Modules.DFT_ADDCardid_DS

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_ADDCardid_DS class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_ADDCardid_DS
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
#Region "Event Handlers"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Page_Load runs when the control is loaded
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("th-TH")
            txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
            lblError.Text = ""
            If Not Page.IsPostBack Then

            End If
        End Sub

#End Region

#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Registers the module actions required for interfacing with the portal framework
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region
#Region "by rut"
        'ค้นหาเฉพาะ พวกที่ internet Y
        Function DataCompany_byTax(ByVal By_tax As String) As DataSet
            Dim SQL As String = ""
            SQL = "SELECT     * FROM         company WHERE     (company_taxno = @company_taxno) AND (company_internet = 'Y') AND (company_internet_ds_edi = 'Y')"

            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@company_taxno", By_tax)

            Dim ds As DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, SQL, prm)

            Return ds
        End Function

        Function DataRFCard_byTax(ByVal By_tax As String) As String
            Dim SQL As String = ""
            SQL = "SELECT     company_taxno, company_branchNo, card_id, commit_name, expire_date, password, active_flag, AuthName, AuthName_Thai " & _
                    "FROM         rfcard " & _
                    "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Z')"

            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@company_taxno", By_tax)

            Dim ds As DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, SQL, prm)
            Dim Temp As String = ""

            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0)
                    For i As Integer = 0 To .Rows.Count - 1
                        Temp = Temp & .Rows(i).Item("AuthName_Thai") & "[" & .Rows(i).Item("card_id") & "][" & .Rows(i).Item("password") & "]" & "<br/>"

                    Next
                End With
            End If

            Return Temp
        End Function

        Sub FormSet_Com()
            With DataCompany_byTax(txtSearch.Text.Trim).Tables(0)
                If .Rows.Count > 0 Then
                    Panel_Company.Visible = True
                    lblcompany_thai.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_thai"))
                    lblcompany_eng.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_eng"))
                    lblcompany_taxno.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_taxno"))
                    lblStatus.Text = "สมัครใช้งาน company_internet,company_internet_ds_edi แล้ว"
                    'If Date.Now > CommonUtility.Get_DateTime(.Rows(0).Item("Exp_date")) Then
                    '    lblError.Text = "บริษัทไม่ได้ต่ออายุ"
                    '    btnAddCo.Enabled = False
                    'End If
                    lblListCard.Text = DataRFCard_byTax(txtSearch.Text.Trim)
                End If
            End With
        End Sub

        Function DataRFCard_TempString(ByVal By_tax As String) As String
            Dim SQL As String = ""
            SQL = "SELECT     company_taxno, company_branchNo, card_id, commit_name, expire_date, password, active_flag, AuthName, AuthName_Thai " & _
                    "FROM         rfcard " & _
                    "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Z')"

            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@company_taxno", By_tax)

            Dim ds As DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, SQL, prm)
            Dim Temp As String = ""

            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0)
                    For i As Integer = 0 To .Rows.Count - 1
                        If Temp = "" Then
                            Temp = .Rows(i).Item("card_id")
                        Else
                            Temp = Temp & "," & .Rows(i).Item("card_id")
                        End If

                    Next
                End With
            End If

            Return Temp
        End Function
        Function AddRFCardTo_PirotCardId(ByVal By_Tax As String) As Integer
            Dim temp_data As String = ""
            Dim sql_select As String = ""
            sql_select = "SELECT     card_id FROM         PirotCardId"
            Try
                Dim ds_select As DataSet
                ds_select = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql_select)
                With ds_select.Tables(0)
                    If .Rows.Count > 0 Then
                        temp_data = .Rows(0).Item("card_id") & ","
                        txtTempCardAll_inDatabase.Text = temp_data
                    End If
                End With

                'temp_data ต้อง ไม่เท่าค่าว่าง เพื่อที่จะ update
                If txtTempCardAll_inDatabase.Text <> "" Then
                    'temp บัตรมาต่อกัน
                    txtTempCardAll.Text = DataRFCard_TempString(lblcompany_taxno.Text)
                    If txtTempCardAll.Text <> "" Then
                        Dim sql_update As String = ""
                        sql_update = "UPDATE    PirotCardId " & _
                                    "SET              card_id = @card_id"
                        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@card_id", txtTempCardAll_inDatabase.Text & txtTempCardAll.Text)

                        SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, sql_update, prm)

                        Return 9
                    End If
                End If
                Return 1 'กรณี update ไม่ได้เพราะ หาบัตรไม่เจอทั้งสองส่วน
            Catch ex As Exception
                Return 0 'กรณี update ไม่ได้เพราะ error
            End Try
        End Function
#End Region

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            lblError.Text = ""
            With DataCompany_byTax(txtSearch.Text.Trim).Tables(0)
                If .Rows.Count > 0 Then
                    FormSet_Com()
                Else
                    lblError.Text = "ไม่พบข้อมูล"
                End If
            End With
        End Sub

        Protected Sub btnAddCo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCo.Click
            Select Case AddRFCardTo_PirotCardId(txtSearch.Text.Trim)
                Case 0
                    lblError.Text = "Error"
                Case 1
                    lblError.Text = "ไม่สามารถบันทึกข้อมูลได้ เนื่องจากไม่พบเลขบัตร"
                Case 9
                    lblError.Text = "บันทึกข้อมูลเรียบร้อย"
                    btnAddCo.Enabled = False
            End Select
        End Sub
    End Class

End Namespace
