'Title                         : Master Data Kurs
'Form                          : FM25_Expedition
'Table Used                    : Tbm_Expedition, tbm_city
'Stored Procedure Used (MySQL) : RunSQL
'Created By                    : tedi 13.02.2009

Public Class FM25_Expedition
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
        Group_Name.Text = ""
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

        '        DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_expedition")
        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select COMPANY_CODE as CompanyCode, COMPANY_NAME as CompanyName, COMPANY_SHORTNAME as CompanyShortName, " & _
                                             "ABBREVIATION	as Abbreviation, NPWP, IZIN_PERUSAHAAN	as IzinPerusahaan, API_U_APIT_NO as APIT_No, IZIN_DEPTAN_NO	as DEPTAN_No, " & _
                                             "EDI_NO as EDI_No, ADDRESS	as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, AUTHORIZE_PERSON	as AuthorizePerson1, " & _
                                             "AUTHORIZE_PERSON2	as AuthorizePerson2, AUTHORIZE_PERSON3	as AuthorizePerson3, TITLE as Title1, TITLE2 as Title2, TITLE3 as Title3, " & _
                                             "IDENTITY_NO as IdentityNo1, IDENTITY_NO2 as IdentityNo2, IDENTITY_NO3	as IdentityNo3, " & _
                                             "BANK_NAME as BankName1, BANK_BRANCH as BankBranch1, ACCOUNT_NO as AccountNo1, ACCOUNT_NAME as AccountName1, " & _
                                             "BANK_NAME2 as BankName2, BANK_BRANCH2 as BankBranch2, ACCOUNT_NO2 as AccountNo2, ACCOUNT_NAME2 as AccountName2, " & _
                                             "BANK_NAME3 as BankName3, BANK_BRANCH3 as BankBranch3, ACCOUNT_NO3 as AccountNo3, ACCOUNT_NAME3 as AccountName3, " & _
                                             "PPJK_STAT as StatusPPJK from tbm_expedition) as tbm_expedition ")
        '        DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        '        DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        brs = DataGridView1.RowCount
        btnDelete.Enabled = False
        txt_company_code.Enabled = True
