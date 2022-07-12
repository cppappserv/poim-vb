Imports System.IO
Imports System.Data

Public Class exp_csv

    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String


    Dim Ship, NONum, v_Rate As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE As String
    Dim MODULE_NAME As String
    Dim ModCode As String
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim v_doccode, V_DOCNAME, BLStatus, BLNo As String
    Dim CSNo, ShipOrdNo As Integer
    Dim FinalApp As Boolean
    Dim dts As DataTable
    Dim vnom_po, vnom_invoice, vuserid As String
    Dim v_length As Integer
    Dim v_string As Integer
    Dim v_type As String
    Dim v_num As String
    Dim v_pono, v_temp As String
    Dim v_shipmentno As String
    Dim v_printOn, selKO, line As String
    Dim rows, rows2, xCek As Integer
    Dim v_filter, v_dt, v_by, v_cp, v_ord, v_crea, v_matcd, v_matnm, v_bank, v_acc, v_name As String
    Dim arrTemp() As String
    Dim VEBELN, VINVOICE_NO, vSHIPMENT As String
    Dim v_doc As String


    'Public Sub Nomercsv(ByRef txtPO As String, ByRef xpoitem As String, ByRef tanggal_sob As Date)
    '    Dim value = New System.Drawing.Point(55, 200)
    '    Me.Location = value
    '    MyConn = GetMyConn(MyConn)
    '    If MyConn Is Nothing Then
    '        Me.Close()
    '        Exit Sub
    '    End If

    '    'SQLstr = "SELECT EBELN,INVOICE_NO,USER_ID,SHIPMENT FROM TEMPLATE_CSV"
    '    'ErrMsg = "Datagrid view Failed"
    '    'MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

    '    'If Not MyReader Is Nothing Then
    '    '    While MyReader.Read
    '    '        vEBELN = MyReader.GetString("EBELN")
    '    '        vINVOICE_NO = MyReader.GetString("INVOICE_NO")
    '    '        vuserid = MyReader.GetString("user_id")
    '    '        vSHIPMENT = MyReader.GetString("sHIPMENT")
    '    '    End While
    '    'End If
    '    'CloseMyReader(MyReader, UserData)

    '    SQLstr = "SELECT ebeln,shipment,DATE_FORMAT(sob,'%Y%m%d') AS Vsob, " & _
    '    "approve, prepared, kurs, vessel, quantity, unit_price, unit2, " & _
    '    "invoice_no,DATE_FORMAT(invoice_date,'%Y%m%d') AS Vinvoice_date, " & _
    '    "date_format(invoic_due_date,'%Y%m%d') AS voic_due_date, " & _
    '    "date_format(value_date,'%Y%m%d') AS vvalue_date,insurance,unit8,ppn,unit10, " & _
    '    "import_duty,unit9,clearence_cost,unit11 " & _
    '    "FROM template_poim " & _
    '    "where ebeln='" & txtPO1 & "' " & _
    '    "and DATE_FORMAT(sob,'%d/%m/%Y') ='" & tanggal_sob1 & "' " & _
    '    "AND SHIPMENT='" & xpoitem1 & "' "
    '    ErrMsg = "Datagrid view Failed"
    '    dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
    '    'dgvcost.DataSource = dts


    '    DataGridView1.DataSource = dts
    '    ' DGVHeader.Columns(0).Visible = False


    'End Sub

    Private Sub exp_csv_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim value = New System.Drawing.Point(55, 200)
        Me.Location = value
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If

        SQLstr = "SELECT ebeln,shipment,DATE_FORMAT(sob,'%Y%m%d') AS Vsob, " & _
        "approve, prepared, kurs, vessel, quantity, unit_price, unit2, " & _
        "invoice_no,DATE_FORMAT(invoice_date,'%Y%m%d') AS Vinvoice_date, " & _
        "date_format(invoic_due_date,'%Y%m%d') AS voic_due_date, " & _
        "date_format(value_date,'%Y%m%d') AS vvalue_date,insurance,unit8,ppn,unit10, " & _
        "import_duty,unit9,clearence_cost,unit11 " & _
        "FROM template_poim " & _
        "where ebeln='" & txtPO1 & "' " & _
        "and DATE_FORMAT(sob,'%d/%m/%Y') ='" & tanggal_sob1 & "' " & _
        "AND SHIPMENT='" & xpoitem1 & "' "
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        'dgvcost.DataSource = dts


        DataGridView1.DataSource = dts
        ' DGVHeader.Columns(0).Visible = False

    End Sub

    Private Sub export_csv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export_csv.Click
        'Build the CSV file data as a Comma separated string.
        Dim csv As String = String.Empty
        Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()


        'Add the Header row for CSV file.
        '     For Each column As DataGridViewColumn In DataGridView1.Columns
        '   csv += column.HeaderText & "|"
        'MsgBox(column.HeaderText)
        '  Next

        'Add new line.
        '  csv += vbCr & vbLf
        Dim empty As Boolean = True
        'Adding the Rows
        For Each row As DataGridViewRow In DataGridView1.Rows

            For Each cell As DataGridViewCell In row.Cells
                If Not IsNothing(cell.Value) Then
                    'Add the Data rows.
                    empty = False
                    csv += Trim(cell.Value.ToString()) & "|" '.Replace("|", ";") & ","
                Else
                    empty = True
                    Exit For
                End If

            Next

            If empty = False Then
                'Add new line.
                csv += vbCr & vbLf
            End If

        Next

        'Exporting to Excel
        saveFileDialog1.Filter = "csv files (*.csv)|*.csv|csv files (*.csv)|*.csv"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True
        saveFileDialog1.FileName = "POIM_PO_SHIPMENT"
        If saveFileDialog1.ShowDialog() = DialogResult.OK Then

            '    Dim folderPath As String = "C:\CSV\"
            File.WriteAllText(saveFileDialog1.FileName, csv)
        End If
        MsgBox("Convert Selesai")
        Close()
    End Sub
End Class
