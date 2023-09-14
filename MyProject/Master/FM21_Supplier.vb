'Title        : Master Data Supplier
'Form         : FM21_Supplier
'Created By   : YANTI
'Created Date : Oktober 2008
'Table Used   : tbm_supplier

Imports vbs = Microsoft.VisualBasic.Strings
Imports xlns = Microsoft.Office.Interop.Excel
Imports System.Management
Imports System.Text.RegularExpressions
Imports poim.FM21_Supplier
Public Class FM21_Supplier
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        btnSave.Enabled = False
        btnSaveToExcell.Enabled = False
    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()
        Dim brs As Integer

        'DataGridView1.DataSource = Show_Grid(DataGridView1, "(select SUPPLIER_CODE as SupplierCode, SUPPLIER_NAME as SupplierName, FORWARD as Forward, ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, BANK_NAME as BankName, SWIFT as Swift, ACCOUNT_NO as AccountNo, FOR_CREDIT_BANK as ForCreditBank, SWIFT_CREDIT as SwiftCredit, ACCOUNT_CREDIT as AccountCredit, FAVOURING_SUPPLIER as FavouringSupplier, FAVOURING_ACCOUNT as FavouringAccount from tbm_supplier) as a")
        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select ts.SUPPLIER_CODE as SupplierCode, ts.SUPPLIER_NAME as SupplierName, ts.FORWARD as Forward, ts.ADDRESS as Address, ts.CITY_CODE as CityCode, tc.city_name as CityName, tt.country_name as CountryName, ts.PHONE as Phone, ts.FAX as Fax, ts.BANK_NAME as BankName, ts.SWIFT as Swift, ts.ACCOUNT_NO as AccountNo, ts.FOR_CREDIT_BANK as ForCreditBank, ts.SWIFT_CREDIT as SwiftCredit, ts.ACCOUNT_CREDIT as AccountCredit, ts.FAVOURING_SUPPLIER as FavouringSupplier, ts.FAVOURING_ACCOUNT as FavouringAccount, note, if (active='1','Yes','No') AS Active from tbm_supplier as ts, tbm_city as tc, tbm_country as tt where ts.city_code = tc.city_code and tc.country_code=tt.country_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        If brs > 0 Then btnSaveToExcell.Enabled = True
        chk_active.Enabled = True
        TextBox1.Enabled = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox4.Clear()
        TextBox3.Clear()
        TextBox6.Clear()
        TextBox5.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        TextBox12.Clear()
        TextBox13.Clear()
        TextBox14.Clear()
        TextBox15.Clear()
        note.Clear()
        lblCityName.Text = ""
        lblCountryName.Text = ""

        TextBox1.Text = GetData("SELECT CAST(LPAD(MAX(supplier_code)+1,5,'0') AS CHAR) cod FROM tbm_supplier")

        TextBox1.Focus()
        baru = True
        edit = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Close()
        'Dispose()
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_supplier " & _
                 "where supplier_code='" & TextBox1.Text & "'"

        ErrMsg = "Failed when deleting user data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete user data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_supplier")
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Foreign Key
        SQLstr = "Select * from tbm_city where city_code='" & TextBox5.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = True Then
            MsgBox("City code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox5.Focus()
            Exit Function
        End If
        If Len(Trim(TextBox1.Text)) < 5 Then
            MsgBox("Supplier Code must be 5 digits! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox1.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String
        Dim v_chk_active As String

        If chk_active.Checked = True Then
            v_chk_active = "1"
        Else
            v_chk_active = "0"
        End If

        If baru Then
            If CekData() = False Then Exit Sub
            teks = "Save Data"
            ErrMsg = "Failed when saving user data"
            SQLstr = "INSERT INTO tbm_supplier (supplier_code,supplier_name,forward,address,city_code,phone,fax,bank_name," & _
                                               "swift,account_no,for_credit_bank,swift_credit,account_credit," & _
                                               "favouring_supplier,favouring_account,note,active)" & _
                     "VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & _
                                   TextBox4.Text & "', '" & TextBox5.Text & "', '" & TextBox6.Text & "', '" & _
                                   TextBox7.Text & "', '" & TextBox8.Text & "', '" & TextBox9.Text & "', '" & _
                                   TextBox10.Text & "', '" & TextBox11.Text & "', '" & TextBox12.Text & "', '" & _
                                   TextBox13.Text & "', '" & TextBox14.Text & "', '" & TextBox15.Text & "', '" & note.Text & "', '" & v_chk_active & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating user data"
            SQLstr = "UPDATE tbm_supplier " & _
                     "SET supplier_name = '" & TextBox2.Text & "'," & _
                     "forward = '" & TextBox3.Text & "'," & _
                     "address = '" & TextBox4.Text & "'," & _
                     "city_code = '" & TextBox5.Text & "'," & _
                     "phone = '" & TextBox6.Text & "'," & _
                     "fax = '" & TextBox7.Text & "', " & _
                     "bank_name = '" & TextBox8.Text & "', " & _
                     "swift = '" & TextBox9.Text & "', " & _
                     "account_no = '" & TextBox10.Text & "', " & _
                     "for_credit_bank = '" & TextBox11.Text & "', " & _
                     "swift_credit = '" & TextBox12.Text & "', " & _
                     "account_credit = '" & TextBox13.Text & "', " & _
                     "favouring_supplier = '" & TextBox14.Text & "', " & _
                     "favouring_account = '" & TextBox15.Text & "', " & _
                     "note = '" & note.Text & "', " & _
                     "active = '" & v_chk_active & "' " & _
                     "where supplier_code='" & TextBox1.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_supplier")
        End If
    End Sub
    Private Sub RefreshTombolSave()
        'btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(TextBox2.Text)) > 0) And _
        '                  (Len(Trim(TextBox3.Text)) > 0) And (Len(Trim(TextBox4.Text)) > 0) And _
        '                  (Len(Trim(TextBox5.Text)) > 0) And (Len(Trim(TextBox6.Text)) > 0) And _
        '                  (Len(Trim(TextBox7.Text)) > 0) And (Len(Trim(TextBox8.Text)) > 0) And _
        '                  (Len(Trim(TextBox9.Text)) > 0) And (Len(Trim(TextBox10.Text)) > 0) And _
        '                  (Len(Trim(TextBox11.Text)) > 0) And (Len(Trim(TextBox12.Text)) > 0) And _
        '                  (Len(Trim(TextBox13.Text)) > 0) And (Len(Trim(TextBox14.Text)) > 0) And _
        '                  (Len(Trim(TextBox15.Text)) > 0)

        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(TextBox5.Text)) > 0) And lblCityName.Text <> ""
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        TextBox1.Text = DataGridView1.Item(0, brs).Value.ToString
        TextBox2.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox3.Text = DataGridView1.Item(2, brs).Value.ToString
        TextBox4.Text = DataGridView1.Item(3, brs).Value.ToString
        TextBox5.Text = DataGridView1.Item(4, brs).Value.ToString
        TextBox6.Text = DataGridView1.Item(7, brs).Value.ToString
        TextBox7.Text = DataGridView1.Item(8, brs).Value.ToString
        TextBox8.Text = DataGridView1.Item(9, brs).Value.ToString
        TextBox9.Text = DataGridView1.Item(10, brs).Value.ToString
        TextBox10.Text = DataGridView1.Item(11, brs).Value.ToString
        TextBox11.Text = DataGridView1.Item(12, brs).Value.ToString
        TextBox12.Text = DataGridView1.Item(13, brs).Value.ToString
        TextBox13.Text = DataGridView1.Item(14, brs).Value.ToString
        TextBox14.Text = DataGridView1.Item(15, brs).Value.ToString
        TextBox15.Text = DataGridView1.Item(16, brs).Value.ToString
        note.Text = DataGridView1.Item(17, brs).Value.ToString
        If DataGridView1.Item(18, brs).Value.ToString = "1" Or _
        DataGridView1.Item(18, brs).Value.ToString = "Yes" Then
            chk_active.Checked = True
        Else
            chk_active.Checked = False
        End If
        TextBox1.Enabled = False
        btnDelete.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        lblCityName.Text = AmbilData("CITY_NAME", "tbm_city", "CITY_CODE='" & TextBox5.Text & "'")
        lblCountryName.Text = AmbilData("COUNTRY_NAME", "tbm_city, tbm_country", "tbm_city.country_code=tbm_country.country_code AND CITY_CODE='" & TextBox5.Text & "'")

        TextBox2.Focus()
    End Sub
    Private Sub f_getdata()
        Dim teks2 As String = ""
        Dim teks3 As String = ""
        Dim teks4 As String = ""
        Dim teks5 As String = ""
        Dim teks6 As String = ""
        Dim teks7 As String = ""
        Dim teks8 As String = ""
        Dim teks9 As String = ""
        Dim teks10 As String = ""
        Dim teks11 As String = ""
        Dim teks12 As String = ""
        Dim teks13 As String = ""
        Dim teks14 As String = ""
        Dim teks15 As String = ""

        SQLstr = "select * from tbm_supplier where supplier_code = '" & TextBox1.Text & "' "
        ErrMsg = "Failed when read Users Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    teks2 = MyReader.GetString("supplier_name")
                    teks3 = MyReader.GetString("forward")
                    teks4 = MyReader.GetString("address")
                    teks5 = MyReader.GetString("city_code")
                    teks6 = MyReader.GetString("phone")
                    teks7 = MyReader.GetString("fax")
                    teks8 = MyReader.GetString("bank_name")
                    teks9 = MyReader.GetString("swift")
                    teks10 = MyReader.GetString("account_no")
                    teks11 = MyReader.GetString("for_credit_bank")
                    teks12 = MyReader.GetString("swift_credit")
                    teks13 = MyReader.GetString("account_credit")
                    teks14 = MyReader.GetString("favouring_supplier")
                    teks15 = MyReader.GetString("favouring_account")
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                TextBox1.Enabled = True
                btnDelete.Enabled = False
            Else
                baru = False
                edit = True
                TextBox1.Enabled = False
                btnDelete.Enabled = True
                TextBox2.Text = teks2
                TextBox3.Text = teks3
                TextBox4.Text = teks4
                TextBox5.Text = teks5
                TextBox6.Text = teks6
                TextBox7.Text = teks7
                TextBox8.Text = teks8
                TextBox9.Text = teks9
                TextBox10.Text = teks10
                TextBox11.Text = teks11
                TextBox12.Text = teks12
                TextBox13.Text = teks13
                TextBox14.Text = teks14
                TextBox15.Text = teks15
            End If
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        RefreshTombolSave()
        'Call f_getdata()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        RefreshTombolSave()
        lblCityName.Text = AmbilData("CITY_NAME", "tbm_city", "CITY_CODE='" & TextBox5.Text & "'")
        lblCountryName.Text = AmbilData("COUNTRY_NAME", "tbm_city, tbm_country", "tbm_city.country_code=tbm_country.country_code AND CITY_CODE='" & TextBox5.Text & "'")
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        RefreshTombolSave()
    End Sub

    
    Private Sub TextBox9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox14.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox15.TextChanged
        RefreshTombolSave()
    End Sub
    Private Sub btnSearch_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city"
        PilihanDlg.SQLFilter = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' " & _
                               " and city_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox5.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCityName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Function GetData(ByVal strSQL As String) As String
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = strSQL
        MyComm.CommandType = CommandType.Text
        Try
            GetData = MyComm.ExecuteScalar()
        Catch ex As Exception
            GetData = Nothing
        End Try
        MyComm.Dispose()
    End Function

    Private Sub btnSaveToExcell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveToExcell.Click
        Dim app As New xlns.Application
        Dim wb As xlns.Workbook = app.Workbooks.Add(xlns.XlWBATemplate.xlWBATWorksheet)
        Dim xlsheet As New xlns.Worksheet
        Dim inApp As xlns.Application
        Dim xlwindow As xlns.Workbook
        xlsheet = CType(wb.ActiveSheet, xlns.Worksheet)

        Dim file_name As String
        Dim StrColumn, StrData As String
        Dim i, j, k As Integer

        Try
            app.Visible = False
            ErrMsg = "Gagal baca data detail."
            'Write judul dulu
            xlsheet.Cells(1, 1) = Me.Text

            DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
            For j = 1 To DataGridView1.ColumnCount
                StrColumn = DataGridView1.Columns(j - 1).HeaderText
                xlsheet.Cells(2, j) = StrColumn
            Next

            For i = 0 To DataGridView1.RowCount - 1
                For j = 1 To DataGridView1.ColumnCount

                    StrColumn = DataGridView1.Columns(j - 1).HeaderText
                    StrData = DataGridView1.Rows(i).Cells(StrColumn).Value.ToString
                    If Mid(StrData, 1, 1) = "0" Then StrData = "'" & StrData

                    xlsheet.Cells(i + 3, j) = StrData
                Next
            Next
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1, 1)).Cells.Font.Size = 12
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1, 1)).Cells.Font.Bold = True

            xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, j)).Cells.Font.Size = 9
            xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, j)).Cells.Font.Bold = True

            xlsheet.Range(xlsheet.Cells(3, 1), xlsheet.Cells(i + 3, j)).Cells.Font.Size = 9
            xlsheet.Range(xlsheet.Cells(3, 1), xlsheet.Cells(i + 3, j)).Cells.Font.Bold = False
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(i + 3, j)).EntireColumn.AutoFit()
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(i + 3, j)).EntireColumn.WrapText = False

            'Finally save the file
            ''file_name = "c:/" & UserData.UserId & "-" & vbs.Format(Now(), "ddMMyyyy-mmss") & ".xls"
            ''xlsheet.SaveAs(file_name)
            xlsheet = Nothing
            app.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub

End Class
