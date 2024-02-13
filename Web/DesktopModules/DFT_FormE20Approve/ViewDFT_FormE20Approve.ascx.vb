Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Namespace NTi.Modules.DFT_FormE20Approve

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_FormE20Approve class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_FormE20Approve
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim _UserName As String = CommonUtility.Get_StringValue(DotNetNuke.Entities.Users.UserController.GetUser(PortalId, UserId, False).Username)


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

        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("th-TH")
            txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
            lblError.Text = ""
            If Not Page.IsPostBack Then
                txtSearch.Focus()
            End If
        End Sub

#Region "by rut"
        Function DataCompany_byTax(ByVal By_tax As String) As DataSet
            Dim SQL As String = ""
            SQL = "SELECT     * FROM         company WHERE     (company_taxno = @company_taxno)"

            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@company_taxno", By_tax)

            Dim ds As DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, SQL, prm)

            Return ds
        End Function
        Sub FormSet_Com()
            With DataCompany_byTax(txtSearch.Text.Trim).Tables(0)
                Panel_Company.Visible = True
                lblcompany_thai.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_thai"))
                lblcompany_eng.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_eng"))
                lblcompany_taxno.Text = CommonUtility.Get_StringValue(.Rows(0).Item("company_taxno"))

                If DataFormE_byTax(txtSearch.Text.Trim).Tables(0).Rows.Count > 0 Then
                    With DataFormE_byTax(txtSearch.Text.Trim).Tables(0).Rows(0)
                        Select Case CommonUtility.Get_StringValue(.Item("statusUse"))
                            Case "0", "" 'ปิดการใช้งาน
                                rdoStatus.Items(1).Selected = True
                            Case "1" 'เปิดการใช้งาน
                                rdoStatus.Items(0).Selected = True
                        End Select
                        txtRemarks.Text = CommonUtility.Get_StringValue(.Item("remark"))
                    End With
                Else
                    btnAddCo.Enabled = True
                    lblError.Text = "ยังไม่เคยเปิดการใช้งาน"
                End If
            End With
        End Sub

        'check ใน ฐานว่าเคยเพิ่มแล้วหรือยัง
        Function DataFormE_byTax(ByVal By_tax As String) As DataSet
            Dim YesOrNoCase As Boolean = False

            Dim SQL As String = ""
            SQL = "SELECT    * FROM         FormEStatusTax WHERE     (company_taxno = @company_taxno)"

            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@company_taxno", By_tax)

            Dim ds As DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, SQL, prm)

            Return ds
        End Function

        Function AddFormETax_com(ByVal By_Case As String, ByVal By_status As String) As Boolean
            Try
                Dim sql As String = ""

                Select Case By_Case
                    Case "0"
                        sql = "INSERT      " & _
                                "INTO            FormEStatusTax(company_taxno, statusUse, remark, ByCreateDate, ByUpdateDate, ByUser) " & _
                                "VALUES     (@company_taxno,@statusUse,@remark,@ByCreateDate,@ByUpdateDate, @ByUser)"
                        Dim prm(5) As SqlClient.SqlParameter
                        prm(0) = New SqlClient.SqlParameter("@company_taxno", lblcompany_taxno.Text)
                        prm(1) = New SqlClient.SqlParameter("@statusUse", By_status)
                        prm(2) = New SqlClient.SqlParameter("@remark", txtRemarks.Text)
                        prm(3) = New SqlClient.SqlParameter("@ByCreateDate", Date.Now)
                        prm(4) = New SqlClient.SqlParameter("@ByUpdateDate", Date.Now)
                        prm(5) = New SqlClient.SqlParameter("@ByUser", _UserName)

                        SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, sql, prm)

                        Return True
                    Case "1"
                        sql = "UPDATE    FormEStatusTax " & _
                                "SET              statusUse =@statusUse, remark =@remark,ByUpdateDate =@ByUpdateDate, ByUser =@ByUser " & _
                                "WHERE     (company_taxno = @company_taxno)"
                        Dim prm(4) As SqlClient.SqlParameter
                        prm(0) = New SqlClient.SqlParameter("@statusUse", By_status)
                        prm(1) = New SqlClient.SqlParameter("@remark", txtRemarks.Text)
                        prm(2) = New SqlClient.SqlParameter("@ByUpdateDate", Date.Now)
                        prm(3) = New SqlClient.SqlParameter("@ByUser", _UserName)
                        prm(4) = New SqlClient.SqlParameter("@company_taxno", lblcompany_taxno.Text)

                        SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, sql, prm)

                        Return True
                    Case Else
                        Return False
                End Select
                
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

        Protected Sub btnAddCo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCo.Click
            With DataFormE_byTax(lblcompany_taxno.Text.Trim).Tables(0)
                If .Rows.Count > 0 Then 'มีอยู่แล้ว
                    If AddFormETax_com("1", rdoStatus.SelectedValue) = False Then 'update
                        lblError.Text = "ไม่สามารถบันทึกข้อมูลได้ : กรุณาติดต่อเจ้าหน้าที่"
                    Else
                        lblError.Text = "บันทึกข้อมูลเรียบร้อยแล้ว"
                    End If

                Else 'ยังไม่มี
                    If AddFormETax_com("0", rdoStatus.SelectedValue) = False Then 'add new
                        lblError.Text = "ไม่สามารถบันทึกข้อมูลได้ : กรุณาติดต่อเจ้าหน้าที่"
                    Else
                        lblError.Text = "บันทึกข้อมูลเรียบร้อยแล้ว"
                    End If

                End If
            End With
        End Sub

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
    End Class
End Namespace
