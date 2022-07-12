'Title        : List Shipment
Imports POIM.frmBAPB

Public Class FrmListShipment
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"
        PilihanDlg.SQLGrid = "select tc.company_code CompanyCode, tc.company_name CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                             "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'"
        PilihanDlg.SQLFilter = "select tc.company_code CompanyCode, tc.company_name CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                               "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'" & _
                               "and company_code LIKE 'FilterData1%' AND " & _
                               "company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company as tc inner join tbm_users_company as tuc on tc.COMPANY_CODE = tuc.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCompany_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If f_cekotorisasi_comp() = True Then
            f_getpoheader()
        Else
            MsgBox("You are no authorized using this company code", MsgBoxStyle.Critical, "No Authorization")
        End If
    End Sub

    Private Function f_cekotorisasi_comp() As Boolean
        Dim v_oke As String
        If txtCompany_Code.Text <> "" Then
            v_oke = AmbilData("company_code", "tbm_users_company", "USER_CT='" & UserData.UserCT & "' and company_code = '" & txtCompany_Code.Text & "'")
            If v_oke = "" Then
                f_cekotorisasi_comp = False
            Else
                f_cekotorisasi_comp = True
            End If
        Else
            f_cekotorisasi_comp = True
        End If
    End Function

    Private Sub f_getpoheader()
        Dim SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7 As String
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs
        Dim brs As Integer

        SQL1 = ""
        SQL2 = ""
        SQL3 = ""
        SQL4 = ""
        SQL5 = ""
        SQL6 = ""

        SQLstr = "SELECT DISTINCT concat(trim(tp.po_no), ' (', getorder(tp.shipment_no, tp.po_no),')') AS PONo, tp.shipment_no, tp.po_no, getorder(tp.shipment_no, tp.po_no) ship_no, ts.bl_no BLNo, m0.company_name CompanyName, m1.plant_name PlantName, " & _
                 "m2.port_name PortName, m3.line_name ShippingLineName, ts.Vessel, ts.notice_arrival_dt ArrivalDate, ts.clr_dt DeliveryDate, bapb_dt ClearanceDate, " & _
                 "(SELECT  'Detailed' FROM tbl_bapb WHERE tbl_bapb.shipment_no=tp.shipment_no AND tbl_bapb.po_no=tp.po_no LIMIT 1) DataOfBAPB " & _
                 "FROM tbl_shipping_detail tp, tbl_shipping ts, tbm_company m0, tbm_plant m1, tbm_port m2, tbm_lines m3 " & _
                 "WHERE tp.shipment_no = ts.shipment_no And ts.plant_code = m1.plant_code And m1.company_code = m0.company_code And ts.port_code = m2.port_code And ts.shipping_line = m3.line_code "

        If DTCreated1.Text <> "" And DTCreated2.Text <> "" Then
            SQL1 = "and ((ts.notice_arrival_dt >= '" & Format(DTCreated1.Value, "yyyy-MM-dd") & "' and ts.notice_arrival_dt <= '" & Format(DTCreated2.Value, "yyyy-MM-dd") & "' AND '" & txtPONO.Text & "' = '') OR ('" & txtPONO.Text & "' <> '')) "
        End If

        If txtCompany_Code.Text <> "" Then
            SQL3 = "and m1.company_code ='" & txtCompany_Code.Text & "' "
        Else
            SQL3 = "and m1.company_code in (select company_code from tbm_users_company where user_ct = '" & UserData.UserCT & "') "
        End If

        If txtCompany_Code.Text <> "" Then
            SQL3 = "and m1.plant_code ='" & txtPlant_Code.Text & "' "
        Else
            SQL3 = "and m1.company_code in (select company_code from tbm_users_company where user_ct = '" & UserData.UserCT & "') "
        End If

        If txtSuppCode.Text <> "" Then
            SQL4 = "and ts.SUPPLIER_CODE = '" & txtSuppCode.Text & "' "
        End If

        If txtPONO.Text <> "" Then
            SQL6 = "and tp.po_no LIKE '%" & txtPONO.Text & "%' "
        End If

        SQL7 = "order by ts.notice_arrival_dt desc"

        SQLstr = SQLstr & SQL1 & SQL2 & SQL3 & SQL4 & SQL5 & SQL6 & SQL7
        ErrMsg = "Failed when read PO"
       
        Dim dts As DataTable
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        DGVHeader.DataSource = dts

        brs = DGVHeader.RowCount

        If brs > 0 Then
            DGVHeader.Columns(0).Width = 150
            DGVHeader.Columns(1).Visible = False
            DGVHeader.Columns(2).Visible = False
            DGVHeader.Columns(3).Visible = False

            For i = 0 To brs - 1
                If DGVHeader.Item(13, i).Value.ToString = "Detailed" Then DGVHeader.Rows(i).DefaultCellStyle.BackColor = Color.Thistle
            Next

            DGVHeader.CurrentCell = DGVHeader(0, 0)
            DGVHeader_CellClick(DGVHeader, ee)
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

    Private Sub FrmListPO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Left = 50
        Me.Top = 50
        DTCreated1.Text = GetServerDate()
        DTCreated1.Value = DateAdd(DateInterval.Month, -3, Now)

        lblCompany_Name.Text = ""
        lblPlant_Name.Text = ""
        lblPort_Name.Text = ""
        lblSuppName.Text = ""

        DGVHeader.DataSource = Nothing
        DGVDetail.DataSource = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub DGVHeader_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVHeader.CellClick
        Dim brs As Integer

        v_pono.Text = ""
        brs = 0
        brs = DGVHeader.CurrentCell.RowIndex
        v_shipmentno.Text = DGVHeader.Item(1, brs).Value.ToString
        v_pono.Text = DGVHeader.Item(2, brs).Value.ToString
        v_shipno.Text = DGVHeader.Item(3, brs).Value.ToString
        refresh_DGVdetail(v_pono.Text, v_shipmentno.Text)
    End Sub


    Private Sub refresh_DGVdetail(ByVal v_pono As String, ByVal v_shipmentno As String)
        Dim dts As DataTable

        SQLstr = "SELECT tp.po_item NoItem, m1.material_name DescriptionOfGoods, tp.quantity Quantity, td.unit_code Unit, tp.pack_code PackName, tp.pack_quantity PackSize " & _
                 "FROM tbl_shipping_detail tp, tbm_material m1, tbl_po_detail td " & _
                 "WHERE tp.material_code = m1.material_code And (tp.po_no = td.po_no And tp.po_item = td.po_item) and tp.po_no = '" & v_pono & "' and tp.shipment_no= '" & v_shipmentno & "'"

        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        DGVDetail.DataSource = dts

        If DGVDetail.RowCount > 0 Then
            DGVDetail.Columns(1).Width = 250
        End If
    End Sub


    Private Sub btnViewPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewPO.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnSearchPlant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPlant.Click
        PilihanDlg.Text = "Select Plant Code"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "select tc.plant_code PlantCode, tc.Plant_name PlantName from tbm_plant as tc inner join tbm_users_company as tuc " & _
                             "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'"
        PilihanDlg.SQLFilter = "select tc.plant_code PlantCode, tc.plant_name PlantName from tbm_plant as tc inner join tbm_users_company as tuc " & _
                               "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'" & _
                               "and plant_code LIKE 'FilterData1%' AND " & _
                               "plant_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_plant as tc inner join tbm_users_company as tuc on tc.COMPANY_CODE = tuc.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPlant_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPlant_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnSearchPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPort.Click
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"

        PilihanDlg.SQLGrid = "SELECT a.port_code AS PortCode, a.Port_Name AS PortName " & _
                             "FROM tbm_port a, tbm_city b WHERE a.city_code = b.city_code AND b.country_code = " & _
                             "(SELECT DISTINCT b.country_code FROM tbm_plant a, tbm_city b " & _
                             " WHERE a.city_code = b.city_code AND a.plant_code='" & txtPlant_Code.Text & "')"

        PilihanDlg.SQLFilter = "SELECT a.port_code AS PortCode, a.Port_Name AS PortName " & _
                             "FROM tbm_port a, tbm_city b WHERE a.city_code = b.city_code AND b.country_code = " & _
                             "(SELECT DISTINCT b.country_code FROM tbm_plant a, tbm_city b " & _
                             " WHERE a.city_code = b.city_code AND a.plant_code='" & txtPlant_Code.Text & "') " & _
                             "and b.port_code LIKE 'FilterData1%' AND b.port_name LIKE 'FilterData2%' "

        PilihanDlg.Tables = "tbm_port b, tbm_plant a WHERE a.city_code=b.city_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPort_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPort_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub DGVHeader_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVHeader.CellContentClick

    End Sub
End Class