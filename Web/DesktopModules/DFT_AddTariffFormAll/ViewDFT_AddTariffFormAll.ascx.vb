'
' DotNetNukeฎ - http://www.dotnetnuke.com
' Copyright (c) 2002-2010
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

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Telerik.Web.UI

Namespace YourCompany.Modules.DFT_AddTariffFormAll

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_AddTariffFormAll class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_AddTariffFormAll
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim reader As SqlDataReader = Nothing
        Dim DSSet As DataSet = Nothing
        Protected SiteUrl As String

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
        Function Get_ListRoles(ByVal ByRoleID As Integer, ByVal ByRoleName As String) As String
            Dim DSRoles As SqlDataReader
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList", _
                            New SqlParameter("@ListRoleNameCase", ByRoleID))

            Dim strListRole As String = ""

            If DSRoles.HasRows Then
                strListRole = ByRoleName
                lblRoleID.Text = strListRole
                Session("ssRoleName") = lblRoleID.Text
            End If

            Return strListRole
        End Function
#End Region
        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            SiteUrl = "http://" & DotNetNuke.Common.GetDomainName(Request)
            txtNumTariff.Focus()
            If Not Page.IsPostBack Then
                Session("UName") = UserInfo.DisplayName
                objConn = New SqlConnection(strConn)
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_get_countryByFormTypeForm1_NewDS", _
                 New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue("FORM1")))
                If dr.HasRows() Then
                    DDLcountry.DataSource = dr
                    DDLcountry.DataBind()
                    dr.Close()
                End If
            End If
            'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

                'by rut 7-09-2555 ใช้วันที่ 22-01-2556 ใช้แทนด้านล่าง ต้องเพิ่มใน Table "RoleList" แทน
                If Get_ListRoles(myRoleInfo.RoleID, myRoleInfo.RoleName) <> "" Then
                    Exit For
                End If

                'Select Case myRoleInfo.RoleID
                '    Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 26, 28
                '        lblRoleID.Text = myRoleInfo.RoleName
                '        Session("ssRoleName") = lblRoleID.Text
                '        Exit For
                'End Select
            Next i
        End Sub
        
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If CommonUtility.Get_StringValue(DDLListForm.SelectedValue) = "FORM2_1" Then
                Grid2.DataSource = Search()
                Grid2.Rebind()

                Grid2.Visible = True
                GridTariff.Visible = False
            Else
                GridTariff.DataSource = Search()
                GridTariff.Rebind()

                Grid2.Visible = False
                GridTariff.Visible = True
            End If
        End Sub

        Function Search() As SqlDataReader
            Try
                If txtNumTariff.Text.Length < 4 And txtNumTariff.Text <> "" Then
                    lblMsg.Text = "กรุณากรอกพิกัดอย่างน้อย 4 ตัว"
                Else
                    lblMsg.Text = ""
                End If
                objConn = New SqlConnection(strConn)
                Select Case CommonUtility.Get_StringValue(DDLListForm.SelectedValue)
                    Case "FORM2_1"
                        reader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_getTariffNew_NewDS", _
                                        New SqlParameter("@TARIFF_CODE", txtNumTariff.Text), _
                                        New SqlParameter("@COUNTRY_CODE", "ZZ"), _
                                        New SqlParameter("@FORM_TYPE", DDLListForm.SelectedItem.Value))
                    Case Else
                        reader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_getTariffNew_NewDS", _
                                        New SqlParameter("@TARIFF_CODE", txtNumTariff.Text), _
                                        New SqlParameter("@COUNTRY_CODE", DDLcountry.SelectedItem.Value), _
                                        New SqlParameter("@FORM_TYPE", DDLListForm.SelectedItem.Value))
                End Select
                
                Return reader
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        'Function Search() As DataSet
        '    Try
        '        If txtNumTariff.Text.Length < 4 And txtNumTariff.Text <> "" Then
        '            lblMsg.Text = "กรุณากรอกพิกัดอย่างน้อย 4 ตัว"
        '        Else
        '            lblMsg.Text = ""
        '        End If
        '        objConn = New SqlConnection(strConn)
        '        Select Case CommonUtility.Get_StringValue(DDLListForm.SelectedValue)
        '            Case "FORM2_1"
        '                DSSet = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_common_getTariffNew_NewDS", _
        '                                New SqlParameter("@TARIFF_CODE", txtNumTariff.Text), _
        '                                New SqlParameter("@COUNTRY_CODE", "ZZ"), _
        '                                New SqlParameter("@FORM_TYPE", DDLListForm.SelectedItem.Value))
        '            Case Else
        '                DSSet = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_common_getTariffNew_NewDS", _
        '                                New SqlParameter("@TARIFF_CODE", txtNumTariff.Text), _
        '                                New SqlParameter("@COUNTRY_CODE", DDLcountry.SelectedItem.Value), _
        '                                New SqlParameter("@FORM_TYPE", DDLListForm.SelectedItem.Value))
        '        End Select

        '        Return DSSet
        '    Catch ex As Exception
        '        Return Nothing
        '    End Try
        'End Function

        Private Sub GridTariff_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTariff.DataBound
            'reader.Close()
            'objConn.Close()
        End Sub

        Private Sub GridTariff_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles GridTariff.ItemCreated
            If TypeOf e.Item Is GridDataItem Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowViewForm('{0}','{1}','{2}','{3}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), "view", CommonUtility.Get_StringValue(DDLListForm.SelectedValue))

                Dim editLink As HyperLink = DirectCast(e.Item.FindControl("EditLink"), HyperLink)
                editLink.Attributes("href") = "#"
                editLink.Attributes("onclick") = [String].Format("return ShowEditForm('{0}','{1}','{2}','{3}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), "edit", CommonUtility.Get_StringValue(DDLListForm.SelectedValue))


                Dim deleteLink As HyperLink = DirectCast(e.Item.FindControl("DeleteLink"), HyperLink)
                
                If CommonUtility.Get_StringValue(DDLListForm.SelectedValue) = "FORM2_1" Then
                    deleteLink.Attributes("href") = "#"
                    deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteForm21('{0}','{1}','{2}','{3}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("cat"), CommonUtility.Get_StringValue(DDLListForm.SelectedValue))
                Else
                    deleteLink.Attributes("href") = "#"
                    deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteForm('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), CommonUtility.Get_StringValue(DDLListForm.SelectedValue))
                End If
            End If
        End Sub

        Private Sub GridTariff_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridTariff.NeedDataSource
            If CommonUtility.Get_StringValue(DDLListForm.SelectedValue) = "FORM2_1" Then
                Grid2.DataSource = Search()
                Grid2.Visible = True
                GridTariff.Visible = False
            Else
                GridTariff.DataSource = Search()
            End If

            
        End Sub

        Function Form_selectTariff(ByVal Form_) As String
            Dim str_store As String
            Select Case Form_
                Case "FORM1"
                    str_store = "sp_common_get_countryByFormTypeForm1_NewDS"
                Case "FORM1_1"
                    str_store = "sp_common_get_countryByFormTypeFORM1_1_NewDS"
                Case "FORM1_2"
                    str_store = "sp_common_get_countryByFormTypeFORM1_1_NewDS"
                Case "FORM1_3"
                    str_store = "sp_common_get_countryByFormTypeFORM1_3_NewDS"
                Case "FORM1_4"
                    str_store = "sp_common_get_countryByFormTypeFORM1_4_NewDS"
                Case "FORM2"
                    str_store = "sp_common_get_countryByFormTypeFORM2_NewDS"
                Case "FORM2_1"
                    str_store = "sp_common_get_countryByFormTypeFORM2_1_NewDS"
                Case "FORM2_2"
                    str_store = "sp_common_get_countryByFormTypeFORM2_2_NewDS"
                Case "FORM2_3"
                    str_store = "sp_common_get_countryByFormTypeFORM2_3_NewDS"
                Case "FORM2_4"
                    str_store = "sp_common_get_countryByFormTypeFORM2_4_NewDS"
                Case "FORM3"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM3_1"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4"
                    str_store = "sp_common_get_countryByFormTypeFORM4_NewDS"

                    'By Tine
                Case "FORM44_4"
                    str_store = "sp_common_get_countryByFormTypeFORM44_4_NewDS"
                Case "FORM44_41"
                    str_store = "sp_common_get_countryByFormTypeFORM44_41_NewDS"
                Case "FORM44_44"
                    str_store = "sp_common_get_countryByFormTypeForm44_44_NewDS"
                Case "FORM44"
                    str_store = "sp_common_get_countryByFormTypeForm44_NewDS"
                Case "FORM441_4"
                    str_store = "sp_common_get_countryByFormTypeFORM441_4_NewDS"
                Case "FORM441"
                    str_store = "sp_common_get_countryByFormTypeForm441_NewDS"

                Case "FORM4_1"
                    str_store = "sp_common_get_countryByFormTypeForm4_1_NewDS"
                Case "FORM4_2"
                    str_store = "sp_common_get_countryByFormTypeForm4_2_NewDS"
                Case "FORM4_3"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4_4"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4_5"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4_6"
                    str_store = "sp_common_get_countryByFormTypeForm4_6_NewDS"
                    'By Tine
                Case "FORM4_61"
                    str_store = "sp_common_get_countryByFormTypeForm4_61_NewDS"

                Case "FORM4_8"
                    str_store = "sp_common_get_countryByFormTypeForm4_8_NewDS"
                    'By Tine
                Case "FORM4_81"
                    str_store = "sp_common_get_countryByFormTypeForm4_81_NewDS"

                Case "FORM4_9"
                    str_store = "sp_common_get_countryByFormTypeForm4_9_NewDS"

                Case "FORM5"
                    str_store = "sp_common_get_countryByFormTypeFORM5_NewDS"
                    'by rut
                Case "FORM5_1"
                    str_store = "sp_common_get_countryByFormType_NewDS"

                    ''ByTine 28-10-2558 เพิ่มฟอร์มใหม่ไทย-ชิลี
                Case "FORM5_2"
                    str_store = "sp_common_get_countryByFormTypeForm5_2_NewDS"

                Case "FORM6"
                    str_store = "sp_common_get_countryByFormTypeFORM6_NewDS"
                Case "FORM7"
                    str_store = "sp_common_get_countryByFormTypeFORM7_NewDS"
                Case "FORM8"
                    str_store = "sp_common_get_countryByFormTypeFORM8_NewDS"
                Case "FORM9"
                    str_store = "sp_common_get_countryByFormTypeFORM9_NewDS"
                Case "FORMRussia"
                    str_store = "sp_common_get_countryByFormTypeFORMRussia_NewDS"
            End Select
            Return str_store
        End Function
        Protected Sub DDLListForm_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles DDLListForm.SelectedIndexChanged
            objConn = New SqlConnection(strConn)
            Dim dr As SqlDataReader

            dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, Form_selectTariff(CommonUtility.Get_StringValue(DDLListForm.SelectedItem.Value)), _
             New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(DDLListForm.SelectedItem.Value)))

            If dr.HasRows() Then
                DDLcountry.DataSource = dr
                DDLcountry.DataBind()
                Select Case CommonUtility.Get_StringValue(DDLListForm.SelectedItem.Value)
                    Case "FORM2_1"
                        DDLcountry.Enabled = False
                    Case Else
                        DDLcountry.Enabled = True
                End Select

            End If
        End Sub

        Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            GridTariff.MasterTableView.SortExpressions.Clear()
            GridTariff.MasterTableView.GroupByExpressions.Clear()
            GridTariff.Rebind()
            If CommonUtility.Get_StringValue(DDLListForm.SelectedValue) = "FORM2_1" Then
                Grid2.MasterTableView.SortExpressions.Clear()
                Grid2.MasterTableView.GroupByExpressions.Clear()
                Grid2.Rebind()
                Grid2.Visible = True
                GridTariff.Visible = False
            End If

            reader.Close()
            objConn.Close()
        End Sub

        Private Sub Grid2_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles Grid2.ItemCreated
            If TypeOf e.Item Is GridDataItem Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowViewForm21('{0}','{1}','{2}','{3}','{4}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), "view", CommonUtility.Get_StringValue(DDLListForm.SelectedValue), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("cat"))

                Dim editLink As HyperLink = DirectCast(e.Item.FindControl("EditLink"), HyperLink)
                editLink.Attributes("href") = "#"
                editLink.Attributes("onclick") = [String].Format("return ShowEditForm21('{0}','{1}','{2}','{3}','{4}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), "edit", CommonUtility.Get_StringValue(DDLListForm.SelectedValue), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("cat"))


                Dim deleteLink As HyperLink = DirectCast(e.Item.FindControl("DeleteLink"), HyperLink)

                If CommonUtility.Get_StringValue(DDLListForm.SelectedValue) = "FORM2_1" Then
                    deleteLink.Attributes("href") = "#"
                    deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteForm21('{0}','{1}','{2}','{3}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("cat"), CommonUtility.Get_StringValue(DDLListForm.SelectedValue))
                Else
                    deleteLink.Attributes("href") = "#"
                    deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteForm('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("country_code"), CommonUtility.Get_StringValue(DDLListForm.SelectedValue))
                End If
            End If
        End Sub
    End Class

End Namespace
