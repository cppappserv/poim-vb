

'Title        : Master Data Attachment Doc
'Form         : FM06_AttachDoc
'Created By   : Hanny
'Created Date : 05 Oktober 2008
'Table Used   : Tbm_Attachment_Document

Imports poim.FM02_MaterialGroup
Public Class FM06_AttachDoc
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_idtable As String = "Attachment Document"
    Dim in_field As String = "ta.group_code as GroupCode, tm.group_name as GroupName, ta.doc_code as DocCode, td.doc_name as DocName, ta.doc_no as DocNo"
    Dim in_tbl As String = "tbm_attachment_doc as ta inner join tbm_document as td on ta.doc_code = td.doc_code inner join tbm_material_group as tm on ta.group_code = tm.group_code"


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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select ta.group_code as GroupCode, tm.group_name as GroupName, ta.doc_code as DocCode, td.doc_name as DocName, ta.doc_no as DocNo from tbm_attachment_doc as ta inner join tbm_document as td on ta.doc_code = td.doc_code inner join tbm_material_group as tm on ta.group_code = tm.group_code) as a")
        'DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)

        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtGroup_Code.Enabled = True
        txtGroup_Code.Clear()
        txtDoc_Code.Enabled = True
        txtDoc_Code.Clear()
        txtDoc_No.Clear()
        lblGroupName.Text = "Group Name"
        lblDocumentName.Text = "Document Name"

        txtGroup_Code.Focus()
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

        SQLstr = "DELETE from tbm_attachment_doc " & _
                 "where group_code='" & txtGroup_Code.Text & "' and " & _
                 "doc_code = '" & txtDoc_Code.Text & "'"

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
        SQLstr = "Select * from tbm_attachment_doc where group_code='" & txtGroup_Code.Text & "' and " & _
                 "doc_code = '" & txtDoc_Code.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtGroup_Code.Focus()
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
            SQLstr = "INSERT INTO tbm_attachment_doc (GROUP_CODE,DOC_CODE,DOC_NO) " & _
                     "VALUES ('" & txtGroup_Code.Text & "', '" & txtDoc_Code.Text & "', '" & txtDoc_No.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_attachment_doc " & _
                     "SET DOC_NO = '" & txtDoc_No.Text & "' " & _
                     "where GROUP_CODE='" & txtGroup_Code.Text & "' and " & _
                     "DOC_CODE ='" & txtDoc_Code.Text & "'"
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
        btnSave.Enabled = (Len(Trim(txtGroup_Code.Text)) > 0) And (Len(Trim(txtDoc_Code.Text)) > 0) _
                          And (Len(Trim(txtDoc_No.Text)) > 0)
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
        txtGroup_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        lblGroupName.Text = DataGridView1.Item(1, brs).Value.ToString
        txtDoc_Code.Text = DataGridView1.Item(2, brs).Value.ToString
        lblDocumentName.Text = DataGridView1.Item(3, brs).Value.ToString
        txtDoc_No.Text = DataGridView1.Item(4, brs).Value.ToString

        txtGroup_Code.Enabled = False
        txtDoc_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtGroup_Code.Text)) > 0) And (Len(Trim(txtDoc_Code.Text)) > 0)
    End Sub

    Private Sub f_getdata()

        SQLstr = "select * from tbm_attachment_doc where GROUP_CODE = '" & txtGroup_Code.Text & "' " & _
                 "and DOC_CODE = '" & txtDoc_Code.Text & "'"
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            'txtDoc_Code.Text = ""
            txtDoc_No.Text = ""
            While MyReader.Read

                'Try
                '    txtDoc_Code.Text = MyReader.GetString("CITY_CODE")
                'Catch ex As Exception
                'End Try
                Try
                    txtDoc_No.Text = MyReader.GetString("DOC_NO")
                Catch ex As Exception
                End Try


            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtGroup_Code.Enabled = True
                txtDoc_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtGroup_Code.Text)) > 0) And (Len(Trim(txtDoc_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtGroup_Code.Enabled = False
                txtDoc_Code.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtGroup_Code.Text)) > 0) And (Len(Trim(txtDoc_Code.Text)) > 0)
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







    Private Sub txtDoc_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDoc_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtGroup_Code.Text)) > 0) And (Len(Trim(txtDoc_Code.Text)) > 0)
        'f_getdata()
    End Sub

    Private Sub txtGroup_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGroup_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtGroup_Code.Text)) > 0) And (Len(Trim(txtDoc_Code.Text)) > 0)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select Group Code"
        PilihanDlg.LblKey1.Text = "Group Code"
        PilihanDlg.LblKey2.Text = "Group Name"
        PilihanDlg.SQLGrid = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group "
        PilihanDlg.SQLFilter = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group " & _
                               "WHERE group_code LIKE 'FilterData1%' " & _
                               " and group_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtGroup_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblGroupName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSearchDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDoc.Click
        PilihanDlg.Text = "Select Document Code"
        PilihanDlg.LblKey1.Text = "Document Code"
        PilihanDlg.LblKey2.Text = "Document Name"
        PilihanDlg.SQLGrid = "select DOC_CODE as DocCode, DOC_NAME as DocName from tbm_document"
        PilihanDlg.SQLFilter = "select DOC_CODE as DocCode, DOC_NAME as DocName from tbm_document " & _
                               "WHERE doc_code LIKE 'FilterData1%' " & _
                               " and doc_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_document"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtDoc_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblDocumentName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub


    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class