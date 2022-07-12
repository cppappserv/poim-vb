'Title        : User Admin Maintain
'Form         : FM01_UserAdmin
'Created By   : HannY
'Created Date : 08 September 2008
'Table Used   : tbm_users, tbm_company, tbm_moduls, tbm_reports, 
'               tbm_users_company, tbm_users_module, tbm_users_reports        
Imports BPConnection.EncryptionUtility
Public Class FM01_UserAdmin
    'Dim MyConn As MySqlConnection
    Dim PilihanDlg As New DlgPilihan
    Dim MyReader As MySqlDataReader
    Dim lockedCheckState As Boolean = False
    Dim FormCode As New Hashtable(1000)
    Dim SQLStr, ErrMsg As String
    Dim SQLStr1, ErrMsg1 As String
    Dim SQLStr2, ErrMsg2 As String
    Dim SQLStr3, ErrMsg3 As String
    Dim SQLStr4, ErrMsg4 As String
    Dim mNew As Boolean 'TRUE = INSERT/NEW; FALSE = UPDATE
    Dim varUSER_CT, PaswdUser, FlagBlock As String, TotRow As Integer
    Dim UpdateDB, var_saveaudittrail As Boolean
    Dim mDATE As String = Format(Now, "yyyy-MM-dd HH:mm:ss")
    'Dim varbut
    Dim v_block As String

    Private Sub FM01_UserAdmin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim a, cnt As Integer
        'Me.Tbm_countryTableAdapter.Fill(Me.ImprDSCountry.tbm_country)
        Dim column As DataGridViewColumn = _
                   New DataGridViewTextBoxColumn()

        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        'SQLStr = Form.ActiveForm

        Call InitializeDGV()
        btnDelete.Enabled = False
        mNew = True

        'header TIDAK BOLEH di sort
        'karena checkbox datanya jadi gak valid 
        cnt = dgvComp.Columns.Count - 1
        For a = 0 To cnt
            dgvComp.Columns(a).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        cnt = dgvModul.Columns.Count - 1
        For a = 0 To cnt
            dgvModul.Columns(a).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        cnt = dgvReport.Columns.Count - 1
        For a = 0 To cnt
            dgvReport.Columns(a).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    'Private Sub FindData()
    '    SQLStr = "SELECT user_name " & _
    '             "FROM ms_user " & _
    '             "WHERE user_id = '" & TxtKodeUser.Text & "'"
    '    ErrMsg = "Gagal baca data user."
    '    MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

    '    LblIsiNamaUser.Text = ""

    '    For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
    '        dgvAkses.Rows.Item(i).Cells.Item(0).Value = False
    '    Next

    '    Dim found As Boolean = False

    '    If Not MyReader Is Nothing Then
    '        If MyReader.Read Then
    '            LblIsiNamaUser.Text = MyReader.GetString(0)
    '            found = True
    '        End If
    '    End If
    '    CloseMyReader(MyReader, UserData)

    '    lockedCheckState = True
    '    CbSemuaAkses.CheckState = CheckState.Unchecked

    '    If Not found Then
    '        lockedCheckState = False
    '        Exit Sub
    '    End If

    '    FillChecked()

    '    lockedCheckState = False
    'End Sub

    'Private Sub FillChecked()
    '    FormCode.Clear()

    '    SQLStr = "SELECT form_code, access " & _
    '             "FROM ms_user_form " & _
    '             "WHERE user_id = '" & TxtKodeUser.Text & "'"
    '    ErrMsg = "Gagal membaca detail data akses user."
    '    MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

    '    If Not MyReader Is Nothing Then
    '        While MyReader.Read
    '            formCode.Add(MyReader.GetString(0), MyReader.GetChar(1))
    '        End While

    '        Dim counter As Integer = 0

    '        For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
    '            If formCode.ContainsKey(dgvAkses.Rows.Item(i).Cells.Item(3).Value) AndAlso _
    '                formCode.Item(dgvAkses.Rows.Item(i).Cells.Item(3).Value).Equals(CChar("Y")) Then

    '                dgvAkses.Rows.Item(i).Cells.Item(0).Value = True
    '                counter += 1
    '            End If
    '        Next

    '        If counter = dgvAkses.RowCount Then
    '            CbSemuaAkses.CheckState = CheckState.Checked
    '        ElseIf counter > 0 Then
    '            CbSemuaAkses.CheckState = CheckState.Indeterminate
    '        End If
    '    End If

    '    CloseMyReader(MyReader, UserData)
    'End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Visible = False
        PilihanDlg.SQLGrid = "SELECT user_id AS 'User ID', " & _
                                "name AS 'User Name' " & _
                            "FROM tbm_users " & _
                            "ORDER BY user_id ASC"
        PilihanDlg.SQLFilter = "SELECT user_id AS 'User ID', " & _
                                "name AS 'User Name' " & _
                            "FROM tbm_users " & _
                            "WHERE user_id LIKE 'FilterData1%'" & _
                            "ORDER BY user_id ASC"
        PilihanDlg.Tables = "tbm_users"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtUser_ID.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub
    Private Sub FindData()


        'For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
        '    dgvAkses.Rows.Item(i).Cells.Item(0).Value = False
        'Next

        'Dim found As Boolean = False

        'If Not MyReader Is Nothing Then
        '    If MyReader.Read Then
        '        LblIsiNamaUser.Text = MyReader.GetString(0)
        '        found = True
        '    End If
        'End If
        'CloseMyReader(MyReader, UserData)

        'lockedCheckState = True
        'CbSemuaAkses.CheckState = CheckState.Unchecked

        'If Not found Then
        '    lockedCheckState = False
        '    Exit Sub
        'End If

        'FillChecked()

        'lockedCheckState = False
    End Sub
    Private Sub ok_message(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal var_msg As String)
        Dim x As Boolean
        x = f_msgbox_successful(var_msg)
        btnNew_Click(sender, e)
        txtUser_ID.Focus()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim kodeComp As String
        Dim kodeModul As String
        Dim kodeReport As String
        Dim insertStr As String = ""
        Dim kodegroup As Integer

        dgvComp.CommitEdit(DataGridViewDataErrorContexts.Commit)
        dgvModul.CommitEdit(DataGridViewDataErrorContexts.Commit)
        dgvReport.CommitEdit(DataGridViewDataErrorContexts.Commit)

        If Not CekInput(txtUser_ID, "User ID") Then Exit Sub
        If Not CekInput(txtName, "Username") Then Exit Sub
        If Not CekInput(txtPassword, "Password") Then Exit Sub

        kodegroup = cbGroup.SelectedIndex
        If cbBlock.Checked Then FlagBlock = "Y" Else FlagBlock = "N"


        PaswdUser = GetEncryption(txtPassword.Text)
        'DBQueryUpdate("BEGIN", MyConn, False, "Start Transaction Failed", UserData)

        'First table tbm_users
        If mNew = True Then
            'Dim AffRow As Integer

            SQLStr = "INSERT INTO tbm_users (USER_ID,NAME,TITLE,PASSWORD,BLOCKED,CREATEDDT,PHONE,FAX,EMAIL,GROUP_ID) " & _
                     "VALUES ('" & txtUser_ID.Text & "', '" & txtName.Text & "', '" & txtTitle.Text & "', '" & PaswdUser & "'," & _
                     "'" & FlagBlock & "', '" & mDATE & "', '" & txtPhone.Text & "', '" & txtFax.Text & "', '" & txtEmail.Text & "', '" & kodegroup & "') "
            ErrMsg = "Failed when saving data tbm_users(Table User)."
        Else
            SQLStr = " UPDATE tbm_users SET Name = '" & txtName.Text & "', password = '" & PaswdUser & "', " & _
                     " title = '" & txtTitle.Text & "', blocked = '" & FlagBlock & "'," & _
                     " phone = '" & txtPhone.Text & "', fax = '" & txtFax.Text & "'," & _
                     " email = '" & txtEmail.Text & "', group_id = '" & kodegroup & "'" & _
                     " WHERE USER_CT ='" & varUSER_CT & "' "
            ErrMsg = "Failed when updating data tbm_users(Table User)."
        End If
        TotRow = DBQueryUpdate(SQLStr, MyConn, True, ErrMsg, UserData)
        If TotRow < 0 Then
            Exit Sub
        End If

        'get user ct
        SQLStr = "SELECT * FROM tbm_users " & _
                 "WHERE user_id = '" & txtUser_ID.Text & "' "
        ErrMsg = "Failed when read Users Data"
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            'txtCountry.Text = ""
            mNew = False
            While MyReader.Read
                Try
                    varUSER_CT = MyReader.GetString("User_CT")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        If Not varUSER_CT Is Nothing Then

            ' Tabel tbm_users_company
            FormCode.Clear()
            insertStr = ""

            SQLStr2 = "DELETE FROM tbm_users_company " & _
                     "WHERE USER_CT='" & varUSER_CT & "' "
            ErrMsg2 = "Failed when accessing tbm_users_company(Table User Company)."
            TotRow = DBQueryUpdate(SQLStr2, MyConn, True, ErrMsg2, UserData)
            'MyReader = DBQueryMyReader(SQLStr2, MyConn, ErrMsg2, UserData)
            'CloseMyReader(MyReader, UserData)
            For i As Integer = dgvComp.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvComp.Rows.GetLastRow(DataGridViewElementStates.Visible)
                kodeComp = dgvComp.Rows.Item(i).Cells.Item(1).Value.ToString
                If dgvComp.Rows.Item(i).Cells.Item(0).Value.Equals(True) Then
                    If Not FormCode.ContainsKey(kodeComp) Then
                        insertStr &= "('" & varUSER_CT & "', '" & kodeComp & "'), "
                    End If
                End If
            Next
            If Not insertStr.Equals("") Then
                insertStr = insertStr.Substring(0, insertStr.Length - 2)
                SQLStr2 = "INSERT INTO tbm_users_company (USER_CT, COMPANY_CODE) " & _
                          "VALUES " & insertStr
                ErrMsg2 = "Gagal update data tbm_users_company(Table User Company)."
                TotRow = DBQueryUpdate(SQLStr2, MyConn, True, ErrMsg2, UserData)
                If TotRow < 0 Then
                    Exit Sub
                End If
            End If

            ' Tabel tbm_users_modul
            FormCode.Clear()
            insertStr = ""
            SQLStr3 = "DELETE FROM tbm_users_modul " & _
                     "WHERE USER_CT='" & varUSER_CT & "' "
            ErrMsg3 = "Failed when accessing tbm_users_modul(Table User Module)."

            'MyReader = DBQueryMyReader(SQLStr3, MyConn, ErrMsg3, UserData)
            'CloseMyReader(MyReader, UserData)
            TotRow = DBQueryUpdate(SQLStr3, MyConn, True, ErrMsg3, UserData)
            For i As Integer = dgvModul.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvModul.Rows.GetLastRow(DataGridViewElementStates.Visible)
                kodeModul = dgvModul.Rows.Item(i).Cells.Item(1).Value.ToString
                If dgvModul.Rows.Item(i).Cells.Item(0).Value.Equals(True) Then
                    If Not FormCode.ContainsKey(kodeModul) Then
                        insertStr &= "('" & varUSER_CT & "', '" & kodeModul & "'), "
                    End If
                End If
            Next
            If Not insertStr.Equals("") Then
                insertStr = insertStr.Substring(0, insertStr.Length - 2)
                SQLStr3 = "INSERT INTO tbm_users_modul (USER_CT, MODUL_CODE) " & _
                          "VALUES " & insertStr
                ErrMsg3 = "Gagal update data tbm_users_modul(Table User Module)."
                TotRow = DBQueryUpdate(SQLStr3, MyConn, True, ErrMsg3, UserData)
                If TotRow < 0 Then
                    Exit Sub
                End If
            End If
            ' Tabel tbm_users_report
            FormCode.Clear()
            insertStr = ""
            SQLStr4 = "DELETE FROM tbm_users_report " & _
                     "WHERE USER_CT='" & varUSER_CT & "' "
            ErrMsg4 = "Failed when accessing tbm_users_report(Table User Report)."

            TotRow = DBQueryUpdate(SQLStr4, MyConn, True, ErrMsg4, UserData)
            For i As Integer = dgvReport.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvReport.Rows.GetLastRow(DataGridViewElementStates.Visible)
                kodeReport = dgvReport.Rows.Item(i).Cells.Item(1).Value.ToString
                If dgvReport.Rows.Item(i).Cells.Item(0).Value.Equals(True) Then
                    If Not FormCode.ContainsKey(kodeReport) Then
                        insertStr &= "('" & varUSER_CT & "', '" & kodeReport & "'), "
                    End If
                End If
            Next
            If Not insertStr.Equals("") Then
                insertStr = insertStr.Substring(0, insertStr.Length - 2)
                SQLStr4 = "INSERT INTO tbm_users_report (USER_CT, REPORT_CODE) " & _
                          "VALUES " & insertStr
                ErrMsg4 = "Gagal update data tbm_users_report(Table User REPORT)."
                TotRow = DBQueryUpdate(SQLStr4, MyConn, True, ErrMsg4, UserData)
                If TotRow < 0 Then
                    Exit Sub
                End If
            End If

            UpdateDB = True
            ErrMsg = "User Data saved."
            MsgBox(ErrMsg)
        Else
            ErrMsg = "Failed when read Users Data"
            MsgBox(ErrMsg)
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        newFM01(True, "")
        mNew = True
        Call InitializeDGV()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If mNew = True Or txtUser_ID.Text = "" Then
            MsgBox("No Data Found")
        Else
            Dim AffRow As Integer

            SQLStr = " Delete from tbm_users " & _
                     " WHERE User_ID ='" & txtUser_ID.Text & "' "
            ErrMsg = "Failed when deleting data"""
            AffRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
            If AffRow < 0 Then
                MsgBox("Delete failed...", MsgBoxStyle.Information, "Information")
                Exit Sub
            Else
                Call ok_message(sender, e, "Delete data ")
            End If
        End If
    End Sub

    Private Sub txtUser_ID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUser_ID.TextChanged
        Call f_getuser()
    End Sub
    Private Sub f_getuser()
        SQLStr = "SELECT * FROM tbm_users " & _
                 "WHERE user_id = '" & txtUser_ID.Text & "' "
        ErrMsg = "Failed when read Users Data"
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            newFM01(False, "*")
            'txtCountry.Text = ""
            mNew = False
            While MyReader.Read
                Try
                    varUSER_CT = MyReader.GetString("User_CT")
                Catch ex As Exception
                End Try
                Try
                    txtPassword.Text = ReadEncryption(MyReader.GetString("password"))
                Catch ex As Exception
                End Try
                Try
                    txtUser_ID.Text = MyReader.GetString("User_ID")
                Catch ex As Exception
                End Try
                Try
                    txtName.Text = MyReader.GetString("name")
                Catch ex As Exception
                End Try
                Try
                    txtTitle.Text = MyReader.GetString("Title")
                Catch ex As Exception
                End Try
                Try
                    txtEmail.Text = MyReader.GetString("Email")
                Catch ex As Exception
                End Try
                Try
                    txtPhone.Text = MyReader.GetString("Phone")
                Catch ex As Exception
                End Try
                Try
                    txtFax.Text = MyReader.GetString("Fax")
                Catch ex As Exception
                End Try
                Try
                    cbGroup.SelectedIndex = MyReader.GetString("Group_Id")
                Catch ex As Exception
                    cbGroup.SelectedIndex = 0
                End Try

                Try
                    v_block = MyReader.GetString("BLOCKED")
                    If v_block = "Y" Then
                        cbBlock.Checked = True
                    Else
                        cbBlock.Checked = False
                    End If
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                'txtDocCode.Text = ""
                'txtUser_ID.Text = ""
                txtPassword.Text = ""
                mNew = True
            Else
                mNew = False
            End If

            'While MyReader.Read
            '    Try
            '        txtUser_ID.Text = MyReader.GetString("user_id")
            '    Catch ex As Exception
            '    End Try
            '    Try
            '        txtUser_ID.Text = MyReader.GetString("name")
            '    Catch ex As Exception
            '    End Try
            '    'Try
            '    '    txtCountry.Text = MyReader.GetString("title")
            '    'Catch ex As Exception
            '    'End Try
            'End While

            CloseMyReader(MyReader, UserData)
        End If
        'Exit Function

        Call InitializeDGV()
        Call FillCheckedDGV()



    End Sub
    Private Sub newFM01(ByVal boolUSERID As Boolean, ByVal pass As String)
        'If boolUSERID = True Then
        '    txtUser_ID.Text = ""
        'End If
        'If pass = "*" Then
        '    txtPassword.Text = "*****"
        'Else
        '    txtPassword.Text = ""
        'End If
        txtName.Text = ""
        txtTitle.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        cbBlock.Checked = False
        cbCompAll.Checked = False
        cbModulAll.Checked = False
        cbReportAll.Checked = False

    End Sub
    Private Sub InitializeDGV()
        dgvComp.DataSource = Show_Grid(dgvComp, "tbm_company")
        dgvModul.DataSource = Show_Grid(dgvModul, "tbm_moduls order by ord")
        dgvReport.DataSource = Show_Grid(dgvReport, "tbm_reports")
        If dgvComp.Rows.GetLastRow(DataGridViewElementStates.Visible) >= 0 Then
            For i As Integer = dgvComp.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvComp.Rows.GetLastRow(DataGridViewElementStates.Visible)
                dgvComp.Rows.Item(i).Cells.Item(0).Value = False
                dgvComp.Columns(2).Width = 250
            Next
        End If
        If dgvModul.Rows.GetLastRow(DataGridViewElementStates.Visible) >= 0 Then
            For i As Integer = dgvModul.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvModul.Rows.GetLastRow(DataGridViewElementStates.Visible)
                dgvModul.Rows.Item(i).Cells.Item(0).Value = False
                dgvModul.Rows.Item(i).Cells.Item(3).Value = False
                dgvModul.Columns(3).Visible = False
                dgvModul.Columns(2).Width = 250
            Next
        End If
        If dgvReport.Rows.GetLastRow(DataGridViewElementStates.Visible) >= 0 Then
            For i As Integer = dgvReport.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvReport.Rows.GetLastRow(DataGridViewElementStates.Visible)
                dgvReport.Rows.Item(i).Cells.Item(0).Value = False
            Next
        End If
    End Sub
    Private Sub FillCheckedDGV()
        ' company
        FormCode.Clear()

        SQLStr = "SELECT company_code " & _
                 "FROM tbm_users_company " & _
                 "WHERE user_ct = '" & varUSER_CT & "'"
        ErrMsg = "Gagal membaca detail data akses user."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                FormCode.Add(MyReader.GetString(0), MyReader.GetString(0))
            End While

            Dim counter As Integer = 0
            If MyReader.HasRows = True Then
                For i As Integer = dgvComp.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvComp.Rows.GetLastRow(DataGridViewElementStates.Visible)
                    If FormCode.ContainsKey(dgvComp.Rows.Item(i).Cells.Item(1).Value) Then
                        dgvComp.Rows.Item(i).Cells.Item(0).Value = True
                        counter += 1
                    End If
                Next
                cbCompAll.Checked = False
                If counter = dgvComp.RowCount Then
                    cbCompAll.CheckState = CheckState.Checked
                ElseIf counter > 0 Then
                    cbCompAll.CheckState = CheckState.Indeterminate
                End If
            End If
        End If

        CloseMyReader(MyReader, UserData)

        'modul
        FormCode.Clear()

        SQLStr = "SELECT MODUL_CODE " & _
                 "FROM tbm_users_modul " & _
                 "WHERE user_ct = '" & varUSER_CT & "'"
        ErrMsg = "Gagal membaca detail data akses user."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                FormCode.Add(MyReader.GetString(0), MyReader.GetString(0))
            End While

            Dim counter As Integer = 0
            If MyReader.HasRows = True Then

                For i As Integer = dgvModul.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvModul.Rows.GetLastRow(DataGridViewElementStates.Visible)
                    If FormCode.ContainsKey(dgvModul.Rows.Item(i).Cells.Item(1).Value) Then

                        dgvModul.Rows.Item(i).Cells.Item(0).Value = True
                        counter += 1
                    End If
                Next
                cbModulAll.Checked = False
                If counter = dgvModul.RowCount Then
                    cbModulAll.CheckState = CheckState.Checked
                ElseIf counter > 0 Then
                    cbModulAll.CheckState = CheckState.Indeterminate
                End If
            End If
        End If

        CloseMyReader(MyReader, UserData)


        'report
        FormCode.Clear()

        SQLStr = "SELECT REPORT_CODE " & _
                 "FROM tbm_users_report " & _
                 "WHERE user_ct = '" & varUSER_CT & "'"
        ErrMsg = "Gagal membaca detail data akses user."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                FormCode.Add(MyReader.GetString(0), MyReader.GetString(0))
            End While

            Dim counter As Integer = 0
            If MyReader.HasRows = True Then

                For i As Integer = dgvReport.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvReport.Rows.GetLastRow(DataGridViewElementStates.Visible)
                    If FormCode.ContainsKey(dgvReport.Rows.Item(i).Cells.Item(1).Value) Then

                        dgvReport.Rows.Item(i).Cells.Item(0).Value = True
                        counter += 1
                    End If
                Next
                cbReportAll.Checked = False
                If counter = dgvReport.RowCount Then
                    cbReportAll.CheckState = CheckState.Checked
                ElseIf counter > 0 Then
                    cbReportAll.CheckState = CheckState.Indeterminate
                End If
            End If
        End If

        CloseMyReader(MyReader, UserData)

    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Me.Close()    
        closeForm(sender, e, Me)
    End Sub
End Class