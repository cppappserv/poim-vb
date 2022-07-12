Imports BPUtility.RegistryUtility
Imports Microsoft.Win32
Module Module1
    Public DbHost, DbName As String
    Public HaveAccess As Boolean
    Public UserData As New BPUtility.SetUserData
    Public ApplData As New BPUtility.SetApplData
    Public MyConn, MyConn1, MyConn2 As MySqlConnection
    Public var_msg As String
    Public f_caller As String
    Public v_group_code As String
    Public TempTableName As String 'Added by estrika 251010
    Public v_no, v_ord As String
    Public txtPO1, txtPO2 As String
    Public xpoitem1, xaju_no, xto_sap, xto_sap2 As String
    Public tanggal_sob1 As Date

    Public sp_po, sp_ebeln, sp_shipment, sp_invoic_due_date, sp_value_date, sp_approve, sp_prepared, sp_vessel, sp_unit2, sp_invoice_no, sp_unit8, sp_unit10, sp_unit9, sp_unit11, sp_user_id, sp_ord_no As String
    Public sp_quantity As String
    Public sp_sob, sp_invoice_date As Date
    Public sp_flag_upload As Integer
    Public sp_kurs, sp_unit_price, sp_insurance, sp_ppn, sp_import_duty, sp_clearence_cost As Double



    Sub Main()
        ' run GetConfig
        xto_sap = ""
        xto_sap2 = ""
        GetConfig()

        ' open login form
        FRMLogin.ShowDialog()
        If HaveAccess Then
            MDIParent1.ShowDialog()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub GetConfig()
        Dim ConfigData As New BPConnection.SetConfig
        'Dim MyConn As MySqlConnection
        'Dim MyReader As MySqlDataReader
        'Dim SQLStr, ErrMsg, 
        Dim StrValue As String
        Dim DataConfig As New Hashtable(10)
        'Dim KeyName As String = Registry.LocalMachine.ToString & "\SOFTWARE\CPI\BukuPetambak"

        ' set config data
        ConfigData.KeepIn = "Registry"
        ConfigData.FullName = Registry.LocalMachine.ToString & "\SOFTWARE\CPI\POIMPORT"
        'ConfigData.KeepIn = "File"
        'ConfigData.FullName = My.Application.Info.DirectoryPath & "\koneksi.enc"

        ' set UserData info
        With UserData
            .UserCT = 1
            .UserId = "admin"
            .AdmAccess = True
            .TrapErr = True
            .DbgAccess = True
            .SaveAudittrail = True
            .ConfigData = ConfigData
            '.ConfigData.KeepIn = "Registry"
            '.ConfigData.FullName = Registry.LocalMachine.ToString & "\SOFTWARE\CPI\BukuPetambak"
            '.ConfigData.KeepIn = "File"
            '.ConfigData.FullName = My.Application.Info.DirectoryPath & "\koneksi.enc"
        End With

        ' Find connection string location
        StrValue = GetRegistry("KeepConnectionIn", UserData)
        If StrValue.Equals("") Then
            ApplData.SimpanKoneksi = "Registry"
            SetRegistry("KeepConnectionIn", "Registry", RegistryValueKind.String, UserData)
        Else
            ApplData.SimpanKoneksi = StrValue
        End If

        ' open connection
        MyConn = FncMyConnection(UserData.ConfigData)
        If MyConn Is Nothing Then
            FrmSettingConnection.ShowDialog()
            MyConn = FncMyConnection(UserData.ConfigData)
            If MyConn Is Nothing Then
                System.Windows.Forms.MessageBox.Show("Gagal membuat koneksi ke database.")
                Exit Sub
            End If

        End If
        ' keep DbHost & DbName
        DbHost = MyConn.DataSource
        DbName = MyConn.Database

        '' get nama pt, alamat pt & other config
        'SQLStr = "SELECT ConfName, ConfValue FROM ot_config " & _
        '         "WHERE ConfName = 'AlamatPT' OR ConfName = 'NamaPT' OR ConfName='TrapError' " & _
        '                "OR ConfName='SaveAudittrail'"
        'ErrMsg = "Gagal baca data konfigurasi."
        'MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        'If Not MyReader Is Nothing Then
        '    If MyReader.HasRows = True Then
        '        While MyReader.Read()
        '            Try
        '                'If MyReader.GetString(0).Equals("TrapError") Then
        '                '    If MyReader.GetString("ConfValue") = "N" Then UserData.TrapErr = False
        '                '    DataConfig.Add("TrapError", True)
        '                'End If
        '                'If MyReader.GetString(0).Equals("SaveAudittrail") Then
        '                '    If MyReader.GetString("ConfValue") = "N" Then UserData.SaveAudittrail = False
        '                '    DataConfig.Add("SaveAudittrail", True)
        '                'End If
        '                'If MyReader.GetString(0).Equals("NamaPT") Then
        '                '    ApplData.NamaPT = MyReader.GetString("ConfValue")
        '                '    DataConfig.Add("NamaPT", True)
        '                'End If
        '                'If MyReader.GetString(0).Equals("AlamatPT") Then
        '                '    ApplData.AlamatPT = MyReader.GetString("ConfValue")
        '                '    DataConfig.Add("AlamatPT", True)
        '                'End If

        '            Catch ex As Exception
        '            End Try
        '        End While
        '    End If
        'End If
        'CloseMyReader(MyReader, UserData)

        '' cek setting
        'If DataConfig.ContainsKey("TrapError") = False Then SetConfigData("TrapError", "Y", False, MyConn)
        'If DataConfig.ContainsKey("SaveAudittrail") = False Then SetConfigData("SaveAudittrail", "Y", False, MyConn)
        'If DataConfig.ContainsKey("NamaPT") = False Then SetConfigData("NamaPT", "Nama PT", False, MyConn)
        'If DataConfig.ContainsKey("AlamatPT") = False Then SetConfigData("AlamatPT", "Alamat PT", False, MyConn)

        'CloseMyConn(MyConn)
        ''MyConn = Nothing

        '' Get setting awal tgl proses
        'StrValue = GetRegistry("AwalTglProses", UserData)
        'If StrValue.Equals("") Then
        '    ApplData.AwalTglProses = "TglHariIni"
        '    SetRegistry("AwalTglProses", "TglHariIni", RegistryValueKind.String, UserData)
        'Else
        '    ApplData.AwalTglProses = StrValue
        'End If

    End Sub

    Public Function SetConfigData(ByVal ConfName As String, ByVal ConfValue As String, _
                                  ByVal UpdateData As Boolean, ByVal MyConn As MySqlConnection) _
                                  As Integer
        Dim SQLStr, ErrMsg As String
        If UpdateData Then
            SQLStr = "UPDATE ot_Config SET ConfValue='" & ConfValue & "', User_id='" & UserData.UserId & "' " & _
                     "WHERE ConfName='" & ConfName & "'"
        Else
            SQLStr = "INSERT INTO ot_config (ConfName, ConfValue, User_Id) " & _
                     "VALUES ('" & ConfName & "', '" & ConfValue & "', '" & UserData.UserId & "')"
        End If
        ErrMsg = "Gagal update data config."
        Return DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
    End Function

    Public Function ChkEmptyValue(ByVal aControl As Control, ByRef Result As String, _
                                      Optional ByVal ErrMsg As String = "Field tidak diisi.", _
                                      Optional ByVal ErrTitle As String = "Informasi") _
                                      As Boolean
        ' False jika ada isinya, true jika tidak ada
        Dim Empty As Boolean = False

        If aControl Is Nothing Then
            Return True
        Else
            Result = aControl.Text
            If Result.Equals("") Then
                MsgBox(ErrMsg, MsgBoxStyle.Exclamation, ErrTitle)
                aControl.Focus()
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Function CekInputTgl(ByVal nControl As Control, ByVal nControldt As DateTimePicker, ByVal nPesan As String) As Boolean
        If nControl.Text = "" Or nControl.Text = "0" Then
            'MsgBox(nPesan & " Tidak Boleh Kosong...!", vbInformation, "PERHATIAN")
            'nControl.Focus()
            'CekInputTgl = False
            CekInputTgl = True
        Else
            If nControldt.Checked = False Then
                MsgBox(nPesan & " Tidak Boleh Kosong...!", vbInformation, "PERHATIAN")
                nControldt.Focus()
                CekInputTgl = False
            Else
                CekInputTgl = True
            End If
        End If
    End Function


    Public Function CekInput(ByVal nControl As Control, ByVal nPesan As String) As Boolean
        If nControl.Text = "" Or nControl.Text = "0" Then
            MsgBox(nPesan & " Tidak Boleh Kosong...!", vbInformation, "PERHATIAN")
            nControl.Focus()
            CekInput = False
        Else
            CekInput = True
        End If
    End Function
    Public Function CHECK_INT(ByVal nControl As Control, ByVal nPesan As String) As Boolean
        If Not IsNumeric(nControl.Text) Then
            MsgBox(nPesan & " Fill with Number", vbInformation, "Information")
            nControl.Focus()
            CHECK_INT = False
        Else
            CHECK_INT = True
        End If
    End Function
    Public Function Show_Grid(ByVal nControl As DataGridView, ByVal lv_tbl As String) As DataTable

        Dim SQLStr, ErrMsg As String
        Dim dts As DataTable

        SQLStr = "SELECT * from " & lv_tbl
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLStr, MyConn, "", ErrMsg, UserData)
        Return dts
    End Function
    Public Function Show_Grid_JoinTable(ByVal nControl As DataGridView, ByVal lv_field As String, ByVal lv_tbl As String) As DataTable

        Dim SQLStr, ErrMsg As String
        Dim dts As DataTable

        SQLStr = "SELECT " & lv_field & " from " & lv_tbl
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLStr, MyConn, "", ErrMsg, UserData)
        Return dts
    End Function
    Public Function f_msgbox_successful(ByVal p_message As String) As Boolean
        var_msg = p_message & " Success"
        MsgBox(var_msg)
        Return True
    End Function
    Public Function f_msgbox_otorisasi(ByVal p_message As String) As Boolean
        var_msg = "You are not authorized for this module"
        MsgBox(var_msg, MsgBoxStyle.Critical, "No Authorization")
        Return True
    End Function
    Public Function f_msgbox_custom(ByVal p_message As String) As Boolean
        MsgBox("p_message")
        Return True
    End Function
    Public Function AmbilAngka(ByVal strFieldname As String, ByVal strTable As String, ByVal strCondition As String) As Decimal
        'strFieldname As String, strtable As String, Optional strcondition As String) As Variant
        Dim ErrMsg, SQLstr As String
        Dim MyReader As MySqlDataReader


        If strCondition = "" Then
            SQLstr = "SELECT " & strFieldname & " FROM " & strTable
        Else
            SQLstr = "SELECT " & strFieldname & " FROM " & strTable & " WHERE " & strCondition & ""
        End If
        ErrMsg = "Failed when read " & strTable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        Dim label1 As New Label
        label1.Text = 0
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    'AmbilData.Text = MyReader.GetString(" & strFieldname & ")
                    'AmbilData = MyReader.GetValue(" & strfieldname & ")
                    label1.Text = MyReader.GetDecimal(0)

                Catch ex As Exception
                    label1.Text = 0
                End Try
            End While

            If MyReader.HasRows = False Then
                label1.Text = 0
            End If

            CloseMyReader(MyReader, UserData)
        End If
        Return label1.Text

        'rsAD.Open(SQ, Conn, adOpenKeyset, adLockReadOnly)
        'If rsAD.RecordCount > 0 Then
        '    AmbilData = rsAD.Fields(0).Value
        'Else
        '    AmbilData = ""
        'End If
    End Function

    Public Function AmbilData(ByVal strFieldname As String, ByVal strTable As String, ByVal strCondition As String) As String
        'strFieldname As String, strtable As String, Optional strcondition As String) As Variant
        Dim ErrMsg, SQLstr As String
        Dim MyReader As MySqlDataReader


        If strCondition = "" Then
            SQLstr = "SELECT " & strFieldname & " FROM " & strTable
        Else
            SQLstr = "SELECT " & strFieldname & " FROM " & strTable & " WHERE " & strCondition & ""
        End If
        ErrMsg = "Failed when read " & strTable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        Dim label1 As New Label
        label1.Text = ""
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    'AmbilData.Text = MyReader.GetString(" & strFieldname & ")
                    'AmbilData = MyReader.GetValue(" & strfieldname & ")
                    label1.Text = MyReader.GetString(0)

                Catch ex As Exception
                    label1.Text = ""
                End Try
            End While

            If MyReader.HasRows = False Then
                label1.Text = ""
            End If

            CloseMyReader(MyReader, UserData)
        End If
        Return label1.Text

        'rsAD.Open(SQ, Conn, adOpenKeyset, adLockReadOnly)
        'If rsAD.RecordCount > 0 Then
        '    AmbilData = rsAD.Fields(0).Value
        'Else
        '    AmbilData = ""
        'End If
    End Function
    Public Sub closeForm(ByVal sender As System.Object, ByRef e As System.EventArgs, ByVal frm As Windows.Forms.Form)
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub
        frm.Close()
        frm.Dispose()
    End Sub

    Public Function GetMyConn(ByVal MyConn As MySqlConnection) As MySqlConnection
        MyConn = CekMyConn(MyConn, UserData.ConfigData)
        If MyConn Is Nothing Then
            FrmSettingConnection.ShowDialog()
            MyConn = FncMyConnection(UserData.ConfigData)
        End If
        Return MyConn
    End Function
    Public Function SpellDigit(ByVal strNumeric As Integer) As String
        Dim cRet As String
        On Error GoTo Pesan
        cRet = ""
        Select Case strNumeric
            Case 0 : cRet = " Zero"
            Case 1 : cRet = " One"
            Case 2 : cRet = " Two"
            Case 3 : cRet = " Three"
            Case 4 : cRet = " Four"
            Case 5 : cRet = " Five"
            Case 6 : cRet = " Six"
            Case 7 : cRet = " Seven"
            Case 8 : cRet = " Eight"
            Case 9 : cRet = " Nine"
            Case 10 : cRet = " Ten"
            Case 11 : cRet = " Eleven"
            Case 12 : cRet = " Twelve"
            Case 13 : cRet = " Thirteen"
            Case 14 : cRet = " Fourteen"
            Case 15 : cRet = " Fifteen"
            Case 16 : cRet = " Sixteen"
            Case 17 : cRet = " Seventeen"
            Case 18 : cRet = " Eighteen"
            Case 19 : cRet = " Nineteen"
            Case 20 : cRet = " Twenty"
            Case 30 : cRet = " Thirty"
            Case 40 : cRet = " Fourty"
            Case 50 : cRet = " Fifty"
            Case 60 : cRet = " Sixty"
            Case 70 : cRet = " Seventy"
            Case 80 : cRet = " Eighty"
            Case 90 : cRet = " Ninety"
            Case 100 : cRet = " One Hundred"
            Case 200 : cRet = " Two Hundred"
            Case 300 : cRet = " Three Hundred"
            Case 400 : cRet = " Four Hundred"
            Case 500 : cRet = " Five Hundred"
            Case 600 : cRet = " Six Hundred"
            Case 700 : cRet = " Seven Hundred"
            Case 800 : cRet = " Eight Hundred"
            Case 900 : cRet = " Nine Hundred"
        End Select
        SpellDigit = cRet
        Exit Function
