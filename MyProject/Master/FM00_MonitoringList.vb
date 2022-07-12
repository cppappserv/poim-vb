'Title                         : Monitoring Status
'Form                          : FM00_MonitoringList
'Table Used                    : all
'Stored Procedure Used (MySQL) : RunSQL

Imports vbs = Microsoft.VisualBasic.Strings

Public Class FM00_MonitoringList
    Dim NmList, FieldList As String
    Dim SQLstr As String
    Dim ErrMsg As String
    Dim MyReader As MySqlDataReader

    Private Sub FM00_MonitoringList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tempVar As String()
        Dim stradd() As String
        Dim z, y As Integer

        btnDelete.Enabled = PunyaAkses("FM01")
        txtListName.Text = NmList
        If txtListName.Text = "*" Then txtListName.Text = ""

        stradd = Split(FieldList, ";")
        For z = LBound(stradd) To UBound(stradd)
            y = z + 1
            tempVar = New String() {stradd(z), y.ToString}
            dgList.Rows.Add(tempVar)
            'If z < 4 Then dgList.Rows.Item(z).Cells.Item(1).ReadOnly = True
            If z < 4 Then dgList.Rows(z).Visible = False

        Next z
    End Sub

    Public Sub New(ByVal ListNm As String, ByVal FieldNm As String)
        FieldList = FieldNm
        NmList = ListNm
        InitializeComponent()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim irow As Integer

        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_listdata " & _
                 "where ListName='" & txtListName.Text & "'"

        ErrMsg = "Failed when deleting template"
        irow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If irow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete Template Items")
        Else
            f_msgbox_successful("Delete Data")
        End If
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim irow, crow As Integer
        Dim dstr, dno As String
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        If txtListName.Text = "" Then
            MsgBox("Enter List Name. first! ", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        End If

        SQLstr = "SELECT * FROM tbm_listdata WHERE listname = '" & txtListName.Text & "'"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                dstr = MyReader.GetString("ListName")
            End While
            CloseMyReader(MyReader, UserData)
        End If
        If dstr <> "" Then
            SQLstr = "DELETE from tbm_listdata " & _
                 "where ListName='" & txtListName.Text & "'"

            crow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
            If crow < 0 Then
                MsgBox("Update failed...", MsgBoxStyle.Information, "Update Template Items")
                Exit Sub
            End If
        End If

        SQLstr = "INSERT INTO tbm_listdata (ListName, FieldName, OrdNo) VALUE "
        dgList.CommitEdit(DataGridViewDataErrorContexts.Commit)
        For irow = 0 To dgList.RowCount - 2
            dstr = dgList.Rows(irow).Cells(0).Value.ToString
            dno = dgList.Rows(irow).Cells(1).Value.ToString
            If dno <> "0" And dno <> "" Then
                If irow > 3 Then dno = dno + 4
                SQLstr = SQLstr & "('" & txtListName.Text & "','" & dstr & "','" & dno & "'), "
            End If
        Next
        SQLstr = Mid(SQLstr, 1, Len(SQLstr) - 2)
        crow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If crow < 0 Then
            MsgBox("Save failed...", MsgBoxStyle.Information, "Save Template Items")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Function DataExist(ByVal str As String) As Boolean
        MyReader = DBQueryMyReader(str, MyConn, "", UserData)
        If MyReader Is Nothing Then
            Return False
        Else
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return True
            End While
        End If
        CloseMyReader(MyReader, UserData)
    End Function

    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function
End Class
