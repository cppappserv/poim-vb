

'Title        : Master Data Lines
'Form         : FM24_Line
'Created By   : Prie
'Created Date : 23 September 2008
'Table Used   : tbm_lines, tbm_city
'modified by  : Hanny - 5 nov 08

Imports poim.FM02_MaterialGroup
Public Class FM24_Line
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_idtable As String = "Lines"

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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tl.LINE_CODE as LineCode, tl.LINE_NAME as LineName, tl.ADDRESS as Address, tl.CITY_CODE as CityCode, tc.City_name as CityName, tl.PHONE as Phone, tl.FAX as Fax from tbm_lines as tl inner join tbm_city as tc on tl.city_code = tc.city_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtLine_Code.Enabled = True
        txtLine_Code.Clear()
        txtLine_Name.Clear()

        txtCity_Code.Clear()
        txtPhone.Clear()
        txtAddress.Clear()
        txtFax.Clear()


        txtLine_Code.Focus()
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

        SQLstr = "DELETE from tbm_lines " & _
                 "where line_code='" & txtLine_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_lines")
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_lines where line_code='" & txtLine_Code.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtLine_Code.Focus()
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
            SQLstr = "INSERT INTO tbm_lines (LINE_CODE,LINE_NAME,ADDRESS," & _
                     "CITY_CODE,PHONE,FAX) " & _
                     "VALUES ('" & txtLine_Code.Text & "', '" & txtLine_Name.Text & "', '" & txtAddress.Text & "', '" & txtCity_Code.Text & "', " & _
                              "'" & txtPhone.Text & "', '" & txtFax.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_lines " & _
                     "SET LINE_NAME = '" & txtLine_Name.Text & "'," & _
                     "ADDRESS = '" & txtAddress.Text & "'," & _
                     "CITY_CODE = '" & txtCity_Code.Text & "'," & _
                     "PHONE = '" & txtPhone.Text & "', " & _
                     "FAX = '" & txtFax.Text & "' " & _
                     "where LINE_CODE='" & txtLine_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_lines")
        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(txtLine_Code.Text)) > 0) And (Len(Trim(txtLine_Name.Text)) > 0)
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
        txtLine_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtLine_Name.Text = DataGridView1.Item(1, brs).Value.ToString
        txtAddress.Text = DataGridView1.Item(2, brs).Value.ToString
        txtCity_Code.Text = DataGridView1.Item(3, brs).Value.ToString
        txtPhone.Text = DataGridView1.Item(5, brs).Value.ToString
        txtFax.Text = DataGridView1.Item(6, brs).Value.ToString


        txtLine_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtLine_Code.Text)) > 0)
    End Sub

    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city"
        PilihanDlg.SQLFilter = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' " & _
                               " and city_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCity_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCity.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub f_getdata()

        SQLstr = "select * from tbm_lines where line_code = '" & txtLine_Code.Text & "' "
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtLine_Name.Text = ""
            txtAddress.Text = ""
            txtCity_Code.Text = ""
            txtPhone.Text = ""
            txtFax.Text = ""
            While MyReader.Read
                Try
                    txtLine_Name.Text = MyReader.GetString("LINE_NAME")
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
                    txtAddress.Text = MyReader.GetString("ADDRESS")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtLine_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtLine_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtLine_Code.Enabled = False
                'txtPayment_Name.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtLine_Code.Text)) > 0)
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


    'Private Sub txtPayment_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayment_Name.TextChanged
    '    RefreshTombolSave()
    'End Sub

    Private Sub txtLine_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLine_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtLine_Code.Text)) > 0) And (Len(Trim(txtLine_Name.Text)) > 0)
        'f_getdata()

    End Sub

    Private Sub txtLine_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLine_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtLine_Code.Text)) > 0) And (Len(Trim(txtLine_Name.Text)) > 0)
    End Sub

    Private Sub txtCity_Code_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCity_Code.TextChanged
        lblCity.Text = AmbilData("CITY_NAME", "tbm_city", "CITY_CODE='" & txtCity_Code.Text & "'")
    End Sub


End Class