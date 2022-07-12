Imports BPConnection.EncryptionUtility
Public Class FRMLogin
    'Dim MyConn As MySqlConnection

    'Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
    '    HaveAccess = True
    '    Me.Close()
    'End Sub

    'Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
    '    Me.Close()
    'End Sub



    'Dim MyConn As MySqlConnection
    Dim MyReader As MySqlDataReader
    Dim SQLStr, ErrMsg As String
    Dim Paswdtext As String
    Dim TotRow, Cnt As Integer
    Dim LastUser As String

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        If LastUser = "" Or LastUser <> TxtUSER_ID.Text Then
            Cnt = 1
            LastUser = TxtUSER_ID.Text
        Else
            Cnt = Cnt + 1
        End If

        If Cnt < 4 Then
            ValidasiLogin()
            If HaveAccess = True Then
                Me.Close()
                Exit Sub
            Else
                If Cnt = 3 Then
                    TotRow = BlockAccess(LastUser)
                    If TotRow = 1 Then MsgBox("User id " & LastUser & " blocked!", MsgBoxStyle.Exclamation, "Info")
                    Me.Close()
                    Exit Sub
                End If
                Me.TxtPassword.Focus()
            End If
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub FrmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'MyConn = FncMyConnection()
        'If MyConn Is Nothing Then
        '    FrmSettingConnection.ShowDialog()
        '    MyConn = FncMyConnection()
        '    If MyConn Is Nothing Then
        '        Me.Close()
        '        Exit Sub
        '    End If
        'End If
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If

        'SQLStr = "SELECT user_id FROM tbm_users"
        'ErrMsg = "Failed accessing users data."
        'UserData.UserId = "admin"
        'MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
        'If Not MyReader Is Nothing Then
        '    If MyReader.HasRows = False Then
        '        CloseMyReader(MyReader, UserData)
        '        If InsertUserAdmin() < 0 Then Exit Sub
        '    Else
        '        CloseMyReader(MyReader, UserData)
        '    End If
        'Else
        'CloseMyReader(MyReader, UserData)
        'SQLStr = "CREATE TABLE tbm_users ( " & _
        '                "user_id varchar(10) NOT NULL COMMENT 'Kode User', " & _
        '                "user_name varchar(30) NOT NULL COMMENT 'Nama user', " & _
        '                "password text NOT NULL, " & _
        '                "last_login datetime default NULL, " & _
        '                "admin char(1) NOT NULL default 'N' COMMENT 'setting akses untuk admin', " & _
        '                "debug_msg char(1) NOT NULL default 'N', " & _
        '                "blocked char(1) NOT NULL default 'N', " & _
        '            "PRIMARY KEY  (user_id) " & _
        '         ") ENGINE=InnoDB DEFAULT CHARSET=latin1"
        'ErrMsg = "Gagal saat membuat tabel kode user."
        'If DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData) < 0 Then Exit Sub

        'If InsertUserAdmin() < 0 Then Exit Sub
        'End If
    End Sub

    Private Sub ValidasiLogin()
        ' encrypt password
        Paswdtext = GetEncryption(TxtPassword.Text)
        UserData.UserId = "admin"
        ' cek validasi data
        SQLStr = "SELECT * " & _
                 "FROM tbm_users " & _
                 "WHERE user_id='" & TxtUSER_ID.Text & "' AND password='" & Paswdtext & "' " & _
                 "AND blocked='N'"

        ErrMsg = "Failed when reading user data"
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
        HaveAccess = False
        ' cek isi data
        If Not MyReader Is Nothing Then
            If MyReader.HasRows = False Then
                MsgBox("Login Tidak Berhasil, Cek Kode user & Password.", MsgBoxStyle.Exclamation, "Info")
                TxtPassword.SelectAll()
            Else
                'define inisialisasi umum
                Try
                    MyReader.Read()
                    UserData.UserCT = CInt(MyReader.GetString("user_ct"))
                    UserData.UserId = MyReader.GetString("user_id")
                    UserData.UserName = MyReader.GetString("name")
                    UserData.OtorisasiComp = "company_code in (select company_code from tbm_users_company " & _
                                             "where user_ct = '" & UserData.UserCT & "')"
                    'If MyReader.GetString("admin") = "Y" Then
                    '    'AdmAccess = True
                    '    UserData.AdmAccess = True
                    'Else
                    '    'AdmAccess = False
                    '    UserData.AdmAccess = False
                    'End If
                    'If MyReader.GetString("debug_msg") = "Y" Then
                    '    'DbgAccess = True
                    '    UserData.DbgAccess = True
                    'Else
                    '    'DbgAccess = False
                    '    UserData.DbgAccess = False
                    'End If
                    HaveAccess = True
                    CloseMyReader(MyReader, UserData)
                    'UpdateLastLogin(UserData.UserId)

                    ' close login form
                    CloseMyConn(MyConn)
                    Me.Close()
                Catch ex As Exception
                    MsgBox("Gagal membaca informasi data user.", MsgBoxStyle.Exclamation, "Info")
                End Try

            End If
        End If
        MyReader.Close()
    End Sub

    Private Function InsertUserAdmin() As Integer
        'Paswdtext = GetEncryption("info1")
        'UserData.UserId = "admin"
        'SQLStr = "INSERT INTO ms_user" & _
        '                "(user_id, user_name, password, admin, debug_msg) " & _
        '         "VALUES ('admin', 'Administrator', '" & Paswdtext & "', 'Y', 'Y')"
        'ErrMsg = "Gagal memasukan default data admin ke tabel user."
        'TotRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
        'Return TotRow
    End Function

    Private Function BlockAccess(ByVal UserId As String) As Integer
        UserData.UserId = "admin"
        SQLStr = "UPDATE tbm_users " & _
                 "SET blocked='Y' " & _
                 "WHERE user_id='" & UserId & "'"
        ErrMsg = "Gagal saat block user."
        TotRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
        Return TotRow
    End Function

    Private Sub UpdateLastLogin(ByVal UserId As String)
        'SQLStr = "UPDATE ms_user " & _
        '         "SET last_login='" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "' " & _
        '         "WHERE user_id='" & UserId & "'"
        'ErrMsg = "Gagal update last login time."
        'TotRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
    End Sub


End Class
