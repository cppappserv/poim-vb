
'Title        : Master Data Unit
'Form         : FM23_Unit
'Created By   : Prie
'Created Date : September 2008
'Table Used   : tbm_unit
'modifed by   : Hanny - 5 nov 2008

Imports POIM.FM02_MaterialGroup

Public Class FM23_Unit
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim v_idtable As String = "Unit"
    Dim PilihanDlg As New DlgPilihan
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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select UNIT_CODE as UnitCode, UNIT_NAME as UnitName, TYPE_CODE as TypeCode from tbm_unit) as x")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtUnit_Code.Enabled = True
        txtUnit_Code.Clear()
        txtUnit_Name.Clear()
        txtUnit_Code.Focus()
        typecode.Text = ""
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

        SQLstr = "DELETE from tbm_unit " & _
                 "where unit_code='" & txtUnit_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_unit")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            SQLstr = "Select * from tbm_unit where UNIT_CODE='" & txtUnit_Code.Text & "'"
            If FM02_MaterialGroup.DataOK(SQLstr) = False Then
                MsgBox("Unit code already created! ", MsgBoxStyle.Critical, "Warning")
                txtUnit_Code.Focus()
                Exit Sub
            End If
            teks = "Save Data"
            ErrMsg = "Failed when saving " & v_idtable & " data"
            SQLstr = "INSERT INTO tbm_unit (UNIT_CODE,UNIT_NAME,TYPE_CODE) " & _
                     "VALUES ('" & txtUnit_Code.Text & "', '" & txtUnit_Name.Text & "', '" & typecode.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_unit " & _
                     "SET UNIT_NAME = '" & txtUnit_Name.Text & "', " & _
                     "TYPE_CODE = '" & typecode.Text & "' " & _
                     "where UNIT_CODE='" & txtUnit_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_unit")
        End If
    End Sub

    Private Sub txtPack_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnit_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtUnit_Code.Text)) > 0) And (Len(Trim(txtUnit_Name.Text)) > 0)
        'Call f_getdata()
    End Sub

    Private Sub txtPack_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnit_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtUnit_Code.Text)) > 0) And (Len(Trim(txtUnit_Name.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtUnit_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtUnit_Name.Text = DataGridView1.Item(1, brs).Value.ToString
        typecode.Text = DataGridView1.Item(2, brs).Value.ToString
        txtUnit_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtUnit_Code.Text)) > 0)
    End Sub
    Private Sub f_getdata()
        SQLstr = "select * from tbm_unit where UNIT_CODE = '" & txtUnit_Code.Text & "' "
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtUnit_Name.Text = ""

            While MyReader.Read
                Try
                    txtUnit_Name.Text = MyReader.GetString("UNIT_NAME")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtUnit_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtUnit_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtUnit_Code.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtUnit_Code.Text)) > 0)
            End If
            CloseMyReader(MyReader, UserData)
        End If
        'Exit Function
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select Type Code"
        PilihanDlg.LblKey1.Text = "Type Code"
        PilihanDlg.LblKey2.Text = "Type Name"
        PilihanDlg.SQLGrid = "select TYPE_CODE as TypeCode, TYPE_NAME as TypeName from tbm_unit_type "
        PilihanDlg.SQLFilter = PilihanDlg.SQLGrid & "WHERE type_code LIKE 'FilterData1%' and type_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_unit_type"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then typecode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub typecode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles typecode.TextChanged
        typename.Text = AmbilData("TYPE_NAME", "tbm_unit_type", "TYPE_CODE='" & typecode.Text & "'")
    End Sub
End Class


