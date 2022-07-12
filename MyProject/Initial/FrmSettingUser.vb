Imports System.Text.RegularExpressions
Imports BPConnection.EncryptionUtility

Public Class FrmSettingUser
    
    Dim KodeUser, NamaUser, PaswdUser As String
    Dim FlagAccAdmin, FlagDisplayErr, FlagBlockUser As String
    Dim MyConn As MySqlConnection
    Dim MyReader As MySqlDataReader
    Dim SQLStr, ErrMsg As String
    Dim TotRow As Integer
    Dim ResetPass As Boolean
    Dim PassCodeCheck As Boolean = False

    Private Sub FrmSettingUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' open connection
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

        Me.BtnCari.Hide()
        Me.BtnBatal.Hide()
        If UserData.AdmAccess = True Then
            Me.BtnUserBaru.Show()
            Me.BtnModifikasi.Show()

            Me.BtnSetPassword.Hide()
            Me.BtnSimpan.Hide()
            Me.LblPassBaru1.Hide()
            Me.LblPassBaru2.Hide()
            Me.TxtPassBaru1.Hide()
            Me.TxtPassBaru2.Hide()
            Me.BtnSimpan.Hide()
            Me.BtnSetPassword.Hide()
            ToogleControl(False)

            Me.GrpSetting.Location = New System.Drawing.Point(22, 103)
            Me.BtnUserBaru.Location = New System.Drawing.Point(130, 190)
            Me.BtnModifikasi.Location = New System.Drawing.Point(210, 190)
            Me.BtnSimpan.Location = New System.Drawing.Point(130, 190)
            Me.BtnBatal.Location = New System.Drawing.Point(210, 190)
            Me.Size = New System.Drawing.Size(408, 285)

            Me.BtnUserBaru.Focus()
        Else
            Me.GrpSetting.Hide()
            Me.LblPassword.Text = "Password Lama"
            Me.TxtKodeUser.Text = UserData.UserId
            Me.TxtKodeUser.Enabled = False
            Me.TxtPassword.Enabled = False
            Me.BtnModifikasi.Hide()
            Me.BtnUserBaru.Hide()

            Me.BtnSimpan.Location = New System.Drawing.Point(220, 160)
            Me.BtnSetPassword.Location = New System.Drawing.Point(110, 160)
            Me.Size = New System.Drawing.Size(408, 255)

            KodeUser = UserData.UserId
            CariData()
            Me.BtnSetPassword.Focus()
        End If
    End Sub

    Private Sub FrmSettingUser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PassCodeCheck = True
        CloseMyConn(MyConn)
    End Sub

    Private Sub TxtKodeUser_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKodeUser.GotFocus
        StsKeterangan.Text = "Masukkan Kode User Maksimal 10 Karakter"
    End Sub

    Private Sub TxtNamaUser_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNamaUser.MouseClick
        StsKeterangan.Text = "Masukkan Nama User Maksimal 30 Karakter"
    End Sub

    Private Sub TxtPassword_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPassword.MouseClick
        StsKeterangan.Text = "Masukkan Password Yang Baru"
    End Sub

    Private Sub TxtPassBaru1_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPassBaru1.MouseClick
        StsKeterangan.Text = "Masukkan Password Yang Baru"
    End Sub

    Private Sub TxtPassBaru2_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPassBaru2.MouseClick
        StsKeterangan.Text = "Ketik Ulang Password Seperti Password Baru 2"
    End Sub

    Private Sub ChkDbgMsg_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDbgMsg.MouseClick
        StsKeterangan.Text = "Aktifkan / Non Aktifkan Detail Debug Error"
    End Sub

    Private Sub ChkAccAdmin_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAccAdmin.MouseClick
        StsKeterangan.Text = "Aktifkan / Non Aktifkan Status Admin"
    End Sub

    Private Sub ChkBlock_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBlock.MouseClick
        StsKeterangan.Text = "Aktifkan / Non Aktifkan Akses User"
    End Sub

    Private Sub BtnUserBaru_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUserBaru.MouseHover
        StsKeterangan.Text = "Buat Kode User Baru"
    End Sub

    Private Sub BtnModifikasi_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnModifikasi.MouseHover
        StsKeterangan.Text = "Edit Kode User Yang Sudah Ada"
    End Sub

    Private Sub TxtKodeUser_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKodeUser.LostFocus
        If Not PassCodeCheck Then
            KodeUser = TxtKodeUser.Text
            If KodeUser <> "" Then
                If Not Regex.IsMatch(KodeUser, "^[0-9A-Za-z]*$") Then
                    MsgBox("Hanya boleh menggunakan angka dan huruf", MsgBoxStyle.Exclamation, "Error")
                    TxtKodeUser.Focus()
                    Exit Sub
                End If
                CariData()

            End If
        End If
    End Sub

    Private Sub BtnModifikasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModifikasi.Click
        Me.BtnUserBaru.Hide()
        Me.BtnModifikasi.Hide()

        Me.BtnSetPassword.Location = New System.Drawing.Point(83, 190)
        Me.BtnSimpan.Location = New System.Drawing.Point(190, 190)
        Me.BtnBatal.Location = New System.Drawing.Point(260, 190)

        Me.BtnCari.Show()
        Me.BtnSetPassword.Show()
        Me.BtnSimpan.Show()
        Me.BtnBatal.Show()

        ToogleControl(True)
        TxtPassword.Enabled = False
        BtnSetPassword.Text = "&Reset Password"
        BtnSimpan.Tag = "UserEdit"
        TxtKodeUser.Focus()
    End Sub

    Private Sub BtnUserBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserBaru.Click
        Me.BtnUserBaru.Hide()
        Me.BtnModifikasi.Hide()

        Me.BtnSimpan.Show()
        Me.BtnBatal.Show()

        ToogleControl(True)
        BtnSimpan.Tag = "UserBaru"
        TxtKodeUser.Focus()
    End Sub

    Private Sub BtnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        Me.BtnUserBaru.Show()
        Me.BtnModifikasi.Show()

        Me.BtnCari.Hide()
        Me.BtnBatal.Hide()
        Me.BtnSimpan.Hide()

        ToogleControl(False)

        If BtnSimpan.Tag.Equals("UserEdit") Then
            Me.BtnSetPassword.Hide()
            ResetPass = False
            Me.BtnSimpan.Location = New System.Drawing.Point(130, 190)
            Me.BtnBatal.Location = New System.Drawing.Point(210, 190)
        End If
        BtnSimpan.Tag = ""

    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If CekInputan() = False Then Exit Sub

        If BtnSimpan.Tag.Equals("UserBaru") Then
            ' set query untuk create user baru
            PaswdUser = GetEncryption(TxtPassword.Text)
            SQLStr = "INSERT INTO ms_user " & _
                        "(user_id, user_name, password, admin, debug_msg, blocked) " & _
                     "VALUES ('" & KodeUser & "', '" & NamaUser & "', '" & PaswdUser & "', " & _
                             "'" & FlagAccAdmin & "', '" & FlagDisplayErr & "', '" & FlagBlockUser & "')"
        Else
            If UserData.AdmAccess Then
                If ResetPass Then
                    ' set query update kode user untuk access admin saat reset password
                    PaswdUser = GetEncryption(TxtPassword.Text)
                    SQLStr = "UPDATE ms_user " & _
                             "SET user_name='" & NamaUser & "', password='" & PaswdUser & "', " & _
                                 "admin='" & FlagAccAdmin & "', debug_msg='" & FlagDisplayErr & "', " & _
                                 "blocked='" & FlagBlockUser & "' " & _
                             "WHERE user_id='" & KodeUser & "'"
                Else
                    ' set query update kode user untuk access admin saat tanpa reset password
                    SQLStr = "UPDATE ms_user " & _
                             "SET user_name='" & NamaUser & "', admin='" & FlagAccAdmin & "', " & _
                                 "debug_msg='" & FlagDisplayErr & "', blocked='" & FlagBlockUser & "' " & _
                             "WHERE user_id='" & KodeUser & "'"
                End If

            Else
                If ResetPass Then
                    If CekPass() Then
                        SQLStr = "UPDATE ms_user " & _
                                 "SET user_name='" & NamaUser & "', password='" & PaswdUser & "' " & _
                                 "WHERE user_id='" & UserData.UserId & "'"
                    Else
                        Exit Sub
                    End If
                Else
                    SQLStr = "UPDATE ms_user " & _
                             "SET user_name='" & NamaUser & "' " & _
                             "WHERE user_id='" & UserData.UserId & "'"
                End If

            End If
        End If
        ErrMsg = "Gagal simpan data kode user."
        TotRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
        If TotRow < 0 Then Exit Sub
        Me.Close()
    End Sub

    Private Sub BtnSetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSetPassword.Click
        TxtPassword.Enabled = True
        TxtPassword.Text = ""
        TxtPassword.Focus()
        ResetPass = True
        If UserData.AdmAccess = False Then
            TxtPassBaru1.Enabled = True
            TxtPassBaru2.Enabled = True
        End If
    End Sub

    Private Sub ToogleControl(ByVal Status As Boolean)
        Me.TxtKodeUser.Enabled = Status
        Me.TxtNamaUser.Enabled = Status
        Me.TxtPassword.Enabled = Status
        Me.ChkAccAdmin.Enabled = Status
        Me.ChkDbgMsg.Enabled = Status
        Me.ChkBlock.Enabled = Status
        If Status = False Then
            Me.TxtKodeUser.Text = ""
            Me.TxtNamaUser.Text = ""
            Me.TxtPassword.Text = ""
            Me.ChkAccAdmin.Checked = False
            Me.ChkDbgMsg.Checked = False
            Me.ChkBlock.Checked = False
        End If
    End Sub

    Private Function CekInputan() As Boolean

        KodeUser = TxtKodeUser.Text
        NamaUser = TxtNamaUser.Text
        If ChkEmptyValue(TxtKodeUser, KodeUser, _
                 "Kode user harus diisi.", "Kesalahan Isian Kode User") Then Return False
        If ChkEmptyValue(TxtNamaUser, NamaUser, _
                 "Nama user harus diisi.", "Kesalahan Isian Nama User") Then Return False
        If ChkEmptyValue(TxtPassword, TxtPassword.Text, _
                 "Password user harus diisi.", "Kesalahan Isian Password") Then Return False

        If ChkAccAdmin.Checked Then FlagAccAdmin = "Y" Else FlagAccAdmin = "N"
        If ChkDbgMsg.Checked Then FlagDisplayErr = "Y" Else FlagDisplayErr = "N"
        If ChkBlock.Checked Then FlagBlockUser = "Y" Else FlagBlockUser = "N"
        Return True
    End Function

    Private Sub BtnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCari.Click
        Dim PilihanDlg As New DlgPilihan
        PilihanDlg.Text = "Pilih Kode User"
        PilihanDlg.LblKey1.Text = "Kode User"
        PilihanDlg.SQLGrid = "SELECT user_id AS 'Kode User', user_name AS 'Nama User' " & _
                             "FROM ms_user ORDER BY user_id ASC"
        PilihanDlg.SQLFilter = "SELECT user_id AS 'Kode User', user_name AS 'Nama User' " & _
                               "FROM ms_user WHERE user_id LIKE 'FilterData1%' ORDER BY user_id ASC"
        PilihanDlg.LblKey2.Hide()
        PilihanDlg.TxtKey2.Hide()
        PilihanDlg.DgvResult.Location = New System.Drawing.Point(12, 39)
        PilihanDlg.TblLayoutPanel.Location = New System.Drawing.Point(236, 240)
        PilihanDlg.Size = New System.Drawing.Size(400, 278)
        PilihanDlg.Tables = "ms_user"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtKodeUser.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            KodeUser = TxtKodeUser.Text
            CariData()
        End If
    End Sub

    Private Sub CariData()
        ' cek kode user
        ErrMsg = "Gagal membaca tabel user."
        SQLStr = "SELECT * " & _
                 "FROM ms_user " & _
                 "WHERE user_id='" & KodeUser & "'"
        ErrMsg = "Gagal membaca tabel user."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            If MyReader.HasRows Then
                MyReader.Read()
                If UserData.AdmAccess Then
                    If BtnSimpan.Tag.Equals("UserEdit") Then
                        TxtKodeUser.Enabled = False
                        TxtNamaUser.Text = MyReader.GetString("user_name")
                        TxtPassword.Text = "******"
                        If MyReader.GetString("admin") = "Y" Then
                            ChkAccAdmin.Checked = True
                        Else
                            ChkAccAdmin.Checked = False
                        End If
                        If MyReader.GetString("debug_msg") = "Y" Then
                            ChkDbgMsg.Checked = True
                        Else
                            ChkDbgMsg.Checked = False
                        End If
                        If MyReader.GetString("blocked") = "Y" Then
                            ChkBlock.Checked = True
                        Else
                            ChkBlock.Checked = False
                        End If

                    Else
                        MsgBox("Kode User telah ada, silahkan coba kode yang berbeda.", MsgBoxStyle.Exclamation)
                        TxtKodeUser.Focus()
                    End If

                Else
                    TxtPassword.Text = "******"
                    TxtNamaUser.Text = MyReader.GetString("user_name")
                End If

            Else

                If BtnSimpan.Tag.Equals("UserEdit") Then
                    MsgBox("Kode User tidak ada.", MsgBoxStyle.Exclamation)
                    TxtKodeUser.Focus()

                End If
            End If
        Else
            ' problem baca data
            If UserData.AdmAccess Then
                MsgBox("Cek Inputan Kode User.", MsgBoxStyle.Exclamation)
                TxtKodeUser.Focus()
            Else
                MsgBox("Ada masalah dalam data user anda.", MsgBoxStyle.Exclamation)
                BtnSetPassword.Focus()
            End If

        End If
        CloseMyReader(MyReader, UserData)
    End Sub

    Private Function CekPass() As Boolean
        If TxtPassBaru1.Text <> TxtPassBaru2.Text Then
            MsgBox("Password Baru 1 & Password Baru 2 Beda.", MsgBoxStyle.Exclamation)
            Return False
        End If
        PaswdUser = GetEncryption(TxtPassword.Text)
        ' cek validasi data
        SQLStr = "SELECT user_id " & _
                 "FROM ms_user " & _
                 "WHERE user_id='" & KodeUser & "' AND password='" & PaswdUser & "'"
        ErrMsg = "Gagal mencek password lama."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            If MyReader.HasRows Then
                CloseMyReader(MyReader, UserData)
                PaswdUser = GetEncryption(TxtPassBaru1.Text)
                Return True
            End If
        End If
        CloseMyReader(MyReader, UserData)
        MsgBox("Password Lama Salah.", MsgBoxStyle.Exclamation)
        Return False
    End Function

    Private Sub TxtKodeUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKodeUser.TextChanged

    End Sub
End Class