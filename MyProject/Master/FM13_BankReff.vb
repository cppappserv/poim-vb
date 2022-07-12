Public Class FM13_BankReff
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_idtable As String = "Bank Reference"
    Dim in_field As String = "tbr.ref_code as RefCode, tc.company_name as CompanyName, tbr.bank_code as BankCode, tb.bank_name as BankName"
    Dim in_tbl As String = "tbm_bank_reference as tbr inner join tbm_company as tc on tbr.ref_code = tc.company_code inner join tbm_bank as tb on tbr.bank_code = tb.bank_code"


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Close()
        'Dispose()
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub

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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tbr.ref_code as CompanyCode, tc.company_name as CompanyName, tbr.bank_code as BankCode, tb.bank_name as BankName,tb.account_no as AccountNo from tbm_bank_reference as tbr inner join tbm_company as tc on tbr.ref_code = tc.company_code inner join tbm_bank as tb on tbr.bank_code = tb.bank_code) as a")
        'DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)
        brs = DataGridView1.RowCount
        acc.Text = ""
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtRef_Code.Enabled = True
        txtRef_Code.Clear()
        txtBank_Code.Enabled = True
        txtBank_Code.Clear()
        lblCompName.Enabled = True
        lblCompName.Text = "Company Name"
        lblBankName.Enabled = True
        lblBankName.Text = "Bank Name"

        txtRef_Code.Focus()
        baru = True
        edit = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_bank_reference " & _
                 "where ref_code='" & txtRef_Code.Text & "' and " & _
                 "bank_code = '" & txtBank_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_bank_reference")
            'DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)
        End If
    End Sub

    Public Function DataOK(ByVal str As String) As Boolean
        MyReader = DBQueryMyReader(str, MyConn, "", UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return False
            End While
            CloseMyReader(MyReader, UserData)
        End If

        Return True
    End Function

    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_bank_reference where ref_code='" & txtRef_Code.Text & "' and " & _
                 "bank_code = '" & txtBank_Code.Text & "'"
        If DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtRef_Code.Focus()
            Exit Function
        End If

        'Foreign Key
        SQLstr = "Select * from tbm_company where company_code='" & txtRef_Code.Text & "'"
        If DataOK(SQLstr) = True Then
            MsgBox("Company code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtRef_Code.Focus()
            Exit Function
        End If

        SQLstr = "Select * from tbm_bank where bank_code='" & txtBank_Code.Text & "'"
        If DataOK(SQLstr) = True Then
            MsgBox("Bank code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtBank_Code.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            If CekData() = False Then Exit Sub
            teks = "Save Data"
            ErrMsg = "Failed when saving " & v_idtable & " data"
            SQLstr = "INSERT INTO tbm_bank_reference (REF_CODE,BANK_CODE) " & _
                     "VALUES ('" & txtRef_Code.Text & "', '" & txtBank_Code.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_bank_reference " & _
                     "SET BANK_CODE = '" & txtBank_Code.Text & "' " & _
                     "where REF_CODE='" & txtRef_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_bank_reference")
            'DataGridView1.DataSource = Show_Grid_JoinTable(DataGridView1, in_field, in_tbl)
        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(txtRef_Code.Text)) > 0) And _
                          (Len(Trim(txtBank_Code.Text)) > 0)
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
        txtRef_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        lblCompName.Text = DataGridView1.Item(1, brs).Value.ToString
        txtBank_Code.Text = DataGridView1.Item(2, brs).Value.ToString
        lblBankName.Text = DataGridView1.Item(3, brs).Value.ToString
        acc.Text = DataGridView1.Item(4, brs).Value.ToString

        txtRef_Code.Enabled = False
        txtBank_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtRef_Code.Text)) > 0) And (Len(Trim(txtBank_Code.Text)) > 0)
    End Sub

    Private Sub f_getdata()

        SQLstr = "select * from tbm_bank_reference where REF_CODE = '" & txtRef_Code.Text & "' " & _
                 "and BANK_CODE = '" & txtBank_Code.Text & "'"
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            'txtDoc_Code.Text = ""
            'txtDoc_No.Text = ""
            'While MyReader.Read

            ''Try
            ''    txtDoc_Code.Text = MyReader.GetString("CITY_CODE")
            ''Catch ex As Exception
            ''End Try
            'Try
            ''txtDoc_No.Text = MyReader.GetString("DOC_NO")
            'Catch ex As Exception
            'End Try


            'End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtRef_Code.Enabled = True
                txtBank_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtRef_Code.Text)) > 0) And (Len(Trim(txtBank_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtRef_Code.Enabled = False
                txtBank_Code.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtRef_Code.Text)) > 0) And (Len(Trim(txtBank_Code.Text)) > 0)
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

    Private Sub txtRef_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRef_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtRef_Code.Text)) > 0) And (Len(Trim(txtBank_Code.Text)) > 0)
        'f_getdata()
    End Sub

    Private Sub txtBank_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBank_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtRef_Code.Text)) > 0) And (Len(Trim(txtBank_Code.Text)) > 0)
    End Sub


    Private Sub btnSearchBank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchBank.Click
        PilihanDlg.Text = "Select Bank Code"
        PilihanDlg.LblKey1.Text = "Bank Code"
        PilihanDlg.LblKey2.Text = "Bank Name"
        PilihanDlg.SQLGrid = "select BANK_CODE as BankCode, BANK_NAME as BankName,account_no as AccountNo from tbm_bank"
        PilihanDlg.SQLFilter = "select BANK_CODE as BankCode, BANK_NAME as BankName from tbm_bank " & _
                               "WHERE bank_code LIKE 'FilterData1%' " & _
                               " and bank_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_bank"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBank_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblBankName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            acc.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            'txtDoc_No.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"
        PilihanDlg.SQLGrid = "select COMPANY_CODE as CompanyCode, COMPANY_NAME as CompanyName,COMPANY_SHORTNAME as CompShortName from tbm_company"
        PilihanDlg.SQLFilter = "select COMPANY_CODE as CompanyCode, COMPANY_NAME as CompanyName from tbm_company " & _
                               "WHERE company_code LIKE 'FilterData1%' " & _
                               " and company_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtRef_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCompName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

End Class