
'Title        : Master Data Unit
'Form         : FM29_PassUser
'Created By   : Prie
'Created Date : 18/05/2009
'Table Used   : 


Imports BPConnection.EncryptionUtility

Public Class FM29_PassUser
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim OldPass, NewPass, KonfPass As String
    Sub New()
        InitializeComponent()
    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
7:      RefreshScreen()
        LblUserId.Text = UserData.UserCT.ToString()
        LblUserName.Text = UserData.UserName
        OldPass = Nothing : NewPass = Nothing : KonfPass = Nothing
    End Sub
    Private Sub RefreshScreen()
        TxtOldPass.Clear()
        TxtNewPass.Clear()
        TxtKonfPass.Clear()
        TxtOldPass.Focus()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnCencel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshScreen()
    End Sub
    Function CekInput(ByVal nControl As Control, ByVal nInput As String) As Boolean
        If nControl.Text <> "" Then
            If nInput = "TxtOldPass" Then
                SQLstr = "SELECT user_ct FROM tbm_users WHERE password ='" & OldPass & "'"
                ErrMsg = "Gagal baca data..."
                MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    If MyReader.HasRows Then
                        CloseMyReader(MyReader, UserData)
                        Return True
                    End If
                End If
                CloseMyReader(MyReader, UserData)
                MsgBox("Changed was unsuccessful, please reenter old password", vbInformation, "PERHATIAN")
                nControl.Text = ""
                nControl.Focus()
            ElseIf nInput = "TxtNewPass" Then
                Return True
            ElseIf nInput = "TxtKonfPass" Then
                If NewPass = KonfPass Then
                    Return True
                End If
                MsgBox("Changed was unsuccessful,please reenter new password and confirmation", vbInformation, "PERHATIAN")
                nControl.Text = ""
                nControl.Focus()
            End If
        Else
            If nInput = "TxtOldPass" Then
                MsgBox("Changed was unsuccessful, please reenter old password", vbInformation, "PERHATIAN")
                nControl.Text = ""
                nControl.Focus()
            ElseIf nInput = "TxtNewPass" Then
                MsgBox("Changed was unsuccessful,please reenter new password", vbInformation, "PERHATIAN")
                nControl.Text = ""
                nControl.Focus()
            ElseIf nInput = "TxtKonfPass" Then
                MsgBox("Changed was unsuccessful,please reenter new password and konfirmation", vbInformation, "PERHATIAN")
                nControl.Text = ""
                nControl.Focus()
            End If
        End If
        Return False
    End Function

    Private Sub BttnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnOK.Click
        OldPass = GetEncryption(TxtOldPass.Text)
        NewPass = GetEncryption(TxtNewPass.Text)
        KonfPass = GetEncryption(TxtKonfPass.Text)
        If Not CekInput(TxtOldPass, "TxtOldPass") Then Exit Sub
        If Not CekInput(TxtNewPass, "TxtNewPass") Then Exit Sub
        If Not CekInput(TxtKonfPass, "TxtKonfPass") Then Exit Sub
        Dim teks As String
        SQLstr = "Update tbm_users set password='" & NewPass & "' where user_ct='" & LblUserId.Text & "'"
        ErrMsg = "Failed when updating tbm_users data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            f_msgbox_successful("Changed was unsuccessful...")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Changed was successful...")
        End If
    End Sub

    Private Sub BttnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnCancel.Click
        RefreshScreen()
    End Sub
End Class


