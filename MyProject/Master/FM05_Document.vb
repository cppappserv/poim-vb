'Title        : Master Data Document
'Form         : FM05_Document
'Created By   : Hanny
'Created Date : 23 September 2008
'Table Used   : tbm_document

Imports poim.FM02_MaterialGroup

Public Class FM05_Document
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim v_idtable As String = "Document"
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

        dgvDocument.DataSource = Show_Grid(dgvDocument, "(select DOC_CODE as DocCode, DOC_NAME as DocName from tbm_document) as a")
        dgvDocument.Columns(1).Width = 300

        brs = dgvDocument.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtDocCode.Enabled = True
        txtDocCode.Clear()
        txtDocName.Clear()
        txtDocCode.Focus()
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

        SQLstr = "DELETE from tbm_document " & _
                 "where DOC_CODE='" & txtDocCode.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'dgvDocument.DataSource = Show_Grid(dgvDocument, "tbm_document")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            SQLstr = "Select * from tbm_document where DOC_CODE='" & txtDocCode.Text & "'"
            If FM02_MaterialGroup.DataOK(SQLstr) = False Then
                MsgBox("Document code already created! ", MsgBoxStyle.Critical, "Warning")
                txtDocCode.Focus()
                Exit Sub
            End If
            teks = "Save Data"
            ErrMsg = "Failed when saving Document data"
            SQLstr = "INSERT INTO tbm_document (DOC_CODE,DOC_NAME) " & _
                     "VALUES ('" & txtDocCode.Text & "', '" & txtDocName.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_document " & _
                     "SET DOC_NAME = '" & txtDocName.Text & "'" & _
                     "where DOC_CODE='" & txtDocCode.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'dgvDocument.DataSource = Show_Grid(dgvDocument, "tbm_document")
        End If
    End Sub

    Private Sub txtDocCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocCode.TextChanged
        btnSave.Enabled = (Len(Trim(txtDocCode.Text)) > 0) And (Len(Trim(txtDocName.Text)) > 0)
        'Call f_getdata()
    End Sub

    Private Sub txtDocName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocName.TextChanged
        btnSave.Enabled = (Len(Trim(txtDocCode.Text)) > 0) And (Len(Trim(txtDocName.Text)) > 0)
    End Sub

    Private Sub dgvDocument_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDocument.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = dgvDocument.CurrentCell.RowIndex
        txtDocCode.Text = dgvDocument.Item(0, brs).Value.ToString
        txtDocName.Text = dgvDocument.Item(1, brs).Value.ToString
        txtDocCode.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtDocCode.Text)) > 0)
    End Sub
    Private Sub f_getdata()
        SQLstr = "select * from tbm_document where DOC_CODE = '" & txtDocCode.Text & "' "
        ErrMsg = "Failed when read Document Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtDocName.Text = ""

            While MyReader.Read
                Try
                    txtDocName.Text = MyReader.GetString("DOC_NAME")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtDocCode.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtDocCode.Text)) > 0)
            Else
                baru = False
                edit = True
                txtDocCode.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtDocCode.Text)) > 0)
            End If
            CloseMyReader(MyReader, UserData)
        End If
        'Exit Function
    End Sub


End Class

