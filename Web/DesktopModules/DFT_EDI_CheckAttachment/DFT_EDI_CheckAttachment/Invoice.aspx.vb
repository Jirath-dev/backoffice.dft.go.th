Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class Invoice
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
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getInvoices_NewDS", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(Request.QueryString("RefNo"))))

            dlInvoice.DataSource = ds.Tables(0)
            dlInvoice.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Function GetInvoiceProducts(ByVal invh_run_auto As String, ByVal invoice_no As String, ByVal Q1 As Decimal, ByVal Q2 As Decimal, ByVal Q3 As Decimal, ByVal Q4 As Decimal, ByVal Q5 As Decimal)
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
"<tr><td rowspan=""2"" class=""header"">Item No.</td><td rowspan=""2"" class=""header"" style=""width:100px;"">Marks and numbers of packages</td><td rowspan=""2""class=""header"">Number and kind of packages; description of goods</td><td rowspan=""2"" class=""header"">Net weight</td><td rowspan=""2"" class=""header"">Gross weight</td><td rowspan=""2"" class=""header"">Amount FOB(USD)</td><td colspan=""" & num_quantity & """ class=""header"" style=""text-align:center"">Quantity</td></tr>"
        str_header &= str_quantity_col

        str_product = "<tr><td colspan=""10"">ไม่มีรายการข้อมูลสินค้า</td><td>&nbsp;</td></tr>"

        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getInvoices_Product_NewDS", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(invh_run_auto)), _
            New SqlParameter("@INVOICE_NO", CommonUtility.Get_StringValue(invoice_no)))

            If ds.Tables.Count > 0 Then
                Dim count As Integer = 1
                Dim row As DataRow

                Dim totalQuantity1 As Decimal = 0
                Dim totalQuantity2 As Decimal = 0
                Dim totalQuantity3 As Decimal = 0
                Dim totalQuantity4 As Decimal = 0
                Dim totalQuantity5 As Decimal = 0

                Dim totalNetWeight As Decimal = 0
                Dim totalGrossWeight As Decimal = 0
                Dim totalFob As Decimal = 0
                Dim totalPackages As Decimal = 0
                Dim totalCIF As Decimal = 0
                Dim totalCFR As Decimal = 0

                Dim Quantity1Unit As String = ""
                Dim Quantity2Unit As String = ""
                Dim Quantity3Unit As String = ""
                Dim Quantity4Unit As String = ""
                Dim Quantity5Unit As String = ""

                Dim g_unit As String = ""
                Dim n_unit As String = ""

                Dim str_product_name As String = ""
                Dim str_title_product As String = ""

                str_product = ""
                For Each row In ds.Tables(0).Rows

                    str_product &= "<tr><td align=""center"">" & count & "</td><td>" & row("mark").ToString().Replace(vbCrLf, "<br/>") & "</td><td>"

                    If count = 1 Then
                        Dim ds2 As New DataSet
                        ds2 = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getProductTitle_NewDS", _
                                                    New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(invh_run_auto)))

                        If ds2.Tables.Count > 0 Then
                            str_title_product = "<div class=""product-title"">" & ds2.Tables(0).Rows(0)(0) & "</div>"
                            str_product &= str_title_product
                        End If
                        ds2.Dispose()
                    End If

                    If row("product_name").ToString() <> str_product_name Then
                        str_product_name = row("product_name").ToString()
                        str_product &= "<div class=""product-name"">" & row("product_name").ToString() & "</div>"
                    End If

                    str_product &= Replace(row("product_description").ToString(), row("product_name").ToString(), "") & "<br/>" & row("letter") & "</td><td>" & FormatNumber(row("net_weight"), 3) & "</td><td>" & FormatNumber(row("gross_weight"), 3) & "</td><td>" & FormatNumber(row("fob_amt"), 2) & "</td>"

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
                    Quantity1Unit = row("quantity1_unit").ToString()

                    totalQuantity2 = totalQuantity2 + row("quantity2")
                    Quantity2Unit = row("quantity2_unit").ToString()

                    totalQuantity3 = totalQuantity3 + row("quantity3")
                    Quantity3Unit = row("quantity3_unit").ToString()

                    totalQuantity4 = totalQuantity4 + row("quantity4")
                    Quantity4Unit = row("quantity4_unit").ToString()

                    totalQuantity5 = totalQuantity5 + row("quantity5")
                    Quantity5Unit = row("quantity5_unit").ToString()

                    totalGrossWeight = totalGrossWeight + row("gross_weight")
                    g_unit = row("gross_weight_unit").ToString()
                    totalNetWeight = totalNetWeight + row("net_weight")
                    n_unit = row("net_weight_unit")
                    totalFob = totalFob + row("fob_amt")
                Next

                str_product &= "<tr><td style=""border:0px;border-top:1px solid #cccccc;"">&nbsp;</td><td style=""border:0px;border-top:1px solid #cccccc;"">&nbsp;</td><td style=""background:#efefef;text-align:center""><b>Total</b></td><td style=""background:#efefef;""><b>" & FormatNumber(totalNetWeight, 3) & "</b> " & n_unit & "</td><td style=""background:#efefef;""><b>" & FormatNumber(totalGrossWeight, 3) & "</b> " & g_unit & "</td><td style=""background:#efefef;""><b>" & FormatNumber(totalFob, 2) & "</b></td>"

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

                'lblNetWeight.Text = FormatNumber(CDec(lblNetWeight.Text) + totalNetWeight, 3)
                'lblGrossWeight.Text = FormatNumber(CDec(lblGrossWeight.Text) + totalGrossWeight, 3)
                'lblFOB.Text = FormatNumber(CDec(lblFOB.Text) + totalFob, 2)
                'lblTotalQuantity.Text = FormatNumber(CDec(lblTotalQuantity.Text) + totalQuantity, 0)

                'lblSumPackage.Text = FormatNumber(CDec(lblSumPackage.Text) + totalPackages, 0)
                'hSumPackagePerInvoice.Value = totalPackages

                'lblTotalCIF.Text = FormatNumber(CDec(lblCIF.Text) + totalCIF, 0)

                'lblSumPackageUnit.Text = PackateUnit
                'lblSumQuantityUnit.Text = QuantityUnit

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return str_header & str_product & "</table>"
    End Function

    Protected Sub dlInvoice_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlInvoice.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
            e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim lblNetWeight As Label = CType(e.Item.FindControl("lblNetWeight"), Label)
            lblSumNetWeight.Text = FormatNumber(CDec(lblSumNetWeight.Text) + CDec(lblNetWeight.Text), 3)

            Dim lblGrossWeight As Label = CType(e.Item.FindControl("lblGrossWeight"), Label)
            lblSumGrossWeight.Text = FormatNumber(CDec(lblSumGrossWeight.Text) + CDec(lblGrossWeight.Text), 3)

            'Dim lblPackageUnit As Label = CType(e.Item.FindControl("lblTotalPackageUnit"), Label)
            'lblPackageUnit.Text = lblSumPackageUnit.Text
            'Dim lblPackage As Label = CType(e.Item.FindControl("lblTotalPackage"), Label)
            'lblPackage.Text = FormatNumber(hSumPackagePerInvoice.Value, 0)   'CDec(lblSumPackage.Text) - CDec(lblPackage.Text)
            'lblSumPackage.Text = FormatNumber(CDec(lblSumPackage.Text) + CDec(lblPackage.Text), 0)

            Dim lblQuantity1 As Label = CType(e.Item.FindControl("lblTotalQuantity1"), Label)
            lblSumQuantity1.Text = FormatNumber(CDec(lblSumQuantity1.Text) + CDec(lblQuantity1.Text), 0)
            Dim lblQuantity1Unit As Label = CType(e.Item.FindControl("lblTotalQuantity1Unit"), Label)
            lblSumQuantity1Unit.Text = lblQuantity1Unit.Text

            Dim lblQuantity2 As Label = CType(e.Item.FindControl("lblTotalQuantity2"), Label)
            lblSumQuantity2.Text = FormatNumber(CDec(lblSumQuantity2.Text) + CDec(lblQuantity2.Text), 0)
            Dim lblQuantity2Unit As Label = CType(e.Item.FindControl("lblTotalQuantity2Unit"), Label)
            lblSumQuantity2Unit.Text = lblQuantity2Unit.Text

            Dim lblQuantity3 As Label = CType(e.Item.FindControl("lblTotalQuantity3"), Label)
            lblSumQuantity3.Text = FormatNumber(CDec(lblSumQuantity3.Text) + CDec(lblQuantity3.Text), 0)
            Dim lblQuantity3Unit As Label = CType(e.Item.FindControl("lblTotalQuantity3Unit"), Label)
            lblSumQuantity3Unit.Text = lblQuantity3Unit.Text

            Dim lblQuantity4 As Label = CType(e.Item.FindControl("lblTotalQuantity4"), Label)
            lblSumQuantity4.Text = FormatNumber(CDec(lblSumQuantity4.Text) + CDec(lblQuantity4.Text), 0)
            Dim lblQuantity4Unit As Label = CType(e.Item.FindControl("lblTotalQuantity4Unit"), Label)
            lblSumQuantity4Unit.Text = lblQuantity4Unit.Text

            Dim lblQuantity5 As Label = CType(e.Item.FindControl("lblTotalQuantity5"), Label)
            lblSumQuantity5.Text = FormatNumber(CDec(lblSumQuantity5.Text) + CDec(lblQuantity5.Text), 0)
            Dim lblQuantity5Unit As Label = CType(e.Item.FindControl("lblTotalQuantity5Unit"), Label)
            lblSumQuantity5Unit.Text = lblQuantity5Unit.Text

            Dim lblFob As Label = CType(e.Item.FindControl("lblTotalFOB"), Label)
            lblSumFOB.Text = FormatNumber(CDec(lblSumFOB.Text) + CDec(lblFob.Text), 2)

            Dim lblCif As Label = CType(e.Item.FindControl("lblTotalCIF"), Label)
            lblSumCIF.Text = FormatNumber(CDec(lblSumCIF.Text) + CDec(lblCif.Text), 2)

            Dim lblCFR As Label = CType(e.Item.FindControl("lblTotalCFR"), Label)
            lblSumCFR.Text = FormatNumber(CDec(lblSumCFR.Text) + CDec(lblCFR.Text), 2)

        End If
    End Sub
End Class