Pesan:
        SpellDigit = "(maximum 12 digit)"
    End Function

    Private Function SpellUnit(ByVal strNumeric As Integer) As String
        Dim cRet As String
        Dim n100 As Integer
        Dim n10 As Integer
        Dim n1 As Integer
        On Error GoTo Pesan
        cRet = ""
        n100 = Int(strNumeric / 100) * 100
        n10 = Int((strNumeric - n100) / 10) * 10
        n1 = (strNumeric - n100 - n10)
        If n100 > 0 Then
            cRet = SpellDigit(n100)
        End If
        If n10 > 0 Then
            If n10 = 10 Then
                cRet = cRet & SpellDigit(n10 + n1)
            Else
                cRet = cRet & SpellDigit(n10)
            End If
        End If
        If n1 > 0 And n10 <> 10 Then
            cRet = cRet & SpellDigit(n1)
        End If
        SpellUnit = cRet
        Exit Function
Pesan:
        SpellUnit = "(maximum 12 digit)"
    End Function

    Public Function TerbilangInggris(ByVal strNumeric As String) As String
        Dim cRet As String
        Dim n1000000000 As Long
        Dim n1000000 As Long
        Dim n1000 As Long
        Dim n1 As Integer
        Dim n0 As Integer
        On Error GoTo Pesan
        Dim strValid As String, huruf As String, var_cent As String
        Dim i As Integer
        var_cent = ""
        'Periksa setiap karakter masukan
        strValid = "1234567890.,"
        For i% = 1 To Len(strNumeric)
            huruf = Chr(Asc(Mid(strNumeric, i%, 1)))
            If InStr(strValid, huruf) = 0 Then
                MsgBox("Should be Numeric!", _
                       vbCritical, "Character Not Valid")
                Exit Function
            End If
        Next i%

        If strNumeric = "" Then Exit Function
        'If Len(Trim(strNumeric)) > 9 Then GoTo Pesan
        If Len(Trim(strNumeric)) > 12 Then GoTo Pesan
        cRet = ""
        'n1000000 = Int(strNumeric / 1000000) * 1000000
        'n1000 = Int((strNumeric - n1000000) / 1000) * 1000
        'n1 = Int(strNumeric - n1000000 - n1000)
        'n0 = (strNumeric - n1000000 - n1000 - n1) * 100
        n1000000000 = Int(strNumeric / 1000000000) * 1000000000
        n1000000 = Int((strNumeric - n1000000000) / 1000000) * 1000000
        n1000 = Int((strNumeric - n1000000000 - n1000000) / 1000) * 1000
        n1 = Int(strNumeric - n1000000000 - n1000000 - n1000)
        n0 = (strNumeric - n1000000000 - n1000000 - n1000 - n1) * 100

        If n1000000000 > 0 Then
            cRet = SpellUnit(n1000000000 / 1000000000) & " Billion "
        End If
        If n1000000 > 0 Then
            cRet = cRet & SpellUnit(n1000000 / 1000000) & " Million"
        End If
        If n1000 > 0 Then
            cRet = cRet & SpellUnit(n1000 / 1000) & " Thousand"
        End If
        If n1 > 0 Then
            cRet = cRet & SpellUnit(n1)
        End If
        If n0 > 0 Then
            cRet = cRet & " and" & " cents" & SpellUnit(n0)
            var_cent = "Y"
        End If
        If var_cent <> "Y" Then
            TerbilangInggris = cRet & " Only "
        Else
            TerbilangInggris = cRet
        End If
        Exit Function