'        txt_company_name.Enabled = True
        TextBox1.Enabled = True
        btnSearch.Visible = (TextBox1.Enabled = True)
        chk_active.Enabled = True

        txt_company_code.Clear()
        txt_company_name.Clear()
        txt_short_name.Clear()
        txt_abbreviation.Clear()
        txt_npwp.Clear()
        txt_siup_no.Clear()
        txt_apit_no.Clear()
        txt_deptan.Clear()
        txt_edi_no.Clear()
        txt_address.Clear()
        TextBox1.Clear()
        txt_phone.Clear()
        txt_fax.Clear()

        txt_title1.Clear()
        txt_auth_name1.Clear()
        txt_auth_id1.Clear()
        txt_title2.Clear()
        txt_auth_name2.Clear()
        txt_auth_id2.Clear()
        txt_title3.Clear()
        txt_auth_name3.Clear()
        txt_auth_id3.Clear()

        'txt_bank_name.Clear()
        'txt_bank_branch.Clear()
        'txt_accno.Clear()
        'txt_accname.Clear()
        'txt_bank_name2.Clear()
        'txt_bank_branch2.Clear()
        'txt_accno2.Clear()
        'txt_accname2.Clear()
        'txt_bank_name3.Clear()
        'txt_bank_branch3.Clear()
        'txt_accno3.Clear()
        'txt_accname3.Clear()

        chk_active.Checked = False

        txt_company_code.Focus()
        baru = True
        edit = False
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
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        If (MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_expedition " & _
                 "where company_code='" & txt_company_code.Text & "'"

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
                'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_expedition")
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
        If txt_company_code.Text = "" Then
            MsgBox("Company code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txt_company_code.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String
        Dim Errmsg As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim v_chk_active As String

        If CekData() = False Then Exit Sub

        If chk_active.Checked = True Then
            v_chk_active = "1"
        Else
            v_chk_active = "0"
        End If

        If baru Then
            teks = "Save Data"
            Errmsg = "Failed when saving user data"

            SQLstr = "insert into tbm_expedition (COMPANY_CODE, COMPANY_NAME, COMPANY_SHORTNAME, ABBREVIATION, NPWP, IZIN_PERUSAHAAN, API_U_APIT_NO, IZIN_DEPTAN_NO, EDI_NO, ADDRESS, CITY_CODE, PHONE, FAX, AUTHORIZE_PERSON, AUTHORIZE_PERSON2, AUTHORIZE_PERSON3, TITLE, TITLE2, TITLE3, IDENTITY_NO, IDENTITY_NO2, IDENTITY_NO3, BANK_NAME, BANK_BRANCH, ACCOUNT_NO, ACCOUNT_NAME, BANK_NAME2, BANK_BRANCH2, ACCOUNT_NO2, ACCOUNT_NAME2, BANK_NAME3, BANK_BRANCH3, ACCOUNT_NO3, ACCOUNT_NAME3, PPJK_STAT ) " & _
                     "VALUES ('" & txt_company_code.Text & "'," & _
                             "'" & txt_company_name.Text & "'," & _
                             "'" & txt_short_name.Text & "'," & _
                             "'" & txt_abbreviation.Text & "'," & _
                             "'" & txt_npwp.Text & "'," & _
                             "'" & txt_siup_no.Text & "'," & _
                             "'" & txt_apit_no.Text & "'," & _
                             "'" & txt_deptan.Text & "'," & _
                             "'" & txt_edi_no.Text & "'," & _
                             "'" & txt_address.Text & "'," & _
                             "'" & TextBox1.Text & "'," & _
                             "'" & txt_phone.Text & "'," & _
                             "'" & txt_fax.Text & "'," & _
                             "'" & txt_auth_name1.Text & "'," & _
                             "'" & txt_auth_name2.Text & "'," & _
                             "'" & txt_auth_name3.Text & "'," & _
                             "'" & txt_title1.Text & "'," & _
                             "'" & txt_title2.Text & "'," & _
                             "'" & txt_title3.Text & "'," & _
                             "'" & txt_auth_id1.Text & "'," & _
                             "'" & txt_auth_id2.Text & "'," & _
                             "'" & txt_auth_id3.Text & "'," & _
                             "'" & txt_bank_name.Text & "'," & _
                             "'" & txt_bank_branch.Text & "'," & _
                             "'" & txt_accno.Text & "'," & _
                             "'" & txt_accname.Text & "'," & _
                             "'" & txt_bank_name2.Text & "'," & _
                             "'" & txt_bank_branch2.Text & "'," & _
                             "'" & txt_accno2.Text & "'," & _
                             "'" & txt_accname2.Text & "'," & _
                             "'" & txt_bank_name3.Text & "'," & _
                             "'" & txt_bank_branch3.Text & "'," & _
                             "'" & txt_accno3.Text & "'," & _
                             "'" & txt_accname3.Text & "'," & _
                             "'" & v_chk_active & "')"
        Else
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbm_expedition " & _
                     "SET company_name = '" & txt_company_name.Text & "' ," & _
                     " COMPANY_SHORTNAME = '" & txt_short_name.Text & "' , " & _
                     " ABBREVIATION      = '" & txt_abbreviation.Text & "' , " & _
                     " NPWP = '" & txt_npwp.Text & "' , " & _
                     " IZIN_PERUSAHAAN = '" & txt_siup_no.Text & "' , " & _
                     " API_U_APIT_NO = '" & txt_apit_no.Text & "' , " & _
                     " IZIN_DEPTAN_NO = '" & txt_deptan.Text & "' , " & _
                     " EDI_NO = '" & txt_edi_no.Text & "' , " & _
                     " ADDRESS = '" & txt_address.Text & "' , " & _
                     " city_code = '" & TextBox1.Text & "' , " & _
                     " PHONE = '" & txt_phone.Text & "' , " & _
                     " FAX = '" & txt_fax.Text & "' , " & _
                     " AUTHORIZE_PERSON = '" & txt_auth_name1.Text & "' , " & _
                     " AUTHORIZE_PERSON2 = '" & txt_auth_name2.Text & "' , " & _
                     " AUTHORIZE_PERSON3 = '" & txt_auth_name3.Text & "' , " & _
                     " TITLE = '" & txt_title1.Text & "' , " & _
                     " TITLE2 = '" & txt_title2.Text & "' , " & _
                     " TITLE3 = '" & txt_title3.Text & "' , " & _
                     " IDENTITY_NO = '" & txt_auth_id1.Text & "' , " & _
                     " IDENTITY_NO2 = '" & txt_auth_id2.Text & "' , " & _
                     " IDENTITY_NO3 = '" & txt_auth_id3.Text & "' , " & _
                     " BANK_NAME = '" & txt_bank_name.Text & "' , " & _
                     " BANK_BRANCH = '" & txt_bank_branch.Text & "' , " & _
                     " ACCOUNT_NO = '" & txt_accno.Text & "' , " & _
                     " ACCOUNT_NAME = '" & txt_accname.Text & "' , " & _
                     " BANK_NAME2 = '" & txt_bank_name2.Text & "' , " & _
                     " BANK_BRANCH2 = '" & txt_bank_branch2.Text & "' , " & _
                     " ACCOUNT_NO2 = '" & txt_accno2.Text & "' , " & _
                     " ACCOUNT_NAME2 = '" & txt_accname2.Text & "' , " & _
                     " BANK_NAME3 = '" & txt_bank_name3.Text & "' , " & _
                     " BANK_BRANCH3 = '" & txt_bank_branch3.Text & "' , " & _
                     " ACCOUNT_NO3 = '" & txt_accno3.Text & "' , " & _
                     " ACCOUNT_NAME3 = '" & txt_accname3.Text & "' , " & _
                     " PPJK_STAT = '" & v_chk_active & "'" & _
                     " where COMPANY_CODE='" & txt_company_code.Text & "'"
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
        txt_company_code.Text = DataGridView1.Item(0, brs).Value.ToString
        txt_company_name.Text = DataGridView1.Item(1, brs).Value.ToString
        txt_short_name.Text = DataGridView1.Item(2, brs).Value.ToString
        txt_abbreviation.Text = DataGridView1.Item(3, brs).Value.ToString
        txt_npwp.Text = DataGridView1.Item(4, brs).Value.ToString
        txt_siup_no.Text = DataGridView1.Item(5, brs).Value.ToString
        txt_apit_no.Text = DataGridView1.Item(6, brs).Value.ToString
        txt_deptan.Text = DataGridView1.Item(7, brs).Value.ToString
        txt_edi_no.Text = DataGridView1.Item(8, brs).Value.ToString
        txt_address.Text = DataGridView1.Item(9, brs).Value.ToString
        TextBox1.Text = DataGridView1.Item(10, brs).Value.ToString
        txt_phone.Text = DataGridView1.Item(11, brs).Value.ToString
        txt_fax.Text = DataGridView1.Item(12, brs).Value.ToString
        txt_auth_name1.Text = DataGridView1.Item(13, brs).Value.ToString
        txt_auth_name2.Text = DataGridView1.Item(14, brs).Value.ToString
        txt_auth_name3.Text = DataGridView1.Item(15, brs).Value.ToString
        txt_title1.Text = DataGridView1.Item(16, brs).Value.ToString
        txt_title2.Text = DataGridView1.Item(17, brs).Value.ToString
        txt_title3.Text = DataGridView1.Item(18, brs).Value.ToString
        txt_auth_id1.Text = DataGridView1.Item(19, brs).Value.ToString
        txt_auth_id2.Text = DataGridView1.Item(20, brs).Value.ToString
        txt_auth_id3.Text = DataGridView1.Item(21, brs).Value.ToString

        txt_bank_name.Text = DataGridView1.Item(22, brs).Value.ToString
        txt_bank_branch.Text = DataGridView1.Item(23, brs).Value.ToString
        txt_accno.Text = DataGridView1.Item(24, brs).Value.ToString
        txt_accname.Text = DataGridView1.Item(25, brs).Value.ToString

        txt_bank_name2.Text = DataGridView1.Item(26, brs).Value.ToString
        txt_bank_branch2.Text = DataGridView1.Item(27, brs).Value.ToString
        txt_accno2.Text = DataGridView1.Item(28, brs).Value.ToString
        txt_accname2.Text = DataGridView1.Item(29, brs).Value.ToString

        txt_bank_name3.Text = DataGridView1.Item(30, brs).Value.ToString
        txt_bank_branch3.Text = DataGridView1.Item(31, brs).Value.ToString
        txt_accno3.Text = DataGridView1.Item(32, brs).Value.ToString
        txt_accname3.Text = DataGridView1.Item(33, brs).Value.ToString

        If DataGridView1.Item(34, brs).Value.ToString = "1" Then
            chk_active.Checked = True
        Else
            chk_active.Checked = False
        End If

        txt_company_code.Enabled = False
        btnSearch.Visible = (TextBox1.Enabled = True)
        btnDelete.Enabled = (Len(Trim(txt_company_code.Text)) > 0)
        txt_company_name.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Group_Name.Text = AmbilData("CITY_NAME", "tbm_city", "CITY_CODE='" & TextBox1.Text & "'")
        '        Call f_getdata()
    End Sub

    Private Sub f_getdata()
        If (Len(Trim(txt_company_code.Text))) = 0 Then Exit Sub
        '        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        '        SQLstr = "select * from tbm_expedition where company_code = '" & txt_company_code.Text & "' "
        SQLstr = "select COMPANY_CODE as CompanyCode, COMPANY_NAME as CompanyName, COMPANY_SHORTNAME as CompanyShortName, ABBREVIATION as Abbreviation, NPWP, IZIN_PERUSAHAAN as IzinPerusahaan, API_U_APIT_NO as APIT_No, IZIN_DEPTAN_NO as DEPTAN_No, EDI_NO	as EDI_No, " & _
                 "ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, AUTHORIZE_PERSON as AuthorizePerson1, AUTHORIZE_PERSON2 as AuthorizePerson2, AUTHORIZE_PERSON3	as AuthorizePerson3, TITLE	as Title1, TITLE2 as Title2, TITLE3	as Title3, IDENTITY_NO	as IdentityNo1, IDENTITY_NO2 as IdentityNo2, IDENTITY_NO3 as IdentityNo3, " & _
                 "BANK_NAME	as BankName1, BANK_BRANCH as Branch1, ACCOUNT_NO as AccountNo1, ACCOUNT_NAME as AccountName1, " & _
                 "BANK_NAME2 as BankName2, BANK_BRANCH2 as Branch2, ACCOUNT_NO2 as AccountNo2, ACCOUNT_NAME2 as AccountName2, " & _
                 "BANK_NAME3 as BankName3, BANK_BRANCH3 as Branch3, ACCOUNT_NO3 as AccountNo3, ACCOUNT_NAME3 as AccountName3, " & _
                 "PPJK_STAT	as StatusPPJK from tbm_expedition where company_code = '" & txt_company_code.Text & "' "
        ErrMsg = "Failed when read Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    'ClearMask()
                    'txt_subgroup_name.Text = MyReader.GetString("kurs")
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                txt_company_code.Enabled = True
                btnDelete.Enabled = False
            Else
                baru = False
                edit = True
                txt_company_code.Enabled = False
                btnDelete.Enabled = True
            End If
            btnSearch.Visible = (TextBox1.Enabled = True)
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select city_code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        '        PilihanDlg.SQLGrid = "SELECT * FROM tbm_city "
        PilihanDlg.SQLGrid = "select CITY_CODE as CityCode, CITY_NAME as CityName, COUNTRY_CODE as CountryCode from tbm_city "
        PilihanDlg.SQLFilter = " select CITY_CODE as CityCode, CITY_NAME as CityName, COUNTRY_CODE as CountryCode from tbm_city  " & _
                               " WHERE city_code LIKE 'FilterData1%' " & _
                               " and city_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
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
        GetMaskText = result
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
        txt_company_name.Text = ""
        txt_company_name.Text = ""
        TextBox1.Text = ""
        chk_active.Checked = False
    End Sub
    Private Sub ClearMask()
        '        txt_subgroup_name.Mask = ""
    End Sub

End Class
