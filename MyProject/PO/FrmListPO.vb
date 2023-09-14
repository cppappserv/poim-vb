'Title        : List Purchase Order
'Form         : FrmPO
'Created By   : Hanny
'Created Date : 17 Oktober 2008
'Table Used   : 

'Imports POIM.FM02_MaterialGroup
'Imports POIM.FrmDOC_Import
Imports poim.FrmPO

Public Class FrmListPO
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
        'PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct UserCT,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-C' "
        PilihanDlg.SQLFilter = "Select tu.user_ct UserCT,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-C' " & _
                               "and tu.name LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Dim sender As System.Object
            'Select Case sender
            '    Case ""
            '        
            '    Case "App"
            'End Select
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
                               "and company_code LIKE 'FilterData1%' AND " & _
                               "company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company as tc inner join tbm_users_company as tuc on tc.COMPANY_CODE = tuc.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCompany_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If


    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        'refreshGrid(txtPO_NO.Text)
        If f_cekotorisasi_comp() = True Then
            f_getpoheader()
        Else
            MsgBox("You are no authorized using this company code", MsgBoxStyle.Critical, "No Authorization")
        End If
    End Sub
    Private Function f_cekotorisasi_comp() As Boolean
        Dim v_oke As String
        If txtCompany_Code.Text <> "" Then
            v_oke = AmbilData("company_code", "tbm_users_company", "USER_CT='" & userct.Text & "' and company_code = '" & txtCompany_Code.Text & "'")
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

        SQL1 = ""
        SQL2 = ""
        SQL3 = ""
        SQL4 = ""
        SQL5 = ""
        SQL6 = ""

        SQLstr = "select distinct tp.po_no as PONo,tp.company_code as CompanyCode,tmc.Company_name as CompanyName, " & _
                "tp.plant_code as PlantCode,tmp.plant_name as PlantName,  " & _
                "tp.port_code as PortCode,tmpo.port_name as PortName, tp.shipment_period_fr as PeriodeFrom,tp.shipment_period_to as PeriodeTo, " & _
                "if(tp.shipment_term_code ='P','Partial Shipment',if(tp.shipment_term_code='W','Whole Shipment','-')) as ShipmentTerm, " & _
                "tp.Supplier_code as SupplierCode,tms.supplier_name as SupplierName,tp.Contract_no as ContractNo, " & _
                "tp.ipa_no as IPA,tp.pr_no as PRNo,tp.payment_code as PaymentCode,tmpt.payment_name as PaymentName, " & _
                "tp.tolerable_delivery as TolerableDelivery, " & _
                "tp.insurance_code as Insurance,tp.currency_code as Currency,tp.kurs as Kurs,tp.status as Status,tbu.name as CreatedBy,tp.createddt as CreatedDate " & _
                "from tbl_po AS TP inner join tbl_po_detail as td on tp.po_no = td.po_no " & _
                "INNER JOIN TBM_MATERIAL AS TM ON td.material_code = tm.material_code " & _
                "inner join tbm_company as tmc on tp.company_code = tmc.company_code " & _
                "inner join tbm_plant as tmp on tp.plant_code = tmp.plant_code " & _
                "inner join tbm_port as tmpo on tp.port_code = tmpo.port_code " & _
                "inner join tbm_supplier as tms on tp.supplier_code = tms.supplier_code " & _
                "inner join tbm_payment_term as tmpt on tp.payment_code = tmpt.payment_code " & _
                "inner join tbm_users as tbu on tp.createdby=tbu.user_ct "

        If DTCreated1.Text <> "" And DTCreated2.Text <> "" Then
            SQL1 = "where tp.createddt >= '" & Format(DTCreated1.Value, "yyyy-MM-dd") & "' and tp.createddt <= '" & Format(DTCreated2.Value, "yyyy-MM-dd") & "' "
        ElseIf DTCreated1.Text <> "" Then
            SQL1 = "where tp.createddt >= '" & Format(DTCreated1.Value, "yyyy-MM-dd") & "' "
        Else
            SQL1 = "where tp.createddt <= '" & Format(DTCreated2.Value, "yyyy-MM-dd") & "' "
        End If

        If userct.Text <> "" And (txtPONO.Text = "") Then
            SQL2 = "and tp.CREATEDBY = '" & userct.Text & "' "
        End If

        If txtCompany_Code.Text <> "" Then
            SQL3 = "and tp.company_code ='" & txtCompany_Code.Text & "' "
        Else
            SQL3 = "and tp.company_code in (select company_code from tbm_users_company where user_ct = '" & userct.Text & "')"
        End If

        If txtSuppCode.Text <> "" Then
            SQL4 = "and tp.SUPPLIER_CODE = '" & txtSuppCode.Text & "' "
        End If

        If txtMatGrp.Text <> "" Then
            SQL5 = "and tm.group_code = '" & txtMatGrp.Text & "' "
        End If

        If txtPONO.Text <> "" Then
            SQL6 = "and tp.po_no LIKE '%" & txtPONO.Text & "%' "
        End If

        SQL7 = "order by tp.createddt desc"

        'If SQL1 <> "" Or SQL2 <> "" Or SQL3 <> "" Or SQL4 <> "" Or SQL5 <> "" Or SQL6 <> "" Then
        '    SQLstr = SQLstr & "where"
        'End If

        SQLstr = SQLstr & SQL1 & SQL2 & SQL3 & SQL4 & SQL5 & SQL6 & SQL7
        ErrMsg = "Failed when read PO"
        'MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        '.DataSource = Show_Grid(DGVHeader, "tbm_country")

        '    Dim in_field As String
        '    Dim in_tbl As String = ""
        Dim dts As DataTable
        'Dim DGVDetail As New DataGridView
        'in_field = "tpo.po_no, dpo.po_item,dpo.material_code,tmat.MATERIAL_name, dpo.country_code,tcou.COUNTRY_NAME, " & _
        '           "dpo.hs_code,dpo.SPECIFICATION,dpo.quantity,dpo.weight,dpo.unit_code,dpo.package_code,tpac.pack_name, " & _
        '           "dpo.price, dpo.note, dpo.shipment_no"
        'in_tbl = "tbl_po as tpo inner join tbl_po_detail as dpo on " & _
        '         "tpo.po_no = dpo.po_no inner join tbm_material as tmat on dpo.material_code = tmat.material_code " & _
        '         "inner join tbm_country as tcou on dpo.country_code = tcou.country_code " & _
        '         "inner join tbm_packing as tpac on dpo.package_code = tpac.PACK_CODE"
        'SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tpo.po_no = '" & vpo_no & "'"
        'ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        DGVHeader.DataSource = dts

        If DGVHeader.RowCount > 0 Then
            DGVHeader.Columns(0).Width = 150
            DGVHeader.CurrentCell = DGVHeader(0, 0)
            DGVHeader_CellClick(DGVHeader, ee)
        End If

        DGVHeader.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(17).DefaultCellStyle.Format = "N2"
        DGVHeader.Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVHeader.Columns(20).DefaultCellStyle.Format = "N2"
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
        'Dim f As New FrmListPO
        'f.MdiParent = Me
        'f.Show()    
        Me.Left = 50
        Me.Top = 50
        DTCreated1.Text = GetServerDate()
        DTCreated1.Value = DateAdd(DateInterval.Month, -3, Now)

        lblCompany_Name.Text = ""
        lblSuppName.Text = ""
        lblMatGrp.Text = ""

        txtCreatedby.Text = UserData.UserName
        userct.Text = UserData.UserCT.ToString
        DGVHeader.DataSource = Nothing
        DGVDetail.DataSource = Nothing
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
        'Dispose()
    End Sub

    Private Sub DGVHeader_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVHeader.CellClick
        Dim brs As Integer

        v_pono.Text = ""
        brs = 0
        brs = DGVHeader.CurrentCell.RowIndex
        v_pono.Text = DGVHeader.Item(0, brs).Value.ToString
        refresh_DGVdetail(v_pono.Text)
    End Sub


    Private Sub refresh_DGVdetail(ByVal v_pono As String)
        Dim in_field As String
        Dim in_tbl As String = ""
        Dim dts As DataTable
        'Dim DGVDetail As New DataGridView
        in_field = "dpo.po_item AS POItem,dpo.material_code as MaterialCode,tmat.MATERIAL_name as MaterialName, dpo.country_code as CountryCode,tcou.COUNTRY_NAME as CountryName, " & _
                   "dpo.hs_code as HSCode,dpo.SPECIFICATION as Specification,dpo.quantity as Quantity,dpo.unit_code as UnitCode,dpo.package_code as PackageCode,tpac.pack_name as PackName, " & _
                   "dpo.price as Price, dpo.note as Note "
        in_tbl = "tbl_po as tpo inner join tbl_po_detail as dpo on " & _
                 "tpo.po_no = dpo.po_no inner join tbm_material as tmat on dpo.material_code = tmat.material_code " & _
                 "inner join tbm_country as tcou on dpo.country_code = tcou.country_code " & _
                 "left join tbm_packing as tpac on dpo.package_code = tpac.PACK_CODE"
        SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tpo.po_no = '" & v_pono & "' order by dpo.po_item"
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DGVDetail.DataSource = dts
        'If dts. > 0 Then
        'Show_Grid_JoinTable(DGVDetail, in_field, in_tbl)
        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then

            'DGVDetail.Rows(0).Cells("po_item").Value = 1
            'DGVDetail.Columns("po_no").Visible = False
        End If

        DGVDetail.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(7).DefaultCellStyle.Format = "N5" '7 ' 5
        DGVDetail.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(8).DefaultCellStyle.Format = "N2" '8 ' 6
        DGVDetail.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(10).DefaultCellStyle.Format = "N2"
        DGVDetail.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(11).DefaultCellStyle.Format = "N2" ' 13 ' 9
        DGVDetail.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(12).DefaultCellStyle.Format = "N2" ' 13 ' 9
    End Sub


    Private Sub btnViewPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewPO.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class