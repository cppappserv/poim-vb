'Title                         : Tanda Terima Pajak

Imports CrystalDecisions.CrystalReports.Engine
Public Class TandaTerimaPajak
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
    Private Sub TandaTerimaPajak_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
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
            tgl.Checked = False

            DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, m1.company_code, m4.company_name Company, t1.Verified2By, m3.name ReceivedBy, t1.Verified2Dt ReceivedDate, t1.bl_no BL, t1.aju_no AJUNo, t1.pib_no PIBNo, " & _
                                                                "        t1.bea_masuk BeaMasuk, t1.vat VAT, t1.pph21 PPh22, t1.piud PIUD, t1.kurs_pajak TAXRate, " & _
                                                                "        (SELECT GROUP_CONCAT(DISTINCT TRIM(po_no)) FROM tbl_shipping_detail t2 WHERE t1.shipment_no=t2.shipment_no GROUP BY t2.shipment_no) DetailofPO, m5.name BLCreatedBy, t1.createdby BLBy " & _
                                                                "FROM tbl_shipping t1, tbm_plant m1, tbm_port m2, tbm_users m3, tbm_company m4, tbm_users m5 " & _
                                                                "WHERE t1.plant_code=m1.plant_code AND t1.port_code=m2.port_code AND t1.Verified2By=m3.user_ct AND m1.company_code=m4.company_code AND t1.createdby = m5.user_ct order by t1.Verified2Dt desc, m3.name, DetailofPO) as a")

        Else

            DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, t1.company_code, t1.company_name Company, t1.Verified2By, m3.name ReceivedBy, t1.Verified2Dt ReceivedDate, t1.bl_no BL, t1.aju_no AJUNo, t1.pib_no PIBNo, " & _
                                                                " t1.bea_masuk BeaMasuk, t1.vat VAT, t1.pph21 PPh22, t1.piud PIUD, t1.kurs_pajak TAXRate, t1.DetailofPO, t1.name BLCreatedBy, t1.createdby BLBy  FROM " & _
                                                                " (SELECT t1.shipment_no, m1.company_code, m4.company_name, t1.Verified2By, t1.Verified2Dt, t1.bl_no, t1.aju_no, t1.pib_no, " & _
                                                                "        t1.bea_masuk, t1.vat, t1.pph21, t1.piud, t1.kurs_pajak, " & _
                                                                "        (SELECT GROUP_CONCAT(DISTINCT TRIM(po_no)) FROM tbl_shipping_detail t2 WHERE t1.shipment_no=t2.shipment_no GROUP BY t2.shipment_no) DetailofPO, m5.name, t1.createdby " & _
                                                                " FROM tbl_shipping t1, tbm_plant m1, tbm_port m2, tbm_company m4, tbm_users m5 " & _
                                                                " WHERE t1.plant_code=m1.plant_code AND t1.port_code=m2.port_code AND m1.company_code=m4.company_code AND t1.createdby = m5.user_ct " & _
                                                                " AND (m1.company_code='" & comp & "' OR ''='" & comp & "')) t1 " & _
                                                                "LEFT JOIN tbm_users m3 ON t1.Verified2By=m3.user_ct " & _
                                                                "WHERE ((t1.Verified2By = '" & by & "') OR (''='" & by & "')) AND ((t1.Verified2Dt='" & dt & "' AND " & tgl.Checked & ") OR (NOT " & tgl.Checked & ")) AND ((t1.createdby = '" & txtPOBy.Text & "') OR ('' = '" & txtPOBy.Text & "')) " & _
                                                                "ORDER BY t1.Verified2Dt desc, m3.name, DetailofPO) as a")


        End If

        brs = DataGridView1.RowCount

        txtBL.Clear()
        txtAJU.Clear()
        txtPIB.Clear()
        txtRate.Text = qt2
        txtBM.Text = qt2
        txtVAT.Text = qt2
        txtPPH.Text = qt2
        txtPIUD.Text = qt2

        btnSave.Enabled = False
        tgl.Enabled = True
        txtBy.Enabled = True
        btnSearch.Visible = True

        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).Visible = False
        DataGridView1.Columns(3).Visible = False
        DataGridView1.Columns(16).Visible = False 'kolom terakhir
        DataGridView1.Columns(9).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(10).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(11).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(12).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(13).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(14).Width = 250
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
            SQLstr = "UPDATE tbl_shipping " & _
                     "SET Verified2By='" & txtBy.Text & "'," & _
                     "Verified2Dt='" & tptgl & "' " & _
                     "where shipment_no='" & txtShipNo.Text & "'"
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
        txtPOBy.Text = DataGridView1.Item(16, brs).Value.ToString
        TxtPOByName.Text = DataGridView1.Item(15, brs).Value.ToString

        If txtByName.Text <> "" Then
            tgl.Checked = True
        Else
            tgl.Checked = False
        End If

        txtShipNo.Text = DataGridView1.Item(0, brs).Value.ToString
        txtBL.Text = DataGridView1.Item(6, brs).Value.ToString
        txtAJU.Text = DataGridView1.Item(7, brs).Value.ToString
        txtPIB.Text = DataGridView1.Item(8, brs).Value.ToString
        txtCompanyCode.Text = DataGridView1.Item(1, brs).Value.ToString
        txtCompany.Text = DataGridView1.Item(2, brs).Value.ToString

        txtRate.Text = FormatNumber(DataGridView1.Item(13, brs).Value.ToString, 2, , , TriState.True)
        txtBM.Text = FormatNumber(DataGridView1.Item(9, brs).Value.ToString, 2, , , TriState.True)
        txtVAT.Text = FormatNumber(DataGridView1.Item(10, brs).Value.ToString, 2, , , TriState.True)
        txtPPH.Text = FormatNumber(DataGridView1.Item(11, brs).Value.ToString, 2, , , TriState.True)
        txtPIUD.Text = FormatNumber(DataGridView1.Item(12, brs).Value.ToString, 2, , , TriState.True)

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
            ViewerFrm.Tag = "TTDPAJAK;" & tptgl & "." & txtBy.Text & "." & txtCompanyCode.Text & "." & txtPOBy.Text
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
End Class
