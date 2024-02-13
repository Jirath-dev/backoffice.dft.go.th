Public Class DataSetToExcel
    
    Public Shared Sub Convert(ByVal ds As DataSet, ByVal response As HttpResponse)
        'first let's clean up the response.object
        response.Clear()
        'response.Charset = ""
        response.Charset = "TIS620" '"windows-874"
        'set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        'instantiate a datagrid
        Dim dg As New DataGrid
        'set the datagrid datasource to the dataset passed in

        Dim TableA As New DataTable
        ' สร้าง Column ให้กับ DataTable
        TableA.Columns.Add("ชื่อฟอร์ม", System.Type.GetType("System.String"))
        TableA.Columns.Add("ชื่อประเทศ", System.Type.GetType("System.String"))
        TableA.Columns.Add("เลขคำร้อง", System.Type.GetType("System.String"))
        TableA.Columns.Add("เลขหนังสือรับรอง", System.Type.GetType("System.String"))
        TableA.Columns.Add("เลขภาษี", System.Type.GetType("System.String"))
        TableA.Columns.Add("ชื่อบริษัท", System.Type.GetType("System.String"))
        TableA.Columns.Add("พิกัดสินค้า", System.Type.GetType("System.String"))
        TableA.Columns.Add("น้ำหนักสุทธิ", System.Type.GetType("System.String"))
        TableA.Columns.Add("หน่วย", System.Type.GetType("System.String"))
        TableA.Columns.Add("มูลค่า FOB", System.Type.GetType("System.String"))
        TableA.Columns.Add("Date Approve", System.Type.GetType("System.String"))
        Dim dr As System.Data.DataRow
        For ia As Integer = 0 To ds.Tables(0).Rows.Count - 1
            dr = TableA.NewRow()
            dr("ชื่อฟอร์ม") = ds.Tables(0).Rows(ia).Item("ชื่อฟอร์ม").ToString
            dr("ชื่อประเทศ") = ds.Tables(0).Rows(ia).Item("ชื่อประเทศ").ToString
            dr("เลขคำร้อง") = ds.Tables(0).Rows(ia).Item("เลขคำร้อง").ToString
            dr("เลขหนังสือรับรอง") = ds.Tables(0).Rows(ia).Item("เลขหนังสือรับรอง").ToString
            dr("เลขภาษี") = "'" & ds.Tables(0).Rows(ia).Item("เลขภาษี").ToString
            dr("ชื่อบริษัท") = ds.Tables(0).Rows(ia).Item("ชื่อบริษัท").ToString
            dr("พิกัดสินค้า") = "'" & ds.Tables(0).Rows(ia).Item("พิกัดสินค้า").ToString
            dr("น้ำหนักสุทธิ") = ds.Tables(0).Rows(ia).Item("น้ำหนักสุทธิ").ToString

            If ds.Tables(0).Rows(ia).Item("form_type").ToString.ToUpper = "FORM2_1" Then 'เฉพาะฟอร์มนี้หน่วยต้องตาม CAT
                dr("หน่วย") = ds.Tables(0).Rows(ia).Item("CAT").ToString & " CAT"
            Else
                dr("หน่วย") = ds.Tables(0).Rows(ia).Item("หน่วย").ToString
            End If
            
            dr("มูลค่า FOB") = ds.Tables(0).Rows(ia).Item("มูลค่า FOB").ToString
            dr("Date Approve") = Format(CDate(ds.Tables(0).Rows(ia).Item("Date Approve").ToString), "dd/MM/yyy")

            TableA.Rows.Add(dr)

        Next
        ' เพิ่ม DataTable ให้กับ DataSet
        Dim das As New System.Data.DataSet("myDataSet")
        das.Tables.Add(TableA)

        dg.DataSource = das.Tables(0)
        'bind the datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'all that's left is to output the html
        response.Write(stringWrite.ToString)
        response.End()



        ''first let's clean up the response.object
        'response.Clear()
        ''response.Charset = ""
        'response.Charset = "TIS620" '"windows-874"
        ''set the response mime type for excel
        'response.ContentType = "application/vnd.ms-excel"
        ''create a string writer
        'Dim stringWrite As New System.IO.StringWriter
        ''create an htmltextwriter which uses the stringwriter
        'Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        ''instantiate a datagrid
        'Dim dg As New DataGrid
        ''set the datagrid datasource to the dataset passed in

        'dg.DataSource = ds.Tables(0)
        ''bind the datagrid
        'dg.DataBind()
        ''tell the datagrid to render itself to our htmltextwriter
        'dg.RenderControl(htmlWrite)
        ''all that's left is to output the html
        'response.Write(stringWrite.ToString)
        'response.End()
    End Sub

End Class
