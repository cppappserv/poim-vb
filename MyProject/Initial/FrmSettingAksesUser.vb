Public Class FrmSettingAksesUser
    Dim MyConn As MySqlConnection
    Dim PilihanDlg As New DlgPilihan
    Dim MyReader As MySqlDataReader
    Dim lockedCheckState As Boolean = False
    Dim FormCode As New Hashtable(1000)
    Dim SQLStr, ErrMsg As String

    Private Sub FrmSettingAksesUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
        SQLStr = "SELECT T1.categ_form_code AS 'Kode Kategori', categ_form_name AS 'Nama Kategori', " & _
                    "form_code AS 'Kode Form', form_name AS 'Nama Form' " & _
                 "FROM ms_form_category T1, ms_form_detail T2 " & _
                 "WHERE T1.categ_form_code = T2.categ_form_code " & _
                 "ORDER BY T1.categ_form_code ASC, form_code ASC"
        'SQLStr = "SELECT * from tbm_cust ORDER BY codecust"
        ErrMsg = "Gagal baca data detail form."
        dgvAkses.DataSource = DBQueryDataTable(SQLStr, MyConn, "", ErrMsg, UserData)

        For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
            dgvAkses.Rows.Item(i).Cells.Item(0).Value = False
        Next

        dgvAkses.Columns.Item(1).Visible = False

        dgvAkses.Columns.Item(2).Width = 200
        dgvAkses.Columns.Item(2).ReadOnly = True

        dgvAkses.Columns.Item(3).Visible = False

        dgvAkses.Columns.Item(4).Width = 200
        dgvAkses.Columns.Item(4).ReadOnly = True

        ' define form dialog
        PilihanDlg.LblKey2.Hide()
        PilihanDlg.TxtKey2.Hide()
        PilihanDlg.DgvResult.Location = New System.Drawing.Point(12, 39)
        PilihanDlg.TblLayoutPanel.Location = New System.Drawing.Point(236, 240)
        PilihanDlg.Size = New System.Drawing.Size(400, 278)
    End Sub

    Private Sub btnPilihKodeUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPilihKodeUser.Click
        PilihanDlg.Text = "Pilih Kode User"
        PilihanDlg.LblKey1.Text = "Kode User"
        PilihanDlg.SQLGrid = "SELECT user_id AS 'Kode User', " & _
                                "user_name AS 'Nama User' " & _
                            "FROM ms_user " & _
                            "ORDER BY user_id ASC"
        PilihanDlg.SQLFilter = "SELECT user_id AS 'Kode User', " & _
                                "user_name AS 'Nama User' " & _
                            "FROM ms_user " & _
                            "WHERE user_id LIKE 'FilterData1%' " & _
                            "ORDER BY user_id ASC"
        PilihanDlg.Tables = "ms_user"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtKodeUser.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            FindData()
        End If
    End Sub

    Private Sub TxtKodeUser_Validating(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKodeUser.Validating
        FindData()
    End Sub

    Private Sub CbSemuaAkses_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CbSemuaAkses.CheckStateChanged
        If lockedCheckState Then Exit Sub

        lockedCheckState = True

        If CbSemuaAkses.CheckState = CheckState.Checked Then
            For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
                dgvAkses.Rows.Item(i).Cells.Item(0).Value = True
            Next
        ElseIf CbSemuaAkses.CheckState = CheckState.Unchecked Then
            For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
                dgvAkses.Rows.Item(i).Cells.Item(0).Value = False
            Next
        Else
            For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
                If formCode.ContainsKey(dgvAkses.Rows.Item(i).Cells.Item(3).Value) AndAlso _
                    formCode.Item(dgvAkses.Rows.Item(i).Cells.Item(3).Value).Equals(CChar("Y")) Then

                    dgvAkses.Rows.Item(i).Cells.Item(0).Value = True
                Else
                    dgvAkses.Rows.Item(i).Cells.Item(0).Value = False
                End If
            Next
        End If

        lockedCheckState = False
    End Sub

    Private Sub dgvAkses_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAkses.CellContentClick
        ' -------------------------------------------------------------------------
        ' Masih ada sedikit masalah dengan tampilan.
        ' -------------------------------------------------------------------------
        'If lockedCheckState Then Exit Sub

        'lockedCheckState = True

        'If dgvAkses.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value.Equals(True) Then
        '    dgvAkses.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value = False
        'ElseIf dgvAkses.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value.Equals(False) Then
        '    dgvAkses.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value = True
        'Else
        '    lockedCheckState = False
        '    Exit Sub
        'End If

        'Dim counter As Integer = 0

        'For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
        '    If dgvAkses.Rows.Item(i).Cells.Item(0).Value.Equals(True) Then
        '        counter += 1
        '    End If
        'Next

        'If counter = dgvAkses.RowCount Then
        '    CbSemuaAkses.CheckState = CheckState.Checked
        'ElseIf counter > 0 Then
        '    CbSemuaAkses.CheckState = CheckState.Indeterminate
        'Else
        '    CbSemuaAkses.CheckState = CheckState.Unchecked
        'End If

        'lockedCheckState = False
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        Dim kodeUser As String = ""
        If ChkEmptyValue(TxtKodeUser, kodeUser, "Kode user harus diisi.") Then Exit Sub

        SQLStr = "SELECT user_id FROM ms_user WHERE user_id = '" & kodeUser & "'"
        ErrMsg = "Gagal membaca tabel user."
        If DBQueryGetTotalRows(SQLStr, MyConn, ErrMsg, False, UserData) <= 0 Then
            MsgBox("Kode user tidak ada di database.", MsgBoxStyle.Exclamation, "Error")
            TxtKodeUser.Focus()
            Exit Sub
        End If

        Dim kodeForm As String
        Dim insertStr As String = ""
        Dim updateStr(2) As String
        'ReDim updateStr(2)
        updateStr(0) = ""
        updateStr(1) = ""

        For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
            kodeForm = dgvAkses.Rows.Item(i).Cells.Item(3).Value.ToString
            If dgvAkses.Rows.Item(i).Cells.Item(0).Value.Equals(True) Then
                If Not formCode.ContainsKey(kodeForm) Then
                    insertStr &= "('" & kodeUser & "', '" & kodeForm & "', 'Y'), "
                ElseIf formCode.Item(kodeForm).Equals(CChar("N")) Then
                    updateStr(0) &= "(form_code = '" & kodeForm & "') OR "
                End If
            Else
                If formCode.ContainsKey(kodeForm) AndAlso formCode.Item(kodeForm).Equals(CChar("Y")) Then
                    updateStr(1) &= "(form_code = '" & kodeForm & "') OR "
                End If
            End If
        Next

        Dim sukses As Boolean = True

        If Not insertStr.Equals("") Then
            insertStr = insertStr.Substring(0, insertStr.Length - 2)
            ErrMsg = "Gagal update data akses user form."
            If DBQueryUpdate( _
                "INSERT INTO ms_user_form (user_id, form_code, access) " & _
                "VALUES " & insertStr, _
                MyConn, False, ErrMsg, UserData) <= 0 Then

                sukses = False
            End If
        End If

        Dim checkStr As String

        For i As Integer = 0 To 1
            If Not updateStr(i).Equals("") Then
                If i = 0 Then
                    checkStr = "Y"
                Else
                    checkStr = "N"
                End If

                updateStr(i) = updateStr(i).Substring(0, updateStr(i).Length - 4)
                ErrMsg = "Gagal update data akses user form."
                If DBQueryUpdate( _
                    "UPDATE ms_user_form " & _
                    "SET access = '" & checkStr & "' " & _
                    "WHERE (user_id = '" & kodeUser & "') AND " & _
                        "(" & updateStr(i) & ")", _
                    MyConn, False, ErrMsg, UserData) <= 0 Then

                    sukses = False
                End If
            End If
        Next

        For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
            dgvAkses.Rows.Item(i).Cells.Item(0).Value = False
        Next

        lockedCheckState = True
        CbSemuaAkses.CheckState = CheckState.Unchecked
        fillChecked()
        lockedCheckState = False

        If sukses Then
            MsgBox("Proses menyimpan akses user berhasil dilakukan.", MsgBoxStyle.Information, "Setting Akses User")
        End If
    End Sub

    Private Sub FindData()
        SQLStr = "SELECT user_name " & _
                 "FROM ms_user " & _
                 "WHERE user_id = '" & TxtKodeUser.Text & "'"
        ErrMsg = "Gagal baca data user."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        LblIsiNamaUser.Text = ""

        For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
            dgvAkses.Rows.Item(i).Cells.Item(0).Value = False
        Next

        Dim found As Boolean = False

        If Not MyReader Is Nothing Then
            If MyReader.Read Then
                LblIsiNamaUser.Text = MyReader.GetString(0)
                found = True
            End If
        End If
        CloseMyReader(MyReader, UserData)

        lockedCheckState = True
        CbSemuaAkses.CheckState = CheckState.Unchecked

        If Not found Then
            lockedCheckState = False
            Exit Sub
        End If

        FillChecked()

        lockedCheckState = False
    End Sub

    Private Sub FillChecked()
        FormCode.Clear()

        SQLStr = "SELECT form_code, access " & _
                 "FROM ms_user_form " & _
                 "WHERE user_id = '" & TxtKodeUser.Text & "'"
        ErrMsg = "Gagal membaca detail data akses user."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                formCode.Add(MyReader.GetString(0), MyReader.GetChar(1))
            End While

            Dim counter As Integer = 0

            For i As Integer = dgvAkses.Rows.GetFirstRow(DataGridViewElementStates.Visible) To dgvAkses.Rows.GetLastRow(DataGridViewElementStates.Visible)
                If formCode.ContainsKey(dgvAkses.Rows.Item(i).Cells.Item(3).Value) AndAlso _
                    formCode.Item(dgvAkses.Rows.Item(i).Cells.Item(3).Value).Equals(CChar("Y")) Then

                    dgvAkses.Rows.Item(i).Cells.Item(0).Value = True
                    counter += 1
                End If
            Next

            If counter = dgvAkses.RowCount Then
                CbSemuaAkses.CheckState = CheckState.Checked
            ElseIf counter > 0 Then
                CbSemuaAkses.CheckState = CheckState.Indeterminate
            End If
        End If

        CloseMyReader(MyReader, UserData)
    End Sub

End Class