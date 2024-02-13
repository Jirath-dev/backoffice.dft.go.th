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
Namespace YourCompany.Modules.DFT_AddCOTax

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_AddCOTax class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_AddCOTax
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
        Function DataCompany_byTax(ByVal By_tax As String) As DataSet
            Dim SQL As String = ""
            SQL = "SELECT     * FROM         company WHERE     (company_taxno = @company_taxno)"

            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@company_taxno", By_tax)

            Dim ds As DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, SQL, prm)

            Return ds
        End Function

        'check ใน ฐานว่าเคยเพิ่มแล้วหรือยัง
        Function DataCO_byTax(ByVal By_tax As String) As Boolean
            Dim YesOrNoCase As Boolean = False

            Dim SQL As String = ""
            SQL = "SELECT    * FROM         company_volunteer WHERE     (Company_Taxno = @Company_Taxno)"

            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@company_taxno", By_tax)

            Dim ds As DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, SQL, prm)

            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            End If

            Return YesOrNoCase
        End Function
        Sub FormSet_Com()
            With DataCompany_byTax(txtSearch.Text.Trim).Tables(0)
                Panel_Company.Visible = True
                lblcompany_thai.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_thai"))
                lblcompany_eng.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_eng"))
                lblcompany_taxno.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_taxno"))

                Select Case DataCO_byTax(txtSearch.Text.Trim)
                    Case True
                        lblStatus.Text = "สมัครใจแล้ว"
                        btnAddCo.Enabled = False
                    Case False
                        lblStatus.Text = "ยังไม่ได้สมัครใจ"
                        btnAddCo.Enabled = True
                End Select
            End With
        End Sub
        Function AddCo_com(ByVal By_Tax As String) As Boolean
            Dim sql As String = ""
            sql = "INSERT INTO company_volunteer " & _
                    "                      (Company_Taxno) " & _
                    "VALUES     (@Company_Taxno)"
            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@Company_Taxno", By_Tax)
            Try
                SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, sql, prm)

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            lblError.Text = ""
            With DataCompany_byTax(txtSearch.Text.Trim).Tables(0)
                If .Rows.Count > 0 Then
                    'If Date.Now > CommonUtility.Get_DateTime(.Rows(0).Item("Exp_date")) Then
                    '    lblError.Text = "บริษัทนี้ยังไม่ต่ออายุ"
                    '    btnAddCo.Enabled = False
                    '    FormSet_Com()
                    'Else
                    '    FormSet_Com()
                    'End If
                    FormSet_Com()
                Else
                    lblError.Text = "ไม่พบข้อมูล"
                End If
            End With
        End Sub

        Protected Sub btnAddCo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCo.Click
            Select Case AddCo_com(lblcompany_taxno.Text.Trim)
                Case True
                    lblError.Text = "เพิ่มบริษัทสมัครใจแล้ว"
                    btnAddCo.Enabled = False
                    FormSet_Com()
                Case False
                    lblError.Text = "ไม่สามารถเพิ่มบริษัทสมัครใจได้ : กรุณาติดต่อเจ้าหน้าที่"
            End Select
        End Sub
    End Class

End Namespace
