'Title        : Transaksi Purchase Order
'Form         : FrmPO
'Created By   : Hanny
'Created Date : 07 Oktober 2008
'Table Used   : 

'Imports POIM.FM02_MaterialGroup
Imports poim.FrmDOC_Import
Public Class FrmPO
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_matcode, v_matdesc, v_hscode, v_origin_code, v_origin_name As String
    'Dim counter As Integer = 0
    Dim x As Integer
    Dim v_idtable As String = "tbl_po"
    Dim actv As Boolean = True
    Dim PilihPO As New FrmListPO


    'Sub New()
    '    InitializeComponent()
    '    lblCompany_Name.Text = ""
    '    lblPlant_Name.Text = ""
    '    lblPort_Name.Text = ""
    '    lblPayment_Name.Text = ""
    '    lblInsurance_Desc.Text = ""
    '    lblSupplierName.Text = ""
    '    prodname.Text = ""
    'End Sub

    Private Sub DGVDetail_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDetail.CellClick
        If txtPO_NO.Text <> "" Then
            If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
                'If DGVDetail.Columns(e.ColumnIndex).Name = "btnMat" Then
                'If e.ColumnIndex = 2 And DGVDetail.Columns(e.ColumnIndex).Name = "" Then
                If DGVDetail.Columns(e.ColumnIndex).HeaderText = "SrchMat" Then
                    'MessageBox.Show("You picked " & _
                    '  DGVDetail.Rows(e.RowIndex). _
                    '  Cells("No").Value)
                    Call btnSearchMat_Click("", e)
                    DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value = v_matcode
                    DGVDetail.Rows(e.RowIndex).Cells("Material_Name").Value = v_matdesc
                    DGVDetail.Rows(e.RowIndex).Cells("po_item").Value = e.RowIndex + 1
                    DGVDetail.Rows(e.RowIndex).Cells("HS_CODE").Value = v_hscode
                    DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value = ""
                    DGVDetail.Rows(e.RowIndex).Cells("Country_Name").Value = ""
                    'DGVDetail.Columns.Remove(DGVDetail.Columns(6))
                    'Dim cbn As New DataGridViewComboBoxColumn
                    'With cbn
                    '    .DataSource = Show_Grid(DGVDetail, "tbm_ORIGIN")
                    '    .DisplayMember = "ORI_NAME"
                    '    .ValueMember = "ORI_CODE"
                    'End With
                    'DGVDetail.Columns.Insert(5, cbn)

                End If
                'If DGVDetail.Columns(e.ColumnIndex).Name = "btnOrigin" Then
                'If e.ColumnIndex = 6 And DGVDetail.Columns(e.ColumnIndex).Name = "" Then
                If DGVDetail.Columns(e.ColumnIndex).HeaderText = "SrchOrigin" Then
                    'cek initial
                    If DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value.ToString <> "" Then
                        v_origin_code = DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value.ToString
                        v_origin_name = DGVDetail.Rows(e.RowIndex).Cells("Country_Name").Value.ToString
                    End If
                    If DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value.ToString <> "" Then
                        Call btnSearchOri_Click(DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value, e)
                        DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value = v_origin_code
                        DGVDetail.Rows(e.RowIndex).Cells("Country_Name").Value = v_origin_name
                    Else
                        MsgBox("Please input Material Code", MsgBoxStyle.Information, "Error")
                    End If
                End If
            End If
        Else
            MsgBox("Please fill in PO number", MsgBoxStyle.Information, "Error")
        End If
    End Sub
    'Private Sub DGVDetail_KeyPress(ByVal sender As Object, _
    '                               ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    '                               Handles DGVDetail.KeyPress
    '    If Me.DGVDetail.CurrentCell.ColumnIndex > 0 Then
    '        If DGVDetail.Columns(Me.DGVDetail.CurrentCell.ColumnIndex).Name = "Material_Code" Then
    '            'MessageBox.Show("You picked " & _
    '            '  DGVDetail.Rows(e.RowIndex). _
    '            '  Cells("No").Value)
    '            Call btnSearchMat_Click("", e)
    '            DGVDetail.Rows(Me.DGVDetail.CurrentCell.RowIndex).Cells("Material_Code").Value = v_matcode
    '            DGVDetail.Rows(Me.DGVDetail.CurrentCell.RowIndex).Cells("Material_Name").Value = v_matdesc
    '            DGVDetail.Rows(Me.DGVDetail.CurrentCell.RowIndex).Cells("No").Value = Me.DGVDetail.CurrentCell.RowIndex + 1

    '            'btnMat.ValuesAreIcons = True
    '            'btnMat.Image = POIM.My.Resources.search
    '        End If
    '    End If
    'End Sub

    Private Sub FrmPO_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        f_caller = ""
    End Sub

    Private Sub FrmPO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''TODO: This line of code loads data into the 'ImprDataSetpack.tbm_packing' table. You can move, or remove it, as needed.
        'Me.Tbm_packingTableAdapter.Fill(Me.ImprDataSetpack.tbm_packing)
        ''TODO: This line of code loads data into the 'ImprDataSetUnit.tbm_unit' table. You can move, or remove it, as needed.
        'Me.Tbm_unitTableAdapter.Fill(Me.ImprDataSetUnit.tbm_unit)

        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        'RefreshScreen()
        'ClearScreen()
        'Create_RSTemp()
        'refreshGrid("1")
        'counter = 1

        'Dim btnS1 As New Windows.Forms.DataGridViewButtonColumn
        'DataGridView1.Rows(0).Cells("btnS1")
        'btnS1.FlatStyle = FlatStyle.Standard
        'btnS1.Name = "MatCode"
        'btnS1.Text = "Search"
        'btnS1.UseColumnTextForButtonValue = True

        crtcode.Text = UserData.UserCT
        txtCREATEDBY.Text = UserData.UserName
        txtPO_NO.Focus()
        'If DGVDetail.Columns(e.ColumnIndex).Name = "btnMat" Then
        '    'MessageBox.Show("You picked " & _
        '    '  DGVDetail.Rows(e.RowIndex). _
        '    '  Cells("No").Value)
        '    Call btnSearchMat_Click("", e)
        '    DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value = v_matcode
        '    DGVDetail.Rows(e.RowIndex).Cells("Material_Name").Value = v_matdesc
        'End If

        Dim obj As Object
        Dim ee As System.EventArgs
        btnNew_Click(obj, ee)

    End Sub
    Private Sub ClearScreen()
        actv = True
        txtCompany_Code.Text = ""
        lblCompany_Name.Text = ""
        txtPlant_Code.Text = ""
        produsen.Text = ""
        lblPlant_Name.Text = ""
        txtPort_Code.Text = ""
        lblPort_Name.Text = ""
        txtLoadPort_Code.Text = ""
        lblLoadPort_Name.Text = ""
        DTPeriodeFR.Text = ""
        DTPeriodeTO.Text = ""
        txtPayment_Code.Text = ""
        lblPayment_Name.Text = ""
        TxtInsurance.Text = ""
        lblInsurance_Desc.Text = ""
        txtIPA_No.Text = ""
        txtPR_No.Text = ""
        txtSupplier_Code.Text = ""
        lblSupplierName.Text = ""
        txtContract_No.Text = ""
        txtTolerable_Del.Text = FormatNumber(0, 2, , , TriState.True)
        txtTotal.Text = FormatNumber(0, 2, , , TriState.True)

        txtCurrency_Code.Text = ""
        'lblKurs.Text = "0"
        lblKurs.Text = FormatNumber(0, 2, , , TriState.True)
        DTCreated.Text = GetServerDate()
        crtcode.Text = UserData.UserCT
        txtCREATEDBY.Text = UserData.UserName
        TxtStatus.Text = "Open"
        txtPURCHASEDBY.Text = ""
        DtPurchased.Text = ""
        txtAPPROVEDBY.Text = ""
        DtApproved.Text = ""
        txtFUNDAPPBY.Text = ""
        DtFundApp.Text = ""
        cbShipTerm.Text = "W - Whole Shipment"
    End Sub

    Private Sub f_getpoheader(ByVal vpo_no As String)
        Dim MyReader As MySqlDataReader
        Dim v_shipterm, temp As String
        Dim ctcreated As String = ""
        ClearScreen()

        SQLstr = "select a.*,b.company_name,c.plant_name,d.port_name,i.port_name loadport_name,e.payment_name,f.insurance_Description,g.Supplier_name,h.Currency_Name " & _
                 " from tbl_po as a " & _
                 " inner join tbm_company as b on a.company_code=b.company_code " & _
                 " inner join tbm_plant as c on a.plant_code=c.plant_code " & _
                 " inner join tbm_port as d on a.port_code=d.port_code " & _
                 " inner join tbm_payment_term as e on a.payment_code=e.payment_code" & _
                 " left join tbm_insurance as f on a.insurance_code=f.insurance_code" & _
                 " inner join tbm_supplier as g on a.supplier_code=g.supplier_code" & _
                 " inner join tbm_currency as h on a.currency_code=h.currency_code" & _
                 " left join tbm_port AS i ON a.loadport_code=i.port_code " & _
                 " where po_no = '" & vpo_no & "'"

        ErrMsg = "Failed when read PO"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        txtTolerable_Del.Text = "10,00"
        If Not MyReader Is Nothing Then
            If MyReader.HasRows = True Then
                While MyReader.Read
                    Try
                        txtCompany_Code.Text = MyReader.GetString("company_code")
                        lblCompany_Name.Text = MyReader.GetString("company_name")
                        txtPlant_Code.Text = MyReader.GetString("plant_code")
                        lblPlant_Name.Text = MyReader.GetString("plant_name")
                        txtPort_Code.Text = MyReader.GetString("port_code")
                        lblPort_Name.Text = MyReader.GetString("port_name")
                        Try
                            txtLoadPort_Code.Text = MyReader.GetString("loadport_code")
                            lblLoadPort_Name.Text = MyReader.GetString("loadport_name")
                        Catch ex As Exception
                            txtLoadPort_Code.Text = ""
                            lblLoadPort_Name.Text = ""
                        End Try

                        DTPeriodeFR.Text = MyReader.GetString("shipment_period_fr")
                        DTPeriodeTO.Text = MyReader.GetString("shipment_period_to")
                        v_shipterm = MyReader.GetString("shipment_term_code")
                        If v_shipterm = "W" Then
                            cbShipTerm.SelectedIndex = 0
                        Else
                            cbShipTerm.SelectedIndex = 1
                        End If
                        txtPayment_Code.Text = MyReader.GetString("payment_code")
                        Try
                            lblPayment_Name.Text = MyReader.GetString("payment_name")
                        Catch ex As Exception
                            lblPayment_Name.Text = ""
                        End Try
                        TxtInsurance.Text = MyReader.GetString("insurance_code")
                        Try
                            lblInsurance_Desc.Text = MyReader.GetString("insurance_description")
                        Catch ex As Exception
                            lblInsurance_Desc.Text = ""
                        End Try
                        txtIPA_No.Text = MyReader.GetString("ipa_no")
                        txtPR_No.Text = MyReader.GetString("pr_no")
                        txtSupplier_Code.Text = MyReader.GetString("supplier_code")
                        Try
                            lblSupplierName.Text = MyReader.GetString("supplier_name")
                        Catch ex As Exception
                            lblSupplierName.Text = ""
                        End Try
                        temp = MyReader.GetString("produsen_code")
                        txtContract_No.Text = MyReader.GetString("contract_no")
                        txtTolerable_Del.Text = FormatNumber(MyReader.GetString("tolerable_delivery"), 2)
                        'Tol_Delivery.Mask = ""
                        'Tol_Delivery.Text = MyReader.GetString("tolerable_delivery")
                        'Tol_Delivery.Mask = FM11_Kurs.GetMask(Tol_Delivery.Text)
                        actv = False
                        txtCurrency_Code.Text = MyReader.GetString("currency_code")
                        'lbCurrency_Name.Text = MyReader.GetString("currency_name")
                        Try
                            lblKurs.Text = FormatNumber(MyReader.GetString("kurs"), 2)
                        Catch ex As Exception
                            lblKurs.Text = 0
                        End Try
                        'txtrate.Mask = ""
                        'txtrate.Text = MyReader.GetString("kurs")
                        'txtrate.Mask = FM11_Kurs.GetMask(txtrate.Text)
                        DTCreated.Text = MyReader.GetString("createddt")
                        ctcreated = MyReader.GetString("createdby")

                        TxtStatus.Text = MyReader.GetString("status")
                        Try
                            CTpur.Text = MyReader.GetString("purchasedby")
                            If CTpur.Text <> "" Then
                                DtPurchased.Text = MyReader.GetString("purchaseddt")
                                DtPurchased.Checked = True
                            End If
                        Catch ex As Exception
                            CTpur.Text = ""
                            DtPurchased.Checked = False
                        End Try
                        Try
                            CTApp.Text = MyReader.GetString("approvedby")
                            If CTApp.Text <> "" Then
                                DtApproved.Text = MyReader.GetString("approveddt")
                                DtApproved.Checked = True
                            End If
                        Catch ex As Exception
                            CTApp.Text = ""
                            DtApproved.Checked = False
                        End Try
                        Try
                            CTFun.Text = MyReader.GetString("fundappby")
                            If CTFun.Text <> "" Then
                                DtFundApp.Text = MyReader.GetString("fundappdt")
                                DtFundApp.Checked = True
                            End If
                        Catch ex As Exception
                            CTFun.Text = ""
                            DtFundApp.Checked = False
                        End Try
                    Catch ex As Exception
                    End Try
                End While
                'If MyReader.HasRows = True Then
                CloseMyReader(MyReader, UserData)
                crtcode.Text = ctcreated
                txtCREATEDBY.Text = AmbilData("name", "tbm_users", "user_ct='" & ctcreated & "'")
                txtPURCHASEDBY.Text = AmbilData("name", "tbm_users", "user_ct='" & CTpur.Text & "'")
                txtAPPROVEDBY.Text = AmbilData("name", "tbm_users", "user_ct='" & CTApp.Text & "'")
                txtFUNDAPPBY.Text = AmbilData("name", "tbm_users", "user_ct='" & CTFun.Text & "'")
                produsen.Text = temp
                txtTotal.Text = FormatNumber(FrmDOC_Import.GetAmount(txtPO_NO.Text), 2)
                'Total.Mask = ""
                'Total.Text = GetAmount(txtPO_NO.Text)
                'Total.Mask = FormatNumber((Total.Text), 0)

                btnSchedulePO.Enabled = True
                btnDelete.Enabled = (UserData.UserCT = crtcode.Text) And (TxtStatus.Text <> "Closed")
                btnClosing.Enabled = btnDelete.Enabled
                btnClosing_text()
            Else
                CloseMyReader(MyReader, UserData)
            End If
        End If
        'txtPO_NO.Focus()
    End Sub

    Private Sub refreshGrid(ByVal vpo_no As String)
        Dim in_field As String
        Dim in_tbl As String = ""
        Dim dts As DataTable
        Dim strSQLu, strSQLp As String
        Dim cbu As New DataGridViewComboBoxColumn
        Dim cbp As New DataGridViewComboBoxColumn
        Dim cbtm, cbto As New DataGridViewButtonColumn
        Dim dtsu, dtsp As DataTable

        DGVDetail.DataSource = Nothing
        DGVDetail.Columns.Clear()
        DGVDetail.Rows.Clear()
        'Dim DGVDetail As New DataGridView
        'in_field = "tpo.po_no , dpo.po_item,dpo.material_code,tmat.MATERIAL_name, dpo.country_code,tcou.COUNTRY_NAME, " & _
        '           "dpo.hs_code,dpo.SPECIFICATION,dpo.quantity,dpo.weight,dpo.unit_code,dpo.package_code,tpac.pack_name, " & _
        '           "dpo.price, dpo.note "
        in_field = "dpo.po_item,dpo.material_code,dpo.material_code as btnMat, tmat.MATERIAL_name, dpo.country_code,dpo.country_code as btnOrigin,tcou.COUNTRY_NAME, " & _
                   "dpo.hs_code,dpo.SPECIFICATION,dpo.quantity,dpo.unit_code,dpo.package_code, " & _
                   "dpo.price, dpo.quantity * dpo.price as Total, dpo.note "
        ' dpo.shipment_no
        in_tbl = "tbl_po as tpo inner join tbl_po_detail as dpo on " & _
                 "tpo.po_no = dpo.po_no inner join tbm_material as tmat on dpo.material_code = tmat.material_code " & _
                 "inner join tbm_country as tcou on dpo.country_code = tcou.country_code " & _
                 "left join tbm_packing as tpac on dpo.package_code = tpac.PACK_CODE"
        SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tpo.po_no = '" & vpo_no & "'"
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        'If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then
        DGVDetail.DataSource = dts
        'Combo Box Document
        ErrMsg = "tbm_unit data view failed"
        strSQLu = "select UNIT_CODE from tbm_unit"
        dtsu = DBQueryDataTable(strSQLu, MyConn, "", ErrMsg, UserData)
        With cbu
            .DataSource = dtsu
            .HeaderText = "Unit Code"
            .DataPropertyName = "unit_code"
            .DisplayMember = "UNIT_CODE"
            .ValueMember = "UNIT_CODE"
            .Width = 70
        End With
        'Combo Box Document
        ErrMsg = "tbm_packing data view failed"
        strSQLp = "select PACK_CODE from tbm_packing"
        dtsp = DBQueryDataTable(strSQLp, MyConn, "", ErrMsg, UserData)
        With cbp
            .HeaderText = "Pack Code"
            .DataSource = dtsp
            .DataPropertyName = "package_code"
            .DisplayMember = "PACK_CODE"
            .ValueMember = "PACK_CODE"
            .Width = 70
        End With
        With cbtm
            .DataPropertyName = "btnMatz"
            .HeaderText = "SrchMat"
            .Width = 15
            .DefaultCellStyle.BackColor = Color.LightGray
            .Text = "..."
        End With
        With cbto
            .DataPropertyName = "btnOriginz"
            .HeaderText = "SrchOrigin"
            .Width = 15
            .DefaultCellStyle.BackColor = Color.LightGray
            .Text = "..."
        End With

        'If dts. > 0 Then
        'Show_Grid_JoinTable(DGVDetail, in_field, in_tbl)
        'If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then
        DGVDetail.Columns.Insert(2, cbtm)
        DGVDetail.Columns.Insert(6, cbto)
        DGVDetail.Columns.Insert(12, cbu)
        DGVDetail.Columns.Insert(13, cbp)
        DGVDetail.Columns(3).Visible = False
        DGVDetail.Columns(7).Visible = False
        DGVDetail.Columns(14).Visible = False
        DGVDetail.Columns(15).Visible = False

        'DGVDetail.Rows(0).Cells("po_item").Value = 1
        DGVDetail.Columns(0).HeaderText = "Item"
        DGVDetail.Columns(0).Width = 30
        DGVDetail.Columns(1).HeaderText = "Material Code"
        DGVDetail.Columns(1).Width = 100
        DGVDetail.Columns(4).HeaderText = "Material Name"
        DGVDetail.Columns(4).Width = 150
        DGVDetail.Columns(4).ReadOnly = True
        DGVDetail.Columns(5).HeaderText = "Country Code"
        DGVDetail.Columns(5).Width = 100
        DGVDetail.Columns(8).HeaderText = "Country Name"
        DGVDetail.Columns(8).ReadOnly = True
        DGVDetail.Columns(9).HeaderText = "HS Code"
        DGVDetail.Columns(9).Width = 90
        DGVDetail.Columns(10).HeaderText = "Specification"
        DGVDetail.Columns(11).HeaderText = "Quantity"
        DGVDetail.Columns(16).HeaderText = "Unit Price"
        DGVDetail.Columns(17).HeaderText = "Amount"
        DGVDetail.Columns(18).HeaderText = "Remark"
        DGVDetail.Columns(18).Width = 150
        DGVDetail.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(11).DefaultCellStyle.Format = "N5"
        DGVDetail.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(16).DefaultCellStyle.Format = "N2"
        DGVDetail.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(17).DefaultCellStyle.Format = "N2"
        'DGVDetail.Columns("po_no").Visible = True
        'DGVDetail.Columns("shipment_no").Visible = True
        'End If
    End Sub
    Private Sub RefreshScreen()
        'Dim brs As Integer

        '        DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_attachment_doc")
        'brs = DGVDetail.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnClosing.Enabled = False
        txtPO_NO.Enabled = True
        txtPO_NO.Clear()

        btnSearchDestin.Enabled = False
        btnSearchPort.Enabled = False
        btnSchedulePO.Enabled = False
        baru = True
        edit = False
        txtPO_NO.Focus()
    End Sub

    Private Function f_GetTotAmount() As Double
        Dim v_totAmount As Double

        DGVDetail.CommitEdit(DataGridViewDataErrorContexts.Commit)
        For i = 0 To DGVDetail.RowCount - 1
            ErrMsg = "Failed to update PO detail data."
            If DGVDetail.Rows(i).Cells(4).Value Is Nothing Then
            Else
                v_totAmount = v_totAmount + CDbl(DGVDetail.Rows(i).Cells(17).Value.ToString)

            End If
        Next
        Return v_totAmount
    End Function

    'Private Sub DGVDetail_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGVDetail.EditingControlShowing
    '    ---restrict inputs on the second field---
    '    If Me.DGVDetail.CurrentCell.ColumnIndex = 1 And _
    '        Not e.Control Is Nothing Then
    '        Dim tb As TextBox = CType(e.Control, TextBox)

    '        ---add an event handler to the TextBox control---
    '        AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
    '    End If

    'End Sub

    'Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox.KeyPress
    '    ---if textbox is empty and user pressed a decimal char---
    '    If CType(sender, TextBox).Text = String.Empty And _
    '       e.KeyChar = Chr(46) Then
    '        e.Handled = True
    '        Return
    '    End If
    '    '---if textbox already has a decimal point---
    '    If CType(sender, TextBox).Text.Contains(Chr(46)) And _
    '       e.KeyChar = Chr(46) Then
    '        e.Handled = True
    '        Return
    '    End If
    '    '---if the key pressed is not a valid decimal number---
    '    If (Not (Char.IsDigit(e.KeyChar) Or _
    '       Char.IsControl(e.KeyChar) Or _
    '       (e.KeyChar = Chr(46)))) Then
    '        e.Handled = True
    '    End If

    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Dim brs As Integer
        'Dim test As String
        Dim lv_poitem, lv_matcode, lv_origin, lv_hsno, lv_specs, lv_unitcode, lv_packcode, lv_note, tempPO As String
        Dim lv_qty, lv_weight, lv_price As Decimal
        Dim ExistPO As Boolean
        Dim lv_shipmentno, i As Integer
        Dim insertStr As String = ""
        Dim v_tolerable As Decimal

        SQLstr = ""
        insertStr = ""
        tempPO = AmbilData("po_no", "tbl_po", "po_no='" & txtPO_NO.Text & "'")
        If tempPO <> "" And baru Then
            MsgBox("PO " & txtPO_NO.Text & " already created")
            Exit Sub
        End If
        DGVDetail.CommitEdit(DataGridViewDataErrorContexts.Commit)
        If Not CekInput(txtPO_NO, "PO No") Then Exit Sub
        If Not CekInput(txtCompany_Code, "Company Code") Then Exit Sub
        If Not CekInput(txtPlant_Code, "Destination") Then Exit Sub
        If Not CekInput(txtPort_Code, "Port") Then Exit Sub
        If Not CekInput(txtPort_Code, "Shipment Period") Then Exit Sub
        If Not CekInput(cbShipTerm, "Shipment Term") Then Exit Sub
        If Not CekInput(txtPayment_Code, "Payment Term") Then Exit Sub
        If Not CekInput(TxtInsurance, "Incoterm") Then Exit Sub
        If Not CekInput(txtSupplier_Code, "Supplier Code") Then Exit Sub
        If Not CekInput(txtTolerable_Del, "Tolerable delivery") Then Exit Sub
        If Not CekInput(txtCurrency_Code, "Currency") Then Exit Sub

        'tolerabel delivery
        v_tolerable = Replace(txtTolerable_Del.Text, ",", ".")

        'Approved/Purchase/FundApp by boleh kosong, tp klo diisi, tgl harus di checked
        If Not CekInputTgl(CTApp, DtApproved, "Approved Date") Then Exit Sub
        If Not CekInputTgl(CTpur, DtPurchased, "Purchased Date") Then Exit Sub
        If Not CekInputTgl(CTFun, DtFundApp, "FundApp Date") Then Exit Sub

        'If DGVDetail.Rows(0).Cells(0).Value Is Nothing Then
        '    MsgBox("PO detail should be filed")
        '    DGVDetail.Focus()
        '    Exit Sub
        'End If
        'MyConn.BeginTransaction()

        txtPO_NO.Focus()
        Dim DT As New System.Data.DataTable
        Dim sprodusen As String
        DT = DGVDetail.DataSource
        If DT.Rows.Count = 0 Then
            MsgBox("PO detail should be filed")
            DGVDetail.Focus()
            Exit Sub
        End If

        sprodusen = produsen.Text
        If sprodusen = "" Then sprodusen = "00000"

        DBQueryUpdate("BEGIN", MyConn, False, "Start Transaction Failed.", UserData)

        SQLstr = "SELECT po_no FROM tbl_po WHERE po_no='" & txtPO_NO.Text & "'"
        ErrMsg = "Fail to check existing PO No."
        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then
            ExistPO = True
            Dim LV_CREATEDBY As String
            LV_CREATEDBY = AmbilData("CREATEDBY", "tbl_po", "po_no ='" & txtPO_NO.Text & "'")
            If LV_CREATEDBY = UserData.UserCT Then
                SQLstr = "UPDATE tbl_po SET " & _
                         "company_code = '" & txtCompany_Code.Text & "', " & _
                         "PLANT_CODE = '" & txtPlant_Code.Text & "', " & _
                         "PORT_CODE = '" & txtPort_Code.Text & "', " & _
                         "LOADPORT_CODE = if('" & txtLoadPort_Code.Text & "'='',null,'" & txtLoadPort_Code.Text & "'), " & _
                         "SHIPMENT_PERIOD_FR = '" & Format(DTPeriodeFR.Value, "yyyy-MM-dd") & "', " & _
                         "SHIPMENT_PERIOD_TO = '" & Format(DTPeriodeTO.Value, "yyyy-MM-dd") & "', " & _
                         "SHIPMENT_TERM_CODE = '" & cbShipTerm.Text.Substring(0, 1) & "', " & _
                         "SUPPLIER_CODE = '" & txtSupplier_Code.Text & "', " & _
                         "PRODUSEN_CODE = '" & sprodusen & "', " & _
                         "CONTRACT_NO = '" & txtContract_No.Text & "', " & _
                         "IPA_NO = '" & txtIPA_No.Text & "', " & _
                         "pr_no='" & txtPR_No.Text & "', " & _
                         "PAYMENT_CODE ='" & txtPayment_Code.Text & "', " & _
                         "TOLERABLE_DELIVERY ='" & v_tolerable & "', " & _
                         "INSURANCE_CODE ='" & TxtInsurance.Text & "', " & _
                         "CURRENCY_CODE ='" & txtCurrency_Code.Text & "', "
                If txtAPPROVEDBY.Text <> "" Then
                    SQLstr = SQLstr & "APPROVEDBY ='" & CTApp.Text & "', APPROVEDDT ='" & Format(DtApproved.Value, "yyyy-MM-dd") & "', "
                End If
                If txtPURCHASEDBY.Text <> "" Then
                    SQLstr = SQLstr & "PURCHASEDBY ='" & CTpur.Text & "', PURCHASEDDT ='" & Format(DtPurchased.Value, "yyyy-MM-dd") & "', "
                End If
                If txtFUNDAPPBY.Text <> "" Then
                    SQLstr = SQLstr & "FUNDAPPBY ='" & CTFun.Text & "', FUNDAPPDT ='" & Format(DtFundApp.Value, "yyyy-MM-dd") & "', "
                End If
                SQLstr = SQLstr & "KURS ='" & CDbl(lblKurs.Text) & "' " & _
                                  "WHERE po_no='" & txtPO_NO.Text & "'"
            Else
                MsgBox("You are not allowed to Edit this PO (created by " & txtCREATEDBY.Text & ")", vbCritical, "Error")
            End If
        Else
            ExistPO = False
            SQLstr = "INSERT INTO tbl_po (po_no, ipa_no, pr_no, shipment_period_fr, " & _
                        "shipment_period_to, shipment_term_code, company_code, plant_code, " & _
                        "port_code, loadport_code, supplier_code, produsen_code,contract_no, currency_code, kurs, " & _
                        "payment_code, tolerable_delivery, insurance_code, note, " & _
                        "createddt, createdby, approvedby, approveddt, purchasedby, purchaseddt, " & _
                        "fundappby, fundappdt,status) " & _
                     "VALUES ('" & txtPO_NO.Text & "', '" & txtIPA_No.Text & "', '" & txtPR_No.Text & "', '" & Format(DTPeriodeFR.Value, "yyyy-MM-dd") & "', " & _
                        "'" & Format(DTPeriodeTO.Value, "yyyy-MM-dd") & "', '" & cbShipTerm.Text.Substring(0, 1) & "', '" & txtCompany_Code.Text & "', '" & txtPlant_Code.Text & "', " & _
                        "'" & txtPort_Code.Text & "',if('" & txtLoadPort_Code.Text & "'='',null,'" & txtLoadPort_Code.Text & "'), '" & txtSupplier_Code.Text & "', '" & sprodusen & "', '" & txtContract_No.Text & "', '" & txtCurrency_Code.Text & "', '" & CDbl(lblKurs.Text) & "', " & _
                        "'" & txtPayment_Code.Text & "', '" & v_tolerable & "', '" & TxtInsurance.Text & "', '', " & _
                        "'" & Format(DTCreated.Value, "yyyy-MM-dd") & "', '" & UserData.UserCT & "' , "
            If txtAPPROVEDBY.Text <> "" Then
                SQLstr = SQLstr & "'" & CTApp.Text & "', '" & Format(DtApproved.Value, "yyyy-MM-dd") & "', "
            Else
                SQLstr = SQLstr & "NULL, NULL, "
            End If
            If txtPURCHASEDBY.Text <> "" Then
                SQLstr = SQLstr & "'" & CTpur.Text & "', '" & Format(DtPurchased.Value, "yyyy-MM-dd") & "', "
            Else
                SQLstr = SQLstr & "NULL, NULL, "
            End If
            If txtFUNDAPPBY.Text <> "" Then
                SQLstr = SQLstr & "'" & CTFun.Text & "', '" & Format(DtFundApp.Value, "yyyy-MM-dd") & "', "
            Else
                SQLstr = SQLstr & "NULL, NULL, "
            End If
            SQLstr = SQLstr & "'" & TxtStatus.Text & "')"
            '" & Format(DtPurchased.Value, "yyyy-MM-dd") & "', '" & CTpur.Text & "' , " & _
            '            "'" & Format(DtApproved.Value, "yyyy-MM-dd") & "','" & CTApp.Text & "' , '" & Format(DtFundApp.Value, "yyyy-MM-dd") & "', '" & CTFun.Text & "')"
        End If
        ErrMsg = "Failed to update PO Header data."
        If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub

        If ExistPO Then
            ' TODO: perlu konfirmasi, apa perlu di clear dulu detail po sebelum update detail po ?
            SQLstr = "DELETE from tbl_po_detail WHERE po_no='" & txtPO_NO.Text & "'"
            ErrMsg = "Failed to clear PO Detail data."
            If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
        End If

        For i = 0 To DGVDetail.RowCount - 1
            ErrMsg = "Failed to update PO detail data."
            'If DGVDetail.Rows(i).Cells(2).Value Is Nothing Or DGVDetail.Rows(i).Cells(2).Value Is DBNull.Value Then
            'Else
            'IsDBNull(DGVDetail.Rows(i).Cells(4).Value) 
            Try
                'lv_poitem = DGVDetail.Rows(i).Cells(4).Value.ToString
                lv_poitem = DGVDetail.Rows(i).Cells("po_item").Value.ToString
            Catch ex As Exception
                lv_poitem = ""
            End Try
            Try
                'lv_matcode = DGVDetail.Rows(i).Cells(5).Value.ToString
                lv_matcode = DGVDetail.Rows(i).Cells("Material_Code").Value.ToString
            Catch ex As Exception
                lv_matcode = ""
            End Try
            Try
                'lv_origin = DGVDetail.Rows(i).Cells(7).Value.ToString
                lv_origin = DGVDetail.Rows(i).Cells("Country_Code").Value.ToString
            Catch ex As Exception
                lv_origin = ""
            End Try
            Try
                'lv_hsno = DGVDetail.Rows(i).Cells(9).Value.ToString
                lv_hsno = DGVDetail.Rows(i).Cells("hs_code").Value.ToString
            Catch ex As Exception
                lv_hsno = ""
            End Try
            Try
                'lv_specs = EscapeStr(DGVDetail.Rows(i).Cells(10).Value.ToString)
                lv_specs = EscapeStr(DGVDetail.Rows(i).Cells("specification").Value.ToString)
            Catch ex As Exception
                lv_specs = ""
            End Try
            Try
                'lv_qty = CDec(DGVDetail.Rows(i).Cells(11).Value)
                lv_qty = CDec(DGVDetail.Rows(i).Cells("Quantity").Value.ToString)
            Catch ex As Exception
            End Try
            'kolom weight dihapus
            'Try
            '    'lv_weight = CDec(DGVDetail.Rows(i).Cells(12).Value)
            '    lv_weight = CDec(DGVDetail.Rows(i).Cells("Weight").Value.ToString)
            'Catch ex As Exception
            'End Try
            Try
                'lv_unitcode = DGVDetail.Rows(i).Cells(13).Value.ToString
                lv_unitcode = DGVDetail.Rows(i).Cells("Unit_Code").Value.ToString
                ' If Not CekInput(lv_unitcode, "Unit Code") Then Exit Sub
                If lv_unitcode = "" Or lv_unitcode = "0" Then
                    MsgBox("Unit Code Tidak Boleh Kosong...!", vbInformation, "PERHATIAN")
                    Exit Sub
                End If
            Catch ex As Exception
                lv_unitcode = ""
            End Try
            Try
                'lv_packcode = DGVDetail.Rows(i).Cells(14).Value.ToString
                lv_packcode = DGVDetail.Rows(i).Cells("Package_code").Value.ToString
            Catch ex As Exception
                lv_packcode = ""
            End Try
            Try
                'lv_price = CDec(DGVDetail.Rows(i).Cells(15).Value)
                lv_price = CDec(DGVDetail.Rows(i).Cells("Price").Value.ToString)
            Catch ex As Exception
            End Try
            Try
                'lv_note = EscapeStr(DGVDetail.Rows(i).Cells(16).Value.ToString)
                lv_note = EscapeStr(DGVDetail.Rows(i).Cells("Note").Value.ToString)
            Catch ex As Exception
                lv_note = ""
            End Try

            If lv_matcode <> "" And Not lv_matcode Is Nothing Then
                insertStr &= "('" & txtPO_NO.Text & "', '" & lv_poitem & "', '" & lv_matcode & "', '" & lv_origin & "', " & _
                            "'" & lv_hsno & "', '" & lv_specs & "', " & Replace(lv_qty.ToString, ",", ".") & ", " & Replace(lv_weight.ToString, ",", ".") & ", '" & lv_unitcode & "', " & _
                            "'" & lv_packcode & "', " & Replace(lv_price.ToString, ",", ".") & ", '" & lv_note & "', '" & lv_shipmentno & "'), "
            End If
            'End If
        Next

        If edit Then
            SQLstr = "DELETE from tbl_po_detail WHERE PO_NO='" & txtPO_NO.Text & "'"
            If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
        End If
        ' insert data
        If insertStr <> "" Then
            insertStr = insertStr.Substring(0, insertStr.Length - 2)
            SQLstr = "INSERT INTO tbl_po_detail (po_no, po_item, material_code, country_code, hs_code, specification, " & _
                        "quantity, weight, unit_code, package_code, price, note, shipment_no) " & _
                     "VALUES " & insertStr
            If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
        Else
            MsgBox("PO detail should be filed")
            DGVDetail.Focus()
            Exit Sub
        End If

        SQLstr = "Update tbm_material Set hs_code='" & lv_hsno & "' where material_code='" & lv_matcode & "'"
        If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
        ' commit transaction

        Dim temp, tgl, tgl2 As String
        temp = AmbilData("contract_no", "tbl_contract", "contract_no='" & txtContract_No.Text & "'")
        If temp = "" And txtContract_No.Text <> "" Then
            'tgl = "Format(curdate(), " & Chr(34) & "yyyy-MM-dd" & Chr(34) & ")"
            tgl = "curdate()"
            tgl2 = "DATE_ADD(curdate(),INTERVAL +3 MONTH)"
            insertStr = "('" & txtContract_No.Text & "'," & tgl & "," & tgl & "," & tgl2 & ",'" & txtCompany_Code.Text & "','" _
            & txtSupplier_Code.Text & "'," & tgl & ",'" & txtCurrency_Code.Text & "','" & "Open" & "'," & "now()" & ",'" & UserData.UserCT & "',0)"
            SQLstr = "INSERT INTO tbl_contract (contract_no,contract_Dt,contract_period_fr,contract_period_to," _
            & "company_code,supplier_code,delivery_bydt,currency_code,status,createddt,createdby,amount) VALUES " & insertStr
            'If txtContract_No.Text <> "" Then
            If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
        End If
        MsgBox("Data saved!", MsgBoxStyle.Information, "Success")
        DBQueryUpdate("COMMIT", MyConn, False, "Commit Transaction Failed.", UserData)
        txtTotal.Text = FormatNumber(FrmDOC_Import.GetAmount(txtPO_NO.Text), 2)

        Dim obj As Object
        Dim ee As System.EventArgs

        'di tutup agar data setelah di simpan tidak di kosongkan
        'btnNew_Click(obj, ee)
    End Sub
    Private Sub txtPO_NO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPO_NO.TextChanged
        'btnSave.Enabled = (Len(Trim(txtPO_NO.Text)) > 0) 
        'refreshGrid(txtPO_NO.Text)
        'f_getpoheader(txtPO_NO.Text)
    End Sub

    Private Sub btnSearchMat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchMat.Click
        PilihanDlg.Text = "Select Material Code"
        PilihanDlg.Width = 600
        PilihanDlg.Height = 402
        PilihanDlg.DgvResult.Width = 570
        PilihanDlg.DgvResult.Height = 267
        PilihanDlg.LblKey1.Text = "Material Code"
        PilihanDlg.LblKey2.Text = "Material Name"
        PilihanDlg.SQLGrid = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                             "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                             "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code"
        PilihanDlg.SQLFilter = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                               "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                               "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code " & _
                               "WHERE tm.material_code LIKE 'FilterData1%' AND " & _
                               "tm.Material_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            v_matcode = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            v_matdesc = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            v_hscode = PilihanDlg.DgvResult.CurrentRow.Cells.Item(4).Value.ToString
        End If
    End Sub

    Private Sub btnSearchOri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchOri.Click
        PilihanDlg.Text = "Select Country Code"
        PilihanDlg.LblKey1.Text = "Country Code"
        PilihanDlg.LblKey2.Text = "Country Name"
        PilihanDlg.SQLGrid = "select tc.country_code as CountryCode, tc.country_name as CountryName from tbm_country as tc " & _
                             "inner join tbm_material_origin as tm on tm.country_code = tc.country_code " & _
                             "where tm.material_code = '" & sender.ToString & "'"
        PilihanDlg.SQLFilter = "select tc.country_code as CountryCode, tc.country_name as CountryName from tbm_country as tc " & _
                             "inner join tbm_material_origin as tm on tm.country_code = tc.country_code " & _
                             "where tm.material_code = '" & sender.ToString & "' and tm.country_code LIKE 'FilterData1%' and tc.country_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_country as tc inner join tbm_material_origin as tm on tm.country_code = tc.country_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            v_origin_code = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            v_origin_name = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

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

    Private Sub btnSearchDestin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDestin.Click
        PilihanDlg.Text = "Select Plant"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "SELECT PLANT_CODE as PlantCode, PLANT_NAME as PlantName, CITY_CODE as CityCode FROM tbm_plant"
        '                             "company_code = '" & txtCompany_Code.Text & "'"
        PilihanDlg.SQLFilter = "SELECT PLANT_CODE AS PlantCode, PLANT_NAME as PlantName, CITY_CODE as CityCode FROM tbm_plant " & _
                               "WHERE PLANT_CODE LIKE 'FilterData1%' AND " & _
                                    "PLANT_NAME LIKE 'FilterData2%' and " & _
                                    "COMPANY_CODE = '" & txtCompany_Code.Text & "'"
        PilihanDlg.Tables = "tbm_plant"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPlant_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPlant_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString

        End If
    End Sub

    Private Sub btnSearchPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPort.Click
        'Dim lv_cityplant As String
        Dim lv_countrycomp As String

        'lv_cityplant = AmbilData("city_code", "tbm_plant", "plant_code='" & txtPlant_Code.Text & "'")
        lv_countrycomp = AmbilData("t2.country_code", "tbm_city as t2 inner join tbm_company as t1", "t1.city_code = t2.city_code and t1.company_code ='" & txtCompany_Code.Text & "'")
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        PilihanDlg.SQLGrid = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port " & _
                             "where country_code = '" & lv_countrycomp & "'"
        PilihanDlg.SQLFilter = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port " & _
                               "WHERE port_code LIKE 'FilterData1%' AND " & _
                                    "port_name LIKE 'FilterData2%' AND " & _
                                    "country_code = '" & lv_countrycomp & "'"
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPort_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPort_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSearchPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPayment.Click
        PilihanDlg.Text = "Select Payment Code"
        PilihanDlg.LblKey1.Text = "Payment Code"
        PilihanDlg.LblKey2.Text = "Payment Name"
        PilihanDlg.SQLGrid = "SELECT payment_code as PaymentCode, payment_name as PaymentName FROM tbm_payment_term"
        PilihanDlg.SQLFilter = "SELECT payment_code as PaymentCode, payment_name as PaymentName FROM tbm_payment_term " & _
                               "WHERE payment_code LIKE 'FilterData1%' AND " & _
                                    "payment_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_payment_term"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPayment_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPayment_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSearchSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSupplier.Click
        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName, Bank_Name as BankName, Account_No as AccountNo FROM tbm_supplier WHERE active='1' "
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
            'lblPayment_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSearchInsurance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchInsurance.Click
        PilihanDlg.Text = "Select Insurance"
        PilihanDlg.LblKey1.Text = "Incoterm Code"
        PilihanDlg.LblKey2.Text = "Incoterm Name"
        PilihanDlg.SQLGrid = "SELECT INSURANCE_CODE as IncotermCode, INSURANCE_DESCRIPTION as Description FROM tbm_insurance"
        PilihanDlg.SQLFilter = "SELECT INSURANCE_CODE as InsuranceCode, INSURANCE_DESCRIPTION as InsuranceDescription FROM tbm_insurance " & _
                               "WHERE INSURANCE_CODE LIKE 'FilterData1%' AND " & _
                                    "INSURANCE_DESCRIPTION LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_insurance"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtInsurance.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblInsurance_Desc.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub txtCurrency_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCurrency_Code.TextChanged
        If actv And txtCurrency_Code.Text <> "" Then
            Call f_getkurs()
        End If
    End Sub
    Private Sub f_getkurs()
        SQLstr = "select * from tbm_kurs where CURRENCY_CODE = '" & txtCurrency_Code.Text & "' " & _
                 "and EFFECTIVE_DATE='" & Format(Now, "yyyy-MM-dd") & "' "
        ErrMsg = "There's no effective rate for today. Please maintain!"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        lblKurs.Text = FormatNumber(0, 2, , , TriState.True)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    lblKurs.Text = MyReader.GetString("effective_kurs")
                    lblKurs.Text = FormatNumber(lblKurs.Text, 2)
                    'CInt(MyReader.GetString("effective_kurs"))
                Catch ex As Exception
                    lblKurs.Text = FormatNumber(0, 2, , , TriState.True)
                End Try
            End While
            If MyReader.HasRows = False Then
                MsgBox(ErrMsg, MsgBoxStyle.Critical, "Warning")
            End If
            CloseMyReader(MyReader, UserData)
        End If

    End Sub

    Private Sub txtCompany_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompany_Code.TextChanged
        If txtCompany_Code.Text = "" Then
            btnSearchDestin.Enabled = False
        Else
            btnSearchDestin.Enabled = True
        End If
    End Sub

    Private Sub btnUserPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserPur.Click
        Dim lv_mod As String
        If sender Is "App" Then
            lv_mod = "PO-A"
        ElseIf sender Is "Fun" Then
            lv_mod = "FI-C"
        Else
            lv_mod = "PO-C"
        End If
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = '" & lv_mod & "' "
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT, tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = '" & lv_mod & "' " & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name like 'FilterData2%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Dim sender As System.Object
            'Select Case sender
            '    Case ""
            '        
            '    Case "App"
            'End Select
            If sender Is "App" Then
                txtAPPROVEDBY.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
                CTApp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            ElseIf sender Is "Fun" Then
                txtFUNDAPPBY.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
                CTFun.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Else
                txtPURCHASEDBY.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
                CTpur.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            End If
        End If
    End Sub


    Private Sub btnUserApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserApp.Click
        Call btnUserPur_Click("App", e)
    End Sub

    Private Sub btnUserFun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserFun.Click
        Call btnUserPur_Click("Fun", e)
    End Sub



    Private Sub txtPO_NO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPO_NO.Validated
        'refreshGrid(txtPO_NO.Text)
        'f_getpoheader(txtPO_NO.Text)

    End Sub


    Private Sub btnListPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListPO.Click
        'f_caller = "FrmPO"
        'FrmListPO.ShowDialog()
        If PilihPO.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ClearScreen()
            txtPO_NO.Text = PilihPO.v_pono.Text
            refreshGrid(txtPO_NO.Text)
            f_getpoheader(txtPO_NO.Text)
            txtPO_NO.Focus()

            edit = True
            baru = False
            btnSave.Enabled = (Len(Trim(txtPO_NO.Text)) > 0) And (UserData.UserCT = crtcode.Text) And (TxtStatus.Text <> "Closed")
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub txtTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.TextChanged
        'txtTotal.Text = FormatNumber(txtTotal.Text, 2)
    End Sub

    Private Sub txtTotal_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTotal.Validating
        Try
            txtTotal.Text = FormatNumber(txtTotal.Text, 2)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub txtPlant_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlant_Code.TextChanged
        If txtPlant_Code.Text = "" Then
            btnSearchPort.Enabled = False
        Else
            btnSearchPort.Enabled = True
        End If
    End Sub

    Private Sub DGVDetail_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DGVDetail.UserDeletingRow
        If (Not e.Row.IsNewRow) Then
            Dim response As DialogResult = _
            MessageBox.Show( _
            "Are you sure you want to delete this row?", _
            "Delete row?", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button2)
            If (response = DialogResult.No) Then
                e.Cancel = True
            End If
        End If

    End Sub


    Private Sub DGVDetail_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDetail.CellValidated
        If DGVDetail.Columns(e.ColumnIndex).Name = "HSNo" Then

            'DGVDetail.Rows(x).Cells("No").Value = counter
            'DGVDetail.RowCount = counter
            'DGVDetail.Rows(x).Cells("No").Value = counter
        End If
    End Sub
    Private Sub DGVDetail_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DGVDetail.CellValidating

        Dim v_qty As Double
        Dim v_price As Double
        Dim v_total As Double
        '---Price field---
        'If e.ColumnIndex = 11 Or e.ColumnIndex = 12 Or e.ColumnIndex = 15 Then
        If DGVDetail.Columns(e.ColumnIndex).Name = "Quantity" Or _
           DGVDetail.Columns(e.ColumnIndex).Name = "Price" Then
            If Not IsNumeric(e.FormattedValue) Then
                DGVDetail.Rows(e.RowIndex).ErrorText = _
                   "Please enter numeric value for " & DGVDetail.Columns(e.ColumnIndex).Name
                e.Cancel = True
            End If
        End If
        'If DGVDetail.Rows(e.RowIndex).Cells(9).Value.ToString <> "" And DGVDetail.Rows(e.RowIndex).Cells(13).Value.ToString <> "" Then
        '    v_qty = CDbl(DGVDetail.Rows(e.RowIndex).Cells(9).Value.ToString)
        '    v_price = CDbl(DGVDetail.Rows(e.RowIndex).Cells(13).Value.ToString)
        '    v_total = v_qty * v_price
        '    DGVDetail.Rows(e.RowIndex).Cells(2).Value = FormatNumber(v_total, 2)
        'End If
    End Sub
    Private Sub DGVDetail_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDetail.CellEndEdit
        Dim v_qty As Double
        Dim v_price As Double
        Dim v_total As Double

        '---clear the error message---
        DGVDetail.Rows(e.RowIndex).ErrorText = String.Empty
        'If e.ColumnIndex = 11 Or e.ColumnIndex = 12 Or e.ColumnIndex = 15 Then
        'DGVDetail.Columns(e.ColumnIndex).Name = "Weight" Or _
        If DGVDetail.Columns(e.ColumnIndex).Name = "quantity" Or _
           DGVDetail.Columns(e.ColumnIndex).Name = "price" Then
            If Not IsNumeric(DGVDetail.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString) Then


                'DGVDetail.Rows(e.RowIndex).ErrorText = _
                '   "Please enter numeric value for " & DGVDetail.Columns(e.ColumnIndex).Name
                'e.Cancel = True
            Else
                If DGVDetail.Rows(e.RowIndex).Cells(11).Value.ToString <> "" And DGVDetail.Rows(e.RowIndex).Cells(16).Value.ToString <> "" Then
                    v_qty = CDbl(DGVDetail.Rows(e.RowIndex).Cells(11).Value.ToString)
                    v_price = CDbl(DGVDetail.Rows(e.RowIndex).Cells(16).Value.ToString)
                    v_total = v_qty * v_price
                    DGVDetail.Rows(e.RowIndex).Cells(17).Value = v_total

                    DGVDetail.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    DGVDetail.Columns(17).DefaultCellStyle.Format = "N2"
                    'txtTotal.Text = txtTotal.Text + v_total
                End If
                'DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value
            End If
        End If

    End Sub
    Private Sub DGVDetail_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDetail.CellValueChanged
        'counter += 1

        'x = e.RowIndex + 1
        If DGVDetail.Columns(e.ColumnIndex).Name = "No" Then
            'DGVDetail.Rows(x).Cells("No").Value = counter
            'btnMat.Text = "Search"
            'btnMat.UseColumnTextForButtonValue = True
            'DGVDetail.Columns.Add(btnMat)
            'btnMat.Image = POIM.My.Resources.search
        End If
    End Sub

    Private Sub btnCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCD.Click
        PilihanDlg.Text = "Select Contract No."
        PilihanDlg.LblKey1.Text = "Contract No."
        PilihanDlg.LblKey2.Text = "Company Code "
        PilihanDlg.SQLGrid = "select tc.Contract_no as ContractNo, tc.Contract_dt as ContractDate, tc.Company_code as CompanyCode, " & _
                            "tco.company_name as CompanyName, tc.Supplier_code as SupplierCode, ts.supplier_name as SupplierName " & _
                            "from tbl_contract as tc " & _
                            "inner join tbm_company as tco on tc.company_code = tco.company_code " & _
                            "inner join tbm_supplier as ts on tc.supplier_code = ts.supplier_code "
        PilihanDlg.SQLFilter = PilihanDlg.SQLGrid & _
                               "WHERE tc.Contract_no LIKE 'FilterData1%' AND " & _
                                    "tc.Company_code LIKE 'FilterData2%' "
        PilihanDlg.Tables = "from tbl_contract as tc inner join tbm_company as tco on tc.company_code = tco.company_code inner join tbm_supplier as ts on tc.supplier_code = ts.supplier_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtContract_No.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PilihanDlg.Text = "Select Produsen Code"
        PilihanDlg.LblKey1.Text = "Produsen Code"
        PilihanDlg.LblKey2.Text = "Produsen Name"
        PilihanDlg.SQLGrid = "Select produsen_code, produsen_name from tbm_produsen"
        PilihanDlg.SQLFilter = "Select produsen_code, produsen_name from tbm_produsen " & _
                               "WHERE produsen_code LIKE 'FilterData1%' AND produsen_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then produsen.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub produsen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles produsen.TextChanged
        prodname.Text = AmbilData("produsen_name", "tbm_produsen", "produsen_code='" & produsen.Text & "'")
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearScreen()
        RefreshScreen()
        refreshGrid("")
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim hasil As Boolean
        Dim EE As System.EventArgs
        Dim strSQL As String

        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub
        strSQL = "Run Stored Procedure DelPO (" & txtPO_NO.Text & "," & UserData.UserCT & ")"
        With MyComm
            .CommandText = "DelPO"
            .CommandType = CommandType.StoredProcedure

            With .Parameters
                .Clear()
                .AddWithValue("PO", txtPO_NO.Text)
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("AuditStr", strSQL)
                .AddWithValue("Hasil", hasil)
            End With
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End With

        If hasil = True Then
            f_msgbox_successful("Delete PO")
            btnNew_Click(sender, EE)
        Else
            MsgBox("Delete PO failed. Bill of Lading Documents has been created")
        End If
    End Sub

    Private Sub txtPO_NO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPO_NO.Validating
        If (AmbilData("po_no", "tbl_po", "po_no='" & txtPO_NO.Text & "'") <> "") Then
            If Not f_GetAuthorized(txtPO_NO.Text) Then txtPO_NO.Text = ""
        End If
        refreshGrid(txtPO_NO.Text)
        f_getpoheader(txtPO_NO.Text)
        btnSave.Enabled = (Len(Trim(txtPO_NO.Text)) > 0) And (UserData.UserCT = crtcode.Text) And (TxtStatus.Text <> "Closed")
    End Sub

    Private Function f_GetAuthorized(ByVal pono As String) As Boolean
        Dim bAuthorized As Boolean = False

        SQLstr = "SELECT po_no FROM tbl_po WHERE po_no='" & pono & "' " & _
                 "AND company_code IN (SELECT company_code FROM tbm_users_company WHERE user_ct='" & UserData.UserCT & "') "

        ErrMsg = "Failed when read Purchase Order"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            If MyReader.HasRows = True Then
                bAuthorized = True
            End If
        End If
        CloseMyReader(MyReader, UserData)

        If Not bAuthorized Then
            MsgBox("You do not have permission to access PO from this company!", vbInformation, "Warning")
        End If

        Return bAuthorized
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLoadPort.Click

        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        PilihanDlg.SQLGrid = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port "
        PilihanDlg.SQLFilter = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port " & _
                               "WHERE port_code LIKE 'FilterData1%' AND " & _
                                    "port_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtLoadPort_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblLoadPort_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSchedulePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedulePO.Click
        Dim PilihItem As New FrmPOSchedule(txtPO_NO.Text, crtcode.Text)
        PilihItem.ShowDialog()
    End Sub

    Private Sub btnClosing_text()
        If TxtStatus.Text = "Closed" Then
            btnClosing.Text = "Unclose PO"
        Else
            btnClosing.Text = "Closing PO"
        End If
    End Sub

    Private Sub btnClosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosing.Click
        Dim Stat As String = "Closed"

        If TxtStatus.Text = "Closed" Then Stat = "Open"

        SQLstr = "UPDATE tbl_po SET status = '" & Stat & "' " & _
                 "WHERE po_no='" & txtPO_NO.Text & "'"

        ErrMsg = "Failed to update PO  data."
        If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub

        MsgBox(btnClosing.Text & "!", MsgBoxStyle.Information, "Success")
        DBQueryUpdate("COMMIT", MyConn, False, "Commit Transaction Failed.", UserData)

        TxtStatus.Text = Stat
        btnClosing_text()
    End Sub

    Private Sub DGVDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDetail.CellContentClick

    End Sub
End Class