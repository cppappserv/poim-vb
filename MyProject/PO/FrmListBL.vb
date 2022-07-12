'Title        : List BL ori
'Form         : FrmListBL
'Created By   : Hanny
'Created Date : 18 Maret 2009
'Table Used   : 

Imports POIM.FrmBL

Public Class FrmListBL
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'DTCreated1.Text = Now.ToString - 30
    End Sub

    Private Sub btnUserPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserPur.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        PilihanDlg.LblKey2.Visible = False
        PilihanDlg.TxtKey2.Visible = False
        PilihanDlg.SQLGrid = "Select distinct tu.user_ct as UserCT,tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code like 'SD%' "
        PilihanDlg.SQLFilter = "Select distinct tu.user_ct as UserCT,tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code like 'SD%' " & _
                               "and tu.name LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCreatedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            userct.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub


    Private Sub btnSearchPlant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPlant.Click
        PilihanDlg.Text = "Select Plant Code"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "SELECT PLANT_CODE as PlantCode, PLANT_NAME as PlantName FROM tbm_plant"
        PilihanDlg.SQLFilter = "SELECT PLANT_CODE as PlantCode, PLANT_NAME as PlantName FROM tbm_plant " & _
                               "WHERE PLANT_CODE LIKE 'FilterData1%' AND " & _
                               "PLANT_NAME LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_plant"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPlant_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPlant_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If


    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        btnViewBL.Enabled = False
        DGVDetail.DataSource = Nothing
        If f_cekotorisasi_comp() = True Then
            f_getpoheader()
            If (DGVHeader.RowCount > 0) Then
                DGVHeader.CurrentCell = DGVHeader(1, 0)
                Dim ee As System.Windows.Forms.DataGridViewCellEventArgs
                DGVHeader_CellClick(sender, ee)
                btnViewBL.Enabled = True
            End If
        Else
            MsgBox("You are no authorized using this company code", MsgBoxStyle.Critical, "No Authorization")
        End If
    End Sub
    Private Function f_cekotorisasi_comp() As Boolean
        Dim v_oke, v_comp As String
        If txtPlant_Code.Text <> "" Then
            v_comp = AmbilData("company_code", "tbm_plant", "plant_code = '" & txtPlant_Code.Text & "'")
            v_oke = AmbilData("company_code", "tbm_users_company", "USER_CT='" & userct.Text & "' and company_code = '" & v_comp & "'")
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
        Dim SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8 As String

        SQL1 = ""
        SQL2 = ""
        SQL3 = ""
        SQL4 = ""
        SQL5 = ""
        SQL6 = ""
        SQL7 = ""
        SQL8 = ""

        SQLstr = "select ts.shipment_no as 'ShipNo', ts.bl_no as 'BLNo', ts.hostbl as 'HostBL', ts.supplier_code as 'Supplier', tms.supplier_name as 'SupplierName', " & _
