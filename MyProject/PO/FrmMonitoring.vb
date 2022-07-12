'Title                         : Master Data Kurs
'Form                          : FrmMonitoring
'Table Used                    : All
'Stored Procedure Used (MySQL) : RunSQL

Public Class FrmMonitoring
    Dim baru, edit, proses As Boolean
    Dim ErrMsg, SQLstr, desimal As String
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan

    Dim mask As String
    Dim ret1, ret2, ret3, dec1, dec2, dec3 As Boolean
    Dim ClientDecimalSeparator, ClientGroupSeparator, ServerDecimal As String
    Dim RegionalSetting As System.Globalization.CultureInfo
    Dim pjg As Integer

    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        'btnSave.Enabled = False
        Curr_Name.Text = ""

        mask = "##,###,###.##"
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        RegionalSetting = Global.System.Globalization.CultureInfo.CurrentCulture
        ServerDecimal = "."
        textbox2.Culture = RegionalSetting
        TextBox3.Culture = RegionalSetting
        Textbox4.Culture = RegionalSetting
        Call RefreshVar()
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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select CURRENCY_CODE as CurrencyCode, EFFECTIVE_DATE as EffectiveDate, EFFECTIVE_KURS as Rate, KURS_PAJAK as TaxRate from tbm_kurs Order by EFFECTIVE_DATE desc, CURRENCY_CODE desc) as a")
        DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(3).DefaultCellStyle.Format = "N2"
        'DataGridView1.Columns(4).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        brs = DataGridView1.RowCount
        'btnSave.Enabled = False
        btnDelete.Enabled = False
        TextBox1.Enabled = True
        btnSearch.Visible = (TextBox1.Enabled = True)
        tgl.Enabled = True
        TextBox1.Clear()
        tgl.Value = Now()
        tgl.Focus()
        baru = True
        edit = False
        Call ClearText()
        Call ClearMask()
        Call RefreshVar()
        Call Refresh0()
    End Sub
    Private Sub RefreshVar()
        dec1 = False
        dec2 = False
        dec3 = False
        ret1 = True
        ret2 = True
        ret3 = True
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub Refresh0()
        textbox2.Text = "000"
        textbox2.Mask = "#.##"
        TextBox3.Text = "000"
        TextBox3.Mask = "#.##"
        Textbox4.Text = "000"
        Textbox4.Mask = "#.##"
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
                'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_kurs")
            Else
                MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete User data")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function CekData() As Boolean
        Dim ee As New System.EventArgs

        CekData = True
        If Curr_Name.Text = "" Then
            MsgBox("Currency code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox1.Focus()
            Exit Function
        End If
        If ret1 = False Then textbox2_LostFocus(textbox2, ee)
        If ret2 = False Then TextBox3_LostFocus(TextBox3, ee)
        If ret3 = False Then TextBox4_LostFocus(Textbox4, ee)
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks, tptgl As String
        Dim Errmsg As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        If CekData() = False Then Exit Sub
        If baru Then
            teks = "Save Data"
            Errmsg = "Failed when saving user data"

            SQLstr = "INSERT INTO tbm_kurs (currency_code,effective_Date,kurs,effective_kurs,kurs_pajak) " & _
                     "VALUES ('" & TextBox1.Text & "','" & tptgl & "'," & GetNum(textbox2.Text) & "," & GetNum(TextBox3.Text) & "," & GetNum(Textbox4.Text) & ")"
        Else
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbm_kurs " & _
                     "SET kurs=" & GetNum(textbox2.Text) & "," & _
                     "effective_kurs=" & GetNum(TextBox3.Text) & "," & _
                     "kurs_pajak=" & GetNum(Textbox4.Text) & _
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
    Private Function GetNum(ByVal strnum As String) As String
        Dim temp As String
        Dim msk As New Windows.Forms.MaskedTextBox

        msk.Text = strnum
        msk.Mask = GetMaskText2Koma(msk.Text)
        temp = msk.Text
        temp = Replace(temp, ClientGroupSeparator, "")
        temp = Replace(temp, ClientDecimalSeparator, ServerDecimal)
        GetNum = temp
    End Function
    'GAK DIPAKE LAGI, SETELAH DI FORM LAIN DIPERBAIKI
    'HAPUS AJA FUNCTION INI
    Public Function GetdbNum(ByVal strnum As String) As Decimal
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        temp = Replace(strnum, ClientGroupSeparator, "")
        GetdbNum = CDec(temp)
    End Function
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientDecimalSeparator, "")
        temp = Replace(temp, ClientGroupSeparator, "")
        GetNum2 = Trim(temp)
    End Function
    Private Function GetNum3(ByVal strnum As String) As String
        GetNum3 = Replace(strnum, ClientDecimalSeparator, "")
    End Function
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        Call RefreshVar()
        Call ClearText()
        Call ClearMask()
        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        TextBox1.Text = DataGridView1.Item(0, brs).Value.ToString
        tgl.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox1.Enabled = False
        btnSearch.Visible = (TextBox1.Enabled = True)
        tgl.Enabled = False
        btnDelete.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        'textbox2.Text = GetNum3(DataGridView1.Item(2, brs).Value.ToString)
        TextBox3.Text = GetNum3(DataGridView1.Item(2, brs).Value.ToString)
        Textbox4.Text = GetNum3(DataGridView1.Item(3, brs).Value.ToString)
        Textbox4_Enter(sender, e)
        TextBox3_Enter(sender, e)
        'textbox2.Focus()
        TextBox3.Focus()
    End Sub

    Private Sub tgl_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tgl.ValueChanged
        'btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(TextBox2.Text)) > 0) And (Len(Trim(TextBox3.Text)) > 0)
        Call f_getdata()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Curr_Name.Text = AmbilData("CURRENCY_NAME", "tbm_currency", "CURRENCY_CODE='" & TextBox1.Text & "'")
        'Call f_getdata()
    End Sub

    Private Sub textbox2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles textbox2.Enter
        pjg = Len(textbox2.Text)
        If pjg > 0 Then textbox2.Mask = GetMaskText2Koma(textbox2.Text)
        If pjg = 0 Then textbox2.Mask = mask
    End Sub
    Private Sub textbox2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles textbox2.LostFocus
        If textbox2.Text = "" Then Exit Sub
        textbox2.TextMaskFormat = MaskFormat.IncludeLiterals
        pjg = 2 - Len(Microsoft.VisualBasic.Mid(textbox2.Text, 12, 2))
        textbox2.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        textbox2.Text = textbox2.Text & Microsoft.VisualBasic.Left("00", pjg)
        textbox2.Mask = GetMaskText2Koma(textbox2.Text)
        ret1 = True
        dec1 = False
    End Sub
    Private Sub textbox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles textbox2.KeyDown
        pjg = Len(textbox2.Text)
        Select Case e.KeyCode
            Case Keys.Return
                If pjg >= 1 Then textbox2_LostFocus(sender, e)
            Case Keys.D0 To Keys.D9, _
                 Keys.NumPad0 To Keys.NumPad9, _
                 Keys.Oem1 To Keys.Oem8

                If pjg > 0 And ret1 = True Then
                    textbox2.Text = ""
                    textbox2.Mask = mask
                End If
                ret1 = False
            Case Else
                ret1 = False
        End Select
    End Sub

    Private Sub textbox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles textbox2.KeyPress
        Dim temp As String

        If e.KeyChar = ClientDecimalSeparator And dec1 = False Then
            temp = Trim(GetNum2(textbox2.Text))
            textbox2.Text = Microsoft.VisualBasic.Left("        ", 8 - Len(temp)) & temp
            dec1 = True
        End If
    End Sub
    Private Sub textbox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles textbox2.TextChanged
        'btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(textbox2.Text)) > 0) And (Len(Trim(TextBox3.Text)) > 0)
    End Sub
    Private Sub TextBox3_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.Enter
        pjg = Len(TextBox3.Text)
        If pjg > 0 Then TextBox3.Mask = GetMaskText2Koma(TextBox3.Text)
        If pjg = 0 Then TextBox3.Mask = mask
    End Sub
    Private Sub TextBox3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.LostFocus
        If TextBox3.Text = "" Then Exit Sub
        TextBox3.TextMaskFormat = MaskFormat.IncludeLiterals
        pjg = 2 - Len(Microsoft.VisualBasic.Mid(TextBox3.Text, 12, 2))
        TextBox3.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        TextBox3.Text = TextBox3.Text & Microsoft.VisualBasic.Left("00", pjg)
        TextBox3.Mask = GetMaskText2Koma(TextBox3.Text)
        ret2 = True
        dec2 = False
    End Sub
    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        pjg = Len(TextBox3.Text)
        Select Case e.KeyCode
            Case Keys.Return
                If pjg >= 1 Then TextBox3_LostFocus(sender, e)
            Case Keys.D0 To Keys.D9, _
                 Keys.NumPad0 To Keys.NumPad9, _
                 Keys.Oem1 To Keys.Oem8

                If pjg > 0 And ret2 = True Then
                    TextBox3.Text = ""
                    TextBox3.Mask = mask
                End If
                ret2 = False
            Case Else
                ret2 = False
        End Select
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim temp As String

        If e.KeyChar = ClientDecimalSeparator And dec2 = False Then
            temp = Trim(GetNum2(TextBox3.Text))
            TextBox3.Text = Microsoft.VisualBasic.Left("        ", 8 - Len(temp)) & temp
            dec2 = True
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(textbox2.Text)) > 0) And (Len(Trim(TextBox3.Text)) > 0)
    End Sub

    Private Sub Textbox4_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Textbox4.Enter
        pjg = Len(Textbox4.Text)
        If pjg > 0 Then Textbox4.Mask = GetMaskText2Koma(Textbox4.Text)
        If pjg = 0 Then Textbox4.Mask = mask
    End Sub

    Private Sub TextBox4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Textbox4.LostFocus
        If Textbox4.Text = "" Then Exit Sub
        Textbox4.TextMaskFormat = MaskFormat.IncludeLiterals
        pjg = 2 - Len(Microsoft.VisualBasic.Mid(Textbox4.Text, 12, 2))
        Textbox4.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        Textbox4.Text = Textbox4.Text & Microsoft.VisualBasic.Left("00", pjg)
        Textbox4.Mask = GetMaskText2Koma(Textbox4.Text)
        ret3 = True
        dec3 = False
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Textbox4.KeyDown
        pjg = Len(Textbox4.Text)
        Select Case e.KeyCode
            Case Keys.Return
                If pjg >= 1 Then TextBox4_LostFocus(sender, e)
            Case Keys.D0 To Keys.D9, _
                 Keys.NumPad0 To Keys.NumPad9, _
                 Keys.Oem1 To Keys.Oem8

                If pjg > 0 And ret3 = True Then
                    Textbox4.Text = ""
                    Textbox4.Mask = mask
                End If
                ret3 = False
            Case Else
                ret3 = False
        End Select
    End Sub

    Private Sub Textbox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Textbox4.KeyPress
        Dim temp As String

        If e.KeyChar = ClientDecimalSeparator And dec3 = False Then
            temp = Trim(GetNum2(Textbox4.Text))
            Textbox4.Text = Microsoft.VisualBasic.Left("        ", 8 - Len(temp)) & temp
            dec3 = True
        End If
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Textbox4.TextChanged
        '        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(textbox2.Text)) > 0) And (Len(Trim(TextBox3.Text)) > 0) And (Len(Trim(Textbox4.Text)) > 0)
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
                    ClearMask()
                    textbox2.Text = MyReader.GetString("kurs")
                    textbox2.Mask = GetMaskText2Koma(textbox2.Text)
                    TextBox3.Text = MyReader.GetString("effective_kurs")
                    TextBox3.Mask = GetMaskText2Koma(TextBox3.Text)
                    Textbox4.Text = MyReader.GetString("kurs_pajak")
                    Textbox4.Mask = GetMaskText2Koma(Textbox4.Text)
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
        PilihanDlg.SQLGrid = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency"
        PilihanDlg.SQLFilter = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency " & _
                               "WHERE currency_code LIKE 'FilterData1%' " & _
                               " and currency_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub
    '----GAK DIPAKE LAGI COI
    'setelah di form lain di hapus
    'hapus aje
    Public Function GetMaskText(ByVal teks As String) As String
        Dim pjg As Integer
        Dim result As String

        'contoh teks 345,67 with koma
        'format indonesia
        teks = Replace(teks, ".", "")
        pjg = Len(Trim(teks))
        If pjg = 0 Then Exit Function

        result = Microsoft.VisualBasic.Right(teks, 3)
        pjg = pjg - 3

        While pjg > 3
            teks = Microsoft.VisualBasic.Left(teks, pjg)
            result = Microsoft.VisualBasic.Right(teks, 3) & result
            If pjg > 3 Then result = "." & result
            pjg = pjg - 3
        End While
        If pjg <= 0 Then pjg = Len(teks)
        result = Microsoft.VisualBasic.Left(teks, pjg) & result
        GetMaskTexT = result
    End Function

    Private Function GetMaskText2Koma(ByVal teks As String) As String
        'decimal (10,2)
        'mask tetap memakai format inggris 
        'apapun Regional Setting komputer client
        Dim pjg As Integer
        Dim result As String

        teks = Replace(teks, ClientDecimalSeparator, "")
        teks = Replace(teks, ClientGroupSeparator, "")
        pjg = Len(Trim(teks))
        If pjg = 0 Then Exit Function
        If pjg < 3 Then
            result = Microsoft.VisualBasic.Left("###", pjg) & "." & "##"
            GetMaskText2Koma = result
            Exit Function
        End If

        result = Microsoft.VisualBasic.Right(teks, 2)
        result = "." & Microsoft.VisualBasic.Left("##", Len(result))
        pjg = pjg - 2
        If pjg <= 3 Then
            GetMaskText2Koma = Microsoft.VisualBasic.Left("###", pjg) & result
            Exit Function
        End If
        While pjg > 3
            teks = Microsoft.VisualBasic.Left(teks, pjg)
            If pjg > 3 Then result = "," & "###" & result
            pjg = pjg - 3
        End While
        result = Microsoft.VisualBasic.Left("###", pjg) & result
        GetMaskText2Koma = result
    End Function
    Private Sub ClearText()
        textbox2.Text = ""
        TextBox3.Text = ""
        Textbox4.Text = ""
    End Sub
    Private Sub ClearMask()
        textbox2.Mask = ""
        TextBox3.Mask = ""
        Textbox4.Mask = ""
    End Sub
End Class
