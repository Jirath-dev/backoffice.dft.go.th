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

Namespace NTi.Modules.DFT_EDI_EditReceipt
    Partial Class ViewDFT_EDI_EditReceipt
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtInvh_run_auto.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnGetData1.UniqueID + "').click();return false;}} else {return true}; ")
            txtReference_code2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnGetData2.UniqueID + "').click();return false;}} else {return true}; ")
            txtTotal_set.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtSet_price.UniqueID + "').focus();return false;}} else {return true}; ")
            txtSet_price.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAmt.UniqueID + "').focus();return false;}} else {return true}; ")
            txtAmt.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAmt_bahttext.UniqueID + "').focus();return false;}} else {return true}; ")
            txtAmt_bahttext.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnOK.UniqueID + "').click();return false;}} else {return true}; ")
            Me.btnDelete.Attributes.Add("onClick", "return confirm('ยืนยันการลบรายการออกจากหมายเลขใบเสร็จฉบับนี้ ?')")

            ''ByTine 08-01-2559 ปรับใบเสร็จใหม่
            txtInvh_run_auto_v2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnGetData1.UniqueID + "').click();return false;}} else {return true}; ")
            txtReference_code2_v2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnGetData2.UniqueID + "').click();return false;}} else {return true}; ")
            txtTotal_set_v2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtSet_price.UniqueID + "').focus();return false;}} else {return true}; ")
            txtSet_price_v2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAmt.UniqueID + "').focus();return false;}} else {return true}; ")
            txtAmt_v2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAmt_bahttext.UniqueID + "').focus();return false;}} else {return true}; ")
            txtAmt_bahttext.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnOK.UniqueID + "').click();return false;}} else {return true}; ")
            Me.btnDelete_v2.Attributes.Add("onClick", "return confirm('ยืนยันการลบรายการออกจากหมายเลขใบเสร็จฉบับนี้ ?')")

            If Not Page.IsPostBack Then
                Call LoadSitePlus()

                tblData1.Visible = True
                tblData2.Visible = True
                tblData3.Visible = True

                txtReceiptSearch.Text = ""
                txtReceiptSearch.Focus()

                btnOK.Enabled = False
                btnCancel.Enabled = False

                ''ByTine 08-01-2559 ปรับใบเสร็จใหม่
                Table1.Visible = True
                Table2.Visible = True
                Table3.Visible = True

                txtReceiptSearch_v2.Text = ""
                txtReceiptSearch_v2.Focus()

                btnOK_v2.Enabled = False
                btnCancel_v2.Enabled = False

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
                '        Exit For
                'End Select
            Next i
        End Sub
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
        Private Sub LoadSitePlus()
            Try
                Dim ds As New DataSet
                'vi_select_SitePlus 2, ''
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_SitePlus", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(2)), _
                New SqlParameter("@Para1", ""))

                If ds.Tables(0).Rows.Count > 0 Then
                    rcbSitePlus.DataSource = ds.Tables(0)
                    rcbSitePlus.DataTextField = "site_name"
                    rcbSitePlus.DataValueField = "site_id"
                    rcbSitePlus.DataBind()

                    rcbSitePlus.SelectedValue = "ST-001"

                    ''ByTine 08-01-2559 ปรับใบเสร็จใหม่
                    rcbSitePlus_v2.DataSource = ds.Tables(0)
                    rcbSitePlus_v2.DataTextField = "site_name"
                    rcbSitePlus_v2.DataValueField = "site_id"
                    rcbSitePlus_v2.DataBind()

                    rcbSitePlus_v2.SelectedValue = "ST-001"
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Dim ds As New DataSet
            ds = SearchReceipt(txtReceiptSearch.Text.Trim(), "1")
            rgBill_Receipt_Detail.DataSource = ds.Tables(0)
            rgBill_Receipt_Detail.DataBind()

            If rgBill_Receipt_Detail.MasterTableView.Items.Count > 0 Then
                tblData1.Visible = True
                txtBill_No.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("bill_no"))
                txtBill_Date.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("bill_date"))
                txtReceipt_Name.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("receipt_name"))
                txtSite_ID.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("site_id"))
                txtReceipt_By.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("receipt_by"))
                txtRemark.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("remark"))

                tblData2.Visible = True
                txtInvh_run_auto.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("invh_run_auto"))
                txtReference_code2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("reference_code2"))
                txtTotal_set.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("total_set"))
                txtSet_price.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("set_price"))
                txtAmt.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("amt"))
                txtAmt_bahttext.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("amt_bahttext"))
                txtRpt_set.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("rpt_set"))

                tblData3.Visible = True
                rgBill_Receipt_Detail.MasterTableView.Items(0).Selected = True

            Else
                tblData1.Visible = True
                tblData2.Visible = True
                tblData3.Visible = True
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลใบเสร็จรับเงินที่ทำการค้นหา');")
            End If
        End Sub

        Function SearchReceipt(ByVal Bill_No As String, ByVal BillType As String) As DataSet
            Dim SpName, TempSiteID As String

            ''ByTine 11-01-2559 ปรับแก้ใบเสร็จใหม่
            SpName = "vi_select_EditReceipt"
            TempSiteID = rcbSitePlus.SelectedValue
            If BillType = 2 Then
                SpName = "vi_select_EditReceipt_v2"
                TempSiteID = rcbSitePlus_v2.SelectedValue
            End If

            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SpName, _
            New SqlParameter("@TCat", CommonUtility.Get_Int32(1)), _
            New SqlParameter("@bill_no", CommonUtility.Get_StringValue(Bill_No)), _
            New SqlParameter("@site_id", CommonUtility.Get_StringValue(TempSiteID)))

            Return ds
        End Function

        Private Sub rgBill_Receipt_Detail_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgBill_Receipt_Detail.ItemCommand
            If e.CommandName = "SELECTED" Then
                txtInvh_run_auto.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto")
                txtReference_code2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("reference_code2")
                txtTotal_set.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("total_set")
                txtSet_price.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("set_price")
                txtAmt.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("amt")
                txtAmt_bahttext.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("amt_bahttext")
                txtRpt_set.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("rpt_set")

                e.Item.Selected = True
            End If
        End Sub

        Protected Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
            Call EnableControl("INSERT", "1")
            txtInvh_run_auto.Focus()
            Session("TYPE") = "INSERT"
        End Sub

        Private Sub EnableControl(ByVal TYPE As String, ByVal BillType As String)
            If BillType = 1 Then
                If TYPE = "INSERT" Then
                    txtInvh_run_auto.Text = ""
                    txtReference_code2.Text = ""
                    txtTotal_set.Text = ""
                    txtSet_price.Text = ""
                    txtAmt.Text = ""
                    txtAmt_bahttext.Text = ""
                    txtRpt_set.Text = "1"
                End If

                txtInvh_run_auto.ReadOnly = False
                txtReference_code2.ReadOnly = False
                txtTotal_set.ReadOnly = False
                txtSet_price.ReadOnly = False
                txtAmt.ReadOnly = False
                txtAmt_bahttext.ReadOnly = False

                btnInsert.Enabled = False
                btnUpdate.Enabled = False
                btnDelete.Enabled = False

                btnOK.Enabled = True
                btnCancel.Enabled = True

            ElseIf BillType = 2 Then
                If TYPE = "INSERT" Then
                    txtInvh_run_auto_v2.Text = ""
                    txtReference_code2_v2.Text = ""
                    txtTotal_set_v2.Text = ""
                    txtSet_price_v2.Text = ""
                    txtAmt_v2.Text = ""
                    txtAmt_bahttext_v2.Text = ""
                    txtRpt_set_v2.Text = "1"
                End If

                txtInvh_run_auto_v2.ReadOnly = False
                txtReference_code2_v2.ReadOnly = False
                txtTotal_set_v2.ReadOnly = False
                txtSet_price_v2.ReadOnly = False
                txtAmt_v2.ReadOnly = False
                txtAmt_bahttext_v2.ReadOnly = False

                btnInsert_v2.Enabled = False
                btnUpdate_v2.Enabled = False
                btnDelete_v2.Enabled = False

                btnOK_v2.Enabled = True
                btnCancel_v2.Enabled = True
            End If

        End Sub

        Private Sub DisableControl(ByVal BillType As String)
            Dim myDataGridItem As GridDataItem

            If BillType = 1 Then
                For Each myDataGridItem In rgBill_Receipt_Detail.MasterTableView.Items
                    If myDataGridItem.Selected = True Then
                        txtInvh_run_auto.Text = myDataGridItem.Item("invh_run_auto").Text
                        txtReference_code2.Text = myDataGridItem.Item("reference_code2").Text
                        txtTotal_set.Text = myDataGridItem.Item("total_set").Text
                        txtSet_price.Text = myDataGridItem.Item("set_price").Text
                        txtAmt.Text = myDataGridItem.Item("amt").Text
                        txtAmt_bahttext.Text = myDataGridItem.Item("amt_bahttext").Text
                        txtRpt_set.Text = myDataGridItem.Item("rpt_set").Text

                        Exit For
                    End If
                Next

                txtInvh_run_auto.ReadOnly = True
                txtReference_code2.ReadOnly = True
                txtTotal_set.ReadOnly = True
                txtSet_price.ReadOnly = True
                txtAmt.ReadOnly = True
                txtAmt_bahttext.ReadOnly = True

                btnInsert.Enabled = True
                btnUpdate.Enabled = True
                btnDelete.Enabled = True

                btnOK.Enabled = False
                btnCancel.Enabled = False

            ElseIf BillType = 2 Then
                For Each myDataGridItem In rgBill_Receipt_Detail_v2.MasterTableView.Items
                    If myDataGridItem.Selected = True Then
                        txtInvh_run_auto_v2.Text = myDataGridItem.Item("invh_run_auto").Text
                        txtReference_code2_v2.Text = myDataGridItem.Item("reference_code2").Text
                        txtTotal_set_v2.Text = myDataGridItem.Item("total_set").Text
                        txtSet_price_v2.Text = myDataGridItem.Item("set_price").Text
                        txtAmt_v2.Text = myDataGridItem.Item("amt").Text
                        txtAmt_bahttext_v2.Text = myDataGridItem.Item("amt_bahttext").Text
                        txtRpt_set_v2.Text = myDataGridItem.Item("rpt_set").Text

                        Exit For
                    End If
                Next

                txtInvh_run_auto_v2.ReadOnly = True
                txtReference_code2_v2.ReadOnly = True
                txtTotal_set_v2.ReadOnly = True
                txtSet_price_v2.ReadOnly = True
                txtAmt_v2.ReadOnly = True
                txtAmt_bahttext_v2.ReadOnly = True

                btnInsert_v2.Enabled = True
                btnUpdate_v2.Enabled = True
                btnDelete_v2.Enabled = True

                btnOK_v2.Enabled = False
                btnCancel_v2.Enabled = False

            End If
        End Sub

        Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Call DisableControl("1")
        End Sub

        Protected Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_mani_EditReceipt1_NewDS", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(3)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtBill_No.Text)), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(rcbSitePlus.SelectedValue)), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtInvh_run_auto.Text.Trim())), _
                New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(txtReference_code2.Text)), _
                New SqlParameter("@total_set", CommonUtility.Get_Int32(txtTotal_set.Text)), _
                New SqlParameter("@amt_bahttext", CommonUtility.Get_StringValue(txtAmt_bahttext.Text)), _
                New SqlParameter("@receipt_by", CommonUtility.Get_StringValue(txtReceipt_By.Text)), _
                New SqlParameter("@rpt_set", CommonUtility.Get_Int32(txtRpt_set.Text)))

                If ds.Tables(0).Rows.Count > 0 Then
                    If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retStatus")) = "0" Then
                        Dim ds1 As New DataSet
                        ds1 = SearchReceipt(txtReceiptSearch.Text.Trim(), "1")
                        rgBill_Receipt_Detail.DataSource = ds1.Tables(0)
                        rgBill_Receipt_Detail.DataBind()

                        If rgBill_Receipt_Detail.MasterTableView.Items.Count > 0 Then
                            tblData1.Visible = True
                            txtBill_No.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_no"))
                            txtBill_Date.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_date"))
                            txtReceipt_Name.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_name"))
                            txtSite_ID.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("site_id"))
                            txtReceipt_By.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_by"))
                            txtRemark.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("remark"))

                            tblData2.Visible = True
                            txtInvh_run_auto.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("invh_run_auto"))
                            txtReference_code2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("reference_code2"))
                            txtTotal_set.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("total_set"))
                            txtSet_price.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("set_price"))
                            txtAmt.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt"))
                            txtAmt_bahttext.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt_bahttext"))
                            txtRpt_set.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("rpt_set"))

                            tblData3.Visible = True
                            rgBill_Receipt_Detail.MasterTableView.Items(0).Selected = True

                        Else
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลใบเสร็จรับเงินที่ทำการค้นหา');")
                        End If
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End If
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            Call EnableControl("UPDATE", "1")
            txtInvh_run_auto.Focus()
            Session("TYPE") = "UPDATE"
        End Sub

        Protected Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Try
                'vi_mani_EditReceipt1 2, '2009-0000001', 'ST-001', '20081230-001028', 'D2009-0000004', 1, '(à¡éÒÊÔººÒ·¶éÇ¹xx)', 1
                Dim TCAT As Integer
                Select Case Session("TYPE")
                    Case "INSERT"
                        TCAT = 1
                    Case "UPDATE"
                        TCAT = 2
                End Select

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_mani_EditReceipt1_NewDS", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(TCAT)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtBill_No.Text)), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(rcbSitePlus.SelectedValue)), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtInvh_run_auto.Text.Trim())), _
                New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(txtReference_code2.Text)), _
                New SqlParameter("@total_set", CommonUtility.Get_Int32(txtTotal_set.Text)), _
                New SqlParameter("@amt_bahttext", CommonUtility.Get_StringValue(txtAmt_bahttext.Text)), _
                New SqlParameter("@receipt_by", CommonUtility.Get_StringValue(txtReceipt_By.Text)), _
                New SqlParameter("@rpt_set", CommonUtility.Get_Int32(txtRpt_set.Text)))

                If ds.Tables(0).Rows.Count > 0 Then
                    If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retStatus")) = "0" Then
                        Dim ds1 As New DataSet
                        ds1 = SearchReceipt(txtReceiptSearch.Text.Trim(), "1")
                        rgBill_Receipt_Detail.DataSource = ds1.Tables(0)
                        rgBill_Receipt_Detail.DataBind()

                        If rgBill_Receipt_Detail.MasterTableView.Items.Count > 0 Then
                            tblData1.Visible = True
                            txtBill_No.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_no"))
                            txtBill_Date.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_date"))
                            txtReceipt_Name.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_name"))
                            txtSite_ID.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("site_id"))
                            txtReceipt_By.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_by"))
                            txtRemark.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("remark"))

                            tblData2.Visible = True
                            txtInvh_run_auto.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("invh_run_auto"))
                            txtReference_code2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("reference_code2"))
                            txtTotal_set.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("total_set"))
                            txtSet_price.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("set_price"))
                            txtAmt.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt"))
                            txtAmt_bahttext.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt_bahttext"))
                            txtRpt_set.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("rpt_set"))

                            tblData3.Visible = True
                            rgBill_Receipt_Detail.MasterTableView.Items(0).Selected = True

                            Call DisableControl("1")
                        Else
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลใบเสร็จรับเงินที่ทำการค้นหา');")
                        End If
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End If
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnGetData1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetData1.Click
            Try
                'vi_select_EditReceipt 2, '20081230-001028', ''
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_EditReceipt_NewDS", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(2)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtInvh_run_auto.Text)), _
                New SqlParameter("@site_id", ""))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtReference_code2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("reference_code2"))
                    txtSet_price.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("price"))

                    txtReference_code2.Focus()
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnGetData2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetData2.Click
            Try
                'vi_select_EditReceipt 3, 'D2009-0000004', ''
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_EditReceipt_NewDS", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(3)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtReference_code2.Text)), _
                New SqlParameter("@site_id", ""))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtInvh_run_auto.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("invh_run_auto"))
                    txtSet_price.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("price"))

                    txtTotal_set.Focus()
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnCreateReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateReceive.Click
            Try
                If txtReceipt_Name.Text.Trim() = String.Empty Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุชื่อลูกค้า !!!');")
                    txtReceipt_Name.Focus()
                    Exit Sub
                End If

                If txtRemark.Text.Trim() = String.Empty Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุเหตุผลการสร้างใบเสร็จรับเงิน !!!');")
                    txtRemark.Focus()
                    Exit Sub
                End If

                'sp_common_CreateReceiptH 'Administrator', 'TEST', 0, 'TEST', 'ST-001'
                'vi_select_EditReceipt 1, '2010-0000004', 'ST-001'
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_CreateReceiptH_NewDS", _
                New SqlParameter("@RECEIPT_BY", CommonUtility.Get_StringValue(txtReceipt_By.Text)), _
                New SqlParameter("@REMARK", CommonUtility.Get_StringValue(txtRemark.Text)), _
                New SqlParameter("@AMOUNT", CommonUtility.Get_Decimal(0)), _
                New SqlParameter("@RECEIPT_NAME", CommonUtility.Get_StringValue(txtReceipt_Name.Text)), _
                New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblRoleID.Text)))

                If ds.Tables(0).Rows.Count > 0 Then
                    If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                        txtReceiptSearch.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMsg"))
                        txtBill_No.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMsg"))
                        txtBill_Date.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retBillDate"))
                        txtSite_ID.Text = lblRoleID.Text
                        'txtReceipt_By.Text = CommonUtility.Get_StringValue(UserInfo.Username)

                        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_EditReceipt_NewDS", _
                        New SqlParameter("@TCat", CommonUtility.Get_Int32(1)), _
                        New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtBill_No.Text)), _
                        New SqlParameter("@site_id", CommonUtility.Get_StringValue(txtSite_ID.Text)))

                        If ds.Tables(0).Rows.Count > 0 Then
                            rgBill_Receipt_Detail.DataSource = ds.Tables(0)
                            rgBill_Receipt_Detail.DataBind()
                        End If
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดข้อผิดพลาดไสามารถทำการสร้างใบเสร็จได้ กรุณาผู้ติดต่อผู้ดูและระบบ !!!');")
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

