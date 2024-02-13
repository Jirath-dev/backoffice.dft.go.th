Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient

Imports Telerik.Web.UI
Namespace Nti.Modules.DFT_EDI_AddPrint

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_EDI_AddPrint class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_EDI_AddPrint
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable

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
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim _UserName As String = CommonUtility.Get_StringValue(DotNetNuke.Entities.Users.UserController.GetUser(PortalId, UserId, False).Username)
        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            Try
                If CallSite_DS.Tables(0).Rows.Count > 0 Then
                    DDL_Site.DataSource = CallSite_DS.Tables(0)
                    DDL_Site.DataTextField = "site_name"
                    DDL_Site.DataValueField = "site_id"
                    DDL_Site.DataBind()

                    CallUserSite_DS(DDL_Site.SelectedValue)
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                PanelReq.Visible = True
            End If
        End Sub
        'แสดงชื่อ site ต่างๆ เพื่อทำการค้นหา username 
        Function CallSite_DS() As DataSet
            Dim ds As New DataSet

            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_siteByAll_NewDS")

            Return ds
        End Function
        'เอา site มาหาชื่อ username เพื่อให้แสดงให้เลือกเครื่องเอาตาม username นั้น
        Sub CallUserSite_DS(ByVal str_site As String)
            Dim ds As New DataSet

            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_UserPrintBySite_NewDS", New SqlParameter("@site_id", str_site))
            If ds.Tables(0).Rows.Count > 0 Then
                DDLUserName.DataSource = ds.Tables(0)
                DDLUserName.DataTextField = "UserName"
                DDLUserName.DataValueField = "UserName"
                DDLUserName.DataBind()
            Else
                DDLUserName.Items.Clear()
                DDLUserName.Items.Insert(0, New ListItem("ไม่พบข้อมูล UserName ของสาขานี้", "000"))

                txtALLPrintSite.Text = ""
                txtFixPrint.Text = ""
            End If
            
        End Sub

        Sub CallUserSiteBy_UserNameSite_DR(ByVal str_site As String, ByVal str_user As String)
            Dim dr As SqlDataReader
            Try
                dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "st_UserPrintBySiteAndUser_NewDS", _
                            New SqlParameter("@site_id", str_site), _
                            New SqlParameter("@UserName", str_user))

                If dr.Read Then
                    txtALLPrintSite.Text = CommonUtility.Get_StringValue(dr("Num_printer"))
                    txtFixPrint.Text = CommonUtility.Get_StringValue(dr("fix_printer"))

                    dr.Close()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub CallUserSiteBy_UserNameSite_DS(ByVal str_site As String, ByVal str_user As String, ByVal str_userAdd As String, ByVal str_remark As String)
            Dim ds As New DataSet
            Dim ds_2 As New DataSet

            Dim dsRe As New DataSet
            Dim ds_2Re As New DataSet

            Dim dsAdd As New DataSet
            Dim dsAddRe As New DataSet

            Try
                'ได้ แถวเดียว
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_UserPrintBySiteAndUser_NewDS", _
                            New SqlParameter("@site_id", str_site), _
                            New SqlParameter("@UserName", str_user))

                'Re
                dsRe = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_UserPrintReceiptBySiteAndUser_NewDS", _
                            New SqlParameter("@site_id", str_site), _
                            New SqlParameter("@UserName", str_user))

                'ได้หลาย แถว
                ds_2 = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_Search_SiteAndUser_NewDS", _
                                            New SqlParameter("@site_id", str_site), _
                                            New SqlParameter("@UserName", str_user))

                'Re
                ds_2Re = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_SearchReceipt_SiteAndUser_NewDS", _
                                            New SqlParameter("@site_id", str_site), _
                                            New SqlParameter("@UserName", str_user))

                If ds.Tables(0).Rows.Count > 0 And dsRe.Tables(0).Rows.Count > 0 Then
                    If ds_2.Tables(0).Rows.Count = ds.Tables(0).Rows(0).Item("Num_printer") Then
                        For i As Integer = 0 To CInt(ds.Tables(0).Rows(0).Item("Num_printer")) - 1
                            dsAdd = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_Add_SiteAndUser_NewDS", _
                                                        New SqlParameter("@site_id", str_site), _
                                                        New SqlParameter("@UserName", str_userAdd), _
                                                        New SqlParameter("@Num_printer", ds_2.Tables(0).Rows(i).Item("Num_printer")), _
                                                        New SqlParameter("@fix_printer", ds_2.Tables(0).Rows(i).Item("fix_printer")), _
                                                        New SqlParameter("@description_nameprinter", ds_2.Tables(0).Rows(i).Item("description_nameprinter")), _
                                                        New SqlParameter("@value_Print", ds_2.Tables(0).Rows(i).Item("value_Print")), _
                                                        New SqlParameter("@remark", str_remark))
                            If dsAdd.Tables(0).Rows(0).Item("retStatus") = 0 Then
                                'ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind('navigateToInserted');", True)
                                txtAddUserName.Text = ""
                                txtRemark.Text = ""
                                lblErrorPage.Text = "ระบบได้ทำการเพิ่มข้อมูลเรียบร้อยแล้ว"
                            Else
                                lblErrorPage.Text = "ไม่สามารถเพิ่ม User ได้"
                                Exit For
                            End If

                        Next

                        'Table ใบเสร็จ
                        For iRe As Integer = 0 To ds_2Re.Tables(0).Rows.Count - 1
                            dsAddRe = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_AddRe_SiteAndUser_NewDS", _
                                                        New SqlParameter("@site_id2", str_site), _
                                                        New SqlParameter("@UserName2", str_userAdd), _
                                                        New SqlParameter("@Num_printer2", ds_2Re.Tables(0).Rows(iRe).Item("Num_printer")), _
                                                        New SqlParameter("@fix_printer2", ds_2Re.Tables(0).Rows(iRe).Item("fix_printer")), _
                                                        New SqlParameter("@description_nameprinter2", ds_2Re.Tables(0).Rows(iRe).Item("description_nameprinter")), _
                                                        New SqlParameter("@value_Print2", ds_2Re.Tables(0).Rows(iRe).Item("value_Print")), _
                                                        New SqlParameter("@remark2", str_remark), _
                                                        New SqlParameter("@IP_UserPrint2", ds_2Re.Tables(0).Rows(iRe).Item("IP_UserPrint")), _
                                                        New SqlParameter("@IPFixPrint2", ds_2Re.Tables(0).Rows(iRe).Item("IPFixPrint")))
                            If dsAddRe.Tables(0).Rows(0).Item("retStatus") = 0 Then
                                'ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind('navigateToInserted');", True)
                                txtAddUserName.Text = ""
                                txtRemark.Text = ""
                                lblErrorPage.Text = "ระบบได้ทำการเพิ่มข้อมูลเรียบร้อยแล้ว"
                            Else
                                lblErrorPage.Text = "ไม่สามารถเพิ่ม User ได้"
                                Exit For
                            End If

                        Next
                    Else
                        lblErrorPage.Text = "จำนวนเครื่องไม่เท่ากันกรุณาตรวจสอบและติดต่อ เจ้าหน้าที่"
                    End If

                Else

                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub DDL_Site_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDL_Site.SelectedIndexChanged
            CallUserSite_DS(DDL_Site.SelectedValue)
            CallUserSiteBy_UserNameSite_DR(DDL_Site.SelectedValue, DDLUserName.SelectedValue)
        End Sub

        Protected Sub DDLUserName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDLUserName.SelectedIndexChanged
            CallUserSiteBy_UserNameSite_DR(DDL_Site.SelectedValue, DDLUserName.SelectedValue)
        End Sub

        Protected Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            
            If txtAddUserName.Text.Trim <> "" Then
                If CStr(DDLUserName.SelectedValue) <> "000" Then
                    CallUserSiteBy_UserNameSite_DS(DDL_Site.SelectedValue, DDLUserName.SelectedValue, txtAddUserName.Text, _UserName & " :" & Date.Today & ": " & txtRemark.Text)
                Else
                    lblErrorPage.Text = "ไม่มีรายชื่อเจ้าหน้าที่ที่จะทำการ Copy เครื่องพิมพ์"
                End If
            Else
                lblErrorPage.Text = "ยังไม่ได้ป้อนชื่อ UserName ที่จะทำการเพิ่มข้อมูล"
            End If

        End Sub


        Protected Sub rdoCase_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCase.SelectedIndexChanged
            Select Case rdoCase.SelectedValue
                Case 1 'กรณีเพิ่มเจ้าหน้าที่
                    PanelReq.Visible = True
                    PanelDel.Visible = False
                Case 2 'กรณีลบเจ้าหน้าที่
                    PanelReq.Visible = False
                    PanelDel.Visible = True

                    If CallSite_DS.Tables(0).Rows.Count > 0 Then
                        DDLDelSite.DataSource = CallSite_DS.Tables(0)
                        DDLDelSite.DataTextField = "site_name"
                        DDLDelSite.DataValueField = "site_id"
                        DDLDelSite.DataBind()

                    End If
            End Select
        End Sub

        Protected Sub btnSearchDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDel.Click
            lblErrorDel.Text = ""
            'Load รายการคำร้อง
            rgRequestForm.DataSource = Load_UserDelList()
            rgRequestForm.DataBind()

            rgRequestForm2.DataSource = Load_UserDelListRe()
            rgRequestForm2.DataBind()

            btnDel.Visible = True
        End Sub

        Function Load_UserDelList() As DataTable
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_Search_SiteAndUser_NewDS", _
                                            New SqlParameter("@site_id", DDLDelSite.SelectedValue), _
                                            New SqlParameter("@UserName", txtDelUserName.Text))
                Return ds.Tables(0)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Function Load_UserDelListRe() As DataTable
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_SearchReceipt_SiteAndUser_NewDS", _
                                            New SqlParameter("@site_id", DDLDelSite.SelectedValue), _
                                            New SqlParameter("@UserName", txtDelUserName.Text))
                Return ds.Tables(0)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Sub Load_UserDelList2()
            Try
                Dim ds2 As New DataSet
                ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_Search_SiteAndUser_NewDS", _
                                            New SqlParameter("@site_id", DDLDelSite.SelectedValue), _
                                            New SqlParameter("@UserName", txtDelUserName.Text))
                
                rgRequestForm.DataSource = ds2.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub Load_UserDelList2Re()
            Try
                Dim ds2 As New DataSet
                ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "st_SearchReceipt_SiteAndUser_NewDS", _
                                            New SqlParameter("@site_id", DDLDelSite.SelectedValue), _
                                            New SqlParameter("@UserName", txtDelUserName.Text))

                rgRequestForm.DataSource = ds2.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub rgRequestForm_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRequestForm.NeedDataSource
            Load_UserDelList2()
        End Sub

        Protected Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
            Dim myDataGridItem As GridDataItem
            Dim runAuto As String = ""
            Dim str_runAutos As String = ""

            Dim myDataGridItem2 As GridDataItem
            Dim runAuto2 As String = ""
            Dim str_runAutos2 As String = ""

            For Each myDataGridItem In rgRequestForm.MasterTableView.Items
                'If myDataGridItem.Selected = True Then

                runAuto = myDataGridItem.Item("AutoID").Text
                str_runAutos += runAuto & ";"

                'End If
            Next

            For Each myDataGridItem2 In rgRequestForm2.MasterTableView.Items
                'If myDataGridItem.Selected = True Then

                runAuto2 = myDataGridItem2.Item("AutoID").Text
                str_runAutos2 += runAuto2 & ";"

                'End If
            Next

            If str_runAutos.Trim = "" Then
                lblErrorDel.Text = "ระบบไม่สามารถทำการลบข้อมูลเจ้าหน้าที่ได้ เนื่องจากไม่พบรายการที่ค้นหา"
            Else
                Select Case Del_(str_runAutos) And Del_Re(str_runAutos2)
                    Case True
                        lblErrorDel.Text = "ระบบได้ทำการลบข้อมูลเจ้าหน้าที่เรียบร้อยแล้ว"
                        rgRequestForm.DataSource = Load_UserDelList()
                        rgRequestForm.DataBind()

                        rgRequestForm2.DataSource = Load_UserDelListRe()
                        rgRequestForm2.DataBind()
                    Case False
                        lblErrorDel.Text = "ระบบไม่สามารถทำการลบข้อมูลเจ้าหน้าที่ได้กรุณาติดต่อเจ้าหน้าที่"
                End Select
            End If
        End Sub

        Function Del_(ByVal _Auto As String) As Boolean
            Dim iReturn As Integer
            Dim _Send As Boolean = False
            Dim arr_ As Array


            arr_ = _Auto.Split(";")

            For i As Integer = 0 To arr_.Length - 1
                If arr_(i).ToString <> "" Then
                    iReturn = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "st_Del_SiteAndUser_NewDS", _
                                                    New SqlParameter("@AutoID", arr_(i).ToString))
                Else
                    Exit For
                End If
                
            Next

            If iReturn = 1 Then
                'ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                _Send = True
            End If

            Return _Send
        End Function

        Function Del_Re(ByVal _Auto As String) As Boolean
            Dim iReturn As Integer
            Dim _Send As Boolean = False
            Dim arr_ As Array


            arr_ = _Auto.Split(";")

            For i As Integer = 0 To arr_.Length - 1
                If arr_(i).ToString <> "" Then
                    iReturn = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "st_DelRe_SiteAndUser_NewDS", _
                                                    New SqlParameter("@AutoID", arr_(i).ToString))
                Else
                    Exit For
                End If

            Next

            If iReturn = 1 Then
                'ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                _Send = True
            End If

            Return _Send
        End Function

        Private Sub rgRequestForm2_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRequestForm2.NeedDataSource
            Load_UserDelList2Re()
        End Sub
    End Class

End Namespace
