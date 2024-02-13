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
                lblCheckResult.Text = "<div class=""check-result"">*** คำขอหนังสือรับรองรายการนี้ตรวจสอบเรียบร้อยแล้ว ***</div>"
            End If

            'ตรวจสอบบริษัท สมัครใจ
            If CheckValunteer(CommonUtility.Get_StringValue(Request.QueryString("TaxNo"))) = False Then
                panelFile.Visible = True
                Call LoadAttachFile(lblInvh_Run_Auto.Text)
                Call LoadAttachXML(lblInvh_Run_Auto.Text)
                lblValunteer2.Visible = False
            Else
                panelFile.Visible = False
                lblValunteer2.Visible = True
            End If

            objConn1.Close()

            'Set Session For Checkform KPI
            START_CHCEK_TIME.Value = Date.Now

            'ตรวจสอบ Seal & Sign
            CheckSealSign()

        End If
    End Sub

    Private Sub CheckSealSign()
        objConn1 = New SqlConnection(strEDIConn)

        myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.Text, "SELECT im_invh_run_auto FROM FormSign_image WHERE im_invh_run_auto='" & lblInvh_Run_Auto.Text & "'  AND (SignImageID <> null OR SignImageID <> '')")
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

        'ตรวจสอบบริษัทที่ขอแบบ สมัครใจ ไม่ต้องมีเอกสารแนบ ให้ซ่อนช่องแสดงเอกสารแนบไว้ - 11 Dec 2012 by prayut
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

        Else
            isValunteer = False
        End If

        Return isValunteer

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
                End If

                myReader1.Close()

                If userview <> "" Then
                    'btnNotApproved.Visible = False
                    'Button1.Visible = False
                    Panel1.Visible = True
                    lblMsg.Text = "เลขที่ใบคำร้อง " & RefNo & " กำลังถูกตรวจสอบโดย " & userview & vbCrLf '& " ดังนั้นจึงดูได้อย่างเดียว ไม่สามารถบันทึกข้อมูลใดๆ ได้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", "<script language='javascript'>alert('เลขที่ใบคำร้อง " & RefNo & " กำลังถูกตรวจสอบโดย " & userview & "');</script>", False)
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
                        strResult = "สถานะ: " & myReader("Check_Result") & _
                                    "<br/>วันที่ส่งคำขอ: " & myReader("Sent_Date") & "&nbsp;&nbsp;วันที่ตรวจสอบ: " & myReader("CheckDoc_Date") & " &nbsp;&nbsp;โดย: " & myReader("CheckDoc_By").ToString() & _
                                    "<br/>วันที่พิมพ์ฟอร์ม: " & myReader("printFormDate") & "&nbsp;&nbsp;โดย: " & myReader("UserPrintForm")

                        If myReader("CheckDoc_By").ToString.ToLower.Equals(objUserInfo.Username.ToLower) And myReader("edi_status_id") <> "A" And myReader("print_flag") <> "Y" Then
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
                ''                New SqlParameter("@detail", "บันทึกผล [ผ่าน] การตรวจสอบแบบคำขอและเอกสารแนบโดยเจ้าหน้าที่"), _
                ''                New SqlParameter("@group_type", 2), _
                ''                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                ''                New SqlParameter("@ip", CommonUtility.Get_String(Request.UserHostAddress)))
                ' ''end

                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('แบบฟอร์มคำร้อง เลขที่อ้างอิง " & RefNo & " [ผ่าน] การตรวจสอบจากเจ้าหน้าที่! \nระบบทำการบันทึกผลการตรวจสอบเอกสารเสร็จเรียบร้อยแล้ว'); CloseMyWin();"
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
                strScript += "alert('เกิดข้อผิดพลาด!\nไม่สามารถบันทึกผลการตรวจสอบเอกสารได้!');"
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

        Dim strCommand As String = "UPDATE FormSign_image SET SignImage_ApproveID='" & SignID.Value & "', SignImage_ApproveName= '" & UserName.Value & "' WHERE im_invh_run_auto = '" & CommonUtility.Get_StringValue(Request.QueryString("RefNo")) & "'"
        Dim m_iReturn As Integer = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, strCommand)

        If m_iReturn = 1 Then
            '======================================
            Dim strscript As String = ""
            strScript = "<script language='javascript'>"
            strScript += "try{ "
            strscript += "alert('ระบบทำการบันทึกผลการตรวจสอบเอกสารเสร็จเรียบร้อยแล้ว'); CloseMyWin();"
            strScript += "}catch(e){ }"
            strScript += "</script>"
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript345")) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript345", strScript, False)
            End If
            '======================================
            'Response.Redirect(EditUrl("ctrlCO_Drafts") & "?COMPANY_TAXNO=" & txtCompanyTaxNo.Text)
        Else
            'lbl_ErrMSG.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบความถูกต้องของข้อมูล และ Certificate ให้เรียบร้อยก่อน"
            Dim strScript As String = "<script language='javascript'>"
            strScript += "try{ "
            strScript += "alert('เกิดข้อผิดพลาด!\nไม่สามารถบันทึกผลการตรวจสอบเอกสารได้!');"
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
                ''                New SqlParameter("@detail", "บันทึกผล [ผ่าน] การตรวจสอบแบบคำขอและเอกสารแนบโดยเจ้าหน้าที่"), _
                ''                New SqlParameter("@group_type", 2), _
                ''                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                ''                New SqlParameter("@ip", CommonUtility.Get_String(Request.UserHostAddress)))
                ' ''end

                'For DS3 Edited.
                'get data from database
                Dim strEdiHeaderData As String = ""

                '=== 20100925 edited ===
                Dim ds As DataSet = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_edi_getDataForSign_NewDS3", _
                            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(RefNo)), _
                            New SqlParameter("@FORM_NO", ""))

                strEdiHeaderData = CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("HEADER_DATA")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA2")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA3"))

                'set string data for signature
                DataText.Value = strEdiHeaderData

                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ var objSign; objSign = new ActiveXObject('NTISignLib.Signature'); var sdataforsign = document.getElementById('" & DataText.ClientID & "').value; "
                strScript += "var sSign = objSign.SignString('" & UserTaxID.Value & "', sdataforsign);"
                strScript += "document.getElementById('" & DataSigned.ClientID & "').value = sSign;"
                strScript += "}catch(e){ alert('Error: ไม่สามารถลงลายมือชื่ออิเล็กทรอนิกส์ได้ \nกรุณาตรวจสอบ Certificate ให้พร้อมใช้งานให้เรียบร้อยก่อน');}"
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
                strScript += "alert('เกิดข้อผิดพลาด!\nไม่สามารถบันทึกผลการตรวจสอบเอกสารได้!');"
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