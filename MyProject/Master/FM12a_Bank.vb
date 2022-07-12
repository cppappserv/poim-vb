'Title                         : Master Data Bank
'Form                          : FM12_Bank
'Table Used                    : tbm_bank,tbm_currency,tbm_city
'Stored Procedure Used (MySQL) : RunSQL
'Created By                    : YANTI Okt 2008
'Modify By                     : Yanti 10.12.2008  Semua inputan angka format flexible mengikuti regional setting 
'Modify By                     : Hanny 31.03.2009  rubah semua inputan edit mask menjadi inputan biasa aja

Public Class FM12a_Bank
    Dim baru, edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan

    Dim mask As String
    Dim ret1, ret2, ret3, dec1, dec2, dec3 As Boolean
    Dim ClientDecimalSeparator, ClientGroupSeparator, ServerDecimal As String
    Dim RegionalSetting As System.Globalization.CultureInfo
    Dim pjg As Integer
    Dim hasil As Boolean = False
    Dim dr As MySqlClient.MySqlDataReader
    Dim MyComm As MySqlCommand = MyConn.CreateCommand()
    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        'btnSave.Enabled = False
        Curr_Name.Text = ""
        City_name.Text = ""

        mask = "##,###,###.##"
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        RegionalSetting = Global.System.Globalization.CultureInfo.CurrentCulture
        ServerDecimal = "."
        'TextBox6.Culture = RegionalSetting
        'TextBox7.Culture = RegionalSetting
        'TextBox8.Culture = RegionalSetting
        Call RefreshVar()
    End Sub
    Private Sub RefreshVar()
        dec1 = False
        dec2 = False
        dec3 = False
        ret1 = True
        ret2 = True
        ret3 = True
    End Sub
    Private Sub Refresh0()
        'TextBox6.Text = "000"
        'TextBox6.Mask = "#.##"
        'TextBox7.Text = "000"
        'TextBox7.Mask = "#.##"
        'TextBox8.Text = "000"
        'TextBox8.Mask = "#.##"
    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen()
    End Sub
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
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
    End Sub
    Private Sub ClearMask()
        'TextBox6.Mask = ""
        'TextBox7.Mask = ""
        'TextBox8.Mask = ""
    End Sub
    Private Sub RefreshScreen()
        Dim brs As Integer

        cb1.Checked = True
        cb2.Checked = False
        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tb.BANK_CODE as BankCode, tb.CURRENCY_CODE as CurrencyCode, tb.BANK_NAME as BankName, tb.CITY_CODE as CityCode, tc.city_name as CityName, tb.ACCOUNT_NO as AccountNo, tb.MARGIN_DEPOSIT as MarginDeposit, tb.COMMISION as Commission, tb.POSTAGE_CHARGES as PostageCharges, if(tb.More_Less='1','Include','Exclude') as MoreOrLess from tbm_bank as tb inner join tbm_city as tc on tb.city_code = tc.city_code) as a")
        DataGridView1.Columns(6).DefaultCellStyle.Format = "N3"
        DataGridView1.Columns(7).DefaultCellStyle.Format = "N3"
        DataGridView1.Columns(8).DefaultCellStyle.Format = "N3"
        DataGridView1.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        brs = DataGridView1.RowCount
        'btnSave.Enabled = False
        btnDelete.Enabled = False
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        Call ClearText()
        Call ClearMask()
        Call RefreshVar()
        Call Refresh0()
        TextBox1.Focus()
        baru = True
        edit = False
        btnSearch.Visible = (TextBox2.Enabled = True)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub
        SQLstr = "DELETE from tbm_bank " & _
                 "where bank_code='" & TextBox1.Text & "' and currency_code='" & TextBox2.Text & "'"

        ErrMsg = "Failed when deleting user data"

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
                'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_bank")
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

        'Foreign Key
        If Curr_Name.Text = "" Then
            MsgBox("Currency code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox2.Focus()
            Exit Function
        End If

        If City_name.Text = "" Then
            MsgBox("City code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox4.Focus()
            Exit Function
        End If
        'If ret1 = False Then TextBox6_LostFocus(TextBox6, ee)
        'If ret2 = False Then TextBox7_LostFocus(TextBox7, ee)
        'If ret3 = False Then TextBox8_LostFocus(TextBox8, ee)
    End Function
    Function f_convertdata(ByVal strNilai As String) As String
        If strNilai <> "" Then
            f_convertdata = Replace(CStr(CDec(strNilai)), ",", ".")
        Else
            f_convertdata = "0"
        End If
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String
        Dim v_margin, v_charges, v_commision As String
        If CekData() = False Then Exit Sub

        v_margin = f_convertdata(TextBox6.Text)
        v_commision = f_convertdata(TextBox7.Text)
        v_charges = f_convertdata(TextBox8.Text)
        If Not cb1.Checked And Not cb2.Checked Then
            MsgBox("Please select Include or Exclude")
            Exit Sub
        End If
        If baru Then
            teks = "Save Data"
            ErrMsg = "Failed when saving user data"
            SQLstr = "INSERT INTO tbm_bank (bank_code,currency_code,bank_name,city_code,account_no,margin_deposit,coMmision,postage_charges,more_less) " & _
                     "VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & _
                                   TextBox4.Text & "', '" & TextBox5.Text & "'," & v_margin & "," & _
                                   v_commision & "," & v_charges & "," & If(cb1.Checked, 1, 0) & ")"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating user data"
            SQLstr = "UPDATE tbm_bank " & _
                     "SET bank_name = '" & TextBox3.Text & "'," & _
                     "city_code = '" & TextBox4.Text & "'," & _
                     "account_no = '" & TextBox5.Text & "'," & _
                     "margin_deposit = " & v_margin & "," & _
                     "commision = " & v_commision & "," & _
                     "postage_charges = " & v_charges & "," & _
                     "more_less = " & If(cb1.Checked, 1, 0) & _
                     " where bank_code='" & TextBox1.Text & "' and currency_code='" & TextBox2.Text & "'"
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
        TextBox2.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox3.Text = DataGridView1.Item(2, brs).Value.ToString
        TextBox4.Text = DataGridView1.Item(3, brs).Value.ToString
        TextBox5.Text = DataGridView1.Item(5, brs).Value.ToString
        'TextBox6.Text = GetNum3(DataGridView1.Item(6, brs).Value.ToString)
        'TextBox7.Text = GetNum3(DataGridView1.Item(7, brs).Value.ToString)
        'TextBox8.Text = GetNum3(DataGridView1.Item(8, brs).Value.ToString)
        TextBox6.Text = FormatNumber(DataGridView1.Item(6, brs).Value.ToString, 3)
        TextBox7.Text = FormatNumber(DataGridView1.Item(7, brs).Value.ToString, 3)
        TextBox8.Text = FormatNumber(DataGridView1.Item(8, brs).Value.ToString, 3)
        TextBox1.Enabled = False
        btnDelete.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        'Textbox6_Enter(sender, e)
        'TextBox7_Enter(sender, e)
        'TextBox8_Enter(sender, e)
        TextBox2.Enabled = False
        btnSearch.Visible = False
        cb1.Checked = (DataGridView1.Item(9, brs).Value.ToString = "Include")
        cb2.Checked = (DataGridView1.Item(9, brs).Value.ToString = "Exclude")
        TextBox3.Focus()
    End Sub
    Private Sub f_getdata()
        If Not (Len(Trim(TextBox1.Text)) > 0 And Len(Trim(TextBox2.Text)) > 0 And Len(Trim(TextBox4.Text)) > 0) Then Exit Sub

        SQLstr = "select * from tbm_BANK where BANK_code = '" & TextBox1.Text & "' " & _
                 "and currency_code='" & TextBox2.Text & "' and city_code='" & TextBox4.Text & "'"
        ErrMsg = "Failed when read Users Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    ClearMask()
                    TextBox3.Text = MyReader.GetString("bank_name")
                    TextBox5.Text = MyReader.GetString("account_no")

                    Try
                        TextBox6.Text = FormatNumber(MyReader.GetString("margin_deposit"), 3)
                    Catch ex As Exception
                        TextBox6.Text = FormatNumber(0, 3)
                    End Try
                    'TextBox6.Mask = GetMaskText2Koma(TextBox6.Text)
                    Try
                        TextBox7.Text = FormatNumber(MyReader.GetString("commision"), 3)
                    Catch ex As Exception
                        TextBox7.Text = FormatNumber(0, 3)
                    End Try
                    'TextBox7.Mask = GetMaskText2Koma(TextBox7.Text)
                    Try
                        TextBox8.Text = FormatNumber(MyReader.GetString("postage_charges"), 3)
                    Catch ex As Exception
                        TextBox8.Text = FormatNumber(0, 3)
                    End Try
                    'TextBox8.Mask = GetMaskText2Koma(TextBox8.Text)
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                TextBox1.Enabled = True
                TextBox2.Enabled = True
                btnDelete.Enabled = False
            Else
                baru = False
                edit = True
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                btnDelete.Enabled = True
            End If
            btnSearch.Visible = (TextBox2.Enabled = True)
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'Call f_getdata()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Curr_Name.Text = AmbilData("CURRENCY_NAME", "tbm_currency", "CURRENCY_CODE='" & TextBox2.Text & "'")
        'Call f_getdata()
        Exit Sub
        SQLstr = "select * from tbm_currency where currency_code = '" & TextBox2.Text & "' "
        ErrMsg = "Failed when read Currency Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        Curr_Name.Text = ""

        If Not MyReader Is Nothing Then
            While MyReader.Read
                'Try
                Curr_Name.Text = MyReader.GetString("Currency_Name")
                'Catch ex As Exception
                'End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    'Private Sub TextBox6_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.Enter
    '    pjg = Len(TextBox6.Text)
    '    If pjg > 0 Then TextBox6.Mask = GetMaskText2Koma(TextBox6.Text)
    '    If pjg = 0 Then TextBox6.Mask = mask
    'End Sub
    'Private Sub TextBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyDown
    '    pjg = Len(TextBox6.Text)
    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            If pjg >= 1 Then TextBox6_LostFocus(sender, e)
    '        Case Keys.D0 To Keys.D9, _
    '             Keys.NumPad0 To Keys.NumPad9, _
    '             Keys.Oem1 To Keys.Oem8

    '            If pjg > 0 And ret1 = True Then
    '                TextBox6.Text = ""
    '                TextBox6.Mask = mask
    '            End If
    '            ret1 = False
    '        Case Else
    '            ret1 = False
    '    End Select
    'End Sub

    'Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
    '    Dim temp As String

    '    If e.KeyChar = ClientDecimalSeparator And dec1 = False Then
    '        temp = Trim(GetNum2(TextBox6.Text))
    '        TextBox6.Text = Microsoft.VisualBasic.Left("        ", 8 - Len(temp)) & temp
    '        dec1 = True
    '    End If
    'End Sub
    'Private Sub TextBox6_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.LostFocus
    '    If TextBox6.Text = "" Then Exit Sub
    '    TextBox6.TextMaskFormat = MaskFormat.IncludeLiterals
    '    pjg = 2 - Len(Microsoft.VisualBasic.Mid(TextBox6.Text, 12, 2))
    '    TextBox6.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
    '    TextBox6.Text = TextBox6.Text & Microsoft.VisualBasic.Left("00", pjg)
    '    TextBox6.Mask = GetMaskText2Koma(TextBox6.Text)
    '    ret1 = True
    '    dec1 = False
    'End Sub

    'Private Sub TextBox7_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox7.Enter
    '    pjg = Len(TextBox7.Text)
    '    If pjg > 0 Then TextBox7.Mask = GetMaskText2Koma(TextBox7.Text)
    '    If pjg = 0 Then TextBox7.Mask = mask
    'End Sub

    'Private Sub TextBox7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyDown
    '    pjg = Len(TextBox7.Text)
    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            If pjg >= 1 Then TextBox7_LostFocus(sender, e)
    '        Case Keys.D0 To Keys.D9, _
    '             Keys.NumPad0 To Keys.NumPad9, _
    '             Keys.Oem1 To Keys.Oem8

    '            If pjg > 0 And ret2 = True Then
    '                TextBox7.Text = ""
    '                TextBox7.Mask = mask
    '            End If
    '            ret2 = False
    '        Case Else
    '            ret2 = False
    '    End Select
    'End Sub
    'Private Sub TextBox8_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox8.Enter
    '    pjg = Len(TextBox8.Text)
    '    If pjg > 0 Then TextBox8.Mask = GetMaskText2Koma(TextBox8.Text)
    '    If pjg = 0 Then TextBox8.Mask = mask
    'End Sub
    'Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
    '    pjg = Len(TextBox8.Text)
    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            If pjg >= 1 Then TextBox8_LostFocus(sender, e)
    '        Case Keys.D0 To Keys.D9, _
    '             Keys.NumPad0 To Keys.NumPad9, _
    '             Keys.Oem1 To Keys.Oem8

    '            If pjg > 0 And ret3 = True Then
    '                TextBox8.Text = ""
    '                TextBox8.Mask = mask
    '            End If
    '            ret3 = False
    '        Case Else
    '            ret3 = False
    '    End Select
    'End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select Currency Code"
        PilihanDlg.LblKey1.Text = "Currency Code"
        PilihanDlg.LblKey2.Text = "Currency Name"
        PilihanDlg.SQLGrid = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency"
        PilihanDlg.SQLFilter = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency " & _
                               "WHERE currency_code LIKE 'FilterData1%' " & _
                               " and currency_name like  'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "select CITY_CODE as CityCode, CITY_NAME as CityName from tbm_city"
        PilihanDlg.SQLFilter = "select CITY_CODE as CityCode, CITY_NAME as CityName from tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' " & _
                               " AND city_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox4.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        City_name.Text = AmbilData("city_name", "tbm_city", "city_code='" & TextBox4.Text & "'")
        'Call f_getdata()
        Exit Sub
        SQLstr = "select * from tbm_city where city_code = '" & TextBox4.Text & "' "
        ErrMsg = "Failed when read City Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        City_name.Text = ""

        If Not MyReader Is Nothing Then
            While MyReader.Read
                'Try
                City_name.Text = MyReader.GetString("City_Name")
                'Catch ex As Exception
                'End Try

            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub



    'Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Dim temp As String

    '    If e.KeyChar = ClientDecimalSeparator And dec2 = False Then
    '        temp = Trim(GetNum2(TextBox7.Text))
    '        TextBox7.Text = Microsoft.VisualBasic.Left("        ", 8 - Len(temp)) & temp
    '        dec2 = True
    '    End If
    'End Sub

    'Private Sub TextBox7_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If TextBox7.Text = "" Then Exit Sub
    '    TextBox7.TextMaskFormat = MaskFormat.IncludeLiterals
    '    pjg = 2 - Len(Microsoft.VisualBasic.Mid(TextBox7.Text, 12, 2))
    '    TextBox7.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
    '    TextBox7.Text = TextBox7.Text & Microsoft.VisualBasic.Left("00", pjg)
    '    TextBox7.Mask = GetMaskText2Koma(TextBox7.Text)
    '    ret2 = True
    '    dec2 = False
    'End Sub


    'Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Dim temp As String

    '    If e.KeyChar = ClientDecimalSeparator And dec3 = False Then
    '        temp = Trim(GetNum2(TextBox8.Text))
    '        TextBox8.Text = Microsoft.VisualBasic.Left("        ", 8 - Len(temp)) & temp
    '        dec3 = True
    '    End If
    'End Sub

    'Private Sub TextBox8_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If TextBox8.Text = "" Then Exit Sub
    '    TextBox8.TextMaskFormat = MaskFormat.IncludeLiterals
    '    pjg = 2 - Len(Microsoft.VisualBasic.Mid(TextBox8.Text, 12, 2))
    '    TextBox8.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
    '    TextBox8.Text = TextBox8.Text & Microsoft.VisualBasic.Left("00", pjg)
    '    TextBox8.Mask = GetMaskText2Koma(TextBox8.Text)
    '    ret3 = True
    '    dec3 = False
    'End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub


    Private Sub TextBox6_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.Validated
        If IsNumeric(TextBox6.Text) Or TextBox6.Text = "" Then
            If TextBox6.Text = "" Then TextBox6.Text = 0
            TextBox6.Text = FormatNumber(TextBox6.Text, 3)
        Else
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub


    Private Sub TextBox7_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox7.Validated
        If IsNumeric(TextBox6.Text) Or TextBox7.Text = "" Then
            If TextBox7.Text = "" Then TextBox7.Text = 0
            TextBox7.Text = FormatNumber(TextBox7.Text, 3)
        Else
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
        End If
    End Sub
    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub


    Private Sub TextBox8_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox8.Validated
        If IsNumeric(TextBox8.Text) Or TextBox8.Text = "" Then
            If TextBox8.Text = "" Then TextBox8.Text = 0
            TextBox8.Text = FormatNumber(TextBox8.Text, 3)
        Else
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub cb1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb1.CheckedChanged
        If cb1.Checked Then cb2.Checked = False
    End Sub

    Private Sub cb2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb2.CheckedChanged
        If cb2.Checked Then cb1.Checked = False
    End Sub
End Class
