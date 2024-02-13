Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.Data
Imports System.Data.SqlClient

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary

Namespace NTi.Modules.DFT_EDI_Quota
    Partial Class ViewDFT_EDI_Quota
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader
        Dim objConn As SqlConnection

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Session("ssUserName") = UserInfo.Username
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = "01/01/" & Now.Year
                rdpToDate.SelectedDate = Today

                repTranStock.DataSource = LoadTranStock()
                repTranStock.DataBind()
            End If
        End Sub

        Function LoadTranStock() As DataTable
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT * FROM TransStock WHERE CONVERT(varchar(8),TSK_Date,112) BETWEEN " & FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value) & " AND " & FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value) & " ORDER BY CONVERT(varchar(8), TSK_Date, 112)"
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dt As New DataTable("xTable")
                dt.Columns.Add("TSK_Date", Type.GetType("System.String"))
                dt.Columns.Add("TSK_Debit", Type.GetType("System.Decimal"))
                dt.Columns.Add("CompanyName_En", Type.GetType("System.String"))
                dt.Columns.Add("TSK_Credit", Type.GetType("System.Decimal"))
                dt.Columns.Add("reference_code2", Type.GetType("System.String"))
                dt.Columns.Add("ReferenceDesc1", Type.GetType("System.String"))
                dt.Columns.Add("ReferenceDesc2", Type.GetType("System.String"))
                dt.Columns.Add("Amount", Type.GetType("System.Decimal"))

                Dim TSK_Date As String
                Dim TSK_Debit As Decimal
                Dim CompanyName_En As String
                Dim TSK_Credit As Decimal
                Dim reference_code2 As String
                Dim ReferenceDesc1 As String
                Dim ReferenceDesc2 As String
                Dim Amount As Decimal
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    With ds.Tables(0).Rows(i)
                        If i = 0 Then
                            lblTotalQuota.Text = CommonUtility.Get_StringValue(.Item("TSK_Debit"))
                            TSK_Date = CommonUtility.Get_StringValue(Convert.ToDateTime(.Item("TSK_Date")).ToString("dd/MM/yyyy"))
                            TSK_Debit = CommonUtility.Get_Decimal(.Item("TSK_Debit"))
                            CompanyName_En = CommonUtility.Get_StringValue(.Item("CompanyName_En"))
                            TSK_Credit = CommonUtility.Get_Decimal(.Item("TSK_Credit"))
                            reference_code2 = CommonUtility.Get_StringValue(.Item("reference_code2"))
                            ReferenceDesc1 = CommonUtility.Get_StringValue(.Item("ReferenceDesc1"))
                            ReferenceDesc2 = CommonUtility.Get_StringValue(.Item("ReferenceDesc2"))
                            If CommonUtility.Get_StringValue(.Item("TSK_Type1")) = "C" Then
                                Amount = CommonUtility.Get_Decimal(TSK_Debit - TSK_Credit)
                            Else
                                Amount = CommonUtility.Get_Decimal(TSK_Debit + TSK_Credit)
                            End If

                            dt.Rows.Add(New Object() {TSK_Date, TSK_Debit, CompanyName_En, TSK_Credit, reference_code2, ReferenceDesc1, ReferenceDesc2, Amount})
                        Else
                            TSK_Date = CommonUtility.Get_StringValue(Convert.ToDateTime(.Item("TSK_Date")).ToString("dd/MM/yyyy"))
                            TSK_Debit = CommonUtility.Get_Decimal(Amount)
                            CompanyName_En = CommonUtility.Get_StringValue(.Item("CompanyName_En"))
                            TSK_Credit = CommonUtility.Get_Decimal(.Item("TSK_Credit"))
                            reference_code2 = CommonUtility.Get_StringValue(.Item("reference_code2"))
                            ReferenceDesc1 = CommonUtility.Get_StringValue(.Item("ReferenceDesc1"))
                            ReferenceDesc2 = CommonUtility.Get_StringValue(.Item("ReferenceDesc2"))
                            If CommonUtility.Get_StringValue(.Item("TSK_Type1")) = "C" Then
                                Amount = CommonUtility.Get_Decimal(TSK_Debit - TSK_Credit)
                            Else
                                Amount = CommonUtility.Get_Decimal(TSK_Debit + TSK_Credit)
                            End If

                            dt.Rows.Add(New Object() {TSK_Date, TSK_Debit, CompanyName_En, TSK_Credit, reference_code2, ReferenceDesc1, ReferenceDesc2, Amount})
                        End If
                    End With
                Next

                Return dt
            End If
        End Function

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            repTranStock.DataSource = LoadTranStock()
            repTranStock.DataBind()
        End Sub

        Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            repTranStock.DataSource = LoadTranStock()
            repTranStock.DataBind()
        End Sub

        Protected Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click
            Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_Quota/frmQuotaReport.aspx" & _
            "?FROM_DATE=" & FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value) & _
            "&TO_DATE=" & FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value) & _
            "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

        End Sub
    End Class

End Namespace
