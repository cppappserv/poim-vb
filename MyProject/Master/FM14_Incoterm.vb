'Title        : Master Data Insurance
'Form         : FM14_Incoterm
'Created By   : YANTI
'Created Date : Oktober 2008
'Table Used   : tbm_insurance

Imports POIM.FM02_MaterialGroup
Public Class FM14_Incoterm
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select INSURANCE_CODE as IncotermCode, INSURANCE_DESCRIPTION as Description from tbm_insurance) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        TextBox1.Enabled = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
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

        SQLstr = "DELETE from tbm_insurance " & _
                 "where insurance_code='" & TextBox1.Text & "'"

        ErrMsg = "Failed when deleting User data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete User data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_insurance")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            SQLstr = "Select * from tbm_insurance where insurance_code='" & TextBox1.Text & "'"
            teks = "Save Data"
            ErrMsg = "Failed when saving user data"
            SQLstr = "INSERT INTO tbm_insurance (insurance_code,insurance_description) " & _
                     "VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating user data"
            SQLstr = "UPDATE tbm_insurance " & _
                     "SET insurance_description = '" & TextBox2.Text & "'" & _
                     "where insurance_code='" & TextBox1.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_insurance")
        End If
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        TextBox1.Text = DataGridView1.Item(0, brs).Value.ToString
        TextBox2.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox1.Enabled = False
        btnDelete.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        TextBox2.Focus()
    End Sub
    Private Sub f_getdata()
        Dim teks As String = ""

        SQLstr = "select * from tbm_insurance where insurance_code = '" & TextBox1.Text & "' "
        ErrMsg = "Failed when read USers Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    teks = MyReader.GetString("insurance_description")
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
                TextBox2.Text = teks
            End If
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(TextBox2.Text)) > 0)
        Call f_getdata()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(TextBox2.Text)) > 0)
    End Sub
End Class
