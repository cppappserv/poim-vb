'Title         : List Import Data
'Form          : FrmListImportData
'Created By    : Prie
'Created Date  : 13 Mei 2009
'Last Modified : 27 Mei 2009
'Table Used    : 
'Modify        : 3 Sept 2009
'                - Referensi supplier dgn company di tutup 
'                - Revisi relasi dgn left join untuk purchasedby karena ada kemungkinan belum di isi
'                - Tambahkan kondisi untuk tglPO yg kosong
'                - Tambahkan Status PO
'                - Agar program bisa jalan untuk proses lebih dari 1x -> Object 'app' dan 'wb' di tempatkan di dalam.

Imports xlns = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel.Constants
Imports Microsoft.Office.Interop.Excel.XlBordersIndex
Imports Microsoft.Office.Interop.Excel.XlLineStyle
Imports Microsoft.Office.Interop.Excel.XlBorderWeight
Imports Microsoft.Office.Interop.Excel.XlHAlign
Imports vbs = Microsoft.VisualBasic.Strings


Public Class FrmListImportData
    Dim ErrMsg, SQLstr, SQLPO, SQLBL As String
    Dim affrow As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim file_name As String
    Dim MyDataReader As DataTableReader
    Dim vrow, vcol, vcol_akhir, vrow_datastart, vcol_Actual As Integer
    'Dim app As New xlns.Application
    'Dim wb As xlns.Workbook '= app.Workbooks.Add(xlns.XlWBATemplate.xlWBATWorksheet)


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
        PilihanDlg.SQLGrid = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-C' "
        PilihanDlg.SQLFilter = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
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
        PilihanDlg.SQLGrid = "select tc.company_code, tc.company_name from tbm_company as tc inner join tbm_users_company as tuc " & _
                             "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'"
        PilihanDlg.SQLFilter = "select tc.company_code, tc.company_name from tbm_company as tc inner join tbm_users_company as tuc " & _
                               "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'" & _
                               "and company_code LIKE 'FilterData1%' AND " & _
                               "company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company as tc inner join tbm_users_company as tuc on tc.COMPANY_CODE = tuc.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCompany_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
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

        SQLPO = "SELECT '' shipment_no, '' po_item, tp.po_no PO_NO, if(tp.purchaseddt is null,'',CAST(tp.purchaseddt AS CHAR)) TGL_PO, ms.supplier_name SUPPLIER, tp.contract_no CONTRACT, tp.ipa_no IPA, " & _
                 "mp.plant_name PLANT, CAST(CONCAT(tp.shipment_period_fr, ' s/d ', tp.shipment_period_to) AS CHAR) SHIPMENT_PERIOD, " & _
                 "tpd.price UNIT_PRICE, mm.material_name DESCRIPTION_OF_GOODS, tpd.hs_code HS_CODE, mr.country_name ORIGIN, " & _
                 "'' QUANTITY, tpd.unit_code UNIT, '' WEIGHT, mc.company_shortname COMPANY, " & _
                 "IF(tpd.package_code IS NULL,'',tpd.package_code) PACKAGE, tp.insurance_code SHIPMENT_TERM, tp.currency_code CURRENCY, " & _
                 "(tpd.price*tpd.quantity) ORIGINAL_PO_AMOUNT_IN_DIFF_CURR, tp.tolerable_delivery TOLERANCE, (tpd.price*tpd.quantity) ORIGINAL_PO_AMOUNT, '' FINAL_PO_AMOUNT_IN_DIFF_CURR, " & _
                 "'' EXCHANGE_RATE, '' FINAL_PO_AMOUNT, " & _
                 "'' FINAL_PLANT, '' SHIPPING_LINE, '' VESSEL, '' LOADPORT, " & _
                 "'' ETD, mo2.port_name DESTINATION, " & _
                 "'' ETA, '' ACTUAL_ETA, '' NO_CONTAINER, '' SIZE, " & _
                 "'' INVOICE_NO, '' INVOICE_DATE, '' VALUE_DATE, '' BUKTI_TRANSFER, " & _
                 "'' BL, tpd.specification PROTEIN, '' SGS, " & _
                 "'' SHIPPING_INSTRUCTION_DATE, '' SK_DEPTAN, '' DEPTAN_NO, '' TGL_DEPTAN, " & _
                 "mtc.class_name PAYMENT_TYPE, " & _
                 "'' OPENING_LC, '' LC_NO, '' REF_NO, " & _
                 "mt.payment_name TERM, " & _
                 "'' BANK_COMMISION_DATE, " & _
                 "'' COPY_RECEIVED, '' MCI_DATE, '' TT_DATE, '' DUE_DATE, '' INS, '' POLIS, " & _
                 "'' TANDA_TERIMA_PAJAK, '' TANDA_TERIMA_CS, '' COSTSLIP, " & _
                 "'' EXCHANGE_RATE_FOR_TAX_PURPOSE, '' BEA_MASUK, '' VAT, '' PPH22, '' PIB_DATE, '' AJU_NO, " & _
                 "'' ORIGINAL_RECEIVED, '' FORWARD_DOC, '' SPPB_DATE, '' SPPB_NO, " & _
                 "'' BAPB_NO, '' BAPB_DATE, '' BAPB_QUANTITY, " & _
                 "'' RECEIVER, " & _
                 "'' EMKL, '' REMARK, mu.name USER_NAME, IF(tp.status='Canceled','C','Y') PO_STATUS, " & _
                 "'' LC_REQUEST_DATE, '' OPENING_BANK_LC, " & _
                 "'' GKBI_LC_APPLICANT, '' GKBI_LC_APPROVAL, '' BANK_LC_APPLICANT, '' BANK_LC_APPROVAL, " & _
                 "'' SKDEPTAN_AJU " & _
                 "FROM tbl_po_detail tpd, tbl_po tp, " & _
                 "tbm_supplier ms, tbm_plant mp, tbm_material mm, tbm_company mc, " & _
                 "tbm_port mo2, tbm_payment_term mt, tbm_payment_class mtc, tbm_country mr, tbm_users mu " & _
                 "WHERE(tp.po_no = tpd.po_no And tp.supplier_code = ms.supplier_code And tp.plant_code = mp.plant_code) " & _
                 "AND tpd.material_code=mm.material_code AND tp.company_code=mc.company_code AND tp.port_code=mo2.port_code " & _
                 "AND tp.payment_code=mt.payment_code AND mt.class_code=mtc.class_code AND tpd.country_code = mr.country_code " & _
                 "AND tp.createdby=mu.user_ct " & _
                 "AND CONCAT(TRIM(tpd.po_no),TRIM(tpd.po_item)) NOT IN (SELECT CONCAT(TRIM(po_no),TRIM(po_item)) FROM tbl_shipping_detail) " & _
                 "AND tp.shipment_period_fr >= '" & vbs.Format(DTCreated1.Value, "yyyy-MM-dd") & "' and tp.shipment_period_to <= '" & vbs.Format(DTCreated2.Value, "yyyy-MM-dd") & "' " & _
                 "AND ('" & txtCompany_Code.Text & "' = '' OR tp.company_code ='" & txtCompany_Code.Text & "') " & _
                 "AND ('" & txtSuppCode.Text & "' = '' OR tp.supplier_code = '" & txtSuppCode.Text & "') " & _
                 "AND ('" & txtCreatedby.Text & "' = '' OR mu.name = '" & txtCreatedby.Text & "') "

        SQLBL = "SELECT t1.shipment_no, t1.po_item, t1.PO_NO, TGL_PO, SUPPLIER, CONTRACT, IPA, PLANT, SHIPMENT_PERIOD, UNIT_PRICE, DESCRIPTION_OF_GOODS, HS_CODE, ORIGIN, " & _
                 "QUANTITY, UNIT, WEIGHT, COMPANY, PACKAGE, SHIPMENT_TERM, CURRENCY, " & _
                 "ORIGINAL_PO_AMOUNT_IN_DIFF_CURR, TOLERANCE, ORIGINAL_PO_AMOUNT, FINAL_PO_AMOUNT_IN_DIFF_CURR, " & _
                 "EXCHANGE_RATE, FINAL_PO_AMOUNT, FINAL_PLANT, SHIPPING_LINE, VESSEL, if(LOADPORT is null,'',LOADPORT) LOADPORT, ETD, DESTINATION, ETA, ACTUAL_ETA, if(t2.NO_CONTAINER is null,'',t2.NO_CONTAINER) NO_CONTAINER, if(t2.SIZE is null, '', t2.SIZE) SIZE, " & _
                 "INVOICE_NO, INVOICE_DATE, VALUE_DATE, BUKTI_TRANSFER, BL, PROTEIN, SGS, if(t3.SHIPPING_INSTRUCTION_DATE is null,'',t3.SHIPPING_INSTRUCTION_DATE) SHIPPING_INSTRUCTION_DATE, " & _
                 "if(t4.SK_DEPTAN is null,'',t4.SK_DEPTAN) SK_DEPTAN, if(t4.DEPTAN_NO is null,'',t4.DEPTAN_NO) DEPTAN_NO, if(t4.TGL_DEPTAN is null, '',t4.TGL_DEPTAN) TGL_DEPTAN, " & _
                 "PAYMENT_TYPE, if(t5.OPENING_LC is null,'',t5.OPENING_LC) OPENING_LC, if(t5.LC_NO is null,'',t5.LC_NO) LC_NO, REF_NO, TERM, BANK_COMMISION_DATE, " & _
                 "COPY_RECEIVED, if(t6.MCI_DATE is null, '', t6.MCI_DATE) MCI_DATE, TT_DATE, DUE_DATE, INS, POLIS, TANDA_TERIMA_PAJAK, TANDA_TERIMA_CS, if(t7.COSTSLIP is null,'',t7.COSTSLIP) COSTSLIP, " & _
                 "EXCHANGE_RATE_FOR_TAX_PURPOSE, BEA_MASUK, VAT, PPH22, PIB_DATE, AJU_NO AJU_NO, " & _
                 "ORIGINAL_RECEIVED, FORWARD_DOC, SPPB_DATE, SPPB_NO, BAPB_NO, BAPB_DATE, BAPB_QUANTITY, RECEIVER, if(t8.EMKL is null,'',t8.EMKL) EMKL, REMARK, USER_NAME, PO_STATUS, " & _
                 "LC_REQUEST_DATE, OPENING_BANK_LC, GKBI_LC_APPLICANT, GKBI_LC_APPROVAL, BANK_LC_APPLICANT, BANK_LC_APPROVAL, SKDEPTAN_AJU " & _
                 "FROM " & _
                 "   (SELECT cast(ts.shipment_no as char) shipment_no, getpoorder(ts.shipment_no, trim(tsd.po_no)) PO_NO, if(tp.purchaseddt is null,'',CAST(tp.purchaseddt AS CHAR)) TGL_PO, ms.supplier_name SUPPLIER, tp.contract_no CONTRACT, tp.ipa_no IPA, " & _
                 "   mp.plant_name PLANT, CAST(CONCAT(tp.shipment_period_fr, ' s/d ', tp.shipment_period_to) AS CHAR) SHIPMENT_PERIOD, " & _
                 "   tpd.price UNIT_PRICE, cast(tsd.po_item as char) po_item, mm.material_name DESCRIPTION_OF_GOODS, tpd.hs_code HS_CODE, mr.country_name ORIGIN, " & _
                 "  CAST(tpd.quantity AS CHAR) QUANTITY, tpd.unit_code UNIT, CAST(tsd.quantity AS CHAR) WEIGHT, mc.company_shortname COMPANY, " & _
                 "  tsd.pack_code PACKAGE, tp.insurance_code SHIPMENT_TERM, CAST(ts.currency_code AS CHAR) CURRENCY, " & _
                 "  (tpd.price*tpd.quantity) ORIGINAL_PO_AMOUNT_IN_DIFF_CURR, tp.tolerable_delivery TOLERANCE, (tpd.price*tpd.quantity) ORIGINAL_PO_AMOUNT, CAST((tpd.price*tsd.quantity) AS CHAR) FINAL_PO_AMOUNT_IN_DIFF_CURR, " & _
                 "  '' EXCHANGE_RATE, CAST((tpd.price*tsd.quantity) AS CHAR) FINAL_PO_AMOUNT, " & _
                 "  mp2.plant_name FINAL_PLANT, ml.line_name SHIPPING_LINE, ts.vessel VESSEL, (SELECT port_name FROM tbm_port WHERE port_code=ts.loadport_code) LOADPORT, " & _
                 "  if(ts.est_delivery_dt is null,'',CAST(ts.est_delivery_dt AS CHAR)) ETD, mo2.port_name DESTINATION, " & _
                 "  if(ts.est_arrival_dt is null,'',CAST(ts.est_arrival_dt AS CHAR)) ETA, if(ts.notice_arrival_dt is null,'',CAST(ts.notice_arrival_dt AS CHAR)) ACTUAL_ETA, ts.total_container NO_CONTAINER, '' SIZE, " & _
                 "  tsv.invoice_no INVOICE_NO, if(tsv.invoice_dt is null,'',CAST(tsv.invoice_dt AS CHAR)) INVOICE_DATE, if(ts.due_dt is null,'',CAST(ts.due_dt AS CHAR)) VALUE_DATE, ts.account_no BUKTI_TRANSFER, " & _
                 "  ts.bl_no BL, tpd.specification PROTEIN, tsd.specification SGS," & _
                 "  '' SHIPPING_INSTRUCTION_DATE, '' SK_DEPTAN, '' DEPTAN_NO, '' TGL_DEPTAN, " & _
                 "  mtc.class_name PAYMENT_TYPE, " & _
                 "  '' OPENING_LC, '' LC_NO, '' REF_NO, " & _
                 "  mt.payment_name TERM, " & _
                 "  '' BANK_COMMISION_DATE, " & _
                 "  if(ts.received_copydoc_dt is null,'',CAST(ts.received_copydoc_dt AS CHAR)) COPY_RECEIVED, '' MCI_DATE, if(ts.tt_dt is null,'',CAST(ts.tt_dt AS CHAR)) TT_DATE, if(ts.due_dt is null,'',CAST(ts.due_dt AS CHAR)) DUE_DATE, CAST(ts.insurance_amount AS CHAR) INS, ts.insurance_no POLIS, " & _
                 "  '' TANDA_TERIMA_PAJAK, '' TANDA_TERIMA_CS, '' COSTSLIP, " & _
                 "  CAST(ts.kurs_pajak AS CHAR) EXCHANGE_RATE_FOR_TAX_PURPOSE, CAST(ts.BEA_MASUK AS CHAR) BEA_MASUK, CAST(ts.VAT AS CHAR) VAT, CAST(ts.pph21 AS CHAR) PPH22, if(ts.pib_dt is null,'',CAST(ts.pib_dt AS CHAR)) PIB_DATE, ts.AJU_NO AJU_NO, " & _
                 "  if(ts.received_doc_dt is null,'',CAST(ts.received_doc_dt AS CHAR)) ORIGINAL_RECEIVED, if(ts.forward_doc_dt is null,'',CAST(ts.forward_doc_dt AS CHAR)) FORWARD_DOC, if(ts.sppb_dt is null,'',CAST(ts.sppb_dt AS CHAR)) SPPB_DATE, ts.SPPB_NO SPPB_NO, " & _
                 "  '' BAPB_NO, '' BAPB_DATE, '' BAPB_QUANTITY, " & _
                 "  mp2.plant_name RECEIVER, " & _
                 "  '' EMKL, '' REMARK, mu.name USER_NAME, IF(tp.status='Canceled','C','Y') PO_STATUS, " & _
                 "  '' LC_REQUEST_DATE, '' OPENING_BANK_LC, " & _
                 "  '' GKBI_LC_APPLICANT, '' GKBI_LC_APPROVAL, '' BANK_LC_APPLICANT, '' BANK_LC_APPROVAL, " & _
                 "  '' SKDEPTAN_AJU " & _
                 "  FROM tbl_shipping_detail tsd, tbl_shipping ts, tbl_shipping_invoice tsv, tbl_po tp, tbl_po_detail tpd, " & _
                 "  tbm_supplier ms, tbm_plant mp, tbm_material mm, tbm_company mc, tbm_plant mp2, tbm_lines ml, " & _
                 "  tbm_port mo2, tbm_payment_term mt, tbm_payment_class mtc, tbm_country mr, tbm_users mu " & _
                 "  WHERE (ts.shipment_no=tsd.shipment_no) AND (tp.po_no=tsd.po_no) " & _
                 "  AND (tsd.po_no=tpd.po_no AND tsd.po_item=tpd.po_item) " & _
                 "  AND (tsd.shipment_no=tsv.shipment_no AND tsd.po_no=tsv.po_no AND tsd.po_item=tsv.ord_no) " & _
                 "  AND ts.supplier_code=ms.supplier_code AND tp.plant_code=mp.plant_code AND tsd.material_code=mm.material_code " & _
                 "  AND tp.company_code=mc.company_code AND ts.plant_code=mp2.plant_code AND ts.shipping_line=ml.line_code " & _
                 "  AND ts.port_code=mo2.port_code AND tp.payment_code=mt.payment_code AND mt.class_code=mtc.class_code " & _
                 "  AND tpd.country_code = mr.country_code AND tp.createdby=mu.user_ct " & _
                 "  AND tp.shipment_period_fr >= '" & vbs.Format(DTCreated1.Value, "yyyy-MM-dd") & "' and tp.shipment_period_to <= '" & vbs.Format(DTCreated2.Value, "yyyy-MM-dd") & "' " & _
                 "  AND ('" & txtCompany_Code.Text & "' = '' OR tp.company_code ='" & txtCompany_Code.Text & "') " & _
                 "  AND ('" & txtSuppCode.Text & "' = '' OR ts.supplier_code = '" & txtSuppCode.Text & "') " & _
                 "  AND ('" & txtCreatedby.Text & "' = '' OR mu.name = '" & txtCreatedby.Text & "') " & _
                 ") t1 " & _
                 "LEFT JOIN " & _
                 "(SELECT shipment_no, cast(COUNT(container_no) as char) no_container, cast(GROUP_CONCAT(DISTINCT(TRIM(unit_code)) SEPARATOR ',') as char) size FROM tbl_shipping_cont GROUP BY shipment_no) t2 ON t1.shipment_no=t2.shipment_no " & _
                 "LEFT JOIN " & _
                 "(SELECT po_no, cast(MIN(openingdt) as char) SHIPPING_INSTRUCTION_DATE FROM tbl_si WHERE STATUS <> 'Rejected' GROUP BY po_no) t3 ON t1.po_no=t3.po_no " & _
                 "LEFT JOIN " & _
                 "(SELECT po_no, cast(MIN(ril_no) as char) SK_DEPTAN, cast(MIN(deptan_no) as char) Deptan_No, CAST(MIN(issueddt) AS CHAR) TGL_DEPTAN FROM tbl_ril WHERE STATUS <> 'Rejected' GROUP BY po_no) t4 ON t1.po_no=t4.po_no " & _
                 "LEFT JOIN " & _
                 "(SELECT po_no, CAST(MIN(openingdt) AS CHAR) opening_lc, cast(GROUP_CONCAT(DISTINCT(lc_no) SEPARATOR ',') as char) lc_no FROM tbl_budget WHERE type_code='BOLC' AND STATUS <> 'Rejected' GROUP BY po_no) t5 ON t1.po_no=t5.po_no " & _
                 "LEFT JOIN " & _
                 "(SELECT shipment_no, CAST(MIN(openingdt) AS CHAR) mci_date FROM tbl_mci WHERE STATUS <> 'Rejected' GROUP BY shipment_no) t6 ON t1.shipment_no=t6.shipment_no " & _
                 "LEFT JOIN " & _
                 "(SELECT shipment_no, ord_no, CAST(findoc_valamt AS CHAR) costslip FROM tbl_shipping_doc WHERE findoc_type='CS' AND findoc_status <> 'Rejected') t7 ON t1.shipment_no=t7.shipment_no and t1.po_item=t7.ord_no " & _
                 "LEFT JOIN " & _
                 "(SELECT shipment_no, cast(company_name as char) emkl FROM tbl_shipping_doc, tbm_expedition WHERE findoc_to=company_code AND findoc_type='KO' AND findoc_status <> 'Rejected') t8 ON t1.shipment_no=t8.shipment_no " & _
                 ""

        If cbStatus.SelectedIndex = 1 Then
            SQLstr = SQLPO & " ORDER BY TGL_PO, PO_NO DESC "
        Else
            SQLstr = SQLBL & " ORDER BY TGL_PO, PO_NO DESC "
        End If

    End Sub

    Private Sub btnSup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSup.Click
        PilihanDlg.Text = "Select Supplier"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"

        PilihanDlg.SQLGrid = "select ts.supplier_code, ts.supplier_name from tbm_supplier as ts"

        PilihanDlg.SQLFilter = "select ts.supplier_code, ts.supplier_name from tbm_supplier as ts " & _
                               "where ts.supplier_code LIKE 'FilterData1%' AND " & _
                               "ts.supplier_name LIKE 'FilterData2%' "

        PilihanDlg.Tables = "tbm_supplier as ts"

        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSuppCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblSuppName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub FrmListPO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DTCreated1.Text = GetServerDate()
        DTCreated1.Value = DateAdd(DateInterval.Month, -3, Now)

        ''txtCreatedby.Text = UserData.UserName
        ''userct.Text = UserData.UserCT.ToString
        cbStatus.SelectedIndex = 0
        LblLocation.Visible = False
        TxtFileLocation.Visible = False
    End Sub

    Private Sub btnMatgrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PilihanDlg.Text = "Select Material Group"
        PilihanDlg.LblKey1.Text = "Material Code"
        PilihanDlg.SQLGrid = "SELECT * FROM tbm_material_group"
        PilihanDlg.SQLFilter = "SELECT * FROM tbm_material_group " & _
                               "WHERE group_code LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'txtMatGrp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            'lblMatGrp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        'If f_cekotorisasi_comp() = True Then
        If True = True Then
            DGVHeader.DataSource = Nothing
            DGVHeader.Columns.Clear()
            f_getpoheader()
            ErrMsg = "Failed when read PO"
            Dim dts As DataTable
            Dim Totrec As Integer = DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData)
            If Totrec > 0 Then
                dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
                DGVHeader.DataSource = dts
                DGVHeader.Columns(0).Visible = False
                DGVHeader.Columns(1).Visible = False
            Else
                MsgBox("Data not found......", MsgBoxStyle.Critical, "Attention")
            End If
        Else
            MsgBox("You are no authorized using this company code", MsgBoxStyle.Critical, "No Authorization")
        End If
    End Sub

    Private Sub CreateExcel(ByVal SQlStr As String)
        Dim app As New xlns.Application
        Dim wb As xlns.Workbook = app.Workbooks.Add(xlns.XlWBATemplate.xlWBATWorksheet)
        Dim xlsheet As New xlns.Worksheet
        Dim inApp As xlns.Application
        Dim xlwindow As xlns.Workbook
        xlsheet = CType(wb.ActiveSheet, xlns.Worksheet)
        Try
            app.Visible = False
            Dim vcol_payment, crow_estimate, jmlrec, vcol_start As Integer
            ErrMsg = "Gagal baca data detail."
            'Write judul dulu
            vrow = 2
            vcol = 1
            vcol_start = 1
            xlsheet.Cells(vrow, 1) = "Listing Import Data"
            vrow = vrow + 1
            'write selection screen dan 'write isi selection screen
            xlsheet.Cells(vrow, 1) = "Period     "
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigndate(DTCreated1.Value, DTCreated2.Value)
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = "Company    "
            xlsheet.Cells(vrow, 2) = ":  " & txtCompany_Code.Text
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = "Supplier   "
            xlsheet.Cells(vrow, 2) = ":  " & txtSuppCode.Text
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = "Creates By "
            xlsheet.Cells(vrow, 2) = ":  " & txtCreatedby.Text
            vrow = vrow + 1

            'write header
            vrow = 10
            vrow_datastart = 10
            vcol = 1
            xlsheet.Cells(vrow, vcol) = "SHIPMENT_NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PO_ITEM"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PO NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "TGL PO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SUPPLIER"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "CONTRACT NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "IPA"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PLANT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SHIPMENT PERIOD"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "UNIT PRICE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "DESCRIPTION OF GOODS"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "HS NUMBER"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "ORIGIN"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "QUANTITY"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "UNIT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "WEIGHT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "COMPANY"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PACKAGE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SHIPMENT TERM"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "CURRENCY"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "ORIGINAL PO AMOUNT IN DIFF CURR"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "TOLERANCE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "ORIGINAL PO AMOUNT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "FINAL PO AMOUNT IN DIFF CURR"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "EXCHANGE RATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "FINAL PO AMOUNT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "FINAL PLANT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SHIPPING LINE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "VESSEL"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "LOAD PORT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "ETD"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "DESTINATION"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "ETA"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "ACTUAL ETA"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "NO CONTAINER"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SIZE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "INVOICE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "INVOICE DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "VALUE DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BUKTI TRANSFER"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BL"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PROTEIN"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SGS"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SHIPPING INSTRUCTION DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SK DEPTAN"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "DEPTAN NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "TGL DEPT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PAYMENT TYPE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "OPENING LC"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "LC NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "REF NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "TERM"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BANK COMMISION DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "COPY RECEIVED"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "MCL DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "TT DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "DUE DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "INS"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "POLIS"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "TANDA TERIMA PAJAK"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "TANDA TERIMA CS"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "COST SLIP"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "EXCHANGE RATE FOR TAX"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BEA MASUK"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "VAT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PPH22"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PIB DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "AJU NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "ORIGINAL RECEIVED"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "FORWARD DOC"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SPPB DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SPPB NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BAPB NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BAPB DATE"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BAPB QUANTITY"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "RECEIVER"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "EMKL"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "REMARKS"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "USER NAME"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PO STATUS"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "GKBI_LC_APPLICANT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "GKBI_LC_APPROVAL"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BANK_LC_APPLICANT"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "BANK_LC_APPROVAL"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "SKDEPTAN_AJU"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter

            vcol = vcol + 1
            vcol_payment = vcol
            crow_estimate = vrow
            vcol_Actual = 83
            vcol_akhir = vcol - 1
            For i = 1 To vcol - 1
                Call f_kotak(10, i, vrow, i, wb)
            Next i
            'fill the data
            vrow = vrow + 1
            Dim Z, X As Integer
            MyDataReader = DBQueryDataReader(SQlStr, MyConn, ErrMsg, UserData)
            If Not MyDataReader Is Nothing Then
                While MyDataReader.Read
                    Z = 0
                    For X = 1 To vcol_Actual
                        xlsheet.Cells(vrow, Z + 1) = MyDataReader.GetValue(Z)

                        Z = Z + 1
                    Next X
                    vrow = vrow + 1
                End While
            End If
            CloseDataReader(MyDataReader, UserData)
            For i = 1 To vcol_Actual
                Call f_kotak(vrow_datastart, i, vrow - 1, i, wb)
            Next i

            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Name = "Tahoma"
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Size = 8
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_payment - 1)).EntireColumn.HorizontalAlignment = xlLeft
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.AutoFit()
            xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, 1)).Cells.Font.Size = 16

            'FOR ALL EXCEL
            xlsheet.Protect(DrawingObjects:=True, Contents:=True, Scenarios:=True, Password:="poimport")

            app.Windows(1).DisplayGridlines = False
            app.Windows(1).DisplayHeadings = False

            'Finally save the file
            file_name = "c:/" & UserData.UserId & "-" & vbs.Format(Now(), "ddMMyyyy-mmss") & ".xls"
            xlsheet.SaveAs(file_name)
            TxtFileLocation.Text = file_name
            xlsheet = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If (app IsNot Nothing) Then
                'app.Quit()
                app = Nothing
                Dim myProcesses() As Process
                Dim myProcess As Process
                myProcesses = Process.GetProcessesByName("EXCEL")

                For Each myProcess In myProcesses
                    myProcess.Kill()
                Next
            End If
        End Try
    End Sub


    Public Sub f_kotak(ByVal brsXLa As Integer, ByVal A As Integer, ByVal BrsXL As Integer, ByVal B As Integer, _
                       ByVal wb As xlns.Workbook)

        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B))
            .Select()
            .Borders(xlDiagonalDown).LineStyle = xlNone
            .Borders(xlDiagonalUp).LineStyle = xlNone
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeLeft)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeTop)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeBottom)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeRight)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim app As New xlns.Application
        Dim wb As xlns.Workbook = app.Workbooks.Add(xlns.XlWBATemplate.xlWBATWorksheet)
        Dim xlsheet As New xlns.Worksheet
        Dim inApp As xlns.Application
        Dim xlwindow As xlns.Workbook
        xlsheet = CType(wb.ActiveSheet, xlns.Worksheet)

        Dim file_name As String
        Dim StrColumn, StrData As String
        Dim i, j, k As Integer

        Try
            app.Visible = False
            ErrMsg = "Gagal baca data detail."
            'Write judul dulu
            xlsheet.Cells(1, 1) = Me.Text

            DGVHeader.CommitEdit(DataGridViewDataErrorContexts.Commit)
            For j = 2 To DGVHeader.ColumnCount - 1
                StrColumn = DGVHeader.Columns(j).HeaderText
                xlsheet.Cells(2, j) = StrColumn
            Next

            For i = 0 To DGVHeader.RowCount - 2
                For j = 2 To DGVHeader.ColumnCount - 1

                    StrColumn = DGVHeader.Columns(j).HeaderText
                    StrData = DGVHeader.Rows(i).Cells(StrColumn).Value.ToString

                    xlsheet.Cells(i + 3, j) = StrData
                Next
            Next
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1, 1)).Cells.Font.Size = 14
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(i + 3, j)).EntireColumn.AutoFit()

            'Finally save the file
            ''file_name = "c:/" & UserData.UserId & "-" & vbs.Format(Now(), "ddMMyyyy-mmss") & ".xls"
            ''xlsheet.SaveAs(file_name)
            xlsheet = Nothing
            app.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub btnsave_Old()
        LblLocation.Visible = True
        TxtFileLocation.Visible = True
        f_getpoheader()
        Dim Totrec As Integer = DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData)
        If Totrec > 0 Then
            CreateExcel(SQLstr)
        Else
            MsgBox("Data not found......", MsgBoxStyle.Critical, "Attention")
        End If
    End Sub
End Class