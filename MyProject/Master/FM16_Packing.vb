
'Title        : Master Data Packing
'Form         : FM16_Packing
'Created By   : Hanny
'Created Date : 05 Oktober 2008
'Table Used   : tbm_packing

Imports POIM.FM02_MaterialGroup

Public Class FM16_Packing
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim v_idtable As String = "Packing"
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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select PACK_CODE as PackCode, PACK_NAME as PackName from tbm_packing) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtPack_Code.Enabled = True
        txtPack_Code.Clear()
        txtPack_Name.Clear()
        txtPack_Code.Focus()
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

        SQLstr = "DELETE from tbm_packing " & _
                 "where pack_code='" & txtPack_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_packing")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            SQLstr = "Select * from tbm_packing where PACK_CODE='" & txtPack_Code.Text & "'"
            If FM02_MaterialGroup.DataOK(SQLstr) = False Then
                MsgBox("Pack code already created! ", MsgBoxStyle.Critical, "Warning")
                txtPack_Code.Focus()
                Exit Sub
            End If
            teks = "Save Data"
            ErrMsg = "Failed when saving " & v_idtable & " data"
            SQLstr = "INSERT INTO tbm_packing (PACK_CODE,PACK_NAME) " & _
                     "VALUES ('" & txtPack_Code.Text & "', '" & txtPack_Name.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_packing " & _
                     "SET PACK_NAME = '" & txtPack_Name.Text & "' " & _
                     "where PACK_CODE='" & txtPack_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_packing")
        End If
    End Sub

    Private Sub txtPack_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPack_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtPack_Code.Text)) > 0) And (Len(Trim(txtPack_Name.Text)) > 0)
        Call f_getdata()
    End Sub

    Private Sub txtPack_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPack_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtPack_Code.Text)) > 0) And (Len(Trim(txtPack_Name.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtPack_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtPack_Name.Text = DataGridView1.Item(1, brs).Value.ToString
        txtPack_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtPack_Code.Text)) > 0)
    End Sub
    Private Sub f_getdata()
        SQLstr = "select * from tbm_packing where PACK_CODE = '" & txtPack_Code.Text & "' "
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtPack_Name.Text = ""

            While MyReader.Read
                Try
                    txtPack_Name.Text = MyReader.GetString("PACK_NAME")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtPack_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtPack_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtPack_Code.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtPack_Code.Text)) > 0)
            End If
            CloseMyReader(MyReader, UserData)
        End If
        'Exit Function
    End Sub

End Class


