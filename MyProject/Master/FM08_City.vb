'Created  by   : Priehanto
'Date Written  : 23/09/2008
'Modified by   : Hanny 
'Last Modified : 04/11/2008

Imports POIM.FM02_MaterialGroup
Public Class FM08_City
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_idtable As String = "City"
    Dim in_field As String = "tc.city_code as CityCode, tc.city_name as CityName, tc.country_code as CountryCode, ty.country_name as CountryName"
    Dim in_tbl As String = "tbm_city as tc inner join tbm_country as ty on tc.country_code = ty.country_code"


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

        'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_attachment_doc")
        DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)

        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtCity_Code.Enabled = True
        txtCity_Code.Clear()
        txtCity_Name.Enabled = True
        txtCity_Name.Clear()
        txtCountry_Code.Clear()


        txtCity_Code.Focus()
        lblCountryName.Text = ""
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

        SQLstr = "DELETE from tbm_city " & _
                 "where city_code='" & txtCity_Code.Text & "' "

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_attachment_doc")
            'DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_city where city_code='" & txtCity_Code.Text & "' and " & _
                 "city_name = '" & txtCity_Name.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtCity_Code.Focus()
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
            SQLstr = "INSERT INTO tbm_city (CITY_CODE,CITY_NAME,COUNTRY_CODE) " & _
                     "VALUES ('" & txtCity_Code.Text & "', '" & txtCity_Name.Text & "', '" & txtCountry_Code.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_city " & _
                     "SET COUNTRY_CODE = '" & txtCountry_Code.Text & "'," & _
                     "CITY_NAME='" & txtCity_Name.Text & "' and " & _
                     "where CITY_CODE ='" & txtCity_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)



            'DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)


            'SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tpo.po_no = '" & v_pono & "' order by dpo.po_item"
            'ErrMsg = "Datagrid view Failed"
            'dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

            'DGVDetail.DataSource = dts
            'If dts. > 0 Then
            'Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)


        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(txtCity_Code.Text)) > 0) And (Len(Trim(txtCity_Name.Text)) > 0) _
                          And (Len(Trim(txtCountry_Code.Text)) > 0)
        'And (Len(Trim(txtBea_Masuk_Tambahan.Text)) > 0) And _
        '(Len(Trim(txtPPN.Text)) > 0) And (Len(Trim(txtPPH_Bea_Masuk.Text)) > 0) And _
        '(Len(Trim(txtPPH_21.Text)) > 0) And (Len(Trim(txtPPN_Status.Text)) > 0) And _
        '(Len(Trim(txtPIUD_TR.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtCity_Code.Text = DataGridView1.Item(0, brs).Value.ToString

        txtCity_Name.Text = DataGridView1.Item(1, brs).Value.ToString

        txtCountry_Code.Text = DataGridView1.Item(2, brs).Value.ToString
        lblCountryName.Text = DataGridView1.Item(3, brs).Value.ToString

        txtCity_Code.Enabled = False
        txtCity_Name.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtCity_Code.Text)) > 0) And (Len(Trim(txtCity_Name.Text)) > 0)
    End Sub

    Private Sub f_getdata()

        SQLstr = "select * from tbm_city where CITY_CODE = '" & txtCity_Code.Text & "' "
        '"and DOC_CODE = '" & txtCity_Name.Text & "'"
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtCity_Name.Text = ""
            txtCountry_Code.Text = ""
            While MyReader.Read

                Try
                    txtCity_Name.Text = MyReader.GetString("CITY_NAME")
                Catch ex As Exception
                End Try
                Try
                    txtCountry_Code.Text = MyReader.GetString("COUNTRY_CODE")
                Catch ex As Exception
                End Try


            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtCity_Code.Enabled = True
                txtCity_Name.Enabled = True
                txtCountry_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtCity_Code.Text)) > 0) And (Len(Trim(txtCity_Name.Text)) > 0)
            Else
                baru = False
                edit = True
                txtCity_Code.Enabled = False
                txtCity_Name.Enabled = False
                txtCountry_Code.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtCity_Code.Text)) > 0) And (Len(Trim(txtCity_Name.Text)) > 0)
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







    Private Sub txtCity_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtCity_Code.Text)) > 0) And (Len(Trim(txtCity_Name.Text)) > 0)        
    End Sub

    Private Sub txtCity_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtCity_Code.Text)) > 0) And (Len(Trim(txtCity_Name.Text)) > 0)
        f_getdata()
    End Sub


    Private Sub btnSearchCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCountry.Click
        PilihanDlg.Text = "Select Country Code"
        PilihanDlg.LblKey1.Text = "Country Code"
        PilihanDlg.LblKey2.Text = "Country Name"
        PilihanDlg.SQLGrid = "select COUNTRY_CODE as CountryCode, COUNTRY_NAME as CountryName from tbm_country "
        PilihanDlg.SQLFilter = "select COUNTRY_CODE as CountryCode, COUNTRY_NAME as CountryName from tbm_country " & _
                               "WHERE country_code LIKE 'FilterData1%' " & _
                               " and country_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_country"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCountry_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCountryName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub


End Class