"ts.packinglist_no as 'PackingList', DATE_FORMAT(ts.received_copydoc_dt,'%d-%m-%Y') as 'CopyDocDate', " & _
"DATE_FORMAT(ts.received_doc_dt,'%d-%m-%Y') as 'ReceiveDocDt', " & _
"ts.CURRENCY_CODE as 'Currency', " & _
"DATE_FORMAT(ts.EST_DELIVERY_DT,'%d-%m-%Y') as 'EstDeliveryDt', " & _
"DATE_FORMAT(ts.EST_ARRIVAL_DT,'%d-%m-%Y') as 'EstArrivalDt', " & _
"ts.PLANT_CODE as 'Plant', tmp.plant_name as 'PlantName', ts.PORT_CODE as Port, tmpo.port_name as 'PortName', " & _
"ts.SHIPPING_LINE as 'ShippingLine', tml.line_name as 'ShippingLineName', ts.VESSEL as 'Wessel', t2.container_size as 'ContainerSize', " & _
"ts.BEA_MASUK as 'BeaMasuk',ts.VAT,ts.PPH21,ts.PIUD,tmu1.name as 'ReceivedbyFinance', " & _
"ts.bank_name as 'BankName',ts.account_no as 'AccountNo', " & _
"DATE_FORMAT(ts.PIB_DT,'%d-%m-%Y') as 'PIBDate',ts.KURS_PIB as 'KursPIB',ts.KURS_PAJAK as 'KursTax', " & _
"ts.STATUS as Status, tmu2.name as 'CreatedBy',DATE_FORMAT(ts.CREATEDDT,'%d-%m-%Y') as 'CreatedDate' " & _
"from tbl_shipping as ts  " & _
"left join tbl_shipping_detail AS TSD ON TS.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
"Left join tbm_supplier as tms on ts.supplier_code = tms.supplier_code " & _
"Left join tbm_plant as tmp on ts.plant_code = tmp.plant_code " & _
"Left join tbm_port as tmpo on ts.port_code = tmpo.port_code " & _
"Left join tbm_users as tmu1 on ts.received_by = tmu1.user_ct " & _
"Left join tbm_users as tmu2 on ts.CREATEDBY = tmu2.user_ct " & _
"left join tbm_lines as tml on ts.shipping_line = tml.line_code " & _
"Left Join " & _
"(Select shipment_no, cast((GROUP_CONCAT(CONCAT(unit_tot,'x',unit_code) separator ', ')) as char) container_size " & _
"From " & _
"(Select sum(1) unit_tot, unit_code, shipment_no " & _
"From tbl_shipping_cont " & _
"Group by shipment_no, unit_code) t1 " & _
"Group by shipment_no) t2 " & _
"On ts.shipment_no = t2.shipment_no "

        If txtBL.Text = "" And txtPONO.Text = "" And txtPL.Text = "" Then
            If DTCreated1.Text <> "" And DTCreated2.Text <> "" Then
                SQL1 = "where cast(ts.createddt as char(10)) >= '" & Format(DTCreated1.Value, "yyyy-MM-dd") & "' and cast(ts.createddt as char(10)) <= '" & Format(DTCreated2.Value, "yyyy-MM-dd") & "' "
            ElseIf DTCreated1.Text <> "" Then
                SQL1 = "where cast(ts.createddt as char(10)) >= '" & Format(DTCreated1.Value, "yyyy-MM-dd") & "' "
            Else
                SQL1 = "where cast(ts.createddt as char(10)) <= '" & Format(DTCreated2.Value, "yyyy-MM-dd") & "' "
            End If
        Else
            SQL1 = "where 1=1 "
        End If

        If userct.Text <> "" And (txtPL.Text = "" And txtBL.Text = "" And txtPONO.Text = "") Then
            SQL2 = "and ts.CREATEDBY = '" & userct.Text & "' "
        End If

        If txtPlant_Code.Text <> "" And (txtPL.Text = "" And txtBL.Text = "" And txtPONO.Text = "") Then
            SQL3 = "and ts.plant_code ='" & txtPlant_Code.Text & "' "
        End If

        If txtSuppCode.Text <> "" And (txtPL.Text = "" And txtBL.Text = "" And txtPONO.Text = "") Then
            SQL4 = "and ts.SUPPLIER_CODE = '" & txtSuppCode.Text & "' "
        End If

        If txtPL.Text <> "" Then
            SQL5 = "and ts.packinglist_no LIKE '%" & txtPL.Text & "%' "
        End If

        If txtBL.Text <> "" Then
            SQL6 = "and ts.BL_NO LIKE '%" & txtBL.Text & "%' "
        End If

        If txtPONO.Text <> "" Then
            SQL7 = "and TSD.po_no LIKE '%" & txtPONO.Text & "%' "
        End If

        SQL8 = "order by ts.createddt desc"

        SQLstr = SQLstr & SQL1 & SQL2 & SQL3 & SQL4 & SQL5 & SQL6 & SQL7 & " group by ts.createddt desc,ts.shipment_no"
        ErrMsg = "Failed when read B/L"

        Dim dts As DataTable
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DGVHeader.DataSource = dts
        DGVHeader.Columns(0).Visible = False

        DGVHeader.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(17).DefaultCellStyle.Format = "N0"
        DGVHeader.Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(18).DefaultCellStyle.Format = "N0"
        DGVHeader.Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(19).DefaultCellStyle.Format = "N0"
        DGVHeader.Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(20).DefaultCellStyle.Format = "N0"
        DGVHeader.Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(21).DefaultCellStyle.Format = "N2"
        DGVHeader.Columns(26).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(26).DefaultCellStyle.Format = "N2"
        DGVHeader.Columns(27).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(27).DefaultCellStyle.Format = "N2"


    End Sub

    Private Sub btnSup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSup.Click
        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier"
        PilihanDlg.SQLFilter = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier " & _
                               "WHERE supplier_code LIKE 'FilterData1%' AND supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSuppCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblSuppName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub FrmListBL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Left = 50
        Me.Top = 50
        lblPlant_Name.Text = ""
        lblSuppName.Text = ""
        DTCreated1.Text = GetServerDate()
        DTCreated1.Value = DateAdd(DateInterval.Month, -3, Now)
        DGVHeader.DataSource = Nothing
        DGVDetail.DataSource = Nothing
        ComboBox1.Text = ""
        txtPlant_Code.Text = ""
        txtSuppCode.Text = ""
        txtPL.Text = ""
        txtBL.Text = ""
        txtPONO.Text = ""

        txtCreatedby.Text = UserData.UserName
        userct.Text = UserData.UserCT.ToString
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
        'Dispose()
    End Sub

    Private Sub DGVHeader_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVHeader.CellClick
        Dim brs As Integer

        v_shipno.Text = ""
        v_blno.Text = ""
        brs = 0
        brs = DGVHeader.CurrentCell.RowIndex
        v_shipno.Text = DGVHeader.Item(0, brs).Value.ToString
        v_blno.Text = DGVHeader.Item(1, brs).Value.ToString
        refresh_DGVdetail(v_shipno.Text)
    End Sub


    Private Sub refresh_DGVdetail(ByVal v_shipno As String)
        Dim in_field As String
        Dim in_tbl As String = ""
        Dim dts As DataTable
        'Dim DGVDetail As New DataGridView
        in_field = "ts.shipment_no as ShipNo, getpoorder(ts.shipment_no,trim(tsd.po_no)) 'PONo', " & _
                    "tsd.po_item as 'POItem',TSD.Material_code as MaterialCode," & _
                    "tmm.material_name as 'MaterialName',tmc.country_name as CountryName," & _
                    "tpd.hs_code as 'HS Number',tpd.specification as Specification," & _
                    "tpd.quantity as 'PO Quantity', tpd.unit_code as 'Unit'," & _
                    "tsd.quantity as 'Actual Qty', tmpc.pack_name as PackName,tsd.Pack_Quantity as PackSize," & _
                    "tpd.Price, (tsd.quantity * tpd.Price) as 'Total Amount', tpd.note as Remark "
        in_tbl = "tbl_shipping as ts inner join tbl_shipping_detail as tsd on ts.shipment_no = tsd.shipment_no " & _
                    "inner join tbl_po_detail as tpd on tsd.po_no = tpd.po_no and tsd.po_item = tpd.po_item " & _
                    "inner join tbm_material as tmm on tsd.material_code = tmm.material_code " & _
                    "inner join tbm_country as tmc on tpd.country_code = tmc.country_code " & _
                    "inner join tbm_packing as tmpc on tsd.pack_code = tmpc.pack_code "
        SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tsd.shipment_no = '" & v_shipno & "' order by tsd.po_no, tsd.po_item"
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DGVDetail.DataSource = dts
        'If dts. > 0 Then
        'Show_Grid_JoinTable(DGVDetail, in_field, in_tbl)
        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then

            DGVDetail.Columns(0).Visible = False
            DGVDetail.Columns(2).Width = 70
            DGVDetail.Columns(4).Width = 150
            DGVDetail.Columns(10).Width = 60
            DGVDetail.Columns(12).Width = 60
            DGVDetail.Columns(14).Width = 60
            'DGVDetail.Columns(17).Width = 150

            DGVDetail.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGVDetail.Columns(9).DefaultCellStyle.Format = "N5"
            DGVDetail.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGVDetail.Columns(11).DefaultCellStyle.Format = "N5"
            DGVDetail.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGVDetail.Columns(14).DefaultCellStyle.Format = "N5"
            DGVDetail.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGVDetail.Columns(15).DefaultCellStyle.Format = "N2"
        End If
    End Sub


    Private Sub btnViewBL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewBL.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub DGVDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDetail.CellContentClick

    End Sub
End Class