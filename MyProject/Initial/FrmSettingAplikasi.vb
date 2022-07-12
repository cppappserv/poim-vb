Public Class FrmSettingAplikasi
    Dim MyConn As MySqlConnection
    Dim SQLStr, ErrMsg As String

    Private Sub FrmSettingAplikasi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If UserData.AdmAccess Then
            If UserData.TrapErr Then ChkTrapErr.Checked = True
            If UserData.SaveAudittrail Then ChkLogTrans.Checked = True
        Else
            TabCtrlSetting.Controls.Remove(TabAdmin)
        End If

    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim ChkValue As String
        Dim TotRow As Integer

        If UserData.AdmAccess Then
            If UserData.TrapErr <> ChkTrapErr.Checked Or _
            UserData.SaveAudittrail <> ChkLogTrans.Checked Then
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

                ' update trap error
                If UserData.TrapErr <> ChkTrapErr.Checked Then
                    If ChkTrapErr.Checked Then ChkValue = "Y" Else ChkValue = "N"
                    SQLStr = "SELECT ConfValue FROM ot_config " & _
                             "WHERE ConfName='TrapError'"
                    ErrMsg = "Gagal baca data config."
                    If DBQueryGetTotalRows(SQLStr, MyConn, ErrMsg, False, UserData) > 0 Then
                        SQLStr = "UPDATE ot_Config SET ConfValue='" & ChkValue & "', User_id='" & UserData.UserId & "' " & _
                                 "WHERE ConfName='TrapError'"
                    Else
                        SQLStr = "INSERT INTO ot_config (ConfName, ConfValue, User_id) " & _
                                 "VALUES ('TrapError', '" & ChkValue & "', '" & UserData.UserId & "'"
                    End If
                    ErrMsg = "Gagal update data config."
                    TotRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
                    If TotRow < 0 Then Exit Sub
                End If
                ' update trap error
                If UserData.SaveAudittrail <> ChkLogTrans.Checked Then
                    If ChkLogTrans.Checked Then ChkValue = "Y" Else ChkValue = "N"
                    SQLStr = "SELECT ConfValue FROM ot_config " & _
                             "WHERE ConfName='SaveAudittrail'"
                    ErrMsg = "Gagal baca data config."
                    If DBQueryGetTotalRows(SQLStr, MyConn, ErrMsg, False, UserData) > 0 Then
                        SQLStr = "UPDATE ot_Config SET ConfValue='" & ChkValue & "', User_id='" & UserData.UserId & "' " & _
                                 "WHERE ConfName='SaveAudittrail'"
                    Else
                        SQLStr = "INSERT INTO ot_config (ConfName, ConfValue, User_id) " & _
                                 "VALUES ('SaveAudittrail', '" & ChkValue & "', '" & UserData.UserId & "'"
                    End If
                    ErrMsg = "Gagal update data config."
                    TotRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
                    If TotRow < 0 Then Exit Sub
                End If

            End If
        End If
        Me.Close()
    End Sub

    Private Sub BtnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTutup.Click
        Me.Close()
    End Sub

End Class