Pesan:
        TerbilangInggris = "(maximum 12 digit)"
    End Function
    Public Function f_SPLITNPWP(ByVal v_npwp As String, ByVal spasi As Integer) As String
        Dim v_split As String
        Dim v_length As Integer
        Dim v_npwp2 As String = ""
        Dim i As Integer

        'v_length = Len(v_npwp)
        For i = 1 To Len(Trim(v_npwp))
            If Mid(v_npwp, i, 1) = "." Or Mid(v_npwp, i, 1) = "-" Then
                Mid(v_npwp, i, 1) = " "
            End If
        Next
        v_npwp = Trim(v_npwp)
        v_npwp = v_npwp.Replace(" ", String.Empty)
        'split sesuai banyak space 
        'For i = 1 To Len(Trim(v_npwp))
        '    If spasi <> 5 Then
        '        If CBool(i = 2) Or CBool(i = 5) Or CBool(i = 8) Or CBool(i = 9) Or CBool(i = 12) Then
        '            v_npwp2 = v_npwp2 & Mid(v_npwp, i, 1) & Space(spasi + 2)
        '        Else
        '            v_npwp2 = v_npwp2 & Mid(v_npwp, i, 1) & Space(spasi)
        '        End If
        '    Else
        '        If CBool(i = 2) Or CBool(i = 8) Or CBool(i = 9) Or CBool(i = 12) Then
        '            v_npwp2 = v_npwp2 & Mid(v_npwp, i, 1) & Space(spasi + 2)
        '        Else

        '            v_npwp2 = v_npwp2 & Mid(v_npwp, i, 1) & Space(spasi)
        '        End If
        '    End If
        'Next

        If spasi = 5 Then
            '1234567890123456789012345678901234567890123456789012345
            '1    4      8    1    4        9    2    5        0      4      8    1      5      9    2
            '   4     6     4    4      8      4    4      8       6      6     4     6      6     4   
            v_npwp2 = v_npwp(0) & Space(4) & _
                      v_npwp(1) & Space(6) & _
                      v_npwp(2) & Space(4) & _
                      v_npwp(3) & Space(4) & _
                      v_npwp(4) & Space(8) & _
                      v_npwp(5) & Space(4) & _
                      v_npwp(6) & Space(4) & _
                      v_npwp(7) & Space(8) & _
                      v_npwp(8) & Space(6) & _
                      v_npwp(9) & Space(6) & _
                      v_npwp(10) & Space(4) & _
                      v_npwp(11) & Space(6) & _
                      v_npwp(12) & Space(6) & _
                      v_npwp(13) & Space(4) & _
                      v_npwp(14)
        Else
            '12345678901234567890123456789012345678901234567890
            '1  3      7  9    2      6  8    1      5    8    1  3      7    0  2
            '  2    6    2   4     6    2   4     6     4    4   2    6     4   2       
            v_npwp2 = v_npwp(0) & Space(2) & _
                      v_npwp(1) & Space(6) & _
                      v_npwp(2) & Space(2) & _
                      v_npwp(3) & Space(4) & _
                      v_npwp(4) & Space(6) & _
                      v_npwp(5) & Space(2) & _
                      v_npwp(6) & Space(4) & _
                      v_npwp(7) & Space(6) & _
                      v_npwp(8) & Space(4) & _
                      v_npwp(9) & Space(4) & _
                      v_npwp(10) & Space(2) & _
                      v_npwp(11) & Space(6) & _
                      v_npwp(12) & Space(4) & _
                      v_npwp(13) & Space(2) & _
                      v_npwp(14)
        End If
        f_SPLITNPWP = LTrim(v_npwp2)
    End Function
    Public Function f_SPLITSpace(ByVal v_string As String, ByVal spasi As Integer) As String
        Dim v_split As String
        Dim v_length As Integer
        Dim v_string2 As String = ""
        Dim i As Integer

        'v_length = Len(v_npwp)
        For i = 1 To Len(Trim(v_string))
            If Mid(v_string, i, 1) = "." Or Mid(v_string, i, 1) = "-" Then
                Mid(v_string, i, 1) = " "

            End If
        Next
        v_string = Trim(v_string)
        v_string = v_string.Replace(" ", String.Empty)
        'split sesuai banyak space 
        For i = 1 To Len(Trim(v_string))
            v_string2 = v_string2 & Mid(v_string, i, 1) & Space(spasi)
        Next

        f_SPLITSpace = LTrim(v_string2)
    End Function

    Function GetServerDate() As Date
        'Dim temp As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function

    Public Function SpellDigitAll(ByVal lang As String, ByVal strNumeric As Integer) As String
        Dim cRet As String
        On Error GoTo Pesan
        cRet = ""
        Select Case strNumeric
            Case 0
                If lang = "EN" Then
                    cRet = " Zero"
                Else
                    cRet = " Nol"
                End If
            Case 1
                If lang = "EN" Then
                    cRet = " One"
                Else
                    cRet = " Satu"
                End If
            Case 2
                If lang = "EN" Then
                    cRet = " Two"
                Else
                    cRet = " Dua"
                End If
            Case 3
                If lang = "EN" Then
                    cRet = " Three"
                Else
                    cRet = " Tiga"
                End If
            Case 4
                If lang = "EN" Then
                    cRet = " Four"
                Else
                    cRet = " Empat"
                End If
            Case 5
                If lang = "EN" Then
                    cRet = " Five"
                Else
                    cRet = " Lima"
                End If
            Case 6
                If lang = "EN" Then
                    cRet = " Six"
                Else
                    cRet = " Enam"
                End If
            Case 7
                If lang = "EN" Then
                    cRet = " Seven"
                Else
                    cRet = " Tujuh"
                End If
            Case 8
                If lang = "EN" Then
                    cRet = " Eight"
                Else
                    cRet = " Delapan"
                End If
            Case 9
                If lang = "EN" Then
                    cRet = " Nine"
                Else
                    cRet = " Sembilan"
                End If
            Case 10
                If lang = "EN" Then
                    cRet = " Ten"
                Else
                    cRet = " Sepuluh"
                End If
            Case 11
                If lang = "EN" Then
                    cRet = " Eleven"
                Else
                    cRet = " Sebelas"
                End If
            Case 12
                If lang = "EN" Then
                    cRet = " Twelve"
                Else
                    cRet = " Dua Belas"
                End If
            Case 13
                If lang = "EN" Then                
                    cRet = " Thirteen"
                Else
                    cRet = " Tiga Belas"
                End If
            Case 14
                If lang = "EN" Then
                    cRet = " Fourteen"
                Else
                    cRet = " Empat Belas"
                End If
            Case 15
                If lang = "EN" Then
                    cRet = " Fifteen"
                Else
                    cRet = " Lima Belas"
                End If
            Case 16
                If lang = "EN" Then
                    cRet = " Sixteen"
                Else
                    cRet = " Enam Belas"
                End If
            Case 17
                If lang = "EN" Then
                    cRet = " Seventeen"
                Else
                    cRet = " Tujuh Belas"
                End If
            Case 18
                If lang = "EN" Then
                    cRet = " Eighteen"
                Else
                    cRet = " Delapan Belas"
                End If
            Case 19
                If lang = "EN" Then
                    cRet = " Ninetieen"
                Else
                    cRet = " Sembilan Belas"
                End If
            Case 20
                If lang = "EN" Then
                    cRet = " Twenty"
                Else
                    cRet = " Dua Puluh"
                End If
            Case 30
                If lang = "EN" Then
                    cRet = " Thirty"
                Else
                    cRet = " Tiga Puluh"
                End If
            Case 40
                If lang = "EN" Then
                    cRet = " Fourthy"
                Else
                    cRet = " Empat Puluh"
                End If
            Case 50
                If lang = "EN" Then
                    cRet = " Fifty"
                Else
                    cRet = " Lima Puluh"
                End If
            Case 60
                If lang = "EN" Then
                    cRet = " Sixty"
                Else
                    cRet = " Enam Puluh"
                End If
            Case 70
                If lang = "EN" Then
                    cRet = " Seventy"
                Else
                    cRet = " Tujuh Puluh"
                End If
            Case 80
                If lang = "EN" Then
                    cRet = " Eighty"
                Else
                    cRet = " Delapan Puluh"
                End If
            Case 90
                If lang = "EN" Then
                    cRet = " Ninety"
                Else
                    cRet = " Sembilan Puluh"
                End If
            Case 100
                If lang = "EN" Then
                    cRet = " One Hundred"
                Else
                    cRet = " Seratus"
                End If
            Case 200
                If lang = "EN" Then
                    cRet = " Two Hundred"
                Else
                    cRet = " Dua Ratus"
                End If
            Case 300
                If lang = "EN" Then
                    cRet = " Three Hundred"
                Else
                    cRet = " Tiga Ratus"
                End If
            Case 400
                If lang = "EN" Then
                    cRet = " Four Hundred"
                Else
                    cRet = " Empat Ratus"
                End If
            Case 500
                If lang = "EN" Then
                    cRet = " Five Hundred"
                Else
                    cRet = " Lima Ratus"
                End If
            Case 600
                If lang = "EN" Then
                    cRet = " Six Hundred"
                Else
                    cRet = " Enam Ratus"
                End If
            Case 700
                If lang = "EN" Then
                    cRet = " Seven Hundred"
                Else
                    cRet = " Tujuh Ratus"
                End If
            Case 800
                If lang = "EN" Then
                    cRet = " Eight Hundred"
                Else
                    cRet = " Delapan Ratus"
                End If
            Case 900
                If lang = "EN" Then
                    cRet = " Nine Hundred"
                Else
                    cRet = " Sembilan Ratus"
                End If
        End Select
        SpellDigitAll = cRet
        Exit Function
Pesan:
        SpellDigitAll = "(maximum 12 digit)"
    End Function

    Private Function SpellUnitAll(ByVal lang As String, ByVal strNumeric As Integer) As String
        Dim cRet As String
        Dim n100 As Integer
        Dim n10 As Integer
        Dim n1 As Integer
        On Error GoTo Pesan
        cRet = ""
        n100 = Int(strNumeric / 100) * 100
        n10 = Int((strNumeric - n100) / 10) * 10
        n1 = (strNumeric - n100 - n10)
        If n100 > 0 Then
            cRet = SpellDigitAll(lang, n100)
        End If
        If n10 > 0 Then
            If n10 = 10 Then
                cRet = cRet & SpellDigitAll(lang, n10 + n1)
            Else
                cRet = cRet & SpellDigitAll(lang, n10)
            End If
        End If
        If n1 > 0 And n10 <> 10 Then
            cRet = cRet & SpellDigitAll(lang, n1)
        End If
        SpellUnitAll = cRet
        Exit Function
Pesan:
        SpellUnitAll = "(maximum 12 digit)"
    End Function

    Public Function TerbilangAll(ByVal lang As String, ByVal strNumeric As String) As String
        Dim cRet As String
        Dim n1000000000 As Long
        Dim n1000000 As Long
        Dim n1000 As Long
        Dim n1 As Integer
        Dim n0 As Integer
        On Error GoTo Pesan
        Dim strValid As String, huruf As String
        Dim i As Integer
        'Periksa setiap karakter masukan
        strValid = "1234567890.,"
        For i% = 1 To Len(strNumeric)
            huruf = Chr(Asc(Mid(strNumeric, i%, 1)))
            If InStr(strValid, huruf) = 0 Then
                MsgBox("Should be Numeric!", _
                       vbCritical, "Character Not Valid")
                Exit Function
            End If
        Next i%

        If strNumeric = "" Then Exit Function
        If Len(Trim(strNumeric)) > 12 Then GoTo Pesan

        cRet = ""
        n1000000000 = Int(strNumeric / 1000000000) * 1000000000
        n1000000 = Int((strNumeric - n1000000000) / 1000000) * 1000000
        n1000 = Int((strNumeric - n1000000000 - n1000000) / 1000) * 1000
        n1 = Int(strNumeric - n1000000000 - n1000000 - n1000)
        n0 = (strNumeric - n1000000000 - n1000000 - n1000 - n1) * 100

        'n1000000 = Int(strNumeric / 1000000) * 1000000
        'n1000 = Int((strNumeric - n1000000) / 1000) * 1000
        'n1 = Int(strNumeric - n1000000 - n1000)
        'n0 = (strNumeric - n1000000 - n1000 - n1) * 100
        If n1000000000 > 0 Then
            If lang = "EN" Then
                cRet = SpellUnitAll(lang, (n1000000000 / 1000000000)) & " Billion "
            Else
                cRet = SpellUnitAll(lang, (n1000000000 / 1000000000)) & " Milyar "
            End If
        End If
        If n1000000 > 0 Then
            If lang = "EN" Then
                cRet = cRet & SpellUnitAll(lang, (n1000000 / 1000000)) & " Million"
            Else
                cRet = cRet & SpellUnitAll(lang, (n1000000 / 1000000)) & " Juta"
            End If
        End If
        If n1000 > 0 Then
            If lang = "EN" Then
                cRet = cRet & SpellUnitAll(lang, (n1000 / 1000)) & " Thousand"
            Else
                cRet = cRet & SpellUnitAll(lang, (n1000 / 1000)) & " Ribu"
            End If
        End If
        If n1 > 0 Then
            cRet = cRet & SpellUnitAll(lang, n1)
        End If
        If n0 > 0 Then
            If lang = "EN" Then
                cRet = cRet & " and cents" & SpellUnitAll(lang, n0)
            Else
                cRet = cRet & " Rupiah koma" & SpellUnitAll(lang, n0) & " sen"
            End If
        End If
        If lang = "EN" Then
            TerbilangAll = cRet & " Only "
        Else
            If n0 > 0 Then
                TerbilangAll = cRet
            Else
                TerbilangAll = cRet & " Rupiah"
            End If
        End If
        Exit Function
Pesan:
        TerbilangAll = "(maximum 12 digit)"
    End Function
    Public Function f_getread(ByVal v_status As String) As Boolean
        f_getread = Not ((v_status = "Open") Or (v_status = "Approved"))
    End Function
    Public Function f_getenable(ByVal v_status As String) As Boolean
        f_getenable = (v_status = "Open") Or (v_status = "Approved")
    End Function
End Module
