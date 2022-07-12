'Title                         : Tanda Terima CostSlip

Imports CrystalDecisions.CrystalReports.Engine
Public Class TandaTerimaCSlip
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Sub New()
        InitializeComponent()
        btnSave.Enabled = False
        btnPrint.Enabled = False
    End Sub
    Private Sub TandaTerimaCSlip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen("", "", "")
    End Sub
    Private Sub RefreshScreen(ByVal dt As String, ByVal by As String, ByVal comp As String)
        Dim brs As Integer
        Dim qt0, qt2 As String
        Dim str1 As String

        qt0 = FormatNumber(0, 0, , , TriState.True)
        qt2 = FormatNumber(0, 2, , , TriState.True)

        If dt = "" Then
            tgl.Value = Now()
            txtBy.Clear()
            txtByName.Clear()
            txtPOBy.Clear()
            TxtPOByName.Clear()
            txtCompany.Clear()
            tgl.Checked = False

            DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, t1.ord_no, t1.company_code, t1.company_name Company, t1.findoc_finappby, m3.name ReceivedBy, t1.findoc_finappdt ReceivedDate, t1.invoice_no InvoiceNo, t1.po_no PONo, t1.est_delivery_dt ShipOnBoard, t1.material_name DescriptionOfGoods, t1.quantity Quantity, t1.unit_code Unit, t1.findoc_valamt CostperKgs, m4.kurs_pajak CostSlipRate, t1.currency_code Currency, t1.POCreatedBy, t1.POBy FROM " & _
                                                                " (SELECT t1.shipment_no, t1.ord_no, t5.company_code, m2.company_name, t2.invoice_no, t2.po_no, t6.est_delivery_dt, t6.currency_code, t3.quantity, t4.unit_code, m1.material_name, t1.findoc_valamt, t1.findoc_finappby, t1.findoc_finappdt, m3.name POCreatedBy, m3.user_ct POBy " & _
                                                                "  FROM tbl_shipping_doc t1, tbl_shipping_invoice t2, tbl_shipping_detail t3, tbl_po_detail t4, tbl_po t5, tbl_shipping t6, tbm_material m1, tbm_company m2, tbm_users m3 " & _
                                                                "  WHERE t1.findoc_type='CS' AND t1.findoc_status='Final Approved' AND t1.findoc_finappdt IS NOT NULL " & _
                                                                "  AND t1.shipment_no=t2.shipment_no AND t1.findoc_no=t2.po_no AND t1.findoc_reff=t2.ord_no " & _
                                                                "  AND t2.shipment_no=t3.shipment_no AND t2.po_no=t3.po_no AND t2.ord_no=t3.po_item " & _
                                                                "  AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item AND t4.po_no=t5.po_no AND t1.shipment_no=t6.shipment_no " & _
                                                                "  AND t3.material_code=m1.material_code AND t5.company_code=m2.company_code AND t5.createdby = m3.user_ct ORDER BY t2.po_no) t1 " & _
                                                                "LEFT JOIN tbm_users m3 ON t1.findoc_finappby=m3.user_ct " & _
                                                                "LEFT JOIN tbm_kurs m4 ON t1.currency_code=m4.currency_code AND t1.est_delivery_dt=m4.effective_date) as a")
            str1 = ""
        Else

            DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, t1.ord_no, t1.company_code, t1.company_name Company, t1.findoc_finappby, m3.name ReceivedBy, t1.findoc_finappdt ReceivedDate, t1.invoice_no InvoiceNo, t1.po_no PONo, t1.est_delivery_dt ShipOnBoard, t1.material_name DescriptionOfGoods, t1.quantity Quantity, t1.unit_code Unit, t1.findoc_valamt CostperKgs, m4.kurs_pajak CostSlipRate, t1.currency_code Currency, t1.POCreatedBy, t1.POBy FROM " & _
                                                               " (SELECT t1.shipment_no, t1.ord_no, t5.company_code, m2.company_name, t2.invoice_no, t2.po_no, t6.est_delivery_dt, t6.currency_code, t3.quantity, t4.unit_code, m1.material_name, t1.findoc_valamt, t1.findoc_finappby, t1.findoc_finappdt, m3.name POCreatedBy, m3.user_ct POBy " & _
                                                               "  FROM tbl_shipping_doc t1, tbl_shipping_invoice t2, tbl_shipping_detail t3, tbl_po_detail t4, tbl_po t5, tbl_shipping t6, tbm_material m1, tbm_company m2, tbm_users m3 " & _
                                                               "  WHERE t1.findoc_type='CS' AND t1.findoc_status='Final Approved' " & _
                                                               "  AND t1.shipment_no=t2.shipment_no AND t1.findoc_no=t2.po_no AND t1.findoc_reff=t2.ord_no " & _
                                                               "  AND t2.shipment_no=t3.shipment_no AND t2.po_no=t3.po_no AND t2.ord_no=t3.po_item " & _
                                                               "  AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item AND t4.po_no=t5.po_no AND t1.shipment_no=t6.shipment_no " & _
                                                               "  AND t3.material_code=m1.material_code AND t5.company_code=m2.company_code AND t5.createdby = m3.user_ct AND (m2.company_code='" & comp & "' OR ''='" & comp & "') ORDER BY t2.po_no) t1 " & _
                                                               "LEFT JOIN tbm_users m3 ON t1.findoc_finappby=m3.user_ct " & _
                                                               "LEFT JOIN tbm_kurs m4 ON t1.currency_code=m4.currency_code AND t1.est_delivery_dt=m4.effective_date " & _
                                                               "WHERE ((t1.findoc_finappby = '" & by & "') OR (t1.findoc_finappby IS NULL AND ''='" & by & "')) AND ((t1.findoc_finappdt='" & dt & "' AND " & tgl.Checked & ") OR (t1.findoc_finappdt IS NULL AND NOT " & tgl.Checked & ")) AND ((t1.POBy = '" & txtPOBy.Text & "') OR ('' = '" & txtPOBy.Text & "')) " & _
                                                               "ORDER BY t1.findoc_finappdt desc, m3.name) as a")


            str1 = "SELECT t1.shipment_no, t1.ord_no, t1.company_code, t1.company_name Company, t1.findoc_finappby, m3.name ReceivedBy, t1.findoc_finappdt ReceivedDate, t1.invoice_no InvoiceNo, t1.po_no PONo, t1.est_delivery_dt ShipOnBoard, t1.material_name DescriptionOfGoods, t1.quantity Quantity, t1.unit_code Unit, t1.findoc_valamt CostperKgs, m4.kurs_pajak CostSlipRate, t1.currency_code Currency, t1.POCreatedBy, t1.POBy FROM " & _
                                                               " (SELECT t1.shipment_no, t1.ord_no, t5.company_code, m2.company_name, t2.invoice_no, t2.po_no, t6.est_delivery_dt, t6.currency_code, t3.quantity, t4.unit_code, m1.material_name, t1.findoc_valamt, t1.findoc_finappby, t1.findoc_finappdt, m3.name POCreatedBy, m3.user_ct POBy " & _
                                                               "  FROM tbl_shipping_doc t1, tbl_shipping_invoice t2, tbl_shipping_detail t3, tbl_po_detail t4, tbl_po t5, tbl_shipping t6, tbm_material m1, tbm_company m2, tbm_users m3 " & _
                                                               "  WHERE t1.findoc_type='CS' AND t1.findoc_status='Final Approved' " & _
                                                               "  AND t1.shipment_no=t2.shipment_no AND t1.findoc_no=t2.po_no AND t1.findoc_reff=t2.ord_no " & _
                                                               "  AND t2.shipment_no=t3.shipment_no AND t2.po_no=t3.po_no AND t2.ord_no=t3.po_item " & _
                                                               "  AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item AND t4.po_no=t5.po_no AND t1.shipment_no=t6.shipment_no " & _
                                                               "  AND t3.material_code=m1.material_code AND t5.company_code=m2.company_code AND t5.createdby = m3.user_ct AND (m2.company_code='" & comp & "' OR ''='" & comp & "') ORDER BY t2.po_no) t1 " & _
                                                               "LEFT JOIN tbm_users m3 ON t1.findoc_finappby=m3.user_ct " & _
                                                               "LEFT JOIN tbm_kurs m4 ON t1.currency_code=m4.currency_code AND t1.est_delivery_dt=m4.effective_date " & _
                                                               "WHERE ((t1.findoc_finappby = '" & by & "') OR (''='" & by & "')) AND ((t1.findoc_finappdt='" & dt & "' AND " & tgl.Checked & ") OR (NOT " & tgl.Checked & ")) AND ((t1.POBy = '" & txtPOBy.Text & "') OR ('' = '" & txtPOBy.Text & "')) " & _
                                                               "ORDER BY t1.findoc_finappdt desc, m3.name"
        End If

        brs = DataGridView1.RowCount

        txtInvoice.Clear()
        txtPO.Clear()
        txtMaterial.Clear()
        txtQuantity.Text = qt2
        txtUnit.Clear()
        txtCost.Text = qt2
        txtCurrency.Clear()
        txtRate.Text = qt2

        btnSave.Enabled = False
        tgl.Enabled = True
        txtBy.Enabled = True
        btnSearch.Visible = True

        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).Visible = False
        DataGridView1.Columns(2).Visible = False
        DataGridView1.Columns(4).Visible = False
        DataGridView1.Columns(17).Visible = False 'kolom terakhir
        DataGridView1.Columns(11).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(13).DefaultCellStyle.Format = "N2"
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks, tptgl As String
        Dim Errmsg As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        If tgl.Checked And Trim(txtBy.Text) <> "" Then
            tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive

            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "Update tbl_shipping_doc " & _
                     "SET findoc_finappby='" & txtBy.Text & "'," & _
                     "findoc_finappdt='" & tptgl & "' " & _
                     "WHERE shipment_no='" & txtShipNo.Text & "' AND ord_no='" & txtOrdNo.Text & "' AND findoc_type='CS'"

            Try
                MyComm.CommandText = "RunSQL"
                MyComm.CommandType = CommandType.StoredProcedure

                MyComm.Parameters.Clear()
                MyComm.Parameters.AddWithValue("SQLStr", SQLstr)
                MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
                MyComm.Parameters.AddWithValue("Hasil", hasil)

                dr = MyComm.ExecuteReader()
                hasil = dr.FieldCount
                CloseMyReader(dr, UserData)
                RefreshScreen("", "", "")

                If hasil = True Then
                    f_msgbox_successful(teks)
                Else
                    MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                    Exit Sub
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        btnSearch.Visible = True
        brs = DataGridView1.CurrentCell.RowIndex

        tgl.Text = DataGridView1.Item(6, brs).Value.ToString
        txtBy.Text = DataGridView1.Item(4, brs).Value.ToString
        txtByName.Text = DataGridView1.Item(5, brs).Value.ToString

        txtPOBy.Text = DataGridView1.Item(17, brs).Value.ToString
        TxtPOByName.Text = DataGridView1.Item(16, brs).Value.ToString

        If txtByName.Text <> "" Then
            tgl.Checked = True
        Else
            tgl.Checked = False
        End If

        txtShipNo.Text = DataGridView1.Item(0, brs).Value.ToString
        txtOrdNo.Text = DataGridView1.Item(1, brs).Value.ToString
        txtCompanyCode.Text = DataGridView1.Item(2, brs).Value.ToString
        txtCompany.Text = DataGridView1.Item(3, brs).Value.ToString
        txtInvoice.Text = DataGridView1.Item(7, brs).Value.ToString
        txtPO.Text = DataGridView1.Item(8, brs).Value.ToString
        txtMaterial.Text = DataGridView1.Item(10, brs).Value.ToString
       
        txtQuantity.Text = FormatNumber(DataGridView1.Item(11, brs).Value.ToString, 2, , , TriState.True)
        txtUnit.Text = DataGridView1.Item(12, brs).Value.ToString
        txtCost.Text = FormatNumber(DataGridView1.Item(13, brs).Value.ToString, 2, , , TriState.True)
        txtCurrency.Text = DataGridView1.Item(15, brs).Value.ToString
        txtRate.Text = FormatNumber(DataGridView1.Item(14, brs).Value.ToString, 2, , , TriState.True)

        tgl.Focus()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then txtNewBy.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString

        If (txtNewBy.Text <> "" And txtNewBy.Text <> txtBy.Text) Then btnSave.Enabled = (Len(Trim(txtNewBy.Text)) > 0)
        If (txtNewBy.Text <> "") Then txtBy.Text = txtNewBy.Text
    End Sub

    Private Function GetNum(ByVal strnum As String) As Decimal
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum = CDec(temp)
    End Function
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientDecimalSeparator, ServerDecimal)
    End Function

    Private Sub txtBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBy.TextChanged
        txtByName.Text = GetData("SELECT name FROM tbm_users WHERE user_ct='" & txtBy.Text & "'")
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

    Private Sub tgl_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tgl.ValueChanged
        btnSave.Enabled = (Len(Trim(txtBy.Text)) > 0)
    End Sub

    Private Sub txtCompanyCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompanyCode.TextChanged
        txtCompany.Text = GetData("SELECT company_name FROM tbm_company WHERE company_code='" & txtCompanyCode.Text & "'")
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Dim tptgl As String
        btnPrint.Enabled = True

        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        RefreshScreen(tptgl, txtBy.Text, txtCompanyCode.Text)
    End Sub

    Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAll.Click
        btnPrint.Enabled = False
        RefreshScreen("", "", "")
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim tptgl As String

        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive

        If tptgl <> "" And txtBy.Text <> "" And txtCompanyCode.Text <> "" Then
            ViewerFrm.Tag = "TTDCSLIP;" & tptgl & "." & txtBy.Text & "." & txtCompanyCode.Text & "." & txtPOBy.Text
            ViewerFrm.ShowDialog()
        Else
            MsgBox("You're must enter filtering data", MsgBoxStyle.Exclamation, "Error")
        End If
    End Sub

    Private Sub btnCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompany.Click
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
            txtCompanyCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txtCompany.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnPOBy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPOBy.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu "
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu " & _
                               "where tu.user_id LIKE 'FilterData1%' and tu.Name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPOBy.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            TxtPOByName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
        End If

    End Sub

    Private Sub txtPOBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPOBy.TextChanged
        TxtPOByName.Text = GetData("SELECT name FROM tbm_users WHERE user_ct='" & txtPOBy.Text & "'")
    End Sub
End Class
