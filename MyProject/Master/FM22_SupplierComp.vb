'Title        : Master Data Supplier Company
'Form         : FM22_SupplierComp
'Created By   : Prie
'Created Date : 23 September 2008
'Table Used   : tbm_supplier_company
'Modified by  : Hanny - 5 nov 2008

Imports POIM.FM02_MaterialGroup

Public Class FM22_SupplierComp
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader    
    Dim PilihanDlg As New DlgPilihan
    Dim in_field As String = "tsc.supplier_code as 'SupplierCode', ts.supplier_name as 'SupplierName', tsc.company_code as 'CompanyCode', tc.company_name as 'CompanyName'"
    Dim in_tbl As String = "tbm_supplier_company as tsc inner join tbm_supplier as ts on tsc.supplier_code = ts.supplier_code inner join tbm_company as tc on tsc.company_code = tc.company_code"
    Dim v_oldcompany As String
    Dim v_idtable As String = "Supplier Company"
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
        'Dim brs As Integer

        'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_supplier_company")
        DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)
        'brs = DataGridView1.RowCount
        'brs = DataGridView1.CurrentCell.RowIndex

        DataGridView1.Columns(0).Width = 90
        DataGridView1.Columns(1).Width = 120
        DataGridView1.Columns(2).Width = 95
        DataGridView1.Columns(3).Width = 200



        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtSupplier_Code.Enabled = True
        txtCompany_Code.Enabled = True
        txtSupplier_Code.Clear()
        txtCompany_Code.Clear()
        txtSupplier_Code.Focus()
        lblSupplierName.Text = "Supplier Name"
        LblCompany.Text = "Company Name"
        baru = True
        edit = False
        v_oldcompany = ""
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Close()
        'Dispose()
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
        v_oldcompany = ""
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_supplier_company " & _
                 "where supplier_code ='" & txtSupplier_Code.Text & "' and company_code = '" & txtCompany_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_country")
            DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)

        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            SQLstr = "Select * from tbm_supplier_company where SUPPLIER_CODE='" & txtSupplier_Code.Text & "' and " & _
                     "company_code = '" & txtCompany_Code.Text & "'"
            If FM02_MaterialGroup.DataOK(SQLstr) = False Then
                MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
                txtSupplier_Code.Focus()
                Exit Sub
            End If
            teks = "Save Data"
            ErrMsg = "Failed when saving Supplier Company data"
            SQLstr = "INSERT INTO tbm_supplier_company (SUPPLIER_CODE,COMPANY_CODE) " & _
                     "VALUES ('" & txtSupplier_Code.Text & "', '" & txtCompany_Code.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_supplier_company " & _
                     "SET COMPANY_CODE = '" & txtCompany_Code.Text & "'" & _
                     "where SUPPLIER_CODE='" & txtSupplier_Code.Text & "' AND " & _
                     "COMPANY_CODE = '" & v_oldcompany & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & "  data")
            Exit Sub
        Else
            'RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_supplier_company")
            DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)
            v_oldcompany = txtCompany_Code.Text

        End If
    End Sub

    Private Sub txtSupplier_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplier_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtSupplier_Code.Text)) > 0) And (Len(Trim(txtCompany_Code.Text)) > 0)
        'Call f_getdata()
    End Sub

    Private Sub txtCompany_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompany_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtSupplier_Code.Text)) > 0) And (Len(Trim(txtCompany_Code.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtSupplier_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        lblSupplierName.Text = DataGridView1.Item(1, brs).Value.ToString
        txtCompany_Code.Text = DataGridView1.Item(2, brs).Value.ToString
        v_oldcompany = txtCompany_Code.Text
        LblCompany.Text = DataGridView1.Item(3, brs).Value.ToString
        txtSupplier_Code.Enabled = False
        'txtCompany_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtSupplier_Code.Text)) > 0) And (Len(Trim(txtCompany_Code.Text)) > 0)
    End Sub
    Private Sub f_getdata()
        'SQLstr = "select * from tbm_country where COUNTRY_CODE = '" & txtSupplier_Code.Text & "' "
        'ErrMsg = "Failed when read Material Group Data"
        'MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        'If Not MyReader Is Nothing Then
        '    txtCompany_Code.Text = ""

        '    While MyReader.Read
        '        Try
        '            txtCompany_Code.Text = MyReader.GetString("COUNTRY_NAME")
        '        Catch ex As Exception
        '        End Try

        '    End While
        '    If MyReader.HasRows = False Then
        '        'TextBox2.Text = ""
        '        baru = True
        '        edit = False
        '        txtSupplier_Code.Enabled = True
        '        btnDelete.Enabled = (Len(Trim(txtSupplier_Code.Text)) > 0)
        '    Else
        '        baru = False
        '        edit = True
        '        txtSupplier_Code.Enabled = False
        '        btnDelete.Enabled = (Len(Trim(txtSupplier_Code.Text)) > 0)
        '    End If
        '    CloseMyReader(MyReader, UserData)
        'End If
        ''Exit Function
    End Sub



    Private Sub BttnCompanyHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnCompanyHelp.Click

        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"
        PilihanDlg.SQLGrid = "SELECT company_code AS 'COMPANY CODE', company_name AS 'COMPANY NAME'" & _
                             " FROM tbm_company ORDER BY company_code ASC"

        PilihanDlg.SQLFilter = "SELECT company_code AS 'COMPANY CODE', company_name AS 'COMPANY NAME'" & _
                               " FROM tbm_company WHERE company_code LIKE 'FilterData1%' " & _
                               " and company_name LIKE 'FilterData2%' " & _
                               " ORDER BY company_code ASC"

        PilihanDlg.Tables = "tbm_company"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            LblCompany.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearchSupp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSupp.Click

        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"

        PilihanDlg.SQLGrid = "SELECT supplier_code AS SupplierCode, SUPPLIER_name AS SupplierName " & _
                             " FROM tbm_supplier ORDER BY supplier_code ASC"

        PilihanDlg.SQLFilter = "SELECT supplier_code AS SupplierCode, SUPPLIER_name AS SupplierName " & _
                               " FROM tbm_supplier WHERE supplier_code LIKE 'FilterData1%' " & _
                               " and supplier_name LIKE 'FilterData2%' " & _
                               " ORDER BY supplier_code ASC"

        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSupplier_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblSupplierName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class