#Region "ByTine 11-01-2559 Code ที่ปรับใช้สำหรับทำใบเสร็จใหม่ทั้งหมด"
        ''ByTine 08-01-2559 ปรับใบเสร็จใหม่
        Protected Sub btnSearch_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_v2.Click
            Dim ds As New DataSet
            ds = SearchReceipt(txtReceiptSearch_v2.Text.Trim(), "2")
            rgBill_Receipt_Detail_v2.DataSource = ds.Tables(0)
            rgBill_Receipt_Detail_v2.DataBind()

            If rgBill_Receipt_Detail_v2.MasterTableView.Items.Count > 0 Then
                Table1.Visible = True
                txtBill_No_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("bill_no"))
                txtBill_Date_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("bill_date"))
                txtReceipt_Name_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("receipt_name"))
                txtSite_ID_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("site_id"))
                txtReceipt_By_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("receipt_by"))
                txtRemark_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("remark"))

                Table2.Visible = True
                txtInvh_run_auto_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("invh_run_auto"))
                txtReference_code2_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("reference_code2"))
                txtTotal_set_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("total_set"))
                txtSet_price_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("set_price"))
                txtAmt_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("amt"))
                txtAmt_bahttext_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("amt_bahttext"))
                txtRpt_set_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("rpt_set"))

                Table3.Visible = True
                rgBill_Receipt_Detail_v2.MasterTableView.Items(0).Selected = True

            Else
                Table1.Visible = True
                Table2.Visible = True
                Table3.Visible = True
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลใบเสร็จรับเงินที่ทำการค้นหา');")
            End If
        End Sub

        Private Sub rgBill_Receipt_Detail_v2_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgBill_Receipt_Detail_v2.ItemCommand
            If e.CommandName = "SELECTED" Then
                txtInvh_run_auto_v2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto")
                txtReference_code2_v2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("reference_code2")
                txtTotal_set_v2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("total_set")
                txtSet_price_v2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("set_price")
                txtAmt_v2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("amt")
                txtAmt_bahttext_v2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("amt_bahttext")
                txtRpt_set_v2.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("rpt_set")

                e.Item.Selected = True
            End If
        End Sub

        Protected Sub btnInsert_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert_v2.Click
            Call EnableControl("INSERT", "2")
            txtInvh_run_auto_v2.Focus()
            Session("TYPE") = "INSERT"
        End Sub

        Protected Sub btnCancel_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel_v2.Click
            Call DisableControl("2")
        End Sub

        Protected Sub btnDelete_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_v2.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_mani_EditReceipt1_NewDS_v2", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(3)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtBill_No_v2.Text)), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(rcbSitePlus_v2.SelectedValue)), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtInvh_run_auto_v2.Text.Trim())), _
                New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(txtReference_code2_v2.Text)), _
                New SqlParameter("@total_set", CommonUtility.Get_Int32(txtTotal_set_v2.Text)), _
                New SqlParameter("@amt_bahttext", CommonUtility.Get_StringValue(txtAmt_bahttext_v2.Text)), _
                New SqlParameter("@receipt_by", CommonUtility.Get_StringValue(txtReceipt_By_v2.Text)), _
                New SqlParameter("@rpt_set", CommonUtility.Get_Int32(txtRpt_set_v2.Text)))

                If ds.Tables(0).Rows.Count > 0 Then
                    If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retStatus")) = "0" Then
                        Dim ds1 As New DataSet
                        ds1 = SearchReceipt(txtReceiptSearch_v2.Text.Trim(), "2")
                        rgBill_Receipt_Detail_v2.DataSource = ds1.Tables(0)
                        rgBill_Receipt_Detail_v2.DataBind()

                        If rgBill_Receipt_Detail_v2.MasterTableView.Items.Count > 0 Then
                            Table1.Visible = True
                            txtBill_No_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_no"))
                            txtBill_Date_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_date"))
                            txtReceipt_Name_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_name"))
                            txtSite_ID_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("site_id"))
                            txtReceipt_By_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_by"))
                            txtRemark_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("remark"))

                            Table2.Visible = True
                            txtInvh_run_auto_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("invh_run_auto"))
                            txtReference_code2_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("reference_code2"))
                            txtTotal_set_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("total_set"))
                            txtSet_price_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("set_price"))
                            txtAmt_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt"))
                            txtAmt_bahttext_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt_bahttext"))
                            txtRpt_set_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("rpt_set"))

                            Table3.Visible = True
                            rgBill_Receipt_Detail_v2.MasterTableView.Items(0).Selected = True

                        Else
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลใบเสร็จรับเงินที่ทำการค้นหา');")
                        End If
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End If
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnUpdate_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate_v2.Click
            Call EnableControl("UPDATE", "2")
            txtInvh_run_auto_v2.Focus()
            Session("TYPE") = "UPDATE"
        End Sub

        Protected Sub btnOK_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK_v2.Click
            Try
                ''ByTine 11-01-2559 ปรับใบเสร็จใหม่
                Dim TCAT As Integer
                Select Case Session("TYPE")
                    Case "INSERT"
                        TCAT = 1
                    Case "UPDATE"
                        TCAT = 2
                End Select

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_mani_EditReceipt1_NewDS_v2", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(TCAT)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtBill_No_v2.Text)), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(rcbSitePlus_v2.SelectedValue)), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtInvh_run_auto_v2.Text.Trim())), _
                New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(txtReference_code2_v2.Text)), _
                New SqlParameter("@total_set", CommonUtility.Get_Int32(txtTotal_set_v2.Text)), _
                New SqlParameter("@amt_bahttext", CommonUtility.Get_StringValue(txtAmt_bahttext_v2.Text)), _
                New SqlParameter("@receipt_by", CommonUtility.Get_StringValue(txtReceipt_By_v2.Text)), _
                New SqlParameter("@rpt_set", CommonUtility.Get_Int32(txtRpt_set_v2.Text)))

                If ds.Tables(0).Rows.Count > 0 Then
                    If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retStatus")) = "0" Then
                        Dim ds1 As New DataSet
                        ds1 = SearchReceipt(txtReceiptSearch_v2.Text.Trim(), "2")
                        rgBill_Receipt_Detail_v2.DataSource = ds1.Tables(0)
                        rgBill_Receipt_Detail_v2.DataBind()

                        If rgBill_Receipt_Detail_v2.MasterTableView.Items.Count > 0 Then
                            Table1.Visible = True
                            txtBill_No_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_no"))
                            txtBill_Date_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("bill_date"))
                            txtReceipt_Name_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_name"))
                            txtSite_ID_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("site_id"))
                            txtReceipt_By_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("receipt_by"))
                            txtRemark_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("remark"))

                            Table2.Visible = True
                            txtInvh_run_auto_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("invh_run_auto"))
                            txtReference_code2_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("reference_code2"))
                            txtTotal_set_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("total_set"))
                            txtSet_price_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("set_price"))
                            txtAmt_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt"))
                            txtAmt_bahttext_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("amt_bahttext"))
                            txtRpt_set_v2.Text = CommonUtility.Get_StringValue(ds1.Tables(0).Rows(0).Item("rpt_set"))

                            Table3.Visible = True
                            rgBill_Receipt_Detail_v2.MasterTableView.Items(0).Selected = True

                            Call DisableControl("2")
                        Else
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลใบเสร็จรับเงินที่ทำการค้นหา');")
                        End If
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End If
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnGetData1_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetData1_v2.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_EditReceipt_NewDS_v2", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(2)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtInvh_run_auto_v2.Text)), _
                New SqlParameter("@site_id", ""))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtReference_code2_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("reference_code2"))
                    txtSet_price_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("price"))

                    txtReference_code2_v2.Focus()
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnGetData2_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetData2_v2.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_EditReceipt_NewDS_v2", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(3)), _
                New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtReference_code2_v2.Text)), _
                New SqlParameter("@site_id", ""))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtInvh_run_auto_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("invh_run_auto"))
                    txtSet_price_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("price"))

                    txtTotal_set_v2.Focus()
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Protected Sub btnCreateReceive_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateReceive_v2.Click
            Try
                If txtReceipt_Name_v2.Text.Trim() = String.Empty Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุชื่อลูกค้า !!!');")
                    txtReceipt_Name_v2.Focus()
                    Exit Sub
                End If

                If txtRemark_v2.Text.Trim() = String.Empty Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุเหตุผลการสร้างใบเสร็จรับเงิน !!!');")
                    txtRemark_v2.Focus()
                    Exit Sub
                End If

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_CreateReceiptH_NewDS_v2", _
                New SqlParameter("@RECEIPT_BY", CommonUtility.Get_StringValue(txtReceipt_By_v2.Text)), _
                New SqlParameter("@REMARK", CommonUtility.Get_StringValue(txtRemark_v2.Text)), _
                New SqlParameter("@AMOUNT", CommonUtility.Get_Decimal(0)), _
                New SqlParameter("@RECEIPT_NAME", CommonUtility.Get_StringValue(txtReceipt_Name_v2.Text)), _
                New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblRoleID.Text)))

                If ds.Tables(0).Rows.Count > 0 Then
                    If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                        txtReceiptSearch_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMsg"))
                        txtBill_No_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMsg"))
                        txtBill_Date_v2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retBillDate"))
                        txtSite_ID_v2.Text = lblRoleID.Text
                        'txtReceipt_By.Text = CommonUtility.Get_StringValue(UserInfo.Username)

                        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_EditReceipt_NewDS_v2", _
                        New SqlParameter("@TCat", CommonUtility.Get_Int32(1)), _
                        New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtBill_No_v2.Text)), _
                        New SqlParameter("@site_id", CommonUtility.Get_StringValue(txtSite_ID_v2.Text)))

                        If ds.Tables(0).Rows.Count > 0 Then
                            rgBill_Receipt_Detail_v2.DataSource = ds.Tables(0)
                            rgBill_Receipt_Detail_v2.DataBind()
                        End If
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดข้อผิดพลาดไสามารถทำการสร้างใบเสร็จได้ กรุณาผู้ติดต่อผู้ดูและระบบ !!!');")
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

#End Region
    End Class

End Namespace
