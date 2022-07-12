


'Title        : Master Data Port
'Form         : FM20_Port
'Created By   : Hanny
'Created Date : 25 September 2008
'Table Used   : tbm_port

Imports POIM.FM02_MaterialGroup
Public Class FM20_Port
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_idtable As String = "Port"

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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tp.PORT_CODE as PortCode, tp.PORT_NAME as PortName, tp.PORT_STATUS as PortStatus, tp.DJBC_CODE as DBJC_Code, tp.CITY_CODE as CityCode, tc.city_name as CityName, tp.COUNTRY_CODE as CountryCode, tco.country_name as CountryName, tp.LT_CODE as LTCode from tbm_port as tp inner join tbm_city as tc on tp.city_code = tc.city_code inner join tbm_country as tco on tp.country_code = tco.country_code order by tp.port_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtPort_Code.Enabled = True
        txtPort_Code.Clear()
        txtPort_Name.Clear()
        txtPort_Status.Clear()
        txtDJBC_Code.Clear()
        txtCity_Code.Clear()
        txtCountry_Code.Clear()
        txtLT_Code.Clear()


        txtPort_Code.Focus()
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

        SQLstr = "DELETE from tbm_port " & _
                 "where port_code='" & txtPort_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_port")
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_port where port_code='" & txtPort_Code.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtPort_Code.Focus()
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
            SQLstr = "INSERT INTO tbm_port (PORT_CODE,PORT_NAME,PORT_STATUS,DJBC_CODE," & _
                     "COUNTRY_CODE,CITY_CODE,LT_CODE) " & _
                     "VALUES ('" & txtPort_Code.Text & "', '" & txtPort_Name.Text & "', '" & txtPort_Status.Text & "', '" & txtDJBC_Code.Text & "', " & _
                              "'" & txtCountry_Code.Text & "', '" & txtCity_Code.Text & "', '" & txtLT_Code.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_port " & _
                     "SET PORT_NAME = '" & txtPort_Name.Text & "'," & _
                     "PORT_STATUS = '" & txtPort_Status.Text & "'," & _
                     "DJBC_CODE = '" & txtDJBC_Code.Text & "'," & _
                     "COUNTRY_CODE = '" & txtCountry_Code.Text & "'," & _
                     "CITY_CODE = '" & txtCity_Code.Text & "'," & _
                     "LT_CODE = '" & txtLT_Code.Text & "' " & _
                     "where PORT_CODE ='" & txtPort_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_port")
        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(txtPort_Code.Text)) > 0) And (Len(Trim(txtPort_Name.Text)) > 0)
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
        txtPort_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtPort_Name.Text = CStr(IIf(IsDBNull(DataGridView1.Item(1, brs).Value), "", DataGridView1.Item(1, brs).Value))
        txtPort_Status.Text = CStr(IIf(IsDBNull(DataGridView1.Item(2, brs).Value), "", DataGridView1.Item(2, brs).Value))
        txtDJBC_Code.Text = CStr(IIf(IsDBNull(DataGridView1.Item(3, brs).Value), "", DataGridView1.Item(3, brs).Value))
        txtCity_Code.Text = CStr(IIf(IsDBNull(DataGridView1.Item(4, brs).Value), "", DataGridView1.Item(4, brs).Value))
        txtCountry_Code.Text = CStr(IIf(IsDBNull(DataGridView1.Item(6, brs).Value), "", DataGridView1.Item(6, brs).Value))
        txtLT_Code.Text = CStr(IIf(IsDBNull(DataGridView1.Item(8, brs).Value), "", DataGridView1.Item(8, brs).Value))

        txtPort_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtPort_Code.Text)) > 0)
    End Sub



    Private Sub f_getdata()

        SQLstr = "select * from tbm_port where PORT_CODE = '" & txtPort_Code.Text & "' "
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtPort_Name.Text = ""
            txtPort_Status.Text = ""
            txtDJBC_Code.Text = ""
            txtCity_Code.Text = ""
            txtCountry_Code.Text = ""
            txtLT_Code.Text = ""
            While MyReader.Read
                Try
                    txtPort_Name.Text = MyReader.GetString("PORT_NAME")
                Catch ex As Exception
                End Try
                Try
                    txtPort_Status.Text = MyReader.GetString("PORT_STATUS")
                Catch ex As Exception
                End Try
                Try
                    txtDJBC_Code.Text = MyReader.GetString("DJBC_CODE")
                Catch ex As Exception
                End Try
                Try
                    txtCity_Code.Text = MyReader.GetString("COUNTRY_CODE")
                Catch ex As Exception
                End Try
                Try
                    txtCountry_Code.Text = MyReader.GetString("CITY_CODE")
                Catch ex As Exception
                End Try
                Try
                    txtLT_Code.Text = MyReader.GetString("LT_CODE")
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtPort_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtPort_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtPort_Code.Enabled = False
                txtPort_Name.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtPort_Code.Text)) > 0)
            End If
            CloseMyReader(MyReader, UserData)
        End If
        'Exit Function
    End Sub

 

    Private Sub txtCompany_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPort_Name.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub txtCompany_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPort_Code.TextChanged
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
            txtCountry_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
        End If
    End Sub

    Private Sub btnSearchCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCountry.Click
        PilihanDlg.Text = "Select Country Code"
        PilihanDlg.LblKey1.Text = "Country Code"
        PilihanDlg.LblKey2.Text = "Country Name"
        PilihanDlg.SQLGrid = "SELECT Country_code as CountryCode, Country_name as CountryName FROM tbm_country"
        PilihanDlg.SQLFilter = "SELECT Country_code as CountryCode, Country_name as CountryName FROM tbm_country " & _
                               "WHERE country_code LIKE 'FilterData1%' " & _
                               "and country_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_country"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCountry_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub txtCity_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity_Code.TextChanged
        lblCityName.Text = AmbilData("CITY_NAME", "tbm_city", "CITY_CODE='" & txtCity_Code.Text & "'")
    End Sub

    Private Sub txtCountry_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountry_Code.TextChanged
        lblCountryName.Text = AmbilData("COUNTRY_NAME", "tbm_country", "COUNTRY_CODE='" & txtCountry_Code.Text & "'")
    End Sub
End Class