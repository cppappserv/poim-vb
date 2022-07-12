'Created  by   : Priehanto
'Date Written  : 23/09/2008
'Last Modified : 24/09/2008
'Modified by   : Hanny -  04/11/2008

Public Class FM19_Plant
    Dim PilihanDlg As New DlgPilihan
    Dim NEW_ As Boolean
    Dim MyReader As MySqlDataReader
    Dim SQLStr, ErrMsg As String
    '
    Dim baru As Boolean
    Dim edit As Boolean
    Dim affrow As Integer
    Dim v_idtable As String = "Company"

    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        btnSave.Enabled = False
    End Sub

    Private Sub key_off(Optional ByVal _off As Boolean = False)
        TxtPlantCode.Enabled = _off
        TxtPlantName.Enabled = _off
        TxtAddress.Enabled = _off
        TxtCityCode.Enabled = _off
        TxtCompanyCode.Enabled = _off
        TxtFax.Enabled = _off
        TxtPhone.Enabled = _off
        BttnCityHelp.Enabled = _off
        BttnCompanyHelp.Enabled = _off
    End Sub

    Private Sub key_on(Optional ByVal _on As Boolean = True)
        TxtPlantCode.Enabled = _on
        TxtPlantName.Enabled = _on
        TxtAddress.Enabled = _on
        TxtCityCode.Enabled = _on
        TxtCompanyCode.Enabled = _on
        TxtFax.Enabled = _on
        TxtPhone.Enabled = _on
    End Sub


    Private Sub FM19_Plant_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen()

    End Sub
    Private Sub RefreshScreen()
        Dim brs As Integer

        cb1.Checked = True
        cb2.Checked = False
        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tp.PLANT_CODE as PlantCode, tp.PLANT_NAME as PlantName, tp.ADDRESS as Address, tp.CITY_CODE as CityCode, tc.city_name as CityName, tp.PHONE as Phone, tp.FAX as Fax, tp.COMPANY_CODE  as CompanyCode, ty.company_name as CompanyName, if (tp.Tax_Rounded='1','Rounded','Not Rounded') as TaxStatus from tbm_plant as tp inner join tbm_city as tc on tp.city_code = tc.city_code inner join tbm_company as ty on tp.company_code = ty.company_code order by tp.plant_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        TxtPlantCode.Enabled = True
        TxtPlantCode.Clear()
        TxtPlantName.Clear()
        TxtAddress.Clear()
        TxtCityCode.Clear()
        TxtPhone.Clear()
        TxtFax.Clear()
        TxtCompanyCode.Clear()
        TxtPlantCode.Focus()
        baru = True
        edit = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLStr = "DELETE from tbm_plant WHERE plant_code='" & TxtPlantCode.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        AffRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
        If AffRow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_company")
        End If

    End Sub

    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLStr = "Select * from tbm_plant where plant_code='" & TxtPlantCode.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TxtPlantCode.Focus()
            Exit Function
        End If

        ''Foreign Key
        'SQLstr = "Select * from tbm_country where country_code='" & txtCompany_Name.Text & "'"
        'If FM02_MaterialGroup.DataOK(SQLstr) = True Then
        '    MsgBox("Country code does not exist! ", MsgBoxStyle.Critical, "Warning")
        '    CekData = False
        '    txtCompany_Name.Focus()
        '    Exit Function
        'End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If cb1.Checked = False And cb2.Checked = False Then
            MsgBox("Please select tax status")
            Exit Sub
        End If
        If baru Then
            If CekData() = False Then Exit Sub
            teks = "Save Data"
            ErrMsg = "Failed when saving " & v_idtable & " data"
            SQLStr = "INSERT INTO tbm_plant (plant_code, plant_name, address, city_code, phone, fax, company_code, tax_rounded) " & _
                     "VALUES ('" & TxtPlantCode.Text & "', '" & TxtPlantName.Text & "', '" & TxtAddress.Text & "', '" & TxtCityCode.Text & "', " & _
                     "'" & TxtPhone.Text & "','" & TxtFax.Text & "','" & TxtCompanyCode.Text & "','" & If(cb1.Checked, 0, 1) & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLStr = "UPDATE tbm_plant SET plant_code='" & TxtPlantCode.Text & "', plant_name='" & TxtPlantName.Text & "', " & _
                     "address='" & TxtAddress.Text & "', city_code='" & TxtCityCode.Text & "', phone='" & TxtPhone.Text & "', tax_rounded=" & If(cb1.Checked, 0, 1) & _
                     ",fax='" & TxtFax.Text & "', company_code='" & TxtCompanyCode.Text & "' WHERE plant_code='" & TxtPlantCode.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_company")
        End If


    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer
        Dim temp As String

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        TxtPlantCode.Text = DataGridView1.Item(0, brs).Value.ToString
        TxtPlantName.Text = CStr(IIf(IsDBNull(DataGridView1.Item(1, brs).Value), "", DataGridView1.Item(1, brs).Value))
        TxtAddress.Text = CStr(IIf(IsDBNull(DataGridView1.Item(2, brs).Value), "", DataGridView1.Item(2, brs).Value))
        TxtCityCode.Text = CStr(IIf(IsDBNull(DataGridView1.Item(3, brs).Value), "", DataGridView1.Item(3, brs).Value))
        TxtPhone.Text = CStr(IIf(IsDBNull(DataGridView1.Item(5, brs).Value), "", DataGridView1.Item(5, brs).Value))
        TxtFax.Text = CStr(IIf(IsDBNull(DataGridView1.Item(6, brs).Value), "", DataGridView1.Item(6, brs).Value))
        TxtCompanyCode.Text = CStr(IIf(IsDBNull(DataGridView1.Item(7, brs).Value), "", DataGridView1.Item(7, brs).Value))
        temp = DataGridView1.Item(9, brs).Value
        If temp = "Not Rounded" Then
            cb1.Checked = True
        Else
            cb2.Checked = True
        End If
        TxtPlantCode.Enabled = False
        '        BttnHelp.Visible = True
        btnDelete.Enabled = (Len(Trim(TxtPlantCode.Text)) > 0)
    End Sub

    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(TxtPlantCode.Text)) > 0) And (Len(Trim(TxtPlantName.Text)) > 0)
    End Sub

    Private Sub Start_()
        clear_()
        key_off()
        NEW_ = False
        btnNew.Enabled = True
        'ToolStripButtonCancel.Enabled = False
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnClose.Enabled = True
        BttnHelp.Enabled = True
    End Sub
    Private Sub clear_()
        TxtPlantCode.Text = ""
        TxtPlantName.Text = ""
        TxtAddress.Text = ""
        TxtCityCode.Text = ""
        TxtCompanyCode.Text = ""
        TxtFax.Text = ""
        TxtPhone.Text = ""
        LblCity.Text = ""
        LblCompany.Text = ""
    End Sub


    'Private Sub ToolStripButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Start_()
    'End Sub

    Function CekInput(ByVal nControl As Control, ByVal nPesan As String) As Boolean
        If nControl.Text = "" Or nControl.Text = "0" Then
            MsgBox(nPesan & " Tidak Boleh Kosong...!", vbInformation, "PERHATIAN")
            nControl.Focus()
            CekInput = False
        Else
            CekInput = True
        End If
    End Function

    
    Function CHECK_PLANT() As Boolean
        SQLStr = "SELECT plant_code FROM tbm_plant " & _
                 "WHERE plant_code = '" & TxtPlantCode.Text & "'"
        ErrMsg = "Gagal baca data master plant..."
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return False
            End While
            CloseMyReader(MyReader, UserData)
        End If
        Return True
    End Function

    Private Sub DataGridViewRefresh()
        Dim SQLGrid, Tables, ErrMsg As String
        SQLGrid = "SELECT plant_code AS 'PLANT CODE', plant_name AS 'PLANT NAME'" & _
                  ",address as 'ADDRESS', city_code as 'CITY CODE', phone as 'PHONE', fax as 'FAX'" & _
                  ",company_code as 'COMPANY CODE'" & _
                  " FROM tbm_plant ORDER BY plant_code ASC"
        Tables = "tbm_plant"
        ErrMsg = "Gagal baca data master plant."
        DataGridView1.ReadOnly = True
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DataGridView1.BorderStyle = BorderStyle.Fixed3D
        DataGridView1.DataSource = DBQueryDataTable(SQLGrid, MyConn, Tables, ErrMsg, UserData)
    End Sub

    Private Sub TxtCityCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        SQLStr = "SELECT city_code,city_name FROM tbm_city " & _
                 "WHERE city_code = '" & TxtCityCode.Text & "'"
        ErrMsg = "Gagal baca data master city..."
        If TxtCityCode.Text <> "" Then
            MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                If MyReader.HasRows Then
                    MyReader.Read()
                    Try
                        TxtCityCode.Text = MyReader.GetString("city_code")
                    Catch ex As Exception
                    End Try
                    Try
                        LblCity.Text = MyReader.GetString("city_name")
                    Catch ex As Exception
                    End Try
                Else
                    MsgBox("City Code tidak ada di tabel Master City...", MsgBoxStyle.Information, "Input Data Master City")
                    TxtCityCode.Text = ""
                    TxtCityCode.Focus()
                End If
                CloseMyReader(MyReader, UserData)
            End If
        End If
    End Sub

    Private Sub TxtCompanyCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        SQLStr = "SELECT company_code,company_name FROM tbm_company " & _
         "WHERE company_code = '" & TxtCompanyCode.Text & "'"
        ErrMsg = "Gagal baca data master company..."
        If TxtCompanyCode.Text <> "" Then
            MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                If MyReader.HasRows Then
                    MyReader.Read()
                    Try
                        TxtCompanyCode.Text = MyReader.GetString("company_code")
                    Catch ex As Exception
                    End Try
                    Try
                        LblCompany.Text = MyReader.GetString("company_name")
                    Catch ex As Exception
                    End Try
                Else
                    MsgBox("Company Code tidak ada di tabel Master Company...", MsgBoxStyle.Information, "Input Data Master City")
                    TxtCompanyCode.Text = ""
                    TxtCompanyCode.Focus()
                End If
                CloseMyReader(MyReader, UserData)
            End If
        End If
    End Sub


    Function DESC_CODE(ByVal mTypeCode As String) As String
        DESC_CODE = ""
        If mTypeCode = "CITY" Then
            SQLStr = "SELECT city_name FROM tbm_city " & _
                     "WHERE city_code = '" & TxtCityCode.Text & "'"
            ErrMsg = "Gagal baca data master city..."
            MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                If MyReader.HasRows Then
                    MyReader.Read()
                    Try
                        DESC_CODE = MyReader.GetString("city_name")
                    Catch ex As Exception
                        DESC_CODE = ""
                    End Try
                End If
                CloseMyReader(MyReader, UserData)
            End If
        ElseIf mTypeCode = "COMPANY" Then
            SQLStr = "SELECT company_name FROM tbm_company " & _
                     "WHERE company_code = '" & TxtCompanyCode.Text & "'"
            ErrMsg = "Gagal baca data master company..."
            MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                If MyReader.HasRows Then
                    MyReader.Read()
                    Try
                        DESC_CODE = MyReader.GetString("company_name")
                    Catch ex As Exception
                        DESC_CODE = ""
                    End Try
                End If
                CloseMyReader(MyReader, UserData)
            End If
        End If
        Return (DESC_CODE)
    End Function



    Private Sub TxtCityCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCityCode.TextChanged
        LblCity.Text = AmbilData("CITY_NAME", "tbm_city", "CITY_CODE='" & TxtCityCode.Text & "'")
        RefreshTombolSave()
    End Sub



    Private Sub TxtCompanyCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCompanyCode.TextChanged
        LblCompany.Text = AmbilData("COMPANY_NAME", "tbm_company", "COMPANY_CODE='" & TxtCompanyCode.Text & "'")
        RefreshTombolSave()
    End Sub



    
    Private Sub BttnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnHelp.Click
        PilihanDlg.Text = "Select Plant Code"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, COMPANY_CODE  as CompanyCode from tbm_plant"
        PilihanDlg.SQLFilter = "select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, COMPANY_CODE  as CompanyCode from tbm_plant " & _
                               " WHERE Plant_code LIKE 'FilterData1%' " & _
                               " and plant_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_plant"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtPlantCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub BttnCityHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnCityHelp.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city Where country_code='ID'"
        PilihanDlg.SQLFilter = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' and city_name  LIKE 'FilterData2%' and country_code='ID'"
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtCityCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            LblCity.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub


    Private Sub BttnCompanyHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnCompanyHelp.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"
        PilihanDlg.SQLGrid = "select COMPANY_CODE as CompanyCode, COMPANY_NAME as CompanyName from tbm_company"
        PilihanDlg.SQLFilter = "select COMPANY_CODE as CompanyCode, COMPANY_NAME as CompanyName from tbm_company " & _
                               "WHERE company_code LIKE 'FilterData1%' " & _
                               " and company_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtCompanyCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            LblCompany.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub TxtPlantCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPlantCode.TextChanged
        LblCity.Text = AmbilData("PLANT_NAME", "tbm_plant", "PLANT_CODE='" & TxtPlantCode.Text & "'")
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub TxtPlantName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPlantName.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TxtAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAddress.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TxtPhone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPhone.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TxtFax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFax.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub cb1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb1.CheckedChanged
        If cb1.Checked Then cb2.Checked = False
    End Sub

    Private Sub cb2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb2.CheckedChanged
        If cb2.Checked Then cb1.Checked = False
    End Sub
End Class