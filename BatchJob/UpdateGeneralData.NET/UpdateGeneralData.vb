Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits System.Windows.Forms.Form
	Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim DestinationPlant As Object
		Dim xCREATEDDT As Object
		Dim xTdTerimaCostSlip As Object
		Dim xperKGs As Object
		Dim xCostSlipRate As Object
		Dim xCostSlip As Object
		Dim xInklaring As Object
		Dim xBudgetCAD As Object
		Dim xBudgetPIB As Object
		Dim xBudgetTT As Object
		Dim xMCI As Object
		Dim xCoverLetter As Object
		Dim xVoucherGiro As Object
		Dim xPaymentVoucher As Object
		Dim xBugdetRemitance As Object
		Dim xShippingInstruction As Object
		Dim xDeptan As Object
		Dim xRILno As Object
		Dim xDeptanNo As Object
		Dim xReqImportLisence As Object
		Dim xLCNo As Object
		Dim xBudgetOpeningLC As Object
		Dim xCreatedBy As Object
		Dim xSPPBNo As Object
		Dim xSPPBDate As Object
		Dim xAJU As Object
		Dim xPIBDate As Object
		Dim xPIBNo As Object
		Dim xDueDate As Object
		Dim xTTDate As Object
		Dim xInsuranceAmount As Object
		Dim xInsuranceNo As Object
		Dim xTdTerimaPajak As Object
		Dim xPIUD As Object
		Dim xPPH22 As Object
		Dim xVAT As Object
		Dim xBeaMasuk As Object
		Dim xTaxRate As Object
		Dim xVessel As Object
		Dim xShippingLine As Object
		Dim xLoadPort As Object
		Dim xFinalPort As Object
		Dim xFinalPlant As Object
		Dim xContainer As Object
		Dim xClearanceDate As Object
		Dim xDeliveryDate As Object
		Dim xFreeTime As Object
		Dim xActualArrival As Object
		Dim xETA As Object
		Dim xShipOnBoard As Object
		Dim xForwardDoc As Object
		Dim xOriginDoc As Object
		Dim xCopyDoc As Object
		Dim xContract As Object
		Dim xProdusen As Object
		Dim xSupplier As Object
		Dim xHostBL As Object
		Dim xBL As Object
		Dim xSGS As Object
		Dim xPackedUnit As Object
		Dim xPackedQuantity As Object
		Dim xInvoiceAmount As Object
		Dim xInvoiceDate As Object
		Dim xInvoiceNo As Object
		Dim xPOAmount As Object
		Dim xIncoterm As Object
		Dim xUnitPrice As Object
		Dim xCurrency As Object
		Dim xUnit As Object
		Dim xShipQuantity As Object
		Dim xPOQuantity As Object
		Dim xTolerable As Object
		Dim xProtein As Object
		Dim xHSCode As Object
		Dim xOrigin As Object
		Dim xDescriptionOfGoods As Object
		Dim xIPA As Object
		Dim xShipmentPeriodTo As Object
		Dim xShipmentPeriodFr As Object
		Dim xShipmentPeriod As Object
		Dim xShipmentTerm As Object
		Dim xDestinationPort As Object
		Dim xDestinationPlant As Object
		Dim xBussinesLine As Object
		Dim xCompany As Object
		Dim xPO As Object
		Dim xbl_no As Object
		Dim xord_no As Object
		Dim xpo_no As Object
		Dim xshipment_no As Object
		Dim rsdata1 As Object
		Dim rsdataWr As Object
		Dim strsqlw As Object
		Dim strsql As Object
		Dim strsqlB As Object
		Dim strsqlA As Object
		' Set ODBC untuk impr --- MySQL
		Dim cnn1 As Object
		Dim cnnWr As New ADODB.Connection
		Dim i As Object

        MsgBox("pingpong")
		cnn1 = New ADODB.Connection
		'UPGRADE_WARNING: Couldn't resolve default property of object cnn1.Open. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		cnn1.Open("DSN=impr;UID=root;pwd=vbdev")
		
		cnnWr = New ADODB.Connection
		cnnWr.Open("DSN=impr;UID=root;pwd=vbdev")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		strsqlA = "SELECT 0 shipment_no, pd.po_no, pd.po_item ord_no, '' bl_no, pd.po_no PO, my.company_name Company, my.line_bussines BussinesLine, mp.plant_name DestinationPlant, mt.port_name DestinationPort, " & "IF(po.shipment_term_code='P', 'Partial Shipment','Whole Shipment') ShipmentTerm, " & "IF(30-DATE_FORMAT(po.shipment_period_fr,'%d')+1 > DATE_FORMAT(po.shipment_period_to,'%d'), DATE_FORMAT(po.shipment_period_fr,'%M %Y'), DATE_FORMAT(po.shipment_period_to,'%M %Y')) ShipmentPeriod, CAST(po.shipment_period_fr AS CHAR) shipment_period_fr, CAST(po.shipment_period_to AS CHAR) shipment_period_to, " & "po.ipa_no IPA, mm.material_name DescriptionOfGoods, mc.country_name Origin, pd.hs_code HSCode, pd.specification Protein, po.tolerable_delivery Tolerable, pd.quantity POQuantity, 0 ShipQuantity, " & "pd.unit_code Unit, po.currency_code Currency, FORMAT(pd.price,2) UnitPrice, po.insurance_code Incoterm, FORMAT((pd.quantity * pd.price),2) POAmount, " & "'' InvoiceNo, '' InvoiceDate, 0 InvoiceAmount, 0 PackedQuantity, '' PackedUnit, '' SGS, '' BL, '' HostBL, " & "ms.supplier_name Supplier, ms2.produsen_name Produsen, po.contract_no Contract, '' CopyDoc, '' OriginDoc, '' ForwardDoc, CAST(IF(po.est_delivery_dt IS NULL,'',po.est_delivery_dt) AS CHAR) ShipOnBoard, " & "'' ETA, '' ActualArrival, 0 FreeTime, '' DeliveryDate, '' ClearanceDate, '' Container, '' FinalPlant, '' FinalPort, '' LoadPort, '' ShippingLine, '' Vessel, " & "FORMAT(0,2) TaxRate, FORMAT(0,2) BeaMasuk, FORMAT(0,2) VAT, FORMAT(0,2) PPH22, FORMAT(0,2) PIUD, '' TdTerimaPajak, '' InsuranceNo, FORMAT(0,2) InsuranceAmount, '' TTDate, '' DueDate, '' PIBNo, '' PIBDate, '' AJU, '' SPPBDate, '' SPPBNo, mu.name CreatedBy " & "FROM tbl_po_detail pd, tbl_po po, " & "tbm_company my, tbm_plant mp, tbm_port mt, tbm_material mm, tbm_material_group gm, tbm_country mc, tbm_supplier ms, tbm_produsen ms2, tbm_users mu "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		strsqlA = strsqlA & " " & "Where (pd.po_no = po.po_no) " & "AND po.company_code=my.company_code AND po.plant_code=mp.plant_code AND po.port_code=mt.port_code " & "AND pd.material_code=mm.material_code AND mm.group_code=gm.group_code AND pd.country_code=mc.country_code AND po.supplier_code=ms.supplier_code " & "AND po.produsen_code=ms2.produsen_code AND po.createdby = mu.user_ct " & "AND po.po_no NOT IN (SELECT po_no FROM tbl_shipping_detail) "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		strsqlB = "SELECT sd.shipment_no, sd.po_no, sd.po_item ord_no, ss.bl_no, getpoorder(sd.shipment_no, TRIM(sd.po_no)) PO, " & "mc.company_name Company, mc.line_bussines BussinesLine, mp2.plant_name DestinationPlant, mo3.port_name DestinationPort, " & "IF(po.shipment_term_code='P', 'Partial Shipment','Whole Shipment') ShipmentTerm, " & "IF(30-DATE_FORMAT(po.shipment_period_fr,'%d')+1 > DATE_FORMAT(po.shipment_period_to,'%d'), DATE_FORMAT(po.shipment_period_fr,'%M %Y'), DATE_FORMAT(po.shipment_period_to,'%M %Y')) ShipmentPeriod, CAST(po.shipment_period_fr AS CHAR) shipment_period_fr, CAST(po.shipment_period_to AS CHAR) shipment_period_to, " & "po.IPA_no IPA, " & "mm.material_name DescriptionOfGoods, my.country_name Origin, pd.hs_code HSCode, pd.specification Protein, po.tolerable_delivery Tolerable, " & "pd.Quantity POQuantity, sd.Quantity ShipQuantity, pd.unit_code Unit, " & "po.currency_code Currency, FORMAT(pd.price,2) UnitPrice, po.insurance_code Incoterm, FORMAT((pd.quantity * pd.price),2) POAmount, sv.invoice_no InvoiceNo, CAST(sv.invoice_dt AS CHAR) InvoiceDate, (sv.invoice_amount-sv.invoice_penalty) InvoiceAmount, " & "sd.pack_quantity PackedQuantity, ma.pack_name PackedUnit, sd.specification SGS, " & "ss.bl_no BL, ss.HostBL HostBL, ms.supplier_name Supplier, mn.produsen_name Produsen, po.contract_no Contract, " & "CAST(IF(ss.received_copydoc_dt IS NULL,'',ss.received_copydoc_dt) AS CHAR) CopyDoc, CAST(IF(ss.received_doc_dt IS NULL,'',ss.received_doc_dt) AS CHAR) OriginDoc, CAST(IF(ss.forward_doc_dt IS NULL,'',ss.forward_doc_dt) AS CHAR) ForwardDoc, " & "CAST(IF(ss.est_delivery_dt IS NULL,'',ss.est_delivery_dt) AS CHAR) ShipOnBoard, CAST(IF(ss.est_arrival_dt IS NULL,'',ss.est_arrival_dt) AS CHAR) ETA, CAST(IF(ss.Notice_arrival_dt IS NULL,'',ss.Notice_arrival_dt) AS CHAR) ActualArrival, " & "IF(ss.free_time IS NULL,0,ss.free_time) FreeTime, CAST(IF(ss.clr_dt IS NULL, IF(ss.est_clr_dt IS NULL,'',ss.est_clr_dt), ss.clr_dt) AS CHAR) DeliveryDate, " & "CAST(IF(ss.bapb_dt IS NULL, IF(ss.est_bapb_dt IS NULL,'',ss.est_bapb_dt), ss.bapb_dt) AS CHAR) ClearanceDate, IF(ss.total_container IS NULL,'',ss.total_container) Container, " & "mp.plant_name FinalPlant, mo.port_name FinalPort, mo2.port_name LoadPort, ml.line_name ShippingLine, ss.Vessel Vessel, " & "FORMAT(ss.kurs_pajak,2) TaxRate, FORMAT(ss.bea_masuk,2) BeaMasuk, FORMAT(ss.Vat,2) VAT, FORMAT(ss.pph21,2) PPH22, FORMAT(ss.PIUD,2) PIUD, CAST(IF(ss.verified2dt IS NULL,'',ss.verified2dt) AS CHAR) TdTerimaPajak, ss.insurance_no InsuranceNo, FORMAT(ss.insurance_amount,2) InsuranceAmount, " & "CAST(IF(ss.tt_dt IS NULL,'',ss.tt_dt) AS CHAR) TTDate, CAST(IF(ss.due_dt IS NULL,'',ss.due_dt) AS CHAR) DueDate, ss.pib_no PIBNo, CAST(IF(ss.pib_dt IS NULL,'',ss.pib_dt) AS CHAR) PIBDate, ss.aju_no AJU, CAST(IF(ss.sppb_dt IS NULL,'',ss.sppb_dt) AS CHAR) SPPBDate, " & "ss.sppb_no SPPBNo, mu.name CreatedBy " & "FROM tbl_shipping_detail sd, tbl_shipping ss, tbl_po_detail pd, tbl_po po, tbl_shipping_invoice sv, " & "tbm_material mm, tbm_material_group gm, tbm_packing ma, tbm_supplier ms, tbm_plant mp, tbm_port mo, tbm_port mo2, tbm_lines ml, tbm_users mu, " & "tbm_country my, tbm_company mc, tbm_plant mp2, tbm_port mo3, tbm_produsen mn "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		strsqlB = strsqlB & " " & "Where (sd.shipment_no = ss.shipment_no) " & "AND sd.po_no=pd.po_no AND sd.po_item=pd.po_item " & "AND sd.po_no=po.po_no " & "AND sd.shipment_no=sv.shipment_no AND sd.po_no=sv.po_no AND sd.po_item=sv.ord_no " & "AND sd.material_code=mm.material_code AND mm.group_code=gm.group_code AND sd.pack_code=ma.pack_code " & "AND ss.supplier_code=ms.supplier_code AND ss.plant_code=mp.plant_code AND ss.port_code=mo.port_code AND ss.loadport_code=mo2.port_code " & "AND ss.shipping_line=ml.line_code AND ss.createdby=mu.user_ct AND pd.country_code=my.country_code " & "AND po.company_code=mc.company_code AND po.plant_code=mp2.plant_code AND po.port_code=mo3.port_code " & "AND po.produsen_code=mn.produsen_code "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object strsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		strsql = "SELECT t1.*, CAST(IF(d1.BudgetOpeningLC IS NULL,'',d1.BudgetOpeningLC) AS CHAR) BudgetOpeningLC, CAST(IF(d0.LCNo IS NULL,'',d0.LCNo) AS CHAR) LCNo, " & "CAST(IF(d2.ReqImportLisence_po IS NULL, IF(d4.ReqImportLisence_bl IS NULL,'',d4.ReqImportLisence_bl), d2.ReqImportLisence_po) AS CHAR) ReqImportLisence, " & "CAST(IF(d20.RILNo_po IS NULL, IF(d19.RILNo_bl IS NULL,'',d19.RILNo_bl),  d20.RILNo_po) AS CHAR) RILNo, " & "CAST(IF(d22.Deptan_po IS NULL, IF(d21.Deptan_bl IS NULL,'',d21.Deptan_bl), d22.Deptan_po) AS CHAR) Deptan, " & "CAST(IF(d17.DeptanNo_po IS NULL, IF(d16.DeptanNo_bl IS NULL,'',d16.DeptanNo_bl),  d17.DeptanNo_po) AS CHAR) DeptanNo, " & "CAST(IF(d3.ShippingInstruction_po IS NULL, IF(d5.ShippingInstruction_bl IS NULL,'',d5.ShippingInstruction_bl), d3.ShippingInstruction_po) AS CHAR) ShippingInstruction, " & "CAST(IF(d6.BugdetRemitance IS NULL,'',d6.BugdetRemitance) AS CHAR) BugdetRemitance, CAST(IF(d7.PaymentVoucher IS NULL,'',d7.PaymentVoucher) AS CHAR) PaymentVoucher, CAST(IF(d8.VoucherGiro IS NULL,'',d8.VoucherGiro) AS CHAR) VoucherGiro, CAST(IF(d9.CoverLetter IS NULL,'',d9.CoverLetter) AS CHAR) CoverLetter, " & "CAST(IF(d12.MCI IS NULL,'',d12.MCI) AS CHAR) MCI, CAST(IF(d13.BudgetTT IS NULL,'',d13.BudgetTT) AS CHAR) BudgetTT, CAST(IF(d14.BudgetPIB IS NULL,'',d14.BudgetPIB) AS CHAR) BudgetPIB, CAST(IF(d15.BudgetCAD IS NULL,'',d15.BudgetCAD) AS CHAR) BudgetCAD, " & "CAST(IF(d10.Inklaring IS NULL,'',d10.Inklaring) AS CHAR) Inklaring, CAST(IF(d11.CostSlip IS NULL,'',d11.CostSlip) AS CHAR) CostSlip, cast(IF(d18.effective_kurs IS NULL,'',format(d18.effective_kurs,2)) as char) CostSlipRate, cast(IF(d11.perKGs IS NULL,'',if(d11.perKGs=0,'',format(d11.perKGs,2))) as char) perKGs, CAST(IF(d11.TdTerimaCostSlip IS NULL,'',d11.TdTerimaCostSlip) AS CHAR) TdTerimaCostSlip, cast(NOW() as char) CREATEDDT FROM " & "(" & strsqlA & " " & "UNION " & " " & strsqlB & ") t1 "
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object strsql. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		strsql = strsql & " " & "LEFT JOIN (SELECT po_no, MIN(OpeningDt) BudgetOpeningLC FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d1 ON d1.po_no=t1.po_no " & "LEFT JOIN (SELECT po_no, GROUP_CONCAT(lc_no SEPARATOR ',') LCNo FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d0 ON d0.po_no=t1.po_no " & "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ReqImportLisence_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d2 ON d2.po_no=t1.po_no " & "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ShippingInstruction_po FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d3 ON d3.po_no=t1.po_no " & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) ReqImportLisence_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d4 ON d4.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) ShippingInstruction_bl FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d5 ON d5.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BugdetRemitance FROM tbl_remitance WHERE STATUS<>'Rejected' GROUP BY shipment_no) d6 ON d6.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) PaymentVoucher FROM tbl_shipping_doc WHERE findoc_type='PV' AND findoc_status<>'Rejected' GROUP BY shipment_no) d7 ON d7.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) VoucherGiro FROM tbl_shipping_doc WHERE findoc_type='VG' AND findoc_status<>'Rejected' GROUP BY shipment_no) d8 ON d8.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) CoverLetter FROM tbl_shipping_doc WHERE findoc_type='CL' AND findoc_status<>'Rejected' GROUP BY shipment_no) d9 ON d9.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) Inklaring FROM tbl_shipping_doc WHERE findoc_type='CC' AND findoc_status<>'Rejected' GROUP BY shipment_no) d10 ON d10.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) CostSlip, MIN(FINDOC_VALAMT) perKGs, CAST(MIN(findoc_finappdt) AS CHAR) TdTerimaCostSlip FROM tbl_shipping_doc WHERE findoc_type='CS' AND findoc_status<>'Rejected' GROUP BY shipment_no) d11 ON d11.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) MCI FROM tbl_mci WHERE STATUS<>'Rejected' GROUP BY shipment_no) d12 ON d12.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetTT FROM tbl_budgets WHERE type_code='TT' AND STATUS<>'Rejected' GROUP BY shipment_no) d13 ON d13.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetPIB FROM tbl_budgets WHERE type_code='BP' AND STATUS<>'Rejected' GROUP BY shipment_no) d14 ON d14.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetCAD FROM tbl_budgets WHERE type_code='CA' AND STATUS<>'Rejected' GROUP BY shipment_no) d15 ON d15.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT shipment_no, MIN(deptan_no) DeptanNo_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL AND deptan_no <> '' GROUP BY shipment_no) d16 ON d16.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT po_no, MIN(deptan_no) DeptanNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL AND deptan_no <> '' GROUP BY po_no) d17 ON d17.po_no=t1.po_no " & "LEFT JOIN tbm_kurs d18 ON d18.currency_code=t1.Currency AND d18.effective_date=t1.ShipOnBoard " & "LEFT JOIN (SELECT shipment_no, MIN(ril_no) RILNo_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d19 ON d19.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT po_no, MIN(ril_no) RILNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d20 ON d20.po_no=t1.po_no " & "LEFT JOIN (SELECT shipment_no, MIN(IssuedDt) Deptan_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d21 ON d21.shipment_no=t1.shipment_no " & "LEFT JOIN (SELECT po_no, MIN(IssuedDt) Deptan_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d22 ON d22.po_no=t1.po_no " & " "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlw. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		strsqlw = "Delete from tmp_generaldata"
		'UPGRADE_WARNING: Couldn't resolve default property of object strsqlw. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		rsdataWr = cnnWr.Execute(strsqlw)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object cnn1.Execute. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		rsdata1 = cnn1.Execute(strsql)
		'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1.EOF. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If Not rsdata1.EOF Then
			'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1.EOF. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Do While Not rsdata1.EOF
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xshipment_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xshipment_no = rsdata1("shipment_no")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xpo_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xpo_no = rsdata1("po_no")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xord_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xord_no = rsdata1("ord_no")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xbl_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xbl_no = rsdata1("bl_no")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPO. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPO = rsdata1("PO")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCompany. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCompany = rsdata1("Company")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBussinesLine. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBussinesLine = rsdata1("BussinesLine")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDestinationPlant. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xDestinationPlant = rsdata1("DestinationPlant")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDestinationPort. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xDestinationPort = rsdata1("DestinationPort")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentTerm. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShipmentTerm = rsdata1("ShipmentTerm")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentPeriod. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShipmentPeriod = rsdata1("ShipmentPeriod")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentPeriodFr. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShipmentPeriodFr = rsdata1("shipment_period_fr")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentPeriodTo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShipmentPeriodTo = rsdata1("shipment_period_to")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xIPA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xIPA = rsdata1("IPA")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDescriptionOfGoods. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xDescriptionOfGoods = rsdata1("DescriptionOfGoods")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xOrigin. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xOrigin = rsdata1("Origin")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xHSCode. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xHSCode = rsdata1("HSCode")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xProtein. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xProtein = rsdata1("Protein")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTolerable. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xTolerable = rsdata1("Tolerable")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPOQuantity. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPOQuantity = rsdata1("POQuantity")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipQuantity. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShipQuantity = rsdata1("ShipQuantity")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xUnit. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xUnit = rsdata1("Unit")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCurrency. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCurrency = rsdata1("Currency")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xUnitPrice. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xUnitPrice = rsdata1("UnitPrice")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xIncoterm. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xIncoterm = rsdata1("Incoterm")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPOAmount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPOAmount = rsdata1("POAmount")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInvoiceNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xInvoiceNo = rsdata1("InvoiceNo")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInvoiceDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xInvoiceDate = rsdata1("InvoiceDate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInvoiceAmount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xInvoiceAmount = rsdata1("InvoiceAmount")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPackedQuantity. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPackedQuantity = rsdata1("PackedQuantity")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPackedUnit. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPackedUnit = rsdata1("PackedUnit")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSGS. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xSGS = rsdata1("SGS")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBL = rsdata1("BL")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xHostBL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xHostBL = rsdata1("HostBL")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSupplier. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xSupplier = rsdata1("Supplier")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xProdusen. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xProdusen = rsdata1("Produsen")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xContract. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xContract = rsdata1("Contract")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCopyDoc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCopyDoc = rsdata1("CopyDoc")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xOriginDoc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xOriginDoc = rsdata1("OriginDoc")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xForwardDoc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xForwardDoc = rsdata1("ForwardDoc")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipOnBoard. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShipOnBoard = rsdata1("ShipOnBoard")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xETA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xETA = rsdata1("ETA")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xActualArrival. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xActualArrival = rsdata1("ActualArrival")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xFreeTime. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xFreeTime = rsdata1("FreeTime")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDeliveryDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xDeliveryDate = rsdata1("DeliveryDate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xClearanceDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xClearanceDate = rsdata1("ClearanceDate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xContainer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xContainer = rsdata1("Container")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xFinalPlant. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xFinalPlant = rsdata1("FinalPlant")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xFinalPort. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xFinalPort = rsdata1("FinalPort")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xLoadPort. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xLoadPort = rsdata1("LoadPort")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShippingLine. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShippingLine = rsdata1("ShippingLine")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xVessel. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xVessel = rsdata1("Vessel")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTaxRate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xTaxRate = rsdata1("TaxRate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBeaMasuk. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBeaMasuk = rsdata1("BeaMasuk")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xVAT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xVAT = rsdata1("VAT")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPPH22. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPPH22 = rsdata1("PPH22")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPIUD. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPIUD = rsdata1("PIUD")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTdTerimaPajak. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xTdTerimaPajak = rsdata1("TdTerimaPajak")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInsuranceNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xInsuranceNo = rsdata1("InsuranceNo")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInsuranceAmount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xInsuranceAmount = rsdata1("InsuranceAmount")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTTDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xTTDate = rsdata1("TTDate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDueDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xDueDate = rsdata1("DueDate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPIBNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPIBNo = rsdata1("PIBNo")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPIBDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPIBDate = rsdata1("PIBDate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xAJU. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xAJU = rsdata1("AJU")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSPPBDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xSPPBDate = rsdata1("SPPBDate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSPPBNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xSPPBNo = rsdata1("SPPBNo")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCreatedBy. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCreatedBy = rsdata1("CreatedBy")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetOpeningLC. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBudgetOpeningLC = rsdata1("BudgetOpeningLC")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xLCNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xLCNo = rsdata1("LCNo")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xReqImportLisence. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xReqImportLisence = rsdata1("ReqImportLisence")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDeptanNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xDeptanNo = rsdata1("DeptanNo")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xRILno. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xRILno = rsdata1("RILNo")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDeptan. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xDeptan = rsdata1("Deptan")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShippingInstruction. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xShippingInstruction = rsdata1("ShippingInstruction")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBugdetRemitance. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBugdetRemitance = rsdata1("BugdetRemitance")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPaymentVoucher. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xPaymentVoucher = rsdata1("PaymentVoucher")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xVoucherGiro. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xVoucherGiro = rsdata1("VoucherGiro")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCoverLetter. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCoverLetter = rsdata1("CoverLetter")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xMCI. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xMCI = rsdata1("MCI")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetTT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBudgetTT = rsdata1("BudgetTT")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetPIB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBudgetPIB = rsdata1("BudgetPIB")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetCAD. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xBudgetCAD = rsdata1("BudgetCAD")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInklaring. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xInklaring = rsdata1("Inklaring")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCostSlip. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCostSlip = rsdata1("CostSlip")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCostSlipRate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCostSlipRate = rsdata1("CostSlipRate")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xperKGs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xperKGs = rsdata1("perKGs")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTdTerimaCostSlip. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xTdTerimaCostSlip = rsdata1("TdTerimaCostSlip")
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCREATEDDT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				xCREATEDDT = rsdata1("CREATEDDT")
				
				
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentPeriodTo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentPeriodFr. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDeptan. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xRILno. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCREATEDDT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTdTerimaCostSlip. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xperKGs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCostSlipRate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCostSlip. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInklaring. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetCAD. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetPIB. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetTT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xMCI. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCoverLetter. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xVoucherGiro. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPaymentVoucher. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBugdetRemitance. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShippingInstruction. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDeptanNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xReqImportLisence. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xLCNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBudgetOpeningLC. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCreatedBy. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSPPBNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSPPBDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xAJU. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPIBDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPIBNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDueDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTTDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInsuranceAmount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInsuranceNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTdTerimaPajak. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPIUD. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPPH22. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xVAT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBeaMasuk. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTaxRate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xVessel. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShippingLine. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xLoadPort. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xFinalPort. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xFinalPlant. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xContainer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xClearanceDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDeliveryDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xFreeTime. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xActualArrival. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xETA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipOnBoard. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xForwardDoc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xOriginDoc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCopyDoc. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xContract. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xProdusen. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSupplier. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xHostBL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xSGS. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPackedUnit. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPackedQuantity. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInvoiceAmount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInvoiceDate. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xInvoiceNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPOAmount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xIncoterm. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xUnitPrice. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCurrency. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xUnit. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipQuantity. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPOQuantity. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xTolerable. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xProtein. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xHSCode. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xOrigin. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDescriptionOfGoods. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xIPA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentPeriod. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xShipmentTerm. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xDestinationPort. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object DestinationPlant. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xBussinesLine. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xCompany. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xPO. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xbl_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xord_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xpo_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object xshipment_no. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				'UPGRADE_WARNING: Couldn't resolve default property of object strsqlw. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				strsqlw = "INSERT INTO tmp_generaldata VALUES ('" & xshipment_no & "','" & xpo_no & "','" & xord_no & "','" & xbl_no & "','" & xPO & "','" & xCompany & "','" & xBussinesLine & "'," & "'" & DestinationPlant & "','" & xDestinationPort & "','" & xShipmentTerm & "','" & xShipmentPeriod & "','" & xIPA & "'," & "'" & xDescriptionOfGoods & "','" & xOrigin & "','" & xHSCode & "','" & xProtein & "','" & xTolerable & "','" & xPOQuantity & "','" & xShipQuantity & "','" & xUnit & "','" & xCurrency & "','" & xUnitPrice & "','" & xIncoterm & "','" & xPOAmount & "'," & "'" & xInvoiceNo & "','" & xInvoiceDate & "','" & xInvoiceAmount & "','" & xPackedQuantity & "','" & xPackedUnit & "','" & xSGS & "','" & xBL & "','" & xHostBL & "','" & xSupplier & "','" & xProdusen & "','" & xContract & "'," & "'" & xCopyDoc & "','" & xOriginDoc & "','" & xForwardDoc & "','" & xShipOnBoard & "','" & xETA & "','" & xActualArrival & "','" & xFreeTime & "','" & xDeliveryDate & "','" & xClearanceDate & "','" & xContainer & "'," & "'" & xFinalPlant & "','" & xFinalPort & "','" & xLoadPort & "','" & xShippingLine & "','" & xVessel & "','" & xTaxRate & "','" & xBeaMasuk & "','" & xVAT & "','" & xPPH22 & "','" & xPIUD & "','" & xTdTerimaPajak & "'," & "'" & xInsuranceNo & "','" & xInsuranceAmount & "','" & xTTDate & "','" & xDueDate & "','" & xPIBNo & "','" & xPIBDate & "','" & xAJU & "','" & xSPPBDate & "','" & xSPPBNo & "','" & xCreatedBy & "','" & xBudgetOpeningLC & "','" & xLCNo & "'," & "'" & xReqImportLisence & "','" & xDeptanNo & "','" & xShippingInstruction & "','" & xBugdetRemitance & "','" & xPaymentVoucher & "','" & xVoucherGiro & "','" & xCoverLetter & "','" & xMCI & "'," & "'" & xBudgetTT & "','" & xBudgetPIB & "','" & xBudgetCAD & "','" & xInklaring & "','" & xCostSlip & "','" & xCostSlipRate & "','" & xperKGs & "','" & xTdTerimaCostSlip & "','" & xCREATEDDT & "','" & xRILno & "','" & xDeptan & "','" & xShipmentPeriodFr & "','" & xShipmentPeriodTo & "')"
				
				'UPGRADE_WARNING: Couldn't resolve default property of object strsqlw. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				rsdataWr = cnnWr.Execute(strsqlw)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object rsdata1.MoveNext. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				rsdata1.MoveNext()
			Loop 
		End If
		
		'UPGRADE_NOTE: Object rsdata1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rsdata1 = Nothing
		'UPGRADE_NOTE: Object rsdataWr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rsdataWr = Nothing
		'UPGRADE_NOTE: Object cnn1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		cnn1 = Nothing
		'UPGRADE_NOTE: Object cnnWr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		cnnWr = Nothing
		Me.Close()
	End Sub
End Class