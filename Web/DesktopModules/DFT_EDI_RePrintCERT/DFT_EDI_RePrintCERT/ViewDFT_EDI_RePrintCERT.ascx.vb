Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_RePrintCERT
    Partial Class ViewDFT_EDI_RePrintCERT
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim reader_receipt As SqlDataReader = Nothing
        Dim DataSet_Userreceipt As DataSet = Nothing
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
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Check ��� User ��� Login ��������� Site �˹
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))
                'by rut 7-09-2555 ���ѹ��� 22-01-2556 ��᷹��ҹ��ҧ ��ͧ����� Table "RoleList" ᷹
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

            If Not Page.IsPostBack Then
                Session("UName") = UserInfo.Username

                tblData1.Visible = False
                txtSearchValue.Text = ""
                txtSearchValue.Focus()

                Call_printName()
                'lblDSRoleID.Text = IsRoleName2("EDI_DS")

                Data_YearsCon()
            End If
            ShowForm_AD(Session("ssRoleName"))
            lblDSRoleID.Text = IsRoleName2("EDI_DS")
        End Sub
        'Check role str for DS2
        Protected Function IsRoleName2(ByVal str_rolename As String) As String
            Dim ret As String = ""
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

                If myRoleInfo.RoleName = str_rolename Then
                    ret = "EDI_DS"
                    Exit For
                End If
            Next i
            Return ret
        End Function
        '�ʴ� ����ͧ��������ʴ�����ͧ����������� A and D �������������ͧ
        Sub ShowForm_AD(ByVal _siteID As String)
            Select Case _siteID
                Case "ST-001"
                    Panel_A.Visible = True
                    Panel_D.Visible = True
                Case Else
                    Panel_A.Visible = False
                    Panel_D.Visible = False
            End Select
        End Sub

        '���¡����ͧ�����Ӣ�
        Sub Call_printName()
            If LoadPrint_receipt.HasRows = True Then
                rdblReceiptPrinter.DataSource = LoadPrint_receipt()
                rdblReceiptPrinter.DataTextField = "description_nameprinter"
                rdblReceiptPrinter.DataValueField = "value_Print"
                rdblReceiptPrinter.DataBind()
                If LoadPrint_SetUser_receipt.Tables(0).Rows.Count > 0 Then
                    For irdb As Integer = 0 To LoadPrint_SetUser_receipt.Tables(0).Rows.Count - 1
                        If LoadPrint_SetUser_receipt.Tables(0).Rows.Count = 1 Then
                            rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(irdb).Item("fix_printer") - 1).Selected = True
                        Else
                            rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(irdb).Item("fix_printer") - 1).Selected = True
                        End If
                    Next
                Else
                    lbl_ErrMSG.Text = "�������ö����������ͧ�ҡ������ͧ�������辺"
                End If
            Else
                lbl_ErrMSG.Text = "�������ö����������ͧ�ҡ������ͧ�������辺"
            End If
        End Sub
        Private Function LoadPrint_receipt() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                reader_receipt = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_requestCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", UserInfo.Username))
                Return reader_receipt
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
        Private Function LoadPrint_SetUser_receipt() As DataSet
            Try
                objConn = New SqlConnection(strEDIConn)
                DataSet_Userreceipt = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_requestCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", UserInfo.Username))
                Return DataSet_Userreceipt
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

#Region "�������ͧ�ҡ �ա�þ������͹��ѧ �Թ�ҡ��㹰ҹ������"
        Sub Data_YearsCon()
            Dim ds As New DataSet
            Dim sqlYearCon As String = ""
            sqlYearCon = "SELECT * FROM  N_NameDataCon ORDER BY auto_id"
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sqlYearCon)

            DDLYears.DataSource = ds
            DDLYears.DataTextField = "DisplayNameServer"
            DDLYears.DataValueField = "auto_id"
            DDLYears.DataBind()
        End Sub

        Function Re_ConCheck(ByVal By_auto As String) As String
            Dim Temp_Con As String = ""
            Dim sql_ As String = ""
            sql_ = "SELECT  * FROM         N_NameDataCon WHERE     (auto_id = @auto_id)"
            Dim ds As New DataSet
            Dim prm(0) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@auto_id", By_auto)

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql_, prm)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim xxx As String = ds.Tables(0).Rows(0).Item("NameConServer").ToString
                Temp_Con = ConfigurationManager.ConnectionStrings(xxx).ConnectionString
            End If

            Return Temp_Con
        End Function
#End Region
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                ChkTrueCopy.Checked = False
                Session("Checkedistatus") = ""
                Dim TCAT As Integer
                If chkUseRef2.Checked Then TCAT = 2 Else TCAT = 1
                Dim ds As New DataSet

                '�������ͧ�ҡ �ա�þ��������Ż���ҷ�����������㹰ҹ��ѡ
                Select Case check_YearsOle.Checked
                    Case True
                        Select Case DDLYears.SelectedValue
                            Case "1" '���� 24TradeDBCon �ҷ������ѡ���ͧ�ҡ��ô֧�����Ū��
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_PrintBack_NewDS_01", _
                                                                                New SqlParameter("@TCat", TCAT), _
                                                                                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchValue.Text.Trim())))

                            Case "2" 'OriginConnection ��ͧ�� Table ���
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_PrintBack_NewDS_02", _
                                                                                New SqlParameter("@TCat", TCAT), _
                                                                                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchValue.Text.Trim())))
                            Case "3" 'OriginConnection ��ͧ�� Table ���
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_PrintBack_NewDS_03", _
                                                                                New SqlParameter("@TCat", TCAT), _
                                                                                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchValue.Text.Trim())))

                        End Select
                    Case False
                        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_PrintBack_NewDS", _
                                                New SqlParameter("@TCat", TCAT), _
                                                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchValue.Text.Trim())))
                End Select

                If ds.Tables(0).Rows.Count > 0 Then
                    rgRequestList.DataSource = ds.Tables(0)
                    rgRequestList.DataBind()

                    If rgRequestList.MasterTableView.Items.Count > 0 Then
                        tblData1.Visible = True

                        If ds.Tables(0).Rows(0).Item("form_type") = "FORM5_2" Then
                            ChkTrueCopy.Visible = True
                        Else
                            ChkTrueCopy.Visible = False
                        End If

                        rgRequestList.MasterTableView.Items(0).Selected = True
                        txtRePrint_Remark.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("RePrint_Remark"))
                        txtRePrint_Remark.Focus()

                        'check ���͹��ѵ������ѧ���ж�����͹��ѵԨ�������ѹ ����ҹ��ҧ
                        Session("Checkedistatus") = ds.Tables(0).Rows(0).Item("edi_status_id")
                        Call_printName()
                    Else
                        tblData1.Visible = False
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('��辺�����Ţ����ͧ���ӡ�ä���');")
                    End If
                Else
                    tblData1.Visible = False
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('��辺�����Ţ����ͧ���ӡ�ä���');")
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Try
                ' Dim datecheck As Nullable(Of DateTime)

                ' If Not rdpSelectDatePrint.SelectedDate.HasValue = True Then datecheck = Nothing Else datecheck = rdpSelectDatePrint.SelectedDate.Value
                Select Case CheckSelectDate.Checked '������ check ������͡�ѹ���������������ʴ�������� �ó� �ѹ͹��ѵ�������ѹ���ǡѹ
                    Case True '�ó����͡�ѹ��������ͧ
                        Select Case rdpSelectDatePrint.SelectedDate.HasValue 'check ������͡�ѹ������������ѧ
                            Case True
                                Dim run_site_id As String = Session("ssRoleName")
                                Dim myDataGridItem As GridDataItem
                                Dim runAuto As String = ""
                                Dim run_form_type As String = ""

                                Dim run_COMPANY_TAXNO As String = ""
                                Dim run_FROM_DATE As String = ""
                                Dim run_TO_DATE As String = ""
                                Dim run_INVOICE_NO As String = ""
                                Dim run_DISPLAY_FLAG As String = ""
                                Dim str_runAutos As String = ""
                                Dim str_runForms As String = ""

                                For Each myDataGridItem In rgRequestList.MasterTableView.Items
                                    If myDataGridItem.Selected = True Then

                                        runAuto = myDataGridItem.Item("invh_run_auto").Text
                                        str_runAutos += runAuto & ";"
                                        run_form_type = myDataGridItem.Item("form_type").Text
                                        str_runForms += run_form_type & ";"
                                    End If
                                Next
                                Session("CheckUser") = UserInfo.Username

                                '�ѹ�֡�˵ؼ�����ѹ���������
                                '=========================================
                                Call UpdateRePrint(runAuto)
                                '=========================================

                                Dim spl_runAuto As Array
                                Dim spl_runForms As Array
                                spl_runAuto = str_runAutos.Split(";")
                                spl_runForms = str_runForms.Split(";")
                                Dim i As Integer

                                '========================================
                                If RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = True Then
                                    If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö �����Ӣ������ͧ�ҡ �ѧ��������͡��¡��');")
                                    Else
                                        '���Ҿ���Ẻ�Ӣ�
                                        Me.RadAjaxManager1.ResponseScripts.Add("window.open('/DesktopModules/DFT_EDI_RePrintCERT/View_ReportDUP.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" &
                                                        runAuto & "&CellType=" & run_form_type &
                                                        "&SendSiteID=" & lblRoleID.Text & "&radioForm=0" & "&SendSeleteDATE=" & CommonUtility.Get_StringValue(rdpSelectDatePrint.SelectedDate.Value) & "&SendCHDATE=" & CheckSelectDate.Checked & "&YearsOle=" & check_YearsOle.Checked & "&YearsID=" & DDLYears.SelectedValue & "&SendTrueCopy=" & ChkTrueCopy.Checked & "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                                        txtFirstpage.Text = ""
                                        txtLastpage.Text = ""
                                    End If

                                ElseIf RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = False Then
                                    If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö �����˹ѧ����Ѻ�ͧ�� �ѧ��������͡��¡��');")
                                    Else
                                        '���Ҿ���˹ѧ����Ѻ�ͧ
                                        'Me.RadAjaxManager1.ResponseScripts.Add("window.open('/DesktopModules/DFT_EDI_RePrintCERT/View_ReportDUP.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" &
                                        '                                    runAuto & "&CellType=" & run_form_type &
                                        '                                    "&SendSiteID=" & lblRoleID.Text & "&radioForm=1" & "&SendSeleteDATE=" & CommonUtility.Get_StringValue(rdpSelectDatePrint.SelectedDate.Value) & "&SendCHDATE=" & CheckSelectDate.Checked & "&YearsOle=" & check_YearsOle.Checked & "&YearsID=" & DDLYears.SelectedValue & "&SendTrueCopy=" & ChkTrueCopy.Checked & "',null,'height=600, width=800, status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                                        Dim url As String = "/DesktopModules/DFT_EDI_CheckAttachment/frmFormPDFPage.aspx?INVH_RUN_AUTO=" & runAuto
                                        If check_YearsOle.Checked Then
                                            url &= "&yearid=" & DDLYears.SelectedValue
                                        End If

                                        Me.RadAjaxManager1.ResponseScripts.Add("window.open('" & url & "',null,'height=600, width=800, status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                                        txtFirstpage.Text = ""
                                        txtLastpage.Text = ""
                                    End If
                                ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = True Then
                                    For i = 0 To spl_runAuto.Length - 1

                                        Dim RBList_Form As String = "0" 'check �� �Ӣ�
                                        '����ͧ��������Ẻ�Ӣ�
                                        If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                            Exit For
                                        Else
                                            Select Case ReportPrintClass.printMultireport(spl_runForms(i),
                                                                                                spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, CommonUtility.Get_StringValue(rdblReceiptPrinter.SelectedValue), "", txtFirstpage.Text.Trim, txtLastpage.Text.Trim, "", "", "", False, check_YearsOle.Checked, DDLYears.SelectedValue, ChkTrueCopy.Checked)
                                                Case 1 'print �� ����
                                                    txtFirstpage.Text = ""
                                                    txtLastpage.Text = ""
                                                Case 2 '���������� ���͹� �������¡���Թ���
                                                    If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                                    End If
                                                    txtFirstpage.Text = ""
                                                    txtLastpage.Text = ""
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����Ẻ�Ӣ��Ţ�����ҧ�ԧ " & spl_runAuto(i) & " �� ���ͧ�ҡ��¡���Թ��������');")
                                                Case 3 '�������������͹� ���
                                                    txtFirstpage.Text = ""
                                                    txtLastpage.Text = ""
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����Ẻ�Ӣ��Ţ�����ҧ�ԧ " & spl_runAuto(i) & " ��');")
                                            End Select
                                        End If
                                    Next
                                    spl_runForms(i) = ""
                                    spl_runAuto(i) = ""
                                ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = False Then
                                    For i = 0 To spl_runAuto.Length - 1
                                        Dim RBList_Form As String = "1" 'check �� ˹ѧ����Ѻ�ͧ
                                        '����ͧ��������˹ѧ����Ѻ�ͧ
                                        If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                            Exit For
                                        Else
                                            '��Ѻ����ͧ����� ���͡˹����
                                            If (txtFirstpage.Text = "" And txtLastpage.Text = "") = True Then '���� print Ẻ����
                                                Select Case ReportPrintClass.printMultireport(spl_runForms(i),
                                                                                                    spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, "", "", txtFirstpage.Text.Trim, txtLastpage.Text.Trim, rdoA_ALL.SelectedValue, rdoD_ALL.SelectedValue, CommonUtility.Get_StringValue(rdpSelectDatePrint.SelectedDate.Value), CheckSelectDate.Checked, check_YearsOle.Checked, DDLYears.SelectedValue, ChkTrueCopy.Checked)
                                                    Case 1 'print ��
                                                        txtFirstpage.Text = ""
                                                        txtLastpage.Text = ""
                                                    Case 2 '�����������������¡���Թ���
                                                        If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                                        End If
                                                        txtFirstpage.Text = ""
                                                        txtLastpage.Text = ""
                                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " �� ���ͧ�ҡ��¡���Թ��������');")
                                                    Case 3 '�������������͹����
                                                        txtFirstpage.Text = ""
                                                        txtLastpage.Text = ""
                                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " ��');")
                                                End Select

                                            Else 'print Ẻ ���͡˹��
                                                If (txtFirstpage.Text = "0" Or txtLastpage.Text = "0") = True Then
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö��͹�Ţ˹���� �ٹ�� ��');")
                                                ElseIf CInt(txtFirstpage.Value) > CInt(txtLastpage.Value) = True Then
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�Ţ˹���á�������ö �ҡ����˹���ش������');")
                                                Else
                                                    If spl_runAuto.Length - 1 > 1 Then 'check ����������͡��¡�� 1 ��¡��
                                                        RadAjaxManager1.ResponseScripts.Add("window.alert('��س����͡��¡�� ��§ 1 ��¡��');")
                                                    Else
                                                        Select Case ReportPrintClass.printMultireport(spl_runForms(i),
                                                                                                            spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, "", "", txtFirstpage.Text.Trim, txtLastpage.Text.Trim, rdoA_ALL.SelectedValue, rdoD_ALL.SelectedValue, CommonUtility.Get_StringValue(rdpSelectDatePrint.SelectedDate.Value), CheckSelectDate.Checked, check_YearsOle.Checked, DDLYears.SelectedValue, ChkTrueCopy.Checked)
                                                            Case 1 'print ��
                                                                txtFirstpage.Text = ""
                                                                txtLastpage.Text = ""
                                                            Case 2 '�����������������¡���Թ���
                                                                If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                                                End If
                                                                txtFirstpage.Text = ""
                                                                txtLastpage.Text = ""
                                                                RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " �� ���ͧ�ҡ��¡���Թ��������');")
                                                            Case 3 '�������������͹����
                                                                txtFirstpage.Text = ""
                                                                txtLastpage.Text = ""
                                                                RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " ��');")
                                                        End Select

                                                    End If
                                                End If

                                            End If
                                        End If
                                    Next
                                    spl_runForms(i) = ""
                                    spl_runAuto(i) = ""
                                End If
                            Case Else
                                RadAjaxManager1.ResponseScripts.Add("window.alert('��س����͡�ѹ�������');")
                        End Select
                    Case False '�ó�����ѹ������ѹ͹��ѵ�
                        Select Case Session("Checkedistatus")
                            Case "A"
                                Dim run_site_id As String = Session("ssRoleName")
                                Dim myDataGridItem As GridDataItem
                                Dim runAuto As String = ""
                                Dim run_form_type As String = ""

                                Dim run_COMPANY_TAXNO As String = ""
                                Dim run_FROM_DATE As String = ""
                                Dim run_TO_DATE As String = ""
                                Dim run_INVOICE_NO As String = ""
                                Dim run_DISPLAY_FLAG As String = ""
                                Dim str_runAutos As String = ""
                                Dim str_runForms As String = ""

                                For Each myDataGridItem In rgRequestList.MasterTableView.Items
                                    If myDataGridItem.Selected = True Then

                                        runAuto = myDataGridItem.Item("invh_run_auto").Text
                                        str_runAutos += runAuto & ";"
                                        run_form_type = myDataGridItem.Item("form_type").Text
                                        str_runForms += run_form_type & ";"
                                    End If
                                Next
                                Session("CheckUser") = UserInfo.Username

                                '�ѹ�֡�˵ؼ�����ѹ���������
                                '=========================================
                                Call UpdateRePrint(runAuto)
                                '=========================================

                                Dim spl_runAuto As Array
                                Dim spl_runForms As Array
                                spl_runAuto = str_runAutos.Split(";")
                                spl_runForms = str_runForms.Split(";")
                                Dim i As Integer

                                '========================================
                                If RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = True Then
                                    If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö �����Ӣ������ͧ�ҡ �ѧ��������͡��¡��');")
                                    Else
                                        '���Ҿ���Ẻ�Ӣ�
                                        Me.RadAjaxManager1.ResponseScripts.Add("window.open('/DesktopModules/DFT_EDI_RePrintCERT/View_ReportDUP.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" &
                                                        runAuto & "&CellType=" & run_form_type &
                                                        "&SendSiteID=" & lblRoleID.Text & "&radioForm=0" & "&SendSeleteDATE=" & CommonUtility.Get_StringValue(Now.Date) & "&SendCHDATE=" & CheckSelectDate.Checked & "&YearsOle=" & check_YearsOle.Checked & "&YearsID=" & DDLYears.SelectedValue & "&SendTrueCopy=" & ChkTrueCopy.Checked & "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                                        txtFirstpage.Text = ""
                                        txtLastpage.Text = ""
                                    End If

                                ElseIf RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = False Then
                                    If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö �����˹ѧ����Ѻ�ͧ�� �ѧ��������͡��¡��');")
                                    Else
                                        '���Ҿ���˹ѧ����Ѻ�ͧ
                                        'Me.RadAjaxManager1.ResponseScripts.Add("window.open('/DesktopModules/DFT_EDI_RePrintCERT/View_ReportDUP.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" &
                                        '                                    runAuto & "&CellType=" & run_form_type &
                                        '                                    "&SendSiteID=" & lblRoleID.Text & "&radioForm=1" & "&SendSeleteDATE=" & CommonUtility.Get_StringValue(Now.Date) & "&SendCHDATE=" & CommonUtility.Get_StringValue(CheckSelectDate.Checked) & "&YearsOle=" & check_YearsOle.Checked & "&YearsID=" & DDLYears.SelectedValue & "&SendTrueCopy=" & ChkTrueCopy.Checked & "',null,'height=600, width=800, status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                                        Dim url As String = "/DesktopModules/DFT_EDI_CheckAttachment/frmFormPDFPage.aspx?INVH_RUN_AUTO=" & runAuto
                                        If check_YearsOle.Checked Then
                                            url &= "&yearid=" & DDLYears.SelectedValue
                                        End If

                                        Me.RadAjaxManager1.ResponseScripts.Add("window.open('" & url & "',null,'height=600, width=800, status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                                        txtFirstpage.Text = ""
                                        txtLastpage.Text = ""

                                    End If
                                ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = True Then
                                    For i = 0 To spl_runAuto.Length - 1

                                        Dim RBList_Form As String = "0" 'check �� �Ӣ�
                                        '����ͧ��������Ẻ�Ӣ�
                                        If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                            Exit For
                                        Else
                                            Select Case ReportPrintClass.printMultireport(spl_runForms(i), _
                                                                                                spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, CommonUtility.Get_StringValue(rdblReceiptPrinter.SelectedValue), "", txtFirstpage.Text.Trim, txtLastpage.Text.Trim, "", "", "", False, check_YearsOle.Checked, DDLYears.SelectedValue, ChkTrueCopy.Checked)
                                                Case 1 'print �� ����
                                                    txtFirstpage.Text = ""
                                                    txtLastpage.Text = ""
                                                Case 2 '���������� ���͹� �������¡���Թ���
                                                    If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                                    End If
                                                    txtFirstpage.Text = ""
                                                    txtLastpage.Text = ""
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����Ẻ�Ӣ��Ţ�����ҧ�ԧ " & spl_runAuto(i) & " �� ���ͧ�ҡ��¡���Թ��������');")
                                                Case 3 '�������������͹� ���
                                                    txtFirstpage.Text = ""
                                                    txtLastpage.Text = ""
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����Ẻ�Ӣ��Ţ�����ҧ�ԧ " & spl_runAuto(i) & " ��');")
                                            End Select
                                        End If
                                    Next
                                    spl_runForms(i) = ""
                                    spl_runAuto(i) = ""
                                ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = False Then
                                    For i = 0 To spl_runAuto.Length - 1
                                        Dim RBList_Form As String = "1" 'check �� ˹ѧ����Ѻ�ͧ
                                        '����ͧ��������˹ѧ����Ѻ�ͧ
                                        If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                                            Exit For
                                        Else
                                            '��Ѻ����ͧ����� ���͡˹����
                                            If (txtFirstpage.Text = "" And txtLastpage.Text = "") = True Then '���� print Ẻ����
                                                Select Case ReportPrintClass.printMultireport(spl_runForms(i), _
                                                                                                    spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, "", "", txtFirstpage.Text.Trim, txtLastpage.Text.Trim, rdoA_ALL.SelectedValue, rdoD_ALL.SelectedValue, CommonUtility.Get_StringValue(Now.Date), CheckSelectDate.Checked, check_YearsOle.Checked, DDLYears.SelectedValue, ChkTrueCopy.Checked)
                                                    Case 1 'print ��
                                                        txtFirstpage.Text = ""
                                                        txtLastpage.Text = ""
                                                    Case 2 '�����������������¡���Թ���
                                                        If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                                        End If
                                                        txtFirstpage.Text = ""
                                                        txtLastpage.Text = ""
                                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " �� ���ͧ�ҡ��¡���Թ��������');")
                                                    Case 3 '�������������͹����
                                                        txtFirstpage.Text = ""
                                                        txtLastpage.Text = ""
                                                        RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " ��');")
                                                End Select

                                            Else 'print Ẻ ���͡˹��
                                                If (txtFirstpage.Text = "0" Or txtLastpage.Text = "0") = True Then
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö��͹�Ţ˹���� �ٹ�� ��');")
                                                ElseIf CInt(txtFirstpage.Value) > CInt(txtLastpage.Value) = True Then
                                                    RadAjaxManager1.ResponseScripts.Add("window.alert('�Ţ˹���á�������ö �ҡ����˹���ش������');")
                                                Else
                                                    If spl_runAuto.Length - 1 > 1 Then 'check ����������͡��¡�� 1 ��¡��
                                                        RadAjaxManager1.ResponseScripts.Add("window.alert('��س����͡��¡�� ��§ 1 ��¡��');")
                                                    Else
                                                        Select Case ReportPrintClass.printMultireport(spl_runForms(i), _
                                                                                                            spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, "", "", txtFirstpage.Text.Trim, txtLastpage.Text.Trim, rdoA_ALL.SelectedValue, rdoD_ALL.SelectedValue, CommonUtility.Get_StringValue(Now.Date), CheckSelectDate.Checked, check_YearsOle.Checked, DDLYears.SelectedValue, ChkTrueCopy.Checked)
                                                            Case 1 'print ��
                                                                txtFirstpage.Text = ""
                                                                txtLastpage.Text = ""
                                                            Case 2 '�����������������¡���Թ���
                                                                If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                                                End If
                                                                txtFirstpage.Text = ""
                                                                txtLastpage.Text = ""
                                                                RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " �� ���ͧ�ҡ��¡���Թ��������');")
                                                            Case 3 '�������������͹����
                                                                txtFirstpage.Text = ""
                                                                txtLastpage.Text = ""
                                                                RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö�����˹ѧ����Ѻ�ͧ�Ţ�����ҧ�ԧ " & spl_runAuto(i) & " ��');")
                                                        End Select

                                                    End If
                                                End If

                                            End If
                                        End If
                                    Next
                                    spl_runForms(i) = ""
                                    spl_runAuto(i) = ""
                                End If
                            Case Else
                                RadAjaxManager1.ResponseScripts.Add("window.alert('�������ö����¡�õ�������ͧ���� ���ͧ�ҡ��¡���ѧ���١͹��ѵ� ��Ҩз���¡�õ�͡�س����͡ �ѹ���������¤�Ѻ');")
                        End Select

                End Select

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub UpdateRePrint(ByVal INVH_RUN_AUTO As String, Optional ByVal By_CheckCase As Object = Nothing, Optional ByVal By_StrConCase As Object = Nothing)
            Try
                Dim ds As New DataSet
                Dim temp_store As String = "sp_common_UpdateRePrint_NewDS"

                Select Case check_YearsOle.Checked
                    Case True
                        Select Case DDLYears.SelectedValue
                            Case "1"
                                temp_store = "sp_common_UpdateRePrint_NewDS01"
                            Case "2"
                                temp_store = "sp_common_UpdateRePrint_NewDS02"
                            Case "3"
                                temp_store = "sp_common_UpdateRePrint_NewDS03"
                        End Select
                End Select

                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, temp_store, _
                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(INVH_RUN_AUTO)), _
                New SqlParameter("@REPRINT_REMARK", CommonUtility.Get_StringValue(txtRePrint_Remark.Text)), _
                New SqlParameter("@RePrint_By", CommonUtility.Get_StringValue(UserInfo.Username)))

            Catch ex As Exception
                RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub CheckSelectDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckSelectDate.CheckedChanged
            Select Case CheckSelectDate.Checked
                Case True
                    rdpSelectDatePrint.Enabled = True
                    rdpSelectDatePrint.SelectedDate = Today
                Case False
                    rdpSelectDatePrint.Enabled = False
                    rdpSelectDatePrint.Clear()
            End Select
        End Sub

        Protected Sub check_YearsOle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_YearsOle.CheckedChanged
            DDLYears.Enabled = True
        End Sub
    End Class
    
End Namespace
