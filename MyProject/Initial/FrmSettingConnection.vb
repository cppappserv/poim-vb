Imports BPConnection.EncryptionUtility

Public Class FrmSettingConnection
    Dim ConnStr As String
    Dim DataSetting(3) As String
    Dim MyConn As MySqlConnection

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles BtnSimpan.Click

        If ValidasiInput() Then
            DataSetting(0) = Me.TxtServer.Text
            DataSetting(1) = Me.TxtDB.Text
            DataSetting(2) = Me.TxtUser.Text
            DataSetting(3) = Me.TxtPassword.Text

            'Dim WriteKoneksi As New WriteConnection(DataSetting)
            WriteEncryption(DataSetting, "Registry")

            ConnStr = FncMyConnStr(DataSetting)

            'MyConn = FncMyConnection(ConnStr)
            MyConn = New MySqlConnection(ConnStr)
            MyConn.Open()
            If Not MyConn Is Nothing Then
                DbHost = MyConn.DataSource
                DbName = MyConn.Database
                Me.Close()
                Exit Sub
            End If
            CloseMyConn(MyConn)
        End If

    End Sub

    Private Sub BtnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTest.Click
        If ValidasiInput() Then
            DataSetting(0) = Me.TxtServer.Text
            DataSetting(1) = Me.TxtDB.Text
            DataSetting(2) = Me.TxtUser.Text
            DataSetting(3) = Me.TxtPassword.Text

            ConnStr = FncMyConnStr(DataSetting)
            MyConn = New MySqlConnection(ConnStr)
            MyConn.Open()
            'MyConn = FncMyConnection(ConnStr)
            If MyConn.State.ToString = "Open" Then MsgBox("Connection Succeed", MsgBoxStyle.OkOnly, "Information")
            CloseMyConn(MyConn)
        End If
    End Sub

    Private Sub FrmSetConnection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ConnValue() As String
        ConnValue = FncMyConnValue(UserData.ConfigData)
        TxtServer.Text = ConnValue(0)
        TxtDB.Text = ConnValue(1)
        TxtUser.Text = ConnValue(2)
        TxtPassword.Text = ConnValue(3)
    End Sub

    Private Function ValidasiInput() As Boolean
        If ChkEmptyValue(TxtServer, Me.TxtServer.Text, _
                         "Nama server/IP harus diisi.", "Kesalahan Isian Nama Server/IP") Then Return False
        If ChkEmptyValue(TxtDB, Me.TxtDB.Text, _
                         "Nama database harus diisi.", "Kesalahan Isian Nama Database") Then Return False
        If ChkEmptyValue(TxtUser, Me.TxtUser.Text, _
                         "Nama user harus diisi.", "Kesalahan Isian Nama User") Then Return False
        If ChkEmptyValue(TxtPassword, Me.TxtPassword.Text, _
                         "Password harus diisi.", "Kesalahan Isian Password") Then Return False
        Return True
    End Function

End Class