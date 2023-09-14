'Title        : Master Material Origin
'Form         : FM04_Material_Origin
'Created By   : Hanny
'Created Date : 23 September 2008
'Table Used   : tbm_material, tbm_material_group, tbm_Material_Origin

Imports poim.FM02_MaterialGroup
Public Class FM04_Material_Origin
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        btnSave.Enabled = False
        lblCountry_Name.Text = ""
        lblMaterial_Name.Text = ""

    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen(True)
    End Sub

    Private Sub ListData(ByVal smaterial As String, ByVal scountry As String)
        Dim brs As Integer
        Dim strSQL As String
        Dim dts As DataTable
        Dim lv_material, lv_country As String

        lv_material = smaterial
        lv_country = scountry

        strSQL = "select a.MATERIAL_CODE as MaterialCode, b.Material_Name as MaterialName,a.COUNTRY_CODE as CountryCode, c.Country_Name as CountryName," & _
                 "BEA_MASUK as BeaMasuk, BEA_MASUK_TAMBAHAN as BeaMasukTambahan, PPN, PPH_BEA_MASUK as PPN_BeaMasuk, PPH_21 as PPH21, " & _
                 "PPN_STATUS as PPNStatus, PIUD_TR from tbm_material_origin as a " & _
                 "left join tbm_material as b on a.material_Code=b.material_code " & _
                 "left join tbm_country as c on a.country_code=c.country_code " & _
                 "Where (('" & lv_material & "' <> '' and a.MATERIAL_CODE='" & lv_material & "' and '" & lv_country & "' <> '' and a.COUNTRY_CODE='" & lv_country & "') " & _
                 "OR ('" & lv_material & "' <> '' and a.MATERIAL_CODE='" & lv_material & "' and '" & lv_country & "' = '') " & _
                 "OR ('" & lv_material & "' = '' and '" & lv_country & "' <> '' and a.COUNTRY_CODE='" & lv_country & "') " & _
                 "OR ('" & lv_material & "' = '' and '" & lv_country & "' = '' )) "

        dts = DBQueryDataTable(strSQL, MyConn, "", ErrMsg, UserData)
        DataGridView1.DataSource = dts
        brs = DataGridView1.RowCount

        If Trim(lv_material) <> "" And Trim(lv_country) <> "" Then f_getdata()
    End Sub

    Private Sub RefreshScreen(ByVal clear1 As Boolean)
        Call ListData("", "")
        btnSave.Enabled = False
        btnDelete.Enabled = False
        ''txtMaterial_Code.Enabled = clear1
        ''txtCountry_Code.Enabled = clear1
        If clear1 = True Then
            txtMaterial_Code.Clear()
            txtCountry_Code.Clear()
        End If
        Call Refresh0()

        txtMaterial_Code.Focus()
        baru = True
        edit = False
    End Sub
    Private Sub Refresh0()
        Dim str0 As String

        str0 = FormatNumber(0, 3)
        txtBea_Masuk_Tambahan.Text = str0
        txtBea_Masuk.Text = str0
        txtPPH_Bea_Masuk.Text = str0
        txtPPN.Text = str0
        txtPPH_21.Text = str0
        txtPPN_Status.Text = str0
        txtPIUD_TR.Text = str0
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
        'Close()
        'Dispose()

    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen(True)
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_material_origin " & _
                 "where material_code='" & txtMaterial_Code.Text & "' and country_code = '" & txtCountry_Code.Text & "'"

        ErrMsg = "Failed when deleting user data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete Material Origin data")
            Exit Sub
        Else
            RefreshScreen(True)
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_material_origin")
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_material_origin where material_code='" & txtMaterial_Code.Text & "' " & _
                 "and country_code='" & txtCountry_Code.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox("Material Code in Country Code related already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtMaterial_Code.Focus()
            Exit Function
        End If

        ''Foreign Key
        'SQLstr = "Select * from tbm_country where country_code='" & txtCountry_Code.Text & "'"
        'If FM02_MaterialGroup.DataOK(SQLstr) = True Then
        '    MsgBox("Country code does not exist! ", MsgBoxStyle.Critical, "Warning")
        '    CekData = False
        '    txtCountry_Code.Focus()
        '    Exit Function
        'End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String
        Dim v_bea, v_bea_tambahan, v_ppn, v_pph_bea, v_pph21, v_ppn_status, v_piud As String

        If txtBea_Masuk.Text <> "" Then
            v_bea = Replace(CStr(CDec(txtBea_Masuk.Text)), ",", ".")
        Else
            v_bea = "0"
        End If

        If txtBea_Masuk_Tambahan.Text <> "" Then
            v_bea_tambahan = Replace(CStr(CDec(txtBea_Masuk_Tambahan.Text)), ",", ".")
        Else
            v_bea_tambahan = "0"
        End If

        If txtPPN.Text <> "" Then
            v_ppn = Replace(CStr(CDec(txtPPN.Text)), ",", ".")
        Else
            v_ppn = "0"
        End If

        If txtPPH_Bea_Masuk.Text <> "" Then
            v_pph_bea = Replace(CStr(CDec(txtPPH_Bea_Masuk.Text)), ",", ".")
        Else
            v_pph_bea = "0"
        End If

        If txtPPH_21.Text <> "" Then
            v_pph21 = Replace(CStr(CDec(txtPPH_21.Text)), ",", ".")
        Else
            v_pph21 = "0"
        End If

        If txtPPN_Status.Text <> "" Then
            v_ppn_status = Replace(CStr(CDec(txtPPN_Status.Text)), ",", ".")
        Else
            v_ppn_status = "0"
        End If

        If txtPIUD_TR.Text <> "" Then
            v_piud = Replace(CStr(CDec(txtPIUD_TR.Text)), ",", ".")
        Else
            v_piud = "0"
        End If

        If baru Then
            If CekData() = False Then Exit Sub
            teks = "Save Data"
            ErrMsg = "Failed when saving Material Origin data"

            SQLstr = "INSERT INTO tbm_material_origin (MATERIAL_CODE,COUNTRY_CODE,BEA_MASUK,BEA_MASUK_TAMBAHAN,PPN,PPH_BEA_MASUK,PPH_21,PPN_STATUS,PIUD_TR) " & _
                     "VALUES ('" & txtMaterial_Code.Text & "', '" & _
                                   txtCountry_Code.Text & "', '" & _
                                   v_bea & "', '" & _
                                   v_bea_tambahan & "', '" & _
                                   v_ppn & "', '" & _
                                   v_pph_bea & "', '" & _
                                   v_pph21 & "', '" & _
                                   v_ppn_status & "', '" & _
                                   v_piud & "' )"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating Material Origin data"
            If (MsgBox("Update all Origin ?", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then
                SQLstr = "UPDATE tbm_material_origin " & _
                         "SET BEA_MASUK = '" & v_bea & "'," & _
                         "BEA_MASUK_TAMBAHAN = '" & v_bea_tambahan & "'," & _
                         "PPN = '" & v_ppn & "'," & _
                         "PPH_BEA_MASUK = '" & v_pph_bea & "'," & _
                         "PPH_21 = '" & v_pph21 & "', " & _
                         "PPN_STATUS = '" & v_ppn_status & "' ," & _
                         "PIUD_TR = '" & v_piud & "' " & _
                         "where MATERIAL_CODE='" & txtMaterial_Code.Text & "' and " & _
                         "COUNTRY_CODE = '" & txtCountry_Code.Text & "'"
            Else
                SQLstr = "UPDATE tbm_material_origin " & _
                         "SET BEA_MASUK = '" & v_bea & "'," & _
                         "BEA_MASUK_TAMBAHAN = '" & v_bea_tambahan & "'," & _
                         "PPN = '" & v_ppn & "'," & _
                         "PPH_BEA_MASUK = '" & v_pph_bea & "'," & _
                         "PPH_21 = '" & v_pph21 & "', " & _
                         "PPN_STATUS = '" & v_ppn_status & "' ," & _
                         "PIUD_TR = '" & v_piud & "' " & _
                         "where MATERIAL_CODE='" & txtMaterial_Code.Text & "'"
            End If

        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input Material Origin data")
            Exit Sub
        Else
            RefreshScreen(True)
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_material_origin")
        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(txtMaterial_Code.Text)) > 0) And (Len(Trim(txtCountry_Code.Text)) > 0) And _
        (Len(Trim(txtBea_Masuk.Text)) > 0 And IsNumeric(txtBea_Masuk.Text)) And (Len(Trim(txtBea_Masuk_Tambahan.Text)) > 0 And IsNumeric(txtBea_Masuk_Tambahan.Text)) And _
        (Len(Trim(txtPPN.Text)) > 0 And IsNumeric(txtPPN.Text)) And (Len(Trim(txtPPH_Bea_Masuk.Text)) > 0 And IsNumeric(txtPPH_Bea_Masuk.Text)) And _
        (Len(Trim(txtPPH_21.Text)) > 0 And IsNumeric(txtPPH_21.Text)) And (Len(Trim(txtPPN_Status.Text)) > 0 And IsNumeric(txtPPN_Status.Text)) And _
        (Len(Trim(txtPIUD_TR.Text)) > 0 And IsNumeric(txtPIUD_TR.Text))
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtMaterial_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtCountry_Code.Text = DataGridView1.Item(2, brs).Value.ToString

        If DataGridView1.Item(4, brs).Value.ToString <> "" Then
            txtBea_Masuk.Text = CStr(FormatNumber(DataGridView1.Item(4, brs).Value, 3))
        Else
            txtBea_Masuk.Text = DataGridView1.Item(3, brs).Value.ToString
        End If

        If DataGridView1.Item(5, brs).Value.ToString <> "" Then
            txtBea_Masuk_Tambahan.Text = CStr(FormatNumber(DataGridView1.Item(5, brs).Value, 3))
        Else
            txtBea_Masuk_Tambahan.Text = DataGridView1.Item(5, brs).Value.ToString
        End If

        If DataGridView1.Item(6, brs).Value.ToString <> "" Then
            txtPPN.Text = CStr(FormatNumber(DataGridView1.Item(6, brs).Value, 3))
        Else
            txtPPN.Text = DataGridView1.Item(6, brs).Value.ToString
        End If

        If DataGridView1.Item(7, brs).Value.ToString <> "" Then
            txtPPH_Bea_Masuk.Text = CStr(FormatNumber(DataGridView1.Item(7, brs).Value, 3))
        Else
            txtPPH_Bea_Masuk.Text = DataGridView1.Item(7, brs).Value.ToString
        End If


        If DataGridView1.Item(8, brs).Value.ToString <> "" Then
            txtPPH_21.Text = CStr(FormatNumber(DataGridView1.Item(8, brs).Value, 3))
        Else
            txtPPH_21.Text = DataGridView1.Item(8, brs).Value.ToString
        End If

        If DataGridView1.Item(9, brs).Value.ToString <> "" Then
            txtPPN_Status.Text = CStr(FormatNumber(DataGridView1.Item(9, brs).Value, 3))
        Else
            txtPPN_Status.Text = DataGridView1.Item(9, brs).Value.ToString
        End If


        If DataGridView1.Item(10, brs).Value.ToString <> "" Then
            txtPIUD_TR.Text = CStr(FormatNumber(DataGridView1.Item(10, brs).Value, 3))
        Else
            txtPIUD_TR.Text = DataGridView1.Item(10, brs).Value.ToString
        End If

        ''txtMaterial_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtMaterial_Code.Text)) > 0)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select Country Code"
        PilihanDlg.LblKey1.Text = "Country Code"
        PilihanDlg.LblKey2.Text = "Country Name"
        PilihanDlg.SQLGrid = "select COUNTRY_CODE as CountryCode, COUNTRY_NAME as CountryName from tbm_country"
        PilihanDlg.SQLFilter = "select COUNTRY_CODE as CountryCode, COUNTRY_NAME as CountryName from tbm_country " & _
                               "WHERE country_code LIKE 'FilterData1%' " & _
                               " and country_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_country"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCountry_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Call ListData(txtMaterial_Code.Text, txtCountry_Code.Text)
        End If
    End Sub
    Private Sub txtMaterial_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMaterial_Code.TextChanged
        RefreshTombolSave()
        lblMaterial_Name.Text = AmbilData("MATERIAL_NAME", "tbm_material", "MATERIAL_CODE='" & txtMaterial_Code.Text & "'")
    End Sub
    Private Sub txtCountry_Code_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCountry_Code.TextChanged
        RefreshTombolSave()
        lblCountry_Name.Text = AmbilData("COUNTRY_NAME", "tbm_country", "COUNTRY_CODE='" & txtCountry_Code.Text & "'")
        If Len(Trim(txtMaterial_Code.Text)) > 0 And Len(Trim(txtCountry_Code.Text)) > 0 Then
            'Call f_getdata()
        End If
    End Sub

    Private Sub txtBea_Masuk_TextAlignChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBea_Masuk_Tambahan.TextAlignChanged
        RefreshTombolSave()
    End Sub
    Private Sub txtBea_Masuk_Tambahan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBea_Masuk.TextChanged
        RefreshTombolSave()
    End Sub
    Private Sub txtPPN_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPH_Bea_Masuk.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub txtPPH_Bea_Masuk_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPN.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub txtPPH_21_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPH_21.TextChanged
        RefreshTombolSave()
    End Sub
    Private Sub txtPPN_Status_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPH_21.TextChanged
        RefreshTombolSave()
    End Sub
    Private Sub txtPIUD_TR_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPH_21.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub txtMaterial_Code_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMaterial_Code.Validating
        'Call f_getdata()
    End Sub
    Private Sub f_getdata()
        Call Refresh0()
        SQLstr = "select * from tbm_material_origin where material_code = '" & txtMaterial_Code.Text & "' and country_code = '" & txtCountry_Code.Text & "' "
        ErrMsg = "Failed when read Material Origin Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        txtBea_Masuk.Text = "0"
        txtBea_Masuk_Tambahan.Text = "0"
        txtPPN.Text = "0"
        txtPPH_Bea_Masuk.Text = "0"
        txtPPH_21.Text = "0"
        txtPIUD_TR.Text = "0"

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtBea_Masuk.Text = MyReader.GetString("BEA_MASUK")
                Catch ex As Exception
                End Try
                Try
                    txtBea_Masuk_Tambahan.Text = MyReader.GetString("BEA_MASUK_TAMBAHAN")
                Catch ex As Exception
                End Try
                Try
                    txtPPN.Text = MyReader.GetString("PPN")
                Catch ex As Exception
                End Try
                Try
                    txtPPH_Bea_Masuk.Text = MyReader.GetString("BEA_MASUK_TAMBAHAN")
                Catch ex As Exception
                End Try
                Try
                    txtPPH_21.Text = MyReader.GetString("PPH_21")
                Catch ex As Exception
                End Try
                Try
                    txtPPN_Status.Text = MyReader.GetString("PPN_STATUS")
                Catch ex As Exception
                End Try
                Try
                    txtPIUD_TR.Text = MyReader.GetString("PIUD_TR")
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                ''txtMaterial_Code.Enabled = True
                ''txtCountry_Code.Enabled = True
                CloseMyReader(MyReader, UserData)

                btnDelete.Enabled = (Len(Trim(txtMaterial_Code.Text)) > 0) And (Len(Trim(txtCountry_Code.Text)) > 0)
            Else
                CloseMyReader(MyReader, UserData)
                baru = False
                edit = True
                ''txtMaterial_Code.Enabled = False
                ''txtCountry_Code.Enabled = False

                btnDelete.Enabled = (Len(Trim(txtMaterial_Code.Text)) > 0) And (Len(Trim(txtCountry_Code.Text)) > 0)
            End If
        End If
        'Exit Function
    End Sub



    Private Sub btnSearchMat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchMat.Click
        PilihanDlg.Text = "Select Material Code"
        PilihanDlg.LblKey1.Text = "Material Code"
        PilihanDlg.LblKey2.Text = "Material Name"
        PilihanDlg.SQLGrid = "select MATERIAL_CODE as MaterialCode, GROUP_CODE as GroupCode, MATERIAL_NAME as MaterialName, MATERIAL_SHORTNAME as ShortName, HS_CODE as HSCode, REGISTER_NO as RegisterNo, ZAT_ACTIVE as ZatAcitve, KELOMPOK_OBAT_HEWAN as KelompokObatHewan from tbm_material "
        PilihanDlg.SQLFilter = "select MATERIAL_CODE as MaterialCode, GROUP_CODE as GroupCode, MATERIAL_NAME as MaterialName, MATERIAL_SHORTNAME as ShortName, HS_CODE as HSCode, REGISTER_NO as RegisterNo, ZAT_ACTIVE as ZatAcitve, KELOMPOK_OBAT_HEWAN as KelompokObatHewan from tbm_material " & _
                               "WHERE material_code LIKE 'FilterData1%' " & _
                               " and material_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtMaterial_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Call ListData(txtMaterial_Code.Text, txtCountry_Code.Text)
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub txtBea_Masuk_Tambahan_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBea_Masuk_Tambahan.LostFocus
        Try
            If txtBea_Masuk_Tambahan.Text <> "" Then
                txtBea_Masuk_Tambahan.Text = FormatNumber(txtBea_Masuk_Tambahan.Text, 2)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
            txtBea_Masuk_Tambahan.Text = ""
            txtBea_Masuk_Tambahan.Focus()
        End Try

    End Sub
    Private Sub txtBea_Masuk_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBea_Masuk.LostFocus
        Try
            If txtBea_Masuk.Text <> "" Then
                txtBea_Masuk.Text = FormatNumber(txtBea_Masuk.Text, 2)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
            txtBea_Masuk.Text = ""
            txtBea_Masuk.Focus()
        End Try

    End Sub
    Private Sub txtPPH_Bea_Masuk_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPH_Bea_Masuk.LostFocus
        Try
            If txtPPH_Bea_Masuk.Text <> "" Then
                txtPPH_Bea_Masuk.Text = FormatNumber(txtPPH_Bea_Masuk.Text, 2)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
            txtPPH_Bea_Masuk.Text = ""
            txtPPH_Bea_Masuk.Focus()
        End Try
    End Sub
    Private Sub txtPPN_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPN.LostFocus
        Try
            If txtPPN.Text <> "" Then
                txtPPN.Text = FormatNumber(txtPPN.Text, 2)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
            txtPPN.Text = ""
            txtPPN.Focus()
        End Try
    End Sub
    Private Sub txtPPH_21_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPH_21.LostFocus
        Try
            If txtPPH_21.Text <> "" Then
                txtPPH_21.Text = FormatNumber(txtPPH_21.Text, 2)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
            txtPPH_21.Text = ""
            txtPPH_21.Focus()
        End Try
    End Sub
    Private Sub txtPIUD_TR_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPIUD_TR.LostFocus
        Try
            If txtPIUD_TR.Text <> "" Then
                txtPIUD_TR.Text = FormatNumber(txtPIUD_TR.Text, 2)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
            txtPIUD_TR.Text = ""
            txtPIUD_TR.Focus()
        End Try

    End Sub
    Private Sub txtPPN_Status_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPPN_Status.LostFocus
        Try
            If txtPPN_Status.Text <> "" Then
                txtPPN_Status.Text = FormatNumber(txtPPN_Status.Text, 2)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Input valid numeric", MsgBoxStyle.Critical, "Error")
            txtPPN_Status.Text = ""
            txtPPN_Status.Focus()
        End Try

    End Sub

    Private Sub txtBea_Masuk_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBea_Masuk.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtBea_Masuk_Tambahan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBea_Masuk_Tambahan.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtPPN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPPN.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtPPH_Bea_Masuk_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPPH_Bea_Masuk.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtPPH_21_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPPH_21.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub

    Private Sub txtPPN_Status_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPPN_Status.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub

    Private Sub txtPIUD_TR_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPIUD_TR.KeyPress
        If (IsNumeric(e.KeyChar)) Or e.KeyChar.ToString = "," Or e.KeyChar.ToString = "." Or Asc(e.KeyChar) = 8 Then
        Else
            e.KeyChar = ""
        End If
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub
End Class