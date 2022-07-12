'Title        : Master Produsen
'Form         : FM30_Produsen
'Created By   : YANTI
'Created Date : Agustus 2009
'Table Used   : tbm_produsen

Public Class FM30_Produsen
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
        cityname.Text = ""
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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tm.produsen_CODE as ProdusenCode, tm.Produsen_NAME as ProdusenName, tm.Address, tm.City_Code as CityCode,tg.City_name as CityName, tm.Phone, tm.Fax, tm.Rendring_No as RendringNo from tbm_produsen as tm inner join tbm_city as tg on tm.city_code = tg.city_code order by tm.produsen_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        TextBox1.Enabled = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox1.Focus()
        baru = True
        edit = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
        btnSave.Enabled = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_produsen " & _
                 "where produsen_code='" & TextBox1.Text & "'"

        ErrMsg = "Failed when deleting user data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete user data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_produsen where produsen_code='" & TextBox1.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox("Produsen code already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox1.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            If CekData() = False Then Exit Sub
            teks = "Save Data"
            ErrMsg = "Failed when saving produsen data"
            SQLstr = "INSERT INTO tbm_produsen (produsen_code,produsen_name,address,city_code,phone,fax,rendring_no) " & _
                     "VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & _
                                   TextBox4.Text & "', '" & TextBox5.Text & "', '" & TextBox6.Text & "', '" & TextBox7.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating produsen data"
            SQLstr = "UPDATE tbm_produsen " & _
                     "SET produsen_code = '" & TextBox1.Text & "'," & _
                     "produsen_name = '" & TextBox2.Text & "'," & _
                     "address = '" & TextBox3.Text & "'," & _
                     "city_code = '" & TextBox4.Text & "'," & _
                     "phone = '" & TextBox5.Text & "'," & _
                     "fax = '" & TextBox6.Text & "'," & _
                     "rendring_no = '" & TextBox7.Text & "'" & _
                     "where produsen_code='" & TextBox1.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
        End If
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        TextBox1.Text = DataGridView1.Item(0, brs).Value.ToString
        TextBox2.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox3.Text = DataGridView1.Item(2, brs).Value.ToString
        TextBox4.Text = DataGridView1.Item(3, brs).Value.ToString
        TextBox5.Text = DataGridView1.Item(5, brs).Value.ToString
        TextBox6.Text = DataGridView1.Item(6, brs).Value.ToString
        TextBox7.Text = DataGridView1.Item(7, brs).Value.ToString
        TextBox1.Enabled = False
        btnDelete.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        TextBox2.Focus()
    End Sub
    Private Sub f_getdata()
        Dim teks As String = ""

        SQLstr = "select * from tbm_produsen where produsen_code = '" & TextBox1.Text & "' "
        ErrMsg = "Failed when read Users Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    TextBox1.Text = MyReader.GetString("produsen_code")
                    TextBox2.Text = MyReader.GetString("produsen_name")
                    TextBox3.Text = MyReader.GetString("address")
                    teks = MyReader.GetString("city_code")
                    TextBox5.Text = MyReader.GetString("phone")
                    TextBox6.Text = MyReader.GetString("fax")
                    TextBox7.Text = MyReader.GetString("rendring_no")
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                TextBox1.Enabled = True
                btnDelete.Enabled = False
            Else
                baru = False
                edit = True
                TextBox1.Enabled = False
                btnDelete.Enabled = True
                TextBox4.Text = teks
            End If
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "select City_Code as CityCode,City_Name as CityName from tbm_city"
        PilihanDlg.SQLFilter = "select City_Code as CityCode,City_Name as CityName from tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' and city_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then TextBox4.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        cityname.Text = AmbilData("city_name", "tbm_city", "city_code='" & TextBox4.Text & "'")
    End Sub
End Class
