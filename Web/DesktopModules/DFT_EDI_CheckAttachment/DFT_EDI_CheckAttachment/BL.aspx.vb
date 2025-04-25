Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class BL
    Inherits System.Web.UI.Page

    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadData()
        End If
    End Sub

    Private Sub LoadData()
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getBL_NewDS", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(Request.QueryString("RefNo"))))

            dlBL.DataSource = ds.Tables(0)
            dlBL.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Function GetBLProducts(ByVal invh_run_auto As String, ByVal bl_no As String, ByVal Q1 As Decimal, ByVal Q2 As Decimal, ByVal Q3 As Decimal, ByVal Q4 As Decimal, ByVal Q5 As Decimal)
        Dim str_product As String
        Dim str_header As String = ""
        Dim str_quantity_col As String = "<tr>"
        Dim num_quantity As Integer = 0

        If Q1 > 0 Then
            str_quantity_col &= "<td class=""header"" style=""text-align:center"">#1</td>"
            num_quantity = num_quantity + 1
        End If

        If Q2 > 0 Then
            str_quantity_col &= "<td class=""header"" style=""text-align:center"">#2</td>"
            num_quantity = num_quantity + 1
        End If

        If Q3 > 0 Then
            str_quantity_col &= "<td class=""header"" style=""text-align:center"">#3</td>"
            num_quantity = num_quantity + 1
        End If

        If Q4 > 0 Then
            str_quantity_col &= "<td class=""header"" style=""text-align:center"">#4</td>"
            num_quantity = num_quantity + 1
        End If

        If Q5 > 0 Then
            str_quantity_col &= "<td class=""header"" style=""text-align:center"">#5</td>"
            num_quantity = num_quantity + 1
        End If

        str_quantity_col &= "</tr>"

        str_header = "<table class=""tb-data2"" cellpadding=""0"" cellspacing=""0"" style=""margin-top:15px;margin-bottom:15px;"">" & _
        "<tr><td rowspan=""2"" class=""header"">Item No.</td><td rowspan=""2"" class=""header"" style=""width:100px;"">Marks and numbers of packages</td><td rowspan=""2""class=""header"">Number and kind of packages; description of goods</td><td rowspan=""2"" class=""header"">Net weight</td><td rowspan=""2"" class=""header"">Gross weight</td><td colspan=""" & num_quantity & """ class=""header"" style=""text-align:center"">Quantity</td></tr>"
        str_header &= str_quantity_col

        str_product = "<tr><td colspan=""10"">ไม่มีรายการข้อมูลสินค้า</td><td>&nbsp;</td></tr>"

        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getBL_Product_NewDS", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(invh_run_auto)), _
            New SqlParameter("@BL_NO", CommonUtility.Get_StringValue(bl_no)))

            If ds.Tables.Count > 0 Then
                Dim count As Integer = 1
                Dim row As DataRow

                Dim totalQuantity1 As Decimal = 0
                Dim totalQuantity2 As Decimal = 0
                Dim totalQuantity3 As Decimal = 0
                Dim totalQuantity4 As Decimal = 0
                Dim totalQuantity5 As Decimal = 0

                Dim Quantity1Unit As String = ""
                Dim Quantity2Unit As String = ""
                Dim Quantity3Unit As String = ""
                Dim Quantity4Unit As String = ""
                Dim Quantity5Unit As String = ""

                Dim g_unit As String = ""
                Dim n_unit As String = ""

                Dim totalNetWeight As Decimal = 0
                Dim totalGrossWeight As Decimal = 0
                Dim totalFob As Decimal = 0

                Dim str_product_name As String = ""

                str_product = ""
                For Each row In ds.Tables(0).Rows

                    str_product &= "<tr><td align=""center"">" & count & "</td><td>" & row("mark").ToString().Replace(vbCrLf, "<br/>") & "</td><td>"

                    If row("product_name").ToString() <> str_product_name Then
                        str_product_name = row("product_name").ToString()
                        str_product &= "<div class=""product-name"">" & row("product_name").ToString() & "</div>"
                    End If

                    str_product &= Replace(row("product_description").ToString(), row("product_name").ToString(), "") & "</td><td>" & FormatNumber(row("net_weight"), 3) & " </td><td>" & FormatNumber(row("gross_weight"), 3) & " </td>"

                    If Q1 > 0 Then
                        str_product &= "<td>" & FormatNumber(row("quantity1"), 0) & "</td>"
                    End If

                    If Q2 > 0 Then
                        str_product &= "<td>" & FormatNumber(row("quantity2"), 0) & "</td>"
                    End If

                    If Q3 > 0 Then
                        str_product &= "<td>" & FormatNumber(row("quantity3"), 0) & "</td>"
                    End If

                    If Q4 > 0 Then
                        str_product &= "<td>" & FormatNumber(row("quantity4"), 0) & "</td>"
                    End If

                    If Q5 > 0 Then
                        str_product &= "<td>" & FormatNumber(row("quantity5"), 0) & "</td>"
                    End If

                    str_product &= "</tr>"

                    count = count + 1

                    totalQuantity1 = totalQuantity1 + row("quantity1")
                    totalQuantity2 = totalQuantity2 + row("quantity2")
                    totalQuantity3 = totalQuantity3 + row("quantity3")
                    totalQuantity4 = totalQuantity4 + row("quantity4")
                    totalQuantity5 = totalQuantity5 + row("quantity5")

                    Quantity1Unit = row("quantity1_unit").ToString()
                    Quantity2Unit = row("quantity2_unit").ToString()
                    Quantity3Unit = row("quantity3_unit").ToString()
                    Quantity4Unit = row("quantity4_unit").ToString()
                    Quantity5Unit = row("quantity5_unit").ToString()

                    totalGrossWeight = totalGrossWeight + row("gross_weight")
                    g_unit = row("gross_weight_unit").ToString()
                    totalNetWeight = totalNetWeight + row("net_weight")
                    n_unit = row("net_weight_unit").ToString()
                Next

                str_product &= "<tr><td align=""center"" style=""border:0px;border-top:1px solid #cccccc;"">&nbsp;</td><td style=""border:0px;border-top:1px solid #cccccc;"">&nbsp;</td><td style=""background:#efefef;text-align:center;""><b>Total</b></td><td style=""background:#efefef;""><b>" & FormatNumber(totalNetWeight, 3) & "</b> " & n_unit & "</td><td style=""background:#efefef;""><b>" & FormatNumber(totalGrossWeight, 3) & "</b> " & g_unit & "</td>"

                If Q1 > 0 Then
                    str_product &= "<td style=""background:#efefef;""><b>" & FormatNumber(totalQuantity1, 0) & "</b> " & Quantity1Unit & "</td>"
                End If

                If Q2 > 0 Then
                    str_product &= "<td style=""background:#efefef;""><b>" & FormatNumber(totalQuantity2, 0) & "</b> " & Quantity2Unit & "</td>"
                End If

                If Q3 > 0 Then
                    str_product &= "<td style=""background:#efefef;""><b>" & FormatNumber(totalQuantity3, 0) & "</b> " & Quantity3Unit & "</td>"
                End If

                If Q4 > 0 Then
                    str_product &= "<td style=""background:#efefef;""><b>" & FormatNumber(totalQuantity4, 0) & "</b> " & Quantity4Unit & "</td>"
                End If

                If Q5 > 0 Then
                    str_product &= "<td style=""background:#efefef;""><b>" & FormatNumber(totalQuantity5, 0) & "</b> " & Quantity5Unit & "</td>"
                End If

                str_product &= "</tr>"

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return str_header & str_product & "</table>"
    End Function

    Protected Sub dlBL_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlBL.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
            e.Item.ItemType = ListItemType.AlternatingItem Then

            ''Dim lblPackage As Label = CType(e.Item.FindControl("lblTotalPackage"), Label)
            ''lblPackage.Text = FormatNumber(CDec(hTotalPackage.Value), 0)

            ''Dim lblPackageUnit As Label = CType(e.Item.FindControl("lblTotalPackageUnit"), Label)
            ''lblPackageUnit.Text = hTotalPackageUnit.Value

            Dim lblBLNoText As Label = CType(e.Item.FindControl("lblBLTypeText"), Label)
            lblBLType.Text = lblBLNoText.Text.Replace(" No.:", "")

        End If
    End Sub
End Class