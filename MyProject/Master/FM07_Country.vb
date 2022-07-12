'Title        : Master Data Country
'Form         : FM07_Country
'Created By   : Hanny
'Created Date : 23 September 2008
'Table Used   : tbm_country

Imports POIM.FM02_MaterialGroup

Public Class FM07_Country
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim v_idtable As String = "Country"
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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select COUNTRY_CODE as CountryCode, COUNTRY_NAME as CountryName, LT as LeadTime from tbm_country) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtCountry_Code.Enabled = True
        txtCountry_Code.Clear()
        txtCountry_Name.Clear()
        txtCountry_Code.Focus()
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

        SQLstr = "DELETE from tbm_country " & _
                 "where country_code='" & txtCountry_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_country")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            SQLstr = "Select * from tbm_country where COUNTRY_CODE='" & txtCountry_Code.Text & "'"
            If FM02_MaterialGroup.DataOK(SQLstr) = False Then
                MsgBox("Country code already created! ", MsgBoxStyle.Critical, "Warning")
                txtCountry_Code.Focus()
                Exit Sub
            End If
            teks = "Save Data"
            ErrMsg = "Failed when saving Country data"
            SQLstr = "INSERT INTO tbm_country (COUNTRY_CODE,COUNTRY_NAME, LT) " & _
                     "VALUES ('" & txtCountry_Code.Text & "', '" & txtCountry_Name.Text & "', '" & txtLT.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_country " & _
                     "SET COUNTRY_NAME = '" & txtCountry_Name.Text & "', " & _
                     "    LT = '" & txtLT.Text & "' " & _
                     "where COUNTRY_CODE='" & txtCountry_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_country")
        End If
    End Sub

    Private Sub txtCountry_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountry_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtCountry_Code.Text)) > 0) And (Len(Trim(txtCountry_Name.Text)) > 0)
        'Call f_getdata()
    End Sub

    Private Sub txtCountry_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCountry_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtCountry_Code.Text)) > 0) And (Len(Trim(txtCountry_Name.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtCountry_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtCountry_Name.Text = DataGridView1.Item(1, brs).Value.ToString
        txtLT.Text = DataGridView1.Item(2, brs).Value.ToString
        txtCountry_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtCountry_Code.Text)) > 0)
    End Sub
    Private Sub f_getdata()
        SQLstr = "select * from tbm_country where COUNTRY_CODE = '" & txtCountry_Code.Text & "' "
        ErrMsg = "Failed when read Material Group Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtCountry_Name.Text = ""

            While MyReader.Read
                Try
                    txtCountry_Name.Text = MyReader.GetString("COUNTRY_NAME")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtCountry_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtCountry_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtCountry_Code.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtCountry_Code.Text)) > 0)
            End If
            CloseMyReader(MyReader, UserData)
        End If
        'Exit Function
    End Sub

 
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class

