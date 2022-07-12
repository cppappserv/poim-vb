'Title                         : Tanda Terima PV

Imports CrystalDecisions.CrystalReports.Engine
Public Class TandaTerimaPV
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
    Private Sub TandaTerimaPV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
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

        qt0 = FormatNumber(0, 0, , , TriState.True)
        qt2 = FormatNumber(0, 2, , , TriState.True)

        If dt = "" Then
            tgl.Value = Now()
            txtBy.Clear()
            txtByName.Clear()
            txtCompany.Clear()
            TxtPOByName.Clear()
            txtPO.Clear()
            txtMaterial.Clear()
            tgl.Checked = False

            DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, t1.company_code, t1.Company, t1.findoc_finappby, t1.ReceivedBy, t1.ReceivedDate, t1.BL, FORMAT(t1.InvoiceAmount,2) InvoiceAmount, FORMAT(t1.BLPenalty,2) BLPenalty, FORMAT(t1.InvoiceAmount - t1.BLPenalty,2) PVAmount, t1.DetailofPO, t1.MaterialGroup, t1.PrintedDate, t1.PVCreatedBy, t1.findoc_createdby, t1.ord_no, t1.MaterialGroupCode FROM " & _
                                                                " (SELECT t1.shipment_no, m2.company_code, m3.company_name Company, t1.findoc_finappby, m4.name ReceivedBy, t1.findoc_finappdt ReceivedDate, t2.bl_no BL, " & _
                                                                "  (SELECT SUM(invoice_amount-invoice_penalty) FROM tbl_shipping_invoice st2 WHERE t1.shipment_no=st2.shipment_no GROUP BY st2.shipment_no) InvoiceAmount, " & _
                                                                "  t2.Finalty BLPenalty, " & _
                                                                "  (SELECT GROUP_CONCAT(DISTINCT getpoorder(t1.shipment_no,TRIM(po_no))) FROM tbl_shipping_detail st1 WHERE t1.shipment_no=st1.shipment_no GROUP BY st1.shipment_no) DetailofPO, " & _
                                                                "  (SELECT GROUP_CONCAT(DISTINCT TRIM(sm5.group_name)) FROM tbl_shipping_detail st4, tbm_material sm4, tbm_material_group sm5 WHERE t1.shipment_no=st4.shipment_no AND st4.material_code=sm4.material_code AND sm4.group_code=sm5.group_code GROUP BY st4.shipment_no) MaterialGroup, " & _
                                                                "  (SELECT GROUP_CONCAT(DISTINCT TRIM(sm5.group_code)) FROM tbl_shipping_detail st4, tbm_material sm4, tbm_material_group sm5 WHERE t1.shipment_no=st4.shipment_no AND st4.material_code=sm4.material_code AND sm4.group_code=sm5.group_code GROUP BY st4.shipment_no) MaterialGroupCode, " & _
                                                                "  t1.findoc_printeddt PrintedDate, m1.name PVCreatedBy, t1.findoc_createdby, t1.ord_no " & _
                                                                "  FROM tbl_shipping_doc t1, tbl_shipping t2, tbm_users m1, tbm_plant m2, tbm_company m3, tbm_users m4 " & _
                                                                "  WHERE t1.findoc_type='PV' AND t1.findoc_status <> 'Rejected' " & _
                                                                "  AND t1.shipment_no=t2.shipment_no AND t2.plant_code=m2.plant_code AND m2.company_code=m3.company_code " & _
                                                                "  AND t1.findoc_createdby=m1.user_ct AND t1.findoc_finappby=m4.user_ct " & _
                                                                "  ORDER BY t1.findoc_finappdt DESC, m4.name, t1.findoc_printeddt DESC, m1.name " & _
                                                                " ) t1) as a")
            '"  (SELECT GROUP_CONCAT(DISTINCT TRIM(po_no)) FROM tbl_shipping_detail st1 WHERE t1.shipment_no=st1.shipment_no GROUP BY st1.shipment_no) DetailofPO, " & _
        Else
            DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, t1.company_code, t1.Company, t1.findoc_finappby, m4.name ReceivedBy, t1.ReceivedDate, t1.BL, FORMAT(t1.InvoiceAmount,2) InvoiceAmount, FORMAT(t1.BLPenalty,2) BLPenalty, FORMAT(t1.InvoiceAmount - t1.BLPenalty,2) PVAmount, t1.DetailofPO, t1.MaterialGroup, t1.PrintedDate, t1.PVCreatedBy, t1.findoc_createdby, t1.ord_no, t1.MaterialGroupCode FROM " & _
                                                                " (SELECT t1.shipment_no, m2.company_code, m3.company_name Company, t1.findoc_finappby, t1.findoc_finappdt ReceivedDate, t2.bl_no BL, " & _
                                                                "  (SELECT SUM(invoice_amount-invoice_penalty) FROM tbl_shipping_invoice st2 WHERE t1.shipment_no=st2.shipment_no GROUP BY st2.shipment_no) InvoiceAmount, " & _
                                                                "  t2.Finalty BLPenalty, " & _
                                                                "  (SELECT GROUP_CONCAT(DISTINCT getpoorder(t1.shipment_no, TRIM(po_no))) FROM tbl_shipping_detail st1 WHERE t1.shipment_no=st1.shipment_no GROUP BY st1.shipment_no) DetailofPO, " & _
                                                                "  (SELECT GROUP_CONCAT(DISTINCT TRIM(sm5.group_name)) FROM tbl_shipping_detail st4, tbm_material sm4, tbm_material_group sm5 WHERE t1.shipment_no=st4.shipment_no AND st4.material_code=sm4.material_code AND sm4.group_code=sm5.group_code GROUP BY st4.shipment_no) MaterialGroup, " & _
                                                                "  (SELECT GROUP_CONCAT(DISTINCT TRIM(sm5.group_code)) FROM tbl_shipping_detail st4, tbm_material sm4, tbm_material_group sm5 WHERE t1.shipment_no=st4.shipment_no AND st4.material_code=sm4.material_code AND sm4.group_code=sm5.group_code GROUP BY st4.shipment_no) MaterialGroupCode, " & _
                                                                "  t1.findoc_printeddt PrintedDate, m1.name PVCreatedBy, t1.findoc_createdby, t1.ord_no " & _
                                                                "  FROM tbl_shipping_doc t1, tbl_shipping t2, tbm_users m1, tbm_plant m2, tbm_company m3 " & _
                                                                "  WHERE t1.findoc_type='PV' AND t1.findoc_status <> 'Rejected' " & _
                                                                "  AND t1.shipment_no=t2.shipment_no AND t2.plant_code=m2.plant_code AND m2.company_code=m3.company_code AND t1.findoc_createdby=m1.user_ct " & _
                                                                " ) t1 " & _
                                                                "LEFT JOIN tbm_users m4 ON t1.findoc_finappby=m4.user_ct " & _
                                                                "WHERE ((t1.findoc_finappby = '" & by & "') OR (''='" & by & "')) AND ((t1.ReceivedDate='" & dt & "' AND " & tgl.Checked & ") OR (NOT " & tgl.Checked & ")) AND ((t1.company_code = '" & txtCompanyCode.Text & "') OR ('' = '" & txtCompanyCode.Text & "')) AND ((t1.MaterialGroupCode = '" & txtMaterialCode.Text & "') OR (''= '" & txtMaterialCode.Text & "')) AND ((t1.findoc_createdby = '" & txtPOBy.Text & "') OR ('' = '" & txtPOBy.Text & "')) " & _
                                                                "ORDER BY t1.ReceivedDate DESC, m4.name, t1.PrintedDate DESC, t1.PVCreatedBy) as a")
            '"  (SELECT GROUP_CONCAT(DISTINCT TRIM(po_no)) FROM tbl_shipping_detail st1 WHERE t1.shipment_no=st1.shipment_no GROUP BY st1.shipment_no) DetailofPO, " & _
        End If

        brs = DataGridView1.RowCount

        txtBL.Clear()
        txtAmount.Text = qt2

        btnSave.Enabled = False
        tgl.Enabled = True
        txtBy.Enabled = True
        btnSearch.Visible = True

        If brs > 0 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(14).Visible = False
            DataGridView1.Columns(15).Visible = False
            DataGridView1.Columns(16).Visible = False 'kolom terakhir
            DataGridView1.Columns(7).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(8).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(9).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(10).Width = 250
            DataGridView1.Columns(11).Width = 250
        End If
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
            SQLstr = "UPDATE tbl_shipping_doc " & _
                     "SET findoc_finappby='" & txtBy.Text & "'," & _
                     "findoc_finappdt='" & tptgl & "' " & _
                     "where shipment_no='" & txtShipNo.Text & "' AND ord_no='" & txtOrdNo.Text & "' AND findoc_type='PV' "
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

        tgl.Text = DataGridView1.Item(5, brs).Value.ToString
        txtBy.Text = DataGridView1.Item(3, brs).Value.ToString
        txtByName.Text = DataGridView1.Item(4, brs).Value.ToString
        txtPOBy.Text = DataGridView1.Item(14, brs).Value.ToString
        TxtPOByName.Text = DataGridView1.Item(13, brs).Value.ToString

        If txtByName.Text <> "" Then
            tgl.Checked = True
        Else
            tgl.Checked = False
        End If

        txtShipNo.Text = DataGridView1.Item(0, brs).Value.ToString
        txtOrdNo.Text = DataGridView1.Item(15, brs).Value.ToString
        txtBL.Text = DataGridView1.Item(6, brs).Value.ToString
        txtCompanyCode.Text = DataGridView1.Item(1, brs).Value.ToString
        txtCompany.Text = DataGridView1.Item(2, brs).Value.ToString
        txtPO.Text = DataGridView1.Item(10, brs).Value.ToString
        txtMaterial.Text = DataGridView1.Item(11, brs).Value.ToString
        txtMaterialCode.Text = DataGridView1.Item(16, brs).Value.ToString
        'txtAmount.Text = FormatNumber(DataGridView1.Item(9, brs).Value.ToString, 2, , , TriState.True)
        txtAmount.Text = DataGridView1.Item(9, brs).Value.ToString()
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
            ViewerFrm.Tag = "TTDPV;" & tptgl & "." & txtBy.Text & "." & txtCompanyCode.Text & "." & txtPOBy.Text & "." & txtMaterialCode.Text & "." & txtMaterial.Text
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

    Private Sub btnMatGrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMatGrp.Click
        PilihanDlg.Text = "Select Material Group Code"
        PilihanDlg.LblKey1.Text = "Group Code"
        PilihanDlg.LblKey2.Text = "Group Name"
        PilihanDlg.SQLGrid = "SELECT group_code GroupCode, group_name GroupName FROM tbm_material_group"
        PilihanDlg.SQLFilter = "SELECT group_code GroupCode, group_name GroupName FROM tbm_material_group " & _
                               "Where group_code LIKE 'FilterData1%' AND group_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtMaterialCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txtMaterial.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
End Class
