Public Class FrmSettingForm
    Dim MyConn As MySqlConnection
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim ProsesEdit As Boolean
    Dim SQLStr, ErrMsg As String

    Private Sub FrmSettingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

        SQLStr = "SELECT categ_form_code " & _
                 "FROM ms_form_category " & _
                 "ORDER BY categ_form_code ASC"
        ErrMsg = "Gagal baca data form kategori."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                CbKategori.Items.Add(MyReader.GetString(0))
            End While
        End If
        CloseMyReader(MyReader, UserData)

        ' define form dialog
        PilihanDlg.TxtKey1.Size = New System.Drawing.Size(377, 20)
        PilihanDlg.LblKey2.Hide()
        PilihanDlg.TxtKey2.Hide()
        PilihanDlg.DgvResult.Location = New System.Drawing.Point(12, 39)
        PilihanDlg.DgvResult.Size = New System.Drawing.Size(470, 167)
        PilihanDlg.TblLayoutPanel.Location = New System.Drawing.Point(236, 240)
        PilihanDlg.Size = New System.Drawing.Size(500, 278)
        PilihanDlg.Text = "Pilih Kode Form"
        PilihanDlg.LblKey1.Text = "Kode Form"
        PilihanDlg.SQLGrid = "SELECT T1.categ_form_code AS 'Kode Kategori', " & _
                                "categ_form_name AS 'Nama Kategori', " & _
                                "form_code AS 'Kode Form', " & _
                                "form_name AS 'Nama Form' " & _
                            "FROM ms_form_category T1, ms_form_detail T2 " & _
                            "WHERE (T1.categ_form_code = T2.categ_form_code) " & _
                            "ORDER BY T1.categ_form_code ASC, form_code ASC"

        PilihanDlg.SQLFilter = "SELECT T1.categ_form_code AS 'Kode Kategori', " & _
                                "categ_form_name AS 'Nama Kategori', " & _
                                "form_code AS 'Kode Form', " & _
                                "form_name AS 'Nama Form' " & _
                            "FROM ms_form_category T1, ms_form_detail T2 " & _
                            "WHERE (T1.categ_form_code = T2.categ_form_code) AND " & _
                                "(form_code LIKE 'FilterData1%') " & _
                            "ORDER BY T1.categ_form_code ASC, form_code ASC"
        PilihanDlg.Tables = "ms_form_category, ms_form_detail"
    End Sub

    Private Sub LblKategori_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblKategori.Click
        CbKategori.Focus()
    End Sub

    Private Sub LblKodeForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblKodeForm.Click
        TxtKodeForm.Focus()
    End Sub

    Private Sub LblNamaForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblNamaForm.Click
        TxtNamaForm.Focus()
    End Sub

    Private Sub SetControlAble(ByVal ability As Boolean, ByVal ability2 As Boolean)
        CbKategori.Enabled = ability
        TxtKodeForm.Enabled = ability2
        btnPilihKodeForm.Visible = ability2
        TxtNamaForm.Enabled = ability
        btnFormBaru.Enabled = Not ability
        btnEditForm.Enabled = Not ability
        btnSimpan.Enabled = ability
        btnBatal.Enabled = ability
    End Sub

    Private Sub btnFormBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFormBaru.Click
        TxtKodeForm.Size = New System.Drawing.Size(154, 20)
        SetControlAble(True, True)
        prosesEdit = False
    End Sub

    Private Sub btnEditForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditForm.Click
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CbKategori.SelectedItem = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            TxtKodeForm.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            TxtNamaForm.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString
        Else
            Exit Sub
        End If

        SetControlAble(True, False)
        prosesEdit = True
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If MsgBox("Apakah Anda yakin ingin membatalkan setting form?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Setting Form") = MsgBoxResult.Yes Then
            TxtKodeForm.Size = New System.Drawing.Size(190, 20)
            SetControlAble(False, False)
            CbKategori.SelectedIndex = 0
            TxtKodeForm.Text = ""
            TxtNamaForm.Text = ""
        End If
    End Sub

    Private Sub CbKategori_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CbKategori.GotFocus
        StsKeterangan.Text = "Silahkan pilih kode kategori."
    End Sub

    Private Sub CbKategori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CbKategori.SelectedIndexChanged
        If CbKategori.SelectedIndex > 0 Then
            SQLStr = "SELECT categ_form_name " & _
                     "FROM ms_form_category " & _
                     "WHERE categ_form_code = '" & CStr(CbKategori.Items.Item(CbKategori.SelectedIndex)) & "' " & _
                     "ORDER BY categ_form_code ASC"
            ErrMsg = "Gagal baca data form kategori."
            MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

            If Not MyReader Is Nothing Then
                While MyReader.Read
                    LblNamaKategori.Text = MyReader.GetString(0)
                End While
            End If
            CloseMyReader(MyReader, UserData)
        Else
            LblNamaKategori.Text = ""
        End If
    End Sub

    Private Sub btnPilihKodeForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPilihKodeForm.Click
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CbKategori.SelectedItem = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            TxtKodeForm.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            TxtNamaForm.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If prosesEdit AndAlso (MsgBox("Apakah Anda yakin ingin memodifikasi data?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Input Tanggal Awal dan Akhir") = MsgBoxResult.No) Then
            Exit Sub
        End If

        If CbKategori.SelectedIndex = 0 Then
            MsgBox("Kode kategori harus dipilih.", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        Dim kodeForm As String = ""
        If ChkEmptyValue(TxtKodeForm, kodeForm, "Kode form harus diisi.") Then Exit Sub

        Dim namaForm As String = ""
        If ChkEmptyValue(TxtNamaForm, namaForm, "Nama form harus diisi.") Then Exit Sub

        If prosesEdit Then
            SQLStr = "UPDATE ms_form_detail " & _
                     "SET form_name = '" & namaForm & "', " & _
                        "categ_form_code = '" & CbKategori.SelectedItem.ToString & "' " & _
                     "WHERE form_code = '" & kodeForm & "'"
            ErrMsg = "Gagal update data form detail."
            If DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData) > 0 Then
                MsgBox("Proses edit form berhasil dilakukan.", _
                       MsgBoxStyle.Information, "Setting Form")
            End If
        Else
            SQLStr = "SELECT form_code FROM ms_form_detail " & _
                     "WHERE form_code = '" & kodeForm & "'"
            ErrMsg = "Gagal baca data form detail."
            If DBQueryGetTotalRows(SQLStr, MyConn, ErrMsg, False, UserData) > 0 Then
                MsgBox("Kode form sudah ada." & vbCrLf & _
                       "Silahkan isi dengan kode form baru.", _
                       MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            SQLStr = "INSERT INTO ms_form_detail (form_code, form_name, categ_form_code) " & _
                     "VALUES ('" & kodeForm & "', '" & namaForm & "', '" & CbKategori.SelectedItem.ToString & "')"
            ErrMsg = "Update data form detail."
            If DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData) > 0 Then
                MsgBox("Proses menyimpan data form baru berhasil dilakukan.", _
                       MsgBoxStyle.Information, "Setting Form")
            End If
        End If
    End Sub

    Private Sub TxtKodeForm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKodeForm.GotFocus
        StsKeterangan.Text = "Silahkan isi kode form (maksimal 20 karakter)."
    End Sub

    Private Sub TxtNamaForm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNamaForm.GotFocus
        StsKeterangan.Text = "Silahkan isi nama form (maksimal 40 karakter)."
    End Sub

    Private Sub btnFormBaru_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFormBaru.MouseHover
        StsKeterangan.Text = "Klik untuk menambah data form baru."
    End Sub

    Private Sub btnEditForm_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditForm.MouseHover
        StsKeterangan.Text = "Klik untuk mengubah data form yang sudah ada."
    End Sub

    Private Sub btnSimpan_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.MouseHover
        If prosesEdit Then
            StsKeterangan.Text = "Klik untuk menyimpan perubahan data form."
        Else
            StsKeterangan.Text = "Klik untuk menyimpan data form baru."
        End If
    End Sub

End Class