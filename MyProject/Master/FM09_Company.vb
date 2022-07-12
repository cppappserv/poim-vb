

'Title        : Master Data Company
'Form         : FM09_Company
'Created By   : Hanny
'Created Date : 23 September 2008
'Table Used   : tbm_company, tbm_city

Imports POIM.FM02_MaterialGroup
Public Class FM09_Company
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_idtable As String = "Company"

    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        btnSave.Enabled = False
    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()
        Dim brs As Integer

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT tc.COMPANY_CODE as CompanyCode, tc.COMPANY_NAME as CompanyName, tc.COMPANY_SHORTNAME as Shortname, tc.ABBREVIATION as Abbreviation, tc.NPWP, tc.IZIN_PERUSAHAAN as IzinPerusahaan, tc.API_U_APIT_NO as APIT_No, tc.IZIN_DEPTAN_NO as IzinDeptan, tc.ADDRESS as Address, tc.CITY_CODE as CityCode, ty.city_name as CityName, tc.PHONE as Phone, tc.FAX as Fax, tc.AUTHORIZE_PERSON as AuthorizePerson FROM tbm_company as tc inner join tbm_city as ty on tc.city_code = ty.city_code order by tc.company_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtCompany_Code.Enabled = True
        txtCompany_Code.Clear()
        txtCompany_Name.Clear()
        txtShort_Name.Clear()
        txtAbbreviation.Clear()
        txtNPWP.Clear()
        txtIzin_Perusahaan.Clear()

        txtAPI.Clear()
        txtIzin_Deptan_No.Clear()
        txtAddress.Clear()
        txtCity_Code.Clear()
        txtPhone.Clear()
        txtFax.Clear()
        txtAuthorize_Person.Clear()

        txtCompany_Code.Focus()
        baru = True
        edit = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Close()
        'Dispose()
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_company " & _
                 "where company_code='" & txtCompany_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
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
        SQLstr = "Select * from tbm_company where company_code='" & txtCompany_Code.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtCompany_Code.Focus()
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

        If baru Then
            If CekData() = False Then Exit Sub
            teks = "Save Data"
            ErrMsg = "Failed when saving " & v_idtable & " data"
            SQLstr = "INSERT INTO tbm_company (COMPANY_CODE,COMPANY_NAME,COMPANY_SHORTNAME,ABBREVIATION," & _
                     "NPWP,IZIN_PERUSAHAAN,API_U_APIT_NO,IZIN_DEPTAN_NO,ADDRESS," & _
                     "CITY_CODE,PHONE,FAX,AUTHORIZE_PERSON) " & _
                     "VALUES ('" & txtCompany_Code.Text & "', '" & txtCompany_Name.Text & "', '" & txtShort_Name.Text & "', '" & txtAbbreviation.Text & "', " & _
                              "'" & txtNPWP.Text & "', '" & txtIzin_Perusahaan.Text & "', '" & txtAPI.Text & "', '" & txtIzin_Deptan_No.Text & "', '" & txtAddress.Text & "', " & _
                              "'" & txtCity_Code.Text & "', '" & txtPhone.Text & "', '" & txtFax.Text & "', '" & txtAuthorize_Person.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_company " & _
                     "SET COMPANY_NAME = '" & txtCompany_Name.Text & "'," & _
                     "COMPANY_SHORTNAME = '" & txtShort_Name.Text & "'," & _
                     "ABBREVIATION = '" & txtAbbreviation.Text & "'," & _
                     "NPWP = '" & txtNPWP.Text & "'," & _
                     "IZIN_PERUSAHAAN = '" & txtIzin_Perusahaan.Text & "'," & _
                     "API_U_APIT_NO = '" & txtAPI.Text & "', " & _
                     "IZIN_DEPTAN_NO = '" & txtIzin_Deptan_No.Text & "', " & _
                     "ADDRESS = '" & txtAddress.Text & "', " & _
                     "CITY_CODE = '" & txtCity_Code.Text & "', " & _
                     "PHONE = '" & txtPhone.Text & "', " & _
                     "FAX = '" & txtFax.Text & "', " & _
                     "AUTHORIZE_PERSON = '" & txtAuthorize_Person.Text & "' " & _
                     "where COMPANY_CODE='" & txtCompany_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_company")
        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(txtCompany_Code.Text)) > 0) And (Len(Trim(txtCompany_Name.Text)) > 0)
        'And _
        '(Len(Trim(txtBea_Masuk.Text)) > 0) And (Len(Trim(txtBea_Masuk_Tambahan.Text)) > 0) And _
        '(Len(Trim(txtPPN.Text)) > 0) And (Len(Trim(txtPPH_Bea_Masuk.Text)) > 0) And _
        '(Len(Trim(txtPPH_21.Text)) > 0) And (Len(Trim(txtPPN_Status.Text)) > 0) And _
        '(Len(Trim(txtPIUD_TR.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtCompany_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtCompany_Name.Text = CStr(IIf(IsDBNull(DataGridView1.Item(1, brs).Value), "", DataGridView1.Item(1, brs).Value))
        txtShort_Name.Text = CStr(IIf(IsDBNull(DataGridView1.Item(2, brs).Value), "", DataGridView1.Item(2, brs).Value))
        txtAbbreviation.Text = CStr(IIf(IsDBNull(DataGridView1.Item(3, brs).Value), "", DataGridView1.Item(3, brs).Value))
        txtNPWP.Text = CStr(IIf(IsDBNull(DataGridView1.Item(4, brs).Value), "", DataGridView1.Item(4, brs).Value))
        txtIzin_Perusahaan.Text = CStr(IIf(IsDBNull(DataGridView1.Item(5, brs).Value), "", DataGridView1.Item(5, brs).Value))
        txtAPI.Text = CStr(IIf(IsDBNull(DataGridView1.Item(6, brs).Value), "", DataGridView1.Item(6, brs).Value))
        txtIzin_Deptan_No.Text = CStr(IIf(IsDBNull(DataGridView1.Item(7, brs).Value), "", DataGridView1.Item(7, brs).Value))
        txtAddress.Text = CStr(IIf(IsDBNull(DataGridView1.Item(8, brs).Value), "", DataGridView1.Item(8, brs).Value))

        txtCity_Code.Text = CStr(IIf(IsDBNull(DataGridView1.Item(9, brs).Value), "", DataGridView1.Item(9, brs).Value))
        txtPhone.Text = CStr(IIf(IsDBNull(DataGridView1.Item(11, brs).Value), "", DataGridView1.Item(11, brs).Value))
        txtFax.Text = CStr(IIf(IsDBNull(DataGridView1.Item(12, brs).Value), "", DataGridView1.Item(12, brs).Value))
        txtAuthorize_Person.Text = CStr(IIf(IsDBNull(DataGridView1.Item(13, brs).Value), "", DataGridView1.Item(13, brs).Value))

        txtCompany_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtCompany_Code.Text)) > 0)
    End Sub



    Private Sub f_getdata()

        SQLstr = "select * from tbm_city where company_code = '" & txtCompany_Code.Text & "' "
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtCompany_Name.Text = ""
            txtShort_Name.Text = ""
            txtAbbreviation.Text = ""
            txtNPWP.Text = ""
            txtIzin_Perusahaan.Text = ""
            txtAPI.Text = ""
            txtIzin_Deptan_No.Text = ""
            txtAddress.Text = ""
            txtCity_Code.Text = ""
            txtPhone.Text = ""
            txtFax.Text = ""
            txtAuthorize_Person.Text = ""
            While MyReader.Read
                Try
                    txtCompany_Name.Text = MyReader.GetString("COMPANY_NAME")
                Catch ex As Exception
                End Try
                Try
                    txtShort_Name.Text = MyReader.GetString("COMPANY_SHORTNAME")
                Catch ex As Exception
                End Try
                Try
                    txtAbbreviation.Text = MyReader.GetString("ABBREVIATION")
                Catch ex As Exception
                End Try
                Try
                    txtNPWP.Text = MyReader.GetString("NPWP")
                Catch ex As Exception
                End Try
                Try
                    txtIzin_Perusahaan.Text = MyReader.GetString("IZIN_PERUSAHAAN")
                Catch ex As Exception
                End Try
                Try
                    txtAPI.Text = MyReader.GetString("API_U_APIT_NO")
                Catch ex As Exception
                End Try
                Try
                    txtIzin_Deptan_No.Text = MyReader.GetString("IZIN_DEPTAN_NO")
                Catch ex As Exception
                End Try
                Try
                    txtAddress.Text = MyReader.GetString("ADDRESS")
                Catch ex As Exception
                End Try
                Try
                    txtCity_Code.Text = MyReader.GetString("CITY_CODE")
                Catch ex As Exception
                End Try
                Try
                    txtPhone.Text = MyReader.GetString("PHONE")
                Catch ex As Exception
                End Try
                Try
                    txtFax.Text = MyReader.GetString("FAX")
                Catch ex As Exception
                End Try
                Try
                    txtAuthorize_Person.Text = MyReader.GetString("AUTHORIZE_PERSON")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtCompany_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtCompany_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtCompany_Code.Enabled = False
                txtCompany_Name.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtCompany_Code.Text)) > 0)
            End If
            CloseMyReader(MyReader, UserData)
        End If
        'Exit Function
    End Sub



    'Private Sub btnSearchComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    PilihanDlg.Text = "Select Company Code"
    '    PilihanDlg.LblKey1.Text = "Company Code"
    '    PilihanDlg.SQLGrid = "SELECT * FROM tbm_company"
    '    PilihanDlg.SQLFilter = "SELECT * FROM tbm_company " & _
    '                           "WHERE Company_code LIKE '%FilterData1%' "
    '    PilihanDlg.Tables = "tbm_company"
    '    If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '        txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    '    End If
    'End Sub


    Private Sub txtCompany_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompany_Name.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub txtCompany_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompany_Code.TextChanged
        RefreshTombolSave()
    End Sub


    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "SELECT CITY_CODE as CityCode, CITY_NAME as CityName, COUNTRY_CODE as CountryCode FROM tbm_city"
        PilihanDlg.SQLFilter = "SELECT CITY_CODE as CityCode, CITY_NAME as CityName, COUNTRY_CODE as CountryCode FROM tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' " & _
                               " and city_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCity_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString            
        End If
    End Sub

    Private Sub txtCity_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity_Code.TextChanged
        lblCityName.Text = AmbilData("CITY_NAME", "tbm_city", "CITY_CODE='" & txtCity_Code.Text & "'")

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub txtAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress.TextChanged

    End Sub
End Class