''' <summary>
''' Title                : Transaksi Closing PO  ==> ubah status PO
''' Form                 : FrmClosingPO
''' Table Used           : tbl_tbl_po, tbm_material, tbm_users
''' Form Created By      : Estrika, 02.11.2010
''' Program Coding By    : Estrika, 02.11.2010
''' Modify By            : -
''' Modify Note          : - 
'''                          
''' 
''' </summary>
''' <remarks></remarks>

Public Class FrmClosingPO
    Dim ErrMsg, SQLstr As String
    Dim affrow, TotalDoc As Integer
    Dim MyReader As MySqlDataReader
    Dim mTahun As String = Format(Now, "yyyy")
    Dim temp, SQLWhr, xView As String

    Dim PilihanDlg As New DlgPilihan

    Private Sub FrmClosingPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        btnSave.Enabled = False
        For xx = 2008 To CInt(mTahun)
            With CmbPeriode.Items
                .Add(xx)
            End With
        Next
        CmbPeriode.Text = mTahun
        cbStatus.SelectedIndex = 2
        lblPlant_Name.Text = ""
        lblPort_Name.Text = ""
        lblCompany_Name.Text = ""
        lblSuppName.Text = ""
        lblMatGrp.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        'If CekOpsi() = False Then Exit Sub
        btnSave.Enabled = True
        GetModul()
        Select Case cbStatus.Text
            Case "Open (All)"
                xView = "vw_qtypo_vs_qtyshipment"
                SQLWhr = SQLWhr & " AND v1.STATUS = 'Open' "
            Case "Open (Diff<Tolerable)"
                xView = "vw_toclosed_po"
                SQLWhr = ""
            Case "Closed"
                xView = "vw_qtypo_vs_qtyshipment"
                SQLWhr = SQLWhr & " AND v1.status='Closed' "
            Case "PurchaseDate Not Completed"
                xView = "vw_qtypo_vs_qtyshipment"
                SQLWhr = ""
        End Select
        OpenAll()
    End Sub
    Private Function CekOpsi() As Boolean
        If cbStatus.Text = "" Then
            MsgBox("Select Status ! ", MsgBoxStyle.Critical, "Warning")
            cbStatus.Focus()
            Return False
        ElseIf CmbPeriode.Text = "" Then
            MsgBox("Select Periode ! ", MsgBoxStyle.Critical, "Warning")
            CmbPeriode.Focus()
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub OpenAll()
        Dim dts As DataTable
        Dim chkClosed As New DataGridViewCheckBoxColumn
        Dim Closed As Integer
        Dim SQLWhr2 As String

        SQLWhr2 = "AND (''='" & txtMatGrp.Text & "' OR v1.material_code IN (SELECT material_code FROM tbm_material WHERE group_code = '" & txtMatGrp.Text & "')) " & _
                  "AND (''='" & userct.Text & "' OR v1.createdby='" & userct.Text & "') " & _
                  "AND ('-'='" & txtLine_Code.Text & "' OR v1.po_no IN (SELECT po_no FROM tbl_po t1, tbm_plant t2, tbm_company t3 WHERE t1.plant_code=t2.plant_code AND t2.company_code=t3.company_code AND t3.line_bussines='" & txtLine_Code.Text & "')) " & _
                  "AND (''='" & txtPlant_Code.Text & "' OR v1.po_no IN (SELECT po_no FROM tbl_po WHERE plant_code='" & txtPlant_Code.Text & "')) " & _
                  "AND (''='" & txtPort_Code.Text & "' OR v1.po_no IN (SELECT po_no FROM tbl_po WHERE port_code='" & txtPort_Code.Text & "')) " & _
                  "AND (''='" & txtCompany_Code.Text & "' OR v1.po_no IN (SELECT po_no FROM tbl_po t1, tbm_plant t2 WHERE t1.plant_code=t2.plant_code AND t2.company_code='" & txtCompany_Code.Text & "')) " & _
                  "AND (''='" & txtSuppCode.Text & "' OR v1.po_no IN (SELECT po_no FROM tbl_po WHERE supplier_code='" & txtSuppCode.Text & "')) " & _
                  ""

        DGVClosing.DataSource = Nothing
        DGVClosing.Columns.Clear()
        DGVClosing.Rows.Clear()

        If cbStatus.SelectedIndex > 0 Then
            SQLstr = "SELECT if(v1.status='Closed',true,false) AS closed,v1.status ,v1.po_no, GROUP_CONCAT(TRIM(m1.material_name) SEPARATOR ',') MaterialName, " & _
                     "cast(GROUP_CONCAT(TRIM(v1.poqty) SEPARATOR ',') AS char) PoQty, cast(v1.potolerable AS char) Potolerable, " & _
                     "cast(GROUP_CONCAT(TRIM(v1.shipqty) SEPARATOR ',') AS char) ShipQty, " & _
                     "cast(GROUP_CONCAT(TRIM(v1.tolerabled) SEPARATOR ',') AS char) Tolerabled, " & _
                     "M2.NAME,v1.purchaseddt " & _
                     "FROM " & xView & " AS v1  " & _
                     "INNER JOIN tbm_material AS m1 ON v1.material_code = m1.material_code " & _
                     "INNER JOIN tbm_users AS m2 ON v1.createdby = m2.user_ct " & _
                     "WHERE (year(v1.purchaseddt)='" & CmbPeriode.Text & "') " & SQLWhr & SQLWhr2 & " " & _
                     "GROUP BY v1.po_no "

                '"WHERE (year(v1.purchaseddt)='" & CmbPeriode.Text & "' OR v1.purchaseddt is null) " & SQLWhr & " " & _
        Else
            SQLstr = "SELECT '' AS hide1, '' AS hide2, v1.status ,v1.po_no, GROUP_CONCAT(TRIM(m1.material_name) SEPARATOR ',') MaterialName, " & _
                      "cast(GROUP_CONCAT(TRIM(v1.poqty) SEPARATOR ',') AS char) PoQty, cast(v1.potolerable AS char) Potolerable, " & _
                      "cast(GROUP_CONCAT(TRIM(v1.shipqty) SEPARATOR ',') AS char) ShipQty, " & _
                      "cast(GROUP_CONCAT(TRIM(v1.tolerabled) SEPARATOR ',') AS char) Tolerabled, " & _
                      "M2.NAME,v1.purchaseddt " & _
                      "FROM " & xView & " AS v1  " & _
                      "INNER JOIN tbm_material AS m1 ON v1.material_code = m1.material_code " & _
                      "INNER JOIN tbm_users AS m2 ON v1.createdby = m2.user_ct " & _
                      "WHERE (v1.purchaseddt is null) " & SQLWhr & SQLWhr2 & " " & _
                      "GROUP BY v1.po_no "
            End If

        If DBQueryGetTotalRows(SQLstr, MyConn, "Failed when read user data", False, UserData) <= 0 Then
            MsgBox("Data not found...", MsgBoxStyle.Exclamation, "Confirmation")
            Exit Sub
            End If
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        DGVClosing.DataSource = dts
        DGVClosing.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGVClosing.Columns(0).Visible = False

        If cbStatus.SelectedIndex > 0 Then
            With chkClosed
                .DataPropertyName = "chkClosed"
                .Name = "Closed"
                .HeaderText = "Double Click to change status (Closed/Open)"
                .Width = 90
                End With
            DGVClosing.Columns.Insert(0, chkClosed)
            Dim i As Integer
            For i = 0 To dts.Rows.Count - 1
                DGVClosing.Rows(i).Cells(0).Value = dts.Rows(i).Item("Closed")
            Next
        Else
            DGVClosing.Columns(0).Visible = False
            DGVClosing.Columns(1).Visible = False
            End If
        DGVClosing.Columns(2).HeaderText = "Status"
        DGVClosing.Columns(2).Width = 60
        DGVClosing.Columns(3).HeaderText = "PO"
        DGVClosing.Columns(3).Width = 100
        DGVClosing.Columns(4).HeaderText = "DescriptionOfGoods"
        DGVClosing.Columns(4).Width = 100
        DGVClosing.Columns(5).HeaderText = "PO Quantity"
        DGVClosing.Columns(5).Width = 100
        DGVClosing.Columns(6).HeaderText = "Tolerable Delivery (%)"
        DGVClosing.Columns(6).Width = 60
        DGVClosing.Columns(7).HeaderText = "Shipment Qty"
        DGVClosing.Columns(7).Width = 100
        DGVClosing.Columns(8).HeaderText = "Different (%)"
        DGVClosing.Columns(8).Width = 100
        DGVClosing.Columns(9).HeaderText = "PO CreatedBy"
        DGVClosing.Columns(9).Width = 100
        DGVClosing.Columns(10).HeaderText = "Purchase Date"
        DGVClosing.Columns(10).Width = 100
    End Sub
    Private Function GetModul() As Boolean
        SQLstr = "SELECT * FROM tbm_users_modul WHERE user_ct='" & UserData.UserCT & "' AND modul_code='FM01' "
        ErrMsg = "Failed when read tbm_users_modul"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                SQLWhr = ""
                CloseMyReader(MyReader, UserData)
                Return True
            End While
        End If
        SQLWhr = "AND v1.createdby='" & UserData.UserCT & "' "
        CloseMyReader(MyReader, UserData)
        Return False
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Simpan()
        OpenAll()
    End Sub

    Private Sub DGVClosing_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGVClosing.CellMouseClick
        
    End Sub

    Sub SetCheck(ByVal Value As Long, ByVal e As Integer, ByVal check As Boolean)
        Static Busy As Boolean
        If Busy Then Exit Sub
        Busy = True
        DGVClosing.Rows(e).Cells(0).Value = check And ((Value = 1) Or (Value = 3))
        Busy = False
    End Sub
    Private Sub Simpan()
        Dim PoNo, xStatus As String
        Dim UpdateDB, xCheck As Boolean
        DGVClosing.CommitEdit(DataGridViewDataErrorContexts.Commit)
        DBQueryUpdate("BEGIN", MyConn, False, "Gagal Start Transaction.", UserData)
        For i As Integer = DGVClosing.Rows.GetFirstRow(DataGridViewElementStates.Visible) To DGVClosing.Rows.GetLastRow(DataGridViewElementStates.Visible)
            If (DGVClosing.Rows.Item(i).Cells.Item(0).Value.ToString = "1" Or DGVClosing.Rows.Item(i).Cells.Item(0).Value.Equals(True)) Then xCheck = True Else xCheck = False
            If xCheck Then xStatus = "Closed" Else xStatus = "Open"
            PoNo = DGVClosing.Rows.Item(i).Cells.Item(3).Value.ToString

            SQLstr = "UPDATE tbl_po " & _
                     "SET status='" & xStatus & "' " & _
                     "WHERE Po_no='" & PoNo & "' "

            affrow = DBQueryUpdate(SQLstr, MyConn, False, "Saving failed", UserData)
            If affrow < 0 Then
                UpdateDB = False
                CloseMyReader(MyReader, UserData)
                Exit Sub
            Else
                UpdateDB = True
            End If
        Next

        If UpdateDB Then
            ' commit insert data
            DBQueryUpdate("COMMIT", MyConn, False, "Saving failed...", UserData)
            MsgBox("Saving Success...", MsgBoxStyle.Information, "Confirmation")
        Else
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Confirmation")
        End If
    End Sub

    Private Sub DGVClosing_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVClosing.CellContentClick
        If e.ColumnIndex = 0 Then
            If DGVClosing.Rows(e.RowIndex).Cells(10).Value.ToString = "" Then
                DGVClosing.Columns(0).ReadOnly = True
            Else
                DGVClosing.Columns(0).ReadOnly = False
                If DGVClosing.Rows(e.RowIndex).Cells(0).Value.ToString = "1" Then
                    SetCheck(1, e.RowIndex, True)
                Else
                    SetCheck(0, e.RowIndex, True)
                End If
            End If
        End If
    End Sub

    Private Sub btnUserPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserPur.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        PilihanDlg.LblKey2.Visible = False
        PilihanDlg.TxtKey2.Visible = False
        PilihanDlg.SQLGrid = "Select tu.user_ct UserCT,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-C' "
        PilihanDlg.SQLFilter = "Select tu.user_ct UserCT,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-C' " & _
                               "and tu.name LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCreatedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            userct.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"
        PilihanDlg.SQLGrid = "select tc.company_code CompanyCode, tc.company_name CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                             "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'"
        PilihanDlg.SQLFilter = "select tc.company_code CompanyCode, tc.company_name CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                               "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'" & _
                               "and tc.company_code LIKE 'FilterData1%' AND " & _
                               "tc.company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company as tc inner join tbm_users_company as tuc on tc.COMPANY_CODE = tuc.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCompany_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSup.Click
        PilihanDlg.Text = "Select Supplier"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "select ts.supplier_code SupplierCode, ts.supplier_name SupplierName from tbm_supplier as ts"
        PilihanDlg.SQLFilter = "select ts.supplier_code SupplierCode, ts.supplier_name SupplierName from tbm_supplier as ts " & _
                               "where ts.supplier_code LIKE 'FilterData1%' AND " & _
                               "ts.supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier as ts"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSuppCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblSuppName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnMatgrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMatgrp.Click
        PilihanDlg.Text = "Select Material Group"
        PilihanDlg.LblKey1.Text = "Material Code"
        PilihanDlg.SQLGrid = "SELECT group_code GroupMaterialCode, group_name GroupMaterialName FROM tbm_material_group"
        PilihanDlg.SQLFilter = "SELECT group_code GroupMaterialCode, group_name GroupMaterialName FROM tbm_material_group " & _
                               "WHERE group_code LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtMatGrp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblMatGrp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub txtCompany_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompany_Code.TextChanged
        lblCompany_Name.Text = GetData("SELECT company_name FROM tbm_company WHERE company_code='" & txtCompany_Code.Text & "'")
    End Sub

    Function GetData(ByVal strSQL As String) As String
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = strSQL
        MyComm.CommandType = CommandType.Text
        Try
            GetData = MyComm.ExecuteScalar()
        Catch ex As Exception
            GetData = Nothing
        End Try
        MyComm.Dispose()
    End Function

    Private Sub txtSuppCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSuppCode.TextChanged
        lblSuppName.Text = GetData("SELECT supplier_name FROM tbm_supplier WHERE supplier_code='" & txtSuppCode.Text & "'")
    End Sub

    Private Sub txtMatGrp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMatGrp.TextChanged
        lblMatGrp.Text = GetData("SELECT group_name FROM tbm_material_group WHERE group_code='" & txtMatGrp.Text & "'")
    End Sub

    Private Sub btnSearchPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPort.Click
        PilihanDlg.Text = "Select Port"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        PilihanDlg.SQLGrid = "select ts.port_code PortCode, ts.port_name PortName from tbm_port as ts"
        PilihanDlg.SQLFilter = "select ts.port_code PortCode, ts.port_name PortName from tbm_port as ts " & _
                               "where ts.port_code LIKE 'FilterData1%' AND " & _
                               "ts.port_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_port as ts"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPort_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPort_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSearchPlant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPlant.Click
        PilihanDlg.Text = "Select Plant"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "select ts.plant_code PlantCode, ts.plant_name PlantName from tbm_plant as ts"
        PilihanDlg.SQLFilter = "select ts.plant_code PlantCode, ts.plant_name PlanttName from tbm_plant as ts " & _
                               "where ts.plant_code LIKE 'FilterData1%' AND " & _
                               "ts.plant_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_plant as ts"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPlant_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPlant_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSearchLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLine.Click
        PilihanDlg.Text = "Select Bussines Line"
        PilihanDlg.LblKey1.Text = "Bussines Line"
        PilihanDlg.LblKey2.Text = ""
        PilihanDlg.SQLGrid = "SELECT DISTINCT line_bussines FROM tbm_company"
        PilihanDlg.SQLFilter = "SELECT DISTINCT line_bussines FROM tbm_company " & _
                               "WHERE line_bussines LIKE  'FilterData1%'"
        PilihanDlg.Tables = "tbm_company"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtLine_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub txtPort_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPort_Code.TextChanged
        lblPort_Name.Text = GetData("SELECT port_name FROM tbm_port WHERE port_code='" & txtPort_Code.Text & "'")
    End Sub

    Private Sub txtPlant_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlant_Code.TextChanged
        lblPlant_Name.Text = GetData("SELECT plant_name FROM tbm_plant WHERE plant_code='" & txtPlant_Code.Text & "'")
    End Sub
End Class