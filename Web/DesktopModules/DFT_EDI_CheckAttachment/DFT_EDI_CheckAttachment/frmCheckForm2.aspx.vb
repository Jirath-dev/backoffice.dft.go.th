Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI
Imports DotNetNuke.Entities.Users


Partial Public Class frmCheckForm2
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim myReader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing
    Dim myReader1 As SqlDataReader = Nothing
    Dim objConn1 As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo
            lblUser_ID.Text = objUserInfo.Username
            lblInvh_Run_Auto.Text = CommonUtility.Get_StringValue(Request.QueryString("RefNo"))
            If Request.QueryString("mode") = "view" Then
                panelResult.Visible = False
                lblCheckResult.Text = "<div class=""check-result"">*** �Ӣ�˹ѧ����Ѻ�ͧ��¡�ù���Ǩ�ͺ���º�������� ***</div>"
            End If

            '��Ǩ�ͺ Seal & Sign
            CheckSealSign()

            ''ByTine 05-08-2558 �ó��繺���ѷ��Ѥ�㨵�ͧṺ�͡��÷ء����
            '��Ǩ�ͺ����ѷ ��Ѥ��
            If CheckValunteer(CommonUtility.Get_StringValue(Request.QueryString("TaxNo"))) = False Then
                panelFile.Visible = True
                Call LoadAttachFile(lblInvh_Run_Auto.Text)
                lblValunteer2.Visible = False
            Else
                panelFile.Visible = True
                Call LoadAttachFile(lblInvh_Run_Auto.Text)
                lblValunteer2.Visible = True
                'If CheckMustHasPDF(CommonUtility.Get_StringValue(Request.QueryString("RefNo"))) = True Then
                '    panelFile.Visible = True
                '    Call LoadAttachFile(lblInvh_Run_Auto.Text)
                '    lblValunteer2.Visible = True
                'Else
                '    panelFile.Visible = False
                '    lblValunteer2.Visible = True
                'End If
            End If

            objConn1.Close()

            'Set Session For Checkform KPI
            START_CHCEK_TIME.Value = Date.Now

            ''��Ǩ�ͺ BlackList / WatchList
            Dim blacklist As String = ReportPrintClass.CheckBlackList(Request.QueryString("TaxNo"))
            If blacklist = "" Or blacklist Is Nothing Then
                ''����繤����ҧ ���� Nothing ����͹ Label ���
                lblMsgBlackList.Visible = False
            Else
                lblMsgBlackList.Visible = True
                lblMsgBlackList.Text = "<a href=# onclick=ShowBlackList();><u>" & blacklist & "</u></a>"
            End If

        End If
    End Sub

    Private Sub CheckSealSign()
        objConn1 = New SqlConnection(strEDIConn)

        Dim cmd As String = "SELECT * FROM form_header_edi WHERE invh_run_auto ='" & lblInvh_Run_Auto.Text & "'  AND ISNULL(IsEss, 0) = 1"

        myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.Text, cmd)
        myReader1.Read()

        If myReader1.HasRows Then
            panelSealSign.Visible = True
            btnSignOK.Visible = True
            Button1.Visible = False
        Else
            panelSealSign.Visible = False
            Button1.Visible = True
            btnSignOK.Visible = False
        End If

        myReader1.Close()

        Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

        myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.Text, "SELECT AutorunID,TAXID,FormTypeCase,UserName FROM imGovernmentSign WHERE UserName='" & objUserInfo.Username & "'  AND (StatusUse='1') AND FormTypeCase LIKE '%" & CommonUtility.Get_StringValue(Request.QueryString("form_type")) & ";%'")
        myReader1.Read()
        If myReader1.HasRows Then
            UserTaxID.Value = myReader1("TAXID").ToString()
            SignID.Value = myReader1("AutorunID").ToString()
            UserName.Value = myReader1("UserName").ToString()
        Else

        End If

        myReader1.Close()

        objConn1.Close()

    End Sub

    Private Sub LoadAttachXML(ByVal RefNo As String)
        If myReader1.IsClosed = False Then
            myReader1.Close()
        End If
        myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.StoredProcedure, "sp_form_CheckXmlAttachFile", New SqlParameter("@INVH_RUN_AUTO", RefNo))
        myReader1.Read()

        If myReader1.HasRows Then
            If myReader1("HasXmlInvoice") > 0 Then
                PanelInv.Visible = True
            Else
                PanelInv.Visible = False
            End If
            If myReader1("HasXmlBl") > 0 Then
                PanelBL.Visible = True
            Else
                PanelBL.Visible = False
            End If
        End If

        myReader1.Close()
    End Sub

    Private Function CheckValunteer(ByVal TaxNo As String) As Boolean
        'open database connection
        objConn1 = New SqlConnection(strEDIConn)

        '��Ǩ�ͺ����ѷ����Ẻ ��Ѥ�� ����ͧ���͡���Ṻ ����͹��ͧ�ʴ��͡���Ṻ��� - 11 Dec 2012 by prayut
        Dim isValunteer As Boolean = False
        Dim formType As String
        formType = CommonUtility.Get_StringValue(Request.QueryString("form_type"))
        If formType.ToUpper() = "FORM2" Then

            myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.Text, "SELECT Company_Taxno FROM company_volunteer WHERE Company_Taxno='" & TaxNo & "'")
            myReader1.Read()

            If myReader1.HasRows Then
                isValunteer = True
            Else
                isValunteer = False
            End If

            myReader1.Close()

            If isValunteer = True Then
                'check ����Ȼ��·ҧ੾�С������������û ����ͧṺ�͡���
                Dim myReader2 As SqlDataReader = Nothing
                myReader2 = SqlHelper.ExecuteReader(objConn1, CommandType.Text, "select count(*) as IsUerope FROM form_header_edi WHERE invh_run_auto='" & lblInvh_Run_Auto.Text & "' AND destination_country IN('AT','BE','BG','CY','CZ','DK','EE','FI','FR','DE','GR','HU','IE','IT','LV','LT','LU','MT','NL','PL','PT','HR','RO','SK','SI','ES','SE','GB')")
                myReader2.Read()

                If myReader2("IsUerope").ToString = "1" Then
                    isValunteer = True
                Else
                    isValunteer = False
                End If

                myReader2.Close()
            End If

        Else
            isValunteer = False
        End If

        Return isValunteer

    End Function

    ''' <summary>
    ''' �ѧ���蹵�Ǩ�ͺ੾�п���� CO ����� �ԡѴ 50-63 ����ѷ��Ѥ�� ����ͧṺ�͡���
    ''' </summary>
    ''' <param name="RefNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckMustHasPDF(ByVal RefNo As String) As Boolean
        Dim ret_value As Boolean
        Dim dr As SqlDataReader
        dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_common_edi_checkFormCOHasPDF_DS2", _
                        New SqlParameter("@invh_run_auto", RefNo))
        dr.Read()
        If dr.HasRows Then
            If CommonUtility.Get_Int32(dr("HasPDF").ToString) > 0 Then
                ret_value = True
            Else
                ret_value = False
            End If
        Else
            ret_value = False
        End If
        dr.Close()

        Return ret_value

    End Function

    Private Sub LoadAttachFile(ByVal RefNo As String)
        Try
            ''objConn1 = New SqlConnection(strEDIConn)

            Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

            If Request.QueryString("mode") <> "view" Then

                myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.StoredProcedure, "sp_Form_EDI_GetUserCheckAttach", New SqlParameter("@INVH_RUN_AUTO", RefNo))
                myReader1.Read()

                Dim userview As String = ""
                If myReader1.HasRows Then
                    userview = myReader1("CheckDoc_By").ToString()

                    If DataUtil.ConvertToBoolean(myReader1("ASW_Support")) _
                        And DataUtil.ConvertToBoolean(myReader1("ASW_CountryAllow")) _
                        And Not DataUtil.ConvertToBoolean(myReader1("IsPrintForm")) Then
                        btnApprove.Visible = True
                        Button1.Visible = False
                        div_asw.Style.Item("display") = ""
                    Else
                        btnApprove.Visible = False
                        Button1.Visible = True
                        div_asw.Style.Item("display") = "none"
                        '//����� ESS �Դ���� Sealsign
                        CheckSealSign()
                    End If

                End If

                myReader1.Close()

                If userview <> "" Then
                    'btnNotApproved.Visible = False
                    'Button1.Visible = False
                    Panel1.Visible = True
                    lblMsg.Text = "�Ţ���㺤���ͧ " & RefNo & " ���ѧ�١��Ǩ�ͺ�� " & userview & vbCrLf '& " �ѧ��鹨֧�������ҧ���� �������ö�ѹ�֡�������� ��"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", "<script language='javascript'>alert('�Ţ���㺤���ͧ " & RefNo & " ���ѧ�١��Ǩ�ͺ�� " & userview & "');</script>", False)
                ElseIf Not objUserInfo.Username.Contains("adminds2") Then
                    Dim sql As String
                    Panel1.Visible = False
                    sql = "UPDATE form_header_edi SET CheckDoc_By='" & objUserInfo.Username.ToLower & "' WHERE invh_run_auto='" & RefNo & "' AND CheckDoc_Date is null AND edi_status_id='Q'"
                    SqlHelper.ExecuteNonQuery(objConn1, CommandType.Text, sql)
                End If
            Else
                Dim strResult As String = ""
                Try
                    myReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_edi_getFormCheckedInfo_DS2", New SqlParameter("@REF_No", RefNo))
                    myReader.Read()
                    If myReader.HasRows Then
                        strResult = "ʶҹ�: " & myReader("Check_Result") & _
                                    "<br/>�ѹ����觤Ӣ�: " & myReader("Sent_Date") & "&nbsp;&nbsp;�ѹ����Ǩ�ͺ: " & myReader("CheckDoc_Date") & " &nbsp;&nbsp;��: " & myReader("CheckDoc_By").ToString() & _
                                    "<br/>�ѹ�����������: " & myReader("printFormDate") & "&nbsp;&nbsp;��: " & myReader("UserPrintForm")

                        'If myReader("CheckDoc_By").ToString.ToLower.Equals(objUserInfo.Username.ToLower) And myReader("edi_status_id") <> "A" And myReader("print_flag") <> "Y" Then
                        '    btnReStatus.Visible = True
                        'End If
                        If objUserInfo.IsInRole("Department_Leader") And myReader("edi_status_id") <> "A" And myReader("print_flag") <> "Y" Then
                            btnReStatus.Visible = True
                        End If

                    End If
                    myReader.Close()
                Catch ex As Exception
                    strResult = "" & ex.Message
                End Try
                lblResultCheck.Text = strResult
            End If

            myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.StoredProcedure, "sp_Form_EDI_SelectAttachFile_NewDS", New SqlParameter("@INVH_RUN_AUTO", RefNo))
            grdAttachFile.DataSource = myReader1
            grdAttachFile.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim result As Integer
            Dim RefNo As String = ""
            RefNo = lblInvh_Run_Auto.Text
            objConn1 = New SqlConnection(strEDIConn)

            Dim prm(3) As SqlClient.SqlParameter
            prm(0) = New SqlParameter("@INVH_RUN_AUTO", RefNo)
            prm(1) = New SqlParameter("@START_CHCEK_TIME", START_CHCEK_TIME.Value)
            prm(2) = New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(lblUser_ID.Text.ToLower))

            result = SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_form_edi_attachdoc_NewDS", prm)

            If result > 0 Then

                ' ''Insert ActivityLog data
                ''Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo
                ''SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_edi_insertActivityLog_NewDS2", _
                ''                New SqlParameter("@user_id", CommonUtility.Get_StringValue(objUserInfo.Username)), _
                ''                New SqlParameter("@refno", CommonUtility.Get_StringValue(RefNo)), _
                ''                New SqlParameter("@detail", "�ѹ�֡�� [��ҹ] ��õ�Ǩ�ͺẺ�Ӣ�����͡���Ṻ�����˹�ҷ��"), _
                ''                New SqlParameter("@group_type", 2), _
                ''                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                ''                New SqlParameter("@ip", CommonUtility.Get_String(Request.UserHostAddress)))
                ' ''end

                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('Ẻ���������ͧ �Ţ�����ҧ�ԧ " & RefNo & " [��ҹ] ��õ�Ǩ�ͺ�ҡ���˹�ҷ��! \n�к��ӡ�úѹ�֡�š�õ�Ǩ�ͺ�͡����������º��������'); CloseMyWin();"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript3")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", strScript, False)
                End If

                '//Remove Session
                'Session.Remove("START_CHCEK_TIME")

                '======================================
            Else
                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('�Դ��ͼԴ��Ҵ!\n�������ö�ѹ�֡�š�õ�Ǩ�ͺ�͡�����!');"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript4", strScript, False)
                End If
                '======================================
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSignData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSignData.Click
        '<!-- DS3 edited -->
        Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

        'By Tine 11/3/2558 Check ��ҵ͹ŧ�����ͪ�������բ������������
        If SignID.Value Is Nothing Or SignID.Value = "" Then
            RadAjaxManager1.Alert("��سһԴ��¡�èҡ������͹��ѵ����� ���ͧ�ҡ ����繵� ��辺������!!!")
            Exit Sub
        End If
        'By Tine 11/3/2558 Check ��ҵ͹ŧ�����ͪ�������բ������������
        If UserName.Value Is Nothing Or UserName.Value = "" Then
            RadAjaxManager1.Alert("��سһԴ��¡�èҡ������͹��ѵ����� ���ͧ�ҡ ����繵� ��辺������!!!")
            Exit Sub
        End If

        Dim strCommand As String = "UPDATE FormSign_image SET SignImage_ApproveID='" & SignID.Value & "', SignImage_ApproveName= '" & UserName.Value & "' WHERE im_invh_run_auto = '" & CommonUtility.Get_StringValue(Request.QueryString("RefNo")) & "'"
        Dim m_iReturn As Integer = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, strCommand)

        If m_iReturn = 1 Then
            '======================================
            Dim strscript As String = ""
            strScript = "<script language='javascript'>"
            strScript += "try{ "
            strscript += "alert('�к��ӡ�úѹ�֡�š�õ�Ǩ�ͺ�͡����������º��������'); CloseMyWin();"
            strScript += "}catch(e){ }"
            strScript += "</script>"
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript345")) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript345", strScript, False)
            End If
            '======================================
            'Response.Redirect(EditUrl("ctrlCO_Drafts") & "?COMPANY_TAXNO=" & txtCompanyTaxNo.Text)
        Else
            'lbl_ErrMSG.Text = "�������ö�ѹ�֡�������� ��سҵ�Ǩ�ͺ�����١��ͧ�ͧ������ ��� Certificate ������º���¡�͹"
            Dim strScript As String = "<script language='javascript'>"
            strScript += "try{ "
            strScript += "alert('�Դ��ͼԴ��Ҵ!\n�������ö�ѹ�֡�š�õ�Ǩ�ͺ�͡�����!');"
            strScript += "}catch(e){ }"
            strScript += "</script>"
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript4")) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript4", strScript, False)
            End If
        End If
    End Sub

    Protected Sub btnSignOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSignOK.Click
        ''For DS3
        Try
            Dim result As Integer
            Dim RefNo As String = ""
            RefNo = lblInvh_Run_Auto.Text
            objConn1 = New SqlConnection(strEDIConn)

            Dim prm(3) As SqlClient.SqlParameter
            prm(0) = New SqlParameter("@INVH_RUN_AUTO", RefNo)
            prm(1) = New SqlParameter("@START_CHCEK_TIME", START_CHCEK_TIME.Value)
            prm(2) = New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(lblUser_ID.Text.ToLower))

            result = SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_form_edi_attachdoc_NewDS", prm)


            If result > 0 Then

                ' ''Insert ActivityLog data
                ''Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo
                ''SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_edi_insertActivityLog_NewDS2", _
                ''                New SqlParameter("@user_id", CommonUtility.Get_StringValue(objUserInfo.Username)), _
                ''                New SqlParameter("@refno", CommonUtility.Get_StringValue(RefNo)), _
                ''                New SqlParameter("@detail", "�ѹ�֡�� [��ҹ] ��õ�Ǩ�ͺẺ�Ӣ�����͡���Ṻ�����˹�ҷ��"), _
                ''                New SqlParameter("@group_type", 2), _
                ''                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                ''                New SqlParameter("@ip", CommonUtility.Get_String(Request.UserHostAddress)))
                ' ''end

                'For DS3 Edited.
                'get data from database
                Dim strEdiHeaderData As String = ""

                '=== 20100925 edited ===
                Dim ds As DataSet = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_edi_getDataForSign_NewDS3",
                            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(RefNo)),
                            New SqlParameter("@FORM_NO", ""))

                strEdiHeaderData = CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("HEADER_DATA")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA2")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA3"))

                'set string data for signature
                DataText.Value = strEdiHeaderData

                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ var objSign; objSign = new ActiveXObject('NTISignLib.Signature'); var sdataforsign = document.getElementById('" & DataText.ClientID & "').value; "
                strScript += "var sSign = objSign.SignString('" & UserTaxID.Value & "', sdataforsign);"
                strScript += "document.getElementById('" & DataSigned.ClientID & "').value = sSign;"
                strScript += "}catch(e){ alert('Error: �������öŧ�����ͪ�������硷�͹ԡ���� \n��سҵ�Ǩ�ͺ Certificate ���������ҹ������º���¡�͹');}"
                strScript += "document.getElementById('" & btnSignData.ClientID & "').click();"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript3")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", strScript, False)
                End If
                '<!-- end DS3 -->


            Else
                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('�Դ��ͼԴ��Ҵ!\n�������ö�ѹ�֡�š�õ�Ǩ�ͺ�͡�����!');"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript4", strScript, False)
                End If
                '======================================
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim result As Integer
            Dim RefNo As String = ""
            RefNo = lblInvh_Run_Auto.Text
            objConn1 = New SqlConnection(strEDIConn)

            Dim prm(3) As SqlClient.SqlParameter
            prm(0) = New SqlParameter("@INVH_RUN_AUTO", RefNo)
            prm(1) = New SqlParameter("@START_CHCEK_TIME", START_CHCEK_TIME.Value)
            prm(2) = New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(lblUser_ID.Text.ToLower))

            '//������͹��ѵԷѹ��
            '//1. ��ҹ��õ�Ǩ
            '//2. �����˹ѧ����Ѻ�ͧ
            '//3. ͹��ѵ�
            '//step ������ ������ sp_common_form_edi_attachdoc_NewDS_ASW
            result = SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_form_edi_attachdoc_NewDS_ASW", prm)

            If result > 0 Then

                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('Ẻ���������ͧ �Ţ�����ҧ�ԧ " & RefNo & " [��ҹ] ��õ�Ǩ�ͺ�ҡ���˹�ҷ��! \n�к��ӡ�úѹ�֡�š�õ�Ǩ�ͺ�͡����������º��������'); CloseMyWin();"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript3")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", strScript, False)
                End If

                '//Remove Session
                'Session.Remove("START_CHCEK_TIME")

                '======================================
            Else
                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('�Դ��ͼԴ��Ҵ!\n�������ö�ѹ�֡�š�õ�Ǩ�ͺ�͡�����!');"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript4", strScript, False)
                End If
                '======================================
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class