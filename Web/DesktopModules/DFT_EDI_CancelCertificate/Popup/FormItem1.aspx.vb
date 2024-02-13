Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient

Partial Public Class FormItem1
    Inherits System.Web.UI.Page
    Dim g_sForm = "FormItem1"
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtTariffCode.Focus()
        If Not IsPostBack Then
            If Request.QueryString("action") = "view" Then
                txtInvHRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("InvHRunAuto"))
                txtInvDRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("InvDRunAuto"))
                Page.Title = "แสดงรายการสินค้า"
                lblHeader.Text = "แสดงรายการสินค้า"
                Call SetForm()
            End If
        End If
    End Sub

    Private Sub SetForm()
        Try
            Dim m_objReader As SqlDataReader
            m_objReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_form_edi_getDetailByDataKey", _
            New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text), _
            New SqlParameter("@INVD_RUN_AUTO", txtInvDRunAuto.Text))

            If m_objReader.Read() Then
                txtInvHRunAuto.Text = CommonUtility.Get_StringValue(m_objReader("InvH_Run_Auto"))
                txtInvDRunAuto.Text = CommonUtility.Get_StringValue(m_objReader("InvD_Run_Auto"))
                txtTariffCode.Text = CommonUtility.Get_StringValue(m_objReader("Tariff_Code"))
                txtProductName.Text = CommonUtility.Get_StringValue(m_objReader("Product_Name"))
                txtProductDescription.Text = CommonUtility.Get_StringValue(m_objReader("Product_Description"))
                txtNetWeight.Text = CommonUtility.Get_StringValue(m_objReader("Net_Weight"))
                dropUnitCode2.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Unit_Code2"))
                txtFOBAmt.Text = CommonUtility.Get_StringValue(m_objReader("FOB_AMT"))
                txtMarks.Text = CommonUtility.Get_StringValue(m_objReader("MARKS"))
                SetIndexOfRadio(CommonUtility.Get_StringValue(m_objReader("Material_Type")), radMaterialType)
                txtPercentImport1.Text = CommonUtility.Get_StringValue(m_objReader("Percent_Import1"))
                txtPercentImport2.Text = CommonUtility.Get_StringValue(m_objReader("Percent_Import2"))
                dropFOBDisplay.SelectedValue = CommonUtility.Get_StringValue(m_objReader("FOBDisplay"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub SetIndexOfRadio(ByVal v_sIndex As String, ByRef v_objRadioButtonControl As RadioButtonList)
        v_objRadioButtonControl.SelectedIndex = v_objRadioButtonControl.Items.IndexOf(v_objRadioButtonControl.Items.FindByValue(v_sIndex))
    End Sub

    Sub SetListBox(ByVal v_sData As String, ByRef v_objListBox As ListBox)
        Dim m_aTemp() As String = Split(v_sData, ";")
        v_objListBox.DataSource = m_aTemp
        v_objListBox.DataBind()
    End Sub

    Function GetListBox(ByVal v_objListBoxControl As ListBox)
        Dim m_sTemp As String = ""
        Dim m_objListItem As ListItem
        Dim m_iCount As Integer
        For m_iCount = 0 To v_objListBoxControl.Items.Count - 1
            If m_iCount = v_objListBoxControl.Items.Count - 1 Then
                m_sTemp = (m_sTemp & v_objListBoxControl.Items(m_iCount).Text)
            Else
                m_sTemp = (m_sTemp & v_objListBoxControl.Items(m_iCount).Text & ";")
            End If
        Next
        GetListBox = m_sTemp
    End Function

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)

    End Sub
End Class