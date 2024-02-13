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
        ' ���ҧ Column ���Ѻ DataTable
        TableA.Columns.Add("���Ϳ����", System.Type.GetType("System.String"))
        TableA.Columns.Add("���ͻ����", System.Type.GetType("System.String"))
        TableA.Columns.Add("�Ţ����ͧ", System.Type.GetType("System.String"))
        TableA.Columns.Add("�Ţ˹ѧ����Ѻ�ͧ", System.Type.GetType("System.String"))
        TableA.Columns.Add("�Ţ����", System.Type.GetType("System.String"))
        TableA.Columns.Add("���ͺ���ѷ", System.Type.GetType("System.String"))
        TableA.Columns.Add("�ԡѴ�Թ���", System.Type.GetType("System.String"))
        TableA.Columns.Add("���˹ѡ�ط��", System.Type.GetType("System.String"))
        TableA.Columns.Add("˹���", System.Type.GetType("System.String"))
        TableA.Columns.Add("��Ť�� FOB", System.Type.GetType("System.String"))
        TableA.Columns.Add("Date Approve", System.Type.GetType("System.String"))
        Dim dr As System.Data.DataRow
        For ia As Integer = 0 To ds.Tables(0).Rows.Count - 1
            dr = TableA.NewRow()
            dr("���Ϳ����") = ds.Tables(0).Rows(ia).Item("���Ϳ����").ToString
            dr("���ͻ����") = ds.Tables(0).Rows(ia).Item("���ͻ����").ToString
            dr("�Ţ����ͧ") = ds.Tables(0).Rows(ia).Item("�Ţ����ͧ").ToString
            dr("�Ţ˹ѧ����Ѻ�ͧ") = ds.Tables(0).Rows(ia).Item("�Ţ˹ѧ����Ѻ�ͧ").ToString
            dr("�Ţ����") = "'" & ds.Tables(0).Rows(ia).Item("�Ţ����").ToString
            dr("���ͺ���ѷ") = ds.Tables(0).Rows(ia).Item("���ͺ���ѷ").ToString
            dr("�ԡѴ�Թ���") = "'" & ds.Tables(0).Rows(ia).Item("�ԡѴ�Թ���").ToString
            dr("���˹ѡ�ط��") = ds.Tables(0).Rows(ia).Item("���˹ѡ�ط��").ToString

            If ds.Tables(0).Rows(ia).Item("form_type").ToString.ToUpper = "FORM2_1" Then '੾�п�������˹��µ�ͧ��� CAT
                dr("˹���") = ds.Tables(0).Rows(ia).Item("CAT").ToString & " CAT"
            Else
                dr("˹���") = ds.Tables(0).Rows(ia).Item("˹���").ToString
            End If
            
            dr("��Ť�� FOB") = ds.Tables(0).Rows(ia).Item("��Ť�� FOB").ToString
            dr("Date Approve") = Format(CDate(ds.Tables(0).Rows(ia).Item("Date Approve").ToString), "dd/MM/yyy")

            TableA.Rows.Add(dr)

        Next
        ' ���� DataTable ���Ѻ DataSet
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
