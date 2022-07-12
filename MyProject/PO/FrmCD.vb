'Title        : Contract Document
'Form         : FrmCD
'Created By   : Hanny
'Created Date : 21 Feb 2009
'Table Used   : 

'Imports POIM.FrmPO

Public Class FrmCD
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim edit, DataError As Boolean
    Dim strSQL As String
    Dim v_matcode, v_matdesc As String
    Function GetServerDate() As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Call GetButtonAccess()
        edit = False
        'txtCDCT.Text = 1

        RefreshItem()
    End Sub
    Function GetDate3MonthAgo() As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "SELECT DATE_ADD(curdate(),INTERVAL +3 MONTH)"
        MyComm.CommandType = CommandType.Text
        GetDate3MonthAgo = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function
    Private Sub GetButtonAccess()
        Dim SQLStr, ModCode As String

        ModCode = "CD-C"
        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        btnNew.Enabled = (DataExist(SQLStr) = True)
        btnSave.Enabled = btnNew.Enabled
        btnReject.Enabled = btnNew.Enabled

    End Sub

    Private Sub GetItemAccess()
        dtCT.Enabled = btnSave.Enabled
        dtPer1.Enabled = btnSave.Enabled
        dtPer2.Enabled = btnSave.Enabled
        btnSearchCompany.Enabled = btnSave.Enabled
        btnSearchSupplier.Enabled = btnSave.Enabled
        remark.ReadOnly = Not (btnSave.Enabled)
        GroupBox2.Enabled = btnSave.Enabled
        btnSearchCurrency.Enabled = btnSave.Enabled
        grid1.ReadOnly = Not (btnSave.Enabled)
        grid2.ReadOnly = Not (btnSave.Enabled)

    End Sub

    Private Function DataExist(ByVal str As String) As Boolean
        MyReader = DBQueryMyReader(str, MyConn, "", UserData)

        If MyReader Is Nothing Then
            Return False
        Else
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return True
            End While
        End If
        CloseMyReader(MyReader, UserData)
    End Function

    Private Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"
        PilihanDlg.SQLGrid = "select tc.company_code as CompanyCode, tc.company_name as CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                             "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'"
        PilihanDlg.SQLFilter = "select tc.company_code as CompanyCode, tc.company_name as CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                               "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "' " & _
                               "and tc.company_code LIKE 'FilterData1%' AND " & _
                               "tc.company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company as tc inner join tbm_users_company as tuc on tc.COMPANY_CODE = tuc.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCompany_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearchSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSupplier.Click
        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier WHERE active='1' "
        PilihanDlg.SQLFilter = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier " & _
                               "WHERE active='1' AND supplier_code LIKE 'FilterData1%' AND " & _
                                    "supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSupplier_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblSupplierName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearchCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCurrency.Click
        PilihanDlg.Text = "Select Currency Code"
        PilihanDlg.LblKey1.Text = "Currency Code"
        PilihanDlg.LblKey2.Text = "Currency Name"
        PilihanDlg.SQLGrid = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency"
        PilihanDlg.SQLFilter = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency " & _
                               "WHERE currency_code LIKE 'FilterData1%' AND " & _
                                    "currency_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCurrency_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCurrency_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub FrmCD_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        GetGrid1Data()
        GetGrid2Data()
        If edit Then
            UpdateCombo1()
            UpdateCombo2()
        End If
    End Sub
    Private Sub UpdateCombo1()
        Dim brs, cnt As Integer
        Dim str As String

        brs = grid1.RowCount - 1
        For cnt = 0 To brs
            str = grid1.Item(1, cnt).Value
            grid1.Rows(cnt).Cells(2).Value = str
        Next
    End Sub
    Private Sub UpdateCombo2()
        Dim brs, cnt As Integer
        Dim str As String

        brs = grid2.RowCount - 1
        For cnt = 0 To brs
            str = grid2.Item(7, cnt).Value
            grid2.Rows(cnt).Cells(6).Value = str
        Next
    End Sub

    Private Sub GetGrid1Data()
        Dim dts, dts2 As DataTable
        Dim DT As New System.Data.DataTable
        Dim cbn As New DataGridViewComboBoxColumn

        grid1.DataSource = Nothing
        grid1.Columns.Clear()
        ErrMsg = "Contract data view failed"
        'txtCDCT.Text = 1
        strSQL = "select procentage,term_code,Note from tbl_contract_term where contract_no = '" & txtCDNo.Text & "' order by ord_no"
        dts = DBQueryDataTable(strSQL, MyConn, "", ErrMsg, UserData)
        grid1.DataSource = dts

        'Combo Box Document
        ErrMsg = "tbm_payment_term data view failed"
        strSQL = "select PAYMENT_CODE,PAYMENT_NAME from tbm_payment_term"
        dts2 = DBQueryDataTable(strSQL, MyConn, "", ErrMsg, UserData)
        With cbn
            .DataSource = dts2
            .DisplayMember = "PAYMENT_NAME"
            .ValueMember = "PAYMENT_CODE"
        End With
        grid1.Columns.Insert(2, cbn)
        grid1.Columns(1).Visible = False
        grid1.Columns(2).Width = 250
        grid1.Columns(2).HeaderText = "Term Payment"
        grid1.Columns(3).HeaderText = "Description"
        grid1.Columns(3).Width = 250
        grid1.Columns(0).HeaderText = "Percentage"
        grid1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid1.Columns(0).DefaultCellStyle.Format = "N2"
    End Sub
    Private Sub GetGrid2Data()
        Dim dts, dts2 As DataTable
        Dim DT As New System.Data.DataTable
        Dim cbn As New DataGridViewComboBoxColumn
        Dim cbt As New DataGridViewButtonColumn

        grid2.DataSource = Nothing
        grid2.Columns.Clear()

        ErrMsg = "Contract detail data view failed"
        strSQL = "select tc.material_code, tm.group_code as btnMat,tm.MATERIAL_NAME,tc.quantity,tc.weight,tc.unit_code,tc.price,tc.quantity*tc.price as totalamount,tc.note  " & _
                "from tbl_contract_detail as tc " & _
                "inner join tbm_material as tm on tc.material_code = tm.material_code " & _
                "where tc.contract_no = '" & txtCDNo.Text & "' order by ord_no "
        dts = DBQueryDataTable(strSQL, MyConn, "", ErrMsg, UserData)
        grid2.DataSource = dts

        'Combo Box Document
        ErrMsg = "tbm_unit data view failed"
        strSQL = "select UNIT_CODE from tbm_unit"
        dts2 = DBQueryDataTable(strSQL, MyConn, "", ErrMsg, UserData)
        With cbn
            .DataSource = dts2
            .DisplayMember = "UNIT_CODE"
            .ValueMember = "UNIT_CODE"
        End With
        With cbt
            .HeaderText = "SrchMat"
            .Width = 35
            .DefaultCellStyle.BackColor = Color.LightBlue
            .Text = "..."
        End With
        grid2.Columns.Insert(5, cbn)
        grid2.Columns.Insert(1, cbt)
        grid2.Columns(2).Visible = False

        grid2.Columns(0).HeaderText = "Material Code"
        grid2.Columns(3).HeaderText = "Material Name"
        grid2.Columns(3).Width = 150
        grid2.Columns(4).HeaderText = "Quantity"
        grid2.Columns(5).HeaderText = "Weight"
        grid2.Columns(6).HeaderText = "Unit Code"
        grid2.Columns(6).Width = 70
        grid2.Columns(7).Visible = False
        grid2.Columns(8).HeaderText = "Unit Price"
        grid2.Columns(9).HeaderText = "Total"
        grid2.Columns(10).HeaderText = "Remark"
        grid2.Columns(10).Width = 250

        grid2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid2.Columns(4).DefaultCellStyle.Format = "N2"
        grid2.Columns(4).Width = 60
        grid2.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid2.Columns(5).DefaultCellStyle.Format = "N2"
        grid2.Columns(5).Width = 60
        grid2.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid2.Columns(8).DefaultCellStyle.Format = "N2"
        grid2.Columns(8).Width = 100
        grid2.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid2.Columns(9).DefaultCellStyle.Format = "N2"
        grid2.Columns(9).Width = 100
    End Sub
    Private Sub txtCDNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCDNo.TextChanged
        Dim v_new As String
        btnSave.Enabled = (Len(Trim(txtCDNo.Text)) > 0)

        v_new = AmbilData("contract_no", "tbl_contract", "contract_no='" & txtCDNo.Text & "'")
        If txtCDNo.Text = "" Or v_new = "" Then
            edit = False
            Call FrmCD_Shown(sender, e)

            btnSave.Text = "Save"
            btnReject.Enabled = False
        ElseIf v_new <> "" Then
            edit = True
            Call FrmCD_Shown(sender, e)
            f_getCDheader(txtCDNo.Text, edit)

            txtCDNo.ReadOnly = True
            btnSave.Enabled = (btnSave.Enabled) And (CInt(crtcode.Text) = UserData.UserCT)
            btnReject.Enabled = btnSave.Enabled
            If CDbl(total.Text) = 0 Then
                btnSave.Enabled = True 'untuk mendukung entry data yg masih kosong (data contract pertama di create otomatis saat pembuatan PO jika Nomor Contract belum ada di tbl_contract)
                btnSave.Text = "Replace"
            Else
                btnSave.Text = "Update"
            End If

        End If
        GetItemAccess()
    End Sub

    Private Sub f_getCDheader(ByVal vpo_no As String, ByVal edit As Boolean)
        Dim MyReader As MySqlDataReader
        Dim ctcreated As String = ""
        ClearScreen(edit)

        SQLstr = "select * from tbl_contract " & _
                 "where contract_no = '" & vpo_no & "'"

        ErrMsg = "Failed when read Contract Document"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            If MyReader.HasRows = True Then
                While MyReader.Read

                    Try
                        dtCT.Value = MyReader.GetString("CONTRACT_DT")
                    Catch ex As Exception
                    End Try
                    dtPer1.Value = MyReader.GetString("CONTRACT_PERIOD_FR")
                    dtPer2.Value = MyReader.GetString("CONTRACT_PERIOD_TO")

                    txtCompany_Code.Text = MyReader.GetString("COMPANY_CODE")
                    txtSupplier_Code.Text = MyReader.GetString("SUPPLIER_CODE")
                    Try
                        dtDel.Value = MyReader.GetString("DELIVERY_BYDT")
                    Catch ex As Exception
                        rdDel.Checked = False
                        dtDel.Checked = False
                    End Try
                    Try
                        txtDD.Text = MyReader.GetString("DELIVERY_BYDD")
                    Catch ex As Exception
                        txtDD.Text = ""
                        rdDD.Checked = False
                    End Try
                    Try
                        txtMM.Text = MyReader.GetString("DELIVERY_BYDM")
                    Catch ex As Exception
                        txtMM.Text = ""
                        rdMM.Checked = False
                    End Try
                    If txtMM.Text <> "" Then
                        rdMM.Checked = True
                    ElseIf txtDD.Text <> "" Then
                        rdDD.Checked = True
                    Else
                        rdDel.Checked = True
                    End If
                    txtCurrency_Code.Text = MyReader.GetString("CURRENCY_CODE")
                    total.Text = FormatNumber(MyReader.GetString("AMOUNT"), 2)
                    Try
                        remark.Text = MyReader.GetString("REMARK")
                    Catch ex As Exception
                    End Try

                    ctcreated = MyReader.GetString("CREATEDBY")
                    crtdt.Text = MyReader.GetString("CREATEDDT")
                    Status.Text = MyReader.GetString("STATUS")

                End While
                CloseMyReader(MyReader, UserData)
                crtcode.Text = ctcreated

                lblCompany_Name.Text = AmbilData("company_name", "tbm_company", "company_code='" & txtCompany_Code.Text & "'")
                lblSupplierName.Text = AmbilData("supplier_name", "tbm_supplier", "supplier_code='" & txtSupplier_Code.Text & "'")
                lblCurrency_Name.Text = AmbilData("currency_name", "tbm_currency", "currency_code='" & txtCurrency_Code.Text & "'")
                crt.Text = AmbilData("name", "tbm_users", "user_ct='" & ctcreated & "'")
            Else
                CloseMyReader(MyReader, UserData)
            End If
        End If

    End Sub
    Private Sub ClearScreen(ByVal b_edit As Boolean)
        'edit = b_edit
        'txtCDNo.Text = ""
        dtCT.Value = Now()
        dtPer1.Value = Now()
        dtPer2.Value = DateAdd(DateInterval.Month, 1, Now)

        txtCompany_Code.Text = ""
        txtSupplier_Code.Text = ""
        lblCompany_Name.Text = ""
        lblSupplierName.Text = ""
        remark.Text = ""

        crtcode.Text = Str(UserData.UserCT)
        crt.Text = UserData.UserName
        crtdt.Value = GetServerDate()
        rdDel.Checked = True
        dtDel.Value = Now()
        txtDD.Text = ""
        txtMM.Text = ""
        txtCurrency_Code.Text = ""
        lblCurrency_Name.Text = ""
        total.Text = ""
        Status.Text = "Open"
    End Sub
    Private Sub btnListCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListCD.Click
        PilihanDlg.Text = "Select Contract No."
        PilihanDlg.LblKey1.Text = "Contract No."
        PilihanDlg.LblKey2.Text = "Company Code "
        PilihanDlg.SQLGrid = "select tc.Contract_no as ContractNo, tc.Contract_dt as ContractDate, tc.Company_code as CompanyCode, " & _
                            "tco.company_name as CompanyName, tc.Supplier_code as SupplierCode, ts.supplier_name as SupplierName, tu.name Createdby, " & _
                            "IF((SELECT MAX(ord_no) FROM tbl_contract_detail st1 WHERE st1.contract_no=tc.contract_no) IS NULL,'No Data','Yes') DataDetail " & _
                            "FROM tbl_contract AS tc, tbm_company AS tco, tbm_supplier AS ts, tbm_users tu " & _
                            "WHERE tc.company_code = tco.company_code AND tc.supplier_code = ts.supplier_code AND tc.createdby = tu.user_ct AND tc.Contract_no <> '' "
        PilihanDlg.SQLFilter = PilihanDlg.SQLGrid & _
                               "AND tc.Contract_no LIKE 'FilterData1%' AND " & _
                                    "tc.Company_code LIKE 'FilterData2%' "
        PilihanDlg.Tables = "FROM tbl_contract AS tc, tbm_company AS tco, tbm_supplier AS ts, tbm_users tu WHERE tc.company_code = tco.company_code AND tc.supplier_code = ts.supplier_code AND tc.createdby = tu.user_ct AND tc.Contract_no <> ''"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCDNo.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            edit = True
        End If
    End Sub

    Private Sub grid1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid1.CellEndEdit
        Dim brs As Integer
        Dim str As String
        Dim tot As Decimal
        Try
            brs = grid1.CurrentCell.RowIndex
            str = grid1.Item(0, brs).Value
        Catch ex As Exception
            grid1.Item(1, brs).Value = 0
        End Try

        Try
            brs = grid1.CurrentCell.RowIndex
            str = grid1.Item(3, brs).Value
        Catch ex As Exception
            grid1.Item(3, brs).Value = ""
        End Try
    End Sub
    Private Sub grid2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid2.CellEndEdit
        Dim brs As Integer
        Dim str1, str2 As String
        Dim tot, temp1, temp2 As Decimal
        Try
            brs = grid2.CurrentCell.RowIndex
            str1 = grid2.Item(4, brs).Value
        Catch ex As Exception
            grid2.Item(4, brs).Value = 0
        End Try

        Try
            brs = grid2.CurrentCell.RowIndex
            str2 = grid2.Item(8, brs).Value
        Catch ex As Exception
            grid2.Item(8, brs).Value = 0
        End Try
        temp1 = grid2.Item(4, brs).Value
        temp2 = grid2.Item(8, brs).Value
        tot = temp1 * temp2
        grid2.Item(9, brs).Value = tot

        tot = GetTotal()
        total.Text = FormatNumber(tot, 2)
    End Sub

    Private Function GetTotal() As Decimal
        Dim DT As System.Data.DataTable
        Dim brs, cnt As Integer
        Dim tot, temp As Decimal

        DT = grid2.DataSource
        brs = DT.Rows.Count
        tot = 0

        For cnt = 0 To brs
            temp = IIf(IsDBNull(grid2.Rows(cnt).Cells(9).Value), 0, grid2.Rows(cnt).Cells(9).Value)
            tot = tot + temp
        Next
        GetTotal = tot
    End Function

    Private Sub grid1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid1.CurrentCellDirtyStateChanged
        If DataError = True And grid1.IsCurrentCellDirty = False Then DataError = False

    End Sub

    Private Sub grid1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid1.DataError
        'MsgBox("Invalid amount, input numeric value")
        DataError = True
    End Sub

    Private Sub RefreshGrid()
        grid1.DataSource = Nothing
        grid1.Columns.Clear()
        grid1.Rows.Clear()

        grid2.DataSource = Nothing
        grid2.Columns.Clear()
        grid2.Rows.Clear()
    End Sub

    Private Sub RefreshItem()
        Dim dt As Date

        dt = GetServerDate()
        crt.Text = UserData.UserName
        crtdt.Value = dt
        dtCT.Value = dt
        dtPer1.Value = dt
        dtDel.Value = dt
        dtPer2.Value = GetDate3MonthAgo()
        total.Text = FormatNumber(0, 0, , , TriState.True)

        txtCompany_Code.Clear()
        lblCompany_Name.Text = ""
        txtSupplier_Code.Clear()
        lblSupplierName.Text = ""
        remark.Clear()
        rdDel.Checked = True
        txtDD.Clear()
        txtMM.Clear()
        txtCurrency_Code.Clear()
        lblCurrency_Name.Text = ""

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtCDNo.Text = ""
        txtCDNo.ReadOnly = False

        RefreshGrid()
        RefreshItem()

        btnSave.Text = "Save"
        edit = False
    End Sub

    Private Sub btnMat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMat.Click
        PilihanDlg.Text = "Select Material Code"
        PilihanDlg.Width = 600
        PilihanDlg.Height = 402
        PilihanDlg.DgvResult.Width = 570
        PilihanDlg.DgvResult.Height = 267
        PilihanDlg.LblKey1.Text = "Material Code"
        PilihanDlg.LblKey2.Text = "Material Group"
        PilihanDlg.SQLGrid = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                             "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                             "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code"
        PilihanDlg.SQLFilter = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                               "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                               "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code " & _
                               "WHERE tm.material_code LIKE 'FilterData1%' AND " & _
                               "tm.Group_code LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            v_matcode = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            v_matdesc = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub grid2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid2.CellClick
        If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            If e.ColumnIndex = 1 Then
                Call btnMat_Click("", e)
                grid2.Rows(e.RowIndex).Cells("Material_Code").Value = v_matcode
                grid2.Rows(e.RowIndex).Cells("Material_Name").Value = v_matdesc
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader
        Dim ds1, ds2 As System.Data.DataTable

        Dim lv_no_term, lv_no_det As String
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""
        Dim lv_procentage As String
        Dim lv_termcode, lv_desc As String
        Dim lv_matcode, lv_unit, lv_note As String
        Dim lv_qty, lv_weight, lv_price As String
        Dim dtct1, dtper1a, dtper2a, dtdel1 As String

        Dim insertStr1 As String = ""
        Dim insertStr2 As String = ""
        SQLstr = ""
        insertStr1 = ""
        insertStr2 = ""

        txtCDNo.Focus()
        ds1 = grid1.DataSource
        ds2 = grid2.DataSource
        If ds1.Rows.Count = 0 Or ds2.Rows.Count = 0 Then
            MsgBox("Please enter detail of contract")
            Exit Sub
        End If
        Try
            grid1.CommitEdit(DataGridViewDataErrorContexts.Commit)
            grid2.CommitEdit(DataGridViewDataErrorContexts.Commit)
            'olah data grid1
            lv_no_term = 0
            For i = 0 To grid1.RowCount - 1
                ErrMsg = "Failed to read Term data."
                If grid1.Rows(i).Cells(0).Value Is Nothing Then
                Else
                    lv_no_term = lv_no_term + 1
                    Try
                        lv_procentage = grid1.Rows(i).Cells("Procentage").Value.ToString
                    Catch ex As Exception
                        lv_procentage = 0
                    End Try
                    Try
                        lv_termcode = grid1.Rows(i).Cells(2).Value.ToString
                    Catch ex As Exception
                        lv_termcode = ""
                    End Try
                    Try
                        lv_desc = grid1.Rows(i).Cells(3).Value.ToString
                    Catch ex As Exception
                        lv_desc = ""
                    End Try
                    lv_no_term = Mid(lv_no_term & Space(5), 1, 5)
                    lv_procentage = Replace(lv_procentage, ",", ".")
                    lv_procentage = Mid(lv_procentage & "     ", 1, 5)
                    lv_termcode = Mid(lv_termcode & "     ", 1, 5)
                    lv_desc = Mid(lv_desc & Space(300), 1, 300)
                    insertStr1 &= lv_no_term & lv_procentage & lv_termcode & lv_desc & ";"
                End If
            Next

            'olah data grid2
            lv_no_det = 0
            For i = 0 To grid2.RowCount - 1
                ErrMsg = "Failed to read Detail data."
                If grid2.Rows(i).Cells(0).Value Is Nothing Then
                Else
                    lv_no_det = lv_no_det + 1
                    lv_matcode = grid2.Rows(i).Cells(0).Value.ToString
                    lv_qty = grid2.Rows(i).Cells(4).Value.ToString
                    Try
                        lv_weight = grid2.Rows(i).Cells(5).Value.ToString
                    Catch ex As Exception
                        lv_weight = ""
                    End Try
                    Try
                        lv_unit = grid2.Rows(i).Cells(6).Value.ToString
                    Catch ex As Exception
                        lv_unit = ""
                    End Try
                    Try
                        lv_price = grid2.Rows(i).Cells(8).Value.ToString
                    Catch ex As Exception
                        lv_price = 0
                    End Try
                    Try
                        lv_note = grid2.Rows(i).Cells(10).Value.ToString
                    Catch ex As Exception
                        lv_note = ""
                    End Try
                    lv_no_det = Mid(lv_no_det & "     ", 1, 5)
                    lv_matcode = Mid(lv_matcode & "          ", 1, 10)
                    lv_qty = Replace(lv_qty, ",", ".")
                    lv_qty = Mid(lv_qty & "                 ", 1, 17)
                    lv_weight = Replace(lv_weight, ",", ".")
                    lv_weight = Mid(lv_weight & "                 ", 1, 17)
                    lv_unit = Mid(lv_unit & "     ", 1, 5)
                    lv_price = Replace(lv_price, ",", ".")
                    lv_price = Mid(lv_price & "                 ", 1, 17)
                    lv_note = Mid(lv_note & Space(300), 1, 300)
                    insertStr2 &= lv_no_det & lv_matcode & lv_qty & lv_weight & lv_unit & lv_price & lv_note & ";"
                End If
            Next

            dtct1 = Format(dtCT.Value, "yyyy-MM-dd")
            dtper1a = Format(dtPer1.Value, "yyyy-MM-dd")
            dtper2a = Format(dtPer2.Value, "yyyy-MM-dd")
            dtdel1 = Format(dtDel.Value, "yyyy-MM-dd")

            If btnSave.Text = "Save" Then
                SQLstr = "Run Stored Procedure SaveCD (Save," & txtCDNo.Text & "," & dtct1 & "," & dtper1a & "," & dtper2a & "," & crt.Text & "," & crtdt.Text & "," & insertStr1 & ")"
                keyprocess = "Save"
            ElseIf (btnSave.Text = "Update" Or btnSave.Text = "Replace") Then
                SQLstr = "Run Stored Procedure SaveCD (Updt," & txtCDNo.Text & "," & dtct1 & "," & dtper1a & "," & dtper2a & "," & crt.Text & "," & crtdt.Text & "," & insertStr1 & ")"
                keyprocess = "Update"
            End If

            MyComm.CommandText = "SaveCD"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("CONT_NO", txtCDNo.Text)
            MyComm.Parameters.AddWithValue("CONT_DT", dtct1)
            MyComm.Parameters.AddWithValue("CONT_PERIOD_FR", dtper1a)
            MyComm.Parameters.AddWithValue("CONT_PERIOD_TO", dtper2a)
            MyComm.Parameters.AddWithValue("COMPANY", txtCompany_Code.Text)
            MyComm.Parameters.AddWithValue("SUPPLIER", txtSupplier_Code.Text)
            If rdDel.Checked = True Then
                MyComm.Parameters.AddWithValue("BYDT", dtdel1)
                MyComm.Parameters.AddWithValue("BYDD", DBNull.Value)
                MyComm.Parameters.AddWithValue("BYDM", DBNull.Value)
            ElseIf rdDD.Checked = True Then
                MyComm.Parameters.AddWithValue("BYDT", DBNull.Value)
                MyComm.Parameters.AddWithValue("BYDD", CInt(txtDD.Text))
                MyComm.Parameters.AddWithValue("BYDM", DBNull.Value)
            ElseIf rdMM.Checked = True Then
                MyComm.Parameters.AddWithValue("BYDT", DBNull.Value)
                MyComm.Parameters.AddWithValue("BYDD", DBNull.Value)
                MyComm.Parameters.AddWithValue("BYDM", CInt(txtMM.Text))
            End If
            MyComm.Parameters.AddWithValue("CURRENCY", txtCurrency_Code.Text)
            MyComm.Parameters.AddWithValue("AMT", CDbl(total.Text))
            MyComm.Parameters.AddWithValue("REM", remark.Text)
            MyComm.Parameters.AddWithValue("AuditStr", SQLstr)
            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            Else
                MyComm.Parameters.AddWithValue("UserCT", crtcode.Text)
            End If
            MyComm.Parameters.AddWithValue("InsertStr1", insertStr1)
            MyComm.Parameters.AddWithValue("InsertStr2", insertStr2)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " Contract Document")
            Else
                MsgBox(btnSave.Text & " Contract Document failed'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
        Dispose()
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader

        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        Dim insertStr1 As String = ""
        Dim insertStr2 As String = ""

        Dim Msg As String

        Msg = "Contract No. " & txtCDNo.Text & " will be deleted PERMANENTLY!!!" & Chr(13) & Chr(10) & "Are you sure to delete it?"
        If (MsgBox(msg, MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then
            Exit Sub
        Else
            Try
                SQLstr = "Run Stored Procedure SaveCD (Delete " & txtCDNo.Text & "," & UserData.UserCT & ")"

                MyComm.CommandText = "SaveCD"
                MyComm.CommandType = CommandType.StoredProcedure

                MyComm.Parameters.Clear()
                MyComm.Parameters.AddWithValue("keyprocess", "Delete")
                MyComm.Parameters.AddWithValue("CONT_NO", txtCDNo.Text)
                MyComm.Parameters.AddWithValue("CONT_DT", DBNull.Value)
                MyComm.Parameters.AddWithValue("CONT_PERIOD_FR", DBNull.Value)
                MyComm.Parameters.AddWithValue("CONT_PERIOD_TO", DBNull.Value)
                MyComm.Parameters.AddWithValue("COMPANY", DBNull.Value)
                MyComm.Parameters.AddWithValue("SUPPLIER", DBNull.Value)

                MyComm.Parameters.AddWithValue("BYDT", DBNull.Value)
                MyComm.Parameters.AddWithValue("BYDD", DBNull.Value)
                MyComm.Parameters.AddWithValue("BYDM", DBNull.Value)

                MyComm.Parameters.AddWithValue("CURRENCY", DBNull.Value)
                MyComm.Parameters.AddWithValue("AMT", DBNull.Value)
                MyComm.Parameters.AddWithValue("REM", DBNull.Value)
                MyComm.Parameters.AddWithValue("AuditStr", SQLstr)
                MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
                MyComm.Parameters.AddWithValue("InsertStr1", insertStr1)
                MyComm.Parameters.AddWithValue("InsertStr2", insertStr2)
                MyComm.Parameters.AddWithValue("Hasil", hasil)

                dr = MyComm.ExecuteReader()
                hasil = dr.FieldCount
                CloseMyReader(dr, UserData)

                If hasil = True Then
                    f_msgbox_successful("Delete Contract Document")
                Else
                    MsgBox("Delete Contract Document failed'")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
End Class