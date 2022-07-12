

'Title        : Master Data Payment Term
'Form         : FM18_PaymentTerm
'Created By   : Hanny
'Created Date : 23 September 2008
'Table Used   : tbm_payment_class, tbm_payment_term

Imports POIM.FM02_MaterialGroup
Public Class FM18_PaymentTerm
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_idtable As String = "Payment Term"

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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tt.PAYMENT_CODE as PaymentCode, tt.PAYMENT_NAME as PaymentName, tt.CLASS_CODE as ClassCode, tc.class_name as ClassName, tt.PAYMENT_DAYS as PaymentDays, tt.REMARK as Remark from tbm_payment_term as tt inner join tbm_payment_class as tc on tt.class_code = tc.class_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        txtPayment_Code.Enabled = True
        txtPayment_Code.Clear()
        txtPayment_Name.Clear()

        txtClass_Code.Clear()
        txtPayment_Days.Clear()
        txtRemark.Clear()

        txtPayment_Code.Focus()
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

        SQLstr = "DELETE from tbm_payment_term " & _
                 "where payment_code='" & txtPayment_Code.Text & "'"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_payment_term")
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_payment_term where payment_code='" & txtPayment_Code.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox(v_idtable & " already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtPayment_Code.Focus()
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
            SQLstr = "INSERT INTO tbm_payment_term (PAYMENT_CODE,PAYMENT_NAME,CLASS_CODE," & _
                     "PAYMENT_DAYS,REMARK) " & _
                     "VALUES ('" & txtPayment_Code.Text & "', '" & txtPayment_Name.Text & "', '" & txtClass_Code.Text & "', " & _
                              "'" & txtPayment_Days.Text & "', '" & txtRemark.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating " & v_idtable & " data"
            SQLstr = "UPDATE tbm_payment_term " & _
                     "SET PAYMENT_NAME = '" & txtPayment_Name.Text & "'," & _
                     "CLASS_CODE = '" & txtClass_Code.Text & "'," & _
                     "PAYMENT_DAYS = '" & txtPayment_Days.Text & "'," & _
                     "REMARK = '" & txtRemark.Text & "' " & _
                     "where PAYMENT_CODE='" & txtPayment_Code.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & " data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_payment_term")
        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(txtPayment_Code.Text)) > 0) And (Len(Trim(txtPayment_Name.Text)) > 0)
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
        txtPayment_Code.Text = DataGridView1.Item(0, brs).Value.ToString
        txtPayment_Name.Text = DataGridView1.Item(1, brs).Value.ToString
        txtClass_Code.Text = DataGridView1.Item(2, brs).Value.ToString
        txtPayment_Days.Text = DataGridView1.Item(4, brs).Value.ToString
        txtRemark.Text = DataGridView1.Item(5, brs).Value.ToString

        txtPayment_Code.Enabled = False
        btnDelete.Enabled = (Len(Trim(txtPayment_Code.Text)) > 0)
    End Sub

    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        PilihanDlg.Text = "Select Payment Class Code"
        PilihanDlg.LblKey1.Text = "Class Code"
        PilihanDlg.LblKey2.Text = "Class Name"

        PilihanDlg.SQLGrid = "select CLASS_CODE as ClassCode, CLASS_NAME as ClassName from tbm_payment_class"
        PilihanDlg.SQLFilter = "select CLASS_CODE as ClassCode, CLASS_NAME as ClassName from tbm_payment_class " & _
                               "WHERE class_code LIKE 'FilterData1%' " & _
                               " and class_name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_payment_class"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtClass_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub f_getdata()

        SQLstr = "select * from tbm_payment_term where payment_code = '" & txtPayment_Code.Text & "' "
        ErrMsg = "Failed when read " & v_idtable & " Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtPayment_Name.Text = ""
            txtClass_Code.Text = ""
            txtPayment_Days.Text = ""
            txtRemark.Text = ""
            While MyReader.Read
                Try
                    txtPayment_Name.Text = MyReader.GetString("PAYMENT_NAME")
                Catch ex As Exception
                End Try
                
                Try
                    txtClass_Code.Text = MyReader.GetString("CLASS_CODE")
                Catch ex As Exception
                End Try
                Try
                    txtPayment_Days.Text = MyReader.GetString("PAYMENT_DAYS")
                Catch ex As Exception
                End Try
                Try
                    txtRemark.Text = MyReader.GetString("REMARK")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                'TextBox2.Text = ""
                baru = True
                edit = False
                txtPayment_Code.Enabled = True
                btnDelete.Enabled = (Len(Trim(txtPayment_Code.Text)) > 0)
            Else
                baru = False
                edit = True
                txtPayment_Code.Enabled = False
                'txtPayment_Name.Enabled = False
                btnDelete.Enabled = (Len(Trim(txtPayment_Code.Text)) > 0)
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

    Private Sub txtPayment_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayment_Code.TextChanged
        btnSave.Enabled = (Len(Trim(txtPayment_Code.Text)) > 0) And (Len(Trim(txtPayment_Name.Text)) > 0)
        'f_getdata()

    End Sub

    Private Sub txtPayment_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayment_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtPayment_Code.Text)) > 0) And (Len(Trim(txtPayment_Name.Text)) > 0)
    End Sub

    Private Sub txtClass_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClass_Code.TextChanged
        Group_Name.Text = AmbilData("CLASS_NAME", "tbm_payment_class", "CLASS_CODE='" & txtClass_Code.Text & "'")
    End Sub
End Class