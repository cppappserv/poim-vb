'Title                         : Master Data Kurs
'Form                          : FM11_Kurs
'Table Used                    : tbm_kurs, tbm_currency
'Stored Procedure Used (MySQL) : RunSQL
'Created By                    : YANTI Sept 2008
'Modify by                     : Hanny 04.11.2008 add kurs pajak
'Modify By                     : Yanti 09.12.2008  Semua inputan angka format flexible mengikuti regional setting 

Public Class FM11_Kurs
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

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
        Dim qt0, qt2 As String

        qt0 = FormatNumber(0, 0, , , TriState.True)
        qt2 = FormatNumber(0, 2, , , TriState.True)

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select CURRENCY_CODE as CurrencyCode, EFFECTIVE_DATE as EffectiveDate, EFFECTIVE_KURS as Rate, KURS as EstimateRate, KURS_PAJAK as TaxRate from tbm_kurs Order by EFFECTIVE_DATE desc, CURRENCY_CODE desc) as a")
        brs = DataGridView1.RowCount

        DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        btnSave.Enabled = False
        btnDelete.Enabled = False

        tgl.Enabled = True
        TextBox1.Enabled = True
        btnSearch.Visible = (TextBox1.Enabled = True)

        tgl.Value = Now()
        TextBox1.Clear()
        txtRate.Text = qt2
        txtEstRate.Text = qt2
        txtTaxRate.Text = qt2
        TextBox1.Focus()
        baru = True
        edit = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tptgl As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        SQLstr = "DELETE from tbm_kurs " & _
                 "where currency_code='" & TextBox1.Text & "' and effective_date='" & tptgl & "'"

        ErrMsg = "Failed when deleting User data"

        Try
            MyComm.CommandText = "RunSQL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("SQLStr", SQLstr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)
            RefreshScreen()

            If hasil = True Then
                f_msgbox_successful("Delete Data")
            Else
                MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete User data")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks, tptgl As String
        Dim Errmsg As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        If baru Then
            teks = "Save Data"
            Errmsg = "Failed when saving user data"

            SQLstr = "INSERT INTO tbm_kurs (currency_code,effective_Date,kurs,effective_kurs,kurs_pajak) " & _
                     "VALUES ('" & TextBox1.Text & "','" & tptgl & "'," & GetNum(txtEstRate.Text) & "," & GetNum(txtRate.Text) & "," & GetNum(txtTaxRate.Text) & ")"
        Else
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbm_kurs " & _
                     "SET kurs=" & GetNum(txtEstRate.Text) & "," & _
                     "effective_kurs=" & GetNum(txtRate.Text) & "," & _
                     "kurs_pajak=" & GetNum(txtTaxRate.Text) & _
                     " where currency_code='" & TextBox1.Text & "'" & _
                     " and effective_date='" & tptgl & "'"
        End If

        Try
            MyComm.CommandText = "RunSQL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("SQLStr", SQLstr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)
            RefreshScreen()

            If hasil = True Then
                f_msgbox_successful(teks)
            Else
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0)
    End Sub

    Private Sub TxtEstRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEstRate.TextChanged
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0)
    End Sub

    Private Sub txtTaxRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTaxRate.TextChanged
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        tgl.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox1.Text = DataGridView1.Item(0, brs).Value.ToString
        tgl.Enabled = False
        TextBox1.Enabled = False
        btnSearch.Visible = (TextBox1.Enabled = True)

        txtRate.Text = FormatNumber(DataGridView1.Item(2, brs).Value.ToString, 2, , , TriState.True)
        txtEstRate.Text = FormatNumber(DataGridView1.Item(3, brs).Value.ToString, 2, , , TriState.True)
        txtTaxRate.Text = FormatNumber(DataGridView1.Item(4, brs).Value.ToString, 2, , , TriState.True)

        btnDelete.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        txtRate.Focus()
    End Sub
    Private Sub f_getdata()
        Dim tptgl As String

        If (Len(Trim(TextBox1.Text))) = 0 Then Exit Sub
        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        SQLstr = "select * from tbm_kurs where currency_code = '" & TextBox1.Text & "' " & _
                 "and effective_date='" & tptgl & "'"

        ErrMsg = "Failed when read Currency Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtRate.Text = FormatNumber(MyReader.GetString("effective_kurs"), 2, , , TriState.True)
                    txtEstRate.Text = FormatNumber(MyReader.GetString("kurs"), 2, , , TriState.True)
                    txtTaxRate.Text = FormatNumber(MyReader.GetString("kurs_pajak"), 2, , , TriState.True)
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                TextBox1.Enabled = True
                tgl.Enabled = True
                btnDelete.Enabled = False
            Else
                baru = False
                edit = True
                TextBox1.Enabled = False
                tgl.Enabled = False
                btnDelete.Enabled = True
            End If
            btnSearch.Visible = (TextBox1.Enabled = True)
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select Currency Code"
        PilihanDlg.LblKey1.Text = "Currency Code"
        PilihanDlg.LblKey2.Text = "Currency Name"
        PilihanDlg.SQLGrid = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency where currency_code <> 'IDR' "
        PilihanDlg.SQLFilter = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency " & _
                               "WHERE currency_code <> 'IDR' AND currency_code LIKE 'FilterData1%' " & _
                               " and currency_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Function GetNum(ByVal strnum As String) As Decimal
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum = CDec(temp)
    End Function
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientDecimalSeparator, ServerDecimal)
    End Function